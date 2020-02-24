using System;
using System.Collections.Generic;
using System.Reflection;

using LepiX.PropertyGrid.Model;
using LepiX.InputParameters;

namespace LepiX.PropertyGrid.Controller
{
    public class ParameterController
    {
        ParameterModel parameterModel;
        IParametersView view;

        public ParameterModel ParameterModel
        {
            get { return parameterModel; }
            set { parameterModel = value; }
        }
        public IParametersView View
        {
            get { return view; }
            set { view = value; }
        }

        public ParameterController(ParameterModel parameterModel, IParametersView view)
        {
            this.parameterModel = parameterModel;
            this.view = view;
            view.SetController(this);

            UpdateViewFromParameters();
        }


        private void UpdateViewFromParameters()
        {
            view.ParameterModel = parameterModel;
        }

        private void UpdateParametersFromView()
        {
            if (view.ParameterModel != null)
            {
                this.parameterModel = view.ParameterModel;
            }
        }

        public void LoadXMLFile(string fileName)
        {
            this.parameterModel.ReadParameters(fileName);

            UpdateViewFromParameters();
        }


        public ComboboxItem[] GetParameterItems()
        {
            PropertyInfo[] properties = parameterModel.Parameters.GetType().GetProperties();
            List<ComboboxItem> items = new List<ComboboxItem>();
            foreach (PropertyInfo p in properties)
            {
                items.Add(new ComboboxItem(p.Name, p.GetValue(parameterModel.Parameters, null)));
            }
            return items.ToArray();
        }
                
        public void Save()
        {
            UpdateParametersFromView();
            parameterModel.Parameters.WriteParametersToXML(parameterModel.XmlFileName);
        }
        public void Save(string filename)
        {
            UpdateParametersFromView();
            parameterModel.Parameters.WriteParametersToXML(filename);
        }


        public void Run()
        {
            parameterModel.StartSimulation();
        }

    }

        public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public ComboboxItem(string Text, object Value)
        {
            this.Text = Text;
            this.Value = Value;
        }
    }
}
