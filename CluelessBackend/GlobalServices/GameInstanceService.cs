using System;
using System.Collections.Generic;
using System.Linq;
using CluelessNetwork.BackendNetworkInterfaces;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;

namespace CluelessBackend
{
    public class GameInstanceService : IGameInstanceService
    {
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