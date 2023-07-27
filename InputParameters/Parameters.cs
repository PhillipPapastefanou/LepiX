//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
using System.Xml;


namespace LepiX.InputParameters
{
    public class Parameters
    {
        private PGeneral general;
        private PWeather weather;
        private PDepositionGeneral depositionGeneral;
        private PDepositionSampled depositionSampled;
        private PIndividualGeneral individualGeneral;
        private PIndividualBetaBirthtime individualBetaBirthtime;
        private PExposition exposition;
        private PDoseResponseGeneral doseResponse;
        private PSSDInput ssdInput;
        private PSSDOutput ssdOutput;
        private PROutput rOutput;

        #region Properties
        public PGeneral General
        {
            get { return general; }
            set { general = value; }
        }
        public PWeather Weather
        {
            get { return weather; }
            set { weather = value; }
        }
        public PDepositionGeneral DepositionGeneral
        {
            get { return depositionGeneral; }
            set { depositionGeneral = value; }
        }
        public PDepositionSampled DepositionSampled
        {
            get { return depositionSampled; }
            set { depositionSampled = value; }
        }
        public PIndividualGeneral IndividualGeneral
        {
            get { return individualGeneral; }
            set { individualGeneral = value; }
        }
        public PIndividualBetaBirthtime IndividualBetaBirthtime
        {
            get { return individualBetaBirthtime; }
            set { individualBetaBirthtime = value; }
        }
        public PDoseResponseGeneral DoseResponseGeneral
        {
            get { return doseResponse; }
            set { doseResponse = value; }
        }
        public PExposition Exposition
        {
            get { return exposition; }
            set { exposition = value; }
        }
        public PSSDInput SSDInput
        {
            get { return ssdInput; }
            set { ssdInput= value; }
        }

        public PSSDOutput SSDOutput
        {
            get { return ssdOutput; }
            set { ssdOutput = value; }
        }
        public PROutput ROutput
        {
            get { return rOutput; }
            set { rOutput = value; }
        }

        #endregion


        public Parameters()
        {
            General = new PGeneral();
            Weather = new PWeather();
            DepositionGeneral = new PDepositionGeneral();
            DepositionSampled = new PDepositionSampled();
            IndividualGeneral = new PIndividualGeneral();
            IndividualBetaBirthtime = new PIndividualBetaBirthtime();
            Exposition = new PExposition();
            DoseResponseGeneral = new PDoseResponseGeneral();
            SSDInput = new PSSDInput();
            SSDOutput = new PSSDOutput();
            ROutput = new PROutput();
        }

        public void ReadParametersFromXML(string inputFileName)
        {
            General.ReadParametersFromXML(inputFileName);
            Weather.ReadParametersFromXML(inputFileName);
            DepositionGeneral.ReadParametersFromXML(inputFileName);
            DepositionSampled.ReadParametersFromXML(inputFileName);
            IndividualGeneral.ReadParametersFromXML(inputFileName);
            IndividualBetaBirthtime.ReadParametersFromXML(inputFileName);
            Exposition.ReadParametersFromXML(inputFileName);
            DoseResponseGeneral.ReadParametersFromXML(inputFileName);
            SSDInput.ReadParametersFromXML(inputFileName);
            ROutput.ReadParametersFromXML(inputFileName);

            General.FoundValuesQ();
            Weather.FoundValuesQ();
            DepositionGeneral.FoundValuesQ();
            DepositionSampled.FoundValuesQ();
            IndividualGeneral.FoundValuesQ();
            IndividualBetaBirthtime.FoundValuesQ();
            Exposition.FoundValuesQ();
            DoseResponseGeneral.FoundValuesQ();
            SSDInput.FoundValuesQ();
            ROutput.FoundValuesQ();
        }
        public void ResetParameters()
        {
            General.SetValues();
            Weather.SetValues();
            DepositionGeneral.SetValues();
            DepositionSampled.SetValues();
            IndividualGeneral.SetValues();
            IndividualBetaBirthtime.SetValues();
            Exposition.SetValues();
            DoseResponseGeneral.SetValues();
            SSDInput.SetValues();
            ROutput.SetValues();
        }
        public void Display()
        {
            ConsoleWriteGreenLine(GetMessageWithBlank("General-Parameters"));
            General.DisplaySetup();

            ConsoleWriteGreenLine(GetMessageWithBlank("Weather-Parameters"));
            Weather.DisplaySetup();

            ConsoleWriteGreenLine(GetMessageWithBlank("Deposition-General-Parameters"));
            DepositionGeneral.DisplaySetup();

            ConsoleWriteGreenLine(GetMessageWithBlank("Deposition-Sampled-Parameters"));
            DepositionSampled.DisplaySetup();

            ConsoleWriteGreenLine(GetMessageWithBlank("Individual-General-Parameters"));
            IndividualGeneral.DisplaySetup();

            ConsoleWriteGreenLine(GetMessageWithBlank("Individual-Beta-Birthtime-Parameters"));
            IndividualBetaBirthtime.DisplaySetup();

            ConsoleWriteGreenLine(GetMessageWithBlank("Exposition-Parameters"));
            Exposition.DisplaySetup();

            ConsoleWriteGreenLine(GetMessageWithBlank("Dose-Response-Parameters"));
            DoseResponseGeneral.DisplaySetup();

            ConsoleWriteGreenLine(GetMessageWithBlank("SSD-Input"));
            SSDInput.DisplaySetup();

            if (ssdInput.CalculateSSD)
            {
                ConsoleWriteGreenLine(GetMessageWithBlank("SSD-Output"));
                SSDOutput.DisplaySetup();
            }
            
            if (general.RPostProcessing)
            {
                ConsoleWriteGreenLine(GetMessageWithBlank("ROutput"));
                ROutput.DisplaySetup();
            }
        }
        public void WriteParametersToXML(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            
            XmlWriter writer = XmlWriter.Create(filename, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("SimulationParameters");

            General.WriteParametersAsXml(writer, "General");
            Weather.WriteParametersAsXml(writer, "Weather");
            DepositionGeneral.WriteParametersAsXml(writer, "DepositionGeneral");
            DepositionSampled.WriteParametersAsXml(writer, "DepositionSampled");
            IndividualGeneral.WriteParametersAsXml(writer, "IndividualGeneral");
            IndividualBetaBirthtime.WriteParametersAsXml(writer, "IndividualBetaBirthtime");
            Exposition.WriteParametersAsXml(writer, "Exposition");
            DoseResponseGeneral.WriteParametersAsXml(writer, "DoseResponse");
            SSDInput.WriteParametersAsXml(writer, "SSD-Input");

            if (SSDInput.CalculateSSD)
            {
                SSDOutput.WriteParametersAsXml(writer, "SSD-Output");
            }

            ROutput.WriteParametersAsXml(writer, "ROutput");


            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        //--------------------------------------------------------------
        // Auxiliary
        //--------------------------------------------------------------
        #region Auxiliary
        private void ConsoleWriteGreenLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private string GetMessageWithBlank(string message)
        {
            const int consoleSize = 56;
            const string blank = "-";


            int blanklength = consoleSize - message.Length;
            string blankString = String.Empty;

            for (int i = 0; i < blanklength; i++)
            {
                blankString = blankString + blank;
            }

            return message + blankString;
        }

        #endregion
        
    }
}
