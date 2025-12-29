namespace Anasayfa1
{
    partial class BufeCard
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
            this.pbUrunResmi = new System.Windows.Forms.PictureBox();
            this.lblUrunAdi = new System.Windows.Forms.Label();
            this.lblFiyat = new System.Windows.Forms.Label();
            this.btnMinuss = new System.Windows.Forms.Button();
            this.lblAdett = new System.Windows.Forms.Label();
            this.btnPluss = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbUrunResmi)).BeginInit();
            this.SuspendLayout();
            // 
            // pbUrunResmi
            // 
            this.pbUrunResmi.Location = new System.Drawing.Point(42, 10);
            this.pbUrunResmi.Name = "pbUrunResmi";
            this.pbUrunResmi.Size = new System.Drawing.Size(140, 100);
            this.pbUrunResmi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbUrunResmi.TabIndex = 0;
            this.pbUrunResmi.TabStop = false;
            // 
            // lblUrunAdi
            // 
            this.lblUrunAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUrunAdi.ForeColor = System.Drawing.Color.White;
            this.lblUrunAdi.Location = new System.Drawing.Point(0, 120);
            this.lblUrunAdi.Name = "lblUrunAdi";
            this.lblUrunAdi.Size = new System.Drawing.Size(230, 22);
            this.lblUrunAdi.TabIndex = 1;
            this.lblUrunAdi.Text = "label1";
            this.lblUrunAdi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFiyat
            // 
            this.lblFiyat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFiyat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(100)))));
            this.lblFiyat.Location = new System.Drawing.Point(19, 150);
            this.lblFiyat.Name = "lblFiyat";
            this.lblFiyat.Size = new System.Drawing.Size(180, 26);
            this.lblFiyat.TabIndex = 2;
            this.lblFiyat.Text = "label1";
            this.lblFiyat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFiyat.Click += new System.EventHandler(this.lblFiyat_Click);
            // 
            // btnMinuss
            // 
            this.btnMinuss.Location = new System.Drawing.Point(58, 190);
            this.btnMinuss.Name = "btnMinuss";
            this.btnMinuss.Size = new System.Drawing.Size(28, 28);
            this.btnMinuss.TabIndex = 3;
            this.btnMinuss.Text = "<";
            this.btnMinuss.UseVisualStyleBackColor = true;
            this.btnMinuss.Click += new System.EventHandler(this.btnMinuss_Click);
            // 
            // lblAdett
            // 
            this.lblAdett.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdett.ForeColor = System.Drawing.Color.White;
            this.lblAdett.Location = new System.Drawing.Point(93, 193);
            this.lblAdett.Name = "lblAdett";
            this.lblAdett.Size = new System.Drawing.Size(30, 22);
            this.lblAdett.TabIndex = 4;
            this.lblAdett.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPluss
            // 
            this.btnPluss.Location = new System.Drawing.Point(128, 190);
            this.btnPluss.Name = "btnPluss";
            this.btnPluss.Size = new System.Drawing.Size(28, 28);
            this.btnPluss.TabIndex = 5;
            this.btnPluss.Text = ">";
            this.btnPluss.UseVisualStyleBackColor = true;
            this.btnPluss.Click += new System.EventHandler(this.btnPluss_Click);
            // 
            // BufeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Controls.Add(this.btnPluss);
            this.Controls.Add(this.lblAdett);
            this.Controls.Add(this.btnMinuss);
            this.Controls.Add(this.lblFiyat);
            this.Controls.Add(this.lblUrunAdi);
            this.Controls.Add(this.pbUrunResmi);
            this.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Name = "BufeCard";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(230, 240);
            this.Load += new System.EventHandler(this.BufeCard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbUrunResmi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pbUrunResmi;
        public System.Windows.Forms.Label lblUrunAdi;
        public System.Windows.Forms.Label lblFiyat;
        public System.Windows.Forms.Button btnMinuss;
        public System.Windows.Forms.Label lblAdett;
        public System.Windows.Forms.Button btnPluss;
    }
}
