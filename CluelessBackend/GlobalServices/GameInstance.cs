using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CluelessBackend.Core;
using CluelessNetwork.BackendNetworkInterfaces;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;
using CluelessNetwork.TransmittedTypes;

namespace CluelessBackend
{
    public class GameInstance : IGameInstance, IDisposable
    {
        private readonly IBackendPlayerNetworkModel _host;

        public GameInstance(IBackendPlayerNetworkModel host)
        {
            _host = host;
            _gameManager = new GameManager();

            _host.GameStartReceived += OnGameStartReceived;
        }

        private void OnGameStartReceived()
        {
            if (!CanStartGame())
                return;

            _isInGame = true;
            _gameManager.StartGame(_playerModels, _suspectSelections);
        }

        private readonly List<IBackendPlayerNetworkModel> _playerModels = new();
        private bool _isInGame;
        private readonly GameManager _gameManager;
        private readonly Dictionary<IBackendPlayerNetworkModel, SUSPECT> _suspectSelections = new();

        private bool CanStartGame()
        {
            return _playerModels.Count == _suspectSelections.Count;
        }

        public bool CanAddPlayers => !_isInGame && _playerModels.Count < Board.MAX_NUM_PLAYERS;

        public void AddPlayer(IBackendPlayerNetworkModel playerNetworkModel)
        {
            _playerModels.Add(playerNetworkModel);
            playerNetworkModel.SuspectSelectionReceived += update => OnSuspectSelection(playerNetworkModel, update);
            playerNetworkModel.TurnEndReceived += () => OnPlayerEndsTurn(playerNetworkModel);
            playerNetworkModel.PlayerSuggestionReceived += update => OnPlayerSuggestion(playerNetworkModel, update);
            playerNetworkModel.PlayerSuggestionResponseReceived +=
                update => OnPlayerSuggestionResponse(playerNetworkModel, update);
        }

        private void OnPlayerSuggestionResponse(IBackendPlayerNetworkModel playerNetworkModel, PlayerSuggestionResponse update)
        {
            _playerAwaitingSuggestionResponse!.SendPlayerSuggestionResponse(update);
            if (update.HasResponse)
            {
                _playerAwaitingSuggestionResponse = null;
                _suggestion = null;
            }
            else
            {
                var nextPlayer = GetNextPlayer(playerNetworkModel);
                if (nextPlayer == _playerAwaitingSuggestionResponse)
                {
                    _playerAwaitingSuggestionResponse = null;
                    _suggestion = null;
                    return;
                }

                nextPlayer.PromptResponseToSuggestion(_suggestion!);
            }
        }

        private IBackendPlayerNetworkModel? _playerAwaitingSuggestionResponse;
        private PlayerSuggestion? _suggestion;

        private IBackendPlayerNetworkModel GetNextPlayer(IBackendPlayerNetworkModel current) =>
            _playerModels[(_playerModels.IndexOf(current) + 1) % _playerModels.Count];

        private void OnPlayerSuggestion(IBackendPlayerNetworkModel playerNetworkModel, PlayerSuggestion suggestion)
        {
            // First broadcast suggestion to all players
            foreach (var client in _playerModels)
            {
                client.SendPlayerSuggestion(suggestion);
            }

            // Then query player by player until there is a response, or all players have been asked
            // Send first query
            GetNextPlayer(playerNetworkModel).PromptResponseToSuggestion(suggestion);
            _playerAwaitingSuggestionResponse = playerNetworkModel;
            _suggestion = suggestion;
        }

        private SUSPECT _currentTurnSuspect = SUSPECT.MISS_SCARLET;

        private void OnPlayerEndsTurn(IBackendPlayerNetworkModel playerNetworkModel)
        {
            if (_suspectSelections[playerNetworkModel] != _currentTurnSuspect)
                return;

            var suspect = _gameManager.GetNextTurn();
            _currentTurnSuspect = suspect;
            foreach (var client in _playerModels)
            {
                client.SendNewTurn(
                    new NewTurnMessage
                    {
                        IsMyTurn = suspect == _suspectSelections[client],
                        NewTurnPlayer = suspect
                    }
                    );
            }
        }

        private void OnSuspectSelection(
            IBackendPlayerNetworkModel player,
            SuspectSelectionUpdate suspectSelectionUpdate)
        {
            _suspectSelections[player] = suspectSelectionUpdate.SuspectSelected;
            foreach (var playerNetworkModel in _playerModels)
                playerNetworkModel.SendSuspectSelectionUpdate(suspectSelectionUpdate.GetWithPlayerName(player.Name));
        }

        public List<IBackendPlayerNetworkModel> GetPlayerModels()
        {
            return _playerModels;
        }

        public void Dispose()
        {
            _host.GameStartReceived -= OnGameStartReceived;
        }
    }
}