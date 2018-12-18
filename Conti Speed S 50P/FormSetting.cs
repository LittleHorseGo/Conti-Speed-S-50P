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
    public partial class FormSetting : Form
    {
        public FormSetting()
        {
            InitializeComponent();
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            txtXLowerLimit.Text = FormMain.mSettingHelper.PosXLowerLimit.ToString();
            txtXUpperLimit.Text = FormMain.mSettingHelper.PosXUpperLimit.ToString();
            txtYlowerLimit.Text = FormMain.mSettingHelper.PosYLowerLimit.ToString();
            txtYUpperLimit.Text = FormMain.mSettingHelper.PosYUpperLimit.ToString();
            txtZLowerLimit.Text = FormMain.mSettingHelper.PosZLowerLimit.ToString();
            txtZUpperLimit.Text = FormMain.mSettingHelper.PosZUpperLimit.ToString();

            cmbXDirection.Items.Clear();
            cmbXDirection.Items.Add("正");
            cmbXDirection.Items.Add("反");
            cmbXDirection.SelectedIndex = FormMain.mSettingHelper.OutputXDirection ? 0 : 1;
            cmbYDirection.Items.Clear();
            cmbYDirection.Items.Add("正");
            cmbYDirection.Items.Add("反");
            cmbYDirection.SelectedIndex = FormMain.mSettingHelper.OutputYDirection ? 0 : 1;
            cmbZDirection.Items.Clear();
            cmbZDirection.Items.Add("正");
            cmbZDirection.Items.Add("反");
            cmbZDirection.SelectedIndex = FormMain.mSettingHelper.OutputZDirection ? 0 : 1;

            txtOutputRatio.Text = FormMain.mSettingHelper.OutputRatio.ToString();

            txtXPerfectLower.Text = FormMain.mSettingHelper.OutputXPerfectLowerLimit.ToString();
            txtXPerfectUpper.Text = FormMain.mSettingHelper.OutputXPerfectUpperLimit.ToString();
            txtYPerfectLower.Text = FormMain.mSettingHelper.OutputYPerfectLowerLimit.ToString();
            txtYPerfectUpper.Text = FormMain.mSettingHelper.OutputYPerfectUpperLimit.ToString();
            txtZPerfectLower.Text = FormMain.mSettingHelper.OutputZPerfectLowerLimit.ToString();
            txtZPerfectUpper.Text = FormMain.mSettingHelper.OutputZPerfectUpperLimit.ToString();

            txtDataNum.Text = FormMain.mSettingHelper.DataNum.ToString();

            txtSt1IP.Text = FormMain.mSettingHelper.PLCIP1;
            txtSt1Port.Text = FormMain.mSettingHelper.PLCPort1.ToString();
            txtSt2IP.Text = FormMain.mSettingHelper.PLCIP2;
            txtSt2Port.Text = FormMain.mSettingHelper.PLCPort2.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FormMain.mSettingHelper.PosXLowerLimit = Convert.ToDouble(txtXLowerLimit.Text);
                FormMain.mSettingHelper.PosXUpperLimit = Convert.ToDouble(txtXUpperLimit.Text);
                FormMain.mSettingHelper.PosYLowerLimit = Convert.ToDouble(txtYlowerLimit.Text);
                FormMain.mSettingHelper.PosYUpperLimit = Convert.ToDouble(txtYUpperLimit.Text);
                FormMain.mSettingHelper.PosZLowerLimit = Convert.ToDouble(txtZLowerLimit.Text);
                FormMain.mSettingHelper.PosZUpperLimit = Convert.ToDouble(txtZUpperLimit.Text);

                FormMain.ConfigIniFile.IniWriteValue("LimitSetting", "xLowerLimit", FormMain.mSettingHelper.PosXLowerLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("LimitSetting", "xUpperLimit", FormMain.mSettingHelper.PosXUpperLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("LimitSetting", "yLowerLimit", FormMain.mSettingHelper.PosYLowerLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("LimitSetting", "yUpperLimit", FormMain.mSettingHelper.PosYUpperLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("LimitSetting", "zLowerLimit", FormMain.mSettingHelper.PosZLowerLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("LimitSetting", "zUpperLimit", FormMain.mSettingHelper.PosZUpperLimit.ToString());

                FormMain.mSettingHelper.OutputXDirection = cmbXDirection.SelectedIndex == 0 ? true : false;
                FormMain.mSettingHelper.OutputYDirection = cmbYDirection.SelectedIndex == 0 ? true : false;
                FormMain.mSettingHelper.OutputZDirection = cmbZDirection.SelectedIndex == 0 ? true : false;

                FormMain.ConfigIniFile.IniWriteValue("Output", "xDirection", FormMain.mSettingHelper.OutputXDirection ? "1" : "-1");
                FormMain.ConfigIniFile.IniWriteValue("Output", "yDirection", FormMain.mSettingHelper.OutputYDirection ? "1" : "-1");
                FormMain.ConfigIniFile.IniWriteValue("Output", "zDirection", FormMain.mSettingHelper.OutputZDirection ? "1" : "-1");

                FormMain.mSettingHelper.OutputRatio = Convert.ToInt32(txtOutputRatio.Text);
                FormMain.ConfigIniFile.IniWriteValue("Output", "outputRatio", FormMain.mSettingHelper.OutputRatio);

                FormMain.mSettingHelper.OutputXPerfectLowerLimit = Convert.ToInt16(txtXPerfectLower.Text);
                FormMain.mSettingHelper.OutputXPerfectUpperLimit = Convert.ToInt16(txtXPerfectLower.Text);
                FormMain.mSettingHelper.OutputYPerfectLowerLimit = Convert.ToInt16(txtYPerfectLower.Text);
                FormMain.mSettingHelper.OutputYPerfectUpperLimit = Convert.ToInt16(txtYPerfectUpper.Text);
                FormMain.mSettingHelper.OutputZPerfectLowerLimit = Convert.ToInt16(txtZPerfectLower.Text);
                FormMain.mSettingHelper.OutputZPerfectUpperLimit = Convert.ToInt16(txtZPerfectUpper.Text);

                FormMain.ConfigIniFile.IniWriteValue("Output", "xPerfectLowerLimit", FormMain.mSettingHelper.OutputXPerfectLowerLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("Output", "xPerfectUpperLimit", FormMain.mSettingHelper.OutputXPerfectUpperLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("Output", "yPerfectLowerLimit", FormMain.mSettingHelper.OutputYPerfectLowerLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("Output", "yPerfectUpperLimit", FormMain.mSettingHelper.OutputYPerfectUpperLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("Output", "zPerfectLowerLimit", FormMain.mSettingHelper.OutputZPerfectLowerLimit.ToString());
                FormMain.ConfigIniFile.IniWriteValue("Output", "zPerfectUpperLimit", FormMain.mSettingHelper.OutputZPerfectUpperLimit.ToString());

                FormMain.mSettingHelper.DataNum = Convert.ToInt32(txtDataNum.Text);
                FormMain.ConfigIniFile.IniWriteValue("Output", "dataNum", FormMain.mSettingHelper.DataNum.ToString());

                FormMain.mSettingHelper.PLCIP1 = txtSt1IP.Text;
                FormMain.mSettingHelper.PLCPort1 = Convert.ToInt16(txtSt1Port.Text);
                FormMain.mSettingHelper.PLCIP2 = txtSt2IP.Text;
                FormMain.mSettingHelper.PLCPort2 = Convert.ToInt16(txtSt2Port.Text);

                FormMain.ConfigIniFile.IniWriteValue("PLC Parameter", "IP1", FormMain.mSettingHelper.PLCIP1);
                FormMain.ConfigIniFile.IniWriteValue("PLC Parameter", "Port1", FormMain.mSettingHelper.PLCPort1.ToString());
                FormMain.MessageSenderConfigIniFile.IniWriteValue("PLC Parameter", "IP2", FormMain.mSettingHelper.PLCIP2);
                FormMain.MessageSenderConfigIniFile.IniWriteValue("PLC Parameter", "Port2", FormMain.mSettingHelper.PLCPort2.ToString());

                this.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
