using Microsoft.AspNetCore.SignalR.Client;
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


HubConnection connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:55748/messages", options => options.Credentials = CredentialCache.DefaultCredentials)
    .WithAutomaticReconnect()
    .Build();

connection.On<string>(nameof(IMessageHubClient.OnJoined), id => Console.WriteLine($"{id} joined."));
connection.On<string>(nameof(IMessageHubClient.OnLeft), id => Console.WriteLine($"{id} left."));
connection.On<string, string>(nameof(IMessageHubClient.NewMessage), (id, message) => Console.WriteLine($"{id}: {message}"));

await connection.StartAsync();

string? input = Console.ReadLine();
while(input is not null && input is not "quit")
{
    await connection.SendAsync(MessageHubMethods.Message, input);
    input = Console.ReadLine();
}
