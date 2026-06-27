using System.Linq.Expressions;

namespace OPSOFT.O3.WebAPI.Application.Extensions;

/// <summary>
/// 表达式谓词合并助手。
/// 用参数重绑定（ExpressionVisitor）的方式把两个 Lambda 合并为一棵干净的表达式树，
/// 而不是用 <see cref="Expression.Invoke(Expression, IEnumerable{Expression})"/>。
/// 原因：SqlSugar（以及多数 ORM）的表达式解析器无法翻译 InvocationExpression，
/// 遇到时会在生成的 SQL 里留下未替换的 "{0}" 模板占位符，导致 SQLite 报
/// 'unrecognized token: "{"'（在 PostgreSQL/SQLServer 上同样会生成非法 SQL）。
/// </summary>
public static class PredicateBuilder
{
    /// <summary>
    /// 合并两个谓词为 (left AND right)，共用同一个参数，生成 SqlSugar 可翻译的表达式树。
    /// </summary>
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var leftBody = new ReplaceParameterVisitor(left.Parameters[0], param).Visit(left.Body)!;
        var rightBody = new ReplaceParameterVisitor(right.Parameters[0], param).Visit(right.Body)!;
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftBody, rightBody), param);
    }

    /// <summary>
    /// 合并两个谓词为 (left OR right)，共用同一个参数。
    /// </summary>
    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var leftBody = new ReplaceParameterVisitor(left.Parameters[0], param).Visit(left.Body)!;
        var rightBody = new ReplaceParameterVisitor(right.Parameters[0], param).Visit(right.Body)!;
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(leftBody, rightBody), param);
    }

    /// <summary>把表达式中某个参数节点替换为统一的目标参数。</summary>
    private sealed class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _from;
        private readonly ParameterExpression _to;

        public ReplaceParameterVisitor(ParameterExpression from, ParameterExpression to)
        {
            _from = from;
            _to = to;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => node == _from ? _to : base.VisitParameter(node);
    }
}
