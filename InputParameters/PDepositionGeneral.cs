using System;

namespace LepiX.InputParameters
{
    //--------------------------------------------------------------
    //  DEPOSITION - General -  Parameters
    //--------------------------------------------------------------
    public class PDepositionGeneral : Methods
    {
        public enum SheddingTypes
        { Sampled }

        private double[] distancesToMaizeField; 
        private SheddingTypes sheddingType;
        private string depositionFolder;
        private string inputFileNameD;
        private bool hasHeadersD;
        private bool isReadOnlyD;
        private char delimiterD;
        private int sheddingOffsetStartDay;
        private int sheddingOffsetStartMonth;
        private int sheddingOffsetEndDay;
        private int sheddingOffsetEndMonth;
        private double lossRate;
        private double pollenAirToLeafFactor;


        #region Properties
        public double[] DistancesToMaizeField
        {
            get { return distancesToMaizeField; }
            set { distancesToMaizeField = value; }
        }
        public SheddingTypes SheddingType
        {
            get { return sheddingType; }
            set { sheddingType = value; }
        }
        public string DepositionFolder
        {
            get { return depositionFolder; }
            set { depositionFolder = value; }
        }
        public string InputFileNameD
        {
            get { return inputFileNameD; }
            set { inputFileNameD = value; }
        }
        [Unit(Unit.Units.Boolean)]
        public bool HasHeadersD
        {
            get { return hasHeadersD; }
            set { hasHeadersD = value; }
        }
        [Unit(Unit.Units.Boolean)]
        public bool IsReadOnlyD
        {
            get { return isReadOnlyD; }
            set { isReadOnlyD = value; }
        }
        public char DelimiterD
        {
            get { return delimiterD; }
            set { delimiterD = value; }
        }
        public int SheddingOffsetStartDay
        {
            get { return sheddingOffsetStartDay; }
            set { sheddingOffsetStartDay = value; }
        }
        public int SheddingOffSetStartMonth
        {
            get { return sheddingOffsetStartMonth; }
            set { sheddingOffsetStartMonth = value; }
        }
        public int SheddingOffSetEndDay
        {
            get { return sheddingOffsetEndDay; }
            set { sheddingOffsetEndDay = value; }
        }
        public int SheddingOffsetEndMonth
        {
            get { return sheddingOffsetEndMonth; }
            set { sheddingOffsetEndMonth = value; }
        }
        public double LossRate
        {
            get { return lossRate; }
            set { lossRate = value; }
        }
        public double PollenAirToLeafFactor
        {
            get { return pollenAirToLeafFactor; }
            set { pollenAirToLeafFactor = value; }
        }
        #endregion

        public override void SetValues()
        {
            sheddingType = SheddingTypes.Sampled;
            depositionFolder = "DepositionData";
            inputFileNameD = "PollenDistributionData.csv";
            hasHeadersD = true;
            isReadOnlyD = true;
            delimiterD = ',';
            sheddingOffsetStartDay = 14;
            sheddingOffsetStartMonth = 7;
            sheddingOffsetEndDay = 5;
            sheddingOffsetEndMonth = 8;
            lossRate = 0.2;
            pollenAirToLeafFactor = 1;
        }

        public void CalculateValues()
        {

        }


    }
}
