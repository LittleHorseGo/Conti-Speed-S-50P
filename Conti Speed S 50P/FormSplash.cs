using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Conti_Speed_S_50P
{
    public partial class FormSplash : Form, ISplashForm
    {
      
        public FormSplash()
        {
            InitializeComponent();
        }

        public void SetProgressInfo(int NewProgressInfo)
        {
            prgProgressInfo.Value = NewProgressInfo;
        }

        public void SetStatusInfo(string NewStatusInfo)
        {
            lbStatusInfo.Text = NewStatusInfo;
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {

        }
    }
}
