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
    class LHS
    {
        Random random;
        
        //------------------------------------------------------------
        // variables
        //------------------------------------------------------------
        int scenarioCount;

        int globalSeed;


        int[][] combinationsIndex;

        //------------------------------------------------------------
        // LHS parameter arrays
        //------------------------------------------------------------
        double[] distanceArray;
        double[] thresholdDaysArray;

        //------------------------------------------------------------
        // GetFunctions
        //------------------------------------------------------------
        public int GetScenarioSeed() { return random.Next(); }
        public int GetScenarioCount() { return scenarioCount; }

        //------------------------------------------------------------
        // Constructor
        //------------------------------------------------------------
        public LHS(int globalSeed)
        {
            this.globalSeed = globalSeed;
            random = new Random(globalSeed);
        }

        //------------------------------------------------------------
        // Public Methods
        //------------------------------------------------------------

        public void Initialise()
        {
            LoadParamters();

            CalculateScenarioCount();
        }

        public void GenerateCombinations()
        {
            CalculateCombinations();
        }

        //------------------------------------------------------------
        // Private Methods
        //------------------------------------------------------------

        private void LoadParamters()
        {
            //Reading the distances and ThresholdDays  
            Console.WriteLine("Reading distances and threshlolddays...");
            distanceArray = CSV.ImportS("Distances.csv", true, true, ',');
            thresholdDaysArray = CSV.ImportS("ThresholdDays.csv", true, true, ',');   
        }
        private void CalculateScenarioCount()
        {
            scenarioCount = distanceArray.Length * thresholdDaysArray.Length;
        }

        private void CalculateCombinations()
        {
            combinationsIndex = new int[distanceArray.Length][];

            for (int i = 0; i < distanceArray.Length; i++)
            {
                combinationsIndex[i] = new int[thresholdDaysArray.Length];

                for (int j = 0; j < thresholdDaysArray.Length; j++)
                {
                    combinationsIndex[i][j] = j;
                }
            }
        }




        

    }
}
