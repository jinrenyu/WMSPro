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
}
