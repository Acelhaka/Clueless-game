using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class Weapon
    {
        public int weaponID_ { get; set; }
        public string weaponName_ { get; set; }
        public Weapon(int weaponID, string weaponName)
        {
            weaponID_ = weaponID;
            weaponName_ = weaponName;
        }
    }
}
