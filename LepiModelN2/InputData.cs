//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
using System.IO;

namespace LepiX.Core
{
    class InputData
    {
        ModelParameters inputParameters;

        //------------------------------------------------------------
        // variables
        //------------------------------------------------------------
        public double[,] weatherData;
        public int weatherDataCols;
        public int weatherDataRows;

        public double[,] pollenDistributionData;
        public int pollenDistributionCols;
        public int pollenDistributionRows;

        //------------------------------------------------------------
        // Constructor
        //------------------------------------------------------------

        public InputData(ModelParameters modelParameters)
        {
            
            this.inputParameters = modelParameters;

            SetUpWeatherData();
            SetUpDepositionData();

            CheckWeatherData();
            CheckDepositionData();
        }

        //------------------------------------------------------------
        // Methods
        //------------------------------------------------------------
        private void SetUpWeatherData()
        {
            weatherData = CSV.Import("Input//" + inputParameters.General.InputLocation + "//" + inputParameters.Weather.WeatherFolder + "//" + inputParameters.Weather.InputFileNameW, inputParameters.Weather.HasHeadersW, inputParameters.Weather.IsReadOnlyW, inputParameters.Weather.DelimiterW);
            weatherDataCols = CSV.ColCount();
            weatherDataRows = CSV.RowCount();
        } 

        private void CheckWeatherData()
        {

            //Iterate over all colums except the first.
            for (int j = 1; j < weatherDataCols; j++)
            {
                //Iterate over all days
                double dayDegreeSum = 0;
                for (int i = 0; i < weatherDataRows; i++)
                {
                    if (weatherData[i, j] - inputParameters.IndividualGeneral.TemperatureThreshold> 0)
                    {
                        dayDegreeSum = weatherData[i, j] - inputParameters.IndividualGeneral.TemperatureThreshold + dayDegreeSum;
                    }

                }

                if (dayDegreeSum < (inputParameters.IndividualGeneral.DayDegreesLarva +inputParameters.IndividualGeneral.DayDegreesPupae))
                {
                    throw new DayDegreesTooLessException((int)dayDegreeSum);
                }
            }
        }


        private void SetUpDepositionData()
        {
            pollenDistributionData = CSV.Import("Input/" + inputParameters.General.InputLocation + "/" + inputParameters.DepositionGeneral.DepositionFolder + "/" + inputParameters.DepositionGeneral.InputFileNameD, inputParameters.DepositionGeneral.HasHeadersD, inputParameters.DepositionGeneral.IsReadOnlyD, inputParameters.DepositionGeneral.DelimiterD);
            pollenDistributionCols = CSV.ColCount();
            pollenDistributionRows = CSV.RowCount();
        }

        private void CheckDepositionData()
        {
            for (int j = 1; j < pollenDistributionCols; j++)
            {
                double sum = 0;
                for (int i = 0; i < pollenDistributionRows; i++)
                {
                    sum = sum + pollenDistributionData[i, j];
                }

                if (sum < 0.99 || sum > 1.01)
                {
                    throw new RelativePollenAmountTooLessException(sum);
                }

            }
        }



    }
}
