using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

namespace TrafficReport.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<string>>> Get()
    {
        return Ok(new List<string>(){"aa","bb","cc"});
    }
}