using System.Collections.Concurrent;
using System.Net.WebSockets;

public static class WebSocketConnections
{
    private static ConcurrentBag<WebSocket> _connections = new ConcurrentBag<WebSocket>();

    public static void AddConnection(WebSocket webSocket)
    {
        _connections.Add(webSocket);
    }

    public static void RemoveConnection(WebSocket webSocket)
    {
        _connections.TryTake(out _);
    }

    public static IEnumerable<WebSocket> GetConnections()
    {
        return _connections;
    }
}
