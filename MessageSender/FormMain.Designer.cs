namespace MessageSender
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
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel_SerialPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelStartupPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(507, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "该窗口仅用于后台程序发送Fins数据给St2号机器，主程序启动时会自动启动，主程序关闭时会自动关闭";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel_SerialPort,
            this.StatusLabelStartupPath});
            this.statusStrip.Location = new System.Drawing.Point(0, 140);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(567, 29);
            this.statusStrip.TabIndex = 3;
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
            // StatusLabelStartupPath
            // 
            this.StatusLabelStartupPath.Name = "StatusLabelStartupPath";
            this.StatusLabelStartupPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusLabelStartupPath.Size = new System.Drawing.Size(432, 24);
            this.StatusLabelStartupPath.Spring = true;
            this.StatusLabelStartupPath.Text = "Startup:";
            this.StatusLabelStartupPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 169);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.ShowInTaskbar = false;
            this.Text = "Message Sender";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_SerialPort;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelStartupPath;
    }
}

