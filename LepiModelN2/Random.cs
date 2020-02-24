//================================================================
// Random number generator classes
// Lorenz Fahse, Phillip Papastefanou
//================================================================

using System;

namespace LepiX.Core
{
    //================================================================
    // Uniform distributed random numbers
    // according to Margsaglias KISS-Generator using a combination
    // out of a linear congruential generator, a XORshift and 
    // a Multiply-with-carry algorithm.
    //================================================================
    public class UniformRandom
    {
        UInt32 x;
        UInt32 y = 362436000;
        UInt32 z = 521288629;
        UInt64 c = 7654321;
        const double InverseUIntMax = 1.0 / ((double)UInt32.MaxValue + 1);


        //------------------------------------------------------------
        //Constructor with system-time dependent seed
        public UniformRandom()
        {
            x = (uint)Environment.TickCount;
        }
        //------------------------------------------------------------
        //Constructor with seed initialisation
        public UniformRandom(int seed)
        {
            if (seed != 0)
                x = (uint)seed;
            else
                x = (uint)Environment.TickCount;
        }

        //------------------------------------------------------------
        //Generates a uint random number greater than or equal to 0, 
        //and less than 2^32-1
        public UInt32 NextUInt()
        {
            UInt64 t;

            // Linear congruential generator
            x = 69069 * x + 12345;


            // XORshift
            y ^= y << 13;
            y ^= y >> 17;
            y ^= y << 5;

            // Mulitpliy-with-carry
            t = 698769069UL * z + c;
            c = (t >> 32);
            z = (UInt32)t;

            return x + y + z;
        }


        public int Next()
        {
            return (int)(NextUInt() - Int32.MaxValue);
        }

        //------------------------------------------------------------
        //Generates a uint random number greater than or equal to 0.0, 
        //and less than 1.0.
        public double NextDouble()
        {
            return (double)NextUInt() * InverseUIntMax;
        }
        //------------------------------------------------------------
        //Returns a positive integer random number less than the 
        //specified maximum.
        public int Next(int maxValue)
        {
            return (int)(maxValue * NextDouble());
        }
        //------------------------------------------------------------
        //Returns a integer random number equal or greater than minVal 
        // and less than maxVal
        public int Next(int minValue, int maxValue)
        {
            return minValue + (int)((maxValue - minValue) * NextDouble());
        }

    }

    //================================================================
    // Uniform distributed random numbers
    // according to Wichmann & Hill
    //================================================================
    public class WiHiRandom
    {
        int x, y, z;               //State variables
        int xSeed, ySeed, zSeed;   //Seed values of state variables

        const double mxR = 30269.0;
        const double myR = 30307.0;
        const double mzR = 30323.0;

        const int mx = 30269;
        const int my = 30307;
        const int mz = 30323;

        //------------------------------------------------------------
        //Constructor with given seed values
        public WiHiRandom(int x, int y, int z)
        {
            if (x > mx) x = x - mx; if (x <= 0) x = x + mx - 1;
            if (y > my) y = y - my; if (y <= 0) y = y + my - 1;
            if (z > mz) z = z - mz; if (z <= 0) z = z + mz - 1;

            xSeed = x;
            ySeed = y;
            zSeed = z;
            Reset();
        }

        //------------------------------------------------------------
        //Constructor without parameter
        public WiHiRandom()
        {
            xSeed = 1;
            ySeed = 10000;
            zSeed = 3000;
            Reset();
        }

        //------------------------------------------------------------
        //Constructor without parameter
        public void Reset()
        {
            x = xSeed;
            y = ySeed;
            z = zSeed;
        }
        //------------------------------------------------------------
        //Generates a double random numbers greater than or equal 
        //to 0.0, and less than 1.0.
        public double NextDouble()
        {
            double nextDouble;

            //1st generator:
            x = 171 * (x % 177) - 2 * (x / 177);
            if (x < 0) x = x + mx;

            //2nd generator:
            y = 172 * (y % 176) - 35 * (y / 176);
            if (y < 0) y = y + my;

            //3rd generator:
            z = 170 * (z % 178) - 63 * (z / 178);
            if (z < 0) z = z + mz;

            //combine to give function value:
            nextDouble = x / mzR + y / mzR + z / mzR;

            return nextDouble - Math.Floor(nextDouble);
        }
        //------------------------------------------------------------
        //Generates a double random numbers greater than or equal to a, 
        //and less than b, where a < b.
        public double NextDouble(double a, double b)
        {
            return a + (b - a) * NextDouble();
        }
        //------------------------------------------------------------
        //Returns a positive integer random number less than the 
        //specified maximum.
        public int Next(int maxValue)
        {
            return (int)(maxValue * NextDouble());
        }
        //------------------------------------------------------------
        //Returns a integer random number equal or greater than minVal 
        // and less than maxVal
        public int Next(int minValue, int maxValue)
        {
            return minValue + (int)((maxValue - minValue) * NextDouble());
        }
    }
    //================================================================
    // Normally distributed random numbers
    // according to the Box-Muller method
    //================================================================
    public class NormalRandom
    {
        UniformRandom random;  //Uniform random number generator reference

        double mean;
        double standardDeviation;

        //------------------------------------------------------------
        //Constructor with system-time dependent seed
        public NormalRandom()
        {
            random = new UniformRandom();

            mean = 0;
            standardDeviation = 1;
        }
        //------------------------------------------------------------
        //Constructor with seed initialisation
        public NormalRandom(int seed)
        {
            if (seed != 0)
                random = new UniformRandom(seed);
            else
                random = new UniformRandom();

            mean = 0;
            standardDeviation = 1;
        }    //------------------------------------------------------------
        //Constructor with seed, mean and standard-deviation initialisation
        public NormalRandom(int seed, double mean, double standardDeviation)
        {
            if (seed != 0)
                random = new UniformRandom(seed);
            else
                random = new UniformRandom();

            this.mean = mean;
            this.standardDeviation = standardDeviation;
        }
        //------------------------------------------------------------
        //Generates normal distributed random numbers
        public double NextDouble()
        {
            double U, T, R, Z;

            U = random.NextDouble();
            T = 2 * Math.PI * random.NextDouble();
            R = Math.Sqrt(-2 * Math.Log(1 - U));
            Z = R * Math.Sin(T);

            return mean + standardDeviation * Z;
        }
        //------------------------------------------------------------
        //Generates random 2-vectors with normal distributed and
        //independent components
        public double[] NextVector()
        {
            double U, T, R, X, Y;

            U = random.NextDouble();
            T = 2 * Math.PI * random.NextDouble();
            R = Math.Sqrt(-2 * Math.Log(1 - U));
            X = R * Math.Cos(T);
            Y = R * Math.Sin(T);

            return new double[] { X, Y };
        }
    }
    //================================================================


    public class LogNormalRandom
    {
        NormalRandom normalRandom;  //Uniform random number generator reference

        //double uMean;
        //double uStandardDeviation;

        //------------------------------------------------------------
        //Constructor with system-time dependent seed
        public LogNormalRandom()
        {
            normalRandom = new NormalRandom();

            //uMean = 0;
            //uStandardDeviation = 1;
        }
        //------------------------------------------------------------
        //Constructor with seed initialisation
        public LogNormalRandom(int seed)
        {
            if (seed != 0)
                normalRandom = new NormalRandom(seed);
            else
                normalRandom = new NormalRandom();

            //uMean = 0;
            //uStandardDeviation = 1;

        }
        //------------------------------------------------------------
        //Constructor with seed, mean and standard-deviation initialisation
        public LogNormalRandom(int seed, double mean, double standardDeviation)
        {

            // mean and standardDeviation have to be transformed. 
            // according to mathematatica script file
            //this.uMean = Math.Log(mean) - (double) 1 / 2 * Math.Log(1 + (standardDeviation * standardDeviation) / (mean * mean));
            //this.uStandardDeviation = Math.Sqrt(Math.Log((mean * mean + standardDeviation * standardDeviation) / (mean * mean)));

            if (seed != 0)
                normalRandom = new NormalRandom(seed, mean, standardDeviation);
            else
                throw new ArgumentOutOfRangeException();
        }
        //------------------------------------------------------------
        //Generates log - normal distributed random numbers
        public double NextDouble()
        {
            return Math.Exp(normalRandom.NextDouble());
        }
    }
    //================================================================


    //================================================================
    // Gamma distributed random numbers
    // according to the Marsaglia and Tsang Method 
    // GSL - Library
    // Generates gamma distributed random numbers with shape parameter a
    // and rate parameter l
    // Works currently for  a > 1 
    //================================================================
    public class GammaRandom
    {
        UniformRandom uniformRandom;                  //Uniform random number generator reference
        NormalRandom normalRandom;      //Normal random number generator reference
        private double a;
        private double l;
        //------------------------------------------------------------
        //Constructor with system-time dependent seed
        public GammaRandom(double shape, double scale)
        {
            uniformRandom = new UniformRandom();
            normalRandom = new NormalRandom();
            a = shape;
            l = scale;
        }
        //------------------------------------------------------------
        //Constructor with seed initialisation
        public GammaRandom(int seed, double shape, double scale)
        {

            if (seed != 0)
            {
                uniformRandom = new UniformRandom(seed);
                normalRandom = new NormalRandom(seed);
            }
            else
            {
                uniformRandom = new UniformRandom();
                normalRandom = new NormalRandom();
            }

            a = shape;
            l = scale;
        }
        //------------------------------------------------------------

        // According to GSL - library
        // NextDouble calculation
        // Works for a > 1
        public double NextDouble()
        {
            double x, v, u;
            double d = a - 1.0 / 3.0;
            double c = (1.0 / 3.0) / Math.Sqrt(d);

            bool flag = true;
            v = 0;


            while (flag)
            {

                do
                {
                    x = normalRandom.NextDouble();
                    v = 1 + c * x;

                }
                while (v <= 0);

                v = v * v * v;
                u = uniformRandom.NextDouble();

                if ((u < 1 - 0.0331 * x * x * x * x) || (Math.Log(u) < 0.5 * x * x + d * (1 - v + Math.Log(v))))
                {
                    flag = false;
                }
            }

            return l * d * v;
        }


        //Returns an integer random number from the GammaDistribution
        public int Next()
        {
            return Convert.ToInt32(Math.Round(NextDouble()));
        }
        //------------------------------------------------------------



    }
    //================================================================

    //================================================================
    // according to Ahrens, 1974 and Cheng, 1978.
    // Works currently for  a > 1, b > 1 
    //
    //================================================================
    public class BetaRandom
    {
        GammaRandom gammaRandomA;
        GammaRandom gammaRandomB;

        //------------------------------------------------------------
        //Constructor with system-time dependent seed
        public BetaRandom(double a, double b)
        {
            gammaRandomA = new GammaRandom(1, a);
            gammaRandomB = new GammaRandom(1, b);
        }
        //------------------------------------------------------------
        //Constructor with seed initialisation
        public BetaRandom(int seedA, int seedB, double a, double b)
        {

            if (seedA != 0 && seedB != 0)
            {
                gammaRandomA = new GammaRandom(seedA, a, 1);
                gammaRandomB = new GammaRandom(seedB, b, 1);
            }

            else
            {
                gammaRandomA = new GammaRandom(1, a);
                gammaRandomB = new GammaRandom(1, b);
            }
        }
        //------------------------------------------------------------

        public double NextDouble()
        {
            double A = gammaRandomA.NextDouble();
            double B = gammaRandomB.NextDouble();

            return A / (A + B);
        }
        //------------------------------------------------------------ 
    }
}
