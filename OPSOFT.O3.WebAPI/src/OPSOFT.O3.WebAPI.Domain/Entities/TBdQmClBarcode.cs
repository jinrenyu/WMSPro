using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验单标签明细
/// </summary>
[SugarTable("T_BD_QM_CL_BARCODE")]
public class TBdQmClBarcode : BaseEntity
{
    /// <summary>
    /// 条码ID
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string FBarcode { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string FMaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLIERID")]
    public string FSupplierid { get; set; } = string.Empty;

    /// <summary>
    /// 制造商内码
    /// </summary>
    [SugarColumn(ColumnName = "FMANUFACID")]
    public string FManufacid { get; set; } = string.Empty;

    /// <summary>
    /// 客户内码
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTOMERID")]
    public string FCustomerid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string FUnitid { get; set; } = string.Empty;

    /// <summary>
    /// (条码)数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal FQty { get; set; }

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal FBadqty { get; set; }

    /// <summary>
    /// 测试值
    /// </summary>
    [SugarColumn(ColumnName = "FQCVALUE")]
    public string FQcvalue { get; set; } = string.Empty;

    /// <summary>
    /// 测试结果
    /// </summary>
    [SugarColumn(ColumnName = "FQCRESULT")]
    public string FQcresult { get; set; } = string.Empty;

    /// <summary>
    /// MRB结果
    /// </summary>
    [SugarColumn(ColumnName = "FMRBRS")]
    public string FMrbrs { get; set; } = string.Empty;

    /// <summary>
    /// 报废数量
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPQTY")]
    public decimal FScrapqty { get; set; }

    /// <summary>
    /// 是否不良
    /// </summary>
    [SugarColumn(ColumnName = "FISBAD")]
    public bool FIsbad { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

    /// <summary>
    /// 来源条码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBARCODE")]
    public string FSrcbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 收料日期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? FKfdate { get; set; }

    /// <summary>
    /// 是否破坏检
    /// </summary>
    [SugarColumn(ColumnName = "FISDESTROY")]
    public bool FIsdestroy { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int FEntryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string FDetailid { get; set; } = string.Empty;
}
