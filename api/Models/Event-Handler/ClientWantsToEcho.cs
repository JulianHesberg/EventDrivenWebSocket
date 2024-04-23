using api.Models.Dto;
using Fleck;
using lib;

namespace api.Models.Event_Handler;

public class ClientWantsToEcho : BaseEventHandler<Dto.ClientWantsToEcho>
{
    public override Task Handle(Dto.ClientWantsToEcho dto, IWebSocketConnection socket)
    {
        socket.Send(dto.message);
        return Task.CompletedTask;
    }
}