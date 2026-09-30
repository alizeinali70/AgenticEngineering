using AgenticEngineering.UI.DTOs;

namespace AgenticEngineering.Application.Workspace;

public interface IWorkspaceService
{
    Task<string> ReadProjectFileAsync(string relativeFilePath, CancellationToken cancellationToken);
    Task ModifyProjectFileAsync(string projectName, string relativeFilePath, string oldText, string newText, CancellationToken cancellationToken);
    Task<WorkspaceDetailsDto> GetWorkspaceDetailsAsync(CancellationToken cancellationToken);
}
