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

            if (WebSocketConnectionManager.MatchSocket(socket))
            {
                string matchKey = null;
                if (WebSocketConnectionManager.matches.Values.Contains(socketId))
                {
                    matchKey = WebSocketConnectionManager.matches.FirstOrDefault(t => t.Value == socketId).Key;
                }
                WebSocketConnectionManager.matches.TryGetValue(socketId, out string matchValue);

                await SendMessageAsync(socketId, " Match is completed....");
                await SendMessageAsync(socketId, "Black");
                if(matchValue != null)
                {
                    await SendMessageAsync(matchValue, " Match is completed....");
                    await SendMessageAsync(matchValue, "White");
                }
                else if(matchKey != null)
                {
                    await SendMessageAsync(matchKey, " Match is completed....");
                    await SendMessageAsync(matchKey, "White");
                }              
            }
            else
            {
                await SendMessageAsync(socketId, "Waiting for opponent...");
            }
            //await SendMessageToAllAsync($"{socketId} is now connected");  
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
            WebSocketConnectionManager.matches.TryGetValue(socketId, out string matchValue);
            if (matchValue != null)
            {
                await SendMessageAsync(matchValue, message);
            }
            if(matchKey != null)
            {
                await SendMessageAsync(matchKey, message);
            }
        }
    }
}
