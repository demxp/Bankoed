namespace BANCOED
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.hpperc = new System.Windows.Forms.ComboBox();
            this.mpperc = new System.Windows.Forms.ComboBox();
            this.flypot = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.atcSpeed = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.currAtkSpeed = new System.Windows.Forms.TextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.healpotion = new System.Windows.Forms.ComboBox();
            this.manapotion = new System.Windows.Forms.ComboBox();
            this.hpform = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorProgressBar3 = new ColorProgressBar.ColorProgressBar();
            this.colorProgressBar2 = new ColorProgressBar.ColorProgressBar();
            this.colorProgressBar1 = new ColorProgressBar.ColorProgressBar();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButton3
            // 
            resources.ApplyResources(this.radioButton3, "radioButton3");
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Click += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Checked = true;
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // hpperc
            // 
            this.hpperc.BackColor = System.Drawing.Color.White;
            this.hpperc.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.hpperc.DisplayMember = "60%";
            this.hpperc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.hpperc, "hpperc");
            this.hpperc.FormattingEnabled = true;
            this.hpperc.Items.AddRange(new object[] {
            resources.GetString("hpperc.Items"),
            resources.GetString("hpperc.Items1"),
            resources.GetString("hpperc.Items2"),
            resources.GetString("hpperc.Items3"),
            resources.GetString("hpperc.Items4"),
            resources.GetString("hpperc.Items5"),
            resources.GetString("hpperc.Items6"),
            resources.GetString("hpperc.Items7"),
            resources.GetString("hpperc.Items8"),
            resources.GetString("hpperc.Items9")});
            this.hpperc.Name = "hpperc";
            this.hpperc.ValueMember = "60%";
            this.hpperc.SelectedIndexChanged += new System.EventHandler(this.hpperc_SelectedIndexChanged);
            // 
            // mpperc
            // 
            this.mpperc.BackColor = System.Drawing.Color.White;
            this.mpperc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.mpperc, "mpperc");
            this.mpperc.FormattingEnabled = true;
            this.mpperc.Items.AddRange(new object[] {
            resources.GetString("mpperc.Items"),
            resources.GetString("mpperc.Items1"),
            resources.GetString("mpperc.Items2"),
            resources.GetString("mpperc.Items3"),
            resources.GetString("mpperc.Items4"),
            resources.GetString("mpperc.Items5"),
            resources.GetString("mpperc.Items6"),
            resources.GetString("mpperc.Items7"),
            resources.GetString("mpperc.Items8"),
            resources.GetString("mpperc.Items9")});
            this.mpperc.Name = "mpperc";
            this.mpperc.SelectedIndexChanged += new System.EventHandler(this.mpperc_SelectedIndexChanged);
            // 
            // flypot
            // 
            this.flypot.BackColor = System.Drawing.Color.White;
            this.flypot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.flypot, "flypot");
            this.flypot.FormattingEnabled = true;
            this.flypot.Name = "flypot";
            this.flypot.SelectedIndexChanged += new System.EventHandler(this.flypot_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Name = "label1";
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // atcSpeed
            // 
            this.atcSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.atcSpeed, "atcSpeed");
            this.atcSpeed.FormattingEnabled = true;
            this.atcSpeed.Items.AddRange(new object[] {
            resources.GetString("atcSpeed.Items"),
            resources.GetString("atcSpeed.Items1"),
            resources.GetString("atcSpeed.Items2"),
            resources.GetString("atcSpeed.Items3"),
            resources.GetString("atcSpeed.Items4"),
            resources.GetString("atcSpeed.Items5"),
            resources.GetString("atcSpeed.Items6"),
            resources.GetString("atcSpeed.Items7"),
            resources.GetString("atcSpeed.Items8")});
            this.atcSpeed.Name = "atcSpeed";
            this.atcSpeed.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // currAtkSpeed
            // 
            resources.ApplyResources(this.currAtkSpeed, "currAtkSpeed");
            this.currAtkSpeed.Name = "currAtkSpeed";
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // healpotion
            // 
            this.healpotion.BackColor = System.Drawing.Color.White;
            this.healpotion.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.healpotion.DisplayMember = "60%";
            this.healpotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.healpotion, "healpotion");
            this.healpotion.FormattingEnabled = true;
            this.healpotion.Name = "healpotion";
            this.healpotion.ValueMember = "60%";
            this.healpotion.SelectedIndexChanged += new System.EventHandler(this.healpotion_SelectedIndexChanged);
            // 
            // manapotion
            // 
            this.manapotion.BackColor = System.Drawing.Color.White;
            this.manapotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.manapotion, "manapotion");
            this.manapotion.FormattingEnabled = true;
            this.manapotion.Name = "manapotion";
            this.manapotion.SelectedIndexChanged += new System.EventHandler(this.manapotion_SelectedIndexChanged);
            // 
            // hpform
            // 
            resources.ApplyResources(this.hpform, "hpform");
            this.hpform.Name = "hpform";
            this.hpform.UseVisualStyleBackColor = true;
            this.hpform.CheckedChanged += new System.EventHandler(this.hpform_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // colorProgressBar3
            // 
            this.colorProgressBar3.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.colorProgressBar3.BorderColor = System.Drawing.Color.Black;
            this.colorProgressBar3.CustomText = null;
            this.colorProgressBar3.DisplayStyle = ColorProgressBar.ColorProgressBar.ProgressBarDisplayText.Percentage;
            this.colorProgressBar3.FillStyle = ColorProgressBar.ColorProgressBar.FillStyles.Solid;
            resources.ApplyResources(this.colorProgressBar3, "colorProgressBar3");
            this.colorProgressBar3.Maximum = 100;
            this.colorProgressBar3.Minimum = 0;
            this.colorProgressBar3.Name = "colorProgressBar3";
            this.colorProgressBar3.ShowPerc = true;
            this.colorProgressBar3.Step = 10;
            this.colorProgressBar3.Value = 1;
            // 
            // colorProgressBar2
            // 
            this.colorProgressBar2.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.colorProgressBar2.BorderColor = System.Drawing.Color.Black;
            this.colorProgressBar2.CustomText = null;
            this.colorProgressBar2.DisplayStyle = ColorProgressBar.ColorProgressBar.ProgressBarDisplayText.Percentage;
            this.colorProgressBar2.FillStyle = ColorProgressBar.ColorProgressBar.FillStyles.Solid;
            resources.ApplyResources(this.colorProgressBar2, "colorProgressBar2");
            this.colorProgressBar2.Maximum = 100;
            this.colorProgressBar2.Minimum = 0;
            this.colorProgressBar2.Name = "colorProgressBar2";
            this.colorProgressBar2.ShowPerc = true;
            this.colorProgressBar2.Step = 10;
            this.colorProgressBar2.Value = 1;
            // 
            // colorProgressBar1
            // 
            this.colorProgressBar1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.colorProgressBar1.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.colorProgressBar1.BorderColor = System.Drawing.Color.Black;
            this.colorProgressBar1.CustomText = null;
            this.colorProgressBar1.DisplayStyle = ColorProgressBar.ColorProgressBar.ProgressBarDisplayText.Percentage;
            this.colorProgressBar1.FillStyle = ColorProgressBar.ColorProgressBar.FillStyles.Solid;
            resources.ApplyResources(this.colorProgressBar1, "colorProgressBar1");
            this.colorProgressBar1.Maximum = 100;
            this.colorProgressBar1.Minimum = 0;
            this.colorProgressBar1.Name = "colorProgressBar1";
            this.colorProgressBar1.ShowPerc = true;
            this.colorProgressBar1.Step = 10;
            this.colorProgressBar1.Value = 1;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.hpform);
            this.Controls.Add(this.manapotion);
            this.Controls.Add(this.healpotion);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.currAtkSpeed);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.atcSpeed);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.colorProgressBar3);
            this.Controls.Add(this.colorProgressBar2);
            this.Controls.Add(this.colorProgressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.flypot);
            this.Controls.Add(this.mpperc);
            this.Controls.Add(this.hpperc);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox hpperc;
        private System.Windows.Forms.ComboBox mpperc;
        private System.Windows.Forms.ComboBox flypot;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private ColorProgressBar.ColorProgressBar colorProgressBar1;
        private ColorProgressBar.ColorProgressBar colorProgressBar2;
        private ColorProgressBar.ColorProgressBar colorProgressBar3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox atcSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox currAtkSpeed;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ComboBox healpotion;
        private System.Windows.Forms.ComboBox manapotion;
        private System.Windows.Forms.CheckBox hpform;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    }
}

