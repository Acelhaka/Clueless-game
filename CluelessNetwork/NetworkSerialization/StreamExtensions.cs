using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace CluelessNetwork.NetworkSerialization
{
	/// <summary>
	/// Contains custom logic for streams
	/// </summary>
	public static class StreamExtensions
    {
	    /// <summary>
	    /// Attempts to read a serialized object from the stream, convert it, and return it
	    /// </summary>
	    /// <param name="stream">The stream to read from</param>
	    /// <typeparam name="T">The type of object to deserialize</typeparam>
	    /// <returns>An instance of the object, or null if it could not be created</returns>
	    public static T? ReadObject<T>(this Stream stream) where T : class
        {
            // TODO: Handle errors
            // Because only DeserializeAsync has the signature that allows a stream, get a Task<T?> and then wait for it to complete. When it completes, return the result.
            var sizeBuffer = new byte[sizeof(int)];
            // Get length of incoming object
            var bytesRead = 0;
            while (bytesRead < sizeBuffer.Length)
                bytesRead += stream.Read(sizeBuffer, bytesRead, sizeBuffer.Length - bytesRead);

            // Create a buffer for serialized data
            var serializationLength = BitConverter.ToInt32(sizeBuffer);
            var serializedDataBuffer = new byte[serializationLength];
            // Get data from the stream
            bytesRead = 0;
            while (bytesRead < serializationLength)
                bytesRead += stream.Read(serializedDataBuffer, bytesRead, serializationLength - bytesRead);
            // Deserialize data into an object
            var json = Encoding.UTF8.GetString(serializedDataBuffer);

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
	    public static void WriteObject<T>(this Stream stream, T value)
        {
            // TODO: Handle errors
            var json = JsonSerializer.Serialize(value);
            var bytes = Encoding.UTF8.GetBytes(json);
            // Always send a number of bytes ahead, so the other side knows how much to read
            stream.Write(BitConverter.GetBytes(bytes.Length));
            // Send serialized data
            stream.Write(bytes);
        }
    }
}