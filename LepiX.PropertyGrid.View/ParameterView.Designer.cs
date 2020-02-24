namespace LepiX.PropertyGrid.View
{
    partial class ParameterView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.propertyGridParameters = new System.Windows.Forms.PropertyGrid();
            this.comboBoxOverview = new System.Windows.Forms.ComboBox();
            this.groupBoxManual = new System.Windows.Forms.GroupBox();
            this.labelManualnotFound = new System.Windows.Forms.Label();
            this.axAcroPDFManual = new AxAcroPDFLib.AxAcroPDF();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSaveAndRun = new System.Windows.Forms.Button();
            this.checkBoxUpdatePDF = new System.Windows.Forms.CheckBox();
            this.richTextBoxXMLFileName = new System.Windows.Forms.RichTextBox();
            this.openFileDialogXMLFile = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorkerSimulate = new System.ComponentModel.BackgroundWorker();
            this.groupBoxParameters.SuspendLayout();
            this.groupBoxManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDFManual)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.Controls.Add(this.propertyGridParameters);
            this.groupBoxParameters.Controls.Add(this.comboBoxOverview);
            this.groupBoxParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxParameters.Location = new System.Drawing.Point(9, 126);
            this.groupBoxParameters.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxParameters.Size = new System.Drawing.Size(486, 709);
            this.groupBoxParameters.TabIndex = 0;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Parameters";
            // 
            // propertyGridParameters
            // 
            this.propertyGridParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertyGridParameters.HelpVisible = false;
            this.propertyGridParameters.Location = new System.Drawing.Point(8, 73);
            this.propertyGridParameters.Margin = new System.Windows.Forms.Padding(4);
            this.propertyGridParameters.Name = "propertyGridParameters";
            this.propertyGridParameters.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGridParameters.Size = new System.Drawing.Size(470, 620);
            this.propertyGridParameters.TabIndex = 1;
            this.propertyGridParameters.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGridParameters_SelectedGridItemChanged);
            // 
            // comboBoxOverview
            // 
            this.comboBoxOverview.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBoxOverview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOverview.FormattingEnabled = true;
            this.comboBoxOverview.Location = new System.Drawing.Point(8, 31);
            this.comboBoxOverview.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxOverview.Name = "comboBoxOverview";
            this.comboBoxOverview.Size = new System.Drawing.Size(470, 33);
            this.comboBoxOverview.TabIndex = 0;
            this.comboBoxOverview.SelectedIndexChanged += new System.EventHandler(this.comboBoxOverview_SelectedIndexChanged);
            // 
            // groupBoxManual
            // 
            this.groupBoxManual.Controls.Add(this.labelManualnotFound);
            this.groupBoxManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxManual.Location = new System.Drawing.Point(518, 126);
            this.groupBoxManual.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxManual.Name = "groupBoxManual";
            this.groupBoxManual.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxManual.Size = new System.Drawing.Size(1214, 709);
            this.groupBoxManual.TabIndex = 1;
            this.groupBoxManual.TabStop = false;
            this.groupBoxManual.Text = "Manual";
            // 
            // labelManualnotFound
            // 
            this.labelManualnotFound.AutoSize = true;
            this.labelManualnotFound.Location = new System.Drawing.Point(401, 59);
            this.labelManualnotFound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelManualnotFound.Name = "labelManualnotFound";
            this.labelManualnotFound.Size = new System.Drawing.Size(64, 25);
            this.labelManualnotFound.TabIndex = 1;
            this.labelManualnotFound.Text = "label1";
            // 
            // axAcroPDFManual
            // 
            this.axAcroPDFManual.Enabled = true;
            this.axAcroPDFManual.Location = new System.Drawing.Point(0, 0);
            this.axAcroPDFManual.Name = "axAcroPDFManual";
            this.axAcroPDFManual.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(17, 15);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(121, 36);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndRun
            // 
            this.buttonSaveAndRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveAndRun.Location = new System.Drawing.Point(147, 15);
            this.buttonSaveAndRun.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSaveAndRun.Name = "buttonSaveAndRun";
            this.buttonSaveAndRun.Size = new System.Drawing.Size(291, 36);
            this.buttonSaveAndRun.TabIndex = 3;
            this.buttonSaveAndRun.Text = "Save and Run";
            this.buttonSaveAndRun.UseVisualStyleBackColor = true;
            this.buttonSaveAndRun.Click += new System.EventHandler(this.buttonSaveAndRun_Click);
            // 
            // checkBoxUpdatePDF
            // 
            this.checkBoxUpdatePDF.AutoSize = true;
            this.checkBoxUpdatePDF.Checked = true;
            this.checkBoxUpdatePDF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpdatePDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUpdatePDF.Location = new System.Drawing.Point(518, 89);
            this.checkBoxUpdatePDF.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxUpdatePDF.Name = "checkBoxUpdatePDF";
            this.checkBoxUpdatePDF.Size = new System.Drawing.Size(141, 29);
            this.checkBoxUpdatePDF.TabIndex = 4;
            this.checkBoxUpdatePDF.Text = "Update PDF";
            this.checkBoxUpdatePDF.UseVisualStyleBackColor = true;
            this.checkBoxUpdatePDF.CheckedChanged += new System.EventHandler(this.checkBoxUpdatePDF_CheckedChanged);
            // 
            // richTextBoxXMLFileName
            // 
            this.richTextBoxXMLFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxXMLFileName.Location = new System.Drawing.Point(17, 59);
            this.richTextBoxXMLFileName.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxXMLFileName.Multiline = false;
            this.richTextBoxXMLFileName.Name = "richTextBoxXMLFileName";
            this.richTextBoxXMLFileName.ReadOnly = true;
            this.richTextBoxXMLFileName.Size = new System.Drawing.Size(417, 34);
            this.richTextBoxXMLFileName.TabIndex = 6;
            this.richTextBoxXMLFileName.Text = "";
            // 
            // openFileDialogXMLFile
            // 
            this.openFileDialogXMLFile.FileName = "openFileDialog1";
            // 
            // backgroundWorkerSimulate
            // 
            this.backgroundWorkerSimulate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSimulate_DoWork_1);
            // 
            // ParameterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1748, 935);
            this.Controls.Add(this.richTextBoxXMLFileName);
            this.Controls.Add(this.checkBoxUpdatePDF);
            this.Controls.Add(this.buttonSaveAndRun);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxManual);
            this.Controls.Add(this.groupBoxParameters);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ParameterView";
            this.Text = "LepiX-Modellkonfiguration";
            this.Load += new System.EventHandler(this.ParameterView_Load);
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxManual.ResumeLayout(false);
            this.groupBoxManual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDFManual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.ComboBox comboBoxOverview;
        private System.Windows.Forms.PropertyGrid propertyGridParameters;
        private System.Windows.Forms.GroupBox groupBoxManual;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSaveAndRun;
        private System.Windows.Forms.CheckBox checkBoxUpdatePDF;
        private System.Windows.Forms.Label labelManualnotFound;
        private System.Windows.Forms.RichTextBox richTextBoxXMLFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialogXMLFile;
        private AxAcroPDFLib.AxAcroPDF axAcroPDFManual;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSimulate;
    }
}

