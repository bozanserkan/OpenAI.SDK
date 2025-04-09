using Microsoft.Extensions.Configuration;

namespace OpenAI.SDK.Common
{
    public class ConfigurationManager
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetApiKey() => _configuration["OpenAI:ApiKey"];
        public string GetBaseUrl() => _configuration["OpenAI:BaseUrl"];
        public int GetTimeout() => int.Parse(_configuration["OpenAI:Timeout"]);
        public string GetProxyAddress() => _configuration["OpenAI:Proxy:Address"];
        public int GetProxyPort() => int.Parse(_configuration["OpenAI:Proxy:Port"]);
    }
}
