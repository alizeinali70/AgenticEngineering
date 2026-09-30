using AgenticEngineering.Application.Ai;

namespace AgenticEngineering.Infrustructure.Ai
{
    public sealed class OllamaHealthService : IAiHealthService
    {
        private readonly HttpClient _httpClient;
        public OllamaHealthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            try
            {
                var uri = new Uri("api/tags", UriKind.Relative);

                var response = await _httpClient.GetAsync(uri, cancellationToken);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }


    }
}
