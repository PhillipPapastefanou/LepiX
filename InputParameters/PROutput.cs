//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;

namespace LepiX.InputParameters
{
    public class PROutput:Methods
    {
        public enum ExportDetailedDistances {All, FirstOnly, FirstAndLast, LastOnly }

        private bool mortalityAnalysis;
        private bool detailedAnalysis;
        private ExportDetailedDistances detailedDistances;     
        

        public bool MortalityAnalysis
        {
            get { return mortalityAnalysis; }
            set { mortalityAnalysis = value; }
        }        
        public bool DetailedAnalysis
        {
            get { return detailedAnalysis; }
            set { detailedAnalysis = value; }
        }
        public ExportDetailedDistances DetailedDistances
        {
            get { return detailedDistances; }
            set { detailedDistances = value; }
        }


        public override void SetValues()
        {
            this.detailedDistances = ExportDetailedDistances.FirstAndLast;
            this.mortalityAnalysis = true;
            this.detailedAnalysis = true;
        }
    }
}
