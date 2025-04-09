using Xunit;
using Moq;
using OpenAI.SDK.Chat;
using OpenAI.SDK.Common;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OpenAI.SDK.Tests
{
    public class ChatModuleTests
    {
        private readonly ChatModule _chatModule;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<HttpClient> _mockHttpClient;

        public ChatModuleTests()
        {
            // Mock Configuration
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(c => c["OpenAI:ApiKey"]).Returns("your-api-key");
            _mockConfig.Setup(c => c["OpenAI:BaseUrl"]).Returns("https://api.openai.com/v1/");

            // Mock HttpClient
            _mockHttpClient = new Mock<HttpClient>();

            var configManager = new ConfigurationManager(_mockConfig.Object);
            _chatModule = new ChatModule(_mockHttpClient.Object, configManager);
        }

        [Fact]
        public async Task SendMessageAsync_ShouldReturnValidResponse()
        {
            // Arrange
            var expectedResponse = "{\"choices\":[{\"message\":\"response\"}]}";
            _mockHttpClient.Setup(c => c.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                           .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                           {
                               Content = new StringContent(expectedResponse)
                           });

            // Act
            var response = await _chatModule.SendMessageAsync("Hello, OpenAI!");

            // Assert
            Assert.NotNull(response);
            Assert.Contains("choices", response);
            Assert.Contains("response", response);
        }

        [Fact]
        public async Task SendMessageAsync_ShouldThrowApiException_WhenErrorOccurs()
        {
            // Arrange
            var errorResponse = "{\"error\":\"invalid API key\"}";
            _mockHttpClient.Setup(c => c.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                           .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                           {
                               Content = new StringContent(errorResponse)
                           });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => _chatModule.SendMessageAsync("Invalid request"));
            Assert.Equal(401, exception.StatusCode);
            Assert.Contains("invalid API key", exception.ResponseBody);
        }
    }
}
