using System;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CluelessNetwork.NetworkSerialization
{
	public static class TaskExtensions
	{
		public static T WaitForResult<T>(this Task<T> task) where T : class
		{
			task.Wait();
			return task.Result;
		}
	}

	/// <summary>
	/// Contains custom logic for streams
	/// </summary>
	public static class WebSocketExtensions
    {
	    /// <summary>
	    /// Attempts to read a serialized object from the stream, convert it, and return it
	    /// </summary>
	    /// <param name="stream">The stream to read from</param>
	    /// <typeparam name="T">The type of object to deserialize</typeparam>
	    /// <returns>An instance of the object, or null if it could not be created</returns>
	    public static T? ReadObject<T>(this WebSocket webSocket) where T : class
        {
            // Deserialize data into an object
            var buffer = new byte[1024 * 1024];
            var json = webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).WaitForResult().ToString();

            // Deserialize the wrapped type correctly using reflection
            var deserializedObject = JsonSerializer.Deserialize<T>(json);
            if (deserializedObject is NetworkTransmittedUpdate {UpdateObject: { }} dynamicUpdateWrapper)
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
	    /// Serializes and writes an object to a stream
	    /// </summary>
	    /// <param name="stream">The stream to write UTF-8 data to</param>
	    /// <param name="value">An object to serialize and write</param>
	    /// <typeparam name="T">A serializable type</typeparam>
	    public static void WriteObject<T>(this WebSocket webSocket, T value)
	    {
		    var json = JsonSerializer.Serialize(value);
		    var bytes = Encoding.UTF8.GetBytes(json);
		    webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
	    }
    }
}