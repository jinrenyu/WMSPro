namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 预制发票 =====

public class InvoiceListDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public decimal Fsettleuntaxamount { get; set; }
    public decimal Fsettletaxamount { get; set; }
    public decimal Ftaxamount { get; set; }
    public int Fbillstatus { get; set; }
    public DateTime? CYmd { get; set; }
}

public class InvoiceDetailDto : InvoiceListDto
{
    public string Frob { get; set; } = string.Empty;
    public string Ftaxinvoice { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public string Fverifynote { get; set; } = string.Empty;
    public string Freverifynote { get; set; } = string.Empty;
    public string Frefureason { get; set; } = string.Empty;
    public List<InvoiceEntryDto> Entries { get; set; } = new();
}

public class InvoiceEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fitemid { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public string Fbatchno { get; set; } = string.Empty;
    public decimal Fprice { get; set; }
    public bool Fistax { get; set; }
    public decimal Ftax { get; set; }
    public decimal Fpretaxamt { get; set; }
    public decimal Ftaxamt { get; set; }
    public decimal Famt { get; set; }
    public string Fnote { get; set; } = string.Empty;
    public int Fentryid { get; set; }
}

public class CreateInvoiceRequest
{
    public DateTime? Fdate { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public string Frob { get; set; } = string.Empty;
    public string Ftaxinvoice { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateInvoiceEntryRequest> Entries { get; set; } = new();
}

public class CreateInvoiceEntryRequest
{
    public string Finstockinterid { get; set; } = string.Empty;
    public string Finstockdetailid { get; set; } = string.Empty;
    public string Fpointerid { get; set; } = string.Empty;
    public string Fpodetailid { get; set; } = string.Empty;
    public string Fdlinterid { get; set; } = string.Empty;
    public string Fdldetailid { get; set; } = string.Empty;
    public string Fitemid { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public string Fbatchno { get; set; } = string.Empty;
    public decimal Fprice { get; set; }
    public bool Fistax { get; set; }
    public decimal Ftax { get; set; }
    public decimal Fpretaxamt { get; set; }
    public decimal Ftaxamt { get; set; }
    public decimal Famt { get; set; }
    public string Fnote { get; set; } = string.Empty;
}

public class UpdateInvoiceRequest
{
    public DateTime? Fdate { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public string Frob { get; set; } = string.Empty;
    public string Ftaxinvoice { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateInvoiceEntryRequest> Entries { get; set; } = new();
}
