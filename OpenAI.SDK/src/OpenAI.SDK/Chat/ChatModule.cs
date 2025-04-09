using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OpenAI.SDK.Common;
using System.Text.Json;
using System;

namespace OpenAI.SDK.Chat
{
    public class ChatModule
    {
        private readonly HttpClient _httpClient;
        private readonly ConfigurationManager _configManager;

        public ChatModule(HttpClient httpClient, ConfigurationManager configManager)
        {
            _httpClient = httpClient;
            _configManager = configManager;
            _httpClient.Timeout = TimeSpan.FromSeconds(_configManager.GetTimeout());
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] { new { role = "user", content = message } }
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configManager.GetApiKey()}");

            var response = await _httpClient.PostAsync(_configManager.GetBaseUrl() + "chat/completions", content);

            // Hata yönetimi
            ErrorHandling.HandleApiError(response);

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
