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
    public class PlayerTests
    {

        [Fact]
        public void TestPlayer()
        {
            Player p1 = new Player(Suspect.SUSPECT.MISS_SCARLET);
            p1.SetIsActive(true);
            // a newly created player should have actions to perform 
            Assert.True(p1.HasActions().Equals(true), "player has no actions when they should...");
            




        }

    }
}
