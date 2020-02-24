//================================================================
// LepiModel: CSV input and output environment
// Lorenz Fahse, Phillip Papastefanou, 21-10-2013
//================================================================
using System;
using System.Collections.Generic;

using ComLib;
using ComLib.CsvParse;

namespace LepiX.Core
{
    class CSV
    {
        //------------------------------------------------------------
        // Data-Import
        //
        // imports csv-files and returns a double array
        //------------------------------------------------------------

        private static double[,] TempData;
        private static double[] singleColumnData;
        private static int colCount;
        private static int rowCount;

        public static double[,] Import(string fileName, bool headers, bool readOnly, char delimiter)
        {
            CsvDoc csv = Csv.Load(fileName, headers, readOnly, delimiter);

            colCount = csv.Columns.Count;
            rowCount = csv.Data.Count;


            TempData = new double[rowCount, colCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    if (col == 0) //the date is in the first column. dates cannot convert into double's, so there will be an index for every date starting by 0 for 1/1/s.
                    {
                        TempData[row, col] = row;
                    }


                    else //to rest of the array is calculatable data, so its converted into double.
                    {
                        TempData[row, col] = Convert.ToDouble(csv.Data[row][col], System.Globalization.CultureInfo.InvariantCulture);
                    }

                }
            }

            return TempData;
        }


        //------------------------------------------------------------
        // Data-Import
        //
        // imports csv-files and returns a double array
        //------------------------------------------------------------


        // Import overlaod 
        // imports a single columend file
        public static double[] ImportS(string fileName, bool headers, bool readOnly, char delimiter)
        {
            CsvDoc csv = Csv.Load(fileName, headers, readOnly, delimiter);

            rowCount = csv.Data.Count;

            singleColumnData = new double[rowCount];
            for (int row = 0; row < rowCount; row++)
            {
                singleColumnData[row] = Convert.ToDouble(csv.Data[row][0], System.Globalization.CultureInfo.InvariantCulture);
            }

            return singleColumnData;
        }

        // Get the number of columns
        public static int ColCount()
        {
            return colCount;
        }

        // Get the number of rows
        public static int RowCount()
        {
            return rowCount;
        }



        //------------------------------------------------------------
        // Data-Export
        //
        // Exports csv-data. needs an array as import. Currentky works with 2 headers.
        //------------------------------------------------------------

        //public static void ExportI(List<Individual> individuals, string exportName)
        //{
        //    var cols = new List<string>() { "ID", "BirthTime", "LarvalTime", "Exposition Amount (Pollen/cm^2)" }; //Header

        //    var dataList = new List<List<object>>();    //Data

        //    for (int i = 0; i < individuals.Count; i++)
        //    {
        //        dataList.Add(new List<object>() {
        //            individuals[i].PID.ToString(System.Globalization.CultureInfo.InvariantCulture),
        //            individuals[i].ID.ToString(System.Globalization.CultureInfo.InvariantCulture),
        //            individuals[i].BirthTime.ToString(System.Globalization.CultureInfo.InvariantCulture),
        //            individuals[i].LarvalTime.ToString(System.Globalization.CultureInfo.InvariantCulture),
        //            individuals[i].TotalPollenExposition.ToString(System.Globalization.CultureInfo.InvariantCulture)
        //            }
        //            );
        //    }

        //    Csv.Write(exportName, dataList, ",", cols, true, false, "", Environment.NewLine, true);

        //}


        //------------------------------------------------------------
        // Finalise-Export
        //
        // Exports the average individual data per population 
        //------------------------------------------------------------

        public static void ExportArray(double[,] multiArray, List<string> header, string exportName)
        {
            var dataList = new List<List<object>>();

            for (int i = 0; i < multiArray.GetLength(0); i++)
            {

                var subDataList = new List<object>();

                for (int v = 0; v < multiArray.GetLength(1); v++)
                {
                    subDataList.Add(multiArray[i, v].ToString(System.Globalization.CultureInfo.InvariantCulture));
                }

                dataList.Add(subDataList);

            }

            Csv.Write(exportName, dataList, ",", header, false, false, "", Environment.NewLine, false);
        }

        public static void ExportArray(int[,] multiArray, List<string> header, string exportName)
        {
            var dataList = new List<List<object>>();

            for (int i = 0; i < multiArray.GetLength(0); i++)
            {

                var subDataList = new List<object>();

                for (int v = 0; v < multiArray.GetLength(1); v++)
                {
                    subDataList.Add(multiArray[i, v].ToString(System.Globalization.CultureInfo.InvariantCulture));
                }

                dataList.Add(subDataList);

            }

            Csv.Write(exportName, dataList, ",", header, false, false, "", Environment.NewLine, false);
        }



        public static void ExportArray(double[] singleArray, List<string> header, string exportName)
        {
            var dataList = new List<List<object>>();

                for (int v = 0; v < singleArray.Length; v++)
                {
                    var subDataList = new List<object>();

                    subDataList.Add(singleArray[v].ToString(System.Globalization.CultureInfo.InvariantCulture));

                    dataList.Add(subDataList);
                }                

            Csv.Write(exportName, dataList, ",", header, false, false, "", Environment.NewLine, false);

        }


    }
}
