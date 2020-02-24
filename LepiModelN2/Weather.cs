//================================================================
// LepiModel: weather class
// Lorenz Fahse, Phillip Papastefanou, 20-11-2013
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
        // GetFunctions
        //------------------------------------------------------------
        public double[] RandomYearWeather() { return randomYearWeather; }
        public int WeatherID() { return weatherID; }


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
