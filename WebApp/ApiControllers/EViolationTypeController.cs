using System.Net;
using App.Contracts.DAL;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrafficReport.Helpers;

namespace TrafficReport.ApiControllers;
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/violations/[controller]/[action]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class EViolationTypeController: ControllerBase
{
    private readonly IAppUnitOfWork _uow;
    private readonly PublicDTOBllMapper<App.DTO.v1_0.EViolationType, App.DAL.DTO.EViolationType> _mapper;



    public EViolationTypeController(IAppUnitOfWork uow, IMapper autoMapper)
    {
        _uow = uow;
        _mapper = new PublicDTOBllMapper<App.DTO.v1_0.EViolationType, App.DAL.DTO.EViolationType>(autoMapper);

    }
    [HttpGet]
    [ProducesResponseType(typeof(List<App.DTO.v1_0.EViolationType>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<List<App.DTO.v1_0.EViolationType>>> GetViolations()
    {
        var uowviolationTypesResult = await _uow.EViolationTypeRepository.GetAllAsync();
        var uowviolationTypes = uowviolationTypesResult.Select(e => _mapper.Map(e)).ToList();

        return Ok(uowviolationTypes);
    }
}