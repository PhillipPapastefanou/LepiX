//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;

namespace LepiX.InputParameters
{
    //--------------------------------------------------------------
    // DEPOSITION - Sampled - Parameters
    //--------------------------------------------------------------
    public class PDepositionSampled : Methods
    {
        private double depositionSlope;
        private double lowerQuantileIntercept;
        private double upperQuantileIntercept;
        private double quantileValue;
        private bool includeVarOnDistances;
        private double logSigmaPlant;
        private bool includeVarOnLeafs;
                
        #region Properties
        public double DepositionSlope
        {
            get { return depositionSlope; }
            set { depositionSlope = value; }
        }
        public double LowerQuantileIntercept
        {
            get { return lowerQuantileIntercept; }
            set { lowerQuantileIntercept = value; }
        }
        public double UpperQuantileIntercept
        {
            get { return upperQuantileIntercept; }
            set { upperQuantileIntercept = value; }
        }
        public double QuantileValue
        {
            get { return quantileValue; }
            set { quantileValue = value; }
        }
        public bool IncludeVarOnDistances
        {
            get { return includeVarOnDistances; }
            set { includeVarOnDistances = value; }
        }
        public double LogSigmaPlant
        {
            get { return logSigmaPlant; }
            set { logSigmaPlant = value; }
        }
        public bool IncludeVarOnLeafs
        {
            get { return includeVarOnLeafs; }
            set { includeVarOnLeafs = value; }
        }
        #endregion

        public PDepositionSampled()
        {
            // empty
        }

        public override void SetValues()
        {
            depositionSlope = -0.5471;
            lowerQuantileIntercept = 1.14529;
            upperQuantileIntercept = 3.0235;
            quantileValue = 1.95996;
            logSigmaPlant = 1;
            includeVarOnDistances = true;
            includeVarOnLeafs = true;
        }
    }
}
