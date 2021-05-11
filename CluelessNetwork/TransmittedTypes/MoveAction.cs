namespace CluelessNetwork.TransmittedTypes
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class MoveAction
    {
        public Direction Direction { get; init; }
    }
}