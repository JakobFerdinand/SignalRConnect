using TheServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
app.Urls.Add("http://+:5000");

app.MapGet("/", () => "Welcome to the SignalRConnect server!");
app.MapHub<MessageHub>("/messages");

app.Run();
