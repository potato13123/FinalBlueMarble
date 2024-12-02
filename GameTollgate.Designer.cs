namespace BlueMarbleProject
{
    partial class GameTollgate
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
            this.lbPayMoney = new System.Windows.Forms.Label();
            this.lbNowMoney = new System.Windows.Forms.Label();
            this.btnFree = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbAreaName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbPayMoney
            // 
            this.lbPayMoney.AutoSize = true;
            this.lbPayMoney.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPayMoney.Location = new System.Drawing.Point(82, 370);
            this.lbPayMoney.Name = "lbPayMoney";
            this.lbPayMoney.Size = new System.Drawing.Size(77, 25);
            this.lbPayMoney.TabIndex = 0;
            this.lbPayMoney.Text = "통행료: ";
            // 
            // lbNowMoney
            // 
            this.lbNowMoney.AutoSize = true;
            this.lbNowMoney.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbNowMoney.Location = new System.Drawing.Point(82, 459);
            this.lbNowMoney.Name = "lbNowMoney";
            this.lbNowMoney.Size = new System.Drawing.Size(125, 25);
            this.lbNowMoney.TabIndex = 1;
            this.lbNowMoney.Text = "지불 후 금액: ";
            // 
            // btnFree
            // 
            this.btnFree.Location = new System.Drawing.Point(535, 370);
            this.btnFree.Name = "btnFree";
            this.btnFree.Size = new System.Drawing.Size(174, 88);
            this.btnFree.TabIndex = 2;
            this.btnFree.Text = "우대권 사용";
            this.btnFree.UseVisualStyleBackColor = true;
            this.btnFree.Click += new System.EventHandler(this.btnFree_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(535, 479);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(185, 118);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbAreaName
            // 
            this.lbAreaName.AutoSize = true;
            this.lbAreaName.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbAreaName.Location = new System.Drawing.Point(82, 256);
            this.lbAreaName.Name = "lbAreaName";
            this.lbAreaName.Size = new System.Drawing.Size(48, 25);
            this.lbAreaName.TabIndex = 4;
            this.lbAreaName.Text = "지명";
            // 
            // GameTollgate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 780);
            this.Controls.Add(this.lbAreaName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnFree);
            this.Controls.Add(this.lbNowMoney);
            this.Controls.Add(this.lbPayMoney);
            this.Name = "GameTollgate";
            this.Text = "GameTallgate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPayMoney;
        private System.Windows.Forms.Label lbNowMoney;
        private System.Windows.Forms.Button btnFree;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbAreaName;
    }
}