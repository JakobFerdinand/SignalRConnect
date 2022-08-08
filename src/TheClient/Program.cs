using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System.Net;
using TheAbstraction;

const string header = @"

 _____ _              ___ _ _            _   
/__   \ |__   ___    / __\ (_) ___ _ __ | |_ 
  / /\/ '_ \ / _ \  / /  | | |/ _ \ '_ \| __|
 / /  | | | |  __/ / /___| | |  __/ | | | |_ 
 \/   |_| |_|\___| \____/|_|_|\___|_| |_|\__|
                                             
";
Console.WriteLine(header);

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);
IConfiguration config = builder.Build();
var baseUrl = config.GetSection("ApiSettings").Get<ApiSettings>().BaseUrl!;


HubConnection connection = new HubConnectionBuilder()
    .WithUrl(new Uri(new Uri(baseUrl), "messages").AbsoluteUri, options => options.Credentials = CredentialCache.DefaultCredentials)
    .WithAutomaticReconnect()
    .Build();

connection.On<string>(nameof(IMessageHubClient.OnJoined), id => Console.WriteLine($"{id} joined."));
connection.On<string>(nameof(IMessageHubClient.OnLeft), id => Console.WriteLine($"{id} left."));
connection.On<string, string>(nameof(IMessageHubClient.NewMessage), (id, message) => Console.WriteLine($"{id}: {message}"));

await connection.StartAsync();

string? input = Console.ReadLine();
while (input is not null && input is not "quit")
{
    await connection.SendAsync(MessageHubMethods.Message, input);
    input = Console.ReadLine();
}

class ApiSettings
{
    public string? BaseUrl { get; set; }
}
