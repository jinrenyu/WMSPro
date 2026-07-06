namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ERP 集成配置（字段自定义）：一主两从
//   主表 T_SYN_INFO        —— 同步任务（功能代码/频率/启用/过滤/ERP表单ID…）
//   从1  T_SYN_INFOENTRY   —— 实体信息（一行=一个 源→目标 实体，如物料主表 / 物料辅助属性子表）
//   从2  T_SYN_INFOENTRY1  —— 字段映射（一行=一个字段；经 Fbodyid 归属到某个实体行）
// 前端/接口采用「实体内嵌字段映射」的树形结构，落库时展平到两张从表并回填 Fbodyid=实体行 Fdetailid。

// ===== 列表 =====
public class ErpSyncConfigListDto
{
    public string Uid { get; set; } = string.Empty;
    /// <summary>功能代码</summary>
    public string Fnumber { get; set; } = string.Empty;
    /// <summary>功能名称</summary>
    public string Fname { get; set; } = string.Empty;
    /// <summary>ERP表单ID</summary>
    public string Ferpbillid { get; set; } = string.Empty;
    /// <summary>开始时间点</summary>
    public int Fstart { get; set; }
    /// <summary>同步频率</summary>
    public int Fsynrate { get; set; }
    /// <summary>时间单位 1=秒/2=分/3=小时/4=天</summary>
    public string Ftimetype { get; set; } = string.Empty;
    public string FtimetypeName { get; set; } = string.Empty;
    /// <summary>同步过滤规则</summary>
    public string Fruleid { get; set; } = string.Empty;
    /// <summary>是否启用同步</summary>
    public bool Fisenable { get; set; }
    /// <summary>系统预设</summary>
    public bool Isdefault { get; set; }
    /// <summary>备注</summary>
    public string Fnote { get; set; } = string.Empty;
    /// <summary>最后同步时间</summary>
    public DateTime? Ftimestamp { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
}

// ===== 详情 =====
public class ErpSyncConfigDetailDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fnumber { get; set; } = string.Empty;
    public string Fname { get; set; } = string.Empty;
    public string Ferpbillid { get; set; } = string.Empty;
    public int Fstart { get; set; }
    public int Fsynrate { get; set; }
    public string Ftimetype { get; set; } = string.Empty;
    public string Fruleid { get; set; } = string.Empty;
    public bool Fisenable { get; set; }
    public bool Isdefault { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public DateTime? Ftimestamp { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string CUser { get; set; } = string.Empty;
    public string CuserName { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
    public string MUser { get; set; } = string.Empty;
    /// <summary>实体信息（内嵌各自的字段映射）</summary>
    public List<ErpSyncEntityDto> Entities { get; set; } = new();
}

/// <summary>实体信息行（从1）+ 其字段映射（从2）</summary>
public class ErpSyncEntityDto
{
    public string Uid { get; set; } = string.Empty;
    public int Fentryid { get; set; }
    /// <summary>表体内码（字段映射行以此为 Fbodyid 归属）</summary>
    public string Fdetailid { get; set; } = string.Empty;
    /// <summary>是否主表</summary>
    public bool Fismain { get; set; }
    /// <summary>源数据类型 0=自定义/1=表/2=视图</summary>
    public int Fsrcdatatype { get; set; }
    /// <summary>源系统实体</summary>
    public string Fsrcdataname { get; set; } = string.Empty;
    /// <summary>源单据描述</summary>
    public string Fsrcdatades { get; set; } = string.Empty;
    /// <summary>源唯一标识</summary>
    public string Fsrcdatakey { get; set; } = string.Empty;
    /// <summary>源时间戳字段</summary>
    public string Ftimestampname { get; set; } = string.Empty;
    /// <summary>过滤条件</summary>
    public string Filter { get; set; } = string.Empty;
    /// <summary>目标数据类型 1=表</summary>
    public int Faimdatatype { get; set; }
    /// <summary>O3目标实体（表名）</summary>
    public string Faimdataname { get; set; } = string.Empty;
    /// <summary>O3目标描述</summary>
    public string Faimdatades { get; set; } = string.Empty;
    /// <summary>目标唯一标识（多列逗号分隔，如 FCOMPANYID,FNUMBER）</summary>
    public string Faimdatakey { get; set; } = string.Empty;
    /// <summary>关联字段</summary>
    public string Frelationfield { get; set; } = string.Empty;
    /// <summary>重复是否覆盖</summary>
    public bool Fiscover { get; set; }
    /// <summary>ERP查询标识脚本（ExecuteBillQuery FieldKeys）</summary>
    public string Fieldkeys { get; set; } = string.Empty;
    /// <summary>ERP表单代码（ExecuteBillQuery FormId）</summary>
    public string Ferpbillid { get; set; } = string.Empty;
    /// <summary>ERP排序标识（ExecuteBillQuery OrderString）</summary>
    public string Forderstring { get; set; } = string.Empty;
    /// <summary>同步过滤条件（ExecuteBillQuery FilterString）</summary>
    public string Fruleid { get; set; } = string.Empty;
    /// <summary>字段映射</summary>
    public List<ErpSyncFieldMapDto> FieldMaps { get; set; } = new();
}

/// <summary>字段映射行（从2）</summary>
public class ErpSyncFieldMapDto
{
    public string Uid { get; set; } = string.Empty;
    public int Fentryid { get; set; }
    public string Fdetailid { get; set; } = string.Empty;
    /// <summary>归属实体行的 Fdetailid</summary>
    public string Fbodyid { get; set; } = string.Empty;
    /// <summary>源数据字段（首字段）</summary>
    public string Fsrcfield { get; set; } = string.Empty;
    public string Fsecondsrcfield { get; set; } = string.Empty;
    public string Fthirdsrcfield { get; set; } = string.Empty;
    /// <summary>源字段描述</summary>
    public string Fsrcfielddes { get; set; } = string.Empty;
    /// <summary>是否排序源字段</summary>
    public bool Fissort { get; set; }
    /// <summary>源主键</summary>
    public bool Fissrcfieldkey { get; set; }
    /// <summary>目标字段</summary>
    public string Faimfield { get; set; } = string.Empty;
    /// <summary>目标字段类型</summary>
    public string Faimfieldtype { get; set; } = string.Empty;
    /// <summary>目标字段描述</summary>
    public string Faimfielddes { get; set; } = string.Empty;
    /// <summary>目标主键</summary>
    public bool Faimfieldkey { get; set; }
    /// <summary>是否必录</summary>
    public bool Fmustinput { get; set; }
    /// <summary>默认值</summary>
    public string Fdefaultvalue { get; set; } = string.Empty;
    /// <summary>是否固定值</summary>
    public bool Fissetfixed { get; set; }
    /// <summary>固定值</summary>
    public string Ffixedvalue { get; set; } = string.Empty;
    /// <summary>关联表</summary>
    public string Flookupclassid { get; set; } = string.Empty;
    /// <summary>关联字段</summary>
    public string Flookupfield { get; set; } = string.Empty;
    /// <summary>关联取值字段</summary>
    public string Flookupsavefield { get; set; } = string.Empty;
    /// <summary>是否唯一内码</summary>
    public bool Fisinnerid { get; set; }
    /// <summary>是否加入日志</summary>
    public bool Fislogshow { get; set; }
    /// <summary>字段类型 0=标准/1=辅助属性/2=仓位/4=仓库/3=自定义</summary>
    public int Ffieldtype { get; set; }
    /// <summary>ERP标识对应顺序</summary>
    public string Ferpseq { get; set; } = string.Empty;
}

// ===== 新增 / 更新 请求 =====
public class CreateErpSyncConfigRequest
{
    public string Fnumber { get; set; } = string.Empty;
    public string Fname { get; set; } = string.Empty;
    public string Ferpbillid { get; set; } = string.Empty;
    public int Fstart { get; set; }
    public int Fsynrate { get; set; }
    public string Ftimetype { get; set; } = string.Empty;
    public string Fruleid { get; set; } = string.Empty;
    public bool Fisenable { get; set; }
    public bool Isdefault { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public List<ErpSyncEntityRequest> Entities { get; set; } = new();
}

public class UpdateErpSyncConfigRequest : CreateErpSyncConfigRequest { }

public class ErpSyncEntityRequest
{
    public bool Fismain { get; set; }
    public int Fsrcdatatype { get; set; }
    public string Fsrcdataname { get; set; } = string.Empty;
    public string Fsrcdatades { get; set; } = string.Empty;
    public string Fsrcdatakey { get; set; } = string.Empty;
    public string Ftimestampname { get; set; } = string.Empty;
    public string Filter { get; set; } = string.Empty;
    public int Faimdatatype { get; set; }
    public string Faimdataname { get; set; } = string.Empty;
    public string Faimdatades { get; set; } = string.Empty;
    public string Faimdatakey { get; set; } = string.Empty;
    public string Frelationfield { get; set; } = string.Empty;
    public bool Fiscover { get; set; }
    public string Fieldkeys { get; set; } = string.Empty;
    public string Ferpbillid { get; set; } = string.Empty;
    public string Forderstring { get; set; } = string.Empty;
    public string Fruleid { get; set; } = string.Empty;
    public List<ErpSyncFieldMapRequest> FieldMaps { get; set; } = new();
}

public class ErpSyncFieldMapRequest
{
    public string Fsrcfield { get; set; } = string.Empty;
    public string Fsecondsrcfield { get; set; } = string.Empty;
    public string Fthirdsrcfield { get; set; } = string.Empty;
    public string Fsrcfielddes { get; set; } = string.Empty;
    public bool Fissort { get; set; }
    public bool Fissrcfieldkey { get; set; }
    public string Faimfield { get; set; } = string.Empty;
    public string Faimfieldtype { get; set; } = string.Empty;
    public string Faimfielddes { get; set; } = string.Empty;
    public bool Faimfieldkey { get; set; }
    public bool Fmustinput { get; set; }
    public string Fdefaultvalue { get; set; } = string.Empty;
    public bool Fissetfixed { get; set; }
    public string Ffixedvalue { get; set; } = string.Empty;
    public string Flookupclassid { get; set; } = string.Empty;
    public string Flookupfield { get; set; } = string.Empty;
    public string Flookupsavefield { get; set; } = string.Empty;
    public bool Fisinnerid { get; set; }
    public bool Fislogshow { get; set; }
    public int Ffieldtype { get; set; }
    public string Ferpseq { get; set; } = string.Empty;
}

// ===== 目标目录（反射驱动 UI 下拉） =====
public class ErpTargetTableDto
{
    /// <summary>表名（如 T_BD_MATERIAL）</summary>
    public string TableName { get; set; } = string.Empty;
    /// <summary>实体类名</summary>
    public string EntityName { get; set; } = string.Empty;
    /// <summary>描述（取自实体 XML summary）</summary>
    public string Description { get; set; } = string.Empty;
}

public class ErpTargetFieldDto
{
    /// <summary>列名（如 FNUMBER）</summary>
    public string FieldName { get; set; } = string.Empty;
    /// <summary>数据类型（VARCHAR/NVARCHAR/DECIMAL/BIT/INT/DATETIME…）</summary>
    public string FieldType { get; set; } = string.Empty;
    /// <summary>字段描述（取自属性 XML summary）</summary>
    public string Description { get; set; } = string.Empty;
    public bool IsPrimaryKey { get; set; }
}

/// <summary>功能代码/ERP表单ID 候选（来自 SYS_BILLTEMPLATE）</summary>
public class ErpBillTemplateDto
{
    public string Fnumber { get; set; } = string.Empty;
    public string Fname { get; set; } = string.Empty;
}

// ===== 同步运行结果 =====
public class ErpSyncRunResultDto
{
    public bool Success { get; set; }
    public int SuccessCount { get; set; }
    public int FailCount { get; set; }
    public string Message { get; set; } = string.Empty;
    public string LogUid { get; set; } = string.Empty;
    public long ElapsedMs { get; set; }
}
