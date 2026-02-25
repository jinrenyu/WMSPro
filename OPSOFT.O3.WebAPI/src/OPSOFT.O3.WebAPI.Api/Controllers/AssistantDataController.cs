using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 辅助资料类别
/// </summary>
[Route("api/[controller]")]
public class AssistantDataController : ApprovableDisableableMasterDataController<TBasAssistantdata, AssistantDataListDto, AssistantDataDetailDto, CreateAssistantDataRequest, UpdateAssistantDataRequest>
{
    public AssistantDataController(IApprovableDisableableService<TBasAssistantdata, AssistantDataListDto, AssistantDataDetailDto, CreateAssistantDataRequest, UpdateAssistantDataRequest> service)
        : base(service) { }
}

/// <summary>
/// 辅助资料明细
/// </summary>
[Route("api/[controller]")]
public class AssistantDataEntryController : ApprovableDisableableMasterDataController<TBasAssistantdataentry, AssistantDataEntryListDto, AssistantDataEntryDetailDto, CreateAssistantDataEntryRequest, UpdateAssistantDataEntryRequest>
{
    public AssistantDataEntryController(IApprovableDisableableService<TBasAssistantdataentry, AssistantDataEntryListDto, AssistantDataEntryDetailDto, CreateAssistantDataEntryRequest, UpdateAssistantDataEntryRequest> service)
        : base(service) { }
}
