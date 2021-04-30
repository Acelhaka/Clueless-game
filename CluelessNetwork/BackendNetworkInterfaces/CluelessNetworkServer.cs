using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading.Tasks;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;
using CluelessNetwork.NetworkSerialization;

namespace CluelessNetwork.BackendNetworkInterfaces
{
    /// <summary>
    /// Runs a TCP listener which accetps clients, packages them into player network models, and hands them off to a
    /// game instance.
    /// </summary>
    public class CluelessNetworkServer
    {
        /// <summary>
        /// A service implemented on the backend to handle game instances
        /// </summary>
        private readonly IGameInstanceService _gameInstanceService;

        /// <summary>
        /// Private constructor ensures that the server can only be created from inside this class
        /// Initializes TCP socket, but doesn't start listening yet
        /// </summary>
        public CluelessNetworkServer(IGameInstanceService gameInstanceService)
        {
            _gameInstanceService = gameInstanceService;
        }

        /// <summary>
        /// Creates a client instance, and reads the data stream to determine if they are hosting a new game instance or
        /// joining one
        /// </summary>
        /// <param name="client">TCP client handle for the incoming connection</param>
        private void HandleClientConnect(WebSocket client, bool listenContinuously)
        {
            // Get the connection info from the connecting client
            var frontendConnection = TryBuildPlayerNetworkModel(client);
            // If a frontend connection object could not be built, ignore incoming connection
            //TODO log this
            if (frontendConnection == null)
                return;

            // Create a new game instance if the connecting player indicated they are hosting
            if (frontendConnection.IsHost)
            {
                if (Settings.PrintNetworkDebugMessagesToConsole)
                    Console.WriteLine(
                        "Connection set IsHost=true. Requesting a new game instance from GameInstanceService");
                _gameInstanceService.CreateGameInstance(frontendConnection);
            }

            // Add player to an appropriate game instance
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Adding player to game instance");
            _gameInstanceService.AddPlayerToGameInstance(frontendConnection);
            if (listenContinuously)
                Task.Run(frontendConnection.ListenForUpdatesContinuously);
        }

        /// <summary>
        /// Try to build a player network model from a tcp stream. Receives initial connection info.
        /// </summary>
        /// <param name="tcpStream">A stream to build the model from</param>
        /// <returns>A player network model, or null if the connection info couldn't be deserialized</returns>
        private static IBackendPlayerNetworkModel? TryBuildPlayerNetworkModel(WebSocket webSocket)
        {
            var connectionInfo = webSocket.ReadObject<InitialConnectionInfo>();
            if (connectionInfo == null)
            {
                // Could not deserialize connection info. Ignore connection request.
                // TODO: Close connection
                if (Settings.PrintNetworkDebugMessagesToConsole)
                    Console.WriteLine("Did not receive InitialConnectionInfo from client");
                return null;
            }

            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Received InitialConnectionInfo");

            // Require a name to be set
            if (connectionInfo.Name == null)
                return null;
            
            return new BackendPlayerNetworkModel.BackendPlayerNetworkModel(webSocket)
            {
                IsHost = connectionInfo.IsHost,
                Name = connectionInfo.Name
            };
        }
    }
}