using System;
using CluelessNetwork.TransmittedTypes;

namespace CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel
{
    /// <summary>
    /// Interface for the backend to use to communicate with the front end
    /// </summary>
    public interface IBackendPlayerNetworkModel : IPlayerNetworkModel
    {
        /// <summary>
        /// The user's handle
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Subscribe to run code when an accusation is received from the frontend for this player
        /// </summary>
        public event Action<Accusation>? AccusationReceived;

        /// <summary>
        /// Subscribe to run code when a chat message is received
        /// </summary>
        public event Action<ChatMessage>? ChatMessageReceived;

        /// <summary>
        /// Subscribe to run code when the host wants to start the game
        /// </summary>
        public event Action? GameStartReceived;

        public event Action? TurnEndReceived;

        public void SendNewTurn(NewTurnMessage newTurnMessage);

        /// <summary>
        /// Subscribe to run code when a move action is received from the frontend for this player
        /// </summary>
        public event Action<MoveAction>? MoveActionReceived;

        public void SendMoveActionInformation(MoveActionInformation moveActionInformation);

        /// <summary>
        /// Subscribe to run code when a player suggestion is received from the frontend for this player
        /// </summary>
        public event Action<PlayerSuggestion>? PlayerSuggestionReceived;

        /// <summary>
        /// Subscribe to run code when a player suggestion response is received from the frontend for this player
        /// </summary>
        public event Action<PlayerSuggestionResponse>? PlayerSuggestionResponseReceived;

        /// <summary>
        /// Subscribe to run code when a player selects a suspect
        /// </summary>
        public event Action<SuspectSelectionUpdate>? SuspectSelectionReceived;

        /// <summary>
        /// Send a request to the front end so it can prompt the user for a response to another player's suggestion
        /// </summary>
        /// <param name="playerSuggestion"></param>
        /// <param name="suggestion">The suggestion requiring a response</param>
        public void PromptResponseToSuggestion(PlayerSuggestion playerSuggestion);

        /// <summary>
        /// Send an accusation result to this player
        /// </summary>
        /// <param name="accusationResult">The accusation result</param>
        public void SendAccusationResult(AccusationResult accusationResult);

        /// <summary>
        /// Sends a chat message to everyone on the server
        /// </summary>
        /// <param name="message">The message</param>
        public void SendChatMessage(ChatMessage message);

        /// <summary>
        /// Send game start information to this player
        /// </summary>
        /// <param name="gameStartInfo">The game start information</param>
        public void SendGameStartInfo(GameStartInfo gameStartInfo);

        /// <summary>
        /// Send a player suggestion response to this player
        /// </summary>
        /// <param name="playerSuggestionResponse">The player suggestion response to send</param>
        public void SendPlayerSuggestionResponse(PlayerSuggestionResponse playerSuggestionResponse);

        /// <summary>
        /// Send a suspect selection update to this player
        /// </summary>
        /// <param name="suspectSelectionUpdate">The suspect selection update</param>
        public void SendSuspectSelectionUpdate(SuspectSelectionUpdate suspectSelectionUpdate);

        /// <summary>
        /// Send a player option collection to the frontend
        /// </summary>
        /// <param name="playerOptionCollection"></param>
        public void UpdatePlayerOptions(PlayerOptionCollection playerOptionCollection);

        /// <summary>
        /// Disconnects the player from the server
        /// </summary>
        void Disconnect();

        void SendPlayerSuggestion(PlayerSuggestion playerSuggestion);
    }
}