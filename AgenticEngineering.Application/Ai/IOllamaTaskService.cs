using AgenticEngineering.Domain.Ai;

namespace AgenticEngineering.Application.Ai;

public interface IOllamaTaskService
{
    Task<string> SendChatHistoryAsync(List<ChatMessageModel> messages, CancellationToken cancellationToken);
}
