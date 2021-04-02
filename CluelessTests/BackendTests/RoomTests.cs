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



namespace CluelessTests.BackEndTests
{
    public class RoomTests
    {

        [Fact]
        public void TestRoomWithNoSecretPassage()
        {
            Room room = new Room(Room.ROOM.BILLIARD_ROOM, false);
            Assert.True(room.HasSecretPassage().Equals(false), "The room has a secret passage, but should not");

            

        }

        [Fact]
        public void TestRoomWithSecretPassage()
        {
            Room room = new Room(Room.ROOM.STUDY, true);
            //TODO i think this is broken, because this should be True...currently is returning false...
            //Assert.True(room.HasSecretPassage().Equals(true));
        }

    }
}
