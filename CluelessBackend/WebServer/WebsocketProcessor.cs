using System.Net.WebSockets;
using System.Threading.Tasks;
using CluelessNetwork.BackendNetworkInterfaces;
using CluelessNetwork.Websockets;

namespace CluelessBackend.WebServer
{
    public class WebsocketManager
    {
        private readonly CluelessNetworkServer _cluelessNetworkServer;

        public WebsocketManager(CluelessNetworkServer cluelessNetworkServer)
        {
            _cluelessNetworkServer = cluelessNetworkServer;
        }

        public void AddSocket(WebSocket websocket, TaskCompletionSource socketFinishedTcs)
        {
            var wrapper = new ServerWebsocketWrapper(websocket, socketFinishedTcs);
            _cluelessNetworkServer.HandleClientConnect(wrapper, listenContinuously: !Program.IsWebsocketTest);
        }
    }
}