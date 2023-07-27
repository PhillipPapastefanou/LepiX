//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;

namespace LepiX.InputParameters
{
    //--------------------------------------------------------------
    // DoseResponse - Parameters
    //--------------------------------------------------------------
    public class PDoseResponseGeneral : Methods
    {
        public enum ResponseTypes
        { LogLogistic }

        private ResponseTypes responseType;
        private double responseSlope;
        private double[] ld50ClassValues;


        #region Properties
        public ResponseTypes ResponseType
        {
            get { return responseType; }
            set { responseType = value; }
        }
        public double ResponseSlope
        {
            get { return responseSlope; }
            set { responseSlope = value; }
        }
        public double[] LD50ClassValues
        {
            get { return ld50ClassValues; }
            set { ld50ClassValues = value; }
        }

        #endregion
        public override void SetValues()
        {
            responseType = ResponseTypes.LogLogistic;
            responseSlope = 1.095;
            ld50ClassValues = new double[] { 1.265, 14.36, 163.2, 1853, 21057 };
        }
    }

}
