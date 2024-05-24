using System.Net;
using App.Contracts.DAL;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrafficReport.ApiControllers;
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/violations/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class EViolationTypeController: ControllerBase
{
    private readonly IAppUnitOfWork _uow;


    public EViolationTypeController(IAppUnitOfWork uow)
    {
        _uow = uow;
    }
    [HttpGet]
    [ProducesResponseType(typeof(List<App.DTO.v1_0.EViolationType>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<List<App.DTO.v1_0.EViolationType>>> GetViolationTypeEnums()
    {
        var violationTypes = await _uow.EViolationTypeRepository.GetAllAsync();
        return Ok(violationTypes);
    }
}