using Microsoft.AspNetCore.SignalR;
using TheAbstraction;

namespace TheServer;

public sealed class MessageHub : Hub<IMessageHubClient>
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        await Clients.All.OnJoined(Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
        await Clients.All.OnLeft(Context.ConnectionId);
    }

    [HubMethodName(MessageHubMethods.Message)]
    public async Task Message(string message)
        => await Clients.AllExcept(Context.ConnectionId).NewMessage(Context.ConnectionId, message);
}
