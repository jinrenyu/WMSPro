using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class InvoiceService : DocumentService<OdkSrmInvoice, OdkSrmInvoiceEntry,
    InvoiceListDto, InvoiceDetailDto, CreateInvoiceRequest, UpdateInvoiceRequest>
{
    public InvoiceService(
        IRepository<OdkSrmInvoice> headerRepo,
        IRepository<OdkSrmInvoiceEntry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog) { }

    protected override string PrgKey => "Invoice";

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");

        var result = await Db.Updateable<OdkSrmInvoice>()
            .SetColumns(h => h.Fbillstatus == 1)
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

        var result = await Db.Updateable<OdkSrmInvoice>()
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

        var result = await Db.Updateable<OdkSrmInvoice>()
            .SetColumns(h => h.Fbillstatus == 3)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<OdkSrmInvoice, bool>> BuildSearchPredicate(string keyword)
    {
        return h => h.Fbillno.Contains(keyword) || h.Fsupplyid.Contains(keyword);
    }

    protected override InvoiceListDto MapToListDto(OdkSrmInvoice entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fdate = entity.Fdate,
        Fsupplyid = entity.Fsupplyid,
        Fcurrencyid = entity.Fcurrencyid,
        Fsettleuntaxamount = entity.Fsettleuntaxamount,
        Fsettletaxamount = entity.Fsettletaxamount,
        Ftaxamount = entity.Ftaxamount,
        Fbillstatus = entity.Fbillstatus,
        CYmd = entity.CYmd
    };

    protected override InvoiceDetailDto MapToDetailDto(OdkSrmInvoice header, List<OdkSrmInvoiceEntry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fdate = header.Fdate,
        Fsupplyid = header.Fsupplyid,
        Fcurrencyid = header.Fcurrencyid,
        Fsettleuntaxamount = header.Fsettleuntaxamount,
        Fsettletaxamount = header.Fsettletaxamount,
        Ftaxamount = header.Ftaxamount,
        Fbillstatus = header.Fbillstatus,
        CYmd = header.CYmd,
        Frob = header.Frob,
        Ftaxinvoice = header.Ftaxinvoice,
        Fnote = header.Fnote,
        Fverifynote = header.Fverifynote,
        Freverifynote = header.Freverifynote,
        Frefureason = header.Frefureason,
        Entries = entries.Select(e => new InvoiceEntryDto
        {
            Uid = e.Uid,
            Fitemid = e.Fitemid,
            Funitid = e.Funitid,
            Fqty = e.Fqty,
            Fbatchno = e.Fbatchno,
            Fprice = e.Fprice,
            Fistax = e.Fistax,
            Ftax = e.Ftax,
            Fpretaxamt = e.Fpretaxamt,
            Ftaxamt = e.Ftaxamt,
            Famt = e.Famt,
            Fnote = e.Fnote,
            Fentryid = e.Fentryid
        }).ToList()
    };

    protected override OdkSrmInvoice MapToHeaderEntity(CreateInvoiceRequest dto) => new()
    {
        Fdate = dto.Fdate,
        Fsupplyid = dto.Fsupplyid,
        Fcurrencyid = dto.Fcurrencyid,
        Frob = dto.Frob,
        Ftaxinvoice = dto.Ftaxinvoice,
        Fnote = dto.Fnote,
        Fbillstatus = 0
    };

    protected override List<OdkSrmInvoiceEntry> MapToEntryEntities(CreateInvoiceRequest dto, string headerUid)
    {
        return dto.Entries.Select(e => new OdkSrmInvoiceEntry
        {
            FInterId = headerUid,
            Finstockinterid = e.Finstockinterid,
            Finstockdetailid = e.Finstockdetailid,
            Fpointerid = e.Fpointerid,
            Fpodetailid = e.Fpodetailid,
            Fdlinterid = e.Fdlinterid,
            Fdldetailid = e.Fdldetailid,
            Fitemid = e.Fitemid,
            Funitid = e.Funitid,
            Fqty = e.Fqty,
            Fbatchno = e.Fbatchno,
            Fprice = e.Fprice,
            Fistax = e.Fistax,
            Ftax = e.Ftax,
            Fpretaxamt = e.Fpretaxamt,
            Ftaxamt = e.Ftaxamt,
            Famt = e.Famt,
            Fnote = e.Fnote
        }).ToList();
    }

    protected override void UpdateHeaderEntity(OdkSrmInvoice entity, UpdateInvoiceRequest dto)
    {
        entity.Fdate = dto.Fdate;
        entity.Fsupplyid = dto.Fsupplyid;
        entity.Fcurrencyid = dto.Fcurrencyid;
        entity.Frob = dto.Frob;
        entity.Ftaxinvoice = dto.Ftaxinvoice;
        entity.Fnote = dto.Fnote;
    }

    protected override List<OdkSrmInvoiceEntry> MapToEntryEntities(UpdateInvoiceRequest dto, string headerUid)
    {
        return dto.Entries.Select(e => new OdkSrmInvoiceEntry
        {
            FInterId = headerUid,
            Finstockinterid = e.Finstockinterid,
            Finstockdetailid = e.Finstockdetailid,
            Fpointerid = e.Fpointerid,
            Fpodetailid = e.Fpodetailid,
            Fdlinterid = e.Fdlinterid,
            Fdldetailid = e.Fdldetailid,
            Fitemid = e.Fitemid,
            Funitid = e.Funitid,
            Fqty = e.Fqty,
            Fbatchno = e.Fbatchno,
            Fprice = e.Fprice,
            Fistax = e.Fistax,
            Ftax = e.Ftax,
            Fpretaxamt = e.Fpretaxamt,
            Ftaxamt = e.Ftaxamt,
            Famt = e.Famt,
            Fnote = e.Fnote
        }).ToList();
    }

    protected override void SetEntryIndex(OdkSrmInvoiceEntry entry, int index)
    {
        entry.Fentryid = index;
        entry.Fdetailid = entry.Uid;
    }

    protected override async Task<List<OdkSrmInvoiceEntry>> GetEntriesByHeaderIdAsync(string headerUid)
    {
        return await Db.Queryable<OdkSrmInvoiceEntry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .OrderBy(e => e.Fentryid)
            .ToListAsync();
    }
}
