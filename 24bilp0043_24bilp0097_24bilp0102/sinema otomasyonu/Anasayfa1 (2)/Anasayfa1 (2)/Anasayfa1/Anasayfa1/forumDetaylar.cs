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
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Anasayfa1
{
    public partial class forumDetaylar : Form
    {
        private bool filmVizyondaMi = true;
        public forumDetaylar()
        {
            InitializeComponent();
        }
        string _safFilmAdi = "";
        // Veritabanı bağlantı adresi (Senin projendeki adres)
        string baglantiAdresi = @"Data Source=.\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True";

        // Bu metod Anasayfa'dan çağrılıyor
        public void VeriAl(string filmAdi, Image resim)
        {
            // 1. Resmi ve Adı UserControl'e veya Formdaki yerlerine koy
            // Eğer resim kutun varsa: pictureBox1.Image = resim;
            // Eğer başlık label'ın varsa: lblBaslik.Text = filmAdi;

            // Eğer UserControl kullanıyorsan onu güncelleyebilirsin:
            if (movıePictureDetay1 != null)
            {
                movıePictureDetay1.BilgiVer(filmAdi, resim);
            }

            // 2. ŞİMDİ VERİTABANINDAN DİĞER BİLGİLERİ ÇEKELİM
            FilminDetaylariniGetir(filmAdi);
        }

        private void FilminDetaylariniGetir(string gelenFilmAdi)
        {
            pbClose.Image = Properties.Resources.cikis;

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                try
                {
                    baglanti.Open();
                    string sorgu = "SELECT * FROM Filmler WHERE FilmAdi = @p1";
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@p1", gelenFilmAdi);

                    SqlDataReader oku = komut.ExecuteReader();

                    if (oku.Read())
                    {
                        _safFilmAdi = oku["FilmAdi"].ToString();
                        label1.Text = "Tür: " + oku["Tur"].ToString();
                        label2.Text = "Süre: " + oku["Sure"].ToString();
                        label3.Text = "Yönetmen: " + (oku["Yonetmen"] != DBNull.Value ? oku["Yonetmen"].ToString() : "Bilgi Yok");
                        label4.Text = "Oyuncular: " + (oku["Oyuncular"] != DBNull.Value ? oku["Oyuncular"].ToString() : "Bilgi Yok");
                        label5.Text = "Film: " + oku["FilmAdi"].ToString();
                        label6.Text = oku["Ozet"] != DBNull.Value ? oku["Ozet"].ToString() : "Film özeti henüz eklenmemiş.";

                        // 🔥 KRİTİK KISIM
                        int durum = Convert.ToInt32(oku["Durum"]);
                        filmVizyondaMi = (durum == 1);

                        // ❌ Yakındaysa → Bilet Al butonu yok
                        button2.Visible = filmVizyondaMi;
                    }
                    else
                    {
                        MessageBox.Show("Film detayları bulunamadı!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
            }
        }
        // Anasayfa'dan gönderilen verileri bu metotla alacağız.
        // BU FONKSİYON ANASAYFA TARAFINDAN ÇAĞIRILACAK
        

        private void forumDetaylar_Load(object sender, EventArgs e)
        {

        }

        private void profilfoto_isim1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            // --- GİRİŞ KONTROLÜ ---
            if (string.IsNullOrEmpty(Oturum.AktifKullaniciMail))
            {
                DialogResult cevap = MessageBox.Show(
                    "Bu işlemi yapmak için giriş yapmalısınız! Giriş sayfasına gitmek ister misiniz?",
                    "Giriş Gerekli", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (cevap == DialogResult.Yes)
                {
                    // 1. Anasayfayı bul ve gizle
                    Form anaSayfa = Application.OpenForms["Anasayfa"];
                    if (anaSayfa != null) anaSayfa.Hide();

                    // 2. Kendini (Detay sayfasını) gizle
                    this.Hide();

                    // 3. Giriş ekranını DİYALOG olarak aç (Kod burada bekler)
                    Giris_Kayit girisFormu = new Giris_Kayit();
                    girisFormu.ShowDialog();

                    // --- Giriş ekranı kapandıktan sonra burası çalışır ---

                    if (!string.IsNullOrEmpty(Oturum.AktifKullaniciMail)) // Giriş başarılıysa
                    {
                        MessageBox.Show("Giriş Başarılı! İşleme devam edebilirsiniz.");

                        // Detay sayfasını geri aç (Kullanıcı tekrar Bilet Al'a basabilsin diye)
                        this.Show();

                        // Anasayfa gizli kalsın, çünkü detaydayız.
                    }
                    else // Vazgeçtiyse veya giriş yapamadıysa
                    {
                        // Kullanıcı giriş yapmadan geri döndü, her şeyi eski haline getir.
                        if (anaSayfa != null) anaSayfa.Show();
                        this.Close(); // Detay sayfasını kapatıp anasayfaya dönüyoruz
                    }
                }
            }
            else
            {
                // --- ZATEN GİRİŞ YAPILMIŞSA ---
                MessageBox.Show("Bilet alma ekranına yönlendiriliyorsunuz...");

                // 1. Bilet ekranını oluştur
                Form1 biletEkrani = new Form1();

                // Film ismini aktar (Eğer property tanımlıysa)
                biletEkrani.OtomatikSecilecekFilm = _safFilmAdi;

                // 2. Bilet ekranını göster
                biletEkrani.Show();

                // 3. Arkadaki Anasayfayı bul ve gizle (Kapatma!)
                Form anaSayfa = Application.OpenForms["Anasayfa"];
                if (anaSayfa != null)
                {
                    anaSayfa.Hide();
                }

                // 4. Detay sayfasını tamamen KAPAT (Çünkü artık işimiz Bilet ekranında)
                // Bilet ekranından geri dönünce Anasayfa açılacak (Adım 1'deki kod sayesinde)
                this.Close();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pbClose_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void movıePictureDetay1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
