using api.Models.Dto;
using api.Models.Services;
using Fleck;
using lib;

namespace api.Models.Event_Handler;

public class ClientWantsToEnterRoom : BaseEventHandler<ClientWantsToEnterRoomDto>
{
    public override Task Handle(ClientWantsToEnterRoomDto dto, IWebSocketConnection socket)
    {
        StateService.AddToRoom(socket, dto.RoomId);
        return Task.CompletedTask;
    }
}