using System;
using System.Collections.Generic;
using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;

namespace CluelessNetwork.BackendNetworkInterfaces
{
    public interface IGameInstance
    {
        /// <summary>
        /// Indicates if players can be added to this game instance
        /// </summary>
        bool CanAddPlayers { get; }
        
        /// <summary>
        /// Adds a player to the game instance
        /// </summary>
        /// <param name="playerNetworkModel">The network model for the player</param>
        void AddPlayer(IBackendPlayerNetworkModel playerNetworkModel);

        /// <summary>
        /// Get player models from a game instance
        /// </summary>
        /// <returns>A list of player models</returns>
        List<IBackendPlayerNetworkModel> GetPlayerModels();
    }

    /// <summary>
    /// A service that manages game instances and the players associated with them
    /// </summary>
    public interface IGameInstanceService
    {
        public event Action<(IGameInstance, IBackendPlayerNetworkModel)>? PlayerAdded;

        /// <summary>
        /// Adds a player to a game instance. If there are no game instances accepting players (either none exist, or
        /// all are already in game), does nothing.
        /// TODO: Disconnect the player if they cannot join a game
        /// </summary>
        /// <param name="playerNetworkModel">The player to be added to a game instance</param>
        void AddPlayerToGameInstance(IBackendPlayerNetworkModel playerNetworkModel);

        /// <summary>
        /// Creates a game instance with no players. Note that the host must also join their own game with
        /// AddPlayerToGameInstance
        /// </summary>
        /// <param name="hostPlayerNetworkModel">The connection for the host</param>
        void CreateGameInstance(IBackendPlayerNetworkModel hostPlayerNetworkModel);

        /// <summary>
        /// Gets all game instances
        /// </summary>
        /// <returns>A list of game instances</returns>
        public List<IGameInstance> GetAllGameInstances();

        /// <summary>
        /// Gets a specific player's game instance
        /// </summary>
        /// <param name="playerNetworkModel"></param>
        /// <returns></returns>
        IGameInstance GetGameInstanceFromPlayer(IBackendPlayerNetworkModel playerNetworkModel);
    }
}