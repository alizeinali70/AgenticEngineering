namespace AgenticEngineering.UI.DTOs
{
    public class WorkspaceDetailsDto
    {
        public string ProjectName { get; set; } = string.Empty;

        public List<string> Files { get; init; } = new();
    }
}
