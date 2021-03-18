using System;
using System.IO;
using CluelessNetwork.TransmittedTypes;

namespace CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel
{
    internal class BackendPlayerNetworkModel : PlayerModelBase, IBackendPlayerNetworkModel
    {
        /// <summary>
        /// Create a backend player network mode using a communication stream with the frontend
        /// </summary>
        /// <param name="tcpStream">A stream to communicate with the frontend for this player</param>
        public BackendPlayerNetworkModel(Stream tcpStream)
        {
            Initialize(tcpStream);
        }

        /// <summary>
        /// Indicates if this player has selected to host a game. If false, the player must join an existing instance.
        /// </summary>
        public bool IsHost { get; init; }

        /// <summary>
        /// Push a player option update to the client
        /// </summary>
        /// <param name="playerOptionCollection">Object which contains player options</param>
        public void UpdatePlayerOptions(PlayerOptionCollection playerOptionCollection)
        {
            PushUpdate(playerOptionCollection, UpdateType.PlayerOptionsUpdate);
        }

        /// <summary>
        /// Subscribe to run code when a move action is received from the frontend for this player
        /// </summary>
        public event Action<MoveActionInformation>? MoveActionReceived;

        /// <summary>
        /// Subscribe to run code when an accusation is received from the frontend for this player
        /// </summary>
        public event Action<Accusation>? AccusationReceived;

        /// <summary>
        /// Send an accusation result to this player
        /// </summary>
        /// <param name="accusationResult">The accusation result</param>
        public void SendAccusationResult(AccusationResult accusationResult)
        {
            PushUpdate(accusationResult, UpdateType.AccusationResult);
        }

        /// <summary>
        /// Send a suspect selection update to this player
        /// </summary>
        /// <param name="suspectSelectionUpdate">The suspect selection update</param>
        public void SendSuspectSelectionUpdate(SuspectSelectionUpdate suspectSelectionUpdate)
        {
            PushUpdate(suspectSelectionUpdate, UpdateType.SuspectSelection);
        }

        /// <summary>
        /// Send game start information to this player
        /// </summary>
        /// <param name="gameStartInfo">The game start information</param>
        public void SendGameStartInfo(GameStartInfo gameStartInfo)
        {
            PushUpdate(gameStartInfo, UpdateType.GameStart);
        }

        /// <summary>
        /// Subscribe to run code when the host wants to start the game
        /// </summary>
        public event Action? GameStartReceived;

        /// <summary>
        /// Subscribe to run code when a player selects a suspect
        /// </summary>
        public event Action<SuspectSelectionUpdate>? SuspectSelectionReceived;

        /// <summary>
        /// Send a player suggestion response to this player
        /// </summary>
        /// <param name="playerSuggestionResponse">The player suggestion response to send</param>
        public void SendPlayerSuggestionResponse(PlayerSuggestionResponse playerSuggestionResponse)
        {
            PushUpdate(playerSuggestionResponse, UpdateType.PlayerSuggestionResponse);
        }

        /// <summary>
        /// Subscribe to run code when a player suggestion is received from the frontend for this player
        /// </summary>
        public event Action<PlayerSuggestion>? PlayerSuggestionReceived;

        /// <summary>
        /// Send a request to the front end so it can prompt the user for a response to another player's suggestion
        /// </summary>
        /// <param name="suggestion">The suggestion requiring a response</param>
        public void PromptResponseToSuggestion(PlayerSuggestion suggestion)
        {
            PushUpdate(suggestion, UpdateType.PlayerSuggestion);
        }

        /// <summary>
        /// Subscribe to run code when a player suggestion response is received from the frontend for this player
        /// </summary>
        public event Action<PlayerSuggestionResponse>? PlayerSuggestionResponseReceived;

        /// <summary>
        /// Runs when an update is received over the network
        /// </summary>
        /// <param name="updateWrapper"></param>
        protected override void HandleUpdateReceived(NetworkTransmittedUpdate updateWrapper)
        {
            // Choose a handler for the update, based on update type
            switch (updateWrapper.UpdateType)
            {
                case UpdateType.MoveAction:
                    MoveActionReceived?.Invoke((MoveActionInformation) updateWrapper.UpdateObject!);
                    break;
                case UpdateType.PlayerSuggestion:
                    PlayerSuggestionReceived?.Invoke((PlayerSuggestion) updateWrapper.UpdateObject!);
                    break;
                case UpdateType.PlayerSuggestionResponse:
                    PlayerSuggestionResponseReceived?.Invoke((PlayerSuggestionResponse) updateWrapper.UpdateObject!);
                    break;
                case UpdateType.Accusation:
                    AccusationReceived?.Invoke((Accusation) updateWrapper.UpdateObject!);
                    break;
                case UpdateType.SuspectSelection:
                    SuspectSelectionReceived?.Invoke((SuspectSelectionUpdate) updateWrapper.UpdateObject!);
                    break;
                case UpdateType.GameStart:
                    GameStartReceived?.Invoke();
                    break;
                // The following aren't implemented on the backend
                case UpdateType.PlayerOptionsUpdate:
                case UpdateType.AccusationResult:
                    throw new InvalidOperationException(
                        $"The backend has no implementation for update type: {updateWrapper}");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}