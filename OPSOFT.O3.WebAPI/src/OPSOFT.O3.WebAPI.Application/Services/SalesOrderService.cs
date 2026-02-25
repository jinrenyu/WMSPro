using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class SalesOrderService : DocumentService<TSalOrder, TSalOrderentry,
    SalesOrderListDto, SalesOrderDetailDto, CreateSalesOrderRequest, UpdateSalesOrderRequest>
{
    public SalesOrderService(
        IRepository<TSalOrder> headerRepo,
        IRepository<TSalOrderentry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog) { }

    protected override string PrgKey => "SalesOrder";

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");

        var result = await Db.Updateable<TSalOrder>()
            .SetColumns(h => h.Fdocumentstatus == "B")
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

        var result = await Db.Updateable<TSalOrder>()
            .SetColumns(h => h.Fdocumentstatus == "D")
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

        var result = await Db.Updateable<TSalOrder>()
            .SetColumns(h => h.Fclosestatus == "B")
            .SetColumns(h => h.Fcloserid == (CurrentUser.UserId ?? string.Empty))
            .SetColumns(h => h.Fclosedate == DateTime.Now)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<TSalOrder, bool>> BuildSearchPredicate(string keyword)
    {
        return h => h.Fbillno.Contains(keyword) || h.Fcustomerid.Contains(keyword);
    }

    protected override SalesOrderListDto MapToListDto(TSalOrder entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fdate = entity.Fdate,
        Fcustomerid = entity.Fcustomerid,
        Fsaledeptid = entity.Fsaledeptid,
        Fsalerid = entity.Fsalerid,
        Fsettlecurrid = entity.Fsettlecurrid,
        Fdocumentstatus = entity.Fdocumentstatus,
        Fclosestatus = entity.Fclosestatus,
        CYmd = entity.CYmd
    };

    protected override SalesOrderDetailDto MapToDetailDto(TSalOrder header, List<TSalOrderentry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fdate = header.Fdate,
        Fcustomerid = header.Fcustomerid,
        Fsaledeptid = header.Fsaledeptid,
        Fsalerid = header.Fsalerid,
        Fsettlecurrid = header.Fsettlecurrid,
        Fdocumentstatus = header.Fdocumentstatus,
        Fclosestatus = header.Fclosestatus,
        CYmd = header.CYmd,
        Fbilltypeid = header.Fbilltypeid,
        Fbusinesstype = header.Fbusinesstype,
        Fnote = header.Fnote,
        Freceiveaddress = header.Freceiveaddress,
        Flinkman = header.Flinkman,
        Flinkphone = header.Flinkphone,
        Fexchangerate = header.Fexchangerate,
        Fcheckerid = header.Fcheckerid,
        Fcheckdate = header.Fcheckdate,
        Fpredate = header.Fpredate,
        Fsalestyle = header.Fsalestyle,
        Entries = entries.Select(e => new SalesOrderEntryDto
        {
            Uid = e.Uid,
            Fmaterialid = e.Fmaterialid,
            Funitid = e.Funitid,
            Fqty = e.Fqty,
            Fprice = e.Fprice,
            Ftaxprice = e.Ftaxprice,
            Ftaxrate = e.Ftaxrate,
            Ftaxamount = e.Ftaxamount,
            Fisfree = e.Fisfree,
            Fnote = e.Fnote,
            Fstockid = e.Fstockid,
            Flot = e.Flot
        }).ToList()
    };

    protected override TSalOrder MapToHeaderEntity(CreateSalesOrderRequest dto) => new()
    {
        Fdate = dto.Fdate,
        Fcustomerid = dto.Fcustomerid,
        Fsaledeptid = dto.Fsaledeptid,
        Fsalerid = dto.Fsalerid,
        Fsettlecurrid = dto.Fsettlecurrid,
        Fbilltypeid = dto.Fbilltypeid,
        Fbusinesstype = dto.Fbusinesstype,
        Fnote = dto.Fnote,
        Freceiveaddress = dto.Freceiveaddress,
        Flinkman = dto.Flinkman,
        Flinkphone = dto.Flinkphone,
        Fexchangerate = dto.Fexchangerate,
        Fpredate = dto.Fpredate,
        Fsalestyle = dto.Fsalestyle,
        Fdocumentstatus = "A",
        Fclosestatus = "A"
    };

    protected override List<TSalOrderentry> MapToEntryEntities(CreateSalesOrderRequest dto, string headerUid)
    {
        return dto.Entries.Select(e => new TSalOrderentry
        {
            FInterId = headerUid,
            Fmaterialid = e.Fmaterialid,
            Funitid = e.Funitid,
            Fqty = e.Fqty,
            Fprice = e.Fprice,
            Ftaxprice = e.Ftaxprice,
            Ftaxrate = e.Ftaxrate,
            Ftaxamount = e.Ftaxamount,
            Fisfree = e.Fisfree,
            Fnote = e.Fnote,
            Fstockid = e.Fstockid,
            Flot = e.Flot
        }).ToList();
    }

    protected override void UpdateHeaderEntity(TSalOrder entity, UpdateSalesOrderRequest dto)
    {
        entity.Fdate = dto.Fdate;
        entity.Fcustomerid = dto.Fcustomerid;
        entity.Fsaledeptid = dto.Fsaledeptid;
        entity.Fsalerid = dto.Fsalerid;
        entity.Fsettlecurrid = dto.Fsettlecurrid;
        entity.Fbilltypeid = dto.Fbilltypeid;
        entity.Fbusinesstype = dto.Fbusinesstype;
        entity.Fnote = dto.Fnote;
        entity.Freceiveaddress = dto.Freceiveaddress;
        entity.Flinkman = dto.Flinkman;
        entity.Flinkphone = dto.Flinkphone;
        entity.Fexchangerate = dto.Fexchangerate;
        entity.Fpredate = dto.Fpredate;
        entity.Fsalestyle = dto.Fsalestyle;
    }

    protected override List<TSalOrderentry> MapToEntryEntities(UpdateSalesOrderRequest dto, string headerUid)
    {
        return dto.Entries.Select(e => new TSalOrderentry
        {
            FInterId = headerUid,
            Fmaterialid = e.Fmaterialid,
            Funitid = e.Funitid,
            Fqty = e.Fqty,
            Fprice = e.Fprice,
            Ftaxprice = e.Ftaxprice,
            Ftaxrate = e.Ftaxrate,
            Ftaxamount = e.Ftaxamount,
            Fisfree = e.Fisfree,
            Fnote = e.Fnote,
            Fstockid = e.Fstockid,
            Flot = e.Flot
        }).ToList();
    }

    protected override void SetEntryIndex(TSalOrderentry entry, int index)
    {
        // TSalOrderentry inherits from BaseEntity, not BaseEntryEntity
        // No FEntryId field directly, use FInterId as grouping
    }

    protected override async Task<List<TSalOrderentry>> GetEntriesByHeaderIdAsync(string headerUid)
    {
        return await Db.Queryable<TSalOrderentry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .ToListAsync();
    }
}
