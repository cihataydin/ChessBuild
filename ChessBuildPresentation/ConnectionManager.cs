using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChessBuildPresentation
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public ConcurrentDictionary<string, string> matches = new ConcurrentDictionary<string, string>();
        public List<string> waiters = new List<string>();

        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }
        public void AddSocket(WebSocket socket)
        {
            _sockets.TryAdd(CreateConnectionId(), socket);
        }

        public bool MatchSocket(WebSocket socket)
        {
            var socketId = GetId(socket);
            string value2 = matches.FirstOrDefault(t => t.Value == socketId).Value;

            //if (_sockets.Count == 0)
            //{
            //    waiters.Add(socketId);
            //    return false;
            //}

            List<int> vs = new List<int>();
            Random random = new Random();
            bool state = true;

            if(waiters.Count != 0)
            {
                string waitersId = waiters.ElementAt(0);
                string value1 = matches.FirstOrDefault(t => t.Value == waitersId).Value;
                if (waitersId != socketId && !matches.ContainsKey(waitersId) && !matches.ContainsKey(socketId) && value1 == null && value2 == null)
                {                 
                    matches.TryAdd(waitersId, socketId);
                    waiters.Remove(waitersId);
                    return state;
                }
            }

            while (state)
            {
                int rd = random.Next(_sockets.Keys.Count);
                if (!vs.Contains(rd))
                {
                    vs.Add(rd);
                    
                    string rdId = _sockets.Keys.ElementAt(rd);
                    string value1 = matches.FirstOrDefault(t => t.Value == rdId).Value;                 
                    if (rdId != socketId && !matches.ContainsKey(rdId) && !matches.ContainsKey(socketId) && value1 == null && value2 == null)
                    {
                        matches.TryAdd(rdId, socketId);
                        break;
                    }
                }
                if (_sockets.Keys.Count == vs.Count)
                {
                    waiters.Add(socketId);
                    state = false;
                    break;
                }                  
            }
            return state;
        }

        public async Task RemoveSocket(string id)
        {
            WebSocket socket;
            _sockets.TryRemove(id, out socket);

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the ConnectionManager",
                                    cancellationToken: CancellationToken.None);
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }

}
