using System;
using System.IO;
using CluelessNetwork.NetworkSerialization;

namespace CluelessNetwork
{
    /// <summary>
    /// Base class for shared logic between a backend network player model and a frontend network player model
    /// </summary>
    public abstract class PlayerModelBase : IDisposable
    {
        protected Stream? _tcpStream;

        /// <summary>
        /// Runs when an update is received over the network
        /// </summary>
        /// <param name="updateWrapper"></param>
        protected abstract void HandleUpdateReceived(NetworkTransmittedUpdate updateWrapper);

        protected void Initialize(Stream tcpStream)
        {
            _tcpStream = tcpStream;
        }

        /// <summary>
        /// Listen continuously for updates over the network. Runs forever, but provides a task for each update handler invocation
        /// </summary>
        /// <returns>An enumeration for each handler</returns>
        public void ListenForUpdatesContinuously()
        {
            // TODO: Figure out how to stop listening
            {
                while (true) ReceiveUpdate();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        /// <summary>
        /// Send an update over the network. Wrapped in a type so that a handler can be selected by the recipient.
        /// </summary>
        /// <param name="updateObject">An object with information conveyed in the update</param>
        /// <param name="updateType">The update type</param>
        protected void PushUpdate(object? updateObject, UpdateType updateType)
        {
            _tcpStream?.WriteObject(
                new NetworkTransmittedUpdate
                {
                    UpdateObject = updateObject,
                    UpdateType = updateType,
                    UpdateObjectType = updateObject?.GetType().FullName ?? string.Empty
                });
        }

        /// <summary>
        /// Receives an update and handles it
        /// </summary>
        /// <returns>A task that completes when handling finishes</returns>
        public void ReceiveUpdate()
        {
            var update = _tcpStream?.ReadObject<NetworkTransmittedUpdate>();
            // Handle update
            if (update != null)
                HandleUpdateReceived(update);
        }

        /// <summary>
        /// Clean up resources used
        /// </summary>
        public virtual void Dispose()
        {
            _tcpStream?.Dispose();
        }
    }
}