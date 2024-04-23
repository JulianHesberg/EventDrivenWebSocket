using System.Text.Json;
using api.Models.Dto;
using api.Models.Event_Handler;
using lib;
using NUnit.Framework;

namespace Test;

[TestFixture]
public class WebSocketTests
{
    [Test]
    public async Task MyTest()
    {
        
        var ws1 = await new WebSocketTestClient("ws://localhost:8181").ConnectAsync();

        var ws2 = await new WebSocketTestClient("ws://localhost:8181").ConnectAsync();

        await ws1.DoAndAssert(new ClientWantsToSignInDto() { Username = "Billy Joel" });

        await ws2.DoAndAssert(new ClientWantsToSignInDto() { Username = "John Adams" });
        Task.Delay(2000).Wait();

        await ws2.DoAndAssert(new ClientWantsToEnterRoomDto() { RoomId = 1 });

        await ws1.DoAndAssert(new ClientWantsToEnterRoomDto() { RoomId = 1 });
        Task.Delay(2000).Wait();

        await ws1.DoAndAssert(new ClientWantsToBroadcastToRoomDto() { Message = "Hello", RoomId = 1 });
        Task.Delay(2000).Wait();

        Console.WriteLine(JsonSerializer.Serialize(ws2.ReceivedMessages));
        await ws1.DoAndAssert(new ClientWantsToBroadcastToRoomDto() { Message = "Hello", RoomId = 1 },
            r => r.Count(dto => dto.eventType.Equals("ServerBroadcastsMessageWithUsernameDto")) >= 1);
    }
}