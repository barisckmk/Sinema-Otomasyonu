namespace Anasayfa1
{
    partial class FrmBufe
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelUrun;
        private System.Windows.Forms.Panel panelSepet;
        private System.Windows.Forms.ComboBox cmbUrun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudAdet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFiyat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.DataGridView dgvSepet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUrun;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTutar;
        private System.Windows.Forms.Label lblToplam;
        private System.Windows.Forms.Button btnSatisYap;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelUrun = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbUrun = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudAdet = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFiyat = new System.Windows.Forms.Label();
            this.btnEkle = new System.Windows.Forms.Button();
            this.panelSepet = new System.Windows.Forms.Panel();
            this.dgvSepet = new System.Windows.Forms.DataGridView();
            this.colUrun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTutar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblToplam = new System.Windows.Forms.Label();
            this.btnSatisYap = new System.Windows.Forms.Button();
            this.panelUrun.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdet)).BeginInit();
            this.panelSepet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSepet)).BeginInit();
            this.SuspendLayout();
            // 
            // panelUrun
            // 
            this.panelUrun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelUrun.Controls.Add(this.button1);
            this.panelUrun.Controls.Add(this.label1);
            this.panelUrun.Controls.Add(this.cmbUrun);
            this.panelUrun.Controls.Add(this.label2);
            this.panelUrun.Controls.Add(this.nudAdet);
            this.panelUrun.Controls.Add(this.label3);
            this.panelUrun.Controls.Add(this.lblFiyat);
            this.panelUrun.Controls.Add(this.btnEkle);
            this.panelUrun.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelUrun.ForeColor = System.Drawing.Color.White;
            this.panelUrun.Location = new System.Drawing.Point(0, 0);
            this.panelUrun.Name = "panelUrun";
            this.panelUrun.Size = new System.Drawing.Size(280, 522);
            this.panelUrun.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LawnGreen;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(20, 321);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(230, 40);
            this.button1.TabIndex = 7;
            this.button1.Text = "Sepetten Sil";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LawnGreen;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ürün Seç:";
            // 
            // cmbUrun
            // 
            this.cmbUrun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUrun.Location = new System.Drawing.Point(20, 55);
            this.cmbUrun.Name = "cmbUrun";
            this.cmbUrun.Size = new System.Drawing.Size(230, 21);
            this.cmbUrun.TabIndex = 1;
            this.cmbUrun.SelectedIndexChanged += new System.EventHandler(this.cmbUrun_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LawnGreen;
            this.label2.Location = new System.Drawing.Point(20, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Adet:";
            // 
            // nudAdet
            // 
            this.nudAdet.Location = new System.Drawing.Point(20, 135);
            this.nudAdet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAdet.Name = "nudAdet";
            this.nudAdet.Size = new System.Drawing.Size(80, 20);
            this.nudAdet.TabIndex = 3;
            this.nudAdet.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAdet.ValueChanged += new System.EventHandler(this.nudAdet_ValueChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(20, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fiyat:";
            // 
            // lblFiyat
            // 
            this.lblFiyat.AutoSize = true;
            this.lblFiyat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFiyat.ForeColor = System.Drawing.Color.White;
            this.lblFiyat.Location = new System.Drawing.Point(20, 210);
            this.lblFiyat.Name = "lblFiyat";
            this.lblFiyat.Size = new System.Drawing.Size(36, 19);
            this.lblFiyat.TabIndex = 5;
            this.lblFiyat.Text = "0 TL";
            this.lblFiyat.Click += new System.EventHandler(this.lblFiyat_Click);
            // 
            // btnEkle
            // 
            this.btnEkle.BackColor = System.Drawing.Color.LawnGreen;
            this.btnEkle.FlatAppearance.BorderSize = 0;
            this.btnEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEkle.ForeColor = System.Drawing.Color.Black;
            this.btnEkle.Location = new System.Drawing.Point(20, 260);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(230, 40);
            this.btnEkle.TabIndex = 6;
            this.btnEkle.Text = "Sepete Ekle";
            this.btnEkle.UseVisualStyleBackColor = false;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // panelSepet
            // 
            this.panelSepet.Controls.Add(this.dgvSepet);
            this.panelSepet.Controls.Add(this.lblToplam);
            this.panelSepet.Controls.Add(this.btnSatisYap);
            this.panelSepet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSepet.Location = new System.Drawing.Point(280, 0);
            this.panelSepet.Name = "panelSepet";
            this.panelSepet.Size = new System.Drawing.Size(648, 522);
            this.panelSepet.TabIndex = 1;
            // 
            // dgvSepet
            // 
            this.dgvSepet.AllowUserToAddRows = false;
            this.dgvSepet.ColumnHeadersHeight = 29;
            this.dgvSepet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUrun,
            this.colAdet,
            this.colTutar});
            this.dgvSepet.Location = new System.Drawing.Point(20, 20);
            this.dgvSepet.Name = "dgvSepet";
            this.dgvSepet.RowHeadersWidth = 51;
            this.dgvSepet.Size = new System.Drawing.Size(600, 330);
            this.dgvSepet.TabIndex = 0;
            // 
            // colUrun
            // 
            this.colUrun.HeaderText = "Ürün";
            this.colUrun.MinimumWidth = 6;
            this.colUrun.Name = "colUrun";
            this.colUrun.Width = 250;
            // 
            // colAdet
            // 
            this.colAdet.HeaderText = "Adet";
            this.colAdet.MinimumWidth = 6;
            this.colAdet.Name = "colAdet";
            this.colAdet.Width = 80;
            // 
            // colTutar
            // 
            this.colTutar.HeaderText = "Tutar (TL)";
            this.colTutar.MinimumWidth = 6;
            this.colTutar.Name = "colTutar";
            this.colTutar.Width = 120;
            // 
            // lblToplam
            // 
            this.lblToplam.AutoSize = true;
            this.lblToplam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblToplam.ForeColor = System.Drawing.Color.White;
            this.lblToplam.Location = new System.Drawing.Point(20, 360);
            this.lblToplam.Name = "lblToplam";
            this.lblToplam.Size = new System.Drawing.Size(94, 19);
            this.lblToplam.TabIndex = 1;
            this.lblToplam.Text = "Toplam: 0 TL";
            // 
            // btnSatisYap
            // 
            this.btnSatisYap.BackColor = System.Drawing.Color.LawnGreen;
            this.btnSatisYap.FlatAppearance.BorderSize = 0;
            this.btnSatisYap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSatisYap.ForeColor = System.Drawing.Color.Black;
            this.btnSatisYap.Location = new System.Drawing.Point(20, 400);
            this.btnSatisYap.Name = "btnSatisYap";
            this.btnSatisYap.Size = new System.Drawing.Size(600, 45);
            this.btnSatisYap.TabIndex = 2;
            this.btnSatisYap.Text = "Satışı Tamamla";
            this.btnSatisYap.UseVisualStyleBackColor = false;
            this.btnSatisYap.Click += new System.EventHandler(this.btnSatisYap_Click);
            // 
            // FrmBufe
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(928, 522);
            this.Controls.Add(this.panelSepet);
            this.Controls.Add(this.panelUrun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBufe";
            this.Text = "Büfe Satış Paneli";
            this.Load += new System.EventHandler(this.FrmBufe_Load);
            this.panelUrun.ResumeLayout(false);
            this.panelUrun.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdet)).EndInit();
            this.panelSepet.ResumeLayout(false);
            this.panelSepet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSepet)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button button1;
    }
}
