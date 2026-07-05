namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 生产订单 / 生产任务单（真实表 T_PRD_MO / T_PRD_MOENTRY）=====

/// <summary>
/// 生产订单列表项（按明细行展开，一条明细一行，带解析后的名称列，参照设计列表）
/// </summary>
public class ProductionOrderListDto
{
    /// <summary>主表 Uid（用于进入编辑）</summary>
    public string Uid { get; set; } = string.Empty;
    /// <summary>明细 Uid</summary>
    public string EntryUid { get; set; } = string.Empty;
    /// <summary>生产工单编号</summary>
    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    /// <summary>明细行号</summary>
    public int? Fentryid { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    // 生产车间
    public string Fworkshopid { get; set; } = string.Empty;
    public string FworkshopNumber { get; set; } = string.Empty;
    public string FworkshopName { get; set; } = string.Empty;
    // 产品（物料）
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FchartNumber { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    /// <summary>产品类型 1=主产品/2=联产品/3=副产品</summary>
    public string Fproducttype { get; set; } = string.Empty;
    // 工艺流程
    public string Fprorouteid { get; set; } = string.Empty;
    public string FprorouteName { get; set; } = string.Empty;
    // 批次
    public string Flot { get; set; } = string.Empty;
    // 单位 / 数量
    public string FbaseunitName { get; set; } = string.Empty;
    /// <summary>计划数量</summary>
    public decimal Fqty { get; set; }
    /// <summary>派工数量</summary>
    public decimal Ffcqty { get; set; }
    /// <summary>未派工数量 = 计划数量 - 派工数量</summary>
    public decimal Funfcqty { get; set; }
    /// <summary>基本单位计划数量</summary>
    public decimal Fbaseunitqty { get; set; }
    public string Fmachinemodel { get; set; } = string.Empty;
    // 业务/排产状态
    public string Fbstatus { get; set; } = string.Empty;
    public string FbstatusName { get; set; } = string.Empty;
    public string Fschedulestatus { get; set; } = string.Empty;
    public bool Fissuspend { get; set; }
    // 辅助属性
    public bool FisAuxEnabled { get; set; }
    public string FauxpropName { get; set; } = string.Empty;
    // 超产比例
    public decimal Finhighlimit { get; set; }
    // 时间
    public DateTime? Fplanstartdate { get; set; }
    public DateTime? Fplanfinishdate { get; set; }
    public DateTime? Factualstartdate { get; set; }
    public DateTime? Factualfinishdate { get; set; }
    // 计划员（表头）
    public string FplannerName { get; set; } = string.Empty;
    // 审计
    public string CuserName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string MuserName { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
    public string FcheckerName { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public bool FDisabled { get; set; }
    public string FcompanyName { get; set; } = string.Empty;
}

/// <summary>
/// 生产订单明细行
/// </summary>
public class ProductionOrderEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public int? Fentryid { get; set; }
    /// <summary>产品类型 1=主产品/2=联产品/3=副产品</summary>
    public string Fproducttype { get; set; } = "1";
    // 生产车间（部门）
    public string Fworkshopid { get; set; } = string.Empty;
    public string FworkshopNumber { get; set; } = string.Empty;
    public string FworkshopName { get; set; } = string.Empty;
    // 产品（物料）
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FchartNumber { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    /// <summary>启用批次管理（物料带出，只读）</summary>
    public bool FisBatchManage { get; set; }
    /// <summary>生产机型</summary>
    public string Fmachinemodel { get; set; } = string.Empty;
    // 批次
    public string Flot { get; set; } = string.Empty;
    // 基本单位
    public string Fbaseunitid { get; set; } = string.Empty;
    public string FbaseunitNumber { get; set; } = string.Empty;
    public string FbaseunitName { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    // 常用单位
    public string Fcommonunitid { get; set; } = string.Empty;
    public string FcommonunitNumber { get; set; } = string.Empty;
    public string FcommonunitName { get; set; } = string.Empty;
    /// <summary>数量（常用单位）</summary>
    public decimal Fqty { get; set; }
    // 辅助属性
    public bool FisAuxEnabled { get; set; }
    public string Fauxpropid { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;
    // 产品工艺流程
    public string Fprorouteid { get; set; } = string.Empty;
    public string FprorouteNumber { get; set; } = string.Empty;
    public string FprorouteName { get; set; } = string.Empty;
    // 条码规则
    public string Fbarcoderuleid { get; set; } = string.Empty;
    public string FbarcoderuleName { get; set; } = string.Empty;
    // 数量类（多为流程回写，只读）
    public decimal Ffcqty { get; set; }
    public decimal Ftotalfinishqty { get; set; }
    // 业务状态（只读）
    public string Fbstatus { get; set; } = "1";
    public string FbstatusName { get; set; } = string.Empty;
    public bool Fissuspend { get; set; }
    // 超产比例 / 换型时长
    public decimal Finhighlimit { get; set; }
    public decimal Fretime { get; set; }
    // BOM 单
    public string Fbomid { get; set; } = string.Empty;
    public string FbomBillno { get; set; } = string.Empty;
    // 时间
    public DateTime? Fconveydate { get; set; }
    public DateTime? Fplanstartdate { get; set; }
    public DateTime? Fplanfinishdate { get; set; }
    public DateTime? Factualstartdate { get; set; }
    public DateTime? Factualfinishdate { get; set; }
    // 结案（只读）
    public string Ffinalid { get; set; } = string.Empty;
    public string FfinalName { get; set; } = string.Empty;
    public DateTime? Ffinaldate { get; set; }
    public string Fnote { get; set; } = string.Empty;
    // 已入库（只读）
    public decimal Fstockqty { get; set; }
    public decimal Fstockbaseqty { get; set; }
}

/// <summary>
/// 生产订单详情（主表 + 名称解析 + 明细）
/// </summary>
public class ProductionOrderDetailDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    // 单据类型
    public string Fbilltype { get; set; } = string.Empty;
    public string FbilltypeName { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    public string Foastatus { get; set; } = string.Empty;
    public string Foaresult { get; set; } = string.Empty;
    // 组织
    public string Fcompanyid { get; set; } = string.Empty;
    public string FcompanyName { get; set; } = string.Empty;
    // 计划员
    public string Fplannerid { get; set; } = string.Empty;
    public string FplannerName { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    // ---- 其他页签：制单/审核/修改/禁用 ----
    public string CUser { get; set; } = string.Empty;
    public string CuserName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string Fcheckerid { get; set; } = string.Empty;
    public string FcheckerName { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string MUser { get; set; } = string.Empty;
    public string MuserName { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public string FdisableName { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
    public bool FDisabled { get; set; }
    public List<ProductionOrderEntryDto> Entries { get; set; } = new();
}

public class CreateProductionOrderEntryRequest
{
    public string Fproducttype { get; set; } = "1";
    public string Fworkshopid { get; set; } = string.Empty;
    public string Fmaterialid { get; set; } = string.Empty;
    public string Fmachinemodel { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    public string Fcommonunitid { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public string Fauxpropid { get; set; } = string.Empty;
    public string Fprorouteid { get; set; } = string.Empty;
    public string Fbarcoderuleid { get; set; } = string.Empty;
    public decimal Finhighlimit { get; set; }
    public decimal Fretime { get; set; }
    public string Fbomid { get; set; } = string.Empty;
    public DateTime? Fplanstartdate { get; set; }
    public DateTime? Fplanfinishdate { get; set; }
    public string Fnote { get; set; } = string.Empty;
}

public class CreateProductionOrderRequest
{
    public string Fbillno { get; set; } = string.Empty;
    public string Fbilltype { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    /// <summary>组织</summary>
    public string Fcompanyid { get; set; } = string.Empty;
    public string Fplannerid { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateProductionOrderEntryRequest> Entries { get; set; } = new();
}

public class UpdateProductionOrderRequest
{
    public string Fbilltype { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    /// <summary>组织</summary>
    public string Fcompanyid { get; set; } = string.Empty;
    public string Fplannerid { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateProductionOrderEntryRequest> Entries { get; set; } = new();
}

/// <summary>
/// 下达 / 反下达请求：对生产订单（主表 Uid 在路由）中选中的若干明细行（FDETAILID=明细 Uid）执行。
/// 前端单选时传一个元素，保留 List 以便后续支持多选批量下达。
/// </summary>
public class ProductionReleaseRequest
{
    /// <summary>要下达 / 反下达的明细行 Uid（= T_PRD_MOENTRY.FDETAILID）集合</summary>
    public List<string> EntryUids { get; set; } = new();
}

/// <summary>
/// 下达 / 反下达结果：逐行处理信息 + 成功/失败计数 + 生成的用料清单单号。
/// 单行软失败（无 BOM、已下达等）不抛异常，作为一条 Message 返回，前端据 SuccessCount 决定是否刷新。
/// </summary>
public class ProductionReleaseResultDto
{
    /// <summary>逐行处理信息（成功 / 跳过原因）</summary>
    public List<string> Messages { get; set; } = new();
    /// <summary>成功处理的明细行数</summary>
    public int SuccessCount { get; set; }
    /// <summary>跳过 / 失败的明细行数</summary>
    public int FailCount { get; set; }
    /// <summary>本次下达生成的用料清单单据编号（反下达为被删除的单号）</summary>
    public List<string> BillNos { get; set; } = new();
}

/// <summary>
/// 产品代码 / 工艺流程下拉项（数据源：T_ENG_PROROUTE 关联产品物料）。
/// Uid=工艺流程内码（明细"产品工艺流程"键），FNumber=物料编码、FName=物料名称（产品代码显示用）；
/// 选中后前端据扩展字段带出物料/工艺到明细各只读列。继承通用 LookupDto 以复用 LookupSelect。
/// </summary>
public class ProRouteLookupDto : LookupDto
{
    /// <summary>产品物料 Uid（存明细 FMATERIALID）</summary>
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FchartNumber { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    public bool FisBatchManage { get; set; }
    /// <summary>工艺流程 Uid（存明细 FPROROUTEID）</summary>
    public string Fprorouteid { get; set; } = string.Empty;
    public string FprorouteNumber { get; set; } = string.Empty;
    public string FprorouteName { get; set; } = string.Empty;
}
