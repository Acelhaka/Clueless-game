namespace CluelessNetwork.TransmittedTypes
{
    public enum SUSPECT
    {
        COLONEL_MUSTARD,
        MISS_SCARLET,
        PROFESSOR_PLUM,
        MR_GREEN,
        MRS_WHITE,
        MRS_PEACOCK
    }
    
    public class SuspectSelectionUpdate
    {
        // When adding information to be transmitted, use public get/init properties to store the information
        // Do not add a constructor that takes arguments, as this is a serialized class
        public string? PlayerName { get; init; } = default;
        
        public SUSPECT SuspectSelected { get; init; }

        public SuspectSelectionUpdate GetWithPlayerName(string playerName)
        {
            return new SuspectSelectionUpdate()
            {
                PlayerName = playerName,
                SuspectSelected = SuspectSelected
            };
        }
    }
}