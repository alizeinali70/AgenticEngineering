using AgenticEngineering.Application.Ai;
using AgenticEngineering.Application.Config;
using AgenticEngineering.Domain.Ai;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AgenticEngineering.Infrustructure.Ai
{
    public class OllamaTaskService : IOllamaTaskService
    {
        private readonly HttpClient _httpClient;
        private readonly AiSettings _aiSettings;

        public OllamaTaskService(HttpClient httpClient, IOptions<AiSettings> aiSettingsOptions)
        {
            _httpClient = httpClient;
            _aiSettings = aiSettingsOptions.Value;
        }

        public async Task<string> SendChatHistoryAsync(List<ChatMessageModel> messages, CancellationToken cancellationToken)
        {
            var uri = new Uri("api/chat", UriKind.Relative);

            var requestPayload = new
            {
                model = _aiSettings.ModelId,
                messages = messages.Select(m => new { role = m.Role, content = m.Content }),
                stream = false
            };

            var response = await _httpClient.PostAsJsonAsync(uri, requestPayload, cancellationToken);
            response.EnsureSuccessStatusCode();

            var ollamaResponse = await response.Content.ReadFromJsonAsync<OllamaChatApiResponse>(cancellationToken: cancellationToken);

            return ollamaResponse?.Message?.Content ?? "No response generated.";
        }
    }
}
