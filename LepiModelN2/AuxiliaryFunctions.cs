//================================================================
// LepiModel: AuxiliaryFunctions
// Lorenz Fahse, Phillip Papastefanou, 21-10-2013
//================================================================
using System;


namespace LepiX.Core
{
    static class AuxiliaryFunctions
    {
        // Calculate the upper incomplete gamma function
        // The code is based on: "Computation of Special Functions" Zhang and Jin,
        // John Wiley and Sons, 1996

        public static double IncompleteGamma(double a, double x)
        {
            double xam, t0, r, ga, m, s, z, gr;
            z = 0;

            if (a > 171 || x < 0)
            { return Math.Pow(10, 309); }

            if (x > 0)
                xam = -x + a * Math.Log(x);
            else
                xam = 0;

            if (xam > 700)
            { return Math.Pow(10, 309); }

            if (x > 1 + a)
            {
                t0 = 0;
                for (int k = 60; k >= 1; k--)
                {
                    t0 = (k - a) / (1 + k / (x + t0));
                }

                return Math.Exp(xam) / (x + t0);
            }

            r = 1; ga = 1;
            if (a == Math.Round(a))
                ga = Factorial(a - 1);

            else
            {
                if (Math.Abs(a) > 1)
                {
                    z = Math.Abs(a);
                    m = Math.Floor(z);

                    for (int i = 1; i <= m; i++)
                    {
                        r *= (z - i);
                    }
                    z -= m;
                }
                else
                {
                    z = a;
                }

                gr = GetGr(z);
                ga = 1 / (gr * z) * r;
            }

            if (x == 0)
            {
                return ga;
            }

            s = 1 / a;
            r = s;

            for (int i = 1; i < 60 + 1; i++)
            {
                r *= x / (a + i);
                s += r;
            }
            return ga - Math.Exp(xam) * s;

        }


        // The gr-value needed for the gamma calculation.
        private static double GetGr(double z)
        {
            return (((((((((((((((((((((((0.14e-14
          * z - 0.54e-14) * z - 0.206e-13) * z + 0.51e-12)
          * z - 0.36968e-11) * z + 0.77823e-11) * z + 0.1043427e-9)
          * z - 0.11812746e-8) * z + 0.50020075e-8) * z + 0.6116095e-8)
          * z - 0.2056338417e-6) * z + 0.1133027232e-5) * z - 0.12504934821e-5)
          * z - 0.201348547807e-4) * z + 0.1280502823882e-3) * z - 0.2152416741149e-3)
          * z - 0.11651675918591e-2) * z + 0.7218943246663e-2) * z - 0.9621971527877e-2)
          * z - 0.421977345555443e-1) * z + 0.1665386113822915) * z - 0.420026350340952e-1)
          * z - 0.6558780715202538) * z + 0.5772156649015329) * z + 1;
        }

        // Factorial calculation.
        public static double Factorial(double i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }

        // Errorfunction
        // according to... 
        // series explanation
        public static double Erf(double x)
        {
            // constants
            const double a1 = 0.254829592;
            const double a2 = -0.284496736;
            const double a3 = 1.421413741;
            const double a4 = -1.453152027;
            const double a5 = 1.061405429;
            const double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);
            return sign * y;
        }

        public const double machineepsilon = 5E-16;
        public const double maxrealnumber = 1E300;
        public const double minrealnumber = 1E-300;
        public static double incompletebeta(double a, double b, double x)
        {
            double result = 0;
            double t = 0;
            double xc = 0;
            double w = 0;
            double y = 0;
            int flag = 0;
            double sg = 0;
            double big = 0;
            double biginv = 0;
            double maxgam = 0;
            double minlog = 0;
            double maxlog = 0;

            big = 4.503599627370496e15;
            biginv = 2.22044604925031308085e-16;
            maxgam = 171.624376956302725;
            minlog = Math.Log(minrealnumber);
            maxlog = Math.Log(maxrealnumber);

            //alglib.ap.assert((double)(a) > (double)(0) && (double)(b) > (double)(0), "Domain error in IncompleteBeta");
            //alglib.ap.assert((double)(x) >= (double)(0) && (double)(x) <= (double)(1), "Domain error in IncompleteBeta");

            if ((double)(x) == (double)(0))
            {
                result = 0;
                return result;
            }
            if ((double)(x) == (double)(1))
            {
                result = 1;
                return result;
            }
            flag = 0;
            if ((double)(b * x) <= (double)(1.0) && (double)(x) <= (double)(0.95))
            {
                result = incompletebetaps(a, b, x, maxgam);
                return result;
            }
            w = 1.0 - x;
            if ((double)(x) > (double)(a / (a + b)))
            {
                flag = 1;
                t = a;
                a = b;
                b = t;
                xc = x;
                x = w;
            }
            else
            {
                xc = w;
            }
            if ((flag == 1 && (double)(b * x) <= (double)(1.0)) && (double)(x) <= (double)(0.95))
            {
                t = incompletebetaps(a, b, x, maxgam);
                if ((double)(t) <= (double)(machineepsilon))
                {
                    result = 1.0 - machineepsilon;
                }
                else
                {
                    result = 1.0 - t;
                }
                return result;
            }
            y = x * (a + b - 2.0) - (a - 1.0);
            if ((double)(y) < (double)(0.0))
            {
                w = incompletebetafe(a, b, x, big, biginv);
            }
            else
            {
                w = incompletebetafe2(a, b, x, big, biginv) / xc;
            }
            y = a * Math.Log(x);
            t = b * Math.Log(xc);
            if (((double)(a + b) < (double)(maxgam) && (double)(Math.Abs(y)) < (double)(maxlog)) && (double)(Math.Abs(t)) < (double)(maxlog))
            {
                t = Math.Pow(xc, b);
                t = t * Math.Pow(x, a);
                t = t / a;
                t = t * w;
                t = t * (gammafunc.gammafunction(a + b) / (gammafunc.gammafunction(a) * gammafunc.gammafunction(b)));
                if (flag == 1)
                {
                    if ((double)(t) <= (double)(machineepsilon))
                    {
                        result = 1.0 - machineepsilon;
                    }
                    else
                    {
                        result = 1.0 - t;
                    }
                }
                else
                {
                    result = t;
                }
                return result;
            }
            y = y + t + gammafunc.lngamma(a + b, ref sg) - gammafunc.lngamma(a, ref sg) - gammafunc.lngamma(b, ref sg);
            y = y + Math.Log(w / a);
            if ((double)(y) < (double)(minlog))
            {
                t = 0.0;
            }
            else
            {
                t = Math.Exp(y);
            }
            if (flag == 1)
            {
                if ((double)(t) <= (double)(machineepsilon))
                {
                    t = 1.0 - machineepsilon;
                }
                else
                {
                    t = 1.0 - t;
                }
            }
            result = t;
            return result;
        }


        /*************************************************************************
        Continued fraction expansion #1 for incomplete beta integral

        Cephes Math Library, Release 2.8:  June, 2000
        Copyright 1984, 1995, 2000 by Stephen L. Moshier
        *************************************************************************/
        private static double incompletebetafe(double a,
            double b,
            double x,
            double big,
            double biginv)
        {
            double result = 0;
            double xk = 0;
            double pk = 0;
            double pkm1 = 0;
            double pkm2 = 0;
            double qk = 0;
            double qkm1 = 0;
            double qkm2 = 0;
            double k1 = 0;
            double k2 = 0;
            double k3 = 0;
            double k4 = 0;
            double k5 = 0;
            double k6 = 0;
            double k7 = 0;
            double k8 = 0;
            double r = 0;
            double t = 0;
            double ans = 0;
            double thresh = 0;
            int n = 0;

            k1 = a;
            k2 = a + b;
            k3 = a;
            k4 = a + 1.0;
            k5 = 1.0;
            k6 = b - 1.0;
            k7 = k4;
            k8 = a + 2.0;
            pkm2 = 0.0;
            qkm2 = 1.0;
            pkm1 = 1.0;
            qkm1 = 1.0;
            ans = 1.0;
            r = 1.0;
            n = 0;
            thresh = 3.0 * machineepsilon;
            do
            {
                xk = -(x * k1 * k2 / (k3 * k4));
                pk = pkm1 + pkm2 * xk;
                qk = qkm1 + qkm2 * xk;
                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                xk = x * k5 * k6 / (k7 * k8);
                pk = pkm1 + pkm2 * xk;
                qk = qkm1 + qkm2 * xk;
                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                if ((double)(qk) != (double)(0))
                {
                    r = pk / qk;
                }
                if ((double)(r) != (double)(0))
                {
                    t = Math.Abs((ans - r) / r);
                    ans = r;
                }
                else
                {
                    t = 1.0;
                }
                if ((double)(t) < (double)(thresh))
                {
                    break;
                }
                k1 = k1 + 1.0;
                k2 = k2 + 1.0;
                k3 = k3 + 2.0;
                k4 = k4 + 2.0;
                k5 = k5 + 1.0;
                k6 = k6 - 1.0;
                k7 = k7 + 2.0;
                k8 = k8 + 2.0;
                if ((double)(Math.Abs(qk) + Math.Abs(pk)) > (double)(big))
                {
                    pkm2 = pkm2 * biginv;
                    pkm1 = pkm1 * biginv;
                    qkm2 = qkm2 * biginv;
                    qkm1 = qkm1 * biginv;
                }
                if ((double)(Math.Abs(qk)) < (double)(biginv) || (double)(Math.Abs(pk)) < (double)(biginv))
                {
                    pkm2 = pkm2 * big;
                    pkm1 = pkm1 * big;
                    qkm2 = qkm2 * big;
                    qkm1 = qkm1 * big;
                }
                n = n + 1;
            }
            while (n != 300);
            result = ans;
            return result;
        }


        /*************************************************************************
        Continued fraction expansion #2
        for incomplete beta integral

        Cephes Math Library, Release 2.8:  June, 2000
        Copyright 1984, 1995, 2000 by Stephen L. Moshier
        *************************************************************************/
        private static double incompletebetafe2(double a,
            double b,
            double x,
            double big,
            double biginv)
        {
            double result = 0;
            double xk = 0;
            double pk = 0;
            double pkm1 = 0;
            double pkm2 = 0;
            double qk = 0;
            double qkm1 = 0;
            double qkm2 = 0;
            double k1 = 0;
            double k2 = 0;
            double k3 = 0;
            double k4 = 0;
            double k5 = 0;
            double k6 = 0;
            double k7 = 0;
            double k8 = 0;
            double r = 0;
            double t = 0;
            double ans = 0;
            double z = 0;
            double thresh = 0;
            int n = 0;

            k1 = a;
            k2 = b - 1.0;
            k3 = a;
            k4 = a + 1.0;
            k5 = 1.0;
            k6 = a + b;
            k7 = a + 1.0;
            k8 = a + 2.0;
            pkm2 = 0.0;
            qkm2 = 1.0;
            pkm1 = 1.0;
            qkm1 = 1.0;
            z = x / (1.0 - x);
            ans = 1.0;
            r = 1.0;
            n = 0;
            thresh = 3.0 * machineepsilon;
            do
            {
                xk = -(z * k1 * k2 / (k3 * k4));
                pk = pkm1 + pkm2 * xk;
                qk = qkm1 + qkm2 * xk;
                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                xk = z * k5 * k6 / (k7 * k8);
                pk = pkm1 + pkm2 * xk;
                qk = qkm1 + qkm2 * xk;
                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                if ((double)(qk) != (double)(0))
                {
                    r = pk / qk;
                }
                if ((double)(r) != (double)(0))
                {
                    t = Math.Abs((ans - r) / r);
                    ans = r;
                }
                else
                {
                    t = 1.0;
                }
                if ((double)(t) < (double)(thresh))
                {
                    break;
                }
                k1 = k1 + 1.0;
                k2 = k2 - 1.0;
                k3 = k3 + 2.0;
                k4 = k4 + 2.0;
                k5 = k5 + 1.0;
                k6 = k6 + 1.0;
                k7 = k7 + 2.0;
                k8 = k8 + 2.0;
                if ((double)(Math.Abs(qk) + Math.Abs(pk)) > (double)(big))
                {
                    pkm2 = pkm2 * biginv;
                    pkm1 = pkm1 * biginv;
                    qkm2 = qkm2 * biginv;
                    qkm1 = qkm1 * biginv;
                }
                if ((double)(Math.Abs(qk)) < (double)(biginv) || (double)(Math.Abs(pk)) < (double)(biginv))
                {
                    pkm2 = pkm2 * big;
                    pkm1 = pkm1 * big;
                    qkm2 = qkm2 * big;
                    qkm1 = qkm1 * big;
                }
                n = n + 1;
            }
            while (n != 300);
            result = ans;
            return result;
        }


        /*************************************************************************
        Power series for incomplete beta integral.
        Use when b*x is small and x not too close to 1.

        Cephes Math Library, Release 2.8:  June, 2000
        Copyright 1984, 1995, 2000 by Stephen L. Moshier
        *************************************************************************/
        private static double incompletebetaps(double a,
            double b,
            double x,
            double maxgam)
        {
            double result = 0;
            double s = 0;
            double t = 0;
            double u = 0;
            double v = 0;
            double n = 0;
            double t1 = 0;
            double z = 0;
            double ai = 0;
            double sg = 0;

            ai = 1.0 / a;
            u = (1.0 - b) * x;
            v = u / (a + 1.0);
            t1 = v;
            t = u;
            n = 2.0;
            s = 0.0;
            z = machineepsilon * ai;
            while ((double)(Math.Abs(v)) > (double)(z))
            {
                u = (n - b) * x / n;
                t = t * u;
                v = t / (a + n);
                s = s + v;
                n = n + 1.0;
            }
            s = s + t1;
            s = s + ai;
            u = a * Math.Log(x);
            if ((double)(a + b) < (double)(maxgam) && (double)(Math.Abs(u)) < (double)(Math.Log(maxrealnumber)))
            {
                t = gammafunc.gammafunction(a + b) / (gammafunc.gammafunction(a) * gammafunc.gammafunction(b));
                s = s * t * Math.Pow(x, a);
            }
            else
            {
                t = gammafunc.lngamma(a + b, ref sg) - gammafunc.lngamma(a, ref sg) - gammafunc.lngamma(b, ref sg) + u + Math.Log(s);
                if ((double)(t) < (double)(Math.Log(minrealnumber)))
                {
                    s = 0.0;
                }
                else
                {
                    s = Math.Exp(t);
                }
            }
            result = s;
            return result;
        }

    }


    public class gammafunc
    {
        /*************************************************************************
        Gamma function

        Input parameters:
            X   -   argument

        Domain:
            0 < X < 171.6
            -170 < X < 0, X is not an integer.

        Relative error:
         arithmetic   domain     # trials      peak         rms
            IEEE    -170,-33      20000       2.3e-15     3.3e-16
            IEEE     -33,  33     20000       9.4e-16     2.2e-16
            IEEE      33, 171.6   20000       2.3e-15     3.2e-16

        Cephes Math Library Release 2.8:  June, 2000
        Original copyright 1984, 1987, 1989, 1992, 2000 by Stephen L. Moshier
        Translated to AlgoPascal by Bochkanov Sergey (2005, 2006, 2007).
        *************************************************************************/
        public static double gammafunction(double x)
        {
            double result = 0;
            double p = 0;
            double pp = 0;
            double q = 0;
            double qq = 0;
            double z = 0;
            int i = 0;
            double sgngam = 0;

            sgngam = 1;
            q = Math.Abs(x);
            if ((double)(q) > (double)(33.0))
            {
                if ((double)(x) < (double)(0.0))
                {
                    p = (int)Math.Floor(q);
                    i = (int)Math.Round(p);
                    if (i % 2 == 0)
                    {
                        sgngam = -1;
                    }
                    z = q - p;
                    if ((double)(z) > (double)(0.5))
                    {
                        p = p + 1;
                        z = q - p;
                    }
                    z = q * Math.Sin(Math.PI * z);
                    z = Math.Abs(z);
                    z = Math.PI / (z * gammastirf(q));
                }
                else
                {
                    z = gammastirf(x);
                }
                result = sgngam * z;
                return result;
            }
            z = 1;
            while ((double)(x) >= (double)(3))
            {
                x = x - 1;
                z = z * x;
            }
            while ((double)(x) < (double)(0))
            {
                if ((double)(x) > (double)(-0.000000001))
                {
                    result = z / ((1 + 0.5772156649015329 * x) * x);
                    return result;
                }
                z = z / x;
                x = x + 1;
            }
            while ((double)(x) < (double)(2))
            {
                if ((double)(x) < (double)(0.000000001))
                {
                    result = z / ((1 + 0.5772156649015329 * x) * x);
                    return result;
                }
                z = z / x;
                x = x + 1.0;
            }
            if ((double)(x) == (double)(2))
            {
                result = z;
                return result;
            }
            x = x - 2.0;
            pp = 1.60119522476751861407E-4;
            pp = 1.19135147006586384913E-3 + x * pp;
            pp = 1.04213797561761569935E-2 + x * pp;
            pp = 4.76367800457137231464E-2 + x * pp;
            pp = 2.07448227648435975150E-1 + x * pp;
            pp = 4.94214826801497100753E-1 + x * pp;
            pp = 9.99999999999999996796E-1 + x * pp;
            qq = -2.31581873324120129819E-5;
            qq = 5.39605580493303397842E-4 + x * qq;
            qq = -4.45641913851797240494E-3 + x * qq;
            qq = 1.18139785222060435552E-2 + x * qq;
            qq = 3.58236398605498653373E-2 + x * qq;
            qq = -2.34591795718243348568E-1 + x * qq;
            qq = 7.14304917030273074085E-2 + x * qq;
            qq = 1.00000000000000000320 + x * qq;
            result = z * pp / qq;
            return result;
        }


        /*************************************************************************
        Natural logarithm of gamma function

        Input parameters:
            X       -   argument

        Result:
            logarithm of the absolute value of the Gamma(X).

        Output parameters:
            SgnGam  -   sign(Gamma(X))

        Domain:
            0 < X < 2.55e305
            -2.55e305 < X < 0, X is not an integer.

        ACCURACY:
        arithmetic      domain        # trials     peak         rms
           IEEE    0, 3                 28000     5.4e-16     1.1e-16
           IEEE    2.718, 2.556e305     40000     3.5e-16     8.3e-17
        The error criterion was relative when the function magnitude
        was greater than one but absolute when it was less than one.

        The following test used the relative error criterion, though
        at certain points the relative error could be much higher than
        indicated.
           IEEE    -200, -4             10000     4.8e-16     1.3e-16

        Cephes Math Library Release 2.8:  June, 2000
        Copyright 1984, 1987, 1989, 1992, 2000 by Stephen L. Moshier
        Translated to AlgoPascal by Bochkanov Sergey (2005, 2006, 2007).
        *************************************************************************/
        public static double lngamma(double x,
            ref double sgngam)
        {
            double result = 0;
            double a = 0;
            double b = 0;
            double c = 0;
            double p = 0;
            double q = 0;
            double u = 0;
            double w = 0;
            double z = 0;
            int i = 0;
            double logpi = 0;
            double ls2pi = 0;
            double tmp = 0;

            sgngam = 0;

            sgngam = 1;
            logpi = 1.14472988584940017414;
            ls2pi = 0.91893853320467274178;
            if ((double)(x) < (double)(-34.0))
            {
                q = -x;
                w = lngamma(q, ref tmp);
                p = (int)Math.Floor(q);
                i = (int)Math.Round(p);
                if (i % 2 == 0)
                {
                    sgngam = -1;
                }
                else
                {
                    sgngam = 1;
                }
                z = q - p;
                if ((double)(z) > (double)(0.5))
                {
                    p = p + 1;
                    z = p - q;
                }
                z = q * Math.Sin(Math.PI * z);
                result = logpi - Math.Log(z) - w;
                return result;
            }
            if ((double)(x) < (double)(13))
            {
                z = 1;
                p = 0;
                u = x;
                while ((double)(u) >= (double)(3))
                {
                    p = p - 1;
                    u = x + p;
                    z = z * u;
                }
                while ((double)(u) < (double)(2))
                {
                    z = z / u;
                    p = p + 1;
                    u = x + p;
                }
                if ((double)(z) < (double)(0))
                {
                    sgngam = -1;
                    z = -z;
                }
                else
                {
                    sgngam = 1;
                }
                if ((double)(u) == (double)(2))
                {
                    result = Math.Log(z);
                    return result;
                }
                p = p - 2;
                x = x + p;
                b = -1378.25152569120859100;
                b = -38801.6315134637840924 + x * b;
                b = -331612.992738871184744 + x * b;
                b = -1162370.97492762307383 + x * b;
                b = -1721737.00820839662146 + x * b;
                b = -853555.664245765465627 + x * b;
                c = 1;
                c = -351.815701436523470549 + x * c;
                c = -17064.2106651881159223 + x * c;
                c = -220528.590553854454839 + x * c;
                c = -1139334.44367982507207 + x * c;
                c = -2532523.07177582951285 + x * c;
                c = -2018891.41433532773231 + x * c;
                p = x * b / c;
                result = Math.Log(z) + p;
                return result;
            }
            q = (x - 0.5) * Math.Log(x) - x + ls2pi;
            if ((double)(x) > (double)(100000000))
            {
                result = q;
                return result;
            }
            p = 1 / (x * x);
            if ((double)(x) >= (double)(1000.0))
            {
                q = q + ((7.9365079365079365079365 * 0.0001 * p - 2.7777777777777777777778 * 0.001) * p + 0.0833333333333333333333) / x;
            }
            else
            {
                a = 8.11614167470508450300 * 0.0001;
                a = -(5.95061904284301438324 * 0.0001) + p * a;
                a = 7.93650340457716943945 * 0.0001 + p * a;
                a = -(2.77777777730099687205 * 0.001) + p * a;
                a = 8.33333333333331927722 * 0.01 + p * a;
                q = q + a / x;
            }
            result = q;
            return result;
        }


        private static double gammastirf(double x)
        {
            double result = 0;
            double y = 0;
            double w = 0;
            double v = 0;
            double stir = 0;

            w = 1 / x;
            stir = 7.87311395793093628397E-4;
            stir = -2.29549961613378126380E-4 + w * stir;
            stir = -2.68132617805781232825E-3 + w * stir;
            stir = 3.47222221605458667310E-3 + w * stir;
            stir = 8.33333333333482257126E-2 + w * stir;
            w = 1 + w * stir;
            y = Math.Exp(x);
            if ((double)(x) > (double)(143.01608))
            {
                v = Math.Pow(x, 0.5 * x - 0.25);
                y = v * (v / y);
            }
            else
            {
                y = Math.Pow(x, x - 0.5) / y;
            }
            result = 2.50662827463100050242 * y * w;
            return result;
        }
    }
}
