namespace BlueMarbleProject
{
    partial class GameModeScreen
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Start_Btn = new System.Windows.Forms.Button();
            this.P4_RadioBtn = new System.Windows.Forms.RadioButton();
            this.P3_RadioBtn = new System.Windows.Forms.RadioButton();
            this.P2_RadioBtn = new System.Windows.Forms.RadioButton();
            this.SelectTeam_RadioBtn = new System.Windows.Forms.RadioButton();
            this.SelectSolo_RadioBtn = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SelectTeam_RadioBtn);
            this.groupBox1.Controls.Add(this.SelectSolo_RadioBtn);
            this.groupBox1.Location = new System.Drawing.Point(138, 421);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(799, 252);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.P4_RadioBtn);
            this.groupBox2.Controls.Add(this.P3_RadioBtn);
            this.groupBox2.Controls.Add(this.P2_RadioBtn);
            this.groupBox2.Location = new System.Drawing.Point(138, 136);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(799, 254);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "인원수";
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(380, 806);
            this.Start_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(304, 87);
            this.Start_Btn.TabIndex = 3;
            this.Start_Btn.Text = "선택 완료";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // P4_RadioBtn
            // 
            this.P4_RadioBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.P4_RadioBtn.Image = global::BlueMarbleProject.Properties.Resources._4P;
            this.P4_RadioBtn.Location = new System.Drawing.Point(558, 73);
            this.P4_RadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.P4_RadioBtn.Name = "P4_RadioBtn";
            this.P4_RadioBtn.Size = new System.Drawing.Size(166, 100);
            this.P4_RadioBtn.TabIndex = 2;
            this.P4_RadioBtn.TabStop = true;
            this.P4_RadioBtn.UseVisualStyleBackColor = true;
            this.P4_RadioBtn.CheckedChanged += new System.EventHandler(this.P4_RadioBtn_CheckedChanged);
            // 
            // P3_RadioBtn
            // 
            this.P3_RadioBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.P3_RadioBtn.Image = global::BlueMarbleProject.Properties.Resources._3P;
            this.P3_RadioBtn.Location = new System.Drawing.Point(332, 73);
            this.P3_RadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.P3_RadioBtn.Name = "P3_RadioBtn";
            this.P3_RadioBtn.Size = new System.Drawing.Size(116, 94);
            this.P3_RadioBtn.TabIndex = 1;
            this.P3_RadioBtn.TabStop = true;
            this.P3_RadioBtn.UseVisualStyleBackColor = true;
            this.P3_RadioBtn.CheckedChanged += new System.EventHandler(this.P3_RadioBtn_CheckedChanged);
            // 
            // P2_RadioBtn
            // 
            this.P2_RadioBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.P2_RadioBtn.Image = global::BlueMarbleProject.Properties.Resources._2P;
            this.P2_RadioBtn.Location = new System.Drawing.Point(128, 73);
            this.P2_RadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.P2_RadioBtn.Name = "P2_RadioBtn";
            this.P2_RadioBtn.Size = new System.Drawing.Size(81, 94);
            this.P2_RadioBtn.TabIndex = 0;
            this.P2_RadioBtn.TabStop = true;
            this.P2_RadioBtn.UseVisualStyleBackColor = true;
            this.P2_RadioBtn.CheckedChanged += new System.EventHandler(this.P2_RadioBtn_CheckedChanged);
            // 
            // SelectTeam_RadioBtn
            // 
            this.SelectTeam_RadioBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.SelectTeam_RadioBtn.Image = global::BlueMarbleProject.Properties.Resources.Team;
            this.SelectTeam_RadioBtn.Location = new System.Drawing.Point(493, 116);
            this.SelectTeam_RadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.SelectTeam_RadioBtn.Name = "SelectTeam_RadioBtn";
            this.SelectTeam_RadioBtn.Size = new System.Drawing.Size(175, 65);
            this.SelectTeam_RadioBtn.TabIndex = 1;
            this.SelectTeam_RadioBtn.TabStop = true;
            this.SelectTeam_RadioBtn.UseVisualStyleBackColor = true;
            this.SelectTeam_RadioBtn.CheckedChanged += new System.EventHandler(this.SelectTeam_RadioBtn_CheckedChanged);
            // 
            // SelectSolo_RadioBtn
            // 
            this.SelectSolo_RadioBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.SelectSolo_RadioBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SelectSolo_RadioBtn.Image = global::BlueMarbleProject.Properties.Resources.Solo;
            this.SelectSolo_RadioBtn.Location = new System.Drawing.Point(128, 114);
            this.SelectSolo_RadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.SelectSolo_RadioBtn.Name = "SelectSolo_RadioBtn";
            this.SelectSolo_RadioBtn.Size = new System.Drawing.Size(175, 65);
            this.SelectSolo_RadioBtn.TabIndex = 0;
            this.SelectSolo_RadioBtn.TabStop = true;
            this.SelectSolo_RadioBtn.UseVisualStyleBackColor = true;
            this.SelectSolo_RadioBtn.CheckedChanged += new System.EventHandler(this.SelectSolo_RadioBtn_CheckedChanged);
            // 
            // GameModeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 946);
            this.Controls.Add(this.Start_Btn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameModeScreen";
            this.Text = "GameModeScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameModeScreen_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton SelectTeam_RadioBtn;
        private System.Windows.Forms.RadioButton SelectSolo_RadioBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton P4_RadioBtn;
        private System.Windows.Forms.RadioButton P3_RadioBtn;
        private System.Windows.Forms.RadioButton P2_RadioBtn;
        private System.Windows.Forms.Button Start_Btn;
    }
}