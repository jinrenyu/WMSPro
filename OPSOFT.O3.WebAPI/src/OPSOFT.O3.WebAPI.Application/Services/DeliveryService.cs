using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class DeliveryService : DocumentService<OdkSrmDelivery, OdkSrmDeliveryEntry,
    DeliveryListDto, DeliveryDetailDto, CreateDeliveryRequest, UpdateDeliveryRequest>
{
    public DeliveryService(
        IRepository<OdkSrmDelivery> headerRepo,
        IRepository<OdkSrmDeliveryEntry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog) { }

    protected override string PrgKey => "Delivery";

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");

        var result = await Db.Updateable<OdkSrmDelivery>()
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

        var result = await Db.Updateable<OdkSrmDelivery>()
            .SetColumns(h => h.Fbillstatus == 2)
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

        var result = await Db.Updateable<OdkSrmDelivery>()
            .SetColumns(h => h.Fbillstatus == 3)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<OdkSrmDelivery, bool>> BuildSearchPredicate(string keyword)
    {
        return h => h.Fbillno.Contains(keyword) || h.Fsupplyid.Contains(keyword);
    }

    protected override DeliveryListDto MapToListDto(OdkSrmDelivery entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fdeliverydate = entity.Fdeliverydate,
        Fsupplyid = entity.Fsupplyid,
        Fdeliverytype = entity.Fdeliverytype,
        Fcontactname = entity.Fcontactname,
        Fbillstatus = entity.Fbillstatus,
        CYmd = entity.CYmd
    };

    protected override DeliveryDetailDto MapToDetailDto(OdkSrmDelivery header, List<OdkSrmDeliveryEntry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fdeliverydate = header.Fdeliverydate,
        Fsupplyid = header.Fsupplyid,
        Fdeliverytype = header.Fdeliverytype,
        Fcontactname = header.Fcontactname,
        Fbillstatus = header.Fbillstatus,
        CYmd = header.CYmd,
        Faddress = header.Faddress,
        Fcontactphone = header.Fcontactphone,
        Farrivaldate = header.Farrivaldate,
        Fexpress = header.Fexpress,
        Flogistics = header.Flogistics,
        Fnote = header.Fnote,
        Fcheckerid = header.Fcheckerid,
        Fcheckdate = header.Fcheckdate,
        Entries = entries.Select(e => new DeliveryEntryDto
        {
            Uid = e.Uid,
            Fpointerid = e.Fpointerid,
            Fitemid = e.Fitemid,
            Funitid = e.Funitid,
            Fpoqty = e.Fpoqty,
            Fmustqty = e.Fmustqty,
            Fqty = e.Fqty,
            Fboxnum = e.Fboxnum,
            Fboxqty = e.Fboxqty,
            Fbatchno = e.Fbatchno,
            Fdetailstatus = e.Fdetailstatus
        }).ToList()
    };

    protected override OdkSrmDelivery MapToHeaderEntity(CreateDeliveryRequest dto) => new()
    {
        Fdeliverydate = dto.Fdeliverydate,
        Fsupplyid = dto.Fsupplyid,
        Fdeliverytype = dto.Fdeliverytype,
        Faddress = dto.Faddress,
        Fcontactname = dto.Fcontactname,
        Fcontactphone = dto.Fcontactphone,
        Farrivaldate = dto.Farrivaldate,
        Fexpress = dto.Fexpress,
        Flogistics = dto.Flogistics,
        Fnote = dto.Fnote,
        Fbillstatus = 0
    };

    protected override List<OdkSrmDeliveryEntry> MapToEntryEntities(CreateDeliveryRequest dto, string headerUid)
    {
        return dto.Entries.Select(e => new OdkSrmDeliveryEntry
        {
            FInterId = headerUid,
            Fpointerid = e.Fpointerid,
            Fpodetailid = e.Fpodetailid,
            Fpoentryid = e.Fpoentryid,
            Fitemid = e.Fitemid,
            Funitid = e.Funitid,
            Fpoqty = e.Fpoqty,
            Fmustqty = e.Fmustqty,
            Fqty = e.Fqty,
            Fboxnum = e.Fboxnum,
            Fboxqty = e.Fboxqty,
            Fbatchno = e.Fbatchno
        }).ToList();
    }

    protected override void UpdateHeaderEntity(OdkSrmDelivery entity, UpdateDeliveryRequest dto)
    {
        entity.Fdeliverydate = dto.Fdeliverydate;
        entity.Fsupplyid = dto.Fsupplyid;
        entity.Fdeliverytype = dto.Fdeliverytype;
        entity.Faddress = dto.Faddress;
        entity.Fcontactname = dto.Fcontactname;
        entity.Fcontactphone = dto.Fcontactphone;
        entity.Farrivaldate = dto.Farrivaldate;
        entity.Fexpress = dto.Fexpress;
        entity.Flogistics = dto.Flogistics;
        entity.Fnote = dto.Fnote;
    }

    protected override List<OdkSrmDeliveryEntry> MapToEntryEntities(UpdateDeliveryRequest dto, string headerUid)
    {
        return dto.Entries.Select(e => new OdkSrmDeliveryEntry
        {
            FInterId = headerUid,
            Fpointerid = e.Fpointerid,
            Fpodetailid = e.Fpodetailid,
            Fpoentryid = e.Fpoentryid,
            Fitemid = e.Fitemid,
            Funitid = e.Funitid,
            Fpoqty = e.Fpoqty,
            Fmustqty = e.Fmustqty,
            Fqty = e.Fqty,
            Fboxnum = e.Fboxnum,
            Fboxqty = e.Fboxqty,
            Fbatchno = e.Fbatchno
        }).ToList();
    }

    protected override void SetEntryIndex(OdkSrmDeliveryEntry entry, int index)
    {
        // OdkSrmDeliveryEntry inherits from BaseEntity, no FEntryId
    }

    protected override async Task<List<OdkSrmDeliveryEntry>> GetEntriesByHeaderIdAsync(string headerUid)
    {
        return await Db.Queryable<OdkSrmDeliveryEntry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .ToListAsync();
    }
}
