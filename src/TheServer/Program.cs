using Microsoft.Extensions.Hosting.WindowsServices;
using TheServer;

WebApplicationOptions options = new()
{
    Args = Array.Empty<string>(),
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
};

var builder = WebApplication.CreateBuilder(options);
builder.Services.AddSignalR();

var app = builder.Build();
app.Urls.Add("http://+:5000");

app.MapGet("/", () => "Welcome to the SignalRConnect server!");
app.MapHub<MessageHub>("/messages");

app.Run();
