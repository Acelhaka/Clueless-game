namespace CluelessNetwork
{
    /// <summary>
    /// Connection information sent from front end clients to server upon connecting
    /// Serialized as JSON and sent over the network
    /// </summary>
    public class InitialConnectionInfo
    {
        public bool IsHost { get; init; }

        public override bool Equals(object? obj)
        {
            return obj is InitialConnectionInfo connectionInfo && connectionInfo.IsHost == IsHost;
        }

        public override int GetHashCode()
        {
            return IsHost.GetHashCode();
        }
    }
}