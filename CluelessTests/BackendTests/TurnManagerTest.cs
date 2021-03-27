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
    public class TurnManagerTests
    {

        [Fact]
        public void CreateTurnManagerTest()
        {
            Player p1 = new Player(Suspect.SUSPECT.MISS_SCARLET);
            Player p2 = new Player(Suspect.SUSPECT.COLONEL_MUSTARD);
            Player p3 = new Player(Suspect.SUSPECT.MRS_PEACOCK);
            Player p4 = new Player(Suspect.SUSPECT.MRS_WHITE);
            Player p5 = new Player(Suspect.SUSPECT.MR_GREEN);
            Player p6 = new Player(Suspect.SUSPECT.PREOFESSOR_PLUM);

            

            List <Player> players = new List<Player>();
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);
            players.Add(p5);
            players.Add(p6);

            p1.SetIsActive(false);
            p2.SetIsActive(true);
            p3.SetIsActive(true);
            p4.SetIsActive(false);
            p5.SetIsActive(false);
            p6.SetIsActive(true);

            TurnManager turnManager = new TurnManager(players);

            Player currentPlayer = turnManager.CurrentTurn();

            Assert.True(currentPlayer.Equals(p2));
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p3));

            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p6));

            // now set the p2 to inactive so we "skip" their turn
            p2.SetIsActive(false);
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p3));

            // TODO add some more testing





        }

    }
}
