namespace CluelessNetwork.TransmittedTypes
{
    public class MoveAction
    {
        public int LocationID { get; init; }

        public MoveAction GePlayerPosition()
        {
            return new MoveAction()
            {
                LocationID = LocationID
            };
        }
    }
}