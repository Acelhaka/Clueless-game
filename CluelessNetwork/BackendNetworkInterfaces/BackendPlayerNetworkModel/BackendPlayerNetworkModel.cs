using System;
using System.IO;
using CluelessNetwork.TransmittedTypes;
using CluelessNetwork.Websockets;

namespace CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel
{
    internal class BackendPlayerNetworkModel : PlayerModelBase, IBackendPlayerNetworkModel
    {
        /// <summary>
        /// Create a backend player network mode using a communication stream with the frontend
        /// </summary>
        /// <param name="websocket">A stream to communicate with the frontend for this player</param>
        public BackendPlayerNetworkModel(IWebsocket websocket)
        {
            Initialize(websocket);
            if (Settings.PrintNetworkDebugMessagesToConsole)
            {
                AccusationReceived += _ => Console.WriteLine($"Server invoked {nameof(AccusationReceived)}");
                GameStartReceived += () => Console.WriteLine($"Server invoked {nameof(GameStartReceived)}");
                MoveActionReceived += _ => Console.WriteLine($"Server invoked {nameof(MoveActionReceived)}");
                ChatMessageReceived += _ => Console.WriteLine($"Server invoke {nameof(ChatMessageReceived)}");
                PlayerSuggestionReceived +=
                    _ => Console.WriteLine($"Server invoked {nameof(PlayerSuggestionReceived)}");
                PlayerSuggestionResponseReceived += _ =>
                    Console.WriteLine($"Server invoked {nameof(PlayerSuggestionResponseReceived)}");
                SuspectSelectionReceived +=
                    _ => Console.WriteLine($"Server invoked {nameof(SuspectSelectionReceived)}");
            }
        }

        /// <summary>
        /// The user's handle
        /// </summary>
        public string Name { get; init; } = string.Empty;

        public void SendNewTurn(NewTurnMessage newTurnMessage)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending turn message to client");
            PushUpdate(newTurnMessage, UpdateType.NewTurn);
        }

        /// <summary>
        /// Indicates if this player has selected to host a game. If false, the player must join an existing instance.
        /// </summary>
        public bool IsHost { get; init; }

        public event Action? TurnEndReceived;

        /// <summary>
        /// Push a player option update to the client
        /// </summary>
        /// <param name="playerOptionCollection">Object which contains player options</param>
        public void UpdatePlayerOptions(PlayerOptionCollection playerOptionCollection)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending player option collection to client");
            PushUpdate(playerOptionCollection, UpdateType.PlayerOptionsUpdate);
        }

        // TODO: Implement disconnect logic
        public void Disconnect() { }

        public void SendPlayerSuggestion(PlayerSuggestion playerSuggestion)
        {
            Console.WriteLine("Sending player suggestion to client");
            PushUpdate(playerSuggestion, UpdateType.PlayerSuggestion);
        }

        /// <summary>
        /// Send a chat message to the connected front end
        /// </summary>
        /// <param name="message">The message to send</param>
        public void SendChatMessage(ChatMessage message)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending chat message to client");
            PushUpdate(message, UpdateType.ChatMessage);
        }

        /// <summary>
        /// Subscribe to run code when a chat message is received from the frontend for this player
        /// </summary>
        public event Action<ChatMessage>? ChatMessageReceived;

        /// <summary>
        /// Subscribe to run code when a move action is received from the frontend for this player
        /// </summary>
        public event Action<MoveAction>? MoveActionReceived;

        /// <summary>
        /// Subscribe to run code when an accusation is received from the frontend for this player
        /// </summary>
        public event Action<Accusation>? AccusationReceived;

        public void SendMoveActionInformation(MoveActionInformation moveActionInformation)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending move action information to client");
            PushUpdate(moveActionInformation, UpdateType.MoveActionInformation);
        }

        /// <summary>
        /// Send an accusation result to this player
        /// </summary>
        /// <param name="accusationResult">The accusation result</param>
        public void SendAccusationResult(AccusationResult accusationResult)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending accusation result to client");
            PushUpdate(accusationResult, UpdateType.AccusationResult);
        }

        /// <summary>
        /// Send a suspect selection update to this player
        /// </summary>
        /// <param name="suspectSelectionUpdate">The suspect selection update</param>
        public void SendSuspectSelectionUpdate(SuspectSelectionUpdate suspectSelectionUpdate)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending suspect selection to client");
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
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending player suggestion response to client");
            PushUpdate(playerSuggestionResponse, UpdateType.PlayerSuggestionResponse);
        }

        public void SendMoveAction(MoveAction moveAction)
        {
            Console.WriteLine("Sending move action to client");
            PushUpdate(moveAction, UpdateType.MoveAction);
        }

        /// <summary>
        /// Subscribe to run code when a player suggestion is received from the frontend for this player
        /// </summary>
        public event Action<PlayerSuggestion>? PlayerSuggestionReceived;

        /// <summary>
        /// Send a request to the front end so it can prompt the user for a response to another player's suggestion
        /// </summary>
        /// <param name="playerSuggestion"></param>
        /// <param name="suggestion">The suggestion requiring a response</param>
        public void PromptResponseToSuggestion(PlayerSuggestion playerSuggestion)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending player suggestion prompt to client");
            PushUpdate(playerSuggestion, UpdateType.PromptForPlayerSuggestionResponse);
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
                case UpdateType.MoveActionInformation:
                case UpdateType.MoveAction:
                    MoveActionReceived?.Invoke((MoveAction)updateWrapper.UpdateObject!);
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
                case UpdateType.ChatMessage:
                    ChatMessageReceived?.Invoke((ChatMessage) updateWrapper.UpdateObject!);
                    break;
                case UpdateType.TurnEnd:
                    TurnEndReceived?.Invoke();
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