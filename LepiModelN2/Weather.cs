//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================

namespace LepiX.Core
{
    class Weather:Component
    {
        //------------------------------------------------------------
        // variables
        //------------------------------------------------------------
        int numberOfCols;
        int numberOfRows;
        double[,] weatherData;

        int weatherID;
        double[] randomYearWeather;

        //------------------------------------------------------------
        // Properties
        //------------------------------------------------------------
        public double[] RandomYearWeather {get{ return randomYearWeather; }}
        public int WeatherID {get{ return weatherID; }}


        //------------------------------------------------------------
        // Constructor 
        //------------------------------------------------------------

        public Weather(int pID):base(pID)
        {
            this.pID = pID;
        }

        public override void Initialise(Scenario scenario)
        {
            this.modelParameters = scenario.ModelParameters;
            this.inputData = scenario.InputData;
            this.generator = scenario.Generator;
        }

        public override void Input()
        {
            numberOfCols = inputData.weatherDataCols;
            numberOfRows = inputData.weatherDataRows;
            weatherData = inputData.weatherData;
        }

        public override void Update()
        {
            weatherID = generator.UniformRandom.Next(1, numberOfCols);

            randomYearWeather = new double[numberOfRows];

            for (int i = 0; i < numberOfRows; i++)
            {
                randomYearWeather[i] = weatherData[i, weatherID];
            }
        }

        public override void Output()
        {

        }

        public override void ConnectTo(Component component)
        {
        
        }

        public override void Dispose()
        {
            this.modelParameters = null;
            this.inputData = null;
            this.generator = null;

            this.weatherData = null;
            this.randomYearWeather = null;
        }

        public override void Finalise()
        {
            
        }
    }
}
