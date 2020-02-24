using System;

namespace LepiX.InputParameters
{
    //--------------------------------------------------------------
    // INDIVIDUAL - Beta-Birthtime - Parameters
    //--------------------------------------------------------------
    public class PIndividualBetaBirthtime : Methods
    {
        private bool calculateBeta;
        private double alpha;
        private double beta;
        private int x;
        private double sigma;
        private int maxBirthRange;

        #region Properties
        [Unit(Unit.Units.Boolean)]
        public bool CalculateBeta
        {
            get { return calculateBeta; }
            set { calculateBeta = value; }
        }
        public double Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        public double Beta
        {
            get { return beta; }
            set { beta = value; }
        }
        [Unit(Unit.Units.Days)]
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public double Sigma
        {
            get { return sigma; }
            set { sigma = value; }
        }
        [Unit(Unit.Units.Days)]
        public int MaxBirthRange
        {
            get { return maxBirthRange; }
            set { maxBirthRange = value; }
        }


        #endregion
        public override void SetValues()
        {
            calculateBeta = true;
            alpha = 2;
            beta = 2;
            x = 20;
            sigma = 0.95;
            maxBirthRange = 30;
        }
    }
}
