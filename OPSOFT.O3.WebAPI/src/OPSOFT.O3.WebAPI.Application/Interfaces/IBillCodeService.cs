using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 编码规则取号服务：单据编号走 SYS_LISTCODE/ENTRY/NO，条码编号走 SYS_BARCODE/ENTRY/NO。
/// context 按 BillCodeFields 键提供动态字段值（规则配置了对应字段段才会用到，缺值时抛业务异常）。
/// 流水段在 SYS_*CODENO 上做 DB 端原子占号——必须在调用方事务内使用，回滚时号一并释放。
/// </summary>
public interface IBillCodeService
{
    /// <summary>
    /// 解析单据编号：manualNo 非空时按规则头 ISMODIFY 校验后采用（未配置规则时直接采用），
    /// 为空时按规则生成下一个编号。
    /// existsAsync 用于查目标单据表编号是否已被占用（含软删行）：手工号重复时抛业务异常；
    /// 自动取号撞到已占用编号（如手工抢号）时在事务内跳号重取，避免唯一索引冲突回滚后永远重取同号。
    /// </summary>
    Task<string> ResolveBillNoAsync(string formKey, string? manualNo,
        IReadOnlyDictionary<string, string>? context = null, Func<string, Task<bool>>? existsAsync = null);

    /// <summary>
    /// 由单据表头实体类名解析其编码规则 formKey（数据驱动目录 SYS_BILLCODEFORM）。
    /// DocumentService 基类反射取号用：单据未显式声明 formKey 时按实体名匹配；未登记返回 null。
    /// </summary>
    Task<string?> ResolveFormKeyByEntityAsync(string entityName);

    /// <summary>按单据编码规则批量取号</summary>
    Task<List<string>> NextBillNosAsync(string formKey, int count, IReadOnlyDictionary<string, string>? context = null);

    /// <summary>按条码编码规则批量取号</summary>
    Task<List<string>> NextBarcodesAsync(string formKey, int count, IReadOnlyDictionary<string, string>? context = null);

    /// <summary>
    /// 取号预览（不占号）：按已保存规则、用字段白名单样例值 + 当前日期构造上下文，
    /// 读该流水键当前已占用流水，算出"下一个号"。barcode=true 预览条码规则，否则单据编号规则。
    /// </summary>
    Task<BillCodePreviewDto> PreviewAsync(string formKey, bool barcode);
}
