using System;


namespace LepiX.Core.RandomGenerators
{
    class BirthtimeGenerator
    {
        #region ModelParameter
        //------------------------------------------------------------
        // Model Paramters
        //------------------------------------------------------------

        ModelParameters modelParameters;

        bool calculateBeta;
        double alpha;
        double beta;
        int x;
        double sigma;
        int maxBirthSpan;


        private void ReadParameters()
        {
            calculateBeta = modelParameters.IndividualBetaBirthtime.CalculateBeta;
            alpha = modelParameters.IndividualBetaBirthtime.Alpha;
            beta = modelParameters.IndividualBetaBirthtime.Beta;
            x = modelParameters.IndividualBetaBirthtime.X;
            sigma = modelParameters.IndividualBetaBirthtime.Sigma;
            maxBirthSpan = modelParameters.IndividualBetaBirthtime.MaxBirthRange;
        }

        #endregion UserParamer

        //------------------------------------------------------------
        // Beta-random-number-generator
        //------------------------------------------------------------
        BetaRandom betaRandom;

        //------------------------------------------------------------
        // Constructor
        //------------------------------------------------------------
        public BirthtimeGenerator(ModelParameters modelParameters, int seedA, int seedB)
        {
            this.modelParameters = modelParameters;

            ReadParameters();

            CalculateAandB();

            betaRandom = new BetaRandom(seedA, seedB, alpha, beta);

        }

        //------------------------------------------------------------
        // Methods
        //------------------------------------------------------------
        public int GetNextBirthday()
        {
            return (int)Math.Round(betaRandom.NextDouble() * maxBirthSpan);
        }

        private void CalculateAandB()
        {
            GetParameterValues();
        }


        private void GetParameterValues()
        {
            if (calculateBeta) // Calculate lambda with Bisection and CDF
            {
                double precision = 6e-15;
                int nMax = 100;
                double a = 0.1;
                double b = 100;

                beta = AuxiliaryMethods.BiSection(SetBetaRoot, precision, nMax, a, b);
            }

            else // Take beta out of the file
            {

            }

        }

        private double CDF(double alpha, double beta, double x)
        {
            return AuxiliaryFunctions.incompletebeta(alpha, beta, x);
        }

        private double SetBetaRoot(double beta)
        {
            return CDF(alpha, beta, (double)x / maxBirthSpan) - sigma;
        }

    }
}
