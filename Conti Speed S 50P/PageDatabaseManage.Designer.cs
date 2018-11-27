namespace Conti_Speed_S_50P
{
    partial class PageDatabaseManage
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnInsertDataSt = new System.Windows.Forms.Button();
            this.btnReadLatestDataSt = new System.Windows.Forms.Button();
            this.btnReadDataSt = new System.Windows.Forms.Button();
            this.cmbDataTableList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearAllData = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1272, 537);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnInsertDataSt
            // 
            this.btnInsertDataSt.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertDataSt.Location = new System.Drawing.Point(746, 24);
            this.btnInsertDataSt.Name = "btnInsertDataSt";
            this.btnInsertDataSt.Size = new System.Drawing.Size(160, 37);
            this.btnInsertDataSt.TabIndex = 18;
            this.btnInsertDataSt.Text = "插入一条随机数据";
            this.btnInsertDataSt.UseVisualStyleBackColor = true;
            this.btnInsertDataSt.Click += new System.EventHandler(this.btnInsertDataSt_Click);
            // 
            // btnReadLatestDataSt
            // 
            this.btnReadLatestDataSt.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadLatestDataSt.Location = new System.Drawing.Point(579, 24);
            this.btnReadLatestDataSt.Name = "btnReadLatestDataSt";
            this.btnReadLatestDataSt.Size = new System.Drawing.Size(160, 37);
            this.btnReadLatestDataSt.TabIndex = 18;
            this.btnReadLatestDataSt.Text = "读取最新一条数据";
            this.btnReadLatestDataSt.UseVisualStyleBackColor = true;
            this.btnReadLatestDataSt.Click += new System.EventHandler(this.btnReadLatestDataSt_Click);
            // 
            // btnReadDataSt
            // 
            this.btnReadDataSt.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadDataSt.Location = new System.Drawing.Point(412, 24);
            this.btnReadDataSt.Name = "btnReadDataSt";
            this.btnReadDataSt.Size = new System.Drawing.Size(160, 37);
            this.btnReadDataSt.TabIndex = 16;
            this.btnReadDataSt.Text = "读取最新100条数据";
            this.btnReadDataSt.UseVisualStyleBackColor = true;
            this.btnReadDataSt.Click += new System.EventHandler(this.btnReadDataSt_Click);
            // 
            // cmbDataTableList
            // 
            this.cmbDataTableList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataTableList.FormattingEnabled = true;
            this.cmbDataTableList.Location = new System.Drawing.Point(141, 30);
            this.cmbDataTableList.Name = "cmbDataTableList";
            this.cmbDataTableList.Size = new System.Drawing.Size(205, 27);
            this.cmbDataTableList.TabIndex = 22;
            this.cmbDataTableList.SelectedIndexChanged += new System.EventHandler(this.cmbDataTableList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "选择数据表格:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReadDataSt);
            this.groupBox1.Controls.Add(this.cmbDataTableList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClearAllData);
            this.groupBox1.Controls.Add(this.btnInsertDataSt);
            this.groupBox1.Controls.Add(this.btnReadLatestDataSt);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1272, 74);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库查询和操作";
            // 
            // btnClearAllData
            // 
            this.btnClearAllData.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllData.Location = new System.Drawing.Point(913, 24);
            this.btnClearAllData.Name = "btnClearAllData";
            this.btnClearAllData.Size = new System.Drawing.Size(160, 37);
            this.btnClearAllData.TabIndex = 18;
            this.btnClearAllData.Text = "清除所有数据(谨慎)";
            this.btnClearAllData.UseVisualStyleBackColor = true;
            this.btnClearAllData.Click += new System.EventHandler(this.btnClearAllData_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1278, 623);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // PageDatabaseManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PageDatabaseManage";
            this.Size = new System.Drawing.Size(1278, 623);
            this.Load += new System.EventHandler(this.PageDatabaseManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnInsertDataSt;
        private System.Windows.Forms.Button btnReadLatestDataSt;
        private System.Windows.Forms.Button btnReadDataSt;
        private System.Windows.Forms.ComboBox cmbDataTableList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnClearAllData;
    }
}
