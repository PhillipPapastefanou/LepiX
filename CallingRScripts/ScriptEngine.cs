//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
using RDotNet.NativeLibrary;
using RDotNet;


namespace LepiX.CallingRScripts
{
    public static class ScriptEngine
    {
        private static REngine rEngine;

        public static REngine REngine
        {
            get { return rEngine; }
            set { rEngine = value; }
        }

        public static void Setup()
        {
            //StartupParameter parameter = new StartupParameter();
            //REngine.SetEnvironmentVariables();
            //rEngine = REngine.GetInstance(null, true, parameter);

        }
        
        public static void Dispose()
        {
            //rEngine.Dispose();
        }
    }
}
