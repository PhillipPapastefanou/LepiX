using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

using LepiX.PropertyGrid.Model;
using LepiX.PropertyGrid.Controller;
using System.IO;


using System.Globalization;
using System.Resources;
using System.Reflection;
using Microsoft.Win32;


namespace LepiX.PropertyGrid.View
{
    public partial class ParameterView : Form, IParametersView
    {

        ParameterController controller;
        ParameterModel parameterModel;

        string xmlFileName = "InputParameters.xml";
        string manualFileName = "bin//Manual.pdf";


        Assembly viewAssembly;
        CultureInfo ci;
        ResourceManager rm;

        public ParameterModel ParameterModel
        {
            get { return parameterModel; }
            set { parameterModel = value; }
        }

        public ParameterView()
        {
            InitializeComponent();
        }

        public void SetController(ParameterController controller)
        {
            this.controller = controller;            
        }

        private void ParameterView_Load(object sender, EventArgs e)
        {           
            //German is default language
            viewAssembly = Assembly.Load("LepiX.PropertyGrid.View");
            ci = new CultureInfo("de-DE");
            rm = new ResourceManager("LepiX.PropertyGrid.View.Resources.Res", viewAssembly);

            //Set Initial Labels 
            SetButtonandLabelNames();
            
            //Load XML-File
            LCXMLFile();         
                        
            //Check and Load PDF Manual file.
            LCPDFFile();
        }

        private void comboBoxOverview_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.propertyGridParameters.SelectedObject = (comboBoxOverview.SelectedItem as ComboboxItem).Value;
        }

        private void propertyGridParameters_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            if (checkBoxUpdatePDF.Checked)
            {
                this.axAcroPDFManual.setZoom(100);
                this.axAcroPDFManual.setNamedDest(this.propertyGridParameters.SelectedGridItem.Label.ToString());
            }
        }

        private void checkBoxUpdatePDF_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUpdatePDF.Checked && this.propertyGridParameters.SelectedGridItem != null)
            {
                this.axAcroPDFManual.setZoom(100);
                this.axAcroPDFManual.setNamedDest(this.propertyGridParameters.SelectedGridItem.Label.ToString());
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.controller.Save();
        }

        private void buttonSaveAndRun_Click(object sender, EventArgs e)
        {
            DisableButtons();
            
            string path = Directory.GetCurrentDirectory();

            this.controller.Save();

            backgroundWorkerSimulate = new BackgroundWorker();
            backgroundWorkerSimulate.DoWork += BackgroundWorkerSimulate_DoWork;
            backgroundWorkerSimulate.RunWorkerAsync();

            Directory.SetCurrentDirectory(path);

            backgroundWorkerSimulate.Dispose();


            EnableButtons();
        }

        private void BackgroundWorkerSimulate_DoWork(object sender, DoWorkEventArgs e)
        {
            this.controller.Run();
        }

        private void buttonLoadXMLFile_Click(object sender, EventArgs e)
        {
            openFileDialogXMLFile = new OpenFileDialog();
            openFileDialogXMLFile.Filter= "XML files|*.xml";
            openFileDialogXMLFile.InitialDirectory = Directory.GetCurrentDirectory();


            if (openFileDialogXMLFile.ShowDialog()== DialogResult.OK)
            {
                this.controller.ParameterModel = new ParameterModel();

                this.richTextBoxXMLFileName.Text = Path.GetFileNameWithoutExtension(openFileDialogXMLFile.FileName);
                this.xmlFileName = openFileDialogXMLFile.FileName;

                this.controller.LoadXMLFile(xmlFileName);
                this.comboBoxOverview.Items.AddRange(controller.GetParameterItems());
            }            
        }


        
        // Private Methods 

        private void DisableButtons()
        {
            this.buttonSave.Enabled = false;
            this.buttonSaveAndRun.Enabled = false;

            this.comboBoxOverview.Enabled = false;
            this.propertyGridParameters.Enabled = false;
        }

        private void EnableButtons()
        {
            this.buttonSave.Enabled = true;
            this.buttonSaveAndRun.Enabled = true;

            this.comboBoxOverview.Enabled = true;
            this.propertyGridParameters.Enabled = true;
        }


        private void LCPDFFile()
        {
            if (File.Exists(manualFileName))
            {
                try
                {
                    this.labelManualnotFound.Visible = false;
                    this.axAcroPDFManual.CreateControl();
                    this.axAcroPDFManual.LoadFile(manualFileName);
                    this.axAcroPDFManual.setPageMode("none");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }

            else
            {
                this.labelManualnotFound.Text = rm.GetString("pdfNotFoundMessage", ci);
            }
        }

        private void LCXMLFile()
        {
            if (File.Exists(xmlFileName))
            {
                this.controller.LoadXMLFile(xmlFileName);
                this.comboBoxOverview.Items.AddRange(controller.GetParameterItems());
                this.richTextBoxXMLFileName.Text = xmlFileName;
            }

            else
            {
                MessageBox.Show("No XML-File found.");
                Environment.Exit(0);
            }
        }



        private void SetButtonandLabelNames()
        {
            this.groupBoxParameters.Text = rm.GetString("groupParametersName",ci);
            this.groupBoxManual.Text = rm.GetString("groupManualName",ci);
            this.buttonSave.Text = rm.GetString("saveButtonName",ci);
            this.buttonSaveAndRun.Text = rm.GetString("saveAndExecuteButtonName", ci);
            this.checkBoxUpdatePDF.Text = rm.GetString("checkboxUpdatePDFName",ci);
            //this.buttonLoadXMLFile.Text = rm.GetString("loadButtonName", ci);
        }

        private void backgroundWorkerSimulate_DoWork_1(object sender, DoWorkEventArgs e)
        {

        }
    }




	
}
