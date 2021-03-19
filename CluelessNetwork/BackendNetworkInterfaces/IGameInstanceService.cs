using CluelessNetwork.BackendNetworkInterfaces.BackendPlayerNetworkModel;

namespace CluelessNetwork.BackendNetworkInterfaces
{
    /// <summary>
    /// A service that manages game instances and the players associated with them
    /// </summary>
    public interface IGameInstanceService
    {
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
    }
}