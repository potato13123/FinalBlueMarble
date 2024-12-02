namespace BlueMarbleProject
{
    partial class Building
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Building));
            this.Villa = new System.Windows.Forms.RadioButton();
            this.Apart = new System.Windows.Forms.RadioButton();
            this.Hotel = new System.Windows.Forms.RadioButton();
            this.Randmark = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.areaName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Villa
            // 
            this.Villa.AutoSize = true;
            this.Villa.Location = new System.Drawing.Point(52, 74);
            this.Villa.Name = "Villa";
            this.Villa.Size = new System.Drawing.Size(69, 22);
            this.Villa.TabIndex = 0;
            this.Villa.TabStop = true;
            this.Villa.Text = "빌라";
            this.Villa.UseVisualStyleBackColor = true;
            // 
            // Apart
            // 
            this.Apart.AutoSize = true;
            this.Apart.Location = new System.Drawing.Point(243, 74);
            this.Apart.Name = "Apart";
            this.Apart.Size = new System.Drawing.Size(69, 22);
            this.Apart.TabIndex = 1;
            this.Apart.TabStop = true;
            this.Apart.Text = "빌딩";
            this.Apart.UseVisualStyleBackColor = true;
            // 
            // Hotel
            // 
            this.Hotel.AutoSize = true;
            this.Hotel.Location = new System.Drawing.Point(52, 183);
            this.Hotel.Name = "Hotel";
            this.Hotel.Size = new System.Drawing.Size(69, 22);
            this.Hotel.TabIndex = 2;
            this.Hotel.TabStop = true;
            this.Hotel.Text = "호텔";
            this.Hotel.UseVisualStyleBackColor = true;
            // 
            // Randmark
            // 
            this.Randmark.AutoSize = true;
            this.Randmark.Location = new System.Drawing.Point(243, 183);
            this.Randmark.Name = "Randmark";
            this.Randmark.Size = new System.Drawing.Size(105, 22);
            this.Randmark.TabIndex = 3;
            this.Randmark.TabStop = true;
            this.Randmark.Text = "랜드마크";
            this.Randmark.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Randmark);
            this.groupBox1.Controls.Add(this.Apart);
            this.groupBox1.Controls.Add(this.Hotel);
            this.groupBox1.Controls.Add(this.Villa);
            this.groupBox1.Location = new System.Drawing.Point(147, 211);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 260);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(174, 516);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(160, 89);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // areaName
            // 
            this.areaName.AutoSize = true;
            this.areaName.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.areaName.Location = new System.Drawing.Point(301, 118);
            this.areaName.Name = "areaName";
            this.areaName.Size = new System.Drawing.Size(124, 65);
            this.areaName.TabIndex = 5;
            this.areaName.Text = "지명";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(406, 516);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 89);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Level__1.png");
            this.imageList1.Images.SetKeyName(1, "Level__2.png");
            this.imageList1.Images.SetKeyName(2, "Level__3.png");
            this.imageList1.Images.SetKeyName(3, "LandMark_1.png");
            // 
            // Building
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 672);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.areaName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Name = "Building";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Villa;
        private System.Windows.Forms.RadioButton Apart;
        private System.Windows.Forms.RadioButton Hotel;
        private System.Windows.Forms.RadioButton Randmark;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label areaName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imageList1;
    }
}