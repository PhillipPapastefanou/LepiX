using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace LepiX.Core
{
    public class Simulation
    {
       List<Scenario> scenarios;

       ModelParameters modelParameters;
       List<ScenarioParameters> scenariolParametersList;

        private double[] distances;
        private int expositionDays;
        string outputLocation;

        public Simulation(ModelParameters modelParameters)
        {
            this.modelParameters = modelParameters;     

            //Reading the distances and ThresholdDays  
            Console.WriteLine("Reading distances and threshlolddays...");
            this.distances = modelParameters.DepositionGeneral.DistancesToMaizeField;
            this.expositionDays = modelParameters.Exposition.ExpositionDays;
            this.outputLocation = modelParameters.General.OutputLocation;



            Console.WriteLine("Number of distances: {0}", distances.Length);
            Console.WriteLine("Total number of scenarios to calculate: {0}", distances.Length);

            
            scenarios = new List<Scenario>();
            scenariolParametersList = new List<ScenarioParameters>();

            Random generalSeedGenerator = new Random(modelParameters.General.MainGeneratorSeed);

            for (int i = 0; i < distances.Length; i++)
            {
                scenariolParametersList.Add(new ScenarioParameters(distances[i], expositionDays, generalSeedGenerator.Next()));
            }

        }

        public void Initialise()
        {

            //Creating output directory
            //Exporting ParameterSetup 
            Directory.CreateDirectory("Output");
            Directory.CreateDirectory("Output/" + outputLocation);
            this.modelParameters.WriteParametersToXML("Output/" +  outputLocation+  "/InputParameters.xml");

            for (int i = 0; i < scenariolParametersList.Count; i++)
            {
                scenarios.Add(new Scenario(modelParameters, scenariolParametersList[i]));

                //Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Initialising scenario number: {0}", i + 1);
            }

           // Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("Initialising scenarios: Done!");
            Console.WriteLine();
        }


        // main simulation loop
        public void Simulate()
        {


            switch (modelParameters.General.Mode)
            {
                case LepiX.InputParameters.PGeneral.ExecutionMode.Automatic:
                    ParallelPartionerSimulate(modelParameters.General.Threads);
                    break;
                case LepiX.InputParameters.PGeneral.ExecutionMode.Single:
                    SingleSimulate();
                    break;
                case LepiX.InputParameters.PGeneral.ExecutionMode.Parallel:
                    ParallelPartionerSimulate(modelParameters.General.Threads);
                    break;
                default:
                    break;
            }
        }


        private void SingleSimulate()
        {
            foreach (Scenario scenario in scenarios)
            {

                Stopwatch sw = new Stopwatch();

                sw.Restart();

                scenario.Initialise();

                scenario.Simulate();

                scenario.Finalise();

                scenario.Export();

                scenario.Dispose();

                sw.Stop();

                Console.WriteLine("distance: {0} , threshold: {1}, eplasepd time: {2} ", scenario.ScenarioParameters.DistanceToMaizeField, scenario.ScenarioParameters.ExpositionDays, sw.ElapsedMilliseconds);
                Console.WriteLine("Ram:"+ GC.GetTotalMemory(true)/1024/1024);
            }
        }

        readonly object locker = new object();
        int succes;


        private void ParallelPartionerSimulate(int cores)
        {
            Stopwatch sw = Stopwatch.StartNew();

            int time = 0;
            Parallel.ForEach(Partitioner.Create(0, scenarios.Count, ((int)(scenarios.Count / cores) + 1)), range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    scenarios[i].Initialise();

                    scenarios[i].Simulate();

                    scenarios[i].Finalise();

                    scenarios[i].Export();

                    scenarios[i].Dispose();

                    lock (locker)
                    {
                        succes = succes + 1;
                        time = (int)sw.ElapsedMilliseconds / 1000;

                        //Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write("Succesfully simulated {0} Scenarios. {1} remaining. Elapsed time {2}s ", succes, scenarios.Count - succes, time);
                        Console.WriteLine("Ram:" + GC.GetTotalMemory(true) / 1024 / 1024);
                    }
                }
            });

            Console.WriteLine("");
            Console.WriteLine("Succesfully simulated all Scenarios!");
        }


        private void WorkWithTasks(Task[] tasks)
        {

            int numberOfLogicalCores = tasks.Length;

            int[] numberOfRunsPerTask = AuxiliaryMethods.DistributeRunsToTask(scenarios.Count, numberOfLogicalCores);

            int[] mins = new int[numberOfLogicalCores];
            int[] maxs = new int[numberOfLogicalCores];

            int min = 0;
            int max = 0;

            for (int i = 0; i < numberOfLogicalCores; i++)
            {
                min = max;
                max = min + numberOfRunsPerTask[i];

                mins[i] = min;
                maxs[i] = max;
            }



            for (int i = 0; i < numberOfLogicalCores; i++)
            {
                int j = i;
                tasks[i] = new Task(() => RunScenarios(mins[j], maxs[j]));
            }

            foreach (Task task in tasks)
            {
                task.Start();
            }


        }






        private void RunScenarios(int i0, int i1)
        {

            for (int i = i0; i < i1; i++)
            {
                Stopwatch sw = new Stopwatch();

                sw.Restart();

                scenarios[i].Initialise();

                scenarios[i].Simulate();

                scenarios[i].Finalise();

                scenarios[i].Export();

                scenarios[i].Dispose();


                sw.Stop();

                Console.WriteLine("distance: {0} , threshold {1}, eplasepd time {2} ms ", scenarios[i].ScenarioParameters.DistanceToMaizeField, scenarios[i].ScenarioParameters.ExpositionDays, sw.ElapsedMilliseconds);
            }

        }






    }
}
