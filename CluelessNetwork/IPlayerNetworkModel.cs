﻿namespace CluelessNetwork
{
    /// <summary>
    /// Interface for both frontend and backend player network models. For interface logic that is specific to either,
    /// see IBackendPlayerNetworkModel or IFrontendPlayerNetworkModel
    /// </summary>
    public interface IPlayerNetworkModel
    {
        /// <summary>
        /// Indicates if this player has selected to host a game. If false, the player must join an existing instance.
        /// </summary>
        public bool IsHost { get; }

        /// <summary>
        /// Runs forever, listening for updates from the network. When an update is received, a task which handles it is
        /// yielded.
        /// </summary>
        /// <returns>Tasks that handle messages from the network</returns>
        public void ListenForUpdatesContinuously();

        /// <summary>
        /// Receives an update and handles it
        /// This is primarily used in testing. For the game, use ListenForUpdatesContinuously
        /// </summary>
        /// <returns>A task that completes when handling finishes</returns>
        public void ReceiveUpdate();
    }
}