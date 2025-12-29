namespace Anasayfa1
{
    partial class FrmYönetim
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
            this.btnFilmSil = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFilmKaydet = new System.Windows.Forms.Button();
            this.txtSure = new System.Windows.Forms.TextBox();
            this.txtTur = new System.Windows.Forms.TextBox();
            this.txtFilmAdi = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSalonSil = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSalonEkle = new System.Windows.Forms.Button();
            this.txtSalonAdi = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTarihSil = new System.Windows.Forms.Button();
            this.btnSaatSil = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTarihEkle = new System.Windows.Forms.Button();
            this.dtpTarih = new System.Windows.Forms.DateTimePicker();
            this.btnSaatEkle = new System.Windows.Forms.Button();
            this.txtSeansSaati = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnListele = new System.Windows.Forms.Button();
            this.btnTemizle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnGüncelle = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSeansOlustur = new System.Windows.Forms.Button();
            this.cmbTarih = new System.Windows.Forms.ComboBox();
            this.cmbSeans = new System.Windows.Forms.ComboBox();
            this.cmbSalon = new System.Windows.Forms.ComboBox();
            this.cmbFilm = new System.Windows.Forms.ComboBox();
            this.dgvSeanslar = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeanslar)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFilmSil);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnFilmKaydet);
            this.groupBox1.Controls.Add(this.txtSure);
            this.groupBox1.Controls.Add(this.txtTur);
            this.groupBox1.Controls.Add(this.txtFilmAdi);
            this.groupBox1.ForeColor = System.Drawing.Color.LawnGreen;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(333, 246);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Film Ekleme Paneli";
            // 
            // btnFilmSil
            // 
            this.btnFilmSil.ForeColor = System.Drawing.Color.Black;
            this.btnFilmSil.Location = new System.Drawing.Point(189, 177);
            this.btnFilmSil.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFilmSil.Name = "btnFilmSil";
            this.btnFilmSil.Size = new System.Drawing.Size(120, 37);
            this.btnFilmSil.TabIndex = 9;
            this.btnFilmSil.Text = "Film Sil";
            this.btnFilmSil.UseVisualStyleBackColor = true;
            this.btnFilmSil.Click += new System.EventHandler(this.btnFilmSil_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 129);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Süre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tür:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Film Adı:";
            // 
            // btnFilmKaydet
            // 
            this.btnFilmKaydet.ForeColor = System.Drawing.Color.Black;
            this.btnFilmKaydet.Location = new System.Drawing.Point(39, 177);
            this.btnFilmKaydet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFilmKaydet.Name = "btnFilmKaydet";
            this.btnFilmKaydet.Size = new System.Drawing.Size(120, 37);
            this.btnFilmKaydet.TabIndex = 4;
            this.btnFilmKaydet.Text = "Film Kaydet";
            this.btnFilmKaydet.UseVisualStyleBackColor = true;
            this.btnFilmKaydet.Click += new System.EventHandler(this.btnFilmKaydet_Click);
            // 
            // txtSure
            // 
            this.txtSure.Location = new System.Drawing.Point(105, 128);
            this.txtSure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSure.Name = "txtSure";
            this.txtSure.Size = new System.Drawing.Size(204, 22);
            this.txtSure.TabIndex = 3;
            // 
            // txtTur
            // 
            this.txtTur.Location = new System.Drawing.Point(105, 84);
            this.txtTur.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTur.Name = "txtTur";
            this.txtTur.Size = new System.Drawing.Size(204, 22);
            this.txtTur.TabIndex = 2;
            // 
            // txtFilmAdi
            // 
            this.txtFilmAdi.Location = new System.Drawing.Point(105, 42);
            this.txtFilmAdi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFilmAdi.Name = "txtFilmAdi";
            this.txtFilmAdi.Size = new System.Drawing.Size(204, 22);
            this.txtFilmAdi.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSalonSil);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnSalonEkle);
            this.groupBox2.Controls.Add(this.txtSalonAdi);
            this.groupBox2.ForeColor = System.Drawing.Color.LawnGreen;
            this.groupBox2.Location = new System.Drawing.Point(345, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(333, 246);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Salon Ekle";
            // 
            // btnSalonSil
            // 
            this.btnSalonSil.ForeColor = System.Drawing.Color.Black;
            this.btnSalonSil.Location = new System.Drawing.Point(176, 71);
            this.btnSalonSil.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalonSil.Name = "btnSalonSil";
            this.btnSalonSil.Size = new System.Drawing.Size(133, 32);
            this.btnSalonSil.TabIndex = 10;
            this.btnSalonSil.Text = "Salon Sil";
            this.btnSalonSil.UseVisualStyleBackColor = true;
            this.btnSalonSil.Click += new System.EventHandler(this.btnSalonSil_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 46);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Salon Adı:";
            // 
            // btnSalonEkle
            // 
            this.btnSalonEkle.ForeColor = System.Drawing.Color.Black;
            this.btnSalonEkle.Location = new System.Drawing.Point(27, 71);
            this.btnSalonEkle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalonEkle.Name = "btnSalonEkle";
            this.btnSalonEkle.Size = new System.Drawing.Size(133, 32);
            this.btnSalonEkle.TabIndex = 5;
            this.btnSalonEkle.Text = "Salon Ekle";
            this.btnSalonEkle.UseVisualStyleBackColor = true;
            this.btnSalonEkle.Click += new System.EventHandler(this.btnSalonEkle_Click);
            // 
            // txtSalonAdi
            // 
            this.txtSalonAdi.Location = new System.Drawing.Point(105, 42);
            this.txtSalonAdi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSalonAdi.Name = "txtSalonAdi";
            this.txtSalonAdi.Size = new System.Drawing.Size(204, 22);
            this.txtSalonAdi.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnTarihSil);
            this.groupBox3.Controls.Add(this.btnSaatSil);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnTarihEkle);
            this.groupBox3.Controls.Add(this.dtpTarih);
            this.groupBox3.Controls.Add(this.btnSaatEkle);
            this.groupBox3.Controls.Add(this.txtSeansSaati);
            this.groupBox3.ForeColor = System.Drawing.Color.LawnGreen;
            this.groupBox3.Location = new System.Drawing.Point(686, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(320, 246);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tanımlamalar (Saat/Tarih)";
            // 
            // btnTarihSil
            // 
            this.btnTarihSil.ForeColor = System.Drawing.Color.Black;
            this.btnTarihSil.Location = new System.Drawing.Point(171, 159);
            this.btnTarihSil.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTarihSil.Name = "btnTarihSil";
            this.btnTarihSil.Size = new System.Drawing.Size(123, 32);
            this.btnTarihSil.TabIndex = 12;
            this.btnTarihSil.Text = "Tarih Sil";
            this.btnTarihSil.UseVisualStyleBackColor = true;
            this.btnTarihSil.Click += new System.EventHandler(this.btnTarihSil_Click);
            // 
            // btnSaatSil
            // 
            this.btnSaatSil.ForeColor = System.Drawing.Color.Black;
            this.btnSaatSil.Location = new System.Drawing.Point(171, 74);
            this.btnSaatSil.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaatSil.Name = "btnSaatSil";
            this.btnSaatSil.Size = new System.Drawing.Size(123, 32);
            this.btnSaatSil.TabIndex = 11;
            this.btnSaatSil.Text = "Saat Sil";
            this.btnSaatSil.UseVisualStyleBackColor = true;
            this.btnSaatSil.Click += new System.EventHandler(this.btnSaatSil_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 134);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Tarih:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 46);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Saat:";
            // 
            // btnTarihEkle
            // 
            this.btnTarihEkle.ForeColor = System.Drawing.Color.Black;
            this.btnTarihEkle.Location = new System.Drawing.Point(28, 159);
            this.btnTarihEkle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTarihEkle.Name = "btnTarihEkle";
            this.btnTarihEkle.Size = new System.Drawing.Size(123, 32);
            this.btnTarihEkle.TabIndex = 7;
            this.btnTarihEkle.Text = "Tarih Ekle";
            this.btnTarihEkle.UseVisualStyleBackColor = true;
            this.btnTarihEkle.Click += new System.EventHandler(this.btnTarihEkle_Click);
            // 
            // dtpTarih
            // 
            this.dtpTarih.Location = new System.Drawing.Point(76, 129);
            this.dtpTarih.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpTarih.Name = "dtpTarih";
            this.dtpTarih.Size = new System.Drawing.Size(216, 22);
            this.dtpTarih.TabIndex = 6;
            // 
            // btnSaatEkle
            // 
            this.btnSaatEkle.ForeColor = System.Drawing.Color.Black;
            this.btnSaatEkle.Location = new System.Drawing.Point(28, 71);
            this.btnSaatEkle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaatEkle.Name = "btnSaatEkle";
            this.btnSaatEkle.Size = new System.Drawing.Size(123, 32);
            this.btnSaatEkle.TabIndex = 5;
            this.btnSaatEkle.Text = "Saat Ekle";
            this.btnSaatEkle.UseVisualStyleBackColor = true;
            this.btnSaatEkle.Click += new System.EventHandler(this.btnSaatEkle_Click);
            // 
            // txtSeansSaati
            // 
            this.txtSeansSaati.Location = new System.Drawing.Point(76, 42);
            this.txtSeansSaati.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSeansSaati.Name = "txtSeansSaati";
            this.txtSeansSaati.Size = new System.Drawing.Size(216, 22);
            this.txtSeansSaati.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnListele);
            this.groupBox4.Controls.Add(this.btnTemizle);
            this.groupBox4.Controls.Add(this.btnSil);
            this.groupBox4.Controls.Add(this.btnGüncelle);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.btnSeansOlustur);
            this.groupBox4.Controls.Add(this.cmbTarih);
            this.groupBox4.Controls.Add(this.cmbSeans);
            this.groupBox4.Controls.Add(this.cmbSalon);
            this.groupBox4.Controls.Add(this.cmbFilm);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox4.ForeColor = System.Drawing.Color.LawnGreen;
            this.groupBox4.Location = new System.Drawing.Point(4, 258);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(1032, 148);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ANA İŞLEM: SEANS OLUŞTURMA";
            // 
            // btnListele
            // 
            this.btnListele.ForeColor = System.Drawing.Color.Black;
            this.btnListele.Location = new System.Drawing.Point(559, 108);
            this.btnListele.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnListele.Name = "btnListele";
            this.btnListele.Size = new System.Drawing.Size(107, 33);
            this.btnListele.TabIndex = 18;
            this.btnListele.Text = "Listele";
            this.btnListele.UseVisualStyleBackColor = true;
            this.btnListele.Click += new System.EventHandler(this.btnListele_Click);
            // 
            // btnTemizle
            // 
            this.btnTemizle.ForeColor = System.Drawing.Color.Black;
            this.btnTemizle.Location = new System.Drawing.Point(421, 108);
            this.btnTemizle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(107, 33);
            this.btnTemizle.TabIndex = 17;
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.UseVisualStyleBackColor = true;
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);
            // 
            // btnSil
            // 
            this.btnSil.ForeColor = System.Drawing.Color.Black;
            this.btnSil.Location = new System.Drawing.Point(277, 108);
            this.btnSil.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(107, 33);
            this.btnSil.TabIndex = 16;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnGüncelle
            // 
            this.btnGüncelle.ForeColor = System.Drawing.Color.Black;
            this.btnGüncelle.Location = new System.Drawing.Point(131, 108);
            this.btnGüncelle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGüncelle.Name = "btnGüncelle";
            this.btnGüncelle.Size = new System.Drawing.Size(107, 33);
            this.btnGüncelle.TabIndex = 15;
            this.btnGüncelle.Text = "Güncelle";
            this.btnGüncelle.UseVisualStyleBackColor = true;
            this.btnGüncelle.Click += new System.EventHandler(this.btnGüncelle_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(631, 36);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 18);
            this.label11.TabIndex = 14;
            this.label11.Text = "Tarih Seç :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(431, 36);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 18);
            this.label10.TabIndex = 13;
            this.label10.Text = "Saat Seç :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(231, 36);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 18);
            this.label9.TabIndex = 12;
            this.label9.Text = "Salon Seç :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 36);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 18);
            this.label8.TabIndex = 9;
            this.label8.Text = "Film Seç :";
            // 
            // btnSeansOlustur
            // 
            this.btnSeansOlustur.BackColor = System.Drawing.Color.LawnGreen;
            this.btnSeansOlustur.FlatAppearance.BorderSize = 0;
            this.btnSeansOlustur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeansOlustur.ForeColor = System.Drawing.Color.White;
            this.btnSeansOlustur.Location = new System.Drawing.Point(831, 54);
            this.btnSeansOlustur.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSeansOlustur.Name = "btnSeansOlustur";
            this.btnSeansOlustur.Size = new System.Drawing.Size(177, 48);
            this.btnSeansOlustur.TabIndex = 11;
            this.btnSeansOlustur.Text = "SEANSI OLUŞTUR";
            this.btnSeansOlustur.UseVisualStyleBackColor = false;
            this.btnSeansOlustur.Click += new System.EventHandler(this.btnSeansOlustur_Click);
            // 
            // cmbTarih
            // 
            this.cmbTarih.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarih.FormattingEnabled = true;
            this.cmbTarih.Location = new System.Drawing.Point(635, 65);
            this.cmbTarih.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTarih.Name = "cmbTarih";
            this.cmbTarih.Size = new System.Drawing.Size(160, 26);
            this.cmbTarih.TabIndex = 10;
            this.cmbTarih.SelectedIndexChanged += new System.EventHandler(this.cmbTarih_SelectedIndexChanged);
            // 
            // cmbSeans
            // 
            this.cmbSeans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeans.FormattingEnabled = true;
            this.cmbSeans.Location = new System.Drawing.Point(435, 65);
            this.cmbSeans.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSeans.Name = "cmbSeans";
            this.cmbSeans.Size = new System.Drawing.Size(160, 26);
            this.cmbSeans.TabIndex = 9;
            this.cmbSeans.SelectedIndexChanged += new System.EventHandler(this.cmbSeans_SelectedIndexChanged);
            // 
            // cmbSalon
            // 
            this.cmbSalon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalon.FormattingEnabled = true;
            this.cmbSalon.Location = new System.Drawing.Point(235, 65);
            this.cmbSalon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSalon.Name = "cmbSalon";
            this.cmbSalon.Size = new System.Drawing.Size(160, 26);
            this.cmbSalon.TabIndex = 8;
            this.cmbSalon.SelectedIndexChanged += new System.EventHandler(this.cmbSalon_SelectedIndexChanged);
            // 
            // cmbFilm
            // 
            this.cmbFilm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilm.FormattingEnabled = true;
            this.cmbFilm.Location = new System.Drawing.Point(24, 65);
            this.cmbFilm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFilm.Name = "cmbFilm";
            this.cmbFilm.Size = new System.Drawing.Size(169, 26);
            this.cmbFilm.TabIndex = 7;
            // 
            // dgvSeanslar
            // 
            this.dgvSeanslar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSeanslar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeanslar.Location = new System.Drawing.Point(4, 414);
            this.dgvSeanslar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvSeanslar.Name = "dgvSeanslar";
            this.dgvSeanslar.RowHeadersWidth = 51;
            this.dgvSeanslar.Size = new System.Drawing.Size(1019, 251);
            this.dgvSeanslar.TabIndex = 4;
            this.dgvSeanslar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSeanslar_CellContentClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.dgvSeanslar);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1381, 703);
            this.flowLayoutPanel1.TabIndex = 5;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // FrmYönetim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 703);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmYönetim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sinema Yönetim Paneli";
            this.Load += new System.EventHandler(this.FrmYonetim_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeanslar)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFilmAdi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFilmKaydet;
        private System.Windows.Forms.TextBox txtSure;
        private System.Windows.Forms.TextBox txtTur;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSalonEkle;
        private System.Windows.Forms.TextBox txtSalonAdi;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnTarihEkle;
        private System.Windows.Forms.DateTimePicker dtpTarih;
        private System.Windows.Forms.Button btnSaatEkle;
        private System.Windows.Forms.TextBox txtSeansSaati;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSeansOlustur;
        private System.Windows.Forms.ComboBox cmbTarih;
        private System.Windows.Forms.ComboBox cmbSeans;
        private System.Windows.Forms.ComboBox cmbSalon;
        private System.Windows.Forms.ComboBox cmbFilm;
        private System.Windows.Forms.DataGridView dgvSeanslar;
        private System.Windows.Forms.Button btnFilmSil;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnGüncelle;
        private System.Windows.Forms.Button btnListele;
        private System.Windows.Forms.Button btnTemizle;
        private System.Windows.Forms.Button btnSalonSil;
        private System.Windows.Forms.Button btnTarihSil;
        private System.Windows.Forms.Button btnSaatSil;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}