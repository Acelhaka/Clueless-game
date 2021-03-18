using System;

namespace CluelessNetwork.TransmittedTypes
{
    public class PlayerOptionCollection : IEquatable<PlayerOptionCollection>
    {
        // When adding information to be transmitted, use public get/init properties to store the information
        // Do not add a constructor that takes arguments, as this is a serialized class

        // This is just an example of how to contain information to send over the network. Feel free add/remove
        // properties, or chane the type.
        public string[] AvailableOptions { get; init; } = null!;

        public bool Equals(PlayerOptionCollection? other)
        {
            return other != null && other.AvailableOptions.Equals(AvailableOptions);
        }

        public override int GetHashCode()
        {
            return AvailableOptions.GetHashCode();
        }
    }
}