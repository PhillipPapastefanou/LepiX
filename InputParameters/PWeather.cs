using System;

namespace LepiX.InputParameters
{
    //--------------------------------------------------------------
    //  Weather Parameters
    //--------------------------------------------------------------
    public class PWeather : Methods
    {
        private string weatherFolder;
        private string inputFileNameW;
        private bool hasHeadersW;
        private bool isReadOnlyW;
        private char delimiterW;

        #region Properties
        public string WeatherFolder
        {
            get { return weatherFolder; }
            set { weatherFolder = value; }
        }
        public string InputFileNameW
        {
            get { return inputFileNameW; }
            set { inputFileNameW = value; }
        }
        [Unit(Unit.Units.Boolean)]
        public bool HasHeadersW
        {
            get { return hasHeadersW; }
            set { hasHeadersW = value; }
        }
        [Unit(Unit.Units.Boolean)]
        public bool IsReadOnlyW
        {
            get { return isReadOnlyW; }
            set { isReadOnlyW = value; }
        }
        public char DelimiterW
        {
            get { return delimiterW; }
            set { delimiterW = value; }
        }

        #endregion

        public override void SetValues()
        {
            weatherFolder = "WeatherData";
            inputFileNameW = "BadHersfeld.csv";
            hasHeadersW = true;
            isReadOnlyW = true;
            delimiterW = ',';
        }
    }

}
