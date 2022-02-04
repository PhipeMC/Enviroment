using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace App
{
    public partial class App_config : Form
    {
        private Form1 workspace;
        public App_config(Form1 workspace)
        {
            InitializeComponent();
            this.workspace = workspace;
            //MedicalViewerCell
        }


        private void App_config_Load(object sender, EventArgs e)
        {

        }
    }
}
