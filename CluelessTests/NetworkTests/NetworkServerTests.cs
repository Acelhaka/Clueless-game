using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CluelessBackend;
using CluelessNetwork;
using CluelessNetwork.BackendNetworkInterfaces;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;
using CluelessNetwork.FrontendNetworkInterfaces;
using CluelessNetwork.TransmittedTypes;
using FluentAssertions;
using Moq;
using Xunit;

namespace CluelessTests.NetworkTests
{
    public class NetworkServerTests
    {
        // Makes sure tests run one at a time, as the port is reused
        private readonly SemaphoreSlim _sema = new(1);

        [Fact]
        public async Task TestChatMessage()
        {
            await _sema.WaitAsync();
            {
                // Create a game instance factory
                var gameInstanceService = new GameInstanceService();

                // Create a chat service
                var unused = new ChatService(gameInstanceService);

                // Start server
                using var server = new CluelessNetworkServer(gameInstanceService);

                // Connect two clients to the server
                using var client = new CluelessNetworkClient("127.0.0.1", true, "ClientName");
                server.ListenForConnection(listenContinuously: false);

                // Sanity check: we should have two players on the server
                gameInstanceService.GetAllGameInstances().Single().GetPlayerModels().Count.Should().Be(1);
                var playerBackendModel = gameInstanceService.GetAllGameInstances().Single().GetPlayerModels().Single();
                var message = "Test message";
                var clientReceivedChatMessages = 0;

                client.ChatMessageReceived += delegate(ChatMessage incomingMessage)
                {
                    incomingMessage.Content.Should().Be(message);
                    clientReceivedChatMessages++;
                };

                //  
                client.SendChatMessage(new ChatMessage {Content = message, Scope = ChatMessageScope.Server});

                playerBackendModel.ReceiveUpdate();

                client.ReceiveUpdate();

                clientReceivedChatMessages.Should().Be(1);
            }
            _sema.Release();
        }

        [Fact]
        public async Task TestChatMessageTwoPlayers()
        {
            await _sema.WaitAsync();
            {
                // Create a game instance factory
                var gameInstanceService = new GameInstanceService();

                // Create a chat service
                var unused = new ChatService(gameInstanceService);

                // Start server
                using var server = new CluelessNetworkServer(gameInstanceService);

                // Connect two clients to the server
                using var client1 = new CluelessNetworkClient("127.0.0.1", true, "Client 1");
                server.ListenForConnection(listenContinuously: false);
                using var client2 = new CluelessNetworkClient("127.0.0.1", false, "Client 1");
                server.ListenForConnection(listenContinuously: false);

                // Sanity check: we should have two players on the server
                gameInstanceService.GetAllGameInstances().Single().GetPlayerModels().Count.Should().Be(2);
                var message = "Test message";
                var client1ReceivedChatMessages = 0;
                var client2ReceivedChatMessages = 0;

                var playerModel1 = gameInstanceService.GetAllGameInstances().Single().GetPlayerModels().First();

                client1.ChatMessageReceived += delegate(ChatMessage incomingMessage)
                {
                    incomingMessage.Content.Should().Be(message);
                    incomingMessage.SenderName.Should().Be("Client 1");
                    client1ReceivedChatMessages++;
                };

                client2.ChatMessageReceived += delegate(ChatMessage incomingMessage)
                {
                    incomingMessage.Content.Should().Be(message);
                    incomingMessage.SenderName.Should().Be("Client 1");
                    client2ReceivedChatMessages++;
                };

                // Sen message to server player model 1
                client1.SendChatMessage(new ChatMessage {Content = message});

                // Receive message from client 1, and broadcast it to clients 1 and 2
                playerModel1.ReceiveUpdate();

                // Receive message from server broadcast
                await Task.WhenAll(Task.Run(client1.ReceiveUpdate), Task.Run(client2.ReceiveUpdate));

                client1ReceivedChatMessages.Should().Be(1);
                client2ReceivedChatMessages.Should().Be(1);
            }
            _sema.Release();
        }

        [Fact]
        public async Task TestCreateServer()
        {
            await _sema.WaitAsync();
            {
                // Create a game instance factory
                var gameInstanceServiceMock = new Mock<IGameInstanceService>();

                // Start server
                using var server = new CluelessNetworkServer(gameInstanceServiceMock.Object);

                // Request connection to server
                using var client = new CluelessNetworkClient("127.0.0.1", true, string.Empty);

                // Accept connection and send requests to game instance service
                server.ListenForConnection(listenContinuously: false);

                // Verify that we asked the game instance service mock to do all the correct things 
                gameInstanceServiceMock.Invocations.Count.Should().Be(2);
                gameInstanceServiceMock.Invocations[0].Method.Name.Should()
                    .Be(nameof(IGameInstanceService.CreateGameInstance));
                gameInstanceServiceMock.Invocations[1].Method.Name.Should()
                    .Be(nameof(IGameInstanceService.AddPlayerToGameInstance));

                // Check parameters on call to AddPlayerToGameInstance 
                gameInstanceServiceMock.Invocations[1].Arguments.Count.Should().Be(1);
                gameInstanceServiceMock.Invocations[1].Arguments[0].As<IPlayerNetworkModel>().IsHost.Should().BeTrue();
            }
            _sema.Release();
        }

        [Fact]
        public async Task TestSendOptionUpdateToFrontend()
        {
            await _sema.WaitAsync();
            {
                // Create a game instance factory
                var gameInstanceService = new GameInstanceService();

                // Start server
                using var server = new CluelessNetworkServer(gameInstanceService);

                // Request connection to server
                using var client = new CluelessNetworkClient("127.0.0.1", true, string.Empty);

                // Accept one connection and run logic
                server.ListenForConnection(listenContinuously: false);

                var backendPlayerNetworkModel = gameInstanceService.GetAllGameInstances().Single().GetPlayerModels().Single();
                var playerOptionCollection = new PlayerOptionCollection
                {
                    AvailableOptions = new[] {"one", "two"}
                };

                PlayerOptionCollection? subject = null;

                client.OptionsUpdateReceived += received => subject = received;

                backendPlayerNetworkModel.UpdatePlayerOptions(
                    playerOptionCollection);

                client.ReceiveUpdate();

                subject.Should().BeEquivalentTo(playerOptionCollection);
            }
            _sema.Release();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        public async Task TestStartGame(int playerCount)
        {
            await _sema.WaitAsync();
            {
                // Create a game instance factory
                var gameInstanceService = new GameInstanceService();

                // Start server
                using var server = new CluelessNetworkServer(gameInstanceService);

                // Request connection to server
                var clients = new List<CluelessNetworkClient>();
                for (int i = 0; i < playerCount; i++)
                    clients.Add(new CluelessNetworkClient("127.0.0.1", i == 0, string.Empty));

                // Accept connections
                for (int i = 0; i < playerCount; i++)
                    server.ListenForConnection(listenContinuously: false);

                var gameInstance = gameInstanceService.GetAllGameInstances().Single();
                gameInstance.CanAddPlayers.Should().Be(playerCount != 6);
                var backendPlayerModels = gameInstance.GetPlayerModels();
                backendPlayerModels.Count.Should().Be(playerCount);
                for (int i = 0; i < playerCount; i++)
                    backendPlayerModels[i].IsHost.Should().Be(i == 0);
                
                clients.First().SendGameStartRequest();
                backendPlayerModels.First().ReceiveUpdate();

                gameInstance.CanAddPlayers.Should().BeFalse();
                
                foreach (var client in clients)
                    client.Dispose();
            }
            _sema.Release();
        }


    }
}