using Microsoft.AspNetCore.SignalR;
using TheAbstraction;

namespace TheServer;

public sealed class MessageHub : Hub<IMessageHubClient>
{
}
