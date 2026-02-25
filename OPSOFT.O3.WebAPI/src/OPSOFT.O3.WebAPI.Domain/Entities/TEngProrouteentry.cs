using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 产品工艺流程表体-工序
/// </summary>
[SugarTable("T_ENG_PROROUTEENTRY")]
public class TEngProrouteentry : BaseEntity
{
    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流水号
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public string Fseq { get; set; } = string.Empty;

    /// <summary>
    /// 工序绘制的位置X坐标
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTX")]
    public int Fpointx { get; set; }

    /// <summary>
    /// 工序绘制的位置Y坐标
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTY")]
    public int Fpointy { get; set; }

    /// <summary>
    /// 工序颜色
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSCOLOR")]
    public string Fprocesscolor { get; set; } = string.Empty;

    /// <summary>
    /// 序列类型
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSTYPE")]
    public int Fprocesstype { get; set; }

    /// <summary>
    /// 检验方案
    /// </summary>
    [SugarColumn(ColumnName = "FQCTYPE")]
    public string Fqctype { get; set; } = string.Empty;

    /// <summary>
    /// 条码打印方式
    /// </summary>
    [SugarColumn(ColumnName = "FPRSEQBAR")]
    public int Fprseqbar { get; set; }

    /// <summary>
    /// 标准换型时间(分
    /// </summary>
    [SugarColumn(ColumnName = "FRETIME")]
    public decimal Fretime { get; set; }
}
