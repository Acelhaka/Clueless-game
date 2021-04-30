using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CluelessNetwork.NetworkSerialization;

namespace CluelessNetwork.Websockets
{
    public class ServerWebsocketWrapper : WebsocketWrapperBase
    {
        private readonly WebSocket _websocket;
        private readonly TaskCompletionSource _websocketCloseTcs;

        public ServerWebsocketWrapper(WebSocket websocket, TaskCompletionSource websocketCloseTcs)
        {
            _websocket = websocket;
            _websocketCloseTcs = websocketCloseTcs;
        }

        private const int BufferSize = 1024 * 1024;


        protected override void SendString(string json)
        {
            var bytes = Encoding.UTF8.GetBytes(json);
            _websocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None).Wait();
        }

        protected override string? RecvString()
        {
            // Deserialize data into an object
            var buffer = new byte[BufferSize];
            var receiveResult = _websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).WaitForResult();
            var messageBytes = buffer[..receiveResult.Count];
            var messageString = Encoding.UTF8.GetString(messageBytes);
            return messageString;
        }

        public override void Dispose()
        {
            _websocket.Dispose();
            _websocketCloseTcs.SetResult();
        }
    }
}