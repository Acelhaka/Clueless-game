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



namespace CluelessTests.BackEndTests
{
    public class WeaponTests
    {

        [Fact]
        public void TestWeaponSetters()
        {
            // load the weapon with values
            Weapon weapon = new Weapon(Weapon.WEAPON.KNIFE);

            // change the values via the setters
            weapon.weaponName = "Pipe";
            weapon.weaponID = 1;

            // test these new values were indeed updated
            Assert.True(weapon.weaponType.Equals("Pipe"), "expected weapon name to be Pipe, but is not");
            Assert.True(weapon.weaponID.Equals(1), "expected weapon id to be 1, but is not");

        }

        [Fact]
        public void TestWeaponGetters()
        {
            // load the weapon with values
            Weapon weapon = new Weapon(Weapon.WEAPON.KNIFE);

            // test the getters work as expected
            Assert.True(weapon.weaponName.Equals("Knife"));
            Assert.True(weapon.weaponID.Equals(0));

        }

    }
}
