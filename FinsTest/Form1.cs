using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinsTest
{
    public partial class Form1 : Form
    {
        private OmronFinsHelper finsHelper = new OmronFinsHelper();
        public Form1()
        {
            InitializeComponent();
            finsHelper.InitializeOmronFins("192.168.1.10", 9600);
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            finsHelper.FinsSendData(Convert.ToInt16(txtAddress.Text.ToString()), Convert.ToInt16(txtData.Text.ToString()));
        }
    }
}
