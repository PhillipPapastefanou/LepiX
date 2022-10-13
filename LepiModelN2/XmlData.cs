//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================

using System;
using System.Xml;
using System.IO;

namespace LepiX.Core
{
    class XmlData
    {
        string xmlFileName;
        string paramType;

        XmlDocument xmlDoc;
        XmlElement xmlRootElem;
        XmlNodeList xmlNodeList;

        //------------------------------------------------------------
        //Constructor
        //------------------------------------------------------------
        public XmlData(string paramType, string xmlFileName)
        {
            this.xmlFileName = xmlFileName;
            this.paramType = paramType;

            //XML document object model
            xmlDoc = new System.Xml.XmlDocument();

            //Loading XML data file as document object model
            xmlDoc.Load(xmlFileName);

            //Get root-elments of XML file
            xmlRootElem = xmlDoc.DocumentElement;

            //Extraction of the XML-node list that corresponds to key element
            xmlNodeList = xmlRootElem.GetElementsByTagName(paramType);


            //Tests whether the the the parameter type 'paramType'is found
            //in the file 'xmlFileName'
            if (xmlNodeList == null)
                throw new ParamTypeNotFoundException(paramType);
        }


        //------------------------------------------------------------
        //Properties
        //------------------------------------------------------------
        public string XmlFileName
        {
            get { return xmlFileName; }
            set { xmlFileName = value; }
        }
        public string ParamType
        {
            get { return paramType; }
            set { paramType = value; }
        }

        //------------------------------------------------------------
        //Reading INT-parameter
        //------------------------------------------------------------
        public int ReadInt(string paramName)
        {
            int intValue = 0;
            bool paramFound = false;

            foreach (XmlNode childNode in xmlNodeList[0].ChildNodes)
                if (childNode.Name == "Parameter" && childNode.Attributes["name"].Value == paramName)
                {

                    paramFound = true;
                    intValue = Convert.ToInt32(childNode.Attributes["value"].Value);

                }
            if (!paramFound)
                throw new ParamDataNotFoundException(paramName);

            return intValue;
        }


        //------------------------------------------------------------
        //Reading DOUBLE-parameter 
        //------------------------------------------------------------
        public double ReadDouble(string paramName)
        {
            double doubleValue = 0.0;
            bool paramFound = false;

            foreach (XmlNode childNode in xmlNodeList[0].ChildNodes)
                if (childNode.Name == "Parameter" && childNode.Attributes["name"].Value == paramName)
                {
                    paramFound = true;
                    doubleValue = Convert.ToDouble(childNode.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
                }
            if (!paramFound)
                throw new ParamDataNotFoundException(paramName);

            return doubleValue;
        }


        //------------------------------------------------------------
        //Reading STRING-parameter 
        //------------------------------------------------------------
        public string ReadString(string paramName)
        {
            string strValue = "empty";
            bool paramFound = false;

            foreach (XmlNode childNode in xmlNodeList[0].ChildNodes)
                if (childNode.Name == "Parameter" && childNode.Attributes["name"].Value == paramName)
                {
                    paramFound = true;
                    strValue = String.Copy(childNode.Attributes["value"].Value);
                }
            if (!paramFound)
                throw new ParamDataNotFoundException(paramName);

            return strValue;
        }


        //------------------------------------------------------------
        //Reading CHAR-parameter 
        //------------------------------------------------------------
        public char ReadChar(string paramName)
        {
            char chrValue = ' ';
            bool paramFound = false;

            foreach (XmlNode childNode in xmlNodeList[0].ChildNodes)
                if (childNode.Name == "Parameter" && childNode.Attributes["name"].Value == paramName)
                {
                    paramFound = true;
                    chrValue = Convert.ToChar(childNode.Attributes["value"].Value);
                }
            if (!paramFound)
                throw new ParamDataNotFoundException(paramName);

            return chrValue;
        }


        //------------------------------------------------------------
        //Reading BOOL-parameter 
        //------------------------------------------------------------
        public bool ReadBoolean(string paramName)
        {
            bool boolValue = false;
            bool paramFound = false;

            foreach (XmlNode childNode in xmlNodeList[0].ChildNodes)
                if (childNode.Name == "Parameter" && childNode.Attributes["name"].Value == paramName)
                {
                    paramFound = true;
                    boolValue = Convert.ToBoolean(childNode.Attributes["value"].Value);
                }
            if (!paramFound)
                throw new ParamDataNotFoundException(paramName);

            return boolValue;
        }
    }
}
