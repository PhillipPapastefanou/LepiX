using System;


namespace LepiX.InputParameters
{
    class Unit : Attribute
    {
        public enum Units { Days, Month, Boolean, DegreeCelcius, PollenPerSquareCentimeter }

        private Units u;

        public Units U
        {
            get { return u; }
        }

        public Unit(Units name)
        {
            this.u = name;
        }
    }
}
