using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonModule;

namespace Conti_Speed_S_50P
{
    public partial class FormLogin : Form
    {
        private string mPassword;
        private AccessLevel mCurrentAccessLevel;
        // private const string mDefaultOperatorPassword = "";
        private const string mDefaultAdministratorPassword = "admin";
        private const string mDefaultSupervisorPassword = "super";
        private PasswordFile mCurrentPasswordFile;

        public string Password
        {
            get { return mPassword; }
        }

        public AccessLevel GetCurrentAccessLevel { get { return mCurrentAccessLevel; } }

        static public string SignIN_Status;

        public FormLogin(AccessLevel accesslevel)
        {
            InitializeComponent();
            mPassword = "";

            mCurrentAccessLevel = accesslevel;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            comboBox_Login.Items.Add(new AccessLevel_Localized(AccessLevel.Operator, ResourceUtility.GetString("RtOperator")));
            comboBox_Login.Items.Add(new AccessLevel_Localized(AccessLevel.Supervisor, ResourceUtility.GetString("RtSupervisor")));
            comboBox_Login.Items.Add(new AccessLevel_Localized(AccessLevel.Administrator, ResourceUtility.GetString("RtAdministrator")));
            // default access level is "Operator"
            comboBox_Login.SelectedIndex = (int)mCurrentAccessLevel;
            label_CurrentLogin.Text = string.Format("(当前登陆用户：{0})", mCurrentAccessLevel.ToString());

            // create, validate & setup the password file
            string passwordfname = Utility.ResolveAssociatedFilename(FormMain.strConfigFilePath, "passwords.txt");
            mCurrentPasswordFile = new PasswordFile(passwordfname);
            if (mCurrentPasswordFile.PasswordFileFound && !mCurrentPasswordFile.PasswordFileValid)
            {
                string quoted = "\"" + mCurrentPasswordFile.PasswordFilename + "\"";
                // label_controlErrorMessage.Text = ResourceUtility.FormatString("RtInvalidPasswordFile", quoted);
            }
            // mCurrentPasswordFile.SetDefaultPassword(AccessLevel.Operator, mDefaultOperatorPassword);
            mCurrentPasswordFile.SetDefaultPassword(AccessLevel.Administrator, mDefaultAdministratorPassword);
            mCurrentPasswordFile.SetDefaultPassword(AccessLevel.Supervisor, mDefaultSupervisorPassword);

            EnableOk();
        }

        private void button_cancal_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_SignIn_Click(object sender, EventArgs e)
        {
            mPassword = textBox_Password.Text;
            AccessLevel newAccessLevel = ((AccessLevel_Localized)(comboBox_Login.SelectedItem)).val;
            // No password is needed when choosing operator access level
            if (newAccessLevel == AccessLevel.Operator) { mCurrentAccessLevel = newAccessLevel; this.Close(); }
            else
            {
                // Not using passwords, or going "down" in access level - always allowed
                string expected = mCurrentPasswordFile.GetPasswordForAccessLevel(newAccessLevel);
                if (expected != "")
                {
                    // get password from user
                    if (mPassword == expected)
                    {
                        mCurrentAccessLevel = newAccessLevel;
                        this.Close();
                    }
                    else
                    {
                        // prompt for a password - only update accessLevel if promt is successful
                        this.textBox_Password.Clear();
                        MessageBox.Show(ResourceUtility.GetString("RtInvalidPassword2"), ResourceUtility.GetString("RtInvalidPassword"));
                    }
                }
            }
        }

        private void comboBox_Login_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((AccessLevel_Localized)(comboBox_Login.SelectedItem)).val == AccessLevel.Operator)
            {
                // if the current access level is operator, disable the password input
                this.textBox_Password.Visible = false;
                this.labelPassword.Visible = false;
                button_SignIn.Enabled = true;
            }
            else
            {
                this.textBox_Password.Visible = true;
                this.labelPassword.Visible = true;
            }
        }

        private void EnableOk()
        {
            // enable ok button based on dialog contents
            bool en = false;

            // confirms passwords must not be empty
            if (this.textBox_Password.Text != "" || ((AccessLevel_Localized)(comboBox_Login.SelectedItem)).val == AccessLevel.Operator)
                    en = true;
            button_SignIn.Enabled = en;
        }

        private void textBox_Password_TextChanged(object sender, EventArgs e)
        {
            EnableOk();
        }
    }
}
