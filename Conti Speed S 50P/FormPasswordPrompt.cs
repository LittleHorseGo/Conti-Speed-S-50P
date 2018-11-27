using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Conti_Speed_S_50P
{
    /// <summary>
    /// Summary description for FormPasswordPrompt.
    /// </summary>
    public partial class FormPasswordPrompt : Form
    {
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Label label_Password;

        private string mPassword;

        public string Password
        {
            get { return mPassword; }
        }

        public FormPasswordPrompt()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            mPassword = "";
        }
        
        private void button_OK_Click(object sender, System.EventArgs e)
        {
            mPassword = textBox_Password.Text;
        }

        private void FormPasswordPrompt_Load(object sender, System.EventArgs e)
        {
            this.label_Password.Text = ResourceUtility.GetString("RtPassword");
            this.button_OK.Text = ResourceUtility.GetString("RtOK");
            this.button_Cancel.Text = ResourceUtility.GetString("RtCancel");
        }
    }
}
