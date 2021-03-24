using System;
using System.Collections.Generic;
using System.Linq;
using CluelessNetwork.BackendNetworkInterfaces;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;

namespace CluelessBackend
{
    public class GameInstanceService : IGameInstanceService
    {
        // TODO: Make actual games, rather than lists of players
        private readonly List<List<IBackendPlayerNetworkModel>> _gameInstances = new();
        public event Action<IBackendPlayerNetworkModel>? PlayerAdded;

        public void AddPlayerToGameInstance(IBackendPlayerNetworkModel playerNetworkModel)
        {
            // TODO: Handle no games hosted
            _gameInstances.Last()!.Add(playerNetworkModel);
            PlayerAdded?.Invoke(playerNetworkModel);
        }

        public void CreateGameInstance(IBackendPlayerNetworkModel hostPlayerNetworkModel)
        {
            _gameInstances.Add(new List<IBackendPlayerNetworkModel>());
        }

        public List<List<IBackendPlayerNetworkModel>> GetAllGameInstances()
        {
            return _gameInstances;
        }
    }
}