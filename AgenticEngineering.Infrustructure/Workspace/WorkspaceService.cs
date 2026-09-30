using AgenticEngineering.Application.Workspace;
using AgenticEngineering.Domain.Workspace;
using AgenticEngineering.UI.DTOs;
using Microsoft.Extensions.Options;

namespace AgenticEngineering.Infrustructure.Workspace
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly string _workspaceProjectName;
        private readonly string _workspaceDirectory;
        public WorkspaceService(IOptions<WorkspaceOptions> options)
        {
            _workspaceProjectName = options.Value.ProjectName;
            _workspaceDirectory = options.Value.ProjectPath;
        }
        public Task ModifyProjectFileAsync(string projectName, string relativeFilePath, string oldText, string newText, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ReadProjectFileAsync(string relativeFilePath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(relativeFilePath))
            {
                throw new ArgumentException("File path is required.", nameof(relativeFilePath));
            }

            // Combine workspace root directory with the relative path
            var fullPath = Path.Combine(_workspaceDirectory, relativeFilePath);

            // Security check: Ensure the path doesn't escape the workspace folder (directory traversal protection)
            var fullWorkspacePath = Path.GetFullPath(_workspaceDirectory);
            var fullFilePath = Path.GetFullPath(fullPath);
            if (!fullFilePath.StartsWith(fullWorkspacePath, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Access outside the workspace is not permitted.");
            }

            if (!File.Exists(fullFilePath))
            {
                throw new FileNotFoundException($"File not found: {relativeFilePath}");
            }

            // Read and return the file content as a string
            return await File.ReadAllTextAsync(fullFilePath, cancellationToken);
        }

        public async Task<WorkspaceDetailsDto> GetWorkspaceDetailsAsync(CancellationToken cancellationToken)
        {
            var projectName = _workspaceProjectName;
            var projectPath = _workspaceDirectory;

            var dto = new WorkspaceDetailsDto
            {
                ProjectName = projectName
            };

            if (!string.IsNullOrEmpty(projectPath) && Directory.Exists(projectPath))
            {
                // 1. Folders to completely skip
                string[] ignoredDirectories = { "bin", "obj", ".git", ".vs", "node_modules", "publish" };

                // 2. File extensions you actually want to work with
                string[] allowedExtensions = { ".cs", ".razor", ".cshtml", ".html", ".css", ".js", ".ts", ".json", ".sln", ".md" };

                var files = Directory.GetFiles(projectPath, "*.*", SearchOption.AllDirectories)
                    .Where(f =>
                    {
                        // Exclude if path contains any ignored directory name
                        var pathParts = f.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                        if (pathParts.Any(part => ignoredDirectories.Contains(part, StringComparer.OrdinalIgnoreCase)))
                        {
                            return false;
                        }

                        // Keep only files with allowed developer extensions
                        var extension = Path.GetExtension(f);
                        return allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
                    });

                // Use AddRange to populate the existing read-only list collection
                dto.Files.AddRange(files.Select(f => Path.GetRelativePath(projectPath, f)));
            }
            else
            {
                dto.Files.Add("Project path not found!");
            }

            return dto;
        }
    }
}
