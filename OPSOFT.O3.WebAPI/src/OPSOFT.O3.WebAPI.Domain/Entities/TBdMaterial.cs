using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料
/// </summary>
[SugarTable("T_BD_MATERIAL")]
public class TBdMaterial : BaseEntity, IApprovable, IDisableable
{
    /// <summary>
    /// 主料号内码
    /// </summary>
    [SugarColumn(ColumnName = "FMASTERID")]
    public string FMasterId { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 物料名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 规格型号
    /// </summary>
    [SugarColumn(ColumnName = "FSPECIFICATION")]
    public string FSpecification { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string FDescription { get; set; } = string.Empty;

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string FBarcode { get; set; } = string.Empty;

    /// <summary>
    /// 物料属性
    /// </summary>
    [SugarColumn(ColumnName = "FERPCLSID")]
    public string FErpClsId { get; set; } = string.Empty;

    /// <summary>
    /// ABC分类
    /// </summary>
    [SugarColumn(ColumnName = "FABC")]
    public string FAbc { get; set; } = string.Empty;

    /// <summary>
    /// 最大库存
    /// </summary>
    [SugarColumn(ColumnName = "FMAXQTY")]
    public decimal FMaxQty { get; set; }

    /// <summary>
    /// 安全库存
    /// </summary>
    [SugarColumn(ColumnName = "FSAFEQTY")]
    public decimal FSafeQty { get; set; }

    /// <summary>
    /// 净重
    /// </summary>
    [SugarColumn(ColumnName = "FNETWEIGHT")]
    public decimal FNetWeight { get; set; }

    /// <summary>
    /// 毛重
    /// </summary>
    [SugarColumn(ColumnName = "FGROSSWEIGHT")]
    public decimal FGrossWeight { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string FBaseUnitId { get; set; } = string.Empty;

    /// <summary>
    /// 库存单位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOREUNITID")]
    public string FStoreUnitId { get; set; } = string.Empty;

    /// <summary>
    /// 销售单位
    /// </summary>
    [SugarColumn(ColumnName = "FSALEUNITID")]
    public string FSaleUnitId { get; set; } = string.Empty;

    /// <summary>
    /// 采购单位
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASEUNITID")]
    public string FPurchaseUnitId { get; set; } = string.Empty;

    /// <summary>
    /// 启用保质期
    /// </summary>
    [SugarColumn(ColumnName = "FISKFPERIOD")]
    public bool FIsKfPeriod { get; set; }

    /// <summary>
    /// 保质期单位
    /// </summary>
    [SugarColumn(ColumnName = "FKFUNIT")]
    public int FKfUnit { get; set; }

    /// <summary>
    /// 保质期限
    /// </summary>
    [SugarColumn(ColumnName = "FKFPERIOD")]
    public int FKfPeriod { get; set; }

    /// <summary>
    /// 启用批号管理
    /// </summary>
    [SugarColumn(ColumnName = "FISBATCHMANAGE")]
    public bool FIsBatchManage { get; set; }

    /// <summary>
    /// 最小订货量
    /// </summary>
    [SugarColumn(ColumnName = "FMINPOQTY")]
    public decimal FMinPoQty { get; set; }

    /// <summary>
    /// 最小包装量
    /// </summary>
    [SugarColumn(ColumnName = "FINCREASEQTY")]
    public decimal FIncreaseQty { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime FCheckDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime Fdisabledate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 来料检验
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKINCOMING")]
    public bool FCheckIncoming { get; set; }

    /// <summary>
    /// 旧料号
    /// </summary>
    [SugarColumn(ColumnName = "FOLDNUMBER")]
    public string FOldNumber { get; set; } = string.Empty;

    /// <summary>
    /// 默认仓库
    /// </summary>
    [SugarColumn(ColumnName = "FDESTOCKID")]
    public string FDeStockId { get; set; } = string.Empty;

    /// <summary>
    /// 默认仓位
    /// </summary>
    [SugarColumn(ColumnName = "FDESPID")]
    public string FDeSpId { get; set; } = string.Empty;

    /// <summary>
    /// 虚拟件
    /// </summary>
    [SugarColumn(ColumnName = "FVPART")]
    public bool FVPart { get; set; }

    /// <summary>
    /// 物料类别ID
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string FTypeId { get; set; } = string.Empty;
}
