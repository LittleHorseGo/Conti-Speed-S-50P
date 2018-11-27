namespace Conti_Speed_S_50P
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripHome1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripHome2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDatabase = new System.Windows.Forms.ToolStripButton();
            this.toolStripChart = new System.Windows.Forms.ToolStripButton();
            this.toolStripSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripLogin = new System.Windows.Forms.ToolStripButton();
            this.toolStripSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTest = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel_SerialPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_Login = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelStartupPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(60, 60);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripHome1,
            this.toolStripHome2,
            this.toolStripDatabase,
            this.toolStripChart,
            this.toolStripSetting,
            this.toolStripLogin,
            this.toolStripSave,
            this.toolStripLabel1,
            this.toolStripTest});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1167, 59);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripHome1
            // 
            this.toolStripHome1.AutoSize = false;
            this.toolStripHome1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStripHome1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripHome1.Image = global::Conti_Speed_S_50P.Properties.Resources.Station1;
            this.toolStripHome1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripHome1.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolStripHome1.Name = "toolStripHome1";
            this.toolStripHome1.Size = new System.Drawing.Size(55, 55);
            this.toolStripHome1.Text = "1#机台";
            this.toolStripHome1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripHome1.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // toolStripHome2
            // 
            this.toolStripHome2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripHome2.Image = global::Conti_Speed_S_50P.Properties.Resources.Station2;
            this.toolStripHome2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripHome2.Name = "toolStripHome2";
            this.toolStripHome2.Size = new System.Drawing.Size(64, 56);
            this.toolStripHome2.Text = "2#机台";
            this.toolStripHome2.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // toolStripDatabase
            // 
            this.toolStripDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDatabase.Image = global::Conti_Speed_S_50P.Properties.Resources.data_search;
            this.toolStripDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDatabase.Name = "toolStripDatabase";
            this.toolStripDatabase.Size = new System.Drawing.Size(64, 56);
            this.toolStripDatabase.Text = "查询数据表格";
            this.toolStripDatabase.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // toolStripChart
            // 
            this.toolStripChart.AutoSize = false;
            this.toolStripChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripChart.Image = global::Conti_Speed_S_50P.Properties.Resources.line_chart;
            this.toolStripChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripChart.Margin = new System.Windows.Forms.Padding(5, 1, 3, 2);
            this.toolStripChart.Name = "toolStripChart";
            this.toolStripChart.Size = new System.Drawing.Size(55, 55);
            this.toolStripChart.Text = "波形数据";
            this.toolStripChart.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // toolStripSetting
            // 
            this.toolStripSetting.AutoSize = false;
            this.toolStripSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSetting.Image = global::Conti_Speed_S_50P.Properties.Resources.setting;
            this.toolStripSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSetting.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolStripSetting.Name = "toolStripSetting";
            this.toolStripSetting.Size = new System.Drawing.Size(55, 55);
            this.toolStripSetting.Text = "设置";
            this.toolStripSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripSetting.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // toolStripLogin
            // 
            this.toolStripLogin.AutoSize = false;
            this.toolStripLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLogin.Image = global::Conti_Speed_S_50P.Properties.Resources.login;
            this.toolStripLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLogin.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolStripLogin.Name = "toolStripLogin";
            this.toolStripLogin.Size = new System.Drawing.Size(55, 55);
            this.toolStripLogin.Text = "登录";
            this.toolStripLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripLogin.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // toolStripSave
            // 
            this.toolStripSave.AutoSize = false;
            this.toolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSave.Image = global::Conti_Speed_S_50P.Properties.Resources.save;
            this.toolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSave.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolStripSave.Name = "toolStripSave";
            this.toolStripSave.Size = new System.Drawing.Size(55, 55);
            this.toolStripSave.Text = "保存";
            this.toolStripSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripSave.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel1.Image = global::Conti_Speed_S_50P.Properties.Resources.Logo_small;
            this.toolStripLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(149, 56);
            this.toolStripLabel1.Text = ".....................................................";
            // 
            // toolStripTest
            // 
            this.toolStripTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripTest.Image = ((System.Drawing.Image)(resources.GetObject("toolStripTest.Image")));
            this.toolStripTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripTest.Name = "toolStripTest";
            this.toolStripTest.Size = new System.Drawing.Size(64, 56);
            this.toolStripTest.Text = "toolStripButton1";
            this.toolStripTest.Visible = false;
            this.toolStripTest.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 59);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1167, 665);
            this.panelMain.TabIndex = 1;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel_SerialPort,
            this.StatusLabel_Login,
            this.StatusLabelStartupPath});
            this.statusStrip.Location = new System.Drawing.Point(0, 724);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(1167, 29);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // StatusLabel_SerialPort
            // 
            this.StatusLabel_SerialPort.AutoSize = false;
            this.StatusLabel_SerialPort.BackColor = System.Drawing.SystemColors.Control;
            this.StatusLabel_SerialPort.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StatusLabel_SerialPort.Name = "StatusLabel_SerialPort";
            this.StatusLabel_SerialPort.Size = new System.Drawing.Size(120, 24);
            this.StatusLabel_SerialPort.Text = "TCP 已连接";
            // 
            // StatusLabel_Login
            // 
            this.StatusLabel_Login.AutoSize = false;
            this.StatusLabel_Login.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StatusLabel_Login.Name = "StatusLabel_Login";
            this.StatusLabel_Login.Size = new System.Drawing.Size(220, 24);
            this.StatusLabel_Login.Text = "当前登陆用用户：Operator";
            // 
            // StatusLabelStartupPath
            // 
            this.StatusLabelStartupPath.Name = "StatusLabelStartupPath";
            this.StatusLabelStartupPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusLabelStartupPath.Size = new System.Drawing.Size(812, 24);
            this.StatusLabelStartupPath.Spring = true;
            this.StatusLabelStartupPath.Text = "Startup:";
            this.StatusLabelStartupPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1167, 753);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TE Software - Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripButton toolStripHome1;
        private System.Windows.Forms.ToolStripButton toolStripLogin;
        private System.Windows.Forms.ToolStripButton toolStripSave;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_SerialPort;
        private System.Windows.Forms.ToolStripButton toolStripSetting;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_Login;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelStartupPath;
        private System.Windows.Forms.ToolStripButton toolStripChart;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripHome2;
        private System.Windows.Forms.ToolStripButton toolStripDatabase;
        private System.Windows.Forms.ToolStripButton toolStripTest;
    }
}

