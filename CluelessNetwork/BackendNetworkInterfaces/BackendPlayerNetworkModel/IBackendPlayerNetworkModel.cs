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
        /// Subscribe to run code when an accusation is received from the frontend for this player
        /// </summary>
        public event Action<Accusation>? AccusationReceived;

        /// <summary>
        /// Subscribe to run code when the host wants to start the game
        /// </summary>
        public event Action? GameStartReceived;

        /// <summary>
        /// Subscribe to run code when a move action is received from the frontend for this player
        /// </summary>
        public event Action<MoveActionInformation>? MoveActionReceived;

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
        /// <param name="suggestion">The suggestion requiring a response</param>
        public void PromptResponseToSuggestion(PlayerSuggestion suggestion);

        /// <summary>
        /// Send an accusation result to this player
        /// </summary>
        /// <param name="accusationResult">The accusation result</param>
        public void SendAccusationResult(AccusationResult accusationResult);

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
    }
}