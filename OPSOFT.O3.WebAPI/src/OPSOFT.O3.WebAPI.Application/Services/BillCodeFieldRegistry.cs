using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 编码规则的业务表单标识（与 T_BAS_BILLTYPE.FBILLFORMID 一致；规则按表单一条，
/// 存 SYS_LISTCODE/SYS_BARCODE 的 FCLASSTYPEID）
/// </summary>
public static class BillCodeFormKeys
{
    public const string PurchaseOrder = "PUR_PurchaseOrder";
    public const string ReceiveBill = "PUR_ReceiveBill";
}

/// <summary>
/// 编码规则动态字段标识（分段 FIELDID 的取值；调用取号服务时按这些键提供上下文值）
/// </summary>
public static class BillCodeFields
{
    /// <summary>日期（单据日期/打印日期，值为可解析的日期字符串）</summary>
    public const string Date = "FDATE";
    /// <summary>单据类型编码（T_BAS_BILLTYPE.FNUMBER）</summary>
    public const string BillType = "FBILLTYPE";
    /// <summary>供应商编码（T_BD_SUPPLIER.FNUMBER）</summary>
    public const string Supplier = "FSUPPLIER";
    /// <summary>物料编码（T_BD_MATERIAL.FNUMBER）</summary>
    public const string Material = "FMATERIAL";
    /// <summary>批次</summary>
    public const string Lot = "FLOT";
    /// <summary>源单编号</summary>
    public const string SourceBillNo = "FSRCBILLNO";
}

/// <summary>
/// 编码规则字段白名单注册表：各表单"单据编号/条码编号"可用动态字段。
/// 不做反射任意实体字段——调用方生成编号时必须按 BillCodeFields 键显式提供上下文值，
/// 带字段段的单据接入时在此登记字段并在创建处把上下文传齐；纯常量/日期/流水的单据无需登记字段。
/// 注：业务表单清单（哪些单据可配规则）已数据驱动，迁至 SYS_BILLCODEFORM / IBillCodeFormCatalog。
/// </summary>
public static class BillCodeFieldRegistry
{
    private static readonly IReadOnlyList<BillCodeFieldDto> PurBillFields = new List<BillCodeFieldDto>
    {
        new() { FieldId = BillCodeFields.Date, FieldName = "单据日期", Kind = "date" },
        new() { FieldId = BillCodeFields.BillType, FieldName = "单据类型编码", Kind = "text", Sample = "CGDD01_SYS" },
        new() { FieldId = BillCodeFields.Supplier, FieldName = "供应商编码", Kind = "text", Sample = "GYS001" },
    };

    private static readonly IReadOnlyList<BillCodeFieldDto> PurBarcodeFields = new List<BillCodeFieldDto>
    {
        new() { FieldId = BillCodeFields.Date, FieldName = "打印日期", Kind = "date" },
        new() { FieldId = BillCodeFields.Material, FieldName = "物料编码", Kind = "text", Sample = "MAT001" },
        new() { FieldId = BillCodeFields.Lot, FieldName = "批次", Kind = "text", Sample = "LOT001" },
        new() { FieldId = BillCodeFields.SourceBillNo, FieldName = "源单编号", Kind = "text", Sample = "CGDD202606100001" },
        new() { FieldId = BillCodeFields.Supplier, FieldName = "供应商编码", Kind = "text", Sample = "GYS001" },
    };

    /// <summary>单据编号可用动态字段</summary>
    public static IReadOnlyList<BillCodeFieldDto> GetBillFields(string formKey) => formKey switch
    {
        BillCodeFormKeys.PurchaseOrder or BillCodeFormKeys.ReceiveBill => PurBillFields,
        _ => Array.Empty<BillCodeFieldDto>()
    };

    /// <summary>条码编号可用动态字段</summary>
    public static IReadOnlyList<BillCodeFieldDto> GetBarcodeFields(string formKey) => formKey switch
    {
        BillCodeFormKeys.PurchaseOrder or BillCodeFormKeys.ReceiveBill => PurBarcodeFields,
        _ => Array.Empty<BillCodeFieldDto>()
    };
}
