//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
using System.Collections.Generic;

namespace LepiX.Core
{
    class Statistics: Component
    {
        //------------------------------------------------------------
        // Component references 
        //------------------------------------------------------------
        Weather weather;
        PollenDepositionSampled pollenDepositionSampled;
        Population population;
        Mortality mortality;

        //------------------------------------------------------------
        // variables
        //------------------------------------------------------------
        int weatherID;
        int offsetID;
        double totalAirAmountID;
        double totalIndividualAmount;
        int distributionID;
        Individual[] individuals;
        double[] totalEstimatedExpositionAmount;
        double[][] mortalityPerPopulation;

        double[] popAvgData;
        double[] medianMortData;
        int[,] individualOverall;
        double[] additionalPopulationData;

        double[] totalSheddingData;




        //------------------------------------------------------------
        // Properties
        //------------------------------------------------------------
        public double[] PopAvgData
        {get {return popAvgData;}} 
        public double[] MedianMortData
        {get{return medianMortData;}}
        public int[,] IndividualOverall
        {get {return individualOverall;}}
        public double[] AdditionalPopulationData
        { get { return additionalPopulationData;}}
        public double[] TotalSheddingData
        { get { return totalSheddingData;}}

        //------------------------------------------------------------
        // Model constructor 
        //------------------------------------------------------------
        public Statistics(int pID):base(pID)
        {
            this.pID = pID;
        }



        public override void Initialise(Scenario scenario)
        {
            this.modelParameters = scenario.ModelParameters;  
        }

        public override void Input()
        {
            weatherID = weather.WeatherID;

            offsetID = pollenDepositionSampled.OffsetID;
            totalAirAmountID = pollenDepositionSampled.TotalAirAmount;
            distributionID = pollenDepositionSampled.DistributionID;
            totalIndividualAmount = mortality.TotalExposedAmount / modelParameters.General.NumberOfIndividuals;

            individuals = population.Individuals();

            totalEstimatedExpositionAmount = mortality.TotalEstimatedExpositionAmount;
            mortalityPerPopulation = mortality.MortalityPerPopulation;

            totalSheddingData = pollenDepositionSampled.RandomYearDeposition;
        }

        public override void Update()
        {
            MeanPopulationIndividualData();

            MedianPopulationMortality();

            TotalIndividualStatistics();

            AdditionalPopData();
        }

        public override void Output()
        {
     
        }

        public override void ConnectTo(Component component)
        {
            if (component is Weather)
                this.weather = component as Weather;
            if (component is PollenDepositionSampled)
                this.pollenDepositionSampled = component as PollenDepositionSampled;
            if (component is Population)
                this.population = component as Population;
            if (component is Mortality)
                this.mortality = component as Mortality;
        }

        public override void Dispose()
        {
            individuals = null;
            totalEstimatedExpositionAmount = null;
            mortalityPerPopulation = null; 
        }

        public override void Finalise()
        {
            
        }
        //------------------------------------------------------------
        // Private Methods
        //------------------------------------------------------------

        // Average individual data for each population
        // avg. birthtime, larvaltime, pollen amount
        private void MeanPopulationIndividualData()
        {
            popAvgData = new double[3];  

            foreach (Individual individual in individuals)
            {
                popAvgData[0] = popAvgData[0] + individual.BirthTime;
                popAvgData[1] = popAvgData[1] + individual.LarvalTime;
                popAvgData[2] = popAvgData[2] + totalEstimatedExpositionAmount[individual.ID];
            }

            for (int i = 0; i < 3; i++)
            {
                popAvgData[i] = popAvgData[i] / individuals.Length;
            }
        }


        // Generate the Median of the mortality of the individuals for each class per population
        public void MedianPopulationMortality()
        {
            medianMortData = new double[modelParameters.DoseResponseGeneral.LD50ClassValues.Length];

            // Iterate through every sensitiviy class
            for (int i = 0; i < medianMortData.Length; i++)
            {

                // Arrange jagged array
                double[] data = new double[individuals.Length];
                for (int j = 0; j < data.Length; j++)
                {
                    data[j] = mortalityPerPopulation[j][i];
                }

                Array.Sort<double>(data);


                medianMortData[i] = Median(data); 
            }
        }

        // overall birthtime, overall larvalTime 
        private void TotalIndividualStatistics()
        {

            individualOverall = new int[365, 3];

            // overall individual BirthTime Data
            foreach (Individual individual in individuals)
            {
                individualOverall[individual.BirthTime, 0]++;
            }

            // overall living larva calculation
            foreach (Individual individual in individuals)
            {
                for (int i = 0; i < individual.LarvalTime; i++)
                {
                    individualOverall[(individual.BirthTime + i) % 365, 1]++;
                }
            }

            // overall living Imagines
            foreach (Individual individual in individuals)
            {
                for (int i = individual.BirthTime + individual.LarvalTime + individual.PupalTime; i < 365; i++)
                {
                    individualOverall[i, 2]++;
                }
            }

        }

        // Weather id, offset , pollenTotal, distribution
        private void AdditionalPopData()
        {
            additionalPopulationData = new double[5];

            additionalPopulationData[0] = weatherID;
            additionalPopulationData[1] = offsetID;
            additionalPopulationData[2] = totalAirAmountID;
            additionalPopulationData[3] = distributionID;
            additionalPopulationData[4] = totalIndividualAmount;
        }





        //------------------------------------------------------------
        // Auxiliary Methods
        //------------------------------------------------------------

        private double Median(int[] sortedArray)
        {
            double median;

            if (sortedArray.Length % 2 == 0)
            {
                int uS = sortedArray.Length / 2;
                int oS = sortedArray.Length / 2 + 1;

                median = (double)(sortedArray[uS - 1] + sortedArray[oS - 1]) / 2;
            }

            else
            {
                median = sortedArray[(sortedArray.Length + 1) / 2 - 1];
            }

            return median;
        }

        private double Median(double[] sortedArray)
        {
            double median;

            if (sortedArray.Length % 2 == 0)
            {
                int uS = sortedArray.Length / 2;
                int oS = sortedArray.Length / 2 + 1;

                median = (double)(sortedArray[uS - 1] + sortedArray[oS - 1]) / 2;
            }

            else
            {
                median = sortedArray[(sortedArray.Length + 1) / 2 - 1];
            }

            return median;
        }

    }

}
