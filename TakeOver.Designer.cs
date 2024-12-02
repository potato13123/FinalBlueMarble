namespace BlueMarbleProject
{
    partial class TakeOver
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
            this.lbNowMoney = new System.Windows.Forms.Label();
            this.lbAreaName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbPayMoney = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbNowMoney
            // 
            this.lbNowMoney.AutoSize = true;
            this.lbNowMoney.Location = new System.Drawing.Point(346, 600);
            this.lbNowMoney.Name = "lbNowMoney";
            this.lbNowMoney.Size = new System.Drawing.Size(115, 18);
            this.lbNowMoney.TabIndex = 0;
            this.lbNowMoney.Text = "aftertakeover";
            // 
            // lbAreaName
            // 
            this.lbAreaName.AutoSize = true;
            this.lbAreaName.Location = new System.Drawing.Point(219, 225);
            this.lbAreaName.Name = "lbAreaName";
            this.lbAreaName.Size = new System.Drawing.Size(62, 18);
            this.lbAreaName.TabIndex = 1;
            this.lbAreaName.Text = "지역명";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "인수 비용 >> ";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(109, 468);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(146, 75);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(349, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(149, 80);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbPayMoney
            // 
            this.lbPayMoney.AutoSize = true;
            this.lbPayMoney.Location = new System.Drawing.Point(335, 297);
            this.lbPayMoney.Name = "lbPayMoney";
            this.lbPayMoney.Size = new System.Drawing.Size(118, 18);
            this.lbPayMoney.TabIndex = 6;
            this.lbPayMoney.Text = "takeoverprice";
            // 
            // TakeOver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 687);
            this.Controls.Add(this.lbPayMoney);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbAreaName);
            this.Controls.Add(this.lbNowMoney);
            this.Name = "TakeOver";
            this.Text = "TakeOver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbNowMoney;
        private System.Windows.Forms.Label lbAreaName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbPayMoney;
    }
}