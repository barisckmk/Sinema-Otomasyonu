using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using System.Net.NetworkInformation;

namespace Anasayfa1
{
    public partial class Odeme2 : Form
    {
        // Değişkenler
        string cnn = @"Data Source=.\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True";
        private decimal _gelenToplam;
        private int _filmID, _salonID, _seansID;
        private string _koltukAdlari, _bDetay, _uDetay;
        private List<int> _secilenKoltukIdleri;
        Dictionary<string, decimal> biletFiyatlari = new Dictionary<string, decimal>();

        // --- CONSTRUCTORLAR ---
        public Odeme2(int satisId)
        {
            InitializeComponent();
            BiletFiyatlariniYukle();
            HesaplaVeGoster();
        }

        public Odeme2(decimal toplamUcret)
        {
            InitializeComponent();
            _gelenToplam = toplamUcret;
        }

        public Odeme2(decimal toplamUcret, int fId, int sId, int seansId, string koltukAdlari, List<int> koltukIdleri, string bDetay, string uDetay)
        {
            InitializeComponent();
            _gelenToplam = toplamUcret;
            _filmID = fId;
            _salonID = sId;
            _seansID = seansId;
            _koltukAdlari = koltukAdlari;
            _secilenKoltukIdleri = koltukIdleri;
            _bDetay = bDetay;
            _uDetay = uDetay;
        }

        private void Odeme2_Load(object sender, EventArgs e)
        {
            // --- EKLENEN KISIM: Otomatik Buton Yazısı ---
            // Bu satır sayesinde label değiştiği an buton da değişecek.
            lblToplamTutar.TextChanged += lblToplamTutar_TextChanged;

            // 1. Klavyeden veri girişini kapat
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            // 2. Ayları ekle
            comboBox1.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                comboBox1.Items.Add(i.ToString("00"));
            }

            // 3. Yılları ekle
            comboBox2.Items.Clear();
            int suankiYil = DateTime.Now.Year;
            for (int i = 0; i < 15; i++)
            {
                comboBox2.Items.Add((suankiYil + i).ToString());
            }

            // Kart numarası sınırı
            textBox2.MaxLength = 19;

            // Placeholder Ayarları
            PlaceholderEkle(textBox1, "Ad Soyad");
            PlaceholderEkle(textBox2, "0000 0000 0000 0000");
            PlaceholderEkle(comboBox1, "Ay");
            PlaceholderEkle(comboBox2, "Yıl");
            PlaceholderEkle(textBox3, "CVC");

            // Başlangıç değerini ata (Event tetiklenir ve buton güncellenir)
            lblToplamTutar.Text = $"Toplam Tutar: {_gelenToplam:N2} TL";
        }

        // --- EKLENEN METOT: Label değişince Butonu Güncelle ---
        private void lblToplamTutar_TextChanged(object sender, EventArgs e)
        {
            // "Toplam Tutar: 150,00 TL" -> "150,00 TL" kısmını alır
            string sadeceTutar = lblToplamTutar.Text.Replace("Toplam Tutar: ", "");
            // Butona yazar
            button1.Text = $"Ödemeyi Onayla ({sadeceTutar})";
        }

        private void PlaceholderEkle(Control ctrl, string yerTutucuMetin)
        {
            ctrl.Text = yerTutucuMetin;
            ctrl.ForeColor = Color.Silver;

            ctrl.Enter += (s, e) =>
            {
                if (ctrl.Text == yerTutucuMetin)
                {
                    ctrl.Text = "";
                    ctrl.ForeColor = Color.Black;
                }
            };

            ctrl.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(ctrl.Text))
                {
                    ctrl.Text = yerTutucuMetin;
                    ctrl.ForeColor = Color.Silver;
                }
            };
        }

        private void BiletFiyatlariniYukle()
        {
            using (SqlConnection baglanti = new SqlConnection(cnn))
            {
                string sql = "SELECT Ad, Fiyat FROM BiletTipleri";
                SqlCommand cmd = new SqlCommand(sql, baglanti);

                baglanti.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string tipAd = dr["Ad"].ToString();
                    decimal fiyat = Convert.ToDecimal(dr["Fiyat"]);
                    biletFiyatlari[tipAd] = fiyat;
                }
            }
        }

        private void HesaplaVeGoster()
        {
            decimal biletToplami = 0;

            if (!string.IsNullOrEmpty(_bDetay))
            {
                string[] parcalar = _bDetay.Split(',');

                foreach (var parca in parcalar)
                {
                    foreach (var biletTipi in biletFiyatlari)
                    {
                        if (parca.Contains(biletTipi.Key))
                        {
                            var match = System.Text.RegularExpressions.Regex.Match(parca, @"\d+");
                            if (match.Success)
                            {
                                int adet = Convert.ToInt32(match.Value);
                                biletToplami += adet * biletTipi.Value;
                            }
                        }
                    }
                }
            }

            _gelenToplam = biletToplami;
            // Label güncellenince yukarıdaki Event çalışır ve butonu günceller
            lblToplamTutar.Text = $"Toplam Tutar: {_gelenToplam:N2} TL";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "0000 0000 0000 0000" || string.IsNullOrEmpty(textBox2.Text)) return;

            textBox2.TextChanged -= textBox2_TextChanged;

            string temizMetin = new string(textBox2.Text.Where(char.IsDigit).ToArray());
            string formatliMetin = "";

            for (int i = 0; i < temizMetin.Length; i++)
            {
                if (i > 0 && i % 4 == 0) formatliMetin += " ";
                formatliMetin += temizMetin[i];
            }

            textBox2.Text = formatliMetin;
            textBox2.SelectionStart = textBox2.Text.Length;

            textBox2.TextChanged += textBox2_TextChanged;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UyariVer(string mesaj)
        {
            MessageBox.Show(mesaj, "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // --- KONTROL KISMI ---
            if (TextBosMuVeyaPlaceholderMi(textBox1, "Ad Soyad"))
            {
                UyariVer("Lütfen Ad ve Soyad bilgisini giriniz.");
                return;
            }

            if (TextBosMuVeyaPlaceholderMi(textBox2, "0000 0000 0000 0000"))
            {
                UyariVer("Lütfen Kart Numarasını giriniz.");
                return;
            }

            // YENİ EKLENEN KISIM: KART NUMARASI UZUNLUK KONTROLÜ
            // Boşlukları silip sadece rakam sayısına bakıyoruz.
            string temizKartNo = textBox2.Text.Replace(" ", "");
            if (temizKartNo.Length < 12)
            {
                UyariVer("Kart numarası en az 12 haneli olmalıdır. Lütfen kontrol ediniz.");
                return;
            }

            if (TextBosMuVeyaPlaceholderMi(comboBox1, "Ay"))
            {
                UyariVer("Lütfen Son Kullanma Tarihi (Ay) seçiniz.");
                return;
            }

            if (TextBosMuVeyaPlaceholderMi(comboBox2, "Yıl"))
            {
                UyariVer("Lütfen Son Kullanma Tarihi (Yıl) seçiniz.");
                return;
            }

            if (TextBosMuVeyaPlaceholderMi(textBox3, "CVC"))
            {
                UyariVer("Lütfen güvenlik kodunu (CVC) giriniz.");
                return;
            }

            // --- SQL İŞLEMLERİ ---
            using (SqlConnection baglanti = new SqlConnection(cnn))
            {
                baglanti.Open();
                SqlTransaction islem = baglanti.BeginTransaction();
                try
                {
                    if (_secilenKoltukIdleri != null)
                    {
                        foreach (int kId in _secilenKoltukIdleri)
                        {
                            // A) Koltuk No Bul
                            string noSorgu = "SELECT KoltukNo FROM Koltuklar WHERE KoltukID = @id";
                            SqlCommand cmdNo = new SqlCommand(noSorgu, baglanti, islem);
                            cmdNo.Parameters.AddWithValue("@id", kId);
                            string gercekKoltukNo = cmdNo.ExecuteScalar()?.ToString();

                            // B) Bilet Kontrol (VAR MI? DURUMU NE? KİME AİT?)
                            string kontrolSql = "SELECT TOP 1 BiletID, Durum, KullaniciID FROM Biletler WHERE SeansID = @se AND KoltukNo = @kNo";
                            SqlCommand cmdKontrol = new SqlCommand(kontrolSql, baglanti, islem);
                            cmdKontrol.Parameters.AddWithValue("@se", _seansID);
                            cmdKontrol.Parameters.AddWithValue("@kNo", gercekKoltukNo);

                            int mevcutBiletID = 0;
                            bool biletKaydiVar = false;
                            bool koltukDoluMu = false;

                            using (SqlDataReader dr = cmdKontrol.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    biletKaydiVar = true;
                                    mevcutBiletID = dr.GetInt32(0);
                                    byte durum = Convert.ToByte(dr[1]);
                                    object kID = dr[2];

                                    // EĞER Durum 1 İSE VEYA KullaniciID DOLU İSE -> KOLTUK DOLUDUR
                                    if (durum == 1 || (kID != DBNull.Value))
                                    {
                                        koltukDoluMu = true;
                                    }
                                }
                            }

                            // C) KARAR AŞAMASI (Ekleme mi Güncelleme mi?)
                            if (biletKaydiVar)
                            {
                                if (koltukDoluMu)
                                {
                                    // HATA: Koltuk başkası tarafından alınmış
                                    throw new Exception($"{gercekKoltukNo} numaralı koltuk maalesef az önce başkası tarafından alındı!");
                                }
                                else
                                {
                                    // GÜNCELLEME (RECYCLE): Bilet var ama iade edilmiş (Pasif/Null).
                                    // Eski bileti tekrar canlandırıyoruz.
                                    string guncelleSql = @"UPDATE Biletler 
                                                           SET KullaniciID = @u, Durum = 1, Tarih = @t, FilmID = @f, SalonID = @s 
                                                           WHERE BiletID = @bId";

                                    SqlCommand cmdUpdate = new SqlCommand(guncelleSql, baglanti, islem);
                                    cmdUpdate.Parameters.AddWithValue("@u", (object)Oturum.KullaniciID ?? DBNull.Value);
                                    cmdUpdate.Parameters.AddWithValue("@t", DateTime.Now);
                                    cmdUpdate.Parameters.AddWithValue("@f", _filmID);
                                    cmdUpdate.Parameters.AddWithValue("@s", _salonID);
                                    cmdUpdate.Parameters.AddWithValue("@bId", mevcutBiletID);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // EKLEME (INSERT): Daha önce hiç bilet oluşturulmamış. Yeni kayıt.
                                string biletSql = @"INSERT INTO Biletler (FilmID, SalonID, SeansID, Tarih, KoltukNo, KullaniciID, Durum) 
                                                    VALUES (@f, @s, @se, @t, @kNo, @u, 1)";

                                SqlCommand cmdBilet = new SqlCommand(biletSql, baglanti, islem);
                                cmdBilet.Parameters.AddWithValue("@f", _filmID);
                                cmdBilet.Parameters.AddWithValue("@s", _salonID);
                                cmdBilet.Parameters.AddWithValue("@se", _seansID);
                                cmdBilet.Parameters.AddWithValue("@t", DateTime.Now);
                                cmdBilet.Parameters.AddWithValue("@kNo", gercekKoltukNo);
                                cmdBilet.Parameters.AddWithValue("@u", (object)Oturum.KullaniciID ?? DBNull.Value);
                                cmdBilet.ExecuteNonQuery();
                            }

                            // D) SEANS KOLTUKLARINI GÜNCELLE (Görsel Durum İçin)
                            string durumSql = @"IF NOT EXISTS (SELECT 1 FROM SeansKoltuklari WHERE SeansID=@se AND KoltukID=@k)
                                                INSERT INTO SeansKoltuklari (SeansID, KoltukID, Durum) VALUES (@se, @k, 1)
                                                ELSE
                                                UPDATE SeansKoltuklari SET Durum = 1 WHERE SeansID=@se AND KoltukID=@k";

                            SqlCommand cmdDurum = new SqlCommand(durumSql, baglanti, islem);
                            cmdDurum.Parameters.AddWithValue("@se", _seansID);
                            cmdDurum.Parameters.AddWithValue("@k", kId);
                            cmdDurum.ExecuteNonQuery();
                        }
                    }

                    // ... Muhasebe ve Satış kaydı kodlarınız buradan devam etsin ...

                    islem.Commit();
                    MessageBox.Show("Ödeme Başarılı!");
                    Anasayfa frm = new Anasayfa();
                    frm.Show();
                    this.Close();

                    this.Close();
                }
                catch (Exception ex)
                {
                    islem.Rollback();
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private bool TextBosMuVeyaPlaceholderMi(Control ctrl, string placeholder)
        {
            return string.IsNullOrWhiteSpace(ctrl.Text) || ctrl.Text == placeholder;
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Owner != null && !this.Owner.IsDisposed)
            {
                this.Owner.Show();   // önceki odeme formu
            }

            this.Close(); // sadece Odeme2 kapanır
        }

        private void lblToplamTutar_Click(object sender, EventArgs e) { }
    }
}