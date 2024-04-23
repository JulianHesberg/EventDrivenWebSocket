using Fleck;

namespace api.Models;

public class WsWithMetaData(IWebSocketConnection connection)
{
    public IWebSocketConnection Connection { get; set; } = connection;
    public String Username { get; set; }
}