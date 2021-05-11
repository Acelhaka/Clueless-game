namespace CluelessNetwork.TransmittedTypes
{
    public class MoveAction
    {
        // Does not need to be set by the client. Set by the server.
        public SUSPECT? Suspect { get; init; }
        public int RoomNumber { get; init; }

        public MoveAction WithSuspect(SUSPECT suspectSelection)
        {
            return new MoveAction()
            {
                RoomNumber = RoomNumber,
                Suspect = suspectSelection
            };
        }
    }
}