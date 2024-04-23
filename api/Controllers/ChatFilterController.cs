using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace api.Controllers;

[Route("api/filter")]
[ApiController]
public class ChatFilterController : ControllerBase
{
    [HttpGet("profanity")]
    public async Task<object> IsProfaneLanguage(String text)
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://hardrfilter.cognitiveservices.azure.com/contentsafety/text:analyze?api-version=2023-10-01"))
            {
                request.Headers.TryAddWithoutValidation("accept", "application/json");
                request.Headers.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", "ff19b1c0dc484a4397fba3ea85947be3"); 

                request.Content = new StringContent($"{{\n  \"text\": \"{text}\",\n  \"categories\": [\n    \"Hate\"\n  ],\n  \"blocklistNames\": [\n    \"Profanity\"\n  ],\n  \"haltOnBlocklistHit\": true,\n  \"outputType\": \"FourSeverityLevels\"\n}}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json"); 
                
                Console.WriteLine(request);
                var response = await httpClient.SendAsync(request);
                var jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("check out this content:   " + response);
                //var jObject = JObject.Parse(jsonString);

               // int severity = Int32.Parse((string)jObject["severity"]);

               return jsonString;
            }
        }
    }
    
}