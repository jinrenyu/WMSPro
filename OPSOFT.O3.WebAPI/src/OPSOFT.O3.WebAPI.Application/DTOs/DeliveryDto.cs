namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 送货单 =====

public class DeliveryListDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdeliverydate { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fdeliverytype { get; set; } = string.Empty;
    public string Fcontactname { get; set; } = string.Empty;
    public int Fbillstatus { get; set; }
    public DateTime? CYmd { get; set; }
}

public class DeliveryDetailDto : DeliveryListDto
{
    public string Faddress { get; set; } = string.Empty;
    public string Fcontactphone { get; set; } = string.Empty;
    public DateTime? Farrivaldate { get; set; }
    public string Fexpress { get; set; } = string.Empty;
    public string Flogistics { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public string Fcheckerid { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public List<DeliveryEntryDto> Entries { get; set; } = new();
}

public class DeliveryEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fpointerid { get; set; } = string.Empty;
    public string Fitemid { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public decimal Fpoqty { get; set; }
    public decimal Fmustqty { get; set; }
    public decimal Fqty { get; set; }
    public int Fboxnum { get; set; }
    public decimal Fboxqty { get; set; }
    public string Fbatchno { get; set; } = string.Empty;
    public int Fdetailstatus { get; set; }
}

public class CreateDeliveryRequest
{
    public DateTime? Fdeliverydate { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fdeliverytype { get; set; } = string.Empty;
    public string Faddress { get; set; } = string.Empty;
    public string Fcontactname { get; set; } = string.Empty;
    public string Fcontactphone { get; set; } = string.Empty;
    public DateTime? Farrivaldate { get; set; }
    public string Fexpress { get; set; } = string.Empty;
    public string Flogistics { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateDeliveryEntryRequest> Entries { get; set; } = new();
}

public class CreateDeliveryEntryRequest
{
    public string Fpointerid { get; set; } = string.Empty;
    public string Fpodetailid { get; set; } = string.Empty;
    public int Fpoentryid { get; set; }
    public string Fitemid { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public decimal Fpoqty { get; set; }
    public decimal Fmustqty { get; set; }
    public decimal Fqty { get; set; }
    public int Fboxnum { get; set; }
    public decimal Fboxqty { get; set; }
    public string Fbatchno { get; set; } = string.Empty;
}

public class UpdateDeliveryRequest
{
    public DateTime? Fdeliverydate { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fdeliverytype { get; set; } = string.Empty;
    public string Faddress { get; set; } = string.Empty;
    public string Fcontactname { get; set; } = string.Empty;
    public string Fcontactphone { get; set; } = string.Empty;
    public DateTime? Farrivaldate { get; set; }
    public string Fexpress { get; set; } = string.Empty;
    public string Flogistics { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateDeliveryEntryRequest> Entries { get; set; } = new();
}
