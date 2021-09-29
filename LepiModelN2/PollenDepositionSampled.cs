//================================================================
// LepiModel: Sampled pollen distribution class
// Lorenz Fahse, Phillip Papastefanou, 20-11-2013
//================================================================
using System;
using System.Diagnostics.Eventing.Reader;

namespace LepiX.Core
{
    class PollenDepositionSampled:Component
    {

        //------------------------------------------------------------
        // variables
        //------------------------------------------------------------

        int numberOfCols;
        int numberOfRows;
        double[,] distributionData;

        int dayMin;
        int dayMax;

        int offsetID;
        double totalAirAmount;
        int distributionID;

        double airToLeafFactor;
        double lossRate;

        double[] randomPollenDeposition;

        bool getAmountByUpper95;
        bool getAmountBy80;


        bool includeVariabilityOfAirPollen;

        //------------------------------------------------------------
        // Properties
        //------------------------------------------------------------
        public double[] RandomYearDeposition{get { return randomPollenDeposition; }}
        public int OffsetID {get{ return offsetID; }}
        public double TotalAirAmount{get { return totalAirAmount; }}
        public int DistributionID {get { return distributionID; }}




        public PollenDepositionSampled(int pID): base(pID)
        {
            this.pID = pID;
        }



        public override void Initialise(Scenario scenario)
        {
            this.modelParameters = scenario.ModelParameters;
            this.scenarioParameters = scenario.ScenarioParameters;

            this.inputData = scenario.InputData;
            this.generator= scenario.Generator;
        }

        public override void Input()
        {
            this.distributionData = inputData.pollenDistributionData;
            this.numberOfRows = inputData.pollenDistributionRows;
            this.numberOfCols = inputData.pollenDistributionCols;

            this.dayMin = modelParameters.PollenDepositionAdd.SheddingOffsetStartInt;
            this.dayMax = modelParameters.PollenDepositionAdd.SheddingOffsetEndInt;

            this.lossRate = modelParameters.DepositionGeneral.LossRate;
            this.airToLeafFactor = modelParameters.DepositionGeneral.PollenAirToLeafFactor;

            this.getAmountByUpper95 = modelParameters.PollenDepositionAdd.GetAmountByUpper95;
            this.getAmountBy80 = modelParameters.PollenDepositionAdd.GetAmountBy80;

            this.includeVariabilityOfAirPollen = modelParameters.DepositionSampled.IncludeVarOnDistances;
        }

        public override void Update()
        {
            this.offsetID = generator.UniformRandom.Next(dayMin, dayMax);

            this.totalAirAmount = GetTotalAirAmount();

            this.distributionID = generator.UniformRandom.Next(1, numberOfCols);

            randomPollenDeposition = new double[365];

            // Set depositions to zero
            for (int i = 0; i < 365; i++)
            {
                randomPollenDeposition[i] = 0;
            }

            // Calculate pollen deposition for the pollen shedding
            for (int i = 0; i < numberOfRows; i++)
            {
                randomPollenDeposition[i + offsetID] = randomPollenDeposition[i + offsetID - 1] * (1 - lossRate) + distributionData[i, distributionID] * totalAirAmount*airToLeafFactor; 
            }

            // Calculate the pollen deposition after the pollen shedding
            // This is acually follows an exponential decay 
            for (int i = numberOfRows + offsetID; i < 365; i++)
            {
                randomPollenDeposition[i] = randomPollenDeposition[i - 1] * (1 - lossRate);
            }
 
        }

        public override void Output()
        {
            // Empty
        }

        public override void ConnectTo(Component component)
        {
            // Empty
        }      

        public override void Dispose()
        {
            modelParameters = null;
            inputData = null;
            generator = null;

            distributionData = null;
            randomPollenDeposition = null;
        }

        public override void Finalise()
        {

        }


        private double GetTotalAirAmount()
        {
            double pollenAmount;

            // Checking if its Worst-Case-Scenario! 
            // If yes, we take the amount just by the upper 95 value
            if (getAmountByUpper95)
            {
                pollenAmount = generator.PollenAirGenerator.GetUpper95Amount();
            }

            else if (getAmountBy80)
            {
                pollenAmount = generator.PollenAirGenerator.GetQ80Amount();
            }

            else
            {
                if (includeVariabilityOfAirPollen)
                {
                    pollenAmount = generator.PollenAirGenerator.GetNextAirAmount();
                }

                else
                {
                    pollenAmount = generator.PollenAirGenerator.GetMedianAirAmount();
                }
            }

            return pollenAmount;
        }
    }
}
