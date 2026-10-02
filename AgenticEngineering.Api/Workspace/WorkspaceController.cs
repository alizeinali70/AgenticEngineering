using AgenticEngineering.Application.Workspace;
using AgenticEngineering.UI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AgenticEngineering.Api.Workspace
{
    [Route("api/workspace")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceService;
        public WorkspaceController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        [HttpGet("read-project-file")]
        public async Task<IActionResult> GetFileContent([FromQuery] string path, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _workspaceService.ReadProjectFileAsync(path, cancellationToken);
                return Ok(content);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("details")]
        public async Task<ActionResult<WorkspaceDetailsDto>> GetWorkspaceDetails()
        {
            var workspaceDetails = await _workspaceService.GetWorkspaceDetailsAsync(CancellationToken.None);
            return Ok(workspaceDetails);
        }

        [HttpPost("modify-file")]
        public async Task<IActionResult> ModifyFile([FromBody] ModifyFileRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                await _workspaceService.ModifyProjectFileAsync(request.ProjectName, request.FilePath, request.OldText, request.NewText, cancellationToken);
                return Ok(new { Success = true, Message = $"File '{request.FilePath}' updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Error = ex.Message });
            }
        }
    }
}
