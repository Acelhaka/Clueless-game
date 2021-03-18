using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Fact]
        public async Task TestCreateServer()
        {
            // Create a game instance factory
            var gameInstanceServiceMock = new Mock<IGameInstanceService>();

            // Start server
            var server = new CluelessNetworkServer(gameInstanceServiceMock.Object);

            // Request connection to server
            using var client = new CluelessNetworkClient("127.0.0.1", true);

            // Accept connection and send requests to game instance service
            await server.ListenForConnection();

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

        [Fact]
        public async Task TestSendOptionUpdateToFrontend()
        {
            // Create a game instance factory
            var gameInstanceService = new TestGameInstanceService();

            // Start server
            var server = new CluelessNetworkServer(gameInstanceService);

            // Request connection to server
            using var client = new CluelessNetworkClient("127.0.0.1", true);

            // Accept one connection and run logic
            await server.ListenForConnection();

            var playerNetworkModel = gameInstanceService.GameInstance!.Players.Single();
            var playerOptionCollection = new PlayerOptionCollection
            {
                AvailableOptions = new[] {"one", "two"}
            };
            playerNetworkModel.UpdatePlayerOptions(
                playerOptionCollection);

            await playerNetworkModel.ReceiveUpdate();
        }
    }

    internal class TestGameInstance
    {
        public TestGameInstance(IPlayerNetworkModel host)
        {
            Host = host;
        }

        public IPlayerNetworkModel Host { get; }
        public List<IBackendPlayerNetworkModel> Players { get; } = new();
    }

    internal class TestGameInstanceService : IGameInstanceService
    {
        public TestGameInstance? GameInstance { get; private set; }

        public void CreateGameInstance(IBackendPlayerNetworkModel hostPlayerNetworkModel)
        {
            GameInstance = new TestGameInstance(hostPlayerNetworkModel);
        }

        public void AddPlayerToGameInstance(IBackendPlayerNetworkModel playerNetworkModel)
        {
            GameInstance!.Players.Add(playerNetworkModel);
        }
    }
}