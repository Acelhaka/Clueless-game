using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessBackend.Core
{
    class Suspect
    {
        int suspectID_;
        string suspectName_;
        string suspectColor_ = "";
        SUSPECT suspectType_;
        public enum SUSPECT
        {
            COLONEL_MUSTARD,
            MISSS_SCARLET,
            PREOFESSOR_PLUM,
            MR_GREEN,
            MRS_WHITE,
            MRS_PEACOCK
        }

        public Suspect(int suspectID, string suspectName, string suspectColor)
        {
            suspectID_ = suspectID;
            suspectName_ = suspectName;
            suspectColor_ = suspectColor;
        }

        public Suspect(SUSPECT suspectEnum)
        {
            suspectType_ = suspectEnum;
        }
    }
}
