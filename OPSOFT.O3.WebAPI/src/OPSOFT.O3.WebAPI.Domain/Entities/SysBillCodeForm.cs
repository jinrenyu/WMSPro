using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 编码规则业务表单登记表（数据驱动目录）：登记"哪些单据可以配置编码规则"。
/// 取代原先写死在代码里的 BillCodeFieldRegistry.Forms——新单据在界面登记一行即出现在配置页。
/// FENTITYNAME 为单据表头实体类名（如 TPurPoOrder），供 DocumentService 基类按实体反射解析 formKey、自动取号；
/// 留空则该单据需在其 Service 显式覆盖 BillCodeFormKey。本表为系统自有配置表（非 K3 原生），生产需补建表脚本。
/// </summary>
[SugarTable("SYS_BILLCODEFORM")]
[SugarIndex("SYS_BILLCODEFORM_1", nameof(Fformkey), OrderByType.Asc, true)]
public class SysBillCodeForm : BaseEntity
{
    /// <summary>业务表单标识（= SYS_LISTCODE/SYS_BARCODE 的 FCLASSTYPEID）</summary>
    [SugarColumn(ColumnName = "FFORMKEY", Length = 100)]
    public string Fformkey { get; set; } = string.Empty;

    /// <summary>表单友好名称（界面显示，如"采购订单"）</summary>
    [SugarColumn(ColumnName = "FFORMNAME", Length = 100)]
    public string Fformname { get; set; } = string.Empty;

    /// <summary>单据表头实体类名（如 TPurPoOrder）；供基类反射取号匹配，留空表示由代码显式声明 formKey</summary>
    [SugarColumn(ColumnName = "FENTITYNAME", Length = 100)]
    public string Fentityname { get; set; } = string.Empty;
}
