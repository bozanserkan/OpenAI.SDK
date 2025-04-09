using System;
using System.Net.Http;

namespace OpenAI.SDK.Common
{
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public string ResponseBody { get; }

        public ApiException(string message, int statusCode, string responseBody)
            : base(message)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }
    }

    public class ErrorHandling
    {
        public static void HandleApiError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = response.Content.ReadAsStringAsync().Result;
                throw new ApiException($"API request failed with status code {response.StatusCode}.", (int)response.StatusCode, errorResponse);
            }
        }
    }
}
