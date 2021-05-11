using System.Collections.Generic;

namespace CluelessNetwork.TransmittedTypes
{

    public class MoveActionInformation
    {
        public int LocationID { get; init; }

        public MoveActionInformation GePlayerPosition()
        {
            return new MoveActionInformation()
            {
                LocationID = LocationID
            };
        }
    }
}