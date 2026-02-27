using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 过程检验
/// </summary>
[SugarTable("T_SFC_PROCESSINPECTION")]
public class TSfcProcessinpection : BaseEntity
{
    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTYPE")]
    public int Fworktype { get; set; }

    /// <summary>
    /// 检验方案
    /// </summary>
    [SugarColumn(ColumnName = "FTESTWAYID")]
    public string Ftestwayid { get; set; } = string.Empty;

    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 检验数量
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNT")]
    public decimal Fcount { get; set; }

    /// <summary>
    /// 检验部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPARTMENTID")]
    public string Fdepartmentid { get; set; } = string.Empty;

    /// <summary>
    /// 质检员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FBEGTIME")]
    public DateTime? Fbegtime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public DateTime? Fendtime { get; set; }

    /// <summary>
    /// 耗用时间
    /// </summary>
    [SugarColumn(ColumnName = "FSPENDTIME")]
    public int Fspendtime { get; set; }

    /// <summary>
    /// 检验方式
    /// </summary>
    [SugarColumn(ColumnName = "FTESTTYPE")]
    public int Ftesttype { get; set; }

    /// <summary>
    /// 合格数
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTQTY")]
    public decimal Facceptqty { get; set; }

    /// <summary>
    /// 不合格数
    /// </summary>
    [SugarColumn(ColumnName = "FUNACCEPTQTY")]
    public decimal Funacceptqty { get; set; }

    /// <summary>
    /// 合格数(旧
    /// </summary>
    [SugarColumn(ColumnName = "FOLDACCEPTQTY")]
    public decimal Foldacceptqty { get; set; }

    /// <summary>
    /// 不合格数(旧)
    /// </summary>
    [SugarColumn(ColumnName = "FOLDUNACCEPTQTY", IsNullable = true)]
    public decimal? FOLDUNACCEPTQTY { get; set; }

    /// <summary>
    /// 汇报表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID", IsNullable = true)]
    public string FREPORTDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 允收数
    /// </summary>
    [SugarColumn(ColumnName = "FALLOWEDQTY", IsNullable = true)]
    public decimal? FALLOWEDQTY { get; set; }

    /// <summary>
    /// 拒收数
    /// </summary>
    [SugarColumn(ColumnName = "FREJECTION", IsNullable = true)]
    public decimal? FREJECTION { get; set; }

    /// <summary>
    /// 抽样类型
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLINGTYPE", IsNullable = true)]
    public int? FSAMPLINGTYPE { get; set; }

    /// <summary>
    /// 样本破坏合格数
    /// </summary>
    [SugarColumn(ColumnName = "FDESQUAQTY", IsNullable = true)]
    public decimal? FDESQUAQTY { get; set; }

    /// <summary>
    /// 汇报ID
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTID", IsNullable = true)]
    public string FREPORTID { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION", IsNullable = true)]
    public string FDESCRIPTION { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY", IsNullable = true)]
    public decimal? FQTY { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID", IsNullable = true)]
    public string FCHECKERID { get; set; } = string.Empty;

    /// <summary>
    /// 样本未破坏合格数
    /// </summary>
    [SugarColumn(ColumnName = "FUNDESQUAQTY", IsNullable = true)]
    public decimal? FUNDESQUAQTY { get; set; }

    /// <summary>
    /// 工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID", IsNullable = true)]
    public string FFLOWCARDDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID", IsNullable = true)]
    public string FDISABLEID { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE", IsNullable = true)]
    public DateTime? FCHECKDATE { get; set; }

    /// <summary>
    /// 样本破坏不合格数
    /// </summary>
    [SugarColumn(ColumnName = "FDESUNQUAQTY", IsNullable = true)]
    public decimal? FDESUNQUAQTY { get; set; }

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE", IsNullable = true)]
    public string FCODE { get; set; } = string.Empty;

    /// <summary>
    /// 样本总数
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEQTY", IsNullable = true)]
    public decimal? FSAMPLEQTY { get; set; }

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID", IsNullable = true)]
    public string FFLOWCARDID { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID", IsNullable = true)]
    public string FPROCESSID { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE", IsNullable = true)]
    public DateTime? FDISABLEDATE { get; set; }

    /// <summary>
    /// 检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FTESTRESULT", IsNullable = true)]
    public string FTESTRESULT { get; set; } = string.Empty;

    /// <summary>
    /// 操作指导书
    /// </summary>
    [SugarColumn(ColumnName = "FOMS", IsNullable = true)]
    public string FOMS { get; set; } = string.Empty;

    /// <summary>
    /// 样本未破坏不合格数
    /// </summary>
    [SugarColumn(ColumnName = "FUNDESUNQUAQTY", IsNullable = true)]
    public decimal? FUNDESUNQUAQTY { get; set; }

    /// <summary>
    /// AQL
    /// </summary>
    [SugarColumn(ColumnName = "FAQL", IsNullable = true)]
    public int? FAQL { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 抽样方案
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEPLANID", IsNullable = true)]
    public string FSAMPLEPLANID { get; set; } = string.Empty;
}
