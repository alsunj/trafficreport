using System.Net;

namespace TrafficReport.DTO;

public class RestApiErrorResponse
{
    public HttpStatusCode Status { get; set; }
    public string Error { get; set; } = default!;
}

