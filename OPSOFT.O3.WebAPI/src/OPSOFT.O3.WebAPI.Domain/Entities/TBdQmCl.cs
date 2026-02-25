using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验单信息
/// </summary>
[SugarTable("T_BD_QM_CL")]
public class TBdQmCl : BaseEntity
{
    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string FMaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 送检数量
    /// </summary>
    [SugarColumn(ColumnName = "FMQTY")]
    public decimal FMqty { get; set; }

    /// <summary>
    /// 合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FGOODQTY")]
    public decimal FGoodqty { get; set; }

    /// <summary>
    /// 不合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal FBadqty { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string FUnitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string FBaseunitId { get; set; } = string.Empty;

    /// <summary>
    /// 检验批次
    /// </summary>
    [SugarColumn(ColumnName = "FQCLOT")]
    public string FQclot { get; set; } = string.Empty;

    /// <summary>
    /// 检验状态
    /// </summary>
    [SugarColumn(ColumnName = "FQCSTATE")]
    public string FQcstate { get; set; } = string.Empty;

    /// <summary>
    /// 检验人
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTID")]
    public string FInspectid { get; set; } = string.Empty;

    /// <summary>
    /// 检验日期
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTDATE")]
    public DateTime? FInspectdate { get; set; }

    /// <summary>
    /// 检验部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string FDeptid { get; set; } = string.Empty;

    /// <summary>
    /// 人工检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FQCRESULT")]
    public string FQcresult { get; set; } = string.Empty;

    /// <summary>
    /// MRB评审
    /// </summary>
    [SugarColumn(ColumnName = "FISMRB")]
    public bool FIsmrb { get; set; }

    /// <summary>
    /// MRB结果
    /// </summary>
    [SugarColumn(ColumnName = "FMRBRS")]
    public string FMrbrs { get; set; } = string.Empty;

    /// <summary>
    /// 是否加急
    /// </summary>
    [SugarColumn(ColumnName = "FURGENT")]
    public bool FUrgent { get; set; }

    /// <summary>
    /// 发起类型
    /// </summary>
    [SugarColumn(ColumnName = "FLAUTYPE")]
    public string FLautype { get; set; } = string.Empty;

    /// <summary>
    /// 是否免检
    /// </summary>
    [SugarColumn(ColumnName = "FISEXEMPT")]
    public bool FIsexempt { get; set; }

    /// <summary>
    /// 已审核
    /// </summary>
    [SugarColumn(ColumnName = "FISAUDIT")]
    public bool FIsaudit { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? FCheckdate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string FSrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string FSrcid { get; set; } = string.Empty;

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string FSrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID")]
    public string FSrcdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID")]
    public int FSrcentryid { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string FStockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string FStocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 版本
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string FAuxpropId { get; set; } = string.Empty;

    /// <summary>
    /// ERP内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPID")]
    public string FErpid { get; set; } = string.Empty;

    /// <summary>
    /// ERP编号
    /// </summary>
    [SugarColumn(ColumnName = "FERPNO")]
    public string FErpno { get; set; } = string.Empty;

    /// <summary>
    /// 是否同步
    /// </summary>
    [SugarColumn(ColumnName = "ISSYN")]
    public bool Issyn { get; set; }

    /// <summary>
    /// 系统检测结果
    /// </summary>
    [SugarColumn(ColumnName = "FSYSQCRESULT")]
    public string FSysqcresult { get; set; } = string.Empty;

    /// <summary>
    /// 样本破坏数
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPQTY")]
    public decimal FScrapqty { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string FLot { get; set; } = string.Empty;

    /// <summary>
    /// 库存请检单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSTKINSPECTBILLNO")]
    public string FStkinspectbillno { get; set; } = string.Empty;

    /// <summary>
    /// 库存请检单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTKINSPECTID")]
    public string FStkinspectid { get; set; } = string.Empty;

    /// <summary>
    /// 库存请检单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTKINSPECTDETAILID")]
    public string FStkinspectdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 库存请检单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSTKINSPECTENTRYID")]
    public int FStkinspectentryid { get; set; }

    /// <summary>
    /// 检验文件号
    /// </summary>
    [SugarColumn(ColumnName = "FFILENO")]
    public string FFileno { get; set; } = string.Empty;

    /// <summary>
    /// 检验文件版本
    /// </summary>
    [SugarColumn(ColumnName = "FFILEAUXPROPID")]
    public string FFileauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 图纸文件号
    /// </summary>
    [SugarColumn(ColumnName = "FDRAWNO")]
    public string FDrawno { get; set; } = string.Empty;

    /// <summary>
    /// 图纸文件版本
    /// </summary>
    [SugarColumn(ColumnName = "FDRAWAUXPROPID")]
    public string FDrawauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 提交人
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITID")]
    public string FSubmitid { get; set; } = string.Empty;

    /// <summary>
    /// 确认人
    /// </summary>
    [SugarColumn(ColumnName = "FSUREID")]
    public string FSureid { get; set; } = string.Empty;

    /// <summary>
    /// 最后暂存时间
    /// </summary>
    [SugarColumn(ColumnName = "FLASTSAVEDATE")]
    public DateTime? FLastsavedate { get; set; }

    /// <summary>
    /// MES资源
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string FResourceid { get; set; } = string.Empty;

    /// <summary>
    /// 确认时间
    /// </summary>
    [SugarColumn(ColumnName = "FSUREDATE")]
    public DateTime? FSuredate { get; set; }

    /// <summary>
    /// 提交时间
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITDATE")]
    public DateTime? FSubmitdate { get; set; }

    /// <summary>
    /// 严格度
    /// </summary>
    [SugarColumn(ColumnName = "FSTRINGENCY")]
    public int FStringency { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string FBillno { get; set; } = string.Empty;
}
