using System;
using System.Text.Json;

namespace CluelessNetwork.Websockets
{
    public abstract class WebsocketWrapperBase : IWebsocket
    {
        /// <summary>
        /// Attempts to read a serialized object from the websocket, convert it, and return it
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize</typeparam>
        /// <returns>An instance of the object, or null if it could not be created</returns>
        public T? ReadObject<T>() where T : class
        {
            var json = RecvString();
            if (json == null)
                return null;

            // Deserialize the wrapped type correctly using reflection
            var deserializedObject = JsonSerializer.Deserialize<T>(json);
            if (deserializedObject is NetworkTransmittedUpdate { UpdateObject: { } } dynamicUpdateWrapper)
            {
                // In this block, we know we are wrapping data. The deserializer doesn't choose the write type by default, so we need to create a new
                var innerData = dynamicUpdateWrapper.UpdateObject!.ToString();
                var innerDataType = Type.GetType(dynamicUpdateWrapper.UpdateObjectType);
                return new NetworkTransmittedUpdate
                {
                    UpdateType = dynamicUpdateWrapper.UpdateType,
                    UpdateObjectType = dynamicUpdateWrapper.UpdateObjectType,
                    UpdateObject = JsonSerializer.Deserialize(innerData ?? string.Empty, innerDataType!)
                } as T;
            }

            return deserializedObject;
        }

        /// <summary>
        /// Serializes and sends an object to a websocket
        /// </summary>
        /// <param name="value">An object to serialize and write</param>
        /// <typeparam name="T">A serializable type</typeparam>
        public void WriteObject<T>(T value)
        {
            var json = JsonSerializer.Serialize(value);
            SendString(json);
        }

        protected abstract void SendString(string json);
        protected abstract string? RecvString();
        public abstract void Dispose();
    }
}