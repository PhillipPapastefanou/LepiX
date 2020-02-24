
namespace LepiX.Core
{
    using LepiX.Core.RandomGenerators;

    abstract class Component
    {
        //------------------------------------------------------------
        // Generic component variables
        //------------------------------------------------------------
        protected int pID;

        protected ModelParameters modelParameters;
        protected ScenarioParameters scenarioParameters;
        protected InputData inputData;
        protected Generator generator;
        
        //------------------------------------------------------------
        // Constructor
        //------------------------------------------------------------
        public Component(int pID)
        {
            this.pID = pID;
        }

        //------------------------------------------------------------
        // properties
        //------------------------------------------------------------
        public int PID
        {
            get { return pID; }
            set { pID = value; }
        }

        //------------------------------------------------------------
        // Base class methods
        //------------------------------------------------------------

        public void SetModelParamters(ModelParameters modelParameters)
        {
            this.modelParameters = modelParameters;
        }

        public void SetInputData(InputData inputData)
        {
            this.inputData = inputData;
        }

        public void SetRandomGenerators(Generator generator)
        {
            this.generator = generator;
        }

        //------------------------------------------------------------
        // Component interface methods
        //------------------------------------------------------------

        // Setting initial state and parameter
        public abstract void Initialise(Scenario scenario);

        // Get model input within a time step
        public abstract void Input();

        // Discrete model update within a time step
        public abstract void Update();

        // Calculate model output within a time step
        public abstract void Output();

        // Connect sub-model (backwards) to the specified sub-model
        public abstract void ConnectTo(Component component);

        // Dispose all recources hold by the sub-models
        public abstract void Dispose();

        // Performs final tasks for a sub-model at the end of the simulation
        public abstract void Finalise();

    }
}
