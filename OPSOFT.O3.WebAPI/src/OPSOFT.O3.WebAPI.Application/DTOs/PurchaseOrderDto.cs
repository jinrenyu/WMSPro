using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 采购订单 =====

public class PurchaseOrderListDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fdeptid { get; set; } = string.Empty;
    public string Fempid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public int Fbillstatus { get; set; }
    public bool Fisurgent { get; set; }
    public DateTime? CYmd { get; set; }
}

public class PurchaseOrderDetailDto : PurchaseOrderListDto
{
    public string Fmatgroupid { get; set; } = string.Empty;
    public string Fphone { get; set; } = string.Empty;
    public string Fdeliverymethod { get; set; } = string.Empty;
    public string Fdeliverysite { get; set; } = string.Empty;
    public int Fsourcetrantype { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public string Fcheckerid { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string Frefureason { get; set; } = string.Empty;
    public List<PurchaseOrderEntryDto> Entries { get; set; } = new();
}

public class PurchaseOrderEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fitemid { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public DateTime? Fdemanddate { get; set; }
    public decimal Fprice { get; set; }
    public decimal Frreight { get; set; }
    public bool Fistax { get; set; }
    public decimal Fpretaxamt { get; set; }
    public decimal Ftaxamt { get; set; }
    public decimal Famt { get; set; }
    public decimal Ftax { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public string Fchecktype { get; set; } = string.Empty;
    public int Fdetailstatus { get; set; }
    public int Fentryid { get; set; }
}

public class CreatePurchaseOrderRequest
{
    public DateTime? Fdate { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fdeptid { get; set; } = string.Empty;
    public string Fempid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public string Fmatgroupid { get; set; } = string.Empty;
    public string Fphone { get; set; } = string.Empty;
    public string Fdeliverymethod { get; set; } = string.Empty;
    public string Fdeliverysite { get; set; } = string.Empty;
    public bool Fisurgent { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public List<CreatePurchaseOrderEntryRequest> Entries { get; set; } = new();
}

public class CreatePurchaseOrderEntryRequest
{
    public string Fitemid { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public DateTime? Fdemanddate { get; set; }
    public decimal Fprice { get; set; }
    public decimal Frreight { get; set; }
    public bool Fistax { get; set; }
    public decimal Fpretaxamt { get; set; }
    public decimal Ftaxamt { get; set; }
    public decimal Famt { get; set; }
    public decimal Ftax { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public string Fchecktype { get; set; } = string.Empty;
}

public class UpdatePurchaseOrderRequest
{
    public DateTime? Fdate { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fdeptid { get; set; } = string.Empty;
    public string Fempid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public string Fmatgroupid { get; set; } = string.Empty;
    public string Fphone { get; set; } = string.Empty;
    public string Fdeliverymethod { get; set; } = string.Empty;
    public string Fdeliverysite { get; set; } = string.Empty;
    public bool Fisurgent { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public List<CreatePurchaseOrderEntryRequest> Entries { get; set; } = new();
}
