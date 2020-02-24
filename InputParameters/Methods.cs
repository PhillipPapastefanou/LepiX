using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace LepiX.InputParameters
{
    public abstract class Methods
    {
        protected Dictionary<string, bool> foundParams = new Dictionary<string, bool>();
        //---------------------------------------
        // Methods
        //---------------------------------------
        public abstract void SetValues();
        public void WriteParametersAsXml(XmlWriter writer, string subNodeTitle)
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();

            writer.WriteStartElement(subNodeTitle);

            foreach (PropertyInfo property in properties)
            {

                if (property.PropertyType == typeof(Species[]))
                {
                    writer.WriteStartElement(property.Name.ToString());

                    string unit = GetAttributeUnit(property);          

                    var array = (Species[])property.GetValue(this, null);

                    if (array != null)
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            writer.WriteStartElement("Value");

                            writer.WriteAttributeString("Name", Convert.ToString(array[i].Name, System.Globalization.CultureInfo.InvariantCulture));
                            writer.WriteValue(Convert.ToString(array[i].LD50, System.Globalization.CultureInfo.InvariantCulture));

                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                    }
                }

                else if(property.PropertyType.IsArray)
                {
                    writer.WriteStartElement(property.Name.ToString());

                    string unit = GetAttributeUnit(property);
                    if (unit != String.Empty)
                    {
                        writer.WriteAttributeString("Unit", unit);
                    }

                        var array = (Array)property.GetValue(this, null);

                        if (array != null)
                        {
                            for (int i = 0; i < array.Length; i++)
                            {
                                writer.WriteElementString("Value", Convert.ToString(array.GetValue(i), System.Globalization.CultureInfo.InvariantCulture));
                            }

                            writer.WriteEndElement();
                        }
                }

                else
                {
                    writer.WriteStartElement(property.Name);
                    string unit = GetAttributeUnit(property);
                    if (unit != String.Empty)
                    {
                        writer.WriteAttributeString("Unit", unit);
                    }
                    writer.WriteString(Convert.ToString(property.GetValue(this, null), System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteEndElement();

                    //writer.WriteElementString(property.Name, Convert.ToString(property.GetValue(this, null), System.Globalization.CultureInfo.InvariantCulture));

                }
            }

            writer.WriteEndElement();
        }
        public void ReadParametersFromXML(string fileName)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();


            // per default all parameters are NOT found.
            foreach (PropertyInfo propery in properties)
            {
                foundParams.Add(propery.Name, false);
            }

            using (XmlReader reader = XmlReader.Create(fileName, settings))
            {

                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        string name = reader.Name;

                        foreach (PropertyInfo property in properties)
                        {
                            if (name == property.Name.ToString())
                            {
                                foundParams[property.Name] = true;


                                if (reader.Read())
                                {
                                    ParseToValue(property, reader.Value);

                                    ParseToArrayValue(property, reader);
                                }
                            }
                        }

                    }
                }



            }
        }
        public void FoundValuesQ()
        {
            bool valueNotFound = false;

            foreach (KeyValuePair<string, bool> pair in foundParams)
            {
                if (pair.Value == false)
                {
                      valueNotFound = true;
                      Console.ForegroundColor = ConsoleColor.Red;
                      Console.WriteLine("Could not find Parameter {0} in {1}!.", pair.Key, this.GetType());
                }
            }

            if (valueNotFound)
            {
                Console.WriteLine("Press any key to terminate program.!");
                Console.ResetColor();
                Console.ReadKey();
                Environment.Exit(0);
            }

        }
        public void DisplaySetup()
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                switch (property.PropertyType.ToString())
                {
                    case ("System.Int32[]"):
                        Console.Write("{0}: ", property.Name);
                        var arrayI = (Array)property.GetValue(this, null);
                        for (int i = 0; i < arrayI.Length; i++)
                        {
                            Console.Write(" " + arrayI.GetValue(i));
                        }
                        Console.WriteLine();
                        break;

                    case ("System.Double[]"):
                        Console.Write("{0}: ", property.Name);
                        var arrayD = (Array)property.GetValue(this, null);
                        for (int i = 0; i < arrayD.Length; i++)
                        {
                            Console.Write(" " + Convert.ToString(arrayD.GetValue(i), System.Globalization.CultureInfo.InvariantCulture));
                        }
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("{0}: {1}", property.Name, Convert.ToString(property.GetValue(this, null), System.Globalization.CultureInfo.InvariantCulture));
                        break;
                }
            }
        }
        private void ParseToValue(PropertyInfo property, string value)
        {
            Type typePROP = property.PropertyType;

            if (typePROP == typeof(System.Int32))
            {
                try
                {
                    property.SetValue(this, Convert.ToInt32(value, System.Globalization.CultureInfo.InvariantCulture));
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid integer value obtainted from parameter {0}, by the XML!", property.Name);
                    Console.WriteLine("Press any key to terminate programm!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    throw;
                }
            }

            else if (typePROP == typeof(System.Double))
            {
                try
                {
                    property.SetValue(this, Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture));
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid double value obtainted from parameter {0}, by the XML!", property.Name);
                    Console.WriteLine("Press any key to terminate programm!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    throw;
                }
            }

            else if (typePROP == typeof(bool))
            {
                try
                {
                    property.SetValue(this, Convert.ToBoolean(value, System.Globalization.CultureInfo.InvariantCulture));
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid boolean value obtainted from parameter {0}, by the XML!", property.Name);
                    Console.WriteLine("Press any key to terminate programm!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    throw;
                }
            }

            else if (typePROP == typeof(string))
            {
                try
                {
                    property.SetValue(this, Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture));
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid string value obtainted from parameter {0}, by the XML!", property.Name);
                    Console.WriteLine("Press any key to terminate programm!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    throw;
                }
            }

            else if (typePROP == typeof(char))
            {
                try
                {
                    property.SetValue(this, Convert.ToChar(value, System.Globalization.CultureInfo.InvariantCulture));
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid char value obtainted from parameter {0}, by the XML!", property.Name);
                    Console.WriteLine("Press any key to terminate programm!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    throw;
                }
            }

            else if (typePROP.IsEnum)
            {
                try
                {
                    property.SetValue(this, (Enum)Enum.Parse((Type)typePROP, value));
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid {0} value obtainted from parameter {1}, by the XML!", typePROP.Name, property.Name);
                    string[] names = typePROP.GetEnumNames();
                    string name = string.Join(" ", names);
                    Console.WriteLine("Possible values are: {0}", name);
                    Console.WriteLine("Press any key to terminate programm!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    throw;
                }
            }
        }
        private void ParseToArrayValue(PropertyInfo property, XmlReader reader)
        {
            Type typePROP = property.PropertyType;

            if (typePROP == typeof(System.Int32[]))
            {

                try
                {
                    List<int> list = new List<int>();

                    while (reader.Name == "Value")
                    {
                        if (reader.Read())
                        {
                            list.Add(Convert.ToInt32(reader.Value, System.Globalization.CultureInfo.InvariantCulture));

                            reader.Read();
                            reader.Read();
                        }
                    }

                    int[] arr = list.ToArray();
                    property.SetValue(this, arr);

                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid interger array obtainted from parameter {0}, by the XML!", property.Name);
                    Console.WriteLine("Press any key to terminate programm!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    throw;
                }
            }

            else if (typePROP == typeof(System.Double[]))
            {
                try
                {
                    List<double> list = new List<double>();

                    while (reader.Name == "Value")
                    {
                        if (reader.Read())
                        {
                            list.Add(Convert.ToDouble(reader.Value, System.Globalization.CultureInfo.InvariantCulture));

                            reader.Read();
                            reader.Read();
                        }
                    }


                    if (list.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No values obtained for parameter {0}, by the XML!", property.Name);
                        Console.WriteLine("Press any key to terminate programm!");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }

                    double[] arr = list.ToArray();
                    property.SetValue(this, arr);

                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid double array obtainted from parameter {0}, by the XML!", property.Name);
                    Console.WriteLine("Press any key to terminate programm!");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }


            else if (typePROP == typeof(Species[]))
            {
                List<Species> list = new List<Species>();

                while (reader.Name == "Value")
                {
                    Species s = new Species();
                    s.Name = Convert.ToString(reader.GetAttribute("Name"));

                    reader.Read();

                    s.LD50 = Convert.ToDouble(reader.Value);

                    list.Add(s);

                    reader.Read();
                    reader.Read();
                }

                Species[] arr = list.ToArray();
                property.SetValue(this, arr);

            }
            

        }
        private string GetAttributeUnit(PropertyInfo property)
        {
            string unit = String.Empty;
            var attributes = property.GetCustomAttributes();
            foreach (Attribute arr in attributes)
            {
                if (arr is Unit)
                {
                    Unit s = arr as Unit;
                    unit = s.U.ToString();
                    unit = unit.Replace("_", " ");
                    unit = unit.ToLower();
                }
            }

            return unit;
        }
    }
}
