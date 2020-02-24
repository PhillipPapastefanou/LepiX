//================================================================
// LepiModel: Mortality Class
// Lorenz Fahse, Phillip Papastefanou, 21-10-2013
//================================================================

using System;

namespace LepiX.Core
{
    class Mortality: Component
    {
        //------------------------------------------------------------
        // Component references 
        //------------------------------------------------------------
        PollenDepositionSampled pollenDepositionSampled;
        Population population;
        SSD ssd;

        //------------------------------------------------------------
        // Parameters
        //------------------------------------------------------------ 

        InputParameters.PExposition.ExposedTimes expositionIntervals;
        int numberOfSensitivityClasses;
        double generalSlope;
        int numberOfExposedDays;
        bool includeLeafVariability;

        //------------------------------------------------------------
        // variables
        //------------------------------------------------------------ 
        double[] depositionData;
        Individual[] individuals;

        double[] totalEstimatedExpositionAmount;

        double[][] mortalityPerPopulation;


        double totalExposedAmount;





        //------------------------------------------------------------
        // GetFunctions
        //------------------------------------------------------------
        public double[] TotalEstimatedExpositionAmount() { return totalEstimatedExpositionAmount; }
        public double[][] MortalityPerPopulation() { return mortalityPerPopulation; }

        public double TotalExposedAmount
        {
            get { return totalExposedAmount; }
        }

        //------------------------------------------------------------
        //  Constructor
        //------------------------------------------------------------
        public Mortality(int pID):base(pID)
        {
            this.pID = pID; 
        }

        public override void Initialise(Scenario scenario)
        {
            this.modelParameters = scenario.ModelParameters;
            this.scenarioParameters = scenario.ScenarioParameters;
            this.generator = scenario.Generator;


            totalEstimatedExpositionAmount = new double[modelParameters.General.NumberOfIndividuals];

            numberOfSensitivityClasses = modelParameters.DoseResponseGeneral.LD50ClassValues.Length;
            expositionIntervals = modelParameters.Exposition.NumberOfExposedIntervals;
            mortalityPerPopulation = new double[modelParameters.General.NumberOfIndividuals][];
            numberOfExposedDays = scenarioParameters.ExpositionDays;
            includeLeafVariability = modelParameters.DepositionSampled.IncludeVarOnLeafs;

            double[] lDvals = new double[numberOfSensitivityClasses];


            for (int i = 0; i < numberOfSensitivityClasses; i++)
            {
                lDvals[i] = modelParameters.DoseResponseGeneral.LD50ClassValues[i];
            }

            generalSlope = modelParameters.DoseResponseGeneral.ResponseSlope;
            ssd = new SSD(lDvals, generalSlope);
        }

        public override void Input()
        {
            depositionData = pollenDepositionSampled.RandomYearDeposition(); 
        }

        public override void Update()
        {
            individuals = population.Individuals();

            foreach (Individual individual in individuals)
            {
                // Calculate the expostionData only once
                double[] exposition = CalclulateExposition(individual);


                // Get the total estimated exposition Amount as Sum of the exposition 
                double sum = 0;
                for (int i = 0; i < exposition.Length; i++)
			    {
			        sum = sum + exposition[i];      
			    }

                
                totalEstimatedExpositionAmount[individual.ID] = sum;
                
                // Calculate the mortality once for every sensitivity class
                mortalityPerPopulation[individual.ID] = new double[numberOfSensitivityClasses];
                
                for (int i = 0; i < numberOfSensitivityClasses; i++)
                {
                    mortalityPerPopulation[individual.ID][i] = CalculateMortalityPerIndividual(i, exposition);
                }
            }
            
        }

        public override void Output()
        {
            
        }

        public override void ConnectTo(Component component)
        {
            if (component is Population)
                this.population = component as Population;
            if (component is PollenDepositionSampled)
                this.pollenDepositionSampled = component as PollenDepositionSampled;
        }

        public override void Dispose()
        {
            depositionData = null;
            individuals = null;

            totalEstimatedExpositionAmount = null;

            mortalityPerPopulation = null;               
        }

        public override void Finalise()
        {
            
        }


        //------------------------------------------------------------
        // Private Methods
        //------------------------------------------------------------ 
        //------------------------------------------------------------
        // Exposition part
        //------------------------------------------------------------ 

        private double[] CalclulateExposition(Individual individual)
        {

            // Calculate the Exposition per Individuals
            double[] pollenExposition = new double[individual.LarvalTime];

            for (int i = individual.BirthTime; i < individual.BirthTime + individual.LarvalTime; i++)
            {

                double muPollenAmount = depositionData[(i % 365)];             


                if (muPollenAmount == 0.0)
                {
                    pollenExposition[i - individual.BirthTime] = 0;
                }

                else
                {
                    double dailyPollenAmount;


                    if (includeLeafVariability)
                    {
                        dailyPollenAmount = generator.PollenLeafGenerator.GetNextPollenAmountOnLeaf(Math.Log(muPollenAmount));
                    }

                    else
                    {
                        dailyPollenAmount = generator.PollenLeafGenerator.GetMedianAmountOnLeaf(Math.Log(muPollenAmount));
                    }
                   

                    pollenExposition[i - individual.BirthTime] = dailyPollenAmount; // i % 365 avoids pollen exposition greater then the 364. day                   
                }

            };

            return pollenExposition;
        }

        //------------------------------------------------------------
        // Mortality Part old
        //------------------------------------------------------------
        private double CalculateMortalityPerIndividualOLD(Individual individual, int mortalityClass, double[] exposition)
        {
            int step = scenarioParameters.ExpositionDays;   
            int fullDaysCycles = (int)individual.LarvalTime / step;
            int restDays = individual.LarvalTime % step;

            

            double[] mort = new double[fullDaysCycles + 1];

            for (int i = 0; i < fullDaysCycles; i++)
            {
                double cycleAmount = 0;
                for (int j = 0 + i * step; j < step + i * step; j++)
                {
                    cycleAmount = cycleAmount + exposition[j];
                }
                mort[i] = TakeReponseType(cycleAmount / step, mortalityClass);
            }


            double restAmount = 0;
            for (int j = 0 + fullDaysCycles * step; j < restDays + fullDaysCycles * step; j++)
            {
                restAmount = restAmount + exposition[j];
            }
            mort[fullDaysCycles] = TakeReponseType(restAmount / step, mortalityClass);

            double survival = 1;
            for (int i = 0; i < fullDaysCycles + 1; i++)
            {
                survival = survival * (1 - mort[i]);
            }
            double mortality = 1 - survival;

            return mortality;
        }


        //------------------------------------------------------------
        // Mortality Part NEW  but not represented
        //------------------------------------------------------------        
        private double CalculateMortalityPerIndividualForEachDay(Individual individual, int mortalityClass, double[] exposition)
        {
            // Find maximum expostion with numberOfExosedDays
            int numberOfExposedDays = scenarioParameters.ExpositionDays;
            int maximumDaysOffset = 0;
            double sum;
            double maxSum = 0;

            for (int i = 0; i < exposition.Length-numberOfExposedDays; i++)
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
            //----           

            // Calculate the mortality per day
            double[] mortalityPerDay = new double[numberOfExposedDays];
            for (int exposedDay = 0; exposedDay < numberOfExposedDays; exposedDay++)
            {
                mortalityPerDay[exposedDay] = TakeReponseType(exposition[maximumDaysOffset + exposedDay], mortalityClass);
            }

            //----  
            // Calculate total Mortality

            double survival = 1;
            for (int exposedDay = 0; exposedDay < numberOfExposedDays; exposedDay++)
            {
                survival = survival * (1 - mortalityPerDay[exposedDay]);
            }


            double mortality = 1 - survival;
            return mortality;
        }


        //------------------------------------------------------------
        // Mortality Part NEW ... Reduced to ExpositionDays-Interval
        //------------------------------------------------------------  
        private double CalculateMortalityPerIndividual(int mortalityClass, double[] exposition)
        {
            
            switch (this.expositionIntervals)
            {
                case LepiX.InputParameters.PExposition.ExposedTimes.One:
                    {
                        return GetMortalityWithOneInterval(mortalityClass, exposition);
                    }

                case LepiX.InputParameters.PExposition.ExposedTimes.Two:
                    {
                        return GetMortalityWithTwoIntervals(mortalityClass, exposition);
                    }
                default:
                    return 0;
            }

        }


        private double GetMortalityWithOneInterval(int mortalityClass, double[] exposition)
        {
            int maximumDaysOffset = GetMaximumIntervalStartDay(exposition);
            return GetMortalityForAnInterval(maximumDaysOffset, exposition, mortalityClass);
        }

        private double GetMortalityWithTwoIntervals(int mortalityClass, double[] exposition)
        {
            //Calculates the two maximum intervals. Optional is a rest day parameter, which is set to zero by default.
            TwoIntervalCalculator calc = new TwoIntervalCalculator(exposition, numberOfExposedDays);
            int maxOffset = calc.MaxOffset;
            int secMaxOffset = calc.SecondMaxOffset;

            // Get the mortality for the exposure of each interval:
            double mortMax = GetMortalityForAnInterval(maxOffset, exposition, mortalityClass);
            double mortSecMax = GetMortalityForAnInterval(secMaxOffset, exposition, mortalityClass);

            //Allocate resuting mortality
            return (1 - (1 - mortMax) * (1 - mortSecMax));
        }



        private int GetMaximumIntervalStartDay(double[] exposition)
        {
            int maximumDaysOffset = 0;
            double sum;
            double maxSum = 0;


            for (int i = 0; i < exposition.Length - numberOfExposedDays; i++)
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

        private double GetMortalityForAnInterval(int startDay, double[] exposition, int mortalityClass)
        {
            //Calculate the mean of this array:

            double amount = 0;

            for (int i = 0; i < numberOfExposedDays; i++)
            {
                amount = amount + exposition[startDay + i];
            }

            amount = amount / numberOfExposedDays;

            this.totalExposedAmount = this.totalExposedAmount + amount / numberOfSensitivityClasses;

            double mortality = TakeReponseType(amount, mortalityClass);
            return mortality;
        }

        

        private double TakeReponseType(double pollenAmount, int i)
        {
            return ssd.DoseResponse(pollenAmount, i);
        }
    }
}
