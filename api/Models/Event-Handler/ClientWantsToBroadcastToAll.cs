using api.Models.Dto;
using api.State;
using Fleck;
using lib;

namespace api.Models.Event_Handler;

public class ClientWantsToBroadcastToAll(ClientConnections clientConnections) : BaseEventHandler<ClientWantsToBroadcastToAllDto>
{
    public override Task Handle(ClientWantsToBroadcastToAllDto dto, IWebSocketConnection socket)
    {
        foreach (var conn in clientConnections.GetClientConnections())
        {
            conn.Send(dto.message);
        }
        return Task.CompletedTask;
    }
}