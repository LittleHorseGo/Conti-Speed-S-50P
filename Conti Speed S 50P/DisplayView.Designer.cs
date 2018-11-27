namespace Conti_Speed_S_50P
{
    partial class DisplayView
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
            this.picBoxView = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxView)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxView
            // 
            this.picBoxView.BackColor = System.Drawing.SystemColors.Control;
            this.picBoxView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxView.Location = new System.Drawing.Point(0, 0);
            this.picBoxView.Name = "picBoxView";
            this.picBoxView.Size = new System.Drawing.Size(500, 500);
            this.picBoxView.TabIndex = 0;
            this.picBoxView.TabStop = false;
            // 
            // DisplayView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picBoxView);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "DisplayView";
            this.Size = new System.Drawing.Size(500, 500);
            this.Load += new System.EventHandler(this.DisplayView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxView;
    }
}
