using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[Route("api/[controller]")]
public class CurrencyController : ApprovableDisableableMasterDataController<TBdCurrency, CurrencyListDto, CurrencyDetailDto, CreateCurrencyRequest, UpdateCurrencyRequest>
{
    public CurrencyController(IApprovableDisableableService<TBdCurrency, CurrencyListDto, CurrencyDetailDto, CreateCurrencyRequest, UpdateCurrencyRequest> service)
        : base(service) { }
}
