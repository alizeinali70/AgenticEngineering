using AgenticEngineering.Application.Ai;
using AgenticEngineering.UI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AgenticEngineering.Api.Ai
{
    [Route("api/ollamatask")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class OllamaTaskController : ControllerBase
    {
        private readonly IOllamaTaskService _ollamaTaskService;

        public OllamaTaskController(IOllamaTaskService ollamaTaskService)
        {
            _ollamaTaskService = ollamaTaskService;
        }

        [HttpPost("chat")]
        public async Task<ActionResult<ChatResponseDto>> SendChat([FromBody] ChatRequestDto request, CancellationToken cancellationToken)
        {
            if (request?.Messages == null || request.Messages.Count == 0)
            {
                return BadRequest("Messages cannot be empty.");
            }

            string aiResult = await _ollamaTaskService.SendChatHistoryAsync(request.Messages, cancellationToken);

            return Ok(new ChatResponseDto(aiResult));
        }
    }
}
