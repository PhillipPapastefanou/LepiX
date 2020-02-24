using System;

namespace LepiX.Core.RandomGenerators
{
    class PollenLeafGenerator
    {
        //------------------------------------------------------------
        // Parameters 
        //------------------------------------------------------------
        double logSigma;
        //------------------------------------------------------------
        // References
        //------------------------------------------------------------
        NormalRandom normalRandom;

        // Constructor
        public PollenLeafGenerator(ModelParameters modelParameters, int seed)
        {
            this.logSigma = modelParameters.DepositionSampled.LogSigmaPlant;
            normalRandom = new NormalRandom(seed);
        }

        public double GetNextPollenAmountOnLeaf(double logMu)
        {
            return Math.Exp(normalRandom.NextDouble() * logSigma + logMu);
        }

        public double GetMedianAmountOnLeaf(double logMu)
        {
            return Math.Exp(logMu);
        }
    }
}
