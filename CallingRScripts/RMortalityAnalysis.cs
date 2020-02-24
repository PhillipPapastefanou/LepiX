using System;
using RDotNet.NativeLibrary;
using RDotNet;

using System.IO;

namespace LepiX.CallingRScripts
{
    public class RMortalityAnalysis : RScript
    {
        public RMortalityAnalysis(string mainXMLfileName)
            : base(mainXMLfileName)
        {
            this.scriptFolder = "Scripts";
            this.scriptName = "MortalityAnalysisAutomatic.R";
        }
        

        public override void RunScript()
        {
            string path = scriptFolder + "\\" + scriptName;

            CharacterVector inputFileCharacter = ScriptEngine.REngine.CreateCharacter(this.mainXMLFileName);
            ScriptEngine.REngine.SetSymbol("inputFile", inputFileCharacter);

            string command = "source('" + rootPath + "\\" + path + "')";
            command = command.Replace("\\", "/");

            ScriptEngine.REngine.Evaluate(command);
        }

    }
}
