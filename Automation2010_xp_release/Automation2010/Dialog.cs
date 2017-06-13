using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automation2010
{
    public partial class Dialog : Form
    {
        public Dialog()
        {
            InitializeComponent();
        }

        public string GetMaterial
        {
            get
            {
                return textMaterial.Text;
            }
        }

        public string GetGas
        {
            get
            {
                return (comboGas.SelectedIndex+1).ToString();
            }
        }
        public string GetAbout
        {
            get
            {
                return textAbout.Text;
            }
        }
        private void Dialog_Load(object sender, EventArgs e)
        {

        }

        private void textMaterial_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
