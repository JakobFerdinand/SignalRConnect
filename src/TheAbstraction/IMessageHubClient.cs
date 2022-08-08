namespace TheAbstraction;

public interface IMessageHubClient
{
    Task OnJoined(string id);
    Task OnLeft(string id);
    Task NewMessage(string id, string message);
}