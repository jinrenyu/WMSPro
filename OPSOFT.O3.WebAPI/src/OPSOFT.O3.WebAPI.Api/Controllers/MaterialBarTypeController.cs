using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[Route("api/[controller]")]
public class MaterialBarTypeController : ApprovableDisableableMasterDataController<TBdMaterialbartype, MaterialBarTypeListDto, MaterialBarTypeDetailDto, CreateMaterialBarTypeRequest, UpdateMaterialBarTypeRequest>
{
    public MaterialBarTypeController(IApprovableDisableableService<TBdMaterialbartype, MaterialBarTypeListDto, MaterialBarTypeDetailDto, CreateMaterialBarTypeRequest, UpdateMaterialBarTypeRequest> service)
        : base(service) { }
}
