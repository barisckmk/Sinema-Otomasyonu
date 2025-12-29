namespace Anasayfa1
{
    partial class BiletCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BiletCard));
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lblBiletAdi = new System.Windows.Forms.Label();
            this.lblBiletFiyati = new System.Windows.Forms.Label();
            this.btnMinus = new System.Windows.Forms.Button();
            this.lblAdet = new System.Windows.Forms.Label();
            this.btnPlus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pbIcon
            // 
            this.pbIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbIcon.Image")));
            this.pbIcon.Location = new System.Drawing.Point(15, 20);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(39, 36);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            // 
            // lblBiletAdi
            // 
            this.lblBiletAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBiletAdi.ForeColor = System.Drawing.Color.White;
            this.lblBiletAdi.Location = new System.Drawing.Point(60, 26);
            this.lblBiletAdi.Name = "lblBiletAdi";
            this.lblBiletAdi.Size = new System.Drawing.Size(500, 32);
            this.lblBiletAdi.TabIndex = 1;
            this.lblBiletAdi.Text = "label1";
            this.lblBiletAdi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBiletFiyati
            // 
            this.lblBiletFiyati.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBiletFiyati.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(100)))));
            this.lblBiletFiyati.Location = new System.Drawing.Point(820, 26);
            this.lblBiletFiyati.Name = "lblBiletFiyati";
            this.lblBiletFiyati.Size = new System.Drawing.Size(120, 22);
            this.lblBiletFiyati.TabIndex = 2;
            this.lblBiletFiyati.Text = "label1";
            this.lblBiletFiyati.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBiletFiyati.Click += new System.EventHandler(this.lblBiletFiyati_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMinus.ForeColor = System.Drawing.Color.White;
            this.btnMinus.Location = new System.Drawing.Point(967, 26);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(26, 32);
            this.btnMinus.TabIndex = 3;
            this.btnMinus.Text = "<";
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // lblAdet
            // 
            this.lblAdet.BackColor = System.Drawing.Color.Transparent;
            this.lblAdet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdet.ForeColor = System.Drawing.Color.White;
            this.lblAdet.Location = new System.Drawing.Point(1001, 29);
            this.lblAdet.Name = "lblAdet";
            this.lblAdet.Size = new System.Drawing.Size(53, 22);
            this.lblAdet.TabIndex = 4;
            this.lblAdet.Text = "0";
            // 
            // btnPlus
            // 
            this.btnPlus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPlus.ForeColor = System.Drawing.Color.White;
            this.btnPlus.Location = new System.Drawing.Point(1033, 24);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(26, 32);
            this.btnPlus.TabIndex = 6;
            this.btnPlus.Text = ">";
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click_1);
            // 
            // BiletCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.lblAdet);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.lblBiletFiyati);
            this.Controls.Add(this.lblBiletAdi);
            this.Controls.Add(this.pbIcon);
            this.Margin = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.Name = "BiletCard";
            this.Size = new System.Drawing.Size(1250, 75);
            this.Load += new System.EventHandler(this.BiletCard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Label lblAdet;
        public System.Windows.Forms.Label lblBiletAdi;
        public System.Windows.Forms.Label lblBiletFiyati;
        private System.Windows.Forms.Button btnPlus;
    }
}
