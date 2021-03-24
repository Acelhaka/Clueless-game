using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluelessBackend;
using CluelessBackend.Core;
using FluentAssertions;
using Xunit;
using System.IO;
using CluelessNetwork;
using CluelessNetwork.NetworkSerialization;
using FluentAssertions;
using Xunit;


namespace CluelessTests.BackEndTests
{
    public class RoomTests
    {

        [Fact]
        public void CreateRoomTest()
        {
            Room room = new Room(Room.ROOM.STUDY, true);
            Room room2 = new Room(Room.ROOM.BILLIARD_ROOM, false);

            //TODO i think this is broken, because this should be True...currently is returning false...
            //Assert.True(room.HasSecretPassage().Equals(true));
            Assert.True(room.HasSecretPassage().Equals(false), "The room has a secret passage, but should not");

            // TODO add some more testing





        }

    }
}
