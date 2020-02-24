//================================================================
// LepiModel: AuxiliaryMethods
// Lorenz Fahse, Phillip Papastefanou, 21-10-2013
//================================================================
using System;

namespace LepiX.Core
{
    static class AuxiliaryMethods
    {
        //------------------------------------------------------------
        // Caclulates a root of a function using interval halving method
        // also known as bisection algorithm
        // This algorithm converges if the root is in [a,b]
        //------------------------------------------------------------
        public static double BiSection(Func<double, double> function, double Precision, int NMax, double A, double B)
        {

            double precision = Precision;       // Expected accurateness of root
            int nMax = NMax;                    // Maximum iteration steps
            double a = A;                       // Left IntervalBracket
            double b = B;                       // Right IntervalBracket 

            double c = 0;

            int k = 0;
            double epsilon;
            epsilon = b - a;

            while ((k < nMax) && (epsilon > Precision))
            {
                epsilon = Math.Abs(a - b);
                k++;

                c = (a + b) / 2;

                if (Math.Sign(function(c)) == Math.Sign(function(a)))
                {
                    a = c;
                }

                else
                {
                    b = c;
                }

            }

            return c;
        }



        // Convert Day / Month/ to an intervalue starting with 0 at 1/1/

        public static int DateToInt(int day, int month)
        {
            DateTime dateTimeNull = new DateTime(2013, 1, 1);
            DateTime offset = new DateTime(2013, month, day);

            return Convert.ToInt32(offset.Subtract(dateTimeNull).TotalDays);
        }


        public static int[] DistributeRunsToTask(int numberOfRuns, int cores)
        {
            int[] distribution = new int[cores];

            int fullCycles = (int)numberOfRuns/ cores;
            int restCycles = (int)numberOfRuns % cores;

            for (int i = 0; i < cores; i++)
            {
                int c = fullCycles;

                if (i < restCycles)
                {
                    c = c + 1;
                }

                distribution[i] = c;
            }

            return distribution;
        }



    }
}
