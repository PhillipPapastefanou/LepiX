//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace LepiX.Core
{
    using LepiX.Core.RandomGenerators;
    class Scenario
    {        
        //------------------------------------------------------------
        // The three base scenario references
        //------------------------------------------------------------        
        ModelParameters modelParameters;
        InputData inputData;
        Generator generator;
        ScenarioParameters scenarioParameters;

        //------------------------------------------------------------
        // variables
        //-----------------------------------------------------------
        Weather[]                 weather;
        PollenDepositionSampled[] pollenDepo;
        Population[]              population;
        Mortality[]               mortality;
        Statistics[]              statistics;

        List<RUN> RUNs;

        //export data
        double[,] popAvgData;
        double[,] medianMortData;
        int[,] totalIndividualData;
        double[,] additionalData;
        double[] totalSheddingData;


        //------------------------------------------------------------
        // properties 
        //------------------------------------------------------------
        public ScenarioParameters ScenarioParameters
        { get { return scenarioParameters; }}
        public ModelParameters ModelParameters
        { get { return modelParameters; }}
        public InputData InputData
        { get { return inputData; }}
        public Generator Generator
        { get { return generator; }}

        //------------------------------------------------------------
        // Constructor 
        //------------------------------------------------------------
        public Scenario(ModelParameters modelParameters, ScenarioParameters scenarioParameters)
        {
            this.modelParameters = modelParameters;
            this.scenarioParameters = scenarioParameters;

            this.inputData = new InputData(this.modelParameters);
            this.generator = new Generator(this.modelParameters,this.scenarioParameters);

            int numberOfRuns = modelParameters.General.NumberOfPopulations;

            weather = new Weather[numberOfRuns];
            pollenDepo = new PollenDepositionSampled[numberOfRuns];
            population = new Population[numberOfRuns];
            mortality = new Mortality[numberOfRuns];
            statistics = new Statistics[numberOfRuns];

            for (int i = 0; i < numberOfRuns; i++)
            {
                weather[i] = new Weather(i);
                pollenDepo[i] = new PollenDepositionSampled(i);
                population[i] = new Population(i);
                mortality[i] = new Mortality(i);
                statistics[i] = new Statistics(i);
            }


            RUNs = new List<RUN>();


            for (int i = 0; i < numberOfRuns; i++)
            {
                RUNs.Add(new RUN(i));
            } 
        }

        //------------------------------------------------------------
        // Initialse Method 
        //------------------------------------------------------------
        public void Initialise()
        {
            for (int i = 0; i < modelParameters.General.NumberOfPopulations; i++)
            {
                population[i].ConnectTo(weather[i]);

                mortality[i].ConnectTo(pollenDepo[i]);
                mortality[i].ConnectTo(population[i]);

                statistics[i].ConnectTo(weather[i]);
                statistics[i].ConnectTo(pollenDepo[i]);
                statistics[i].ConnectTo(population[i]);
                statistics[i].ConnectTo(mortality[i]);
            }

            for (int i = 0; i < modelParameters.General.NumberOfPopulations; i++)
            {
                RUNs[i].Components[0] = weather[i];
                RUNs[i].Components[1] = pollenDepo[i];
                RUNs[i].Components[2] = population[i];
                RUNs[i].Components[3] = mortality[i];
                RUNs[i].Components[4] = statistics[i];
            }
               
            foreach (RUN RUN in RUNs)
            {
                RUN.Initialise(this);
            }
        }

        //------------------------------------------------------------
        // Simulate Method 
        //------------------------------------------------------------
        public void Simulate()
        { 
            foreach (RUN run in RUNs)
            {
                run.Simulate();
            } 
        }


        //------------------------------------------------------------
        // Finalise Method 
        //------------------------------------------------------------
        public void Finalise()
        {
            totalIndividualData = new int[365, 3];
            popAvgData = new double[modelParameters.General.NumberOfPopulations, statistics[0].PopAvgData.Length +1 ];
            medianMortData = new double[modelParameters.General.NumberOfPopulations, statistics[0].MedianMortData.Length + 1];
            additionalData = new double[modelParameters.General.NumberOfPopulations, statistics[0].AdditionalPopulationData.Length + 1];
            totalSheddingData = new double[365];

            for (int i = 0; i < modelParameters.General.NumberOfPopulations; i++)
            {
                //Getting popavgData
                for (int j = 1; j < statistics[i].PopAvgData.Length + 1; j++)
                {
                    popAvgData[i, 0] = i;
                    popAvgData[i, j] = statistics[i].PopAvgData[j-1];
                }

                //Getting medianMortData
                for (int j = 1; j < statistics[i].MedianMortData.Length + 1; j++)
                {
                    medianMortData[i, 0] = i;
                    medianMortData[i, j] = statistics[i].MedianMortData[j-1];
                }

                //Getting AdditionalPopulationData
                for (int j = 1; j < statistics[i].AdditionalPopulationData.Length + 1; j++)
                {
                    additionalData[i, 0] = i;
                    additionalData[i, j] = statistics[i].AdditionalPopulationData[j-1];
                }

                //Getting Individual overalData
                for (int j = 0; j < 365; j++)
                {
                    totalIndividualData[j, 0] = totalIndividualData[j, 0] + statistics[i].IndividualOverall[j, 0];
                    totalIndividualData[j, 1] = totalIndividualData[j, 1] + statistics[i].IndividualOverall[j, 1];
                    totalIndividualData[j, 2] = totalIndividualData[j, 2] + statistics[i].IndividualOverall[j, 2];
                }

                //Getting total shedding Data
                for (int j = 0; j < 365; j++)
                {
                    totalSheddingData[j] = totalSheddingData[j] + statistics[i].TotalSheddingData[j];
                }
            }


            //Calculate the mean of the shedding data over all populations.
            for (int j = 0; j < 365; j++)
            {
                totalSheddingData[j] = totalSheddingData[j] / modelParameters.General.NumberOfPopulations;
            }
        }


        //------------------------------------------------------------
        // export method
        //------------------------------------------------------------

        public void Export()
        {
            string distanceToMaizeFieldString = Convert.ToString(scenarioParameters.DistanceToMaizeField, System.Globalization.CultureInfo.InvariantCulture);
            string exposureThresholdDaysString = Convert.ToString(scenarioParameters.ExpositionDays, System.Globalization.CultureInfo.InvariantCulture);


            // folder setup
            string outputLocation = modelParameters.General.OutputLocation;
            string distanceToMaizeFieldStringFolder = String.Format("d = {0} m ", distanceToMaizeFieldString);
            string exposureThresholdDaysStringFolder = String.Format("InSeries = {0} days ", exposureThresholdDaysString);

            Directory.CreateDirectory("Output/" + outputLocation + "/" + distanceToMaizeFieldStringFolder);
            Directory.CreateDirectory("Output/" + outputLocation + "/" + distanceToMaizeFieldStringFolder + "/" + exposureThresholdDaysStringFolder);

            List<string> headerP = new List<string>() { "Population-ID", "BirthTime", "LarvalTime", "Exposition Amount" };
            CSV.ExportArray(popAvgData, headerP, "Output" + "/" + outputLocation + "/" + distanceToMaizeFieldStringFolder + "/" + exposureThresholdDaysStringFolder + "/" + "AveragePopulationData.csv");

            List<string> headerM = new List<string>();
            headerM.Add("Population-ID");

            for (int i = 0; i < modelParameters.DoseResponseGeneral.LD50ClassValues.Length; i++)
			{
                headerM.Add("Sensitive Class "+ (i+1));
			}
            CSV.ExportArray(medianMortData, headerM, "Output" + "/" + outputLocation + "/" + distanceToMaizeFieldStringFolder + "/" + exposureThresholdDaysStringFolder + "/" + "AverageMortalityData.csv");

            List<string> headerT = new List<string>() { "BirthTime Count", "Current Larval Count", "Current Imagines Count" };
            CSV.ExportArray(totalIndividualData, headerT, "Output" + "/" + outputLocation + "/" + distanceToMaizeFieldStringFolder + "/" + exposureThresholdDaysStringFolder + "/" + "TotalLarvalData.csv");

            List<string> headerA = new List<string>() { "Population-ID", "Weather-ID", "PollenOffset", "PollenTotal", "PollenDistribution-ID" , "AveragePollenAmountPerIndividual"};
            CSV.ExportArray(additionalData, headerA, "Output" + "/" + outputLocation + "/" + distanceToMaizeFieldStringFolder + "/" + exposureThresholdDaysStringFolder + "/" + "AdditionalPopData.csv");

            List<string> headerS = new List<string>() { "Shedded Pollen (Pollen/cm^2)" };
            CSV.ExportArray(totalSheddingData, headerS, "Output" + "/" + outputLocation + "/" + distanceToMaizeFieldStringFolder + "/" + exposureThresholdDaysStringFolder + "/" + "TotalSheddingData.csv");


            List<string> headerTotalMortalityData = new List<string>() { "PID", "IID", };

            for (int i = 0; i < modelParameters.DoseResponseGeneral.LD50ClassValues.Length; i++)
            {
                headerTotalMortalityData.Add("Class" + i.ToString());
            }


            double[,] allMortData = new double[modelParameters.General.NumberOfPopulations * modelParameters.General.NumberOfIndividuals,
                modelParameters.DoseResponseGeneral.LD50ClassValues.Length + 2];

            for (int p = 0; p < modelParameters.General.NumberOfPopulations; p++)
            {
                Mortality mort = mortality[p];

                double[][] mortData = mort.MortalityPerPopulation;

                for (int i = 0; i < modelParameters.General.NumberOfIndividuals; i++)
                {
                    allMortData[p * modelParameters.General.NumberOfIndividuals + i, 0] = p;
                    allMortData[p * modelParameters.General.NumberOfIndividuals + i, 1] = i;

                    for (int j = 0; j < modelParameters.DoseResponseGeneral.LD50ClassValues.Length; j++)
                    {
                        allMortData[p * modelParameters.General.NumberOfIndividuals + i, j + 2] = mortData[i][j];
                    }
                }
                
            }

            CSV.ExportArrayBig(allMortData, headerTotalMortalityData, "Output" + "/" + outputLocation + "/" + distanceToMaizeFieldStringFolder + "/" + exposureThresholdDaysStringFolder + "/" + "FullMortalityOutput.csv");
           


        }

        public void Dispose()
        {
            foreach (RUN run in RUNs)
            {
                run.Dispose();
            }

            weather = null;
            pollenDepo = null;
            population = null;
            mortality = null;
            statistics = null;

            RUNs = null;



            Debug.WriteLine("Current RAM-usage: {0} mb", GC.GetTotalMemory(false) / 1024 / 1024);
        }


    }
}
