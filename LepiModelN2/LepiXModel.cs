using System;
using System.Diagnostics;
using LepiX.CallingRScripts;


namespace LepiX.Core
{
    public class LepiXModel
    {
        static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {            
            ScriptEngine.Setup();

            Console.WriteLine("Starting Lepi-Model...");
            string configFileName = "InputParameters.xml";                


            Console.WriteLine("Loading UserParameters...");
            ModelParameters modelParameters = new ModelParameters(configFileName);

            if (modelParameters.General.EnableConsoleRead)
            {
                Console.ReadKey();
            }

            //------------------------------------------------------------
            // Running main model
            //------------------------------------------------------------

            Stopwatch stopwatch = new Stopwatch();

            // Setting up main model
            Console.WriteLine("Setting up model");

            Simulation lepiX = new Simulation(modelParameters);

            stopwatch.Restart();
            // Simulating main model
            lepiX.Initialise();


            Console.WriteLine("================================================================");
            Console.WriteLine("Starting simulation...");
            lepiX.Simulate();

            stopwatch.Stop();
            Console.WriteLine("================================================================");
            Console.WriteLine("Total simulation time: {0}" + stopwatch.ElapsedMilliseconds);

            //modelParameters.WriteParametersToXML("Test.xml");

            //------------------------------------------------------------
            // Going for post-processing
            //------------------------------------------------------------
            if (modelParameters.General.RPostProcessing)
            {
                Console.WriteLine("================================================================");
                Console.WriteLine("Continuing with PostProcessing...");
                modelParameters.RunRScripts();
                Console.WriteLine("Done!");
            }
            
            if (modelParameters.General.EnableConsoleRead)
            {
                Console.WriteLine("");
                Console.ReadKey();
            }

            ScriptEngine.Dispose();
        }


    }
}
