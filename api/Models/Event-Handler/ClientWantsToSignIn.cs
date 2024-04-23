using api.Models.Dto;
using api.Models.Services;
using Fleck;
using lib;

namespace api.Models.Event_Handler;

public class ClientWantsToSignIn : BaseEventHandler<ClientWantsToSignInDto>
{
    public override Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
    {
        StateService.Connections[socket.ConnectionInfo.Id].Username = dto.Username;
        return Task.CompletedTask;
    }
}