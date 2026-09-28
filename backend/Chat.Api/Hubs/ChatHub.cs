using System.Text.Json;
using Chat.Api.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace Chat.Api.Hubs;

public interface IChatClient
{
    public Task ReceiveMessage(string userName, string message);
}

public class ChatHub(IMemoryCache cache) : Hub<IChatClient>
{
    public async Task JoinGroup(UserConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

        cache.Set(Context.ConnectionId, JsonSerializer.Serialize(connection));

        await Clients
            .Group(connection.ChatRoom)
            .ReceiveMessage("Admin", $"{connection.UserName} присоединился к чату");
    }

    public async Task SendMessage(string message)
    {
        if (cache.TryGetValue<string>(Context.ConnectionId, out var stringConnection))
        {
            var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection!);

            await Clients
                .Group(connection!.ChatRoom)
                .ReceiveMessage(connection.UserName, message);
        }
    }

    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        if (cache.TryGetValue<string>(Context.ConnectionId, out var stringConnection))
        {
            Console.WriteLine("Find");
            var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection!)!;

            cache.Remove(Context.ConnectionId);

            await Groups
                .RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoom);

            await Clients
                .Group(connection!.ChatRoom)
                .ReceiveMessage("Admin", $"{connection.UserName} покинул чат");
        }

        await base.OnDisconnectedAsync(exception);
    }
}
