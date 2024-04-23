using System.Text.Json;
using api.Models.Dto;
using api.Models.Services;
using Fleck;
using lib;
using Microsoft.Net.Http.Headers;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;


namespace api.Models.Event_Handler;

public class ClientWantsToBroadcastToRoom : BaseEventHandler<ClientWantsToBroadcastToRoomDto>
{
    public override async Task Handle(ClientWantsToBroadcastToRoomDto dto, IWebSocketConnection socket)
    {
        await IsMessageToxic(dto.Message);
        var message = new ServerBroadcastsMessageWithUsernameDto()
        {
            message = dto.Message,
            Username = StateService.Connections[socket.ConnectionInfo.Id].Username
        };
        
        StateService.BroadcastToRoom(dto.RoomId, JsonSerializer.Serialize(message));
    }

    
    public record RequestModel(string text, List<string> categories, string outputType)
    {
        public override string ToString()
        {
            return $"{{ text = {text}, categories = {categories}, outputType = {outputType} }}";
        }
    }

    private async Task IsMessageToxic(string message)
    {
        var apiKey = "ff19b1c0dc484a4397fba3ea85947be3";
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://hardrfilter.cognitiveservices.azure.com/contentsafety/text:analyze?api-version=2023-10-01"))
            {
                request.Headers.TryAddWithoutValidation("accept", "application/json");
                request.Headers.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", apiKey);

                var req = new RequestModel(message, new List<string>() { "Hate", "Violence" }, "FourSeverityLevels");

                request.Content = new StringContent(JsonSerializer.Serialize(req));
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json"); 

                var response = await httpClient.SendAsync(request);

                string responseBody = await response.Content.ReadAsStringAsync();
                var obj = JsonSerializer.Deserialize<ContentFilterResponse>(responseBody);

                var isToxic = obj.categoriesAnalysis.Count(e => e.severity > 1) >= 1;

                if (isToxic)
                    throw new Exception("You can't say that!");
            }
        }
    }
    
    
}

public class ServerBroadcastsMessageWithUsernameDto : BaseDto
{
    public String message { get; set; }
    public String Username { get; set; }
}
    
public class CategoriesAnalysis
{
    public string category { get; set; }
    public int severity { get; set; }
}

public class ContentFilterResponse
{
    public List<object> blocklistsMatch { get; set; }
    public List<CategoriesAnalysis> categoriesAnalysis { get; set; }
}