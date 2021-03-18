using System;
using CluelessNetwork.TransmittedTypes;

namespace CluelessNetwork.FrontendNetworkInterfaces
{
    /// <summary>
    /// Interface for the frontend to use to communicate with the backend
    /// </summary>
    public interface IFrontendPlayerNetworkModel : IPlayerNetworkModel
    {
        /// <summary>
        /// Subscribe to run code when an accusation result is received
        /// </summary>
        public event Action<AccusationResult>? AccusationResultReceived;

        /// <summary>
        /// Subscribe to run code when the game is started
        /// </summary>
        public event Action<GameStartInfo>? GameStartInfoReceived;

        /// <summary>
        /// Subscribe to run code when the server requests that options are updated
        /// </summary>
        public event Action<PlayerOptionCollection>? OptionsUpdateReceived;

        /// <summary>
        /// Subscribe to run code when a player suggestion is received
        /// </summary>
        public event Action<PlayerSuggestion>? PlayerSuggestionReceived;

        /// <summary>
        /// Subscribe to run code when a player suggestion response is received
        /// </summary>
        public event Action<PlayerSuggestionResponse>? PlayerSuggestionResponseReceived;

        /// <summary>
        /// Subscribe to run code when a suspect selection is made by another player
        /// </summary>
        public event Action<SuspectSelectionUpdate>? SuspectSelectionUpdateReceived;

        /// <summary>
        /// Send an accusation to the server
        /// </summary>
        /// <param name="accusation">The accusation to send</param>
        public void SendAccusation(Accusation accusation);

        /// <summary>
        /// Sends a request to the server to start the game
        /// </summary>
        public void SendGameStartRequest();

        /// <summary>
        /// Send a move action to the server
        /// </summary>
        /// <param name="moveActionInformation">All the information that the server needs to know to perform the move</param>
        public void SendMoveAction(MoveActionInformation moveActionInformation);

        /// <summary>
        /// Send a suggestion to the server
        /// </summary>
        /// <param name="playerSuggestion">The suggestion to send</param>
        public void SendPlayerSuggestion(PlayerSuggestion playerSuggestion);

        /// <summary>
        /// Send a suggestion response to the server
        /// </summary>
        /// <param name="playerSuggestionResponse">The suggestion response to send</param>
        public void SendPlayerSuggestionResponse(PlayerSuggestionResponse playerSuggestionResponse);

        /// <summary>
        /// Send a selection for a suspect to the server
        /// </summary>
        public void SendSuspectSelection(SuspectSelectionUpdate suspectSelectionUpdate);
    }
}