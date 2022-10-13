//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;

namespace LepiX.Core.RandomGenerators
{
    class PollenAirGenerator
    {
        //------------------------------------------------------------
        // Parameters-Fields 
        //------------------------------------------------------------
        double dL;
        double dU;

        double depositionSlope;
        double quantileValue;


        double distance;

        // The mean value of the pollen-air-deposition can be calcluated via:
        // https://en.wikipedia.org/wiki/Log-normal_distribution
        // Only /mu and /sigma parameters are required.

        double meanAirValue;




        private void ObtainParameters(ModelParameters modelParameters)
        {
            dL = modelParameters.DepositionSampled.LowerQuantileIntercept;
            dU = modelParameters.DepositionSampled.UpperQuantileIntercept;

            depositionSlope = modelParameters.DepositionSampled.DepositionSlope;
            quantileValue = modelParameters.DepositionSampled.QuantileValue;
        }

        LogNormalRandom logNormalRandom;

        // Constructor
        public PollenAirGenerator(ModelParameters modelParameters, int seed, double distance)
        {
            ObtainParameters(modelParameters);

            this.distance = distance;

            logNormalRandom = new LogNormalRandom(seed, CalculateMu(distance), CalculateSigma());

            this.meanAirValue = GetMedian(distance);

        }


        public double GetNextAirAmount()
        {
            return logNormalRandom.NextDouble();
        }

        public double GetMedianAirAmount()
        {
            return meanAirValue;
        }

        public double GetUpper95Amount()
        {
            return Math.Pow(10, dU) * Math.Pow(distance, depositionSlope);
        }

        public double GetQ80Amount()
        {
            double mu = CalculateMu(distance);
            double sigma = CalculateSigma();
            
            //This value could be solved for any p here, we need ErfC function for that however.
            double pst = 0.841621;
            return Math.Exp(mu + pst *sigma);
        }

        private double CalculateMu(double distance)
        {
            double mu = (double)-1 / 2 * Math.Log(Math.Pow(10, -dL - dU) * Math.Pow(distance, -2 * depositionSlope));
            return mu;
        }

        private double CalculateSigma()
        {
            double sigma = Math.Log(Math.Pow(10, dL) * Math.Sqrt(Math.Pow(10, -dL - dU))) / quantileValue;
            return Math.Abs(sigma);
        }
        
        // The Median of a log normal distribution corresponds to Exp(mu), with 
        // mu is the mu parameter of the distribution.
        private double GetMedian(double distance)
        {
            double mu = CalculateMu(distance);
            return Math.Exp(mu);
        }
    }
}
