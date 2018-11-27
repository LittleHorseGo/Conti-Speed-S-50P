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
    public partial class FormShowMsg : Form
    {
        public FormShowMsg()
        {
            InitializeComponent();
        }

        public void UpdateMsg(int progress, string info)
        {
            if (progress % 4 == 0)
                this.labelInfo.Text = info + "";
            else if (progress % 4 == 1)
                this.labelInfo.Text = info + ".";
            else if (progress % 4 == 2)
                this.labelInfo.Text = info + "..";
            else if (progress % 4 == 3)
                this.labelInfo.Text = info + "...";
        }
    }
}
