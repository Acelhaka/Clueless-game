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

        public Suspect(int suspectID, string suspectName, string suspectColor)
        {
            suspectID_ = suspectID;
            suspectName_ = suspectName;
            suspectColor_ = suspectColor;
        }

    }
}
