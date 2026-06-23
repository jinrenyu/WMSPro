using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 出入库流程配置服务接口：在通用单据契约（一主一从）之上，追加「禁用 / 反禁用」能力。
/// </summary>
public interface ISelBillService : IDocumentService<TBosSelbill, TBosSelbillentry,
    SelBillListDto, SelBillDetailDto, CreateSelBillRequest, UpdateSelBillRequest>
{
    /// <summary>禁用（FDisabled=1，记录禁用人/禁用日期）。被禁用的流程配置不再作为源单类型下拉项。</summary>
    Task<bool> DisableAsync(string uid);

    /// <summary>反禁用（FDisabled=0，清空禁用人/禁用日期）。</summary>
    Task<bool> EnableAsync(string uid);

    /// <summary>
    /// 按源单类型查可下推的目标单据类型列表（出入库流程配置的「反向」查询）。
    /// 条件：Fsourcetrantype==源单模板 &amp;&amp; Fisuse &amp;&amp; 未禁用 &amp;&amp; 未删除；返回去重后的目标 Fdesttrantype（带名称）。
    /// 供采购订单/收料通知单/退料申请单等源单维护页的「下推」按钮取可选目标。
    /// </summary>
    Task<List<PushDownTargetDto>> GetPushDownTargetsAsync(string sourceTranType);

    /// <summary>
    /// 下查取可追溯的目标单据类型列表：按源单类型查在出入库流程配置里配置过的目标类型，
    /// 【只排除已删除，不看启用/禁用】——下查反映历史既成事实，不受流程当前启用态影响（与 GetPushDownTargetsAsync 相反）。
    /// 供源单维护页「下查」按钮取候选目标类型。
    /// </summary>
    Task<List<PushDownTargetDto>> GetTraceTargetsAsync(string sourceTranType);

    /// <summary>
    /// 下查（正向追溯）：列出由指定源单生成的、指定类型的下游单据。
    /// 条件：目标单据表头 Fsrcformid==源单模板 &amp;&amp; Fsrcbillno==源单号 &amp;&amp; FStatus==40（已审核）&amp;&amp; 未禁用 &amp;&amp; 未删除。
    /// 仅支持已知目标类型（STK_InStock 采购入库单 / PUR_MRB 采购退料单），其余返回空。
    /// 供采购订单/收料通知单/退料申请单等源单维护页的「下查」按钮列出可跳转的下游单据。
    /// </summary>
    /// <param name="sourceType">源单模板编号（SYS_BILLTEMPLATE.FNUMBER），如 PUR_PurchaseOrder。</param>
    /// <param name="sourceBillNo">源单编号。</param>
    /// <param name="targetType">目标单据模板编号，如 STK_InStock。</param>
    Task<List<DownstreamDocDto>> GetDownstreamDocsAsync(string sourceType, string sourceBillNo, string targetType);
}
