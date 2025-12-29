namespace Anasayfa1
{
    partial class Profil
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Profil));
            this.button1 = new System.Windows.Forms.Button();
            this.pnlAnaEkran = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uC_Ayarlar1 = new Anasayfa1.UC_Ayarlar();
            this.profilfoto_isim1 = new Anasayfa1.Profilfoto_isim();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(-5, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 50);
            this.button1.TabIndex = 14;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pnlAnaEkran
            // 
            this.pnlAnaEkran.BackColor = System.Drawing.Color.SeaShell;
            this.pnlAnaEkran.Location = new System.Drawing.Point(52, 378);
            this.pnlAnaEkran.Margin = new System.Windows.Forms.Padding(4);
            this.pnlAnaEkran.Name = "pnlAnaEkran";
            this.pnlAnaEkran.Size = new System.Drawing.Size(1151, 299);
            this.pnlAnaEkran.TabIndex = 2;
            this.pnlAnaEkran.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAnaEkran_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uC_Ayarlar1);
            this.panel1.Location = new System.Drawing.Point(52, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(927, 351);
            this.panel1.TabIndex = 0;
            // 
            // uC_Ayarlar1
            // 
            this.uC_Ayarlar1.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.uC_Ayarlar1.Location = new System.Drawing.Point(20, 17);
            this.uC_Ayarlar1.Margin = new System.Windows.Forms.Padding(5);
            this.uC_Ayarlar1.Name = "uC_Ayarlar1";
            this.uC_Ayarlar1.Size = new System.Drawing.Size(901, 318);
            this.uC_Ayarlar1.TabIndex = 0;
            this.uC_Ayarlar1.Load += new System.EventHandler(this.uC_Ayarlar1_Load);
            // 
            // profilfoto_isim1
            // 
            this.profilfoto_isim1.BackColor = System.Drawing.Color.Transparent;
            this.profilfoto_isim1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profilfoto_isim1.Location = new System.Drawing.Point(1016, 15);
            this.profilfoto_isim1.Margin = new System.Windows.Forms.Padding(5);
            this.profilfoto_isim1.Name = "profilfoto_isim1";
            this.profilfoto_isim1.Size = new System.Drawing.Size(230, 335);
            this.profilfoto_isim1.TabIndex = 12;
            this.profilfoto_isim1.Load += new System.EventHandler(this.profilfoto_isim1_Load);
            // 
            // Profil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1275, 692);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.profilfoto_isim1);
            this.Controls.Add(this.pnlAnaEkran);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Profil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profil";
            this.Load += new System.EventHandler(this.Profil_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnlAnaEkran;
        private System.Windows.Forms.Panel panel1;
        private UC_Ayarlar uC_Ayarlar1;
        private Profilfoto_isim profilfoto_isim1;
    }
}