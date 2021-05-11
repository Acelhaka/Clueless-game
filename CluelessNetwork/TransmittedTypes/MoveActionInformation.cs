using System.Collections.Generic;

namespace CluelessNetwork.TransmittedTypes
{
    public enum Movement
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public class MoveActionInformation
    {
        // When adding information to be transmitted, use public get/init properties to store the information
        // Do not add a constructor that takes arguments, as this is a serialized class
        List<Movement> Movements { get; init; }
    }
}