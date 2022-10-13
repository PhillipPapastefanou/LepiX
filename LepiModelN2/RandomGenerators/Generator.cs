//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================

namespace LepiX.Core.RandomGenerators
{
    class Generator
    {
        //------------------------------------------------------------
        // ModelParamters reference
        //------------------------------------------------------------
        ModelParameters modelParameters;
        ScenarioParameters scenarioParameters;

        //------------------------------------------------------------
        // Random generator references
        //------------------------------------------------------------
        UniformRandom uniformRandom;
        BirthtimeGenerator birthTimeGenerator;
        PollenAirGenerator pollenAirGenerator;
        PollenLeafGenerator pollenLeafGenerator;

        public PollenLeafGenerator PollenLeafGenerator
        { get { return pollenLeafGenerator; } }
        public PollenAirGenerator PollenAirGenerator
        { get { return pollenAirGenerator; } }
        public BirthtimeGenerator BirthTimeGenerator
        { get { return birthTimeGenerator; } }
        public UniformRandom UniformRandom
        { get { return uniformRandom; } }


        public Generator(ModelParameters modelParameters, ScenarioParameters scenarioParameters)
        {
            this.modelParameters = modelParameters;
            this.scenarioParameters = scenarioParameters;


            // Setting up uniform random generator.
            uniformRandom = new UniformRandom(scenarioParameters.ScenarioSeed);

            // Setting up the betaGenerator for the birthtime.
            birthTimeGenerator = new BirthtimeGenerator(modelParameters, uniformRandom.Next(), uniformRandom.Next());

            // Setting up the logNormalGenerator for the pollenTotalValue
            pollenAirGenerator = new PollenAirGenerator(modelParameters, uniformRandom.Next(), scenarioParameters.DistanceToMaizeField);

            // Setting up the Pollen Leaf Generator for the Pollen Amount on the different plants.
            pollenLeafGenerator = new PollenLeafGenerator(modelParameters, uniformRandom.Next());
        }

    }
}
