using Fleck;

namespace api.Models.Services;

public static class StateService
{
    public static Dictionary<Guid, WsWithMetaData> Connections = new();
    public static Dictionary<int, HashSet<Guid>> Rooms = new();
    

    public static bool AddConnection(IWebSocketConnection ws)
    {
        return Connections.TryAdd(ws.ConnectionInfo.Id, new WsWithMetaData(ws));
    }

    public static void AddToRoom(IWebSocketConnection ws, int room)
    {
        if (!Rooms.ContainsKey(room))
            Rooms.Add(room, new HashSet<Guid>());
        Rooms[room].Add(ws.ConnectionInfo.Id);
    }

    public static void BroadcastToRoom(int room, string message)
    {
        if (Rooms.TryGetValue(room, out var guids))
        {
            foreach (var guid in guids)
            {
                if (Connections.TryGetValue(guid, out var ws))
                    ws.Connection.Send(message);

            }
        }
    }
}