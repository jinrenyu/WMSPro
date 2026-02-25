using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产用料清单明细
/// </summary>
[SugarTable("T_PRD_PPBOMENTRY")]
public class TPrdPpbomentry : BaseEntity
{
    /// <summary>
    /// 项次
    /// </summary>
    [SugarColumn(ColumnName = "FREPLACEGROUP")]
    public int Freplacegroup { get; set; }

    /// <summary>
    /// 子项物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 子项类型
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALTYPE")]
    public string Fmaterialtype { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEQTY")]
    public decimal Fbaseqty { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FCOMMONUNITID")]
    public string Fcommonunitid { get; set; } = string.Empty;

    /// <summary>
    /// 使用比例
    /// </summary>
    [SugarColumn(ColumnName = "FUSERATE")]
    public decimal Fuserate { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 分子
    /// </summary>
    [SugarColumn(ColumnName = "FNUMERATOR")]
    public decimal Fnumerator { get; set; }

    /// <summary>
    /// 工序号
    /// </summary>
    [SugarColumn(ColumnName = "FOPERID")]
    public string Foperid { get; set; } = string.Empty;

    /// <summary>
    /// 分母
    /// </summary>
    [SugarColumn(ColumnName = "FDENOMINATOR")]
    public decimal Fdenominator { get; set; }

    /// <summary>
    /// 变动损耗率%
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPRATE")]
    public decimal Fscraprate { get; set; }

    /// <summary>
    /// 固定损损耗
    /// </summary>
    [SugarColumn(ColumnName = "FFIXSCRAPQTY")]
    public decimal Ffixscrapqty { get; set; }

    /// <summary>
    /// 应发数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 已领数量
    /// </summary>
    [SugarColumn(ColumnName = "FPICKEDQTY")]
    public decimal Fpickedqty { get; set; }

    /// <summary>
    /// 已领基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FPICKEDBASEQTY")]
    public decimal Fpickedbaseqty { get; set; }

    /// <summary>
    /// 未领数量
    /// </summary>
    [SugarColumn(ColumnName = "FNOPICKEDQTY")]
    public decimal Fnopickedqty { get; set; }

    /// <summary>
    /// 未领基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FNOPICKEDBASEQTY")]
    public decimal Fnopickedbaseqty { get; set; }

    /// <summary>
    /// 跳层
    /// </summary>
    [SugarColumn(ColumnName = "FISSKIP")]
    public bool Fisskip { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 倒冲
    /// </summary>
    [SugarColumn(ColumnName = "FBACKFLUSH")]
    public int Fbackflush { get; set; }

    /// <summary>
    /// 计划发料日期
    /// </summary>
    [SugarColumn(ColumnName = "FSENDITEMDATE")]
    public DateTime? Fsenditemdate { get; set; }

    /// <summary>
    /// 是否启用序列号
    /// </summary>
    [SugarColumn(ColumnName = "FFEEDSN")]
    public bool Ffeedsn { get; set; }

    /// <summary>
    /// 是否第三方序列号
    /// </summary>
    [SugarColumn(ColumnName = "FOTHERSN")]
    public bool Fothersn { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 工序所属序列
    /// </summary>
    [SugarColumn(ColumnName = "FBELONGSEQ")]
    public string Fbelongseq { get; set; } = string.Empty;

    /// <summary>
    /// 是否WMS新增
    /// </summary>
    [SugarColumn(ColumnName = "FISNEWADD")]
    public bool Fisnewadd { get; set; }

    /// <summary>
    /// 生产已消耗数量
    /// </summary>
    [SugarColumn(ColumnName = "FCONSUMEDQTY")]
    public decimal Fconsumedqty { get; set; }

    /// <summary>
    /// 生产已消耗基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FCONSUMEDBASEUNITQTY")]
    public decimal Fconsumedbaseunitqty { get; set; }

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

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
