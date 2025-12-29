using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Anasayfa1
{
    public partial class UC_Biletlerim : UserControl
    {
        string cnnStr = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        public UC_Biletlerim()
        {
            InitializeComponent();

            BiletleriGetir();
        }
        private void UC_Biletlerim_Load(object sender, EventArgs e)
        {
            BiletleriGetir();
        }


        public void BiletIadeEt(int biletId, int seansId, int koltukNo)
        {
            using (SqlConnection baglanti = new SqlConnection(cnnStr))
            {
                baglanti.Open();
                SqlTransaction islem = baglanti.BeginTransaction();

                try
                {
                    // 1. ADIM: Bileti PASİF (0) yap
                    string biletGuncelle = "UPDATE Biletler SET Durum = 0 WHERE BiletID = @bId";
                    SqlCommand cmd1 = new SqlCommand(biletGuncelle, baglanti, islem);
                    cmd1.Parameters.AddWithValue("@bId", biletId);
                    cmd1.ExecuteNonQuery();

                    // 2. ADIM: Koltuğu boşa çıkar (SeansKoltuklari'ndan sil)
                    string koltukBosaCikar = "DELETE FROM SeansKoltuklari WHERE SeansID = @sId AND KoltukID = @kId";
                    SqlCommand cmd2 = new SqlCommand(koltukBosaCikar, baglanti, islem);
                    cmd2.Parameters.AddWithValue("@sId", seansId);
                    cmd2.Parameters.AddWithValue("@kId", koltukNo);
                    cmd2.ExecuteNonQuery();

                    // --- 3. ADIM: MUHASEBEYE İADE KAYDI EKLEME (YENİ EKLENEN KISIM) ---

                    // İpucu: Biletin tam satış fiyatı 'Biletler' tablosunda tutulmadığı için 
                    // burada varsayılan olarak 'Tam' bilet fiyatını çekiyoruz veya Satislar tablosundan
                    // oranlama yapılabilir. Basitlik adına güncel 'Tam' bilet fiyatını iade tutarı sayıyoruz.
                    decimal iadeTutari = 0;

                    // Fiyatı BiletTipleri tablosundan çekelim:
                    SqlCommand cmdFiyat = new SqlCommand("SELECT TOP 1 Fiyat FROM BiletTipleri WHERE Ad LIKE '%Tam%'", baglanti, islem);
                    object fiyatSonuc = cmdFiyat.ExecuteScalar();
                    if (fiyatSonuc != null)
                    {
                        iadeTutari = Convert.ToDecimal(fiyatSonuc);
                    }

                    // TblMuhasebe tablosuna ekleme sorgusu
                    string muhasebeKayit = @"INSERT INTO TblMuhasebe 
                                     (Aciklama, Tutar, Tarih, Kategori, OdemeTuru, IslemiYapan, FaturaNo, Durum, BelgeYolu) 
                                     VALUES 
                                     (@aciklama, @tutar, @tarih, @kategori, @odemeTuru, @islemiYapan, @faturaNo, @durum, @belgeYolu)";

                    SqlCommand cmd3 = new SqlCommand(muhasebeKayit, baglanti, islem);
                    cmd3.Parameters.AddWithValue("@aciklama", $"Bilet İadesi - BiletID: {biletId} - Koltuk: {koltukNo}");
                    cmd3.Parameters.AddWithValue("@tutar", iadeTutari); // İade edilen tutar
                    cmd3.Parameters.AddWithValue("@tarih", DateTime.Now);
                    cmd3.Parameters.AddWithValue("@kategori", "Bilet İade"); // Muhasebe kategorisi
                    cmd3.Parameters.AddWithValue("@odemeTuru", "Kredi Kartı"); // Genelde online iadeler karta yapılır
                    cmd3.Parameters.AddWithValue("@islemiYapan", "Sistem / Kullanıcı");
                    cmd3.Parameters.AddWithValue("@faturaNo", "IADE-" + biletId);
                    cmd3.Parameters.AddWithValue("@durum", "İade");
                    cmd3.Parameters.AddWithValue("@belgeYolu", ""); // Boş bırakıyoruz

                    cmd3.ExecuteNonQuery();
                    // ------------------------------------------------------------------

                    islem.Commit();

                    MessageBox.Show("Bilet başarıyla iade edildi ve muhasebe kaydı oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // LİSTEYİ YENİLE
                    BiletleriGetir();
                }
                catch (Exception ex)
                {
                    islem.Rollback();
                    MessageBox.Show("İade işlemi sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BiletleriGetir()
        {
            // 1. HER İKİ PANELİ DE TEMİZLE
            // Bunu yapmazsan her yenilemede eski kartlar durur, yenileri altına eklenir.
            flpAktifBiletler.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();

            using (SqlConnection baglanti = new SqlConnection(cnnStr))
            {
                baglanti.Open();

                // SQL sorgun gayet düzgün, Durum bilgisini zaten çekiyorsun.
                string sqlSorgusu = @"
        SELECT 
            B.BiletID,      
            B.SeansID,      
            F.FilmAdi,
            B.Tarih,
            S.Seans AS Saat,
            Sal.SalonAdi,
            B.KoltukNo,
            B.Durum
        FROM Biletler B
        INNER JOIN Filmler F ON B.FilmID = F.FilmID
        INNER JOIN Salonlar Sal ON B.SalonID = Sal.SalonID
        INNER JOIN FilmSeanslari S ON B.SeansID = S.SeansID
        WHERE B.KullaniciID = @kId
        ORDER BY B.Tarih DESC";

                SqlCommand komut = new SqlCommand(sqlSorgusu, baglanti);
                komut.Parameters.AddWithValue("@kId", Oturum.KullaniciID);

                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    // Verileri al
                    int biletId = Convert.ToInt32(oku["BiletID"]);
                    int seansId = Convert.ToInt32(oku["SeansID"]);
                    // Durum 1 (True) ise Aktif, 0 (False) ise İade
                    bool durum = oku["Durum"] != DBNull.Value && Convert.ToBoolean(oku["Durum"]);

                    // Kartı oluştur (UserControl)
                    aktifBiletler yeniBilet = new aktifBiletler();

                    // Bilgileri doldur
                    yeniBilet.BilgileriDoldur(
                        biletId,
                        seansId,
                        oku["FilmAdi"].ToString(),
                        Convert.ToDateTime(oku["Tarih"]).ToShortDateString(),
                        oku["Saat"].ToString(),
                        oku["SalonAdi"].ToString(),
                        oku["KoltukNo"].ToString(),
                        null
                    );

                    // --- AYRIŞTIRMA MANTIĞI ---
                    if (durum == true)
                    {
                        // DURUM AKTİF İSE -> flpAktifBiletler'e ekle
                        yeniBilet.Width = flpAktifBiletler.Width - 30; // Genişlik ayarı
                        flpAktifBiletler.Controls.Add(yeniBilet);
                    }
                    else
                    {
                        // DURUM PASİF (İADE) İSE -> flowLayoutPanel1'e ekle

                        // Görsel olarak belli olması için rengini değiştirebilirsin
                        yeniBilet.BackColor = Color.MistyRose; // Hafif kırmızımsı bir renk
                                                               // İade edilmiş bilete tekrar tıklanıp işlem yapılmasın diye:
                        yeniBilet.Enabled = false;

                        // İade panelinin genişliğine göre boyutlandır
                        yeniBilet.Width = flowLayoutPanel1.Width - 30;

                        // İADE PANELİNE EKLE
                        flowLayoutPanel1.Controls.Add(yeniBilet);
                    }
                }
            }
        }

        // İsim temizleme fonksiyonun
        
        

        // Senin resim bulma fonksiyonun (Aynen kopyaladım)
        public string IsmiTemizle(string metin)
        {
            if (string.IsNullOrEmpty(metin)) return "";
            metin = metin.Replace(" ", "");
            string eski = "ığüşöçİĞÜŞÖÇ";
            string yeni = "igusocIGUSOC";
            for (int i = 0; i < eski.Length; i++)
            {
                metin = metin.Replace(eski[i], yeni[i]);
            }
            return metin.Replace(":", "").Replace("*", "").Replace("?", "");
        }
        private void flpAktifBiletler_Paint(object sender, PaintEventArgs e)
        {

        }

        private void aktifBiletler1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
