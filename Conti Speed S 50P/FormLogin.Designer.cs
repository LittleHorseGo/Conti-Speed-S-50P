namespace Conti_Speed_S_50P
{
    partial class FormLogin
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
            this.button_cancal = new System.Windows.Forms.Button();
            this.button_SignIn = new System.Windows.Forms.Button();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.comboBox_Login = new System.Windows.Forms.ComboBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_CurrentLogin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_cancal
            // 
            this.button_cancal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancal.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_cancal.Location = new System.Drawing.Point(259, 202);
            this.button_cancal.Margin = new System.Windows.Forms.Padding(4);
            this.button_cancal.Name = "button_cancal";
            this.button_cancal.Size = new System.Drawing.Size(100, 40);
            this.button_cancal.TabIndex = 4;
            this.button_cancal.Text = "取消";
            this.button_cancal.UseVisualStyleBackColor = true;
            this.button_cancal.Click += new System.EventHandler(this.button_cancal_Click);
            // 
            // button_SignIn
            // 
            this.button_SignIn.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SignIn.Location = new System.Drawing.Point(107, 202);
            this.button_SignIn.Margin = new System.Windows.Forms.Padding(4);
            this.button_SignIn.Name = "button_SignIn";
            this.button_SignIn.Size = new System.Drawing.Size(100, 40);
            this.button_SignIn.TabIndex = 3;
            this.button_SignIn.Text = "确认";
            this.button_SignIn.UseVisualStyleBackColor = true;
            this.button_SignIn.Click += new System.EventHandler(this.button_SignIn_Click);
            // 
            // textBox_Password
            // 
            this.textBox_Password.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Password.Location = new System.Drawing.Point(193, 140);
            this.textBox_Password.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.PasswordChar = '*';
            this.textBox_Password.Size = new System.Drawing.Size(185, 30);
            this.textBox_Password.TabIndex = 2;
            this.textBox_Password.TextChanged += new System.EventHandler(this.textBox_Password_TextChanged);
            // 
            // comboBox_Login
            // 
            this.comboBox_Login.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Login.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Login.FormattingEnabled = true;
            this.comboBox_Login.Location = new System.Drawing.Point(193, 76);
            this.comboBox_Login.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_Login.Name = "comboBox_Login";
            this.comboBox_Login.Size = new System.Drawing.Size(185, 31);
            this.comboBox_Login.TabIndex = 1;
            this.comboBox_Login.SelectedIndexChanged += new System.EventHandler(this.comboBox_Login_SelectedIndexChanged);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.Location = new System.Drawing.Point(97, 140);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(61, 23);
            this.labelPassword.TabIndex = 7;
            this.labelPassword.Text = "密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(97, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "用户名：";
            // 
            // label_CurrentLogin
            // 
            this.label_CurrentLogin.AutoSize = true;
            this.label_CurrentLogin.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_CurrentLogin.Location = new System.Drawing.Point(149, 31);
            this.label_CurrentLogin.Name = "label_CurrentLogin";
            this.label_CurrentLogin.Size = new System.Drawing.Size(186, 20);
            this.label_CurrentLogin.TabIndex = 13;
            this.label_CurrentLogin.Text = "(当前登陆用户：Operator）";
            // 
            // FormLogin
            // 
            this.AcceptButton = this.button_SignIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancal;
            this.ClientSize = new System.Drawing.Size(498, 292);
            this.Controls.Add(this.label_CurrentLogin);
            this.Controls.Add(this.button_cancal);
            this.Controls.Add(this.button_SignIn);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.comboBox_Login);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.label1);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancal;
        private System.Windows.Forms.Button button_SignIn;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.ComboBox comboBox_Login;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_CurrentLogin;
    }
}