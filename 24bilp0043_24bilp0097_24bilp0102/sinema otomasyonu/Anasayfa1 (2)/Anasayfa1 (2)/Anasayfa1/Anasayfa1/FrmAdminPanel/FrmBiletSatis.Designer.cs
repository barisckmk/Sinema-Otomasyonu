namespace Anasayfa1
{
    partial class FrmBiletSatis
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.cmbFilm = new System.Windows.Forms.ComboBox();
            this.cmbSalon = new System.Windows.Forms.ComboBox();
            this.cmbSeans = new System.Windows.Forms.ComboBox();
            this.dtpTarih = new System.Windows.Forms.DateTimePicker();
            this.btnSatis = new System.Windows.Forms.Button();
            this.lblFilm = new System.Windows.Forms.Label();
            this.lblSalon = new System.Windows.Forms.Label();
            this.lblSeans = new System.Windows.Forms.Label();
            this.lblTarih = new System.Windows.Forms.Label();
            this.lblSecilenKoltuklar = new System.Windows.Forms.Label();
            this.btnKoltukSeç = new System.Windows.Forms.Button();
            this.lblToplamTutar = new System.Windows.Forms.Label();
            this.nudTam = new System.Windows.Forms.NumericUpDown();
            this.nudOgrenci = new System.Windows.Forms.NumericUpDown();
            this.lblTam = new System.Windows.Forms.Label();
            this.lblOgrenci = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOgrenci)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbFilm
            // 
            this.cmbFilm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilm.Location = new System.Drawing.Point(150, 30);
            this.cmbFilm.Name = "cmbFilm";
            this.cmbFilm.Size = new System.Drawing.Size(200, 24);
            this.cmbFilm.TabIndex = 3;
            this.cmbFilm.SelectedIndexChanged += new System.EventHandler(this.cmbFilm_SelectedIndexChanged);
            // 
            // cmbSalon
            // 
            this.cmbSalon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalon.Location = new System.Drawing.Point(150, 70);
            this.cmbSalon.Name = "cmbSalon";
            this.cmbSalon.Size = new System.Drawing.Size(200, 24);
            this.cmbSalon.TabIndex = 4;
            this.cmbSalon.SelectedIndexChanged += new System.EventHandler(this.cmbSalon_SelectedIndexChanged);
            // 
            // cmbSeans
            // 
            this.cmbSeans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeans.Location = new System.Drawing.Point(150, 110);
            this.cmbSeans.Name = "cmbSeans";
            this.cmbSeans.Size = new System.Drawing.Size(200, 24);
            this.cmbSeans.TabIndex = 5;
            // 
            // dtpTarih
            // 
            this.dtpTarih.Location = new System.Drawing.Point(150, 150);
            this.dtpTarih.Name = "dtpTarih";
            this.dtpTarih.Size = new System.Drawing.Size(200, 22);
            this.dtpTarih.TabIndex = 6;
            // 
            // btnSatis
            // 
            this.btnSatis.ForeColor = System.Drawing.Color.LawnGreen;
            this.btnSatis.Location = new System.Drawing.Point(341, 385);
            this.btnSatis.Name = "btnSatis";
            this.btnSatis.Size = new System.Drawing.Size(200, 54);
            this.btnSatis.TabIndex = 7;
            this.btnSatis.Text = "Bilet Sat";
            this.btnSatis.Click += new System.EventHandler(this.btnSatis_Click);
            // 
            // lblFilm
            // 
            this.lblFilm.Location = new System.Drawing.Point(0, 0);
            this.lblFilm.Name = "lblFilm";
            this.lblFilm.Size = new System.Drawing.Size(100, 23);
            this.lblFilm.TabIndex = 0;
            // 
            // lblSalon
            // 
            this.lblSalon.Location = new System.Drawing.Point(0, 0);
            this.lblSalon.Name = "lblSalon";
            this.lblSalon.Size = new System.Drawing.Size(100, 23);
            this.lblSalon.TabIndex = 0;
            // 
            // lblSeans
            // 
            this.lblSeans.Location = new System.Drawing.Point(0, 0);
            this.lblSeans.Name = "lblSeans";
            this.lblSeans.Size = new System.Drawing.Size(100, 23);
            this.lblSeans.TabIndex = 0;
            // 
            // lblTarih
            // 
            this.lblTarih.Location = new System.Drawing.Point(0, 0);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(100, 23);
            this.lblTarih.TabIndex = 0;
            // 
            // lblSecilenKoltuklar
            // 
            this.lblSecilenKoltuklar.ForeColor = System.Drawing.Color.White;
            this.lblSecilenKoltuklar.Location = new System.Drawing.Point(137, 310);
            this.lblSecilenKoltuklar.Name = "lblSecilenKoltuklar";
            this.lblSecilenKoltuklar.Size = new System.Drawing.Size(100, 23);
            this.lblSecilenKoltuklar.TabIndex = 1;
            this.lblSecilenKoltuklar.Click += new System.EventHandler(this.lblSecilenKoltuklar_Click);
            // 
            // btnKoltukSeç
            // 
            this.btnKoltukSeç.BackgroundImage = global::Anasayfa1.Properties.Resources.koltuk;
            this.btnKoltukSeç.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnKoltukSeç.Location = new System.Drawing.Point(28, 284);
            this.btnKoltukSeç.Name = "btnKoltukSeç";
            this.btnKoltukSeç.Size = new System.Drawing.Size(72, 69);
            this.btnKoltukSeç.TabIndex = 2;
            this.btnKoltukSeç.Click += new System.EventHandler(this.btnKoltukSeç_Click);
            // 
            // lblToplamTutar
            // 
            this.lblToplamTutar.ForeColor = System.Drawing.Color.LawnGreen;
            this.lblToplamTutar.Location = new System.Drawing.Point(39, 393);
            this.lblToplamTutar.Name = "lblToplamTutar";
            this.lblToplamTutar.Size = new System.Drawing.Size(180, 46);
            this.lblToplamTutar.TabIndex = 0;
            this.lblToplamTutar.Text = "Toplam: 0 TL";
            // 
            // nudTam
            // 
            this.nudTam.Location = new System.Drawing.Point(150, 190);
            this.nudTam.Name = "nudTam";
            this.nudTam.Size = new System.Drawing.Size(60, 22);
            this.nudTam.TabIndex = 8;
            this.nudTam.ValueChanged += new System.EventHandler(this.HesaplaToplamTutar);
            // 
            // nudOgrenci
            // 
            this.nudOgrenci.Location = new System.Drawing.Point(150, 230);
            this.nudOgrenci.Name = "nudOgrenci";
            this.nudOgrenci.Size = new System.Drawing.Size(60, 22);
            this.nudOgrenci.TabIndex = 9;
            this.nudOgrenci.ValueChanged += new System.EventHandler(this.HesaplaToplamTutar);
            // 
            // lblTam
            // 
            this.lblTam.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTam.ForeColor = System.Drawing.Color.LawnGreen;
            this.lblTam.Location = new System.Drawing.Point(25, 190);
            this.lblTam.Name = "lblTam";
            this.lblTam.Size = new System.Drawing.Size(100, 23);
            this.lblTam.TabIndex = 10;
            this.lblTam.Text = "Tam Bilet:";
            // 
            // lblOgrenci
            // 
            this.lblOgrenci.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOgrenci.ForeColor = System.Drawing.Color.LawnGreen;
            this.lblOgrenci.Location = new System.Drawing.Point(25, 230);
            this.lblOgrenci.Name = "lblOgrenci";
            this.lblOgrenci.Size = new System.Drawing.Size(100, 23);
            this.lblOgrenci.TabIndex = 11;
            this.lblOgrenci.Text = "Öğrenci:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.LawnGreen;
            this.label1.Location = new System.Drawing.Point(30, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Filmler : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.LawnGreen;
            this.label2.Location = new System.Drawing.Point(30, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "Salon : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.LawnGreen;
            this.label3.Location = new System.Drawing.Point(30, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 23);
            this.label3.TabIndex = 14;
            this.label3.Text = "Seans : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.LawnGreen;
            this.label4.Location = new System.Drawing.Point(30, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 23);
            this.label4.TabIndex = 15;
            this.label4.Text = "Tarih : ";
            // 
            // FrmBiletSatis
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(578, 467);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblToplamTutar);
            this.Controls.Add(this.lblSecilenKoltuklar);
            this.Controls.Add(this.btnKoltukSeç);
            this.Controls.Add(this.cmbFilm);
            this.Controls.Add(this.cmbSalon);
            this.Controls.Add(this.cmbSeans);
            this.Controls.Add(this.dtpTarih);
            this.Controls.Add(this.btnSatis);
            this.Controls.Add(this.nudTam);
            this.Controls.Add(this.nudOgrenci);
            this.Controls.Add(this.lblTam);
            this.Controls.Add(this.lblOgrenci);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBiletSatis";
            this.Text = "Bilet Satış";
            this.Load += new System.EventHandler(this.BiletSatisFormu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOgrenci)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ComboBox cmbFilm;
        private System.Windows.Forms.ComboBox cmbSalon;
        private System.Windows.Forms.ComboBox cmbSeans;
        private System.Windows.Forms.DateTimePicker dtpTarih;
        private System.Windows.Forms.Button btnSatis;
        private System.Windows.Forms.Label lblFilm;
        private System.Windows.Forms.Label lblSalon;
        private System.Windows.Forms.Label lblSeans;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.Button btnKoltukSeç;
        private System.Windows.Forms.Label lblSecilenKoltuklar;
        private System.Windows.Forms.Label lblToplamTutar;
        private System.Windows.Forms.NumericUpDown nudTam;
        private System.Windows.Forms.NumericUpDown nudOgrenci;
        private System.Windows.Forms.Label lblTam;
        private System.Windows.Forms.Label lblOgrenci;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}