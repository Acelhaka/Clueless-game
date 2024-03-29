﻿using System;
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
using CluelessNetwork.TransmittedTypes;


namespace CluelessTests.BackEndTests
{
    public class TurnManagerTests
    {

        [Fact]
        public void TestAllPlayersActive()
        {
            Player p1 = new Player(SUSPECT.MISS_SCARLET);
            Player p2 = new Player(SUSPECT.COLONEL_MUSTARD);
            Player p3 = new Player(SUSPECT.MRS_PEACOCK);
            Player p4 = new Player(SUSPECT.MRS_WHITE);
            Player p5 = new Player(SUSPECT.MR_GREEN);
            Player p6 = new Player(SUSPECT.PROFESSOR_PLUM);

            List<Player> players = new List<Player>();
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);
            players.Add(p5);
            players.Add(p6);

            // TODO i feel like active field should be true by default when a player is created....thoughts?
            p1.SetIsActive(true);
            p2.SetIsActive(true);
            p3.SetIsActive(true);
            p4.SetIsActive(true);
            p5.SetIsActive(true);
            p6.SetIsActive(true);

            TurnManager turnManager = new TurnManager(players);

            Player currentPlayer = turnManager.CurrentTurn();

            Assert.True(currentPlayer.Equals(p1));
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p2));
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p3));
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p4));
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p5));
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p6));

            // this should loop back to the begining of the players
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p1));
        }

        [Fact]
        public void TestSomePlayersActive()
        {
            Player p1 = new Player(SUSPECT.MISS_SCARLET);
            Player p2 = new Player(SUSPECT.COLONEL_MUSTARD);
            Player p3 = new Player(SUSPECT.MRS_PEACOCK);
            Player p4 = new Player(SUSPECT.MRS_WHITE);
            Player p5 = new Player(SUSPECT.MR_GREEN);
            Player p6 = new Player(SUSPECT.PROFESSOR_PLUM);

            List<Player> players = new List<Player>();
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

            Assert.True(currentPlayer.Equals(p2), "should be p2's turn, but is not");
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p3), "should be p3's turn, but is not");

            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p6), "should be p6's turn, but is not");

            // now set the p2 to inactive so we "skip" their turn
            p2.SetIsActive(false);
            turnManager.NextTurn();
            currentPlayer = turnManager.CurrentTurn();
            Assert.True(currentPlayer.Equals(p3), "should be p3's turn, but is not");

        }

        [Fact]
        public void TestNoPlayersActive()
        {
            // this would be a case where all players made a wrong accusation and the game ends
            Player p1 = new Player(SUSPECT.MISS_SCARLET);
            Player p2 = new Player(SUSPECT.COLONEL_MUSTARD);
            Player p3 = new Player(SUSPECT.MRS_PEACOCK);
            Player p4 = new Player(SUSPECT.MRS_WHITE);
            Player p5 = new Player(SUSPECT.MR_GREEN);
            Player p6 = new Player(SUSPECT.PROFESSOR_PLUM);

            List<Player> players = new List<Player>();
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);
            players.Add(p5);
            players.Add(p6);

            p1.SetIsActive(false);
            p2.SetIsActive(false);
            p3.SetIsActive(false);
            p4.SetIsActive(false);
            p5.SetIsActive(false);
            p6.SetIsActive(false);

            TurnManager turnManager = new TurnManager(players);

            Player currentPlayer = turnManager.CurrentTurn();

            Assert.True(currentPlayer == null);
        }
    }
}
