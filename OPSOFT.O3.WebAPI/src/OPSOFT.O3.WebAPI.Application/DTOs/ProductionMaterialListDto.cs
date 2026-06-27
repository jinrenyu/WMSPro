namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 生产用料清单 / 生产投料单（真实表 T_PRD_PPBOM / T_PRD_PPBOMENTRY）=====
// 一主一从：主表=一张用料清单（对应一条生产订单产品行），从表=该产品的用料明细行。
// 列表一行=一张主单（非按明细展开），字段全部取自主表 + 名称解析。

/// <summary>
/// 生产用料清单列表项（一行一张主单，带解析后的名称列，参照设计列表）
/// </summary>
public class ProductionMaterialListListDto
{
    public string Uid { get; set; } = string.Empty;
    /// <summary>单据编号</summary>
    public string Fbillno { get; set; } = string.Empty;
    /// <summary>生产工单（生产订单）编号</summary>
    public string Fmobillno { get; set; } = string.Empty;
    /// <summary>生产工单行号</summary>
    public int Fmoentryseq { get; set; }
    public DateTime? Fdate { get; set; }
    // 产品（物料）
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    /// <summary>生产数量</summary>
    public decimal Fqty { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    public bool FDisabled { get; set; }
    // 审计
    public string CuserName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string MuserName { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
    public string FcheckerName { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string FdisableName { get; set; } = string.Empty;
    public string FcompanyName { get; set; } = string.Empty;
}

/// <summary>
/// 生产用料清单明细行（T_PRD_PPBOMENTRY）
/// </summary>
public class ProductionMaterialListEntryDto
{
    public string Uid { get; set; } = string.Empty;
    /// <summary>行号</summary>
    public int Fentryid { get; set; }
    /// <summary>项次（替代分组）</summary>
    public int Freplacegroup { get; set; }
    /// <summary>子项类型 1=标准件/2=返还件/3=替代件</summary>
    public string Fmaterialtype { get; set; } = "1";
    // 物料
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    public bool FisBatchManage { get; set; }
    public bool FisAuxEnabled { get; set; }
    // 工序
    public string Foperid { get; set; } = string.Empty;
    public string FoperNumber { get; set; } = string.Empty;
    public string FoperName { get; set; } = string.Empty;
    // 基本单位
    public string Fbaseunitid { get; set; } = string.Empty;
    public string FbaseunitNumber { get; set; } = string.Empty;
    public string FbaseunitName { get; set; } = string.Empty;
    public decimal Fbaseqty { get; set; }
    // 常用单位
    public string Fcommonunitid { get; set; } = string.Empty;
    public string FcommonunitNumber { get; set; } = string.Empty;
    public string FcommonunitName { get; set; } = string.Empty;
    // 批次
    public string Flot { get; set; } = string.Empty;
    // 辅助属性
    public string Fauxpropid { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;
    // BOM 用量
    public decimal Fuserate { get; set; }
    public decimal Fnumerator { get; set; }
    public decimal Fdenominator { get; set; }
    public decimal Fscraprate { get; set; }
    public decimal Ffixscrapqty { get; set; }
    // 数量
    public decimal Fmustqty { get; set; }
    public decimal Fpickedqty { get; set; }
    public decimal Fnopickedqty { get; set; }
    // 计划发料日期
    public DateTime? Fsenditemdate { get; set; }
    // 仓库 / 仓位
    public string Fstockid { get; set; } = string.Empty;
    public string FstockNumber { get; set; } = string.Empty;
    public string FstockName { get; set; } = string.Empty;
    public bool FisOpenLocation { get; set; }
    public string Fstocklocid { get; set; } = string.Empty;
    public string FstocklocName { get; set; } = string.Empty;
    // 标记
    public int Fbackflush { get; set; }
    public bool Ffeedsn { get; set; }
    public bool Fothersn { get; set; }
    public string Fnote { get; set; } = string.Empty;
}

/// <summary>
/// 生产用料清单详情（主表 + 名称解析 + 明细）
/// </summary>
public class ProductionMaterialListDetailDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    // 生产工单（生产订单）
    public string Fmobillno { get; set; } = string.Empty;
    public string Fmoid { get; set; } = string.Empty;
    public string Fmoentryid { get; set; } = string.Empty;
    public int Fmoentryseq { get; set; }
    // 产品（物料）
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    public bool FisAuxEnabled { get; set; }
    // 辅助属性
    public string Fauxpropid { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;
    // 生产类型（辅助资料 ProduceType）
    public string Fmotype { get; set; } = string.Empty;
    public string FmotypeNumber { get; set; } = string.Empty;
    public string FmotypeName { get; set; } = string.Empty;
    // 生产数量 / 单位
    public decimal Fqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string FunitNumber { get; set; } = string.Empty;
    public string FunitName { get; set; } = string.Empty;
    // 组织
    public string Fcompanyid { get; set; } = string.Empty;
    public string FcompanyName { get; set; } = string.Empty;
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
    public List<ProductionMaterialListEntryDto> Entries { get; set; } = new();
}

public class CreateProductionMaterialListEntryRequest
{
    public int Freplacegroup { get; set; }
    public string Fmaterialtype { get; set; } = "1";
    public string Fmaterialid { get; set; } = string.Empty;
    public string Foperid { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public string Fcommonunitid { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public decimal Fuserate { get; set; }
    public decimal Fnumerator { get; set; }
    public decimal Fdenominator { get; set; }
    public decimal Fscraprate { get; set; }
    public decimal Ffixscrapqty { get; set; }
    public decimal Fmustqty { get; set; }
    public DateTime? Fsenditemdate { get; set; }
    public string Fstockid { get; set; } = string.Empty;
    public string Fstocklocid { get; set; } = string.Empty;
    public int Fbackflush { get; set; }
    public bool Ffeedsn { get; set; }
    public bool Fothersn { get; set; }
    public string Fnote { get; set; } = string.Empty;
}

public class CreateProductionMaterialListRequest
{
    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fmobillno { get; set; } = string.Empty;
    public string Fmoid { get; set; } = string.Empty;
    public string Fmoentryid { get; set; } = string.Empty;
    public int Fmoentryseq { get; set; }
    public string Fmaterialid { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public string Fmotype { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    /// <summary>组织</summary>
    public string Fcompanyid { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateProductionMaterialListEntryRequest> Entries { get; set; } = new();
}

public class UpdateProductionMaterialListRequest
{
    public DateTime? Fdate { get; set; }
    public string Fmobillno { get; set; } = string.Empty;
    public string Fmoid { get; set; } = string.Empty;
    public string Fmoentryid { get; set; } = string.Empty;
    public int Fmoentryseq { get; set; }
    public string Fmaterialid { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public string Fmotype { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string Fcompanyid { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateProductionMaterialListEntryRequest> Entries { get; set; } = new();
}

/// <summary>
/// 生产工单（生产订单）下拉项：数据源 T_PRD_MOENTRY 关联表头 T_PRD_MO + 产品物料，一行一条生产订单产品行。
/// Uid=生产订单明细内码（存表头 FMOENTRYID），FNumber=生产订单编号、FName=产品名称；
/// 选中后前端据扩展字段带出生产工单行号/产品/数量/单位到主表。继承 LookupDto 复用 LookupSelect。
/// </summary>
public class MoLookupDto : LookupDto
{
    /// <summary>生产订单内码（存表头 FMOID）</summary>
    public string Fmoid { get; set; } = string.Empty;
    /// <summary>生产订单明细内码（存表头 FMOENTRYID）</summary>
    public string Fmoentryid { get; set; } = string.Empty;
    /// <summary>生产工单行号（存表头 FMOENTRYSEQ）</summary>
    public int Fmoentryseq { get; set; }
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public string FunitName { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
}

/// <summary>
/// 物料扩展下拉项：在通用物料下拉基础上带出规格型号 / 基本单位（供生产用料清单产品代码、明细物料代码选用）。
/// Uid=物料 Uid、FNumber=物料编码、FName=物料名称。继承 LookupDto 复用 LookupSelect。
/// </summary>
public class MaterialExtLookupDto : LookupDto
{
    public string FSpecification { get; set; } = string.Empty;
    public string FchartNumber { get; set; } = string.Empty;
    public bool FisBatchManage { get; set; }
    public string FBaseUnitId { get; set; } = string.Empty;
    public string FBaseUnitNumber { get; set; } = string.Empty;
    public string FBaseUnitName { get; set; } = string.Empty;
}
