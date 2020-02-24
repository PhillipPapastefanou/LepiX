using System;

namespace LepiX.InputParameters
{
    //--------------------------------------------------------------
    // Exposition - Parameters
    //--------------------------------------------------------------
    public class PExposition : Methods
    {
        public enum ExpositionTypes
        { MeanOverExposedDays, MaximumInSeriesDays }
        public enum ExposedTimes
        {One, Two}


        private ExpositionTypes expositionType;
        private int expositiondays;
        private ExposedTimes numberOfExposedIntervals;

        #region Properties
        public ExpositionTypes ExpositionType
        {
            get { return expositionType; }
            set { expositionType = value; }
        }
        public int ExpositionDays
        {
            get { return expositiondays; }
            set { expositiondays = value; }
        }

        public ExposedTimes NumberOfExposedIntervals
        {
            get { return numberOfExposedIntervals; }
            set { numberOfExposedIntervals = value; }
        }


        #endregion
        public override void SetValues()
        {
            expositionType = ExpositionTypes.MaximumInSeriesDays;
            expositiondays = 4;
            numberOfExposedIntervals = ExposedTimes.One;
        }
    }

}