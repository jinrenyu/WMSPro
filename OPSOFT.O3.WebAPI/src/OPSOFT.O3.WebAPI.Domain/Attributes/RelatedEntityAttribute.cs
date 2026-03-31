namespace OPSOFT.O3.WebAPI.Domain.Attributes;

/// <summary>
/// 标记外键属性的关联实体类型，用于自动关联排序
/// 放在 FK 属性上（如 FBaseUnitId），指定关联的实体类型（如 TBdUnit）
/// Repository 会自动通过此属性生成 LEFT JOIN + ORDER BY 关联表的 FName
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class RelatedEntityAttribute : Attribute
{
    /// <summary>
    /// 关联实体类型
    /// </summary>
    public Type EntityType { get; }

    /// <summary>
    /// 关联表中用于排序的字段名（默认 FName）
    /// </summary>
    public string SortField { get; set; } = "FName";

    public RelatedEntityAttribute(Type entityType)
    {
        EntityType = entityType;
    }
}
