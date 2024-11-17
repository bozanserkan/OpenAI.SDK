using OpenAI.SDK;
using OpenAI.SDK.Chat;
using OpenAI.SDK.Authentication;

var authManager = new AuthManager("YOUR_API_KEY");
var chatModule = new ChatModule(authManager);

var response = await chatModule.SendMessageAsync("gpt-4", "Hello, OpenAI!");
Console.WriteLine(response);
