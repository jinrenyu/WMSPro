using System.Reflection;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 单据类型花名册（反射发现）：扫描 Application 程序集里所有 DocumentService&lt;THeader,...&gt; 的具体子类，
/// 取出表头实体类名/真实表名，配上中文名与建议 formKey，供编码规则"登记新单据"下拉选择、自动带出技术标识。
/// 新单据只要建了 DocumentService 子类即自动出现在此名册；在 Known 补一行可得友好中文名与建议键（不补则以实体类名兜底）。
/// </summary>
public static class DocumentTypeRegistry
{
    /// <summary>实体类名 → (中文名, 建议formKey)。已登记单据以目录实际值为准，此处仅作未登记时的建议。</summary>
    private static readonly IReadOnlyDictionary<string, (string Name, string FormKey)> Known
        = new Dictionary<string, (string, string)>
        {
            ["TPurPoOrder"] = ("采购订单", BillCodeFormKeys.PurchaseOrder),
            ["TPurReceive"] = ("收料通知单", BillCodeFormKeys.ReceiveBill),
            ["TSalOrder"] = ("销售订单", "SAL_SaleOrder"),
            ["OdkSrmDelivery"] = ("送货单", "SRM_Delivery"),
            ["OdkSrmInvoice"] = ("预制发票", "SRM_Invoice"),
        };

    /// <summary>反射发现所有单据类型（中性信息，未含登记状态），按中文名排序。</summary>
    public static List<DocumentTypeInfo> Discover()
    {
        var baseDef = typeof(DocumentService<,,,,,>);
        var list = new List<DocumentTypeInfo>();
        foreach (var t in baseDef.Assembly.GetTypes())
        {
            if (!t.IsClass || t.IsAbstract) continue;
            // 沿基类链向上找到 DocumentService<,,,,,> 闭合类型，取首个泛型参（THeader 表头实体）
            var b = t.BaseType;
            while (b != null && !(b.IsGenericType && b.GetGenericTypeDefinition() == baseDef))
                b = b.BaseType;
            if (b == null) continue;
            var entityType = b.GetGenericArguments()[0];
            var entityName = entityType.Name;
            var tableName = entityType.GetCustomAttribute<SugarTable>()?.TableName ?? string.Empty;
            var (name, formKey) = Known.TryGetValue(entityName, out var k) ? k : (entityName, entityName);
            list.Add(new DocumentTypeInfo
            {
                EntityName = entityName,
                TableName = tableName,
                SuggestedName = name,
                SuggestedFormKey = formKey
            });
        }
        return list.OrderBy(x => x.SuggestedName).ToList();
    }
}

/// <summary>反射发现的单据类型（中性信息，未含登记状态）</summary>
public class DocumentTypeInfo
{
    public string EntityName { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
    public string SuggestedName { get; set; } = string.Empty;
    public string SuggestedFormKey { get; set; } = string.Empty;
}
