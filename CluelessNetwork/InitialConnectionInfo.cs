namespace CluelessNetwork
{
    /// <summary>
    /// Connection information sent from front end clients to server upon connecting
    /// Serialized as JSON and sent over the network
    /// </summary>
    public class InitialConnectionInfo
    {
        public bool IsHost { get; init; }
        public string? Name { get; init; }

        public override bool Equals(object? obj)
        {
            return obj is InitialConnectionInfo other && (other.IsHost, Name).Equals((IsHost, Name));
        }

        public override int GetHashCode()
        {
            return (IsHost, Name).GetHashCode();
        }
    }
}