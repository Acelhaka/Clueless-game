using System.Linq;
using CluelessNetwork.BackendNetworkInterfaces;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;
using CluelessNetwork.TransmittedTypes;

namespace CluelessBackend
{
    /// <summary>
    /// Receives and sends chat messages to the correct recipients
    /// </summary>
    public class ChatService
    {
        private readonly IGameInstanceService _gameInstanceService;

        /// <summary>
        /// Create a ChatService instance, and starts listening to player added events
        /// </summary>
        /// <param name="gameInstanceService">A game instance service</param>
        public ChatService(IGameInstanceService gameInstanceService)
        {
            _gameInstanceService = gameInstanceService;
            _gameInstanceService.PlayerAdded += OnPlayerAdded;
        }

        /// <summary>
        /// When a player is added to the server, listen for messages from that player
        /// </summary>
        /// <param name="playerModel">The player added</param>
        private void OnPlayerAdded(IBackendPlayerNetworkModel playerModel)
        {
            playerModel.ChatMessageReceived += message => PlayerModelOnChatMessageReceived(message, playerModel);
        }

        /// <summary>
        /// Distributes a received message to all players
        /// </summary>
        /// <param name="message">The message to broadcast</param>
        /// <param name="sender">The player model for the message sender</param>
        private void PlayerModelOnChatMessageReceived(ChatMessage message,
            IBackendPlayerNetworkModel sender)
        {
            var allPlayers = _gameInstanceService.GetAllGameInstances().SelectMany(x => x);
            foreach (var player in allPlayers)
                player.SendChatMessage(new ChatMessage {Content = message.Content, SenderName = sender.Name});
        }
    }
}