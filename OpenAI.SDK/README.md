# OpenAI.SDK

This is a C# SDK for interacting with OpenAI's GPT-3.5 API.

## Installation

Add the OpenAI.SDK package to your project.

dotnet add package OpenAI.SDK

# Usage

using OpenAI.SDK.Chat;
using OpenAI.SDK.Common;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationManager(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
var chatModule = new ChatModule(new HttpClient(), config);

var response = await chatModule.SendMessageAsync("Hello, OpenAI!");
Console.WriteLine(response);

# Configuration
The following configuration should be placed in appsettings.json:

{
  "OpenAI": {
    "ApiKey": "your-api-key",
    "BaseUrl": "https://api.openai.com/v1/",
    "Timeout": 30
  }
}

Bu yapı, kütüphanenin temel işlevselliklerini içeriyor. Artık, ChatModule'ü kullanarak OpenAI API'ye istekler gönderebilirsin ve hata yönetimi, konfigürasyon gibi önemli bileşenler de dahil edildi. Şu an her şey temel düzeyde yapılandırılmış durumda. 

