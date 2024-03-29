﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluelessNetwork.TransmittedTypes;

namespace CluelessBackend.Core
{
    public class Suspect
    {
        int suspectID_;
        string suspectName_;
        string suspectColor_ = "";
        SUSPECT suspectType_;
        public Suspect()
        {

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

        public void SetSuspectType(SUSPECT suspectEnum)
        {
            suspectType_ = suspectEnum;
        }

        public SUSPECT GetSuspectType()
        {
            return suspectType_;
        }
    }
}
