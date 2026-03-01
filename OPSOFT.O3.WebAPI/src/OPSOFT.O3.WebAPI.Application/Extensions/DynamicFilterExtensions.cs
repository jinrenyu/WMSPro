using System.Reflection;
using OPSOFT.O3.WebAPI.Application.DTOs;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Extensions;

public static class DynamicFilterExtensions
{
    public static List<IConditionalModel> ToConditionalModels<T>(this List<DynamicFilterInfo> filters)
    {
        var models = new List<IConditionalModel>();
        if (filters == null || filters.Count == 0) return models;

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

        foreach (var filter in filters)
        {
            if (string.IsNullOrWhiteSpace(filter.Field)) continue;

            var condType = filter.Operator.ToLower() switch
            {
                "eq" => ConditionalType.Equal,
                "like" => ConditionalType.Like,
                "gt" => ConditionalType.GreaterThan,
                "lt" => ConditionalType.LessThan,
                "ge" => ConditionalType.GreaterThanOrEqual,
                "le" => ConditionalType.LessThanOrEqual,
                "in" => ConditionalType.In,
                "notin" => ConditionalType.NotIn,
                "neq" => ConditionalType.NoEqual,
                "inlike" => ConditionalType.Like, // 逗号分隔的 like 可以在此处处理或者前端分拆
                _ => ConditionalType.Equal
            };

            // 自动将含有逗号的 Equal/NoEqual 转换为 In/NotIn
            if (!string.IsNullOrEmpty(filter.Value) && filter.Value.Contains(','))
            {
                if (condType == ConditionalType.Equal) condType = ConditionalType.In;
                else if (condType == ConditionalType.NoEqual) condType = ConditionalType.NotIn;
            }

            var prop = properties.FirstOrDefault(p => string.Equals(p.Name, filter.Field, StringComparison.OrdinalIgnoreCase));
            string csharpTypeName = string.Empty;
            string mappedFieldName = filter.Field;

            if (prop != null)
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                csharpTypeName = type.Name.ToLower();
                
                if (csharpTypeName == "int32") csharpTypeName = "int";
                else if (csharpTypeName == "int64") csharpTypeName = "long";
                else if (csharpTypeName == "int16") csharpTypeName = "short";
                else if (csharpTypeName == "boolean") csharpTypeName = "bool";
                else if (csharpTypeName == "single") csharpTypeName = "float";
                else if (csharpTypeName == "decimal" || csharpTypeName == "double") { /* 保持原样 */ }

                // 防止对数字类型使用 Like 导致 Postgres 报错 (numeric ~~ text)
                if (condType == ConditionalType.Like && 
                    (csharpTypeName == "int" || csharpTypeName == "long" || csharpTypeName == "short" || 
                     csharpTypeName == "decimal" || csharpTypeName == "double" || csharpTypeName == "float"))
                {
                    condType = ConditionalType.Equal;
                }

                // 如果有 SugarColumn 特性，优先使用实际表的列名，避免实体类名与表列名不一致（例如 CYmd -> C_YMD）
                var sugarColumnAtt = prop.GetCustomAttribute<SugarColumn>();
                if (sugarColumnAtt != null && !string.IsNullOrEmpty(sugarColumnAtt.ColumnName))
                {
                    mappedFieldName = sugarColumnAtt.ColumnName;
                }
                else 
                {
                    mappedFieldName = prop.Name;
                }
            }

            models.Add(new ConditionalModel
            {
                FieldName = mappedFieldName,
                ConditionalType = condType,
                FieldValue = filter.Value ?? string.Empty,
                CSharpTypeName = csharpTypeName
            });
        }

        return models;
    }
}
