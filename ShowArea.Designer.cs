namespace BlueMarbleProject
{
    partial class ShowArea
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.lbAngel = new System.Windows.Forms.Label();
            this.lbEscape = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.AreaName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BuildName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AreaName,
            this.BuildName});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(59, 88);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(432, 265);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // lbAngel
            // 
            this.lbAngel.AutoSize = true;
            this.lbAngel.Location = new System.Drawing.Point(92, 410);
            this.lbAngel.Name = "lbAngel";
            this.lbAngel.Size = new System.Drawing.Size(104, 18);
            this.lbAngel.TabIndex = 1;
            this.lbAngel.Text = "우대권 개수";
            // 
            // lbEscape
            // 
            this.lbEscape.AutoSize = true;
            this.lbEscape.Location = new System.Drawing.Point(92, 456);
            this.lbEscape.Name = "lbEscape";
            this.lbEscape.Size = new System.Drawing.Size(104, 18);
            this.lbEscape.TabIndex = 2;
            this.lbEscape.Text = "탈출권 개수";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(135, 596);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // AreaName
            // 
            this.AreaName.Text = "지역 이름";
            this.AreaName.Width = 198;
            // 
            // BuildName
            // 
            this.BuildName.Text = "건물 이름";
            this.BuildName.Width = 221;
            // 
            // ShowArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 714);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbEscape);
            this.Controls.Add(this.lbAngel);
            this.Controls.Add(this.listView1);
            this.Name = "ShowArea";
            this.Text = "ShowArea";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lbAngel;
        private System.Windows.Forms.Label lbEscape;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColumnHeader AreaName;
        private System.Windows.Forms.ColumnHeader BuildName;
    }
}