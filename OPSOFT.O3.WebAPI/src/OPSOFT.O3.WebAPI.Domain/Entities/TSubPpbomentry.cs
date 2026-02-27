using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外用料清单明细
/// </summary>
[SugarTable("T_SUB_PPBOMENTRY")]
public class TSubPpbomentry : BaseEntity
{
    /// <summary>
    /// 项次
    /// </summary>
    [SugarColumn(ColumnName = "FREPLACEGROUP")]
    public string Freplacegroup { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 子项类型
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALTYPE")]
    public string Fmaterialtype { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 用量类型
    /// </summary>
    [SugarColumn(ColumnName = "FDOSAGETYPE")]
    public string Fdosagetype { get; set; } = string.Empty;

    /// <summary>
    /// 使用比例(%
    /// </summary>
    [SugarColumn(ColumnName = "FUSERATE")]
    public decimal Fuserate { get; set; }

    /// <summary>
    /// 超发比例
    /// </summary>
    [SugarColumn(ColumnName = "FOVERRATE", IsNullable = true)]
    public decimal? FOVERRATE { get; set; }

    /// <summary>
    /// 原用料清单分录内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCPPBOMENTRYID", IsNullable = true)]
    public string FSRCPPBOMENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 原分母
    /// </summary>
    [SugarColumn(ColumnName = "FBOMDENOMINATOR", IsNullable = true)]
    public decimal? FBOMDENOMINATOR { get; set; }

    /// <summary>
    /// 供应组织
    /// </summary>
    [SugarColumn(ColumnName = "FCHILDSUPPLYORGID", IsNullable = true)]
    public string FCHILDSUPPLYORGID { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单分录内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQENTRYID1", IsNullable = true)]
    public string FSUBREQENTRYID1 { get; set; } = string.Empty;

    /// <summary>
    /// 最后更新人
    /// </summary>
    [SugarColumn(ColumnName = "FUPDATERID", IsNullable = true)]
    public string FUPDATERID { get; set; } = string.Empty;

    /// <summary>
    /// 未领数量
    /// </summary>
    [SugarColumn(ColumnName = "FNOPICKEDQTY", IsNullable = true)]
    public decimal? FNOPICKEDQTY { get; set; }

    /// <summary>
    /// 是否关键件
    /// </summary>
    [SugarColumn(ColumnName = "FISKEYCOMPONENT", IsNullable = true)]
    public bool? FISKEYCOMPONENT { get; set; }

    /// <summary>
    /// 基本单位良品退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEGOODRETURNQTY", IsNullable = true)]
    public decimal? FBASEGOODRETURNQTY { get; set; }

    /// <summary>
    /// 退料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FSELPRCDRETURNQTY", IsNullable = true)]
    public decimal? FSELPRCDRETURNQTY { get; set; }

    /// <summary>
    /// 分母
    /// </summary>
    [SugarColumn(ColumnName = "FDENOMINATOR", IsNullable = true)]
    public decimal? FDENOMINATOR { get; set; }

    /// <summary>
    /// 在制材料数量
    /// </summary>
    [SugarColumn(ColumnName = "FWIPQTY", IsNullable = true)]
    public decimal? FWIPQTY { get; set; }

    /// <summary>
    /// 需求优先级
    /// </summary>
    [SugarColumn(ColumnName = "FPRIORITY", IsNullable = true)]
    public string FPRIORITY { get; set; } = string.Empty;

    /// <summary>
    /// 拨出仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSRCTRANSSTOCKID", IsNullable = true)]
    public string FSRCTRANSSTOCKID { get; set; } = string.Empty;

    /// <summary>
    /// 项目编号
    /// </summary>
    [SugarColumn(ColumnName = "FPROJECTNO", IsNullable = true)]
    public string FPROJECTNO { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位调拨数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASETRANSLATEQTY", IsNullable = true)]
    public decimal? FBASETRANSLATEQTY { get; set; }

    /// <summary>
    /// 委外订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQID1", IsNullable = true)]
    public string FSUBREQID1 { get; set; } = string.Empty;

    /// <summary>
    /// 分子
    /// </summary>
    [SugarColumn(ColumnName = "FNUMERATOR", IsNullable = true)]
    public decimal? FNUMERATOR { get; set; }

    /// <summary>
    /// 变动损耗率%
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPRATE", IsNullable = true)]
    public decimal? FSCRAPRATE { get; set; }

    /// <summary>
    /// 基本单位原分母
    /// </summary>
    [SugarColumn(ColumnName = "FBASEBOMDENOMINATOR", IsNullable = true)]
    public decimal? FBASEBOMDENOMINATOR { get; set; }

    /// <summary>
    /// 倒冲时机
    /// </summary>
    [SugarColumn(ColumnName = "FBACKFLUSHTYPE", IsNullable = true)]
    public string FBACKFLUSHTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASENEEDQTY", IsNullable = true)]
    public decimal? FBASENEEDQTY { get; set; }

    /// <summary>
    /// 原始携带量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEMUSTQTY", IsNullable = true)]
    public decimal? FBASEMUSTQTY { get; set; }

    /// <summary>
    /// 基本单位补料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESELREPICKEDQTY", IsNullable = true)]
    public decimal? FBASESELREPICKEDQTY { get; set; }

    /// <summary>
    /// BOM展开路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATHENTRYID", IsNullable = true)]
    public string FPATHENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 货源
    /// </summary>
    [SugarColumn(ColumnName = "FSMID", IsNullable = true)]
    public string FSMID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位已备料数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESTOCKREADYQTY", IsNullable = true)]
    public decimal? FBASESTOCKREADYQTY { get; set; }

    /// <summary>
    /// 基本单位已消耗数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASECONSUMEQTY", IsNullable = true)]
    public decimal? FBASECONSUMEQTY { get; set; }

    /// <summary>
    /// 父级行主键
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTROWID", IsNullable = true)]
    public string FPARENTROWID { get; set; } = string.Empty;

    /// <summary>
    /// 补领数量
    /// </summary>
    [SugarColumn(ColumnName = "FREPICKEDQTY", IsNullable = true)]
    public decimal? FREPICKEDQTY { get; set; }

    /// <summary>
    /// 行标识
    /// </summary>
    [SugarColumn(ColumnName = "FROWID", IsNullable = true)]
    public string FROWID { get; set; } = string.Empty;

    /// <summary>
    /// 拨出仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSRCTRANSSTOCKLOCID", IsNullable = true)]
    public string FSRCTRANSSTOCKLOCID { get; set; } = string.Empty;

    /// <summary>
    /// 补料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FSELREPICKEDQTY", IsNullable = true)]
    public decimal? FSELREPICKEDQTY { get; set; }

    /// <summary>
    /// 已备料数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKREADYQTY", IsNullable = true)]
    public decimal? FSTOCKREADYQTY { get; set; }

    /// <summary>
    /// 基本单位在制材料数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEWIPQTY", IsNullable = true)]
    public decimal? FBASEWIPQTY { get; set; }

    /// <summary>
    /// 固定损耗
    /// </summary>
    [SugarColumn(ColumnName = "FFIXSCRAPQTY", IsNullable = true)]
    public decimal? FFIXSCRAPQTY { get; set; }

    /// <summary>
    /// 已消耗数量
    /// </summary>
    [SugarColumn(ColumnName = "FCONSUMEQTY", IsNullable = true)]
    public decimal? FCONSUMEQTY { get; set; }

    /// <summary>
    /// 业务流程
    /// </summary>
    [SugarColumn(ColumnName = "FBFLOWID", IsNullable = true)]
    public string FBFLOWID { get; set; } = string.Empty;

    /// <summary>
    /// 领料考虑最小发料批量
    /// </summary>
    [SugarColumn(ColumnName = "FISMINISSUEQTY", IsNullable = true)]
    public decimal? FISMINISSUEQTY { get; set; }

    /// <summary>
    /// 作业不良退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSDEFECTRETURNQTY", IsNullable = true)]
    public decimal? FPROCESSDEFECTRETURNQTY { get; set; }

    /// <summary>
    /// 需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FNEEDQTY2", IsNullable = true)]
    public decimal? FNEEDQTY2 { get; set; }

    /// <summary>
    /// 偏置提前期
    /// </summary>
    [SugarColumn(ColumnName = "FOFFSETTIME", IsNullable = true)]
    public string FOFFSETTIME { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID", IsNullable = true)]
    public string FSTOCKSTATUSID { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID", IsNullable = true)]
    public string FOWNERID { get; set; } = string.Empty;

    /// <summary>
    /// 标准用量
    /// </summary>
    [SugarColumn(ColumnName = "FSTDQTY", IsNullable = true)]
    public decimal? FSTDQTY { get; set; }

    /// <summary>
    /// 原用料清单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCPPBOMID", IsNullable = true)]
    public string FSRCPPBOMID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位退料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESELPRCDRETURNQTY", IsNullable = true)]
    public decimal? FBASESELPRCDRETURNQTY { get; set; }

    /// <summary>
    /// 分组货主
    /// </summary>
    [SugarColumn(ColumnName = "FGROUPBYOWNERID", IsNullable = true)]
    public string FGROUPBYOWNERID { get; set; } = string.Empty;

    /// <summary>
    /// 拨出组织
    /// </summary>
    [SugarColumn(ColumnName = "FSRCTRANSORGID", IsNullable = true)]
    public string FSRCTRANSORGID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位良品退料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESELGOODRETURNQTY", IsNullable = true)]
    public decimal? FBASESELGOODRETURNQTY { get; set; }

    /// <summary>
    /// 替代方式
    /// </summary>
    [SugarColumn(ColumnName = "FREPLACETYPE", IsNullable = true)]
    public string FREPLACETYPE { get; set; } = string.Empty;

    /// <summary>
    /// 报废数量
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPQTY", IsNullable = true)]
    public decimal? FSCRAPQTY { get; set; }

    /// <summary>
    /// 基本单位分母
    /// </summary>
    [SugarColumn(ColumnName = "FBASEDENOMINATOR", IsNullable = true)]
    public decimal? FBASEDENOMINATOR { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID", IsNullable = true)]
    public string FSTOCKID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位报废数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESCRAPQTY", IsNullable = true)]
    public decimal? FBASESCRAPQTY { get; set; }

    /// <summary>
    /// 位置号
    /// </summary>
    [SugarColumn(ColumnName = "FPOSITIONNO", IsNullable = true)]
    public string FPOSITIONNO { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FMEMO1", IsNullable = true)]
    public string FMEMO1 { get; set; } = string.Empty;

    /// <summary>
    /// MRP运算
    /// </summary>
    [SugarColumn(ColumnName = "FISMRPRUN", IsNullable = true)]
    public string FISMRPRUN { get; set; } = string.Empty;

    /// <summary>
    /// 调拨选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FSELTRANSLATEQTY", IsNullable = true)]
    public decimal? FSELTRANSLATEQTY { get; set; }

    /// <summary>
    /// 调拨数量
    /// </summary>
    [SugarColumn(ColumnName = "FTRANSLATEQTY", IsNullable = true)]
    public decimal? FTRANSLATEQTY { get; set; }

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID", IsNullable = true)]
    public string FOWNERTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位作业不良退料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESELPRCDEFECTRETURNQTY", IsNullable = true)]
    public decimal? FBASESELPRCDEFECTRETURNQTY { get; set; }

    /// <summary>
    /// 基本单位来料不良退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEINCDEFECTRETURNQTY", IsNullable = true)]
    public decimal? FBASEINCDEFECTRETURNQTY { get; set; }

    /// <summary>
    /// 委外订单分录行号
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQENTRYSEQ1", IsNullable = true)]
    public int? FSUBREQENTRYSEQ1 { get; set; }

    /// <summary>
    /// 基本单位来料不良退料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESELINCDEFECTRETURNQTY", IsNullable = true)]
    public decimal? FBASESELINCDEFECTRETURNQTY { get; set; }

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID", IsNullable = true)]
    public string FSTOCKLOCID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位固定损耗数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEFIXSCRAPQTY", IsNullable = true)]
    public decimal? FBASEFIXSCRAPQTY { get; set; }

    /// <summary>
    /// 子项基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID1", IsNullable = true)]
    public string FBASEUNITID1 { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 基本单位已领数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEPICKEDQTY", IsNullable = true)]
    public decimal? FBASEPICKEDQTY { get; set; }

    /// <summary>
    /// 基本单位分子
    /// </summary>
    [SugarColumn(ColumnName = "FBASENUMERATOR", IsNullable = true)]
    public decimal? FBASENUMERATOR { get; set; }

    /// <summary>
    /// 基本单位即时库存数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEINVENTORYQTY", IsNullable = true)]
    public decimal? FBASEINVENTORYQTY { get; set; }

    /// <summary>
    /// 发料组织
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYORG", IsNullable = true)]
    public string FSUPPLYORG { get; set; } = string.Empty;

    /// <summary>
    /// 用料清单类型
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMENTRYTYPE", IsNullable = true)]
    public string FPPBOMENTRYTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 子项工作日历
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCALID2", IsNullable = true)]
    public string FWORKCALID2 { get; set; } = string.Empty;

    /// <summary>
    /// 来料不良退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FINCDEFECTRETURNQTY", IsNullable = true)]
    public decimal? FINCDEFECTRETURNQTY { get; set; }

    /// <summary>
    /// 应发数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY", IsNullable = true)]
    public decimal? FMUSTQTY { get; set; }

    /// <summary>
    /// 需求日期
    /// </summary>
    [SugarColumn(ColumnName = "FNEEDDATE2", IsNullable = true)]
    public DateTime? FNEEDDATE2 { get; set; }

    /// <summary>
    /// 超发控制方式
    /// </summary>
    [SugarColumn(ColumnName = "FOVERCONTROLMODE", IsNullable = true)]
    public string FOVERCONTROLMODE { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQBILLNO1", IsNullable = true)]
    public string FSUBREQBILLNO1 { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位未领数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASENOPICKEDQTY", IsNullable = true)]
    public decimal? FBASENOPICKEDQTY { get; set; }

    /// <summary>
    /// 工序
    /// </summary>
    [SugarColumn(ColumnName = "FOPERID", IsNullable = true)]
    public string FOPERID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位补领数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEREPICKEDQTY", IsNullable = true)]
    public decimal? FBASEREPICKEDQTY { get; set; }

    /// <summary>
    /// 原分子
    /// </summary>
    [SugarColumn(ColumnName = "FBOMNUMERATOR", IsNullable = true)]
    public decimal? FBOMNUMERATOR { get; set; }

    /// <summary>
    /// 子项单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID", IsNullable = true)]
    public string FUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 行展开类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWEXPANDTYPE", IsNullable = true)]
    public string FROWEXPANDTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 跳层
    /// </summary>
    [SugarColumn(ColumnName = "FISSKIP", IsNullable = true)]
    public bool? FISSKIP { get; set; }

    /// <summary>
    /// 替代优先级
    /// </summary>
    [SugarColumn(ColumnName = "FREPLACEPRIORITY", IsNullable = true)]
    public string FREPLACEPRIORITY { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位调拨选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESELTRANSLATEQTY", IsNullable = true)]
    public decimal? FBASESELTRANSLATEQTY { get; set; }

    /// <summary>
    /// 即时库存
    /// </summary>
    [SugarColumn(ColumnName = "FINVENTORYQTY", IsNullable = true)]
    public decimal? FINVENTORYQTY { get; set; }

    /// <summary>
    /// 领料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FSELPICKEDQTY", IsNullable = true)]
    public decimal? FSELPICKEDQTY { get; set; }

    /// <summary>
    /// 发料方式
    /// </summary>
    [SugarColumn(ColumnName = "FISSUETYPE", IsNullable = true)]
    public string FISSUETYPE { get; set; } = string.Empty;

    /// <summary>
    /// 作业编码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID", IsNullable = true)]
    public string FPROCESSID { get; set; } = string.Empty;

    /// <summary>
    /// 良品退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FGOODRETURNQTY", IsNullable = true)]
    public decimal? FGOODRETURNQTY { get; set; }

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO", IsNullable = true)]
    public string FMTONO { get; set; } = string.Empty;

    /// <summary>
    /// 货源分录
    /// </summary>
    [SugarColumn(ColumnName = "FSMENTRYID", IsNullable = true)]
    public string FSMENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位作业不良退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEPRCDEFECTRETURNQTY", IsNullable = true)]
    public decimal? FBASEPRCDEFECTRETURNQTY { get; set; }

    /// <summary>
    /// 偏置提前期单位
    /// </summary>
    [SugarColumn(ColumnName = "FTIMEUNIT2", IsNullable = true)]
    public string FTIMEUNIT2 { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位领料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESELPICKEDQTY", IsNullable = true)]
    public decimal? FBASESELPICKEDQTY { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT", IsNullable = true)]
    public string FLOT { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID2", IsNullable = true)]
    public string FBOMID2 { get; set; } = string.Empty;

    /// <summary>
    /// 已领数量
    /// </summary>
    [SugarColumn(ColumnName = "FPICKEDQTY", IsNullable = true)]
    public decimal? FPICKEDQTY { get; set; }

    /// <summary>
    /// 是否发损耗
    /// </summary>
    [SugarColumn(ColumnName = "FISGETSCRAP", IsNullable = true)]
    public bool? FISGETSCRAP { get; set; }

    /// <summary>
    /// 替代策略
    /// </summary>
    [SugarColumn(ColumnName = "FREPLACEPOLICY", IsNullable = true)]
    public string FREPLACEPOLICY { get; set; } = string.Empty;

    /// <summary>
    /// BOM分录内码
    /// </summary>
    [SugarColumn(ColumnName = "FBOMENTRYID", IsNullable = true)]
    public string FBOMENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 是否超发
    /// </summary>
    [SugarColumn(ColumnName = "FALLOWOVER", IsNullable = true)]
    public bool? FALLOWOVER { get; set; }

    /// <summary>
    /// 委外订单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQTYPE1", IsNullable = true)]
    public string FSUBREQTYPE1 { get; set; } = string.Empty;

    /// <summary>
    /// 最后更新日期
    /// </summary>
    [SugarColumn(ColumnName = "FUPDATEDATE", IsNullable = true)]
    public DateTime? FUPDATEDATE { get; set; }

    /// <summary>
    /// 替代主料
    /// </summary>
    [SugarColumn(ColumnName = "FISKEYITEM", IsNullable = true)]
    public bool? FISKEYITEM { get; set; }

    /// <summary>
    /// 预留类型
    /// </summary>
    [SugarColumn(ColumnName = "FRESERVETYPE", IsNullable = true)]
    public string FRESERVETYPE { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位原分子
    /// </summary>
    [SugarColumn(ColumnName = "FBASEBOMNUMERATOR", IsNullable = true)]
    public decimal? FBASEBOMNUMERATOR { get; set; }

    /// <summary>
    /// 基本单位标准用量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESTDQTY", IsNullable = true)]
    public decimal? FBASESTDQTY { get; set; }
}
