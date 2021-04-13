using System;
using System.Collections.Generic;
using System.Linq;
using CluelessBackend.Core;
using CluelessNetwork.BackendNetworkInterfaces;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;

namespace CluelessBackend
{
    public class GameInstance : IGameInstance, IDisposable
    {
        private readonly IBackendPlayerNetworkModel _host;

        public GameInstance(IBackendPlayerNetworkModel host)
        {
            _host = host;
            _gameManager = new GameManager();

            _host.GameStartReceived += _gameManager.StartGame;
        }
        
        private readonly List<IBackendPlayerNetworkModel> _playerModels = new();
        private bool _isInGame = false;
        private GameManager _gameManager;
        public bool CanAddPlayers => !_isInGame && _playerModels.Count < Board.MAX_NUM_PLAYERS;
        public void AddPlayer(IBackendPlayerNetworkModel playerNetworkModel)
        {
            _playerModels.Add(playerNetworkModel);
        }

        public List<IBackendPlayerNetworkModel> GetPlayerModels()
        {
            return _playerModels;
        }

        public void Dispose()
        {
            _host.GameStartReceived -= _gameManager.StartGame;
        }
    }
    public class GameInstanceService : IGameInstanceService
    {
        // TODO: Make actual games, rather than lists of players
        private readonly List<IGameInstance> _gameInstances = new();
        public event Action<(IGameInstance, IBackendPlayerNetworkModel)>? PlayerAdded;

        public void AddPlayerToGameInstance(IBackendPlayerNetworkModel playerNetworkModel)
        {
            //TODO: Allow player to query/specify available games
            var latestGameAcceptingPlayers = _gameInstances.LastOrDefault(x => x.CanAddPlayers);
            if (latestGameAcceptingPlayers == null)
            {
                playerNetworkModel.Disconnect();
                return;
            }

            latestGameAcceptingPlayers.AddPlayer(playerNetworkModel);
            PlayerAdded?.Invoke((latestGameAcceptingPlayers, playerNetworkModel));
        }

        public void CreateGameInstance(IBackendPlayerNetworkModel hostPlayerNetworkModel)
        {
            _gameInstances.Add(new GameInstance(hostPlayerNetworkModel));
        }

        public List<IGameInstance> GetAllGameInstances()
        {
            return _gameInstances;
        }

        public IGameInstance GetGameInstanceFromPlayer(IBackendPlayerNetworkModel playerNetworkModel)
        {
            return _gameInstances.First(x => x.GetPlayerModels().Contains(playerNetworkModel));
        }
    }
}