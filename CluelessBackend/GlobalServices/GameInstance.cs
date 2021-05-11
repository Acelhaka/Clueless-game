using System;
using System.Collections.Generic;
using System.Text.Json;
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

        private readonly Dictionary<IBackendPlayerNetworkModel, int> _moveSelection = new();
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
            playerNetworkModel.MoveActionReceived += update => OnMoveActionSelection(playerNetworkModel, update);
        }

        private void OnPlayerEndsTurn(IBackendPlayerNetworkModel playerNetworkModel)
        {
            var suspect = _gameManager.GetNextTurn();
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

        private void OnMoveActionSelection(
       IBackendPlayerNetworkModel player,
       MoveActionInformation moveActionUpdate)
        {
            _moveSelection[player] = moveActionUpdate.LocationID;
            foreach (var playerNetworkModel in _playerModels)
                playerNetworkModel.SendMoveActionInformation(moveActionUpdate.GePlayerPosition());

          // _gameManager.MovePlayerToRoom(player, roomID);
      
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