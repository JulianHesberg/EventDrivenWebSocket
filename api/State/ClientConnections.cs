using Fleck;

namespace api.State;

public class ClientConnections
{
    public List<IWebSocketConnection> clientConnections = new List<IWebSocketConnection>();

    public void AddConnectionToPool(IWebSocketConnection socket)
    {
        clientConnections.Add(socket);
    }

    public void RemoveConnectionFromPool(IWebSocketConnection socket)
    {
        clientConnections.Remove(socket);
    }

    public List<IWebSocketConnection> GetClientConnections()
    {
        return clientConnections;
    }
}