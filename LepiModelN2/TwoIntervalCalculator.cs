using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LepiX.Core
{
    class TwoIntervalCalculator
    {


        private double[] exposition;

        public double[] Exposition
        {
            get { return exposition; }
        }


        private int numberOfExposedDays;
        private int numberOfRestDays;



        private int maxOffset;

        public int MaxOffset
        {
            get { return maxOffset; }
            set { maxOffset = value; }
        }


        private int secondMaxOffset;

        public int SecondMaxOffset
        {
            get { return secondMaxOffset; }
            set { secondMaxOffset = value; }
        }



        public TwoIntervalCalculator(double[] exposition, int numberOfExposedDays)
        {
            this.exposition = exposition;
            this.numberOfExposedDays = numberOfExposedDays;
            this.numberOfRestDays = 0;


            CalculateMaximumDayInterval();

            CalculateSecondMaximumInterval();
        }




        private void CalculateMaximumDayInterval()
        {
            this.maxOffset = FindMaxiumInterval(0, exposition.Length - numberOfExposedDays);

            //Console.WriteLine("Maximum Offset: {0}", maxOffset);

            //--
        }



        private void CalculateSecondMaximumInterval()
        {
            //Look before
            if (numberOfExposedDays + numberOfRestDays > maxOffset)
            {
                //Look after
                if (exposition.Length - maxOffset - numberOfExposedDays - numberOfRestDays < numberOfExposedDays)
                {
                    // there is no place for a second max offset!
                    this.secondMaxOffset = -1;
                }

                else
                {
                    this.secondMaxOffset = FindMaxiumInterval(maxOffset + numberOfExposedDays + numberOfRestDays, exposition.Length - numberOfExposedDays);
                }
            }

            else
            {
                int time1 = FindMaxiumInterval(0, maxOffset - numberOfExposedDays - numberOfRestDays);
                double sum1 = GetSum(time1);

                //Look after
                if (exposition.Length - maxOffset - numberOfExposedDays - numberOfRestDays < numberOfExposedDays)
                {
                    this.secondMaxOffset = time1;
                }

                else
                {
                    int time2 = FindMaxiumInterval(maxOffset + numberOfExposedDays + numberOfRestDays, exposition.Length - numberOfExposedDays);
                    double sum2 = GetSum(time2);

                    if (sum2 > sum1)
                    {
                        this.secondMaxOffset = time2;
                    }
                    else
                    {
                        this.secondMaxOffset = time1;
                    }
                }
            }


            //Console.WriteLine("Second Maximum Offset: {0}", secondMaxOffset);
        }


        private int FindMaxiumInterval(int begin, int end)
        {
            int maximumDaysOffset = begin;
            double sum;
            double maxSum = 0;

            for (int i = begin; i <= end; i++)
            {
                sum = 0;

                for (int exposedDay = 0; exposedDay < numberOfExposedDays; exposedDay++)
                {
                    sum = sum + exposition[i + exposedDay];
                }

                if (maxSum <= sum)
                {
                    maximumDaysOffset = i;
                    maxSum = sum;
                }

            }

            return maximumDaysOffset;
        }

        private double GetSum(int begin)
        {
            double sum = 0;

            for (int exposedDay = 0; exposedDay < numberOfExposedDays; exposedDay++)
            {
                sum = sum + exposition[begin + exposedDay];
            }

            return sum;
        }


    }
}

