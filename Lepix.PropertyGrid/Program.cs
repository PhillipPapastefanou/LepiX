using System;
using System.Windows.Forms;

using LepiX.PropertyGrid.Model;
using LepiX.PropertyGrid.Controller;
using LepiX.PropertyGrid.View;
using LepiX.CallingRScripts;
using Microsoft.Win32;


namespace LepiX.PropertyGrid
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            CheckForAdobeReader();


            bool rIsInstalledAndWorking;

            //try
            //{
            //    ScriptEngine.Setup();
            //    rIsInstalledAndWorking = true;
            //}

            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message);
            //    rIsInstalledAndWorking = false;
            //}
            rIsInstalledAndWorking = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            ParameterView view = new ParameterView();
            view.Visible = false;
            ParameterModel model = new ParameterModel();
            ParameterController controller = new ParameterController(model, view);
                       


            view.ShowDialog();

            if (rIsInstalledAndWorking)
            {
                ScriptEngine.Dispose();
            }

        }





        private static void CheckForAdobeReader()
        {
            if (Registry.GetValue(@"HKEY_CLASSES_ROOT\Software\Adobe\Acrobat\Exe", string.Empty, string.Empty) != null)
            {
                //Reader exists
            }

            else
            {
                MessageBox.Show("LepiX did not find an installation of Adobe Reader! Please install adobe Reader");
                Environment.Exit(0);
            }
        }
    }





}
