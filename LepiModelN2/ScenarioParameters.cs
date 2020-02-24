using System;
using System.Reflection;

namespace LepiX.Core
{
    class ScenarioParameters
    {
        private double distanceToMaizeField;
        private int expositionDays;
        private int scenarioSeed;

        public double DistanceToMaizeField
        {
            get { return distanceToMaizeField; }
            set { distanceToMaizeField = value; }
        }
        public int ExpositionDays
        {
            get { return expositionDays; }
            set { expositionDays = value; }
        }
        public int ScenarioSeed
        {
            get { return scenarioSeed; }
            set { scenarioSeed = value; }
        }       
        public ScenarioParameters(double distanceToMaizeField, int expositionDays, int scenarioSeed)
        {
            this.distanceToMaizeField = distanceToMaizeField;
            this.expositionDays = expositionDays;
            this.scenarioSeed = scenarioSeed;
        }
    }
}
