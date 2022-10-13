//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;

namespace LepiX.Core
{
    class Individual
    {
        //------------------------------------------------------------
        // Population reference
        //------------------------------------------------------------
        Population population;
        //------------------------------------------------------------
        // Variables
        //------------------------------------------------------------
 
        int pID;
        int iD;
        int birthTime;  
        int larvalTime;
        int pupalTime;

        //------------------------------------------------------------
        // Properties
        //------------------------------------------------------------
        public int PID
        { get { return pID; } }
        public int ID
        { get { return iD; } }
        public int BirthTime
        { get { return birthTime; } }
        public int LarvalTime
        { get { return larvalTime; } }
        public int PupalTime
        { get { return pupalTime; } }

        //============================================================
        // Model constructor 
        //============================================================ 

        public Individual(Population population, int iD)
        {
            this.population = population;
            this.pID = population.PID;
            this.iD = iD;


            this.birthTime = CalculateBirthTime(this.iD);                                            // Calculate the hatch 
            this.larvalTime = LarvalTimeCalculation(this.birthTime);                                // Calculate the larval time
            this.pupalTime = PupalTimeCalculation(this.birthTime, this.larvalTime);
        }

        //------------------------------------------------------------
        // Methods
        //------------------------------------------------------------
        private int CalculateBirthTime(int x)
        {
            return (population.Generator.BirthTimeGenerator.GetNextBirthday() + population.EarliestLarvalOccurenceInt) % 365; // This avoids birthtimes greater than the 364. day.
        }
        
        private int LarvalTimeCalculation(int birthTime)
        {
            double tempSum = 0;
            int time = birthTime;

            while (tempSum < population.DDLarva)
            {
                if (population.WeatherData[time % 365] > population.TH)
                {
                    tempSum = tempSum + population.WeatherData[time % 365] - population.TH;
                }

                time++;
                
                //New temporarel stuff
                //if (population.WeatherData[time % 365] < 0)
                //{
                //    break;
                //}
            }
            return (time - birthTime);
        }

        private int PupalTimeCalculation(int birthTime, int larvalTime)
        {
            double tempSum = 0;
            int time = birthTime + larvalTime;

            double dayDegree = population.DDPupae;

            while (tempSum < dayDegree)
            {
                if (population.WeatherData[time % 365] > population.TH)
                {
                    tempSum = tempSum + population.WeatherData[time % 365] - population.TH;
                }

                time++;
            }

            return (time - birthTime - larvalTime);
        }





    }
}