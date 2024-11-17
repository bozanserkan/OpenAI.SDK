using System;

namespace OpenAI.SDK.Authentication
{
    public interface IAuthManager
    {
        string GetAuthorizationHeader();
    }

    public class AuthManager : IAuthManager
    {
        private readonly string _apiKey;

        public AuthManager(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("API key cannot be null or empty.");

            _apiKey = apiKey;
        }

        public string GetAuthorizationHeader()
        {
            return $"Bearer {_apiKey}";
        }
    }
}