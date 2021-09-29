//================================================================
// LepiModel: Population Algorithm
// Lorenz Fahse, Phillip Papastefanou, 21-10-2013
//================================================================

namespace LepiX.Core
{
    using LepiX.Core.RandomGenerators;
    class Population:Component
    {

        //------------------------------------------------------------
        // Component references 
        //------------------------------------------------------------

        Weather weather;

        //------------------------------------------------------------
        // variables
        //------------------------------------------------------------ 
        double[] weatherData;
        int earliestLarvalOccurenceInt;
        Individual[] individuals;


        //------------------------------------------------------------
        // Properties
        //------------------------------------------------------------
        public double[] WeatherData
        { get{ return weatherData;}}
        public int EarliestLarvalOccurenceInt
        { get { return earliestLarvalOccurenceInt; } }
        public double DDLarva
        { get { return modelParameters.IndividualGeneral.DayDegreesLarva; } }
        public double DDPupae
        { get { return modelParameters.IndividualGeneral.DayDegreesPupae; } }
        public double TH
        { get { return modelParameters.IndividualGeneral.TemperatureThreshold; } }
        public Generator Generator
        { get { return generator; } }

        //------------------------------------------------------------
        // GetFunctions
        //------------------------------------------------------------
        public Individual[] Individuals() { return individuals; }


        //------------------------------------------------------------
        // Model constructor 
        //------------------------------------------------------------
        public Population(int pID):base(pID)
        {
            this.pID = pID;
        }

        public override void Initialise(Scenario scenario)
        {
            this.modelParameters = scenario.ModelParameters;
            this.generator = scenario.Generator;

            earliestLarvalOccurenceInt = modelParameters.IndividualGeneralAdd.EarliestLarvalOccurenceInt;

            individuals = new Individual[modelParameters.General.NumberOfIndividuals];

        }

        public override void Input()
        {
            weatherData = weather.RandomYearWeather;
        }

        public override void Update()
        {
            for (int i = 0; i < modelParameters.General.NumberOfIndividuals; i++)
            {
                individuals[i] = new Individual(this, i);
            }
        }

        public override void Output()
        {
           
        }

        public override void ConnectTo(Component component)
        {
            if (component is Weather)
                this.weather = component as Weather;
        }

        public override void Dispose()
        {
            weatherData = null;
            individuals = null;   
        }

        public override void Finalise()
        {
           
        }
    }
}
