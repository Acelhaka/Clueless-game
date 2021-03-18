using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
        public IEnumerable<Task> ListenForUpdatesContinuously()
        {
            // TODO: Figure out how to stop listening
            {
                while (true) yield return ReceiveUpdate();
            }
            // ReSharper disable once IteratorNeverReturns
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
                    UpdateType = updateType
                });
        }

        /// <summary>
        /// Receives an update and handles it
        /// </summary>
        /// <returns>A task that completes when handling finishes</returns>
        public Task ReceiveUpdate()
        {
            var update = _tcpStream?.ReadObject<NetworkTransmittedUpdate>();
            // Handle update
            if (update != null)
                return Task.Run(() => HandleUpdateReceived(update));

            // The update could not be read, so no action is taken
            return Task.CompletedTask;
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