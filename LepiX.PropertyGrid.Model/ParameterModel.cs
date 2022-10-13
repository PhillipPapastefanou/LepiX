//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
using System.Diagnostics;

using LepiX.InputParameters;
using LepiX.Core;

namespace LepiX.PropertyGrid.Model
{
    public class ParameterModel
    {
        Parameters parameters;
        string xmlFileName;

        public Parameters Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }        
        public string XmlFileName
        {
            get { return xmlFileName; }
            set { xmlFileName = value; }
        }



        public ParameterModel()
        {
            this.parameters = new Parameters();
        }


        public void StartSimulation()
        {
            //ProcessStartInfo start = new ProcessStartInfo();

            //start.WorkingDirectory = Environment.CurrentDirectory;
            //start.FileName = "LepiX.exe";
            //start.WindowStyle = ProcessWindowStyle.Normal;
            //start.CreateNoWindow = false;
            //using (Process simulation = Process.Start(start))
            //{
            //    simulation.WaitForExit();
            //}


            LepiXModel.Run();
        }


        public void ReadParameters(string inputFileName)
        {
            this.xmlFileName = inputFileName;
            this.parameters.ReadParametersFromXML(inputFileName);
        }

    }
}
