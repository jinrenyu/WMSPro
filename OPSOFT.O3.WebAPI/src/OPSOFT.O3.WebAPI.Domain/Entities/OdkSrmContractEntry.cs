using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 合同单表体
/// </summary>
[SugarTable("ODK_SRM_ContractEntry")]
public class OdkSrmContractEntry : BaseEntity
{
    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 是否含税
    /// </summary>
    [SugarColumn(ColumnName = "FISTAX")]
    public bool Fistax { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAX")]
    public decimal Ftax { get; set; }

    /// <summary>
    /// 含税价格
    /// </summary>
    [SugarColumn(ColumnName = "FTAXPRICE")]
    public decimal Ftaxprice { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 税前金额
    /// </summary>
    [SugarColumn(ColumnName = "FPRETAXAMT")]
    public decimal Fpretaxamt { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMT")]
    public decimal Ftaxamt { get; set; }

    /// <summary>
    /// 总额
    /// </summary>
    [SugarColumn(ColumnName = "FAMT")]
    public decimal Famt { get; set; }

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 交付日期
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDATE")]
    public DateTime? Fdeliverydate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
