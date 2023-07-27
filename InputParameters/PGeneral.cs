//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
using System.ComponentModel;

namespace LepiX.InputParameters
{
    //--------------------------------------------------------------
    //  GENERAL - Parameters
    //--------------------------------------------------------------
    public class PGeneral : Methods
    {
        public enum ModelTypes
        { Default, WorstCaseI, WorstCaseII, WorstCaseIIb, WorstCaseIII}
        public enum ExecutionMode
        { Automatic, Single, Parallel }

        private ModelTypes modelType;
        private ExecutionMode mode;                     // Execution-Mode depending on OS... careful at Linux.
        private int threads;
        private string inputLocation;
        private string outputLocation;
        private int numberOfPopulations;
        private int numberOfIndividuals;
        private int randomGeneratorSeed;                                    // Main random generator Seed.
        private bool enableConsoleRead;
        private bool rPostProcessing;
        private bool getPcInformation;

        #region Properties

        [DefaultValue(ModelTypes.Default)]
        public ModelTypes ModelType
        {
            get { return modelType; }
            set { modelType = value; }
        }

        [DefaultValue(ExecutionMode.Automatic)]
        public ExecutionMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        
        [DefaultValue(0)]
        public int Threads
        {
            get { return threads; }
            set { threads = value; }
        }
        public string InputLocation
        {
            get { return inputLocation; }
            set { inputLocation = value; }
        }
        public string OutputLocation
        {
            get { return outputLocation; }
            set { outputLocation = value; }
        }
                
        [DefaultValue(1000)]
        public int NumberOfPopulations
        {
            get { return numberOfPopulations; }
            set { numberOfPopulations = value; }
        }
               
        [DefaultValue(1000)]
        public int NumberOfIndividuals
        {
            get { return numberOfIndividuals; }
            set { numberOfIndividuals = value; }
        }

        [DefaultValue(10)]
        public int MainGeneratorSeed
        {
            get { return randomGeneratorSeed; }
            set { randomGeneratorSeed = value; }
        }
        [Unit(Unit.Units.Boolean)]
        public bool EnableConsoleRead
        {
            get { return enableConsoleRead; }
            set { enableConsoleRead = value; }
        }
        [Unit(Unit.Units.Boolean)]
        public bool RPostProcessing
        {
            get { return rPostProcessing; }
            set { rPostProcessing = value; }
        }
        [Unit(Unit.Units.Boolean)]
        public bool GetPcInformation
        {
            get { return getPcInformation; }
            set { getPcInformation = value; }
        }
        #endregion

        public override void SetValues()
        {
            mode = ExecutionMode.Single;
            randomGeneratorSeed = 10;
            modelType = ModelTypes.Default;
            inputLocation = "BadHersfeld";
            outputLocation = "BadHersfeld";
            numberOfPopulations = 1000;
            numberOfIndividuals = 1000;
            randomGeneratorSeed = 10;
            enableConsoleRead = false;
            rPostProcessing = false;
            getPcInformation = false;
        }

        public static PGeneral GetInstance()
        {
            return new PGeneral();
        }

    }
}
