    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration; // Veritabanı bağlantısı için
    using System.Data;
    using System.Data.SqlClient; // SQL işlemleri için
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    namespace Anasayfa1
    {
        public partial class odeme : Form
        {
            Timer blinkTimer = new Timer();
            bool blinkState = false;

            int kalanSure = 300; // 5 dakika = 300 saniye
            bool sureDoldu = false;

            int _salonId;
            int _seansId;
            int _biletSayisi;

            decimal _toplamUcret;

            private string _biletDetay;
            private string _bufeDetay;
            private int _filmId;

            // YENİ EKLENEN: Koltuk ID Listesi ve Connection String
            private List<int> _secilenKoltukIdleri;
            private string cnnStr = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

            // Constructor GÜNCELLENDİ: List<int> secilenKoltukIdleri parametresi eklendi
            public odeme(int filmId, int salonId, int seansId, int biletSayisi, decimal toplamUcret,
                 string biletDetay, string bufeDetay, List<int> secilenKoltukIdleri)
            {
                InitializeComponent();
                this._filmId = filmId; // Bu satırı eklediğinden emin ol
                this._salonId = salonId;
                this._seansId = seansId;
                this._biletSayisi = biletSayisi;
                this._toplamUcret = toplamUcret;
                this._biletDetay = biletDetay;
                this._bufeDetay = bufeDetay;
                this._secilenKoltukIdleri = secilenKoltukIdleri;
            }
            public odeme()
            {
                InitializeComponent();
            }

            private void odeme_Load(object sender, EventArgs e)
            {
                blinkTimer.Interval = 500; // yarım saniye
                blinkTimer.Tick += BlinkTimer_Tick;

                lblToplamBaslik.Text = $"Toplam: {_toplamUcret} TL";

                lblBiletDetay.Text = _biletDetay ?? "";
                lblBufeDetay.Text = string.IsNullOrWhiteSpace(_bufeDetay) ? "" : _bufeDetay;

                // Label ayarları
                lblBiletDetay.AutoSize = true;
                lblBufeDetay.AutoSize = true;

                lblBiletDetay.MaximumSize = new Size(pnlOdemeDetay.Width - 20, 0);
                lblBufeDetay.MaximumSize = new Size(pnlOdemeDetay.Width - 20, 0);

                // Konum ayarla
                lblBufeDetay.Top = lblBiletDetay.Bottom + 10;

                // Scroll kontrol
                ScrollKontrolEt();

                // Timer
                SureyiGuncelle();
                timerSure.Start();

                // Placeholder
                PlaceholderAyarla(txtAdSoyad, "Ad Soyad");
                PlaceholderAyarla(txtEmail, "E-posta Adresi");
                PlaceholderAyarla(txtTelefon, "05XXXXXXXXX");

                pbClose.Image = Properties.Resources.cikis;
                pbBack.Image = Properties.Resources.geri;


                // YENİ: Eğer kullanıcı giriş yapmışsa bilgileri veritabanından çek
                if (Oturum.KullaniciID > 0)
                {
                    KullaniciBilgileriniGetir(Oturum.KullaniciID);
                }
                else
                {
                    // Giriş yapılmamışsa placeholder'ları göster
                    PlaceholderAyarla(txtAdSoyad, "Ad Soyad");
                    PlaceholderAyarla(txtEmail, "E-posta Adresi");
                    PlaceholderAyarla(txtTelefon, "05XXXXXXXXX");
                }

            }

            private void KullaniciBilgileriniGetir(int kullaniciId)
            {
                using (SqlConnection cnn = new SqlConnection(cnnStr))
                {
                    string sql = "SELECT ad, soyad, eposta, tel FROM Kayit_Omer WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.Parameters.AddWithValue("@id", kullaniciId);

                    try
                    {
                        cnn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtAdSoyad.Text = dr["ad"].ToString() + " " + dr["soyad"].ToString();
                            txtEmail.Text = dr["eposta"].ToString();
                            txtTelefon.Text = dr["tel"].ToString() == "" ? "05XXXXXXXXX" : dr["tel"].ToString();

                            // MODİFİYE: Yazı renklerini görünür yap (Beyaz veya Siyah)
                            txtAdSoyad.ForeColor = Color.White;
                            txtEmail.ForeColor = Color.White;
                            txtTelefon.ForeColor = Color.White;
                        }
                        dr.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
                }
            }

            private void BlinkTimer_Tick(object sender, EventArgs e)
            {
                blinkState = !blinkState;

                Color c = blinkState
                    ? Color.DarkRed
                    : Color.FromArgb(90, 0, 0);

                pnlSureBar.BackColor = c;
                lblSure.BackColor = c;

                lblSure.ForeColor = Color.White;
            }

            private void PlaceholderAyarla(TextBox txt, string placeholder)
            {
                txt.Text = placeholder;
                txt.ForeColor = Color.Gray;

                txt.GotFocus += (s, e) =>
                {
                    if (txt.Text == placeholder)
                    {
                        txt.Text = "";
                        txt.ForeColor = Color.Black;
                    }
                };

                txt.LostFocus += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txt.Text))
                    {
                        txt.Text = placeholder;
                        txt.ForeColor = Color.Gray;
                    }
                };
            }

            private void ScrollKontrolEt()
            {
                int satirSayisi = 0;

                if (!string.IsNullOrWhiteSpace(lblBiletDetay.Text))
                    satirSayisi += lblBiletDetay.Text.Replace("\r", "").Split('\n').Length;

                if (!string.IsNullOrWhiteSpace(lblBufeDetay.Text))
                    satirSayisi += lblBufeDetay.Text.Replace("\r", "").Split('\n').Length;

                pnlOdemeDetay.AutoScroll = satirSayisi > 4;
            }

            void SureyiGuncelle()
            {
                int dakika = kalanSure / 60;
                int saniye = kalanSure % 60;

                lblSure.Text = $"{dakika:D2}:{saniye:D2} dakika içerisinde biletini almalısın";
            }

            private void timerSure_Tick(object sender, EventArgs e)
            {
                kalanSure--;

                if (kalanSure <= 0)
                {
                    timerSure.Stop();
                    sureDoldu = true;

                    // Süre bitti durumu
                    lblSure.Text = "Süre bitti";
                    lblSure.ForeColor = Color.White;
                    lblSure.BackColor = Color.DarkRed;

                    pnlSureBar.BackColor = Color.DarkRed;

                    // Yanıp sönme başlasın
                    blinkTimer.Start();

                    return;
                }

                SureyiGuncelle();
            }

            private void btnDevamEt_Click(object sender, EventArgs e)
            {
            if (sureDoldu)
            {
                MessageBox.Show("Süre doldu!");
                return;
            }

            if (!chkKosullar.Checked)
            {
                MessageBox.Show("Lütfen kullanım koşullarını kabul edin.");
                return;
            }

            string koltukAdlariString = string.Join(", ", _secilenKoltukIdleri);

            Odeme2 form2 = new Odeme2(
                _toplamUcret,
                _filmId,
                _salonId,
                _seansId,
                koltukAdlariString,
                _secilenKoltukIdleri,
                _biletDetay,
                _bufeDetay
            );

            form2.Owner = this;   // 🔴 MUTLAKA OLACAK
            form2.Show();
            this.Hide();
        }

            private void SatisiVeritabaninaKaydet()
            {
                using (SqlConnection cnn = new SqlConnection(cnnStr))
                {
                    cnn.Open();
                    SqlTransaction trans = cnn.BeginTransaction();
                    try
                    {
                        // 1. ADIM: Satislar tablosuna ana kayıt (Ödeme özeti)
                        string satisSql = @"INSERT INTO Satislar (KullaniciID, Tarih, ToplamTutar, BiletDetay, BufeDetay) 
                                    VALUES (@kId, @tarih, @tutar, @biletDetay, @bufeDetay); 
                                    SELECT SCOPE_IDENTITY();";

                        SqlCommand cmdSatis = new SqlCommand(satisSql, cnn, trans);
                        cmdSatis.Parameters.AddWithValue("@kId", Oturum.KullaniciID);
                        cmdSatis.Parameters.AddWithValue("@tarih", DateTime.Now);
                        cmdSatis.Parameters.AddWithValue("@tutar", _toplamUcret);
                        cmdSatis.Parameters.AddWithValue("@biletDetay", _biletDetay ?? "");
                        cmdSatis.Parameters.AddWithValue("@bufeDetay", _bufeDetay ?? "");

                        int yeniSatisId = Convert.ToInt32(cmdSatis.ExecuteScalar());

                        // 2. ADIM: Muhasebe kaydı
                        string muhasebeSql = @"INSERT INTO TblMuhasebe (Aciklama, Tutar, Tarih, Kategori, OdemeTuru, IslemiYapan, FaturaNo, Durum) 
                                       VALUES (@a, @t, @tar, @k, @o, @i, @f, @d)";
                        SqlCommand cmdMuhasebe = new SqlCommand(muhasebeSql, cnn, trans);
                        cmdMuhasebe.Parameters.AddWithValue("@a", (_biletDetay + " / " + _bufeDetay).Trim());
                        cmdMuhasebe.Parameters.AddWithValue("@t", _toplamUcret);
                        cmdMuhasebe.Parameters.AddWithValue("@tar", DateTime.Now);
                        cmdMuhasebe.Parameters.AddWithValue("@k", "Online Satış");
                        cmdMuhasebe.Parameters.AddWithValue("@o", "Kredi Kartı");
                        cmdMuhasebe.Parameters.AddWithValue("@i", "Sistem / " + (Oturum.AktifKullaniciMail ?? "Müşteri"));
                        cmdMuhasebe.Parameters.AddWithValue("@f", "SN-" + yeniSatisId);
                        cmdMuhasebe.Parameters.AddWithValue("@d", "Ödendi");
                        cmdMuhasebe.ExecuteNonQuery();

                        // 3. ADIM: Koltukları Kilitle ve Biletler Tablosuna Yaz
                        foreach (int koltukId in _secilenKoltukIdleri)
                        {
                            // SeansKoltuklari (Koltukları dolu yapar)
                            string koltukSql = "INSERT INTO SeansKoltuklari (SeansID, KoltukID, Durum) VALUES (@sId, @kId, 1)";
                            SqlCommand cmdKoltuk = new SqlCommand(koltukSql, cnn, trans);
                            cmdKoltuk.Parameters.AddWithValue("@sId", _seansId);
                            cmdKoltuk.Parameters.AddWithValue("@kId", koltukId);
                            cmdKoltuk.ExecuteNonQuery();

                            // Biletler Tablosu (Hata aldığın yer - Bilet kaydını oluşturur)
                            // Not: _filmID bilgisi odeme formuna constructor ile gelmelidir.
                            string biletSql = @"INSERT INTO Biletler (FilmID, SalonID, SeansID, Tarih, KoltukNo, KullaniciID) 
                            VALUES (@fId, @slnId, @seansId, @tarih, @kNo, @uId)";

                            SqlCommand cmdBilet = new SqlCommand(biletSql, cnn, trans);
                            cmdBilet.Parameters.AddWithValue("@fId", _filmId); // Hatanın çözümü burası
                            cmdBilet.Parameters.AddWithValue("@slnId", _salonId);
                            cmdBilet.Parameters.AddWithValue("@seansId", _seansId);
                            cmdBilet.Parameters.AddWithValue("@tarih", DateTime.Now);
                            cmdBilet.Parameters.AddWithValue("@kNo", koltukId.ToString());
                            cmdBilet.Parameters.AddWithValue("@uId", Oturum.KullaniciID);
                            cmdBilet.ExecuteNonQuery();
                        }

                        trans.Commit();

                        // Başarılı! Şimdi Odeme2 formuna geçelim.
                        // Satış ID'sini Odeme2'ye gönderiyoruz ki orada fatura/özet gösterebilesin.
                        Odeme2 form2 = new Odeme2(yeniSatisId);
                        form2.Show();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Veritabanı Hatası: " + ex.Message);
                    }
                }
            }

            private void pnlBody_Paint(object sender, PaintEventArgs e)
            {

            }

            private void pnlOdemeDetay_Paint(object sender, PaintEventArgs e)
            {

            }

            private void lblBufeDetay_Click(object sender, EventArgs e)
            {

            }

            private void chkKosullar_CheckedChanged(object sender, EventArgs e)
            {
                if (chkKosullar.Checked)
                {
                    btnDevamEt.Enabled = true;
                    btnDevamEt.BackColor = Color.FromArgb(154, 205, 50); // yeşil
                    btnDevamEt.ForeColor = Color.White;
                    btnDevamEt.Cursor = Cursors.Hand;
                }
                else
                {
                    btnDevamEt.Enabled = false;
                    btnDevamEt.BackColor = Color.DarkRed;
                    btnDevamEt.ForeColor = Color.White;
                    btnDevamEt.Cursor = Cursors.Hand;
                }
            }

            private void txtAdSoyad_TextChanged(object sender, EventArgs e)
            {

            }

            // --- EKSİK OLAN EVENTLER BURADA ---

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
                    this.Owner.Show(); // biletAlımVeBufe
                }

                this.Close();
            }

            private void lblCircle3_Click(object sender, EventArgs e)
            {
                if (this.Owner != null)
                {
                    this.Owner.Show();
                }

                this.Close();
            }

            private void lblStepText3_Click(object sender, EventArgs e)
            {
                if (this.Owner != null)
                {
                    this.Owner.Show();
                }

                this.Close();
            }

            private void lblStepText1_Click(object sender, EventArgs e)
            {
                if (this.Owner?.Owner?.Owner != null)
                {
                    this.Owner.Owner.Owner.Show();
                }

                this.Owner?.Owner?.Close();
                this.Owner?.Close();
                this.Close();
            }

            private void lblCircle1_Click(object sender, EventArgs e)
            {
                if (this.Owner?.Owner?.Owner != null)
                {
                    this.Owner.Owner.Owner.Show();
                }

                this.Owner?.Owner?.Close();
                this.Owner?.Close();
                this.Close();
            }


            private void pnlCard_Paint(object sender, PaintEventArgs e)
            {

            }


            private void lblStepText2_Click(object sender, EventArgs e)
            {
                if (this.Owner?.Owner != null)
                {
                    this.Owner.Owner.Show();
                }

                this.Hide();
            }

            private void lblCircle2_Click(object sender, EventArgs e)
            {
                if (this.Owner?.Owner != null)
                {
                    this.Owner.Owner.Show();
                }

                this.Hide();
            }

            private void lblSure_Click(object sender, EventArgs e)
            {

            }

            private void pnlSureBar_Click(object sender, EventArgs e)
            {

            }

            private void lblToplamBaslik_Click(object sender, EventArgs e)
            {

            }

            private void pnlUstMenu_Paint(object sender, PaintEventArgs e)
            {

            }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefon_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }