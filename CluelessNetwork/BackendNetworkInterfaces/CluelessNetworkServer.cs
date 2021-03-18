using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;
using CluelessNetwork.NetworkSerialization;

namespace CluelessNetwork.BackendNetworkInterfaces
{
    /// <summary>
    /// Runs a TCP listener which accetps clients, packages them into player network models, and hands them off to a
    /// game instance.
    /// </summary>
    public class CluelessNetworkServer : IDisposable
    {
        /// <summary>
        /// A service implemented on the backend to handle game instances
        /// </summary>
        private readonly IGameInstanceService _gameInstanceService;

        /// <summary>
        /// TCP socket wrapper
        /// </summary>
        private readonly TcpListener _tcpListener;

        /// <summary>
        /// Private constructor ensures that the server can only be created from inside this class
        /// Initializes TCP socket, but doesn't start listening yet
        /// </summary>
        public CluelessNetworkServer(IGameInstanceService gameInstanceService)
        {
            _gameInstanceService = gameInstanceService;
            // TODO: Create configuration scheme for defining custom port number
            const int port = 12321;
            try
            {
                // Create a listener to accept incoming connections with
                _tcpListener = new TcpListener(IPAddress.Any, port);
            }
            catch (ArgumentOutOfRangeException)
            {
                // Port was outside of acceptable ranges
                Console.Error.WriteLine($"Invalid port number: {port}");
                Environment.Exit(1);
            }

            try
            {
                // Start the listener
                _tcpListener.Start();
            }
            catch (SocketException socketException)
            {
                // Error starting the socket listener
                // Look up Windows Sockets version 2 API error code here: https://docs.microsoft.com/en-us/windows/desktop/winsock/windows-sockets-error-codes-2
                Console.Error.WriteLine($"Could not start TCP listener. Error code: {socketException.ErrorCode}");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Creates a client instance, and reads the data stream to determine if they are hosting a new game instance or
        /// joining one
        /// </summary>
        /// <param name="client">TCP client handle for the incoming connection</param>
        private void HandleClientConnect(TcpClient client)
        {
            // Get the connection info from the connecting client
            var frontendConnection = TryBuildPlayerNetworkModel(client.GetStream());
            // If a frontend connection object could not be built, ignore incoming connection
            //TODO log this
            if (frontendConnection == null)
                return;

            // Create a new game instance if the connecting player indicated they are hosting
            if (frontendConnection.IsHost)
                _gameInstanceService.CreateGameInstance(frontendConnection);

            // Add player to an appropriate game instance
            _gameInstanceService.AddPlayerToGameInstance(frontendConnection);
        }


        /// <summary>
        /// Waits for a connection, and spins up a new thread that handles the client connection
        /// </summary>
        public Task ListenForConnection()
        {
            try
            {
                // Accept a TCP connection
                var client = _tcpListener.AcceptTcpClient();
                // Don't block listening for more clients while a client is connecting. Run connect handling on a separate thread.
                return Task.Run(() => HandleClientConnect(client));
            }
            catch (SocketException socketException)
            {
                // Error accepting TCP client
                // Look up Windows Sockets version 2 API error code here: https://docs.microsoft.com/en-us/windows/desktop/winsock/windows-sockets-error-codes-2
                Console.Error.WriteLine($"Unable to accept client. Error code: {socketException.ErrorCode}");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Try to build a player network model from a tcp stream. Receives initial connection info.
        /// </summary>
        /// <param name="tcpStream">A stream to build the model from</param>
        /// <returns>A player network model, or null if the connection info couldn't be deserialized</returns>
        private static IBackendPlayerNetworkModel? TryBuildPlayerNetworkModel(Stream tcpStream)
        {
            var connectionInfo = tcpStream.ReadObject<InitialConnectionInfo>();
            if (connectionInfo == null)
                // Could not deserialize connection info. Ignore connection request.
                // TODO: Close connection
                return null;

            return new BackendPlayerNetworkModel.BackendPlayerNetworkModel(tcpStream)
            {
                IsHost = connectionInfo.IsHost
            };
        }

        /// <summary>
        /// Frees resources used by this instances
        /// </summary>
        public void Dispose()
        {
            _tcpListener.Stop();
        }
    }
}