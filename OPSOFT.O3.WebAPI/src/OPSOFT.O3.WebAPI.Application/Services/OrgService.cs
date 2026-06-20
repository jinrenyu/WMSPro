using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class OrgService : IOrgService
{
    private readonly IRepository<SysUserOrg> _userOrgRepo;
    private readonly IRepository<SysOrgStructure> _orgRepo;
    private readonly ICurrentUserService _currentUser;

    public OrgService(
        IRepository<SysUserOrg> userOrgRepo,
        IRepository<SysOrgStructure> orgRepo,
        ICurrentUserService currentUser)
    {
        _userOrgRepo = userOrgRepo;
        _orgRepo = orgRepo;
        _currentUser = currentUser;
    }

    public async Task<List<MyOrgDto>> GetMyOrgsAsync()
    {
        var userId = _currentUser.UserId ?? string.Empty;
        if (string.IsNullOrEmpty(userId)) return new List<MyOrgDto>();

        var links = await _userOrgRepo.GetListAsync(x => x.UserId == userId);
        if (links.Count == 0) return new List<MyOrgDto>();

        var orgIds = links.Select(l => l.Forgid).Distinct().ToList();
        var orgs = await _orgRepo.GetListAsync(o => orgIds.Contains(o.Uid));
        var dict = orgs.ToDictionary(o => o.Uid);

        // 解析上级（所属）组织名称
        var parentIds = orgs.Where(o => !string.IsNullOrEmpty(o.Fparaid)).Select(o => o.Fparaid).Distinct().ToList();
        var parentDict = parentIds.Count > 0
            ? (await _orgRepo.GetListAsync(o => parentIds.Contains(o.Uid))).ToDictionary(p => p.Uid)
            : new Dictionary<string, SysOrgStructure>();

        return links
            .Select(l =>
            {
                dict.TryGetValue(l.Forgid, out var o);
                var hasParent = o != null && !string.IsNullOrEmpty(o.Fparaid);
                // 公司 = 该组织的 FPARAID（无上级则取自身）
                var parentId = hasParent ? o!.Fparaid : l.Forgid;
                var parentName = hasParent && parentDict.TryGetValue(o!.Fparaid, out var p)
                    ? p.Fname
                    : (o?.Fname ?? string.Empty);
                return new MyOrgDto
                {
                    OrgId = l.Forgid,
                    OrgNumber = o?.Fnumber ?? string.Empty,
                    OrgName = o?.Fname ?? l.Forgid,
                    ParentOrgId = parentId,
                    ParentOrgName = parentName,
                    IsDefault = l.Fisdefault
                };
            })
            .OrderByDescending(x => x.IsDefault)
            .ThenBy(x => x.OrgNumber)
            .ToList();
    }

    public async Task<List<LookupDto>> GetLookupAsync(LookupRequest request)
    {
        // 组织数据量小，取全量后内存过滤/截断，避免捕获布尔进入表达式翻译
        var orgs = await _orgRepo.GetListAsync(o => !o.FDeleted);
        var kw = request.Keyword?.Trim();
        IEnumerable<SysOrgStructure> q = orgs;
        if (!string.IsNullOrEmpty(kw))
            q = q.Where(o => (o.Fnumber ?? string.Empty).Contains(kw) || (o.Fname ?? string.Empty).Contains(kw));
        return q.OrderBy(o => o.Fnumber)
            .Take(request.MaxCount)
            .Select(o => new LookupDto { Uid = o.Uid, FNumber = o.Fnumber, FName = o.Fname })
            .ToList();
    }
}
