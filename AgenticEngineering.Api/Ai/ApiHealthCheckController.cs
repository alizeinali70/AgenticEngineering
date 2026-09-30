using AgenticEngineering.Application.Ai;
using Microsoft.AspNetCore.Mvc;

namespace AgenticEngineering.Api.Ai;

[Route("api/health")]
[ApiController]
public class ApiHealthCheckController : ControllerBase
{
    private readonly IAiHealthService _aiHealthService;

    public ApiHealthCheckController(IAiHealthService aiHealthService)
    {
        _aiHealthService = aiHealthService;
    }

    [HttpGet]
    public async Task<ActionResult<bool>> GetHealthCheck(CancellationToken cancellationToken)
    {
        bool ollamaAvailable = await _aiHealthService.IsAvailableAsync(cancellationToken);
        return Ok(ollamaAvailable);
    }
}
