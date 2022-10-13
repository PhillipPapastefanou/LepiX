//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
using RDotNet.NativeLibrary;
using RDotNet;

using System.IO;
namespace LepiX.CallingRScripts
{
    public abstract class RScript
    {
        protected string mainXMLFileName;
        protected string rootPath;

        protected string scriptFolder;
        protected string scriptName;

        public RScript(string mainXMLFileName)
        {
            this.mainXMLFileName = mainXMLFileName;
            this.rootPath = Directory.GetCurrentDirectory();
        }

        public abstract void RunScript();
    }
}
