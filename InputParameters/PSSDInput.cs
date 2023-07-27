//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;

namespace LepiX.InputParameters
{
    public class PSSDInput: Methods
    {
        private bool calculateSSD;
        private bool useSSDslope;        
        private int numberOfSensitivityClasses;
        private double lowerQuantileValue;
        private double upperQuantileValue;
        private Species[] species;

        [Unit(Unit.Units.Boolean)]
        public bool CalculateSSD
        {
            get { return calculateSSD; }
            set { calculateSSD = value; }
        }
        [Unit(Unit.Units.Boolean)]
        public bool UseSSDSlope
        {
            get { return useSSDslope; }
            set { useSSDslope = value; }
        }
        public int NumberOfSensitivityClasses
        {
            get { return numberOfSensitivityClasses; }
            private set { numberOfSensitivityClasses = value; }
        }
        public double LowerQuantileValue
        {
            get { return lowerQuantileValue; }
            set { lowerQuantileValue = value; }
        }
        public double UpperQuantileValue
        {
            get { return upperQuantileValue; }
            set { upperQuantileValue = value; }
        }
        public Species[] SSDSpecies
        {
            get { return species; }
            set { species = value; }
        }


        
        public override void SetValues()
        {
            this.calculateSSD = false;
        }



    }

    public class Species
    {
        private double ld50;
        private string name;

        public double LD50
        {
            get { return ld50; }
            set { ld50 = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


    }
}
