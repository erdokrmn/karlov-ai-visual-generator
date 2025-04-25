using System.Text.Json;
using System.Text;

namespace SendingPrompt.Services
{
    public class PromptService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://5c58-34-126-140-25.ngrok-free.app/generate";
        // Colab'daki IP ve port

        public PromptService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> GenerateImageAsync(string prompt)
        {
            var requestData = new { prompt };
            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
