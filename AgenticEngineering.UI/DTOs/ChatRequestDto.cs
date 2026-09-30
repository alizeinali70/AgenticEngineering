using AgenticEngineering.Domain.Ai;

namespace AgenticEngineering.UI.DTOs;

public record ChatRequestDto(List<ChatMessageModel> Messages);
