using System;

namespace LepiX.InputParameters
{
    public class PSSDOutput  :Methods
    {
        private bool fitSuccessfull;
        private double ssdSlope;
        private double ssdIntercept;
        private double logQuantileLower;
        private double logQuantileUpper;
        private double[] estimatedLD50Values;

        public bool FitSuccessfull
        {
            get { return fitSuccessfull; }
            set { fitSuccessfull = value; }
        }
        public double SSDSlope
        {
            get { return ssdSlope; }
            set { ssdSlope = value; }
        }
        public double SSDIntercept
        {
            get { return ssdIntercept; }
            set { ssdIntercept = value; }
        }        
        public double LogQuantileLower
        {
            get { return logQuantileLower; }
            set { logQuantileLower = value; }
        }
        public double LogQuantileUpper
        {
            get { return logQuantileUpper; }
            set { logQuantileUpper = value; }
        }
        public double[] EstimatedLD50Values
        {
            get { return estimatedLD50Values; }
            set { estimatedLD50Values = value; }
        }
               

        public override void SetValues()
        {
            this.FitSuccessfull = false;
        }
    }
}
