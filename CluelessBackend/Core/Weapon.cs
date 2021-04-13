using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    public class Weapon
    {
        /// <summary>
        /// Weapon enum, made of 6 different weapon types 
        /// </summary>
        public enum WEAPON
        {
            ROPE = 0,
            LEAD_PIPE,
            KNIFE,
            WRENCH,
            CANDLESTICK,
            REVOLVER
        }

        int weaponID_;
        WEAPON weaponType_;
        string weaponName_;

        public string weaponName { get; set; }
        public int weaponID { get; set; }
        public int weaponType { get; set; }

        public Weapon(int weaponID, string weaponType)
        {
            weaponID_ = weaponID;
            weaponName_ = weaponType;
        }

        public Weapon(WEAPON weaponType)
        {
            weaponType_ = weaponType;
        }
    }
}
