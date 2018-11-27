using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conti_Speed_S_50P
{
    public partial class FormShowAsynProg : Form
    {
        public FormShowAsynProg()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int progress, string info)
        {
            this.progressBar.Value = progress;
            this.labelInfo.Text = info;
        }
    }
}
