using System;
using RDotNet.NativeLibrary;
using RDotNet;

using System.IO;


namespace LepiX.CallingRScripts
{
    public class SSDCalculation
    {
        REngine engine;

        string scriptName;
        string mainXMLfile;
        string path = Directory.GetCurrentDirectory();        

        public SSDCalculation(string mainXMLfileName)
        {
            this.mainXMLfile = mainXMLfileName;
            this.engine = REngine.GetInstance();
        }


        public void Run()
        {
            this.scriptName = "Scripts\\SSD-FIT.R";

            CharacterVector inputFileCharacter = engine.CreateCharacter(mainXMLfile);
            engine.SetSymbol("inputFile", inputFileCharacter);
            
            string command = "source('" + path + "\\" + scriptName + "')";
            command = command.Replace("\\", "/");

            engine.Evaluate(command);
        }
    }
}
