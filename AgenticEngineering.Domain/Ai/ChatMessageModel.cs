namespace AgenticEngineering.Domain.Ai
{
    public class ChatMessageModel
    {
        public string Role { get; set; } = "user";
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
