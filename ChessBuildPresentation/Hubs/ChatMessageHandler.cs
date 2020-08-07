using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessBuildPresentation
{
    public class ChatMessageHandler : WebSocketHandler
    {
        public ChatMessageHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
            
        }
        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            var socketId = WebSocketConnectionManager.GetId(socket);
            WebSocketConnectionManager.MatchSocket(socket);
            //await SendMessageToAllAsync($"{socketId} is now connected");
            await SendMessageAsync(socketId, socketId);
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var message = $"{Encoding.UTF8.GetString(buffer, 0, result.Count)}";

            string matchKey = null;
            if (WebSocketConnectionManager.matches.Values.Contains(socketId))
            {
                matchKey = WebSocketConnectionManager.matches.FirstOrDefault(t => t.Value == socketId).Key;
            }
            WebSocketConnectionManager.matches.TryGetValue(socketId, out string matchvalue);
            if (matchvalue != null)
            {
                await SendMessageAsync(matchvalue, message +" "+matchvalue);
            }
            if(matchKey != null)
            {
                await SendMessageAsync(matchKey, message +" "+ matchKey);
            }
        }
    }
}
