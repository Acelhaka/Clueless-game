using System;
using CluelessNetwork.TransmittedTypes;
using CluelessNetwork.Websockets;

namespace CluelessNetwork.FrontendNetworkInterfaces
{
    /// <summary>
    /// A network client for this player to use to communicate with the server
    /// </summary>
    public class CluelessNetworkClient : PlayerModelBase, IFrontendPlayerNetworkModel
    {
        /// <summary>
        /// Creates a network client and connects to a clueless server
        /// </summary>
        /// <param name="hostname">The server address to connect to</param>
        /// <param name="isHost">If this client is hosting, or simply joining</param>
        /// <param name="name">The user's chosen handle</param>
        public CluelessNetworkClient(string hostname, bool isHost, string name)
        {
            IsHost = isHost;
            const int port = 32123;
            _websocket = new ClientWebsocketWrapper($"ws://{hostname}:{port}/ws");
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Server connection established");
            _websocket?.WriteObject(
                new InitialConnectionInfo
                {
                    IsHost = isHost,
                    Name = name
                });

            if (Settings.PrintNetworkDebugMessagesToConsole)
            {
                Console.WriteLine($"InitialConnectionInfo sent with IsHost={isHost}");

                AccusationResultReceived +=
                    _ => Console.WriteLine($"Client invoked {nameof(AccusationResultReceived)}");
                GameStartInfoReceived += _ => Console.WriteLine($"Client invoked {nameof(GameStartInfoReceived)}");
                OptionsUpdateReceived += _ => Console.WriteLine($"Client invoked {nameof(OptionsUpdateReceived)}");
                ChatMessageReceived += _ => Console.WriteLine($"Server invoke {nameof(ChatMessageReceived)}");
                PlayerSuggestionReceived +=
                    _ => Console.WriteLine($"Client invoked {nameof(PlayerSuggestionReceived)}");
                PlayerSuggestionResponseReceived += _ =>
                    Console.WriteLine($"Client invoked {nameof(PlayerSuggestionResponseReceived)}");
                SuspectSelectionUpdateReceived += _ =>
                    Console.WriteLine($"Client invoked {nameof(SuspectSelectionUpdateReceived)}");
            }
        }

        /// <summary>
        /// Indicates if this player has selected to host a game. If false, the player must join an existing instance.
        /// </summary>
        public bool IsHost { get; }

        /// <summary>
        /// Subscribe to run code when the server requests that options are updated
        /// </summary>
        public event Action<PlayerOptionCollection>? OptionsUpdateReceived;

        /// <summary>
        /// Send a move action to the server
        /// </summary>
        /// <param name="moveActionInformation">All the information that the server needs to know to perform the move</param>
        public void SendMoveAction(MoveActionInformation moveActionInformation)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending move action to server");
            PushUpdate(moveActionInformation, UpdateType.MoveAction);
        }

        /// <summary>
        /// Send a suggestion to the server
        /// </summary>
        /// <param name="playerSuggestion">The suggestion to send</param>
        public void SendPlayerSuggestion(PlayerSuggestion playerSuggestion)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending player suggestion to server");
            PushUpdate(playerSuggestion, UpdateType.PlayerSuggestion);
        }

        /// <summary>
        /// Subscribe to run code when a player suggestion is received
        /// </summary>
        public event Action<PlayerSuggestion>? PlayerSuggestionReceived;

        /// <summary>
        /// Send a suggestion response to the server
        /// </summary>
        /// <param name="playerSuggestionResponse">The suggestion response to send</param>
        public void SendPlayerSuggestionResponse(PlayerSuggestionResponse playerSuggestionResponse)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending player suggestion response to server");
            PushUpdate(playerSuggestionResponse, UpdateType.PlayerSuggestionResponse);
        }

        /// <summary>
        /// Send an accusation to the server
        /// </summary>
        /// <param name="accusation">The accusation to send</param>
        public void SendAccusation(Accusation accusation)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending accusation to server");
            PushUpdate(accusation, UpdateType.Accusation);
        }

        // TODO:: we need a function here to send players position to  backend
        //public void SendPlayerPosition( )
        //{
        //    if (Settings.PrintNetworkDebugMessagesToConsole)
        //        Console.WriteLine("Sending player option to server");
        //    PushUpdate(position, UpdateType.Accusation);
        //}

        /// <summary>
        /// Subscribe to run code when an accusation result is received
        /// </summary>
        public event Action<AccusationResult>? AccusationResultReceived;

        /// <summary>
        /// Send a selection for a suspect to the server
        /// </summary>
        public void SendSuspectSelection(SuspectSelectionUpdate suspectSelectionUpdate)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending suspect selection update to server");
            PushUpdate(suspectSelectionUpdate, UpdateType.SuspectSelection);
        }

        public void SendChatMessage(ChatMessage message)
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending chat message to server");
            PushUpdate(message, UpdateType.ChatMessage);
        }

        public event Action<ChatMessage>? ChatMessageReceived;

        /// <summary>
        /// Subscribe to run code when a suspect selection is made by another player
        /// </summary>
        public event Action<SuspectSelectionUpdate>? SuspectSelectionUpdateReceived;

        /// <summary>
        /// Sends a request to the server to start the game
        /// </summary>
        public void SendGameStartRequest()
        {
            if (Settings.PrintNetworkDebugMessagesToConsole)
                Console.WriteLine("Sending start game request to server");
            PushUpdate(null, UpdateType.GameStart);
        }

        /// <summary>
        /// Subscribe to run code when the game is started
        /// </summary>
        public event Action<GameStartInfo>? GameStartInfoReceived;

        /// <summary>
        /// Subscribe to run code when a player suggestion is received
        /// </summary>
        public event Action<PlayerSuggestionResponse>? PlayerSuggestionResponseReceived;

        /// <summary>
        /// Runs when an update is received over the network
        /// </summary>
        /// <param name="updateWrapper">A wrapper containing the update type and possibly an object with information</param>
        protected override void HandleUpdateReceived(NetworkTransmittedUpdate updateWrapper)
        {
            switch (updateWrapper.UpdateType)
            {
                case UpdateType.PlayerOptionsUpdate:
                    OptionsUpdateReceived?.Invoke((PlayerOptionCollection)updateWrapper.UpdateObject!);
                    break;
                case UpdateType.PlayerSuggestion:
                    PlayerSuggestionReceived?.Invoke((PlayerSuggestion)updateWrapper.UpdateObject!);
                    break;
                case UpdateType.SuspectSelection:
                    SuspectSelectionUpdateReceived?.Invoke((SuspectSelectionUpdate)updateWrapper.UpdateObject!);
                    break;
                case UpdateType.GameStart:
                    GameStartInfoReceived?.Invoke((GameStartInfo)updateWrapper.UpdateObject!);
                    break;
                case UpdateType.AccusationResult:
                    AccusationResultReceived?.Invoke((AccusationResult)updateWrapper.UpdateObject!);
                    break;
                case UpdateType.PlayerSuggestionResponse:
                    PlayerSuggestionResponseReceived?.Invoke((PlayerSuggestionResponse)updateWrapper.UpdateObject!);
                    break;
                case UpdateType.ChatMessage:
                    ChatMessageReceived?.Invoke((ChatMessage)updateWrapper.UpdateObject!);
                    break;
                case UpdateType.MoveAction:
                case UpdateType.Accusation:
                    throw new InvalidOperationException(
                        $"The frontend has no implementation for update type: {updateWrapper}");
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}