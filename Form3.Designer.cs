namespace BANCOED
{
    partial class Form3
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
            this.tname = new System.Windows.Forms.Label();
            this.targethp = new System.Windows.Forms.Label();
            this.targHPperc = new ColorProgressBar.ColorProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tname
            // 
            this.tname.AutoSize = true;
            this.tname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tname.ForeColor = System.Drawing.Color.Yellow;
            this.tname.Location = new System.Drawing.Point(3, 5);
            this.tname.Name = "tname";
            this.tname.Size = new System.Drawing.Size(70, 13);
            this.tname.TabIndex = 1;
            this.tname.Text = "ИМЯМОБА";
            // 
            // targethp
            // 
            this.targethp.AutoSize = true;
            this.targethp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.targethp.ForeColor = System.Drawing.Color.Lime;
            this.targethp.Location = new System.Drawing.Point(3, 26);
            this.targethp.Name = "targethp";
            this.targethp.Size = new System.Drawing.Size(59, 13);
            this.targethp.TabIndex = 2;
            this.targethp.Text = "ХПМОБА";
            // 
            // targHPperc
            // 
            this.targHPperc.BarColor = System.Drawing.Color.Red;
            this.targHPperc.BorderColor = System.Drawing.Color.Black;
            this.targHPperc.CustomText = null;
            this.targHPperc.DisplayStyle = ColorProgressBar.ColorProgressBar.ProgressBarDisplayText.Percentage;
            this.targHPperc.FillStyle = ColorProgressBar.ColorProgressBar.FillStyles.Solid;
            this.targHPperc.Location = new System.Drawing.Point(6, 47);
            this.targHPperc.Maximum = 100;
            this.targHPperc.Minimum = 0;
            this.targHPperc.Name = "targHPperc";
            this.targHPperc.ShowPerc = false;
            this.targHPperc.Size = new System.Drawing.Size(228, 7);
            this.targHPperc.Step = 10;
            this.targHPperc.TabIndex = 3;
            this.targHPperc.Text = "colorProgressBar1";
            this.targHPperc.Value = 20;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.targHPperc);
            this.panel1.Controls.Add(this.tname);
            this.panel1.Controls.Add(this.targethp);
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 60);
            this.panel1.TabIndex = 5;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form3_MouseDown);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Magenta;
            this.ClientSize = new System.Drawing.Size(253, 68);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Magenta;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form3";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label tname;
        public System.Windows.Forms.Label targethp;
        public ColorProgressBar.ColorProgressBar targHPperc;
    }
}