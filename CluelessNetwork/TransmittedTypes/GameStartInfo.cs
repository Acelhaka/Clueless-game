using System.Collections.Generic;

namespace CluelessNetwork.TransmittedTypes
{
    /// <summary>
    /// Includes list of other players and cards that the receiving player gets
    /// </summary>
    public class GameStartInfo
    {
        // When adding information to be transmitted, use public get/init properties to store the information
        // Do not add a constructor that takes arguments, as this is a serialized class
        public List<(int, int)> RoomWeaponMap { get; init; }
        public List<int> Cards { get; init; }
        public bool GoesFirst { get; init; }
    }
}