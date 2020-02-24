using System;

namespace LepiX.InputParameters
{
    public class PIndividualGeneral : Methods
    {
        private int earliestLarvalOccurrenceDay;
        private int earliestLarvalOccurrenceMonth;
        private double dayDegreesLarva;
        private double dayDegreesPupae;
        private double temperatureThreshold;

        #region Properties
        [Unit(Unit.Units.Days)]
        public int EarliestLarvalOccurrenceDay
        {
            get { return earliestLarvalOccurrenceDay; }
            set { earliestLarvalOccurrenceDay = value; }
        }
        [Unit(Unit.Units.Month)]
        public int EarliestLarvalOccurrenceMonth
        {
            get { return earliestLarvalOccurrenceMonth; }
            set { earliestLarvalOccurrenceMonth = value; }
        }
        [Unit(Unit.Units.DegreeCelcius)]
        public double DayDegreesLarva
        {
            get { return dayDegreesLarva; }
            set { dayDegreesLarva = value; }
        }

        [Unit(Unit.Units.DegreeCelcius)]
        public double DayDegreesPupae
        {
            get { return dayDegreesPupae; }
            set { dayDegreesPupae = value; }
        }
        [Unit(Unit.Units.DegreeCelcius)]
        public double TemperatureThreshold
        {
            get { return temperatureThreshold; }
            set { temperatureThreshold = value; }
        }
        #endregion
        public override void SetValues()
        {
            earliestLarvalOccurrenceDay = 14;
            earliestLarvalOccurrenceMonth = 7;
            dayDegreesLarva = 315.2;
            temperatureThreshold = 5.1;
        }





    }
}
