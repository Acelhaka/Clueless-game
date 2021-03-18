namespace CluelessNetwork
{
    /// <summary>
    /// Updates are differentiated based on an UpdateType value sent inside the NetworkTransmittedUpdate wrapper
    /// </summary>
    public enum UpdateType
    {
        PlayerOptionsUpdate,
        MoveAction,
        PlayerSuggestion,
        PlayerSuggestionResponse,
        Accusation,
        AccusationResult,
        SuspectSelection,
        GameStart
    }

    /// <summary>
    /// Packages updates to be able to be dynamically handled after being received and deserialized
    /// </summary>
    public class NetworkTransmittedUpdate
    {
        /// <summary>
        /// The type of update that is being sent
        /// </summary>
        public UpdateType UpdateType { get; init; }
        /// <summary>
        /// Any information that accompanies the update. Each update type has an associated UpdateObject type, which is
        /// usually but not always the same for both frontend and backend. UpdateObject may be null if only the event
        /// happening must be conveyed.
        /// </summary>
        public object? UpdateObject { get; init; }
    }
}