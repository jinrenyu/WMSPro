using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 销售订单 =====

public class SalesOrderListDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fcustomerid { get; set; } = string.Empty;
    public string Fsaledeptid { get; set; } = string.Empty;
    public string Fsalerid { get; set; } = string.Empty;
    public string Fsettlecurrid { get; set; } = string.Empty;
    public string Fdocumentstatus { get; set; } = string.Empty;
    public string Fclosestatus { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
}

public class SalesOrderDetailDto : SalesOrderListDto
{
    public string Fbilltypeid { get; set; } = string.Empty;
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public string Freceiveaddress { get; set; } = string.Empty;
    public string Flinkman { get; set; } = string.Empty;
    public string Flinkphone { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; }
    public string Fcheckerid { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public DateTime? Fpredate { get; set; }
    public string Fsalestyle { get; set; } = string.Empty;
    public List<SalesOrderEntryDto> Entries { get; set; } = new();
}

public class SalesOrderEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fmaterialid { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public decimal Fprice { get; set; }
    public decimal Ftaxprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Ftaxamount { get; set; }
    public bool Fisfree { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public string Fstockid { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
}

public class CreateSalesOrderRequest
{
    public DateTime? Fdate { get; set; }
    public string Fcustomerid { get; set; } = string.Empty;
    public string Fsaledeptid { get; set; } = string.Empty;
    public string Fsalerid { get; set; } = string.Empty;
    public string Fsettlecurrid { get; set; } = string.Empty;
    public string Fbilltypeid { get; set; } = string.Empty;
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public string Freceiveaddress { get; set; } = string.Empty;
    public string Flinkman { get; set; } = string.Empty;
    public string Flinkphone { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; }
    public DateTime? Fpredate { get; set; }
    public string Fsalestyle { get; set; } = string.Empty;
    public List<CreateSalesOrderEntryRequest> Entries { get; set; } = new();
}

public class CreateSalesOrderEntryRequest
{
    public string Fmaterialid { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public decimal Fprice { get; set; }
    public decimal Ftaxprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Ftaxamount { get; set; }
    public bool Fisfree { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public string Fstockid { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
}

public class UpdateSalesOrderRequest
{
    public DateTime? Fdate { get; set; }
    public string Fcustomerid { get; set; } = string.Empty;
    public string Fsaledeptid { get; set; } = string.Empty;
    public string Fsalerid { get; set; } = string.Empty;
    public string Fsettlecurrid { get; set; } = string.Empty;
    public string Fbilltypeid { get; set; } = string.Empty;
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public string Freceiveaddress { get; set; } = string.Empty;
    public string Flinkman { get; set; } = string.Empty;
    public string Flinkphone { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; }
    public DateTime? Fpredate { get; set; }
    public string Fsalestyle { get; set; } = string.Empty;
    public List<CreateSalesOrderEntryRequest> Entries { get; set; } = new();
}
