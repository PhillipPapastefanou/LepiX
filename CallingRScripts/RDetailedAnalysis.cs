using System;
using RDotNet.NativeLibrary;
using RDotNet;

using System.IO;

namespace LepiX.CallingRScripts
{
    public class RDetailedAnalysis:RScript
    {

        private double[] distances;
        public double[] Distances
        {
            get { return distances; }
            set { distances = value; }
        }

        public RDetailedAnalysis(string mainXMLfileName)
            : base(mainXMLfileName)
        {
            this.scriptFolder = "Scripts";
            this.scriptName = "DetailedAnalysisAutomatic.R";
        }

        public override void RunScript()
        {
            string path = scriptFolder + "\\" + scriptName;
            
            CharacterVector inputFileCharacter = ScriptEngine.REngine.CreateCharacter(this.mainXMLFileName);
            ScriptEngine.REngine.SetSymbol("inputFile", inputFileCharacter);

            NumericVector rDistances = ScriptEngine.REngine.CreateNumericVector(distances.Length);
            for (int i = 0; i < distances.Length; i++)
            {
                rDistances[i] = distances[i];
            }
            ScriptEngine.REngine.SetSymbol("distances", rDistances);

            string command = "source('" + rootPath + "\\" + path + "')";
            command = command.Replace("\\", "/");

            ScriptEngine.REngine.Evaluate(command);
        }
    }
}
