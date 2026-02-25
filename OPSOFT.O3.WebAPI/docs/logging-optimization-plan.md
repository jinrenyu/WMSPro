# 日志系统优化方案

> 基于安全合规、性能可靠性、架构功能三个维度的深度审查，整理出以下优化方案。

## 一、现有日志体系概览

| 日志类别 | 实现方式 | 存储位置 | 查询API |
|---------|---------|---------|---------|
| 操作日志 | `OperationLogService` → `SYS_OPERATIONLOG` | 数据库 | 分页查询 + 统计 + CSV导出 |
| 审计日志 | `AuditLogService` → `SYS_AUDITLOG` + `SYS_AUDITLOGENTRY` | 数据库 | 分页查询 + 变更历史 + CSV导出 |
| 登录日志 | `LoginInfoService` → `SYS_LOGININFO` | 数据库 | 分页查询 |
| 请求日志 | `RequestLoggingMiddleware` → Serilog | 文件 | 无API |
| SQL日志 | SqlSugar AOP → Serilog | 文件 | 无API |

辅助设施：`CorrelationIdMiddleware`、`GlobalExceptionMiddleware`、`LogArchivalService`、`LogExportService`

---

## 二、优化项清单

### P0 — 必须立即修复

#### 1. fire-and-forget 日志写入静默丢失

- **文件**: `CrudService.cs`、`DocumentService.cs`、`ApprovableCrudService.cs`、`DisableableCrudService.cs` 等
- **问题**: `_ = OperationLog.LogAsync(...)` 丢弃 Task，异常被静默吞掉。`OperationLogService` 注册为 Scoped，HTTP 请求结束后 `ISqlSugarClient` 可能已 Dispose，导致 `ObjectDisposedException`
- **方案**: 引入 `Channel<T>` 内存队列 + `BackgroundService` 后台消费者，将日志写入解耦为异步队列消费
  - 创建 `LogQueueService`（Singleton），内部持有 `Channel<OperationLogMessage>`
  - 创建 `LogQueueConsumer`（BackgroundService），从队列消费并批量写入数据库
  - 各 Service 改为向队列投递消息，而非直接调用 `LogAsync`
  - 消费失败时写入本地文件兜底
- **验收标准**: 日志写入不再阻塞业务流程，且无静默丢失

#### 2. 审计日志功能形同虚设

- **文件**: `AuditLogService.cs`、`CrudService.cs`
- **问题**: `LogChangeAsync` 全项目无任何业务调用点，审计变更追踪完全未生效；需要手动构建 `AuditChangeItem`
- **方案**: 利用 SqlSugar 的 Diff 功能自动捕获变更
  - 在 `Repository.UpdateAsync` 中，更新前先查询旧数据
  - 使用 SqlSugar 的 `GetDiffColumns` 或手动对比生成变更列表
  - 自动调用 `AuditLogService.LogChangeAsync`
  - 对敏感字段（如密码）在写入审计日志前进行掩码
- **验收标准**: 所有通过 `CrudService.UpdateAsync` 和 `DeleteAsync` 的操作自动生成审计记录

#### 3. 审计日志主表/明细写入非原子性

- **文件**: `AuditLogService.cs:55-76`
- **问题**: 先插主表再插明细，无事务保护，中间失败会产生孤立记录
- **方案**: 使用 `_db.Ado.BeginTranAsync()` 将两次插入包裹在同一事务中
- **验收标准**: 主表和明细要么同时成功，要么同时回滚

#### 4. 日志控制器无权限控制

- **文件**: `OperationLogController.cs`、`AuditLogController.cs`
- **问题**: 仅 `[Authorize]`，任何登录用户都能查看所有日志并导出
- **方案**:
  - 添加 `[RequirePermission("sys:log:view")]` 和 `[RequirePermission("sys:log:export")]` 权限
  - 在 `DataSeedService` 中初始化对应的菜单和权限数据
- **验收标准**: 无权限用户访问日志 API 返回 403

---

### P1 — 重要问题

#### 5. 日志查询缺少租户隔离

- **文件**: `OperationLogService.cs:74-85`、`AuditLogService.cs:120-128`
- **问题**: 查询时未按 `FCompanyId` 过滤
- **方案**: 在所有日志查询方法中注入 `ICurrentUserService`，强制添加 `.Where(x => x.FCompanyId == currentUser.CompanyId)` 过滤
- **验收标准**: A 公司用户只能看到 A 公司的日志

#### 6. 审计日志无防篡改机制

- **文件**: `SysAuditLog.cs`、`AuditLogService.cs`
- **问题**: 普通数据库表，可直接修改删除
- **方案**:
  - `SysAuditLog` 新增 `Fhash` 字段，存储当前记录内容 + 前一条记录哈希的 SHA256 值
  - 写入时计算哈希链
  - 提供完整性校验 API，遍历哈希链检测是否被篡改
  - 审计日志表禁止 UPDATE（仅 INSERT）
- **验收标准**: 任何篡改都能通过哈希链校验检测出来

#### 7. SysLoginInfo 字段严重不足

- **文件**: `SysLoginInfo.cs`、`LoginInfoService.cs`
- **问题**: 接口接收了 `success`、`userAgent`、`message` 参数但实体无对应字段，数据被丢弃
- **方案**: 扩展 `SysLoginInfo` 实体，添加以下字段：
  - `Fsuccess` (bool) — 登录是否成功
  - `Fip` (string) — IP 地址（替代语义不清的 `Fcomputer`）
  - `Fuseragent` (string) — 浏览器/设备信息
  - `Fmessage` (string) — 失败原因
  - `Flogintype` (string) — 登录方式（密码/Token/SSO）
  - 同步修改 `LoginInfoService.RecordLoginAsync` 填充这些字段
- **验收标准**: 登录日志完整记录所有传入参数

#### 8. 日志表无数据库索引

- **文件**: `SysOperationLog.cs`、`SysAuditLog.cs`、`SysAuditLogEntry.cs`
- **问题**: 无任何 `[SugarIndex]`，大数据量下查询退化为全表扫描
- **方案**: 添加以下索引：
  - `SysOperationLog`: `(FDeleted, Fdate DESC)`、`(FDeleted, Fprgkey)`、`(CYmd)` 用于归档
  - `SysAuditLog`: `(Fuid, FDeleted)`、`(FDeleted, CYmd DESC)`、`(CYmd)` 用于归档
  - `SysAuditLogEntry`: `(FInterId)` 用于关联查询
- **验收标准**: 日志查询响应时间在 10 万条数据量下 < 500ms

#### 9. 敏感数据掩码不完整

- **文件**: `RequestLoggingMiddleware.cs`、`appsettings.json`
- **问题**: 仅覆盖 request body 的 5 个字段名，response body、SQL 参数、异常堆栈、审计日志 before/after 数据均未处理
- **方案**:
  - 扩展敏感字段列表，增加 `idCard`、`bankAccount`、`phone`、`mobile`、`email` 等 ERP 常见字段
  - SqlSugar SQL 日志中对参数值进行脱敏（检测参数名包含敏感关键词时掩码）
  - 审计日志写入前对敏感列的 before/after 数据进行掩码
  - `GlobalExceptionMiddleware` 中对异常消息进行脱敏（移除连接字符串等）
- **验收标准**: 日志中不出现明文密码、身份证号、银行账号等敏感信息

#### 10. 缺少安全事件日志

- **问题**: 权限拒绝、异常登录、暴力破解等安全事件无专门记录
- **方案**:
  - 新增 `SysSecurityEvent` 实体（事件类型、严重级别、来源IP、用户、描述、时间）
  - 新增 `ISecurityLogService` 接口和实现
  - 在 `PermissionHandler`（权限拒绝）、`AuthService`（连续失败登录）、JWT 验证失败事件中集成
  - 提供查询 API 和统计
- **验收标准**: 安全相关事件有独立的记录和查询入口

#### 11. Correlation ID 可被客户端伪造

- **文件**: `CorrelationIdMiddleware.cs:18-19`
- **问题**: 直接信任 `X-Correlation-Id` 头，无格式验证
- **方案**: 对客户端提供的值进行 GUID 格式验证和长度限制（最大 36 字符），不合法时忽略并生成新值
- **验收标准**: 非 GUID 格式的 Correlation ID 被拒绝

---

### P2 — 建议优化

#### 12. 日志归档改为真正的归档

- **文件**: `LogArchivalService.cs`
- **问题**: 直接物理删除，无备份
- **方案**:
  - 删除前先导出到归档文件（JSON/CSV 压缩包）
  - 归档文件存储到指定目录，按年月组织
  - 归档/删除操作本身记录审计日志
  - 审计日志和登录日志支持独立的保留期配置
- **验收标准**: 过期日志先归档再删除，归档文件可追溯

#### 13. 归档服务加事务保护和分布式锁

- **文件**: `LogArchivalService.cs`
- **问题**: 多实例部署可能重复执行，删除顺序有数据不一致风险
- **方案**:
  - 审计日志明细和主表删除包裹在事务中
  - 使用数据库锁或分布式锁防止多实例并发执行
  - 使用 `DateTime.UtcNow` 替代 `DateTime.Now`
- **验收标准**: 多实例部署下归档不重复执行，数据一致

#### 14. 日志导出改为流式写入

- **文件**: `LogExportService.cs`
- **问题**: 一次性加载全部数据到内存，峰值内存约 CSV 大小的 3-4 倍
- **方案**:
  - 使用 `StreamWriter` 直接写入 `MemoryStream`，避免 StringBuilder
  - 分批查询数据库（每次 1000 条），边查边写
  - 对导出接口添加并发限制（Semaphore）
  - 导出行数上限移至配置文件
- **验收标准**: 导出 10000 条数据内存占用降低 50% 以上

#### 15. SQL 日志回调优化

- **文件**: `DependencyInjection.cs:52-68`
- **问题**: `OnLogExecuting` 即使不输出也会执行参数序列化
- **方案**:
  - `OnLogExecuting` 中先检查 `logger.IsEnabled(LogLevel.Debug)` 再执行参数序列化
  - 生产环境默认关闭 SQL 日志或仅保留慢查询检测
- **验收标准**: 生产环境 SQL 日志回调无不必要的对象分配

#### 16. 统计接口优化

- **文件**: `OperationLogService.cs:117-186`
- **问题**: 5 次独立数据库查询，无缓存
- **方案**:
  - 合并多个 COUNT 查询为单条 SQL
  - 对统计结果添加短时间缓存（如 5 分钟）
  - 在 `OperationLogStatisticsRequest` 中添加 `CompanyId` 过滤
- **验收标准**: 统计接口数据库查询次数减少，响应时间改善

#### 17. 各类日志统一 CorrelationId

- **文件**: `SysAuditLog.cs`、`SysLoginInfo.cs`
- **问题**: 审计日志和登录日志无 `Fcorrelationid` 字段
- **方案**:
  - `SysAuditLog` 和 `SysLoginInfo` 添加 `Fcorrelationid` 字段
  - `AuditLogService` 注入 `ICorrelationIdProvider` 并写入
  - 提供按 CorrelationId 跨日志类型联合查询的 API
- **验收标准**: 通过一个 CorrelationId 可以关联查到同一请求的所有日志

#### 18. 导出操作记录审计日志

- **文件**: `OperationLogController.cs`、`AuditLogController.cs`
- **问题**: 导出操作无追踪，无速率限制
- **方案**:
  - 导出操作记录到操作日志（谁、什么时候、导出了什么范围的数据）
  - 添加速率限制（如每用户每分钟最多 5 次导出）
- **验收标准**: 每次导出都有审计记录

#### 19. IP 地址获取安全化

- **文件**: `OperationLogService.cs:194-198`、`AuditLogService.cs:176-180`
- **问题**: 直接信任 `X-Forwarded-For`
- **方案**:
  - 提取 `GetClientIp()` 为公共的 `IClientIpResolver` 服务（同时解决 DRY 问题）
  - 配置 ASP.NET Core 的 `ForwardedHeaders` 中间件，设置受信任代理列表
- **验收标准**: IP 地址获取经过受信任代理验证

---

### P3 — 长期规划

#### 20. Serilog 文件配置优化

- **方案**:
  - 添加 `rollOnFileSizeLimit: true` 确保不丢日志
  - 引入 `Serilog.Sinks.Async` 包装文件 sink
- **验收标准**: 单日日志超过 50MB 时自动滚动而非丢弃

#### 21. 请求日志和 SQL 日志提供 API 查询

- **方案**:
  - 将慢请求和慢 SQL 同时写入数据库表（如 `SYS_SLOWQUERYLOG`）
  - 提供查询 API，支持按耗时、路径、时间范围过滤
- **验收标准**: 运维人员可通过管理界面查看慢请求和慢 SQL

#### 22. 结构化日志输出

- **方案**: 添加 `Serilog.Formatting.Compact`（CompactJsonFormatter），生产环境输出 JSON 格式日志
- **验收标准**: 日志文件为 JSON 格式，可被 ELK 等平台直接采集

#### 23. OpenTelemetry 集成

- **方案**: 引入 `OpenTelemetry.Extensions.Hosting` + `OpenTelemetry.Instrumentation.AspNetCore`，与现有 CorrelationId 共存
- **验收标准**: 支持 Traces + Metrics + Logs 三位一体的可观测性

#### 24. 日志分级存储

- **方案**: 热数据（近期）放数据库快速查询，温数据归档到压缩文件，冷数据归档到对象存储
- **验收标准**: 不同时期的日志有不同的存储策略和查询方式

#### 25. 时间统一为 UTC

- **文件**: `OperationLogService.cs`、`AuditLogService.cs` 等
- **方案**: 所有日志时间戳使用 `DateTime.UtcNow`，展示层转换为本地时间
- **验收标准**: 跨时区部署时日志时间一致

---

## 三、实施路线

| 阶段 | 范围 | 优化项 |
|------|------|--------|
| 第一阶段 | P0 必须修复 | #1 Channel队列改造、#2 审计日志自动化、#3 事务保护、#4 权限控制 |
| 第二阶段 | P1 重要问题 | #5 租户隔离、#7 LoginInfo字段补全、#8 数据库索引、#9 敏感掩码、#11 CorrelationId验证 |
| 第三阶段 | P1 + P2 | #6 防篡改哈希链、#10 安全事件日志、#12-19 各项优化 |
| 第四阶段 | P3 长期规划 | #20-25 基础设施升级 |

---

## 四、合规性差距对照

| 合规要求 | 当前状态 | 目标状态 | 对应优化项 |
|---------|---------|---------|-----------|
| 等保三级 - 审计日志完整性 | 无防篡改机制 | 哈希链校验 | #6 |
| 等保三级 - 审计日志保护 | 普通表，可修改删除 | INSERT-only + 归档 | #6, #12 |
| 等保三级 - 日志保留 | 90天硬删除 | ≥180天，归档而非删除 | #12 |
| 等保三级 - 访问控制 | 无角色限制 | 基于权限的日志访问 | #4 |
| GDPR - 数据最小化 | 审计日志含 PII 原始值 | PII 脱敏 | #9 |
| SOX - 审计追踪不可篡改 | 无保护 | 不可变审计日志 | #6 |
