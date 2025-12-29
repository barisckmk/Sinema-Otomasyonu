namespace Anasayfa1
{
    partial class KoltukOnayFormu
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.lblSalon = new System.Windows.Forms.Label();
            this.lblKoltuklar = new System.Windows.Forms.Label();
            this.btnDevam = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.YellowGreen;
            this.pnlHeader.Controls.Add(this.lblBaslik);
            this.pnlHeader.Location = new System.Drawing.Point(48, 31);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(395, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblBaslik
            // 
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(59, 12);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(268, 25);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "Koltuk Seçimi Tamamlandı";
            this.lblBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.pnlCenter);
            this.pnlBody.Controls.Add(this.btnDevam);
            this.pnlBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.pnlBody.Location = new System.Drawing.Point(13, 82);
            this.pnlBody.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(484, 283);
            this.pnlBody.TabIndex = 1;
            this.pnlBody.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBody_Paint);
            // 
            // pnlCenter
            // 
            this.pnlCenter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCenter.AutoSize = true;
            this.pnlCenter.BackColor = System.Drawing.Color.Transparent;
            this.pnlCenter.Controls.Add(this.lblSalon);
            this.pnlCenter.Controls.Add(this.lblKoltuklar);
            this.pnlCenter.Location = new System.Drawing.Point(35, 39);
            this.pnlCenter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(413, 139);
            this.pnlCenter.TabIndex = 3;
            // 
            // lblSalon
            // 
            this.lblSalon.AutoSize = true;
            this.lblSalon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSalon.ForeColor = System.Drawing.Color.White;
            this.lblSalon.Location = new System.Drawing.Point(3, 70);
            this.lblSalon.Name = "lblSalon";
            this.lblSalon.Size = new System.Drawing.Size(59, 20);
            this.lblSalon.TabIndex = 0;
            this.lblSalon.Text = "label1";
            this.lblSalon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKoltuklar
            // 
            this.lblKoltuklar.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKoltuklar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKoltuklar.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblKoltuklar.Location = new System.Drawing.Point(0, 0);
            this.lblKoltuklar.MaximumSize = new System.Drawing.Size(360, 0);
            this.lblKoltuklar.Name = "lblKoltuklar";
            this.lblKoltuklar.Size = new System.Drawing.Size(360, 0);
            this.lblKoltuklar.TabIndex = 1;
            this.lblKoltuklar.Text = "label1";
            this.lblKoltuklar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDevam
            // 
            this.btnDevam.BackColor = System.Drawing.Color.YellowGreen;
            this.btnDevam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDevam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDevam.ForeColor = System.Drawing.Color.White;
            this.btnDevam.Location = new System.Drawing.Point(185, 226);
            this.btnDevam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDevam.Name = "btnDevam";
            this.btnDevam.Size = new System.Drawing.Size(119, 43);
            this.btnDevam.TabIndex = 2;
            this.btnDevam.Text = "Devam Et ";
            this.btnDevam.UseVisualStyleBackColor = false;
            this.btnDevam.Click += new System.EventHandler(this.button1_Click);
            // 
            // KoltukOnayFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(509, 377);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "KoltukOnayFormu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KoltukOnayFormu";
            this.Load += new System.EventHandler(this.KoltukOnayFormu_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            this.pnlCenter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Button btnDevam;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Label lblSalon;
        private System.Windows.Forms.Label lblKoltuklar;
    }
}