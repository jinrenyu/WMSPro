namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// ERP 取数抽象。契约对齐金蝶云星空 K3Cloud WebAPI 的 ExecuteBillQuery：
/// 输入 表单ID/查询字段/过滤/排序，返回按 fieldKeys 顺序对齐的二维数据。
/// 实现可切换：StubErpDataSource（模拟数据，先行联调）/ K3CloudErpDataSource（真连）。
/// </summary>
public interface IErpDataSource
{
    /// <param name="formId">ERP 表单标识（如 BD_MATERIAL）</param>
    /// <param name="fieldKeys">查询字段，逗号分隔（如 FMATERIALID,FNUMBER,FNAME）</param>
    /// <param name="filterString">过滤条件（SQL 片段，如 FDOCUMENTSTATUS='C'）</param>
    /// <param name="orderString">排序字段</param>
    Task<ErpQueryResult> ExecuteBillQueryAsync(string formId, string fieldKeys, string filterString, string orderString);
}

/// <summary>ERP 查询结果：字段名列表 + 与之对齐的行数据</summary>
public class ErpQueryResult
{
    /// <summary>字段名（大写、去空格），与每行数组下标一一对应</summary>
    public List<string> Fields { get; set; } = new();

    /// <summary>行数据，每行长度 = Fields.Count</summary>
    public List<object?[]> Rows { get; set; } = new();
}
