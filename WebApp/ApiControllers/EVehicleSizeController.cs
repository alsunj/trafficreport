using System.Net;
using App.Contracts.DAL;
using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TrafficReport.ApiControllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/vehicles/[controller]/[action]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class EVehicleSizeController: ControllerBase
{
    private readonly IAppUnitOfWork _uow;

    public EVehicleSizeController(IAppUnitOfWork uow)
    {
        _uow = uow;
    }
    [HttpGet]
    [ProducesResponseType(typeof(List<App.DTO.v1_0.EVehicleSize>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<List<App.DTO.v1_0.EVehicleSize>>> GetVehicleSizeEnums()
    {
        var vehicleSizes = await _uow.EVehicleSizeRepository.GetAllAsync();
        return Ok(vehicleSizes);
    }
}