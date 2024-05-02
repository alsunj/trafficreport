using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TrafficReport.ApiControllers;

[ApiController]
[Route("/api/v{version:ApiVersion}")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<string>>> Get()
    {
        return Ok(new List<string>(){"aa","bb","cc"});
    }
}