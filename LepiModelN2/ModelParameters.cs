using System;
using System.Reflection;
using System.IO;

using LepiX.InputParameters;
using LepiX.CallingRScripts;



namespace LepiX.Core
{
    public class ModelParameters: Parameters
    {     
        string mainInputFileName;
        private Parameters parameters;

        private PollenDepositionGeneralADD pollenDepositionAdd;
        private IndividualGeneralADD individualGeneralAdd;

        public PollenDepositionGeneralADD PollenDepositionAdd
        {
            get { return pollenDepositionAdd; }
        }
        public IndividualGeneralADD IndividualGeneralAdd
        {
            get { return individualGeneralAdd; }
            set { individualGeneralAdd = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }




        public class PollenDepositionGeneralADD
        {
            private int sheddingOffsetStart;
            private int sheddingOffsetEnd;
            private bool getAmountByUpper95;
            private bool getAmountBy80;

            public int SheddingOffsetStartInt
            {
                get { return sheddingOffsetStart; }
            }
            public int SheddingOffsetEndInt
            {
                get { return sheddingOffsetEnd; }
            }
            public bool GetAmountByUpper95
            {
                get { return getAmountByUpper95; }
                set { getAmountByUpper95 = value; }
            }

            public bool GetAmountBy80
            {
                get { return getAmountBy80; }
                set { getAmountBy80 = value; }
            }




            public PollenDepositionGeneralADD(Parameters parameters)
            {             
                 CalculateAdditionalParameters(parameters);
            }

            // Main ID





            private void CalculateAdditionalParameters(Parameters parameters)
           {
               try
               {
                   this.sheddingOffsetStart = AuxiliaryMethods.DateToInt(parameters.DepositionGeneral.SheddingOffsetStartDay, parameters.DepositionGeneral.SheddingOffSetStartMonth);
               }
               catch (Exception)
               {
                   throw new InvalidDateExecption(parameters.DepositionGeneral.SheddingOffsetStartDay, parameters.DepositionGeneral.SheddingOffSetStartMonth);
               }

               try
               {
                   this.sheddingOffsetEnd = AuxiliaryMethods.DateToInt(parameters.DepositionGeneral.SheddingOffSetEndDay, parameters.DepositionGeneral.SheddingOffsetEndMonth);
               }
               catch (Exception)
               {
                   throw new InvalidDateExecption(parameters.DepositionGeneral.SheddingOffSetEndDay, parameters.DepositionGeneral.SheddingOffsetEndMonth);
               }
           }

        }        
        
        public class IndividualGeneralADD
        {

            Parameters parameters;

            private int earliestLarvalOccurenceInt;

            public int EarliestLarvalOccurenceInt
            {
                get { return earliestLarvalOccurenceInt; }
                set { earliestLarvalOccurenceInt = value; }
            }
            

            public IndividualGeneralADD(Parameters parameters)
            {
                this.parameters = parameters;
                this.earliestLarvalOccurenceInt = AuxiliaryMethods.DateToInt(parameters.IndividualGeneral.EarliestLarvalOccurrenceDay, parameters.IndividualGeneral.EarliestLarvalOccurrenceMonth);
            }
            
            public void SetBirthtimeToSheddingOffset()
            {
                this.earliestLarvalOccurenceInt = AuxiliaryMethods.DateToInt(parameters.DepositionGeneral.SheddingOffsetStartDay, parameters.DepositionGeneral.SheddingOffSetStartMonth);
            }

        }

        public ModelParameters(string filename)
        {
            this.mainInputFileName = filename;

            //ObtainingParameters
            ObtainParameters();

            //Calculate Additional parameters;
            this.pollenDepositionAdd = new PollenDepositionGeneralADD(parameters);
            this.individualGeneralAdd = new IndividualGeneralADD(parameters);
            
            GeneralCalculations();

            parameters.Display();
        }


        private void ObtainParameters()
        {
            parameters = new Parameters();
            parameters.ReadParametersFromXML(mainInputFileName);
            
            if (parameters.SSDInput.CalculateSSD)
            {
                string path = Directory.GetCurrentDirectory();
                CalculateSSD();
                //Reset Working Directory


                //Use the SSD-Slope to do the dose Response Calculations
                if (parameters.SSDInput.UseSSDSlope)
                {
                    switch (parameters.DoseResponseGeneral.ResponseType)
                    {
                        case PDoseResponseGeneral.ResponseTypes.LogLogistic:
                            throw new NotImplementedException();
                        default:
                            throw new NotImplementedException();
                    }                    
                }

                Directory.SetCurrentDirectory(path);
            }
            
            //Get the properties of the input parameters
            PropertyInfo[] parameterProperties = parameters.GetType().GetProperties();

            //Write userparameters to the modelparameters!
            foreach (PropertyInfo parameter in parameterProperties)
            {
                PropertyInfo modelParamter = this.GetType().GetProperty(parameter.Name);
                modelParamter.SetValue(this, parameter.GetValue(parameters, null), null);
            }
        }


        private void CalculateSSD()
        {


            try
            {
                SSDCalculation ssdCalculation = new SSDCalculation(mainInputFileName);
                Directory.CreateDirectory("SSD");
                ssdCalculation.Run();
                parameters.SSDOutput.ReadParametersFromXML("SSD-Fit.xml");
                parameters.SSDOutput.FoundValuesQ();
                parameters.DoseResponseGeneral.LD50ClassValues = parameters.SSDOutput.EstimatedLD50Values;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not successfully run SSD-R-Script");
                Console.WriteLine("Press any key to terminate!");
                Console.ResetColor();
                Directory.Delete("SSD", true);
                Console.ReadKey();
                Environment.Exit(0);
            }
            finally
            {
                File.Delete("SSD-Fit.xml");
            }

        }

        public void RunRScripts()
        {
                if (ROutput.MortalityAnalysis)
                {
                    try
                    {
                        RMortalityAnalysis mortalityAnalysis = new RMortalityAnalysis(mainInputFileName);

                        mortalityAnalysis.RunScript();
                    }

                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }

                if (ROutput.DetailedAnalysis)
                {
                    try
                    {
                        RDetailedAnalysis detailedAnalysis = new RDetailedAnalysis(mainInputFileName);

                        switch (ROutput.DetailedDistances)
                        {
                            case PROutput.ExportDetailedDistances.All:
                                detailedAnalysis.Distances = DepositionGeneral.DistancesToMaizeField;
                                break;

                            case PROutput.ExportDetailedDistances.FirstOnly:
                                detailedAnalysis.Distances = new double[] { DepositionGeneral.DistancesToMaizeField[0] };
                                break;

                            case PROutput.ExportDetailedDistances.FirstAndLast:
                                if (DepositionGeneral.DistancesToMaizeField.Length > 1)
                                {
                                    detailedAnalysis.Distances = new double[] { DepositionGeneral.DistancesToMaizeField[0], DepositionGeneral.DistancesToMaizeField[DepositionGeneral.DistancesToMaizeField.Length - 1] };
                                }
                                else
                                {
                                    detailedAnalysis.Distances = new double[] { DepositionGeneral.DistancesToMaizeField[0] };
                                }
                                break;

                            case PROutput.ExportDetailedDistances.LastOnly:
                                detailedAnalysis.Distances = new double[] { DepositionGeneral.DistancesToMaizeField[DepositionGeneral.DistancesToMaizeField.Length - 1] };
                                break;

                            default:
                                break;
                        }

                        detailedAnalysis.RunScript();

                    }

                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        throw;
                    }

                }
            
        }

        public void GeneralCalculations()
        {
            switch (General.Mode)
            {
                case PGeneral.ExecutionMode.Automatic:
                    General.Threads = Environment.ProcessorCount;
                    break;
                case PGeneral.ExecutionMode.Single:
                    General.Threads = 1;
                    break;
                case PGeneral.ExecutionMode.Parallel:
                    break;
                default:
                    break;
            }

            this.parameters.General.Threads = General.Threads;






            //Set values corresponting to the ModelType
            switch (General.ModelType)
            {
                case PGeneral.ModelTypes.Default:
                    pollenDepositionAdd.GetAmountByUpper95 = false;
                    pollenDepositionAdd.GetAmountBy80 = false;
                    break;
                case PGeneral.ModelTypes.WorstCaseI:
                    pollenDepositionAdd.GetAmountByUpper95 = false;
                    pollenDepositionAdd.GetAmountBy80 = false;
                    individualGeneralAdd.SetBirthtimeToSheddingOffset();
                    break;
                case PGeneral.ModelTypes.WorstCaseII:
                    pollenDepositionAdd.GetAmountBy80 = false;
                    pollenDepositionAdd.GetAmountByUpper95 = true;
                    break;
                case PGeneral.ModelTypes.WorstCaseIIb:
                    pollenDepositionAdd.GetAmountByUpper95 = false;
                    pollenDepositionAdd.GetAmountBy80 = true;
                    break;
                case PGeneral.ModelTypes.WorstCaseIII:
                    individualGeneralAdd.SetBirthtimeToSheddingOffset();
                    pollenDepositionAdd.GetAmountByUpper95 = true;
                    pollenDepositionAdd.GetAmountBy80 = false;
                    break;
                default:
                    break;
            }
        }


    }







}
