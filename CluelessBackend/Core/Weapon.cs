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
        public int weaponID_ { get; set; }
        public WEAPON weaponType { get; set; }
        public Weapon(int weaponID, WEAPON weaponType)
        {
            this.weaponID_ = weaponID;
            this.weaponType = weaponType;
        }

        public Weapon(WEAPON weaponType)
        {
            this.weaponType = weaponType;
        }
    }
}
