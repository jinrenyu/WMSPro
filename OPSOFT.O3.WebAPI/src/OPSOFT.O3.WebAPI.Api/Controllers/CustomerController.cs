using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[Route("api/[controller]")]
public class CustomerController : ApprovableDisableableMasterDataController<TBdCustomer, CustomerListDto, CustomerDetailDto, CreateCustomerRequest, UpdateCustomerRequest>
{
    public CustomerController(IApprovableDisableableService<TBdCustomer, CustomerListDto, CustomerDetailDto, CreateCustomerRequest, UpdateCustomerRequest> service)
        : base(service) { }
}
