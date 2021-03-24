namespace CluelessNetwork.TransmittedTypes
{
    /// <summary>
    /// Represents a chat message
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// The chat message content
        /// </summary>
        public string Content { get; init; } = string.Empty;
        
        /// <summary>
        /// The chat sender name. Does not need to be set by the client. Set by the server when broadcasting.
        /// </summary>
        public string? SenderName { get; set; } = default;
    }
}