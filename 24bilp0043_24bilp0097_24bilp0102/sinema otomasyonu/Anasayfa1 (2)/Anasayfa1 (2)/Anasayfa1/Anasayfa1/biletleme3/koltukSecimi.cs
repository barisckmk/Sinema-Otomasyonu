using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace Anasayfa1
{
    public partial class koltukSecimi : Form
    {        
        private decimal _toplamUcret;

        private string _biletDetay;

        private string _bufeDetay;

        private string _salonAdi;
      
        // ALT
        private int _seansId;

        private int _salonId;

        private int _maxKoltuk;
        
        private int _filmId;

        // App.config içindeki cnn connection string
        private string cnnStr = @"Data Source=.\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True";

        // Seçilen koltukları tutacağımız liste
        private List<KoltukInfo> secilenKoltuklar = new List<KoltukInfo>();

        // Veritabanından gelen koltuk bilgisi için sınıf
        private class KoltukInfo
        {
            public int KoltukID { get; set; }
            public string SatirHarf { get; set; } // A, B, C...
            public int KoltukNo { get; set; }     // 1,2,3...
            public byte Durum { get; set; }       // 0: boş, 1: dolu, 2: engelli

            
        }


        private Color BtnAktifBack => Color.FromArgb(154, 205, 50);
        private Color BtnPasifBack => Color.DarkRed;

        public koltukSecimi(int filmId, int salonId, string salonAdi, int seansId, int maxKoltuk, decimal toplamUcret, string biletDetay, string bufeDetay)
        {
            InitializeComponent();
            _filmId = filmId; // <-- 2. BURAYI EKLE
            _salonId = salonId;
            _salonAdi = salonAdi;
            _seansId = seansId;
            _maxKoltuk = maxKoltuk;
            _toplamUcret = toplamUcret;
            _biletDetay = biletDetay;
            _bufeDetay = bufeDetay;
        }
        

        private void pnlKoltukAlan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void koltukSecimi_Load(object sender, EventArgs e)
        {
            KoltuklariYukle();

            btnDevamEt.Enabled = false;
            btnDevamEt.BackColor = BtnPasifBack;

            DevamEtButonKontrol();

            pbClose.Image = Properties.Resources.cikis;
            pbBack.Image = Properties.Resources.geri;

        }

        private void KoltuklariYukle()
        {

            tblKoltuklar.Controls.Clear();
            tblKoltuklar.ColumnStyles.Clear();
            secilenKoltuklar.Clear();

            // Tablo sütun yapısı
            tblKoltuklar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
            for (int i = 0; i < 12; i++) tblKoltuklar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 46));
            tblKoltuklar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));

            // EN MANTIKLI SORGULAMA: SeansKoltuklari üzerinden kontrol
            string sql = @"
    SELECT k.KoltukID, k.SatirHarf, k.KoltukNo, ISNULL(sk.Durum, 0)
    FROM Koltuklar k
    LEFT JOIN SeansKoltuklari sk ON k.KoltukID = sk.KoltukID AND sk.SeansID = @seansId
    WHERE k.SalonID = @salonId
    ORDER BY k.SatirHarf, k.KoltukNo";

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@salonId", _salonId);
                cmd.Parameters.AddWithValue("@seansId", _seansId);
                cnn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    string currentRow = null;
                    int currentRowIndex = -1;

                    while (dr.Read())
                    {
                        KoltukInfo info = new KoltukInfo
                        {
                            KoltukID = dr.GetInt32(0),
                            SatirHarf = dr.GetString(1),
                            KoltukNo = dr.GetInt32(2),
                            Durum = (byte)Convert.ToInt32(dr[3]) // Durum buradan geliyor
                        };

                        if (currentRow != info.SatirHarf)
                        {
                            currentRow = info.SatirHarf;
                            currentRowIndex++;
                            tblKoltuklar.Controls.Add(CreateRowLabel(info.SatirHarf), 0, currentRowIndex);
                            tblKoltuklar.Controls.Add(CreateRowLabel(info.SatirHarf), 13, currentRowIndex);
                        }

                        Button btnSeat = CreateSeatButton(info);
                        // KoltukNo'ya göre sütuna yerleştir
                        tblKoltuklar.Controls.Add(btnSeat, info.KoltukNo, currentRowIndex);
                    }
                }
            }
        }

        private Label CreateRowLabel(string harf) 
       {
          
            Label lbl = new Label();
            lbl.Text = harf;
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.ForeColor = Color.Gray;
            lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            return lbl;

        }

        private Button CreateSeatButton(KoltukInfo info)
        {
            Button btn = new Button();
            btn.Width = 40;
            btn.Height = 32;
            btn.Margin = new Padding(3);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Text = info.KoltukNo.ToString();
            btn.Font = new Font("Segoe UI", 8, FontStyle.Bold);

            // Koltuk bilgilerini butonda sakla
            btn.Tag = info;

            // Rengini durumuna göre ayarla
            SetSeatColor(btn, info);

            // Tıklanma olayını bağla
            btn.Click += Seat_Click;
            btn.MouseEnter += (s, e) =>
            {
                if (btn.Enabled && secilenKoltuklar.Count < _maxKoltuk)
                    btn.Cursor = Cursors.Hand;
                else
                    btn.Cursor = Cursors.Default;
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.Cursor = Cursors.Default;
            };
          
            return btn;
        }

        private Color RenkBos => Color.FromArgb(145, 176, 52);    // boş koltuk
        private Color RenkDolu => Color.Gainsboro;// dolu
        private Color RenkEngel => Color.FromArgb(140,180,220);    // engelli
        private Color RenkSecim => Color.FromArgb(239, 158, 32);    // seçili (turuncu)

        private void SetSeatColor(Button btn, KoltukInfo info)
        {
            switch (info.Durum)
            {
                case 0: // Boş
                    btn.BackColor = RenkBos;
                    btn.Enabled = true;
                    break;
                case 1: // Dolu
                    btn.BackColor = RenkDolu;
                    btn.Enabled = false; // tıklanmasın
                    break;
                case 2: // Engelli
                    btn.BackColor = RenkEngel;
                    btn.Enabled = true; // istersen false yaparsın
                    break;
            }
        }

        private void Seat_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            KoltukInfo info = (KoltukInfo)btn.Tag;

            // Dolu koltuk zaten tıklanamaz ama kontrol edelim
            if (info.Durum == 1)
                return;

            bool seciliMi = btn.BackColor == RenkSecim;


            if (!seciliMi) // yeni seçim yapılacaksa
            {
                if (secilenKoltuklar.Count >= _maxKoltuk)
                {
                    MessageBox.Show($"En fazla {_maxKoltuk} koltuk seçebilirsiniz.");
                    return;
                }
            }



            if (!seciliMi)
            {
                // Seç
                btn.BackColor = RenkSecim;
                if (!secilenKoltuklar.Any(k => k.KoltukID == info.KoltukID))
                    secilenKoltuklar.Add(info);
            }
            else
            {
                // Seçimi kaldır
                secilenKoltuklar.RemoveAll(k => k.KoltukID == info.KoltukID);
                SetSeatColor(btn, info); // eski rengine dön
            }

           

            // Seçim durumu değiştiği için cursor davranışı da değişebilir
            foreach (Control c in tblKoltuklar.Controls)
            {
                if (c is Button b)
                    b.Cursor = Cursors.Default;
            }

            DevamEtButonKontrol();

        }



        private void btnDevamEt_Click(object sender, EventArgs e)
        {
            // Seçilen koltukların harf ve numara listesi (Görsel için)
            string koltukListesi = string.Join(", ", secilenKoltuklar.Select(k => k.SatirHarf + k.KoltukNo));
            string salonAdi = SalonAdiniGetir();

            KoltukOnayFormu onayFrm = new KoltukOnayFormu(salonAdi, koltukListesi);

            if (onayFrm.ShowDialog() == DialogResult.OK)
            {
                // Veritabanı için sadece ID listesi
                List<int> secilenIdler = secilenKoltuklar.Select(k => k.KoltukID).ToList();

                // 🔥 DÜZELTME: Burası 'odeme' formuna gitmeli (Odeme2 değil)
                odeme frm = new odeme(
                    _filmId,
                    _salonId,
                    _seansId,
                    _maxKoltuk,
                    _toplamUcret,
                    _biletDetay,
                    _bufeDetay,
                    secilenIdler
                );

                frm.Owner = this;
                frm.Show();
                this.Hide();
            }
        }

        public void SadeceSecilenKoltuklariTemizle()
        {
            // Seçilen koltuk listesini temizle
            secilenKoltuklar.Clear();

            // Sadece seçili olan koltukları eski haline döndür
            foreach (Control c in tblKoltuklar.Controls)
            {
                if (c is Button btn && btn.Tag is KoltukInfo info)
                {
                    // Eğer kullanıcı tarafından seçilmişse
                    if (btn.BackColor == RenkSecim)
                    {
                        // DB'deki durumuna göre eski rengine dön
                        SetSeatColor(btn, info);
                    }
                }
            }

            // Devam Et butonu pasif
            btnDevamEt.Enabled = false;
            btnDevamEt.BackColor = BtnPasifBack;
            btnDevamEt.Cursor = Cursors.Default;
        }



        private void pnlUstMenu_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void DevamEtButonKontrol()
        {
            if (secilenKoltuklar.Count == _maxKoltuk)
            {
                btnDevamEt.Enabled = true;
                btnDevamEt.BackColor = BtnAktifBack;
                btnDevamEt.ForeColor = Color.White;
                btnDevamEt.Cursor = Cursors.Hand;   // 👈 ÖNEMLİ SATIR
            }
            else
            {
                btnDevamEt.Enabled = false;
                btnDevamEt.BackColor = BtnPasifBack;
                btnDevamEt.ForeColor = Color.White;
                btnDevamEt.Cursor = Cursors.Default; // 👈 ÖNEMLİ SATIR
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            var sonuc = MessageBox.Show(
        "Uygulamadan çıkmak istiyor musunuz?",
        "Çıkış",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show(); // ✔ biletAlımVeBufe
            }

            this.Close();
        }

        private void lblCircle2_Click(object sender, EventArgs e)
        {
            if (this.Owner != null && !this.Owner.IsDisposed)
            {
                this.Owner.Show();   // ✅ biletAlımVeBufe geri gelir
            }
            this.Close();            // ✅ koltukSecimi kapanır
        }

        private void lblStepText2_Click(object sender, EventArgs e)
        {
            if (this.Owner != null && !this.Owner.IsDisposed)
            {
                this.Owner.Show();   // ✅ biletAlımVeBufe geri gelir
            }
            this.Close();            // ✅ koltukSecimi kapanır
        }

        private void lblStepText1_Click(object sender, EventArgs e)
        {
            if (this.Owner != null && this.Owner.Owner != null)
            {
                this.Owner.Owner.Show();   // 🔥 Form1
            }

            this.Owner?.Close();  // biletAlımVeBufe kapanır
            this.Close();         // koltukSecimi kapanır
        }

        private void lblCircle1_Click(object sender, EventArgs e)
        {
            if (this.Owner != null && this.Owner.Owner != null)
            {
                this.Owner.Owner.Show();   // 🔥 Form1
            }

            this.Owner?.Close();  // biletAlımVeBufe kapanır
            this.Close();         // koltukSecimi kapanır
        }
        private string SalonAdiniGetir()
        {
            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT SalonAdi FROM Salonlar WHERE SalonID = @id",
                    cnn
                );
                cmd.Parameters.AddWithValue("@id", _salonId);

                return cmd.ExecuteScalar()?.ToString() ?? "Salon";
            }
        }



    }
}
