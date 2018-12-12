namespace Conti_Speed_S_50P
{
    partial class DisplayAndDataView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayAndDataView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.displayView1 = new Conti_Speed_S_50P.DisplayView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPinIndex = new System.Windows.Forms.Label();
            this.txtOutputZ = new System.Windows.Forms.TextBox();
            this.txtCurrentZ = new System.Windows.Forms.TextBox();
            this.txtOutputY = new System.Windows.Forms.TextBox();
            this.txtCurrentY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutputX = new System.Windows.Forms.TextBox();
            this.txtCurrentX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.displayView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(689, 461);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // displayView1
            // 
            this.displayView1.BackgroundColor = System.Drawing.Color.Empty;
            this.displayView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayView1.IsPinExist = true;
            this.displayView1.Location = new System.Drawing.Point(0, 0);
            this.displayView1.Margin = new System.Windows.Forms.Padding(0);
            this.displayView1.Name = "displayView1";
            this.displayView1.PosX = ((System.Collections.Generic.List<System.Nullable<double>>)(resources.GetObject("displayView1.PosX")));
            this.displayView1.PosXLowerLimit = 0D;
            this.displayView1.PosXUpperLimit = 0D;
            this.displayView1.PosY = ((System.Collections.Generic.List<System.Nullable<double>>)(resources.GetObject("displayView1.PosY")));
            this.displayView1.PosYLowerLimit = 0D;
            this.displayView1.PosYUpperLimit = 0D;
            this.displayView1.PosZ = ((System.Collections.Generic.List<System.Nullable<double>>)(resources.GetObject("displayView1.PosZ")));
            this.displayView1.PosZLowerLimit = 0D;
            this.displayView1.PosZUpperLimit = 0D;
            this.displayView1.Size = new System.Drawing.Size(489, 461);
            this.displayView1.Status = 0;
            this.displayView1.TabIndex = 0;
            this.displayView1.ViewIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblPinIndex);
            this.panel1.Controls.Add(this.txtOutputZ);
            this.panel1.Controls.Add(this.txtCurrentZ);
            this.panel1.Controls.Add(this.txtOutputY);
            this.panel1.Controls.Add(this.txtCurrentY);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtOutputX);
            this.panel1.Controls.Add(this.txtCurrentX);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(492, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this.panel1.Size = new System.Drawing.Size(194, 455);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Location = new System.Drawing.Point(9, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 3);
            this.panel2.TabIndex = 3;
            // 
            // lblPinIndex
            // 
            this.lblPinIndex.AutoSize = true;
            this.lblPinIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPinIndex.Location = new System.Drawing.Point(84, 9);
            this.lblPinIndex.Name = "lblPinIndex";
            this.lblPinIndex.Size = new System.Drawing.Size(29, 20);
            this.lblPinIndex.TabIndex = 2;
            this.lblPinIndex.Text = "1#";
            // 
            // txtOutputZ
            // 
            this.txtOutputZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutputZ.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOutputZ.Location = new System.Drawing.Point(116, 171);
            this.txtOutputZ.Name = "txtOutputZ";
            this.txtOutputZ.ReadOnly = true;
            this.txtOutputZ.Size = new System.Drawing.Size(63, 20);
            this.txtOutputZ.TabIndex = 1;
            this.txtOutputZ.Text = "0";
            this.txtOutputZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCurrentZ
            // 
            this.txtCurrentZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentZ.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrentZ.Location = new System.Drawing.Point(28, 171);
            this.txtCurrentZ.Name = "txtCurrentZ";
            this.txtCurrentZ.ReadOnly = true;
            this.txtCurrentZ.Size = new System.Drawing.Size(63, 20);
            this.txtCurrentZ.TabIndex = 1;
            this.txtCurrentZ.Text = "0";
            this.txtCurrentZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOutputY
            // 
            this.txtOutputY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutputY.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOutputY.Location = new System.Drawing.Point(116, 133);
            this.txtOutputY.Name = "txtOutputY";
            this.txtOutputY.ReadOnly = true;
            this.txtOutputY.Size = new System.Drawing.Size(63, 20);
            this.txtOutputY.TabIndex = 1;
            this.txtOutputY.Text = "0";
            this.txtOutputY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCurrentY
            // 
            this.txtCurrentY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentY.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrentY.Location = new System.Drawing.Point(28, 133);
            this.txtCurrentY.Name = "txtCurrentY";
            this.txtCurrentY.ReadOnly = true;
            this.txtCurrentY.Size = new System.Drawing.Size(63, 20);
            this.txtCurrentY.TabIndex = 1;
            this.txtCurrentY.Text = "0";
            this.txtCurrentY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(7, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Z:";
            // 
            // txtOutputX
            // 
            this.txtOutputX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutputX.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOutputX.Location = new System.Drawing.Point(116, 97);
            this.txtOutputX.Name = "txtOutputX";
            this.txtOutputX.ReadOnly = true;
            this.txtOutputX.Size = new System.Drawing.Size(63, 20);
            this.txtOutputX.TabIndex = 1;
            this.txtOutputX.Text = "0";
            this.txtOutputX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCurrentX
            // 
            this.txtCurrentX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentX.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrentX.Location = new System.Drawing.Point(28, 97);
            this.txtCurrentX.Name = "txtCurrentX";
            this.txtCurrentX.ReadOnly = true;
            this.txtCurrentX.Size = new System.Drawing.Size(63, 20);
            this.txtCurrentX.TabIndex = 1;
            this.txtCurrentX.Text = "0";
            this.txtCurrentX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(7, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(104, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 40);
            this.label6.TabIndex = 0;
            this.label6.Text = "反馈值:\r\n(-250-250)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(27, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 40);
            this.label2.TabIndex = 0;
            this.label2.Text = "当前值:\r\n(mm)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // DisplayAndDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DisplayAndDataView";
            this.Size = new System.Drawing.Size(689, 461);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DisplayView displayView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCurrentX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputZ;
        private System.Windows.Forms.TextBox txtCurrentZ;
        private System.Windows.Forms.TextBox txtOutputY;
        private System.Windows.Forms.TextBox txtCurrentY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutputX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPinIndex;
        private System.Windows.Forms.Panel panel2;
    }
}
