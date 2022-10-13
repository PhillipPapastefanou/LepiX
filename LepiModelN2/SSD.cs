//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;

namespace LepiX.Core
{
    class SSD
    {
        double[] LDvals;
        double generalSlope;
  
        //------------------------------------------------------------
        //  Constructor
        //------------------------------------------------------------
        public SSD(double[] lDvals, double generalSlope)
        {
            this.LDvals = lDvals;
            this.generalSlope = generalSlope;
        }


        //------------------------------------------------------------
        //  Methods
        //------------------------------------------------------------
        public double DoseResponse(double pollenAmount, int i)
        {
            return 1 / (1 + Math.Pow(pollenAmount / LDvals[i], -generalSlope));
        }
    }
}
