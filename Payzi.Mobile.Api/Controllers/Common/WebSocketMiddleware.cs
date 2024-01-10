using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;

    public WebSocketMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await HandleWebSocketAsync(context, webSocket);
        }
        else
        {
            await _next(context);
        }
    }

    private async Task HandleWebSocketAsync(HttpContext context, WebSocket webSocket)
    {
        // Lógica de manejo de WebSocket aquí
        // Puedes utilizar la lógica existente del método GetTransaccion, adaptándola según sea necesario

        // Ejemplo de cómo recibir mensajes desde el cliente WebSocket
        var buffer = new ArraySegment<byte>(new byte[8192]);
        var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            // Procesar el mensaje recibido y enviar la respuesta al cliente
            // ...

            // Esperar el siguiente mensaje
            result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
        }

        // Cerrar la conexión cuando el cliente WebSocket se desconecta
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
}
