using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class Weapon
    {
        public int weaponID_;
        public string weaponName_;
        public Weapon(int weaponID, string weaponName)
        {
            weaponID_ = weaponID;
            weaponName_ = weaponName;
        }
    }
}
