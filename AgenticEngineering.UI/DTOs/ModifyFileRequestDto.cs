namespace AgenticEngineering.UI.DTOs
{
    public record ModifyFileRequestDto(string ProjectName, string FilePath, string OldText, string NewText);
}
