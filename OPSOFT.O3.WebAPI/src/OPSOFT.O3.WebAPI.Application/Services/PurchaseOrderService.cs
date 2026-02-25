using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class PurchaseOrderService : DocumentService<OdkSrmPOOrder, OdkSrmPOOrderEntry,
    PurchaseOrderListDto, PurchaseOrderDetailDto, CreatePurchaseOrderRequest, UpdatePurchaseOrderRequest>
{
    public PurchaseOrderService(
        IRepository<OdkSrmPOOrder> headerRepo,
        IRepository<OdkSrmPOOrderEntry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog) { }

    protected override string PrgKey => "PurchaseOrder";

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.Fbillstatus != 0) throw new InvalidOperationException("只有草稿状态的单据才能审核");

        var result = await Db.Updateable<OdkSrmPOOrder>()
            .SetColumns(h => h.Fbillstatus == 1)
            .SetColumns(h => h.Fcheckerid == (CurrentUser.UserId ?? string.Empty))
            .SetColumns(h => h.Fcheckdate == DateTime.Now)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Approve, uid, header.Fbillno, "审核单据", result);
        return result;
    }

    public override async Task<bool> RejectAsync(string uid, string? reason = null)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");

        var result = await Db.Updateable<OdkSrmPOOrder>()
            .SetColumns(h => h.Fbillstatus == 2)
            .SetColumns(h => h.Frefureason == (reason ?? string.Empty))
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Reject, uid, header.Fbillno, reason ?? "驳回单据", result);
        return result;
    }

    public override async Task<bool> CloseAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");

        var result = await Db.Updateable<OdkSrmPOOrder>()
            .SetColumns(h => h.Fbillstatus == 3)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<OdkSrmPOOrder, bool>> BuildSearchPredicate(string keyword)
    {
        return h => h.Fbillno.Contains(keyword) || h.Fsupplyid.Contains(keyword);
    }

    protected override PurchaseOrderListDto MapToListDto(OdkSrmPOOrder entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fdate = entity.Fdate,
        Fsupplyid = entity.Fsupplyid,
        Fdeptid = entity.Fdeptid,
        Fempid = entity.Fempid,
        Fcurrencyid = entity.Fcurrencyid,
        Fbillstatus = entity.Fbillstatus,
        Fisurgent = entity.Fisurgent,
        CYmd = entity.CYmd
    };

    protected override PurchaseOrderDetailDto MapToDetailDto(OdkSrmPOOrder header, List<OdkSrmPOOrderEntry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fdate = header.Fdate,
        Fsupplyid = header.Fsupplyid,
        Fdeptid = header.Fdeptid,
        Fempid = header.Fempid,
        Fcurrencyid = header.Fcurrencyid,
        Fbillstatus = header.Fbillstatus,
        Fisurgent = header.Fisurgent,
        CYmd = header.CYmd,
        Fmatgroupid = header.Fmatgroupid,
        Fphone = header.Fphone,
        Fdeliverymethod = header.Fdeliverymethod,
        Fdeliverysite = header.Fdeliverysite,
        Fsourcetrantype = header.Fsourcetrantype,
        Fnote = header.Fnote,
        Fcheckerid = header.Fcheckerid,
        Fcheckdate = header.Fcheckdate,
        Frefureason = header.Frefureason,
        Entries = entries.Select(e => new PurchaseOrderEntryDto
        {
            Uid = e.Uid,
            Fitemid = e.Fitemid,
            Funitid = e.Funitid,
            Fqty = e.Fqty,
            Fdemanddate = e.Fdemanddate,
            Fprice = e.Fprice,
            Frreight = e.Frreight,
            Fistax = e.Fistax,
            Fpretaxamt = e.Fpretaxamt,
            Ftaxamt = e.Ftaxamt,
            Famt = e.Famt,
            Ftax = e.Ftax,
            Fnote = e.Fnote,
            Fchecktype = e.Fchecktype,
            Fdetailstatus = e.Fdetailstatus,
            Fentryid = e.Fentryid
        }).ToList()
    };

    protected override OdkSrmPOOrder MapToHeaderEntity(CreatePurchaseOrderRequest dto) => new()
    {
        Fdate = dto.Fdate,
        Fsupplyid = dto.Fsupplyid,
        Fdeptid = dto.Fdeptid,
        Fempid = dto.Fempid,
        Fcurrencyid = dto.Fcurrencyid,
        Fmatgroupid = dto.Fmatgroupid,
        Fphone = dto.Fphone,
        Fdeliverymethod = dto.Fdeliverymethod,
        Fdeliverysite = dto.Fdeliverysite,
        Fisurgent = dto.Fisurgent,
        Fnote = dto.Fnote,
        Fbillstatus = 0
    };

    protected override List<OdkSrmPOOrderEntry> MapToEntryEntities(CreatePurchaseOrderRequest dto, string headerUid)
    {
        return dto.Entries.Select(e => new OdkSrmPOOrderEntry
        {
            FInterId = headerUid,
            Fitemid = e.Fitemid,
            Funitid = e.Funitid,
            Fqty = e.Fqty,
            Fdemanddate = e.Fdemanddate,
            Fprice = e.Fprice,
            Frreight = e.Frreight,
            Fistax = e.Fistax,
            Fpretaxamt = e.Fpretaxamt,
            Ftaxamt = e.Ftaxamt,
            Famt = e.Famt,
            Ftax = e.Ftax,
            Fnote = e.Fnote,
            Fchecktype = e.Fchecktype
        }).ToList();
    }

    protected override void UpdateHeaderEntity(OdkSrmPOOrder entity, UpdatePurchaseOrderRequest dto)
    {
        entity.Fdate = dto.Fdate;
        entity.Fsupplyid = dto.Fsupplyid;
        entity.Fdeptid = dto.Fdeptid;
        entity.Fempid = dto.Fempid;
        entity.Fcurrencyid = dto.Fcurrencyid;
        entity.Fmatgroupid = dto.Fmatgroupid;
        entity.Fphone = dto.Fphone;
        entity.Fdeliverymethod = dto.Fdeliverymethod;
        entity.Fdeliverysite = dto.Fdeliverysite;
        entity.Fisurgent = dto.Fisurgent;
        entity.Fnote = dto.Fnote;
    }

    protected override List<OdkSrmPOOrderEntry> MapToEntryEntities(UpdatePurchaseOrderRequest dto, string headerUid)
    {
        return dto.Entries.Select(e => new OdkSrmPOOrderEntry
        {
            FInterId = headerUid,
            Fitemid = e.Fitemid,
            Funitid = e.Funitid,
            Fqty = e.Fqty,
            Fdemanddate = e.Fdemanddate,
            Fprice = e.Fprice,
            Frreight = e.Frreight,
            Fistax = e.Fistax,
            Fpretaxamt = e.Fpretaxamt,
            Ftaxamt = e.Ftaxamt,
            Famt = e.Famt,
            Ftax = e.Ftax,
            Fnote = e.Fnote,
            Fchecktype = e.Fchecktype
        }).ToList();
    }

    protected override void SetEntryIndex(OdkSrmPOOrderEntry entry, int index)
    {
        entry.Fentryid = index;
        entry.Fdetailid = entry.Uid;
    }

    protected override async Task<List<OdkSrmPOOrderEntry>> GetEntriesByHeaderIdAsync(string headerUid)
    {
        return await Db.Queryable<OdkSrmPOOrderEntry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .OrderBy(e => e.Fentryid)
            .ToListAsync();
    }
}
