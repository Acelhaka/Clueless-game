namespace CluelessNetwork.TransmittedTypes
{
    public class NewTurnMessage
    {
        public SUSPECT NewTurnPlayer { get; init; }
        public bool IsMyTurn { get; init; }
    }
}