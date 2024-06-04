using System.Net;
using App.Contracts.DAL;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrafficReport.ApiControllers;
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class EnumsController: ControllerBase
{
    private readonly IAppUnitOfWork _uow;


    public EnumsController(IAppUnitOfWork uow)
    {
        _uow = uow;
    }
    /// <summary>
    /// Get all violationType enums
    /// </summary>
    /// <returns>List of violationType enums</returns>
    [HttpGet("GetViolationTypeEnums")]
    [ProducesResponseType(typeof(List<App.DTO.v1_0.EViolationType>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<List<App.DTO.v1_0.EViolationType>>> GetViolationTypeEnums()
    {
        var violationTypes = (await _uow.EViolationTypeRepository.GetAllAsync()).ToList();
        return Ok(violationTypes);
    }
    /// <summary>
    /// Get all vehicleSize enums
    /// </summary>
    /// <returns>List of vehicleSize enums</returns>
    [HttpGet("GetVehicleSizeEnums")]
    [ProducesResponseType(typeof(List<App.DTO.v1_0.EVehicleSize>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<List<App.DTO.v1_0.EVehicleSize>>> GetVehicleSizeEnums()
    {
        var vehicleSizes = (await _uow.EVehicleSizeRepository.GetAllAsync())
            .ToList();
        return Ok(vehicleSizes);
    }
}