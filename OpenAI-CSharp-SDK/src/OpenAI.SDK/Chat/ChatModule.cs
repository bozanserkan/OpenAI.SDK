using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OpenAI.SDK.Authentication;

namespace OpenAI.SDK.Chat
{
    public class ChatModule
    {
        private readonly IAuthManager _authManager;
        private readonly HttpClient _httpClient;

        public ChatModule(IAuthManager authManager)
        {
            _authManager = authManager;
            _httpClient = new HttpClient();
        }

        public async Task<string> SendMessageAsync(string model, string prompt)
        {
            var requestData = new
            {
                model = model,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            string json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", _authManager.GetAuthorizationHeader());

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
