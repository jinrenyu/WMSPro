using FluentValidation;
using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Validators;

// ===== 采购订单 =====

public class CreatePurchaseOrderRequestValidator : AbstractValidator<CreatePurchaseOrderRequest>
{
    public CreatePurchaseOrderRequestValidator()
    {
        RuleFor(x => x.Fsupplyid).NotEmpty().WithMessage("供应商不能为空");
        RuleFor(x => x.Fcurrencyid).NotEmpty().WithMessage("币别不能为空");
        RuleFor(x => x.Fnote).MaximumLength(500);
        RuleFor(x => x.Fphone).MaximumLength(50);
        RuleFor(x => x.Fdeliverysite).MaximumLength(500);
        RuleFor(x => x.Entries).NotEmpty().WithMessage("明细不能为空");
        RuleForEach(x => x.Entries).ChildRules(entry =>
        {
            entry.RuleFor(e => e.Fitemid).NotEmpty().WithMessage("物料不能为空");
            entry.RuleFor(e => e.Funitid).NotEmpty().WithMessage("单位不能为空");
            entry.RuleFor(e => e.Fqty).GreaterThan(0).WithMessage("数量必须大于0");
            entry.RuleFor(e => e.Fprice).GreaterThanOrEqualTo(0);
            entry.RuleFor(e => e.Fnote).MaximumLength(500);
        });
    }
}

public class UpdatePurchaseOrderRequestValidator : AbstractValidator<UpdatePurchaseOrderRequest>
{
    public UpdatePurchaseOrderRequestValidator()
    {
        RuleFor(x => x.Fsupplyid).NotEmpty().WithMessage("供应商不能为空");
        RuleFor(x => x.Fcurrencyid).NotEmpty().WithMessage("币别不能为空");
        RuleFor(x => x.Fnote).MaximumLength(500);
        RuleFor(x => x.Fphone).MaximumLength(50);
        RuleFor(x => x.Fdeliverysite).MaximumLength(500);
        RuleFor(x => x.Entries).NotEmpty().WithMessage("明细不能为空");
        RuleForEach(x => x.Entries).ChildRules(entry =>
        {
            entry.RuleFor(e => e.Fitemid).NotEmpty().WithMessage("物料不能为空");
            entry.RuleFor(e => e.Funitid).NotEmpty().WithMessage("单位不能为空");
            entry.RuleFor(e => e.Fqty).GreaterThan(0).WithMessage("数量必须大于0");
            entry.RuleFor(e => e.Fprice).GreaterThanOrEqualTo(0);
            entry.RuleFor(e => e.Fnote).MaximumLength(500);
        });
    }
}

// ===== 销售订单 =====

public class CreateSalesOrderRequestValidator : AbstractValidator<CreateSalesOrderRequest>
{
    public CreateSalesOrderRequestValidator()
    {
        RuleFor(x => x.Fcustomerid).NotEmpty().WithMessage("客户不能为空");
        RuleFor(x => x.Fsettlecurrid).NotEmpty().WithMessage("结算币别不能为空");
        RuleFor(x => x.Fnote).MaximumLength(500);
        RuleFor(x => x.Freceiveaddress).MaximumLength(500);
        RuleFor(x => x.Flinkman).MaximumLength(100);
        RuleFor(x => x.Flinkphone).MaximumLength(50);
        RuleFor(x => x.Fexchangerate).GreaterThan(0).WithMessage("汇率必须大于0");
        RuleFor(x => x.Entries).NotEmpty().WithMessage("明细不能为空");
        RuleForEach(x => x.Entries).ChildRules(entry =>
        {
            entry.RuleFor(e => e.Fmaterialid).NotEmpty().WithMessage("物料不能为空");
            entry.RuleFor(e => e.Funitid).NotEmpty().WithMessage("单位不能为空");
            entry.RuleFor(e => e.Fqty).GreaterThan(0).WithMessage("数量必须大于0");
            entry.RuleFor(e => e.Fprice).GreaterThanOrEqualTo(0);
            entry.RuleFor(e => e.Fnote).MaximumLength(500);
        });
    }
}

public class UpdateSalesOrderRequestValidator : AbstractValidator<UpdateSalesOrderRequest>
{
    public UpdateSalesOrderRequestValidator()
    {
        RuleFor(x => x.Fcustomerid).NotEmpty().WithMessage("客户不能为空");
        RuleFor(x => x.Fsettlecurrid).NotEmpty().WithMessage("结算币别不能为空");
        RuleFor(x => x.Fnote).MaximumLength(500);
        RuleFor(x => x.Freceiveaddress).MaximumLength(500);
        RuleFor(x => x.Flinkman).MaximumLength(100);
        RuleFor(x => x.Flinkphone).MaximumLength(50);
        RuleFor(x => x.Fexchangerate).GreaterThan(0).WithMessage("汇率必须大于0");
        RuleFor(x => x.Entries).NotEmpty().WithMessage("明细不能为空");
        RuleForEach(x => x.Entries).ChildRules(entry =>
        {
            entry.RuleFor(e => e.Fmaterialid).NotEmpty().WithMessage("物料不能为空");
            entry.RuleFor(e => e.Funitid).NotEmpty().WithMessage("单位不能为空");
            entry.RuleFor(e => e.Fqty).GreaterThan(0).WithMessage("数量必须大于0");
            entry.RuleFor(e => e.Fprice).GreaterThanOrEqualTo(0);
            entry.RuleFor(e => e.Fnote).MaximumLength(500);
        });
    }
}

// ===== 送货单 =====

public class CreateDeliveryRequestValidator : AbstractValidator<CreateDeliveryRequest>
{
    public CreateDeliveryRequestValidator()
    {
        RuleFor(x => x.Fsupplyid).NotEmpty().WithMessage("供应商不能为空");
        RuleFor(x => x.Faddress).MaximumLength(500);
        RuleFor(x => x.Fcontactname).MaximumLength(100);
        RuleFor(x => x.Fcontactphone).MaximumLength(50);
        RuleFor(x => x.Fnote).MaximumLength(500);
        RuleFor(x => x.Entries).NotEmpty().WithMessage("明细不能为空");
        RuleForEach(x => x.Entries).ChildRules(entry =>
        {
            entry.RuleFor(e => e.Fitemid).NotEmpty().WithMessage("物料不能为空");
            entry.RuleFor(e => e.Funitid).NotEmpty().WithMessage("单位不能为空");
            entry.RuleFor(e => e.Fqty).GreaterThan(0).WithMessage("数量必须大于0");
            entry.RuleFor(e => e.Fboxnum).GreaterThanOrEqualTo(0);
            entry.RuleFor(e => e.Fboxqty).GreaterThanOrEqualTo(0);
        });
    }
}

public class UpdateDeliveryRequestValidator : AbstractValidator<UpdateDeliveryRequest>
{
    public UpdateDeliveryRequestValidator()
    {
        RuleFor(x => x.Fsupplyid).NotEmpty().WithMessage("供应商不能为空");
        RuleFor(x => x.Faddress).MaximumLength(500);
        RuleFor(x => x.Fcontactname).MaximumLength(100);
        RuleFor(x => x.Fcontactphone).MaximumLength(50);
        RuleFor(x => x.Fnote).MaximumLength(500);
        RuleFor(x => x.Entries).NotEmpty().WithMessage("明细不能为空");
        RuleForEach(x => x.Entries).ChildRules(entry =>
        {
            entry.RuleFor(e => e.Fitemid).NotEmpty().WithMessage("物料不能为空");
            entry.RuleFor(e => e.Funitid).NotEmpty().WithMessage("单位不能为空");
            entry.RuleFor(e => e.Fqty).GreaterThan(0).WithMessage("数量必须大于0");
            entry.RuleFor(e => e.Fboxnum).GreaterThanOrEqualTo(0);
            entry.RuleFor(e => e.Fboxqty).GreaterThanOrEqualTo(0);
        });
    }
}

// ===== 预制发票 =====

public class CreateInvoiceRequestValidator : AbstractValidator<CreateInvoiceRequest>
{
    public CreateInvoiceRequestValidator()
    {
        RuleFor(x => x.Fsupplyid).NotEmpty().WithMessage("供应商不能为空");
        RuleFor(x => x.Fcurrencyid).NotEmpty().WithMessage("币别不能为空");
        RuleFor(x => x.Fnote).MaximumLength(500);
        RuleFor(x => x.Entries).NotEmpty().WithMessage("明细不能为空");
        RuleForEach(x => x.Entries).ChildRules(entry =>
        {
            entry.RuleFor(e => e.Fitemid).NotEmpty().WithMessage("物料不能为空");
            entry.RuleFor(e => e.Funitid).NotEmpty().WithMessage("单位不能为空");
            entry.RuleFor(e => e.Fqty).GreaterThan(0).WithMessage("数量必须大于0");
            entry.RuleFor(e => e.Fprice).GreaterThanOrEqualTo(0);
            entry.RuleFor(e => e.Fnote).MaximumLength(500);
        });
    }
}

public class UpdateInvoiceRequestValidator : AbstractValidator<UpdateInvoiceRequest>
{
    public UpdateInvoiceRequestValidator()
    {
        RuleFor(x => x.Fsupplyid).NotEmpty().WithMessage("供应商不能为空");
        RuleFor(x => x.Fcurrencyid).NotEmpty().WithMessage("币别不能为空");
        RuleFor(x => x.Fnote).MaximumLength(500);
        RuleFor(x => x.Entries).NotEmpty().WithMessage("明细不能为空");
        RuleForEach(x => x.Entries).ChildRules(entry =>
        {
            entry.RuleFor(e => e.Fitemid).NotEmpty().WithMessage("物料不能为空");
            entry.RuleFor(e => e.Funitid).NotEmpty().WithMessage("单位不能为空");
            entry.RuleFor(e => e.Fqty).GreaterThan(0).WithMessage("数量必须大于0");
            entry.RuleFor(e => e.Fprice).GreaterThanOrEqualTo(0);
            entry.RuleFor(e => e.Fnote).MaximumLength(500);
        });
    }
}
