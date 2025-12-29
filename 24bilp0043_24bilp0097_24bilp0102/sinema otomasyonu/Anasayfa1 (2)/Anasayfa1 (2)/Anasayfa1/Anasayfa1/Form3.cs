using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Anasayfa1
{
    public partial class Anasayfa : Form
    {
        // Sabit Değişkenler
        private const string VIZYON_FILTRE = "WHERE Durum = 1"; // DAİMA vizyondakiler

        public Anasayfa()
        {
            InitializeComponent();
        }

        // --- FORM YÜKLENME VE BAŞLANGIÇ AYARLARI ---

        private void Form3_Load(object sender, EventArgs e)
        {
            pbClose.Image = Properties.Resources.cikis;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0; // Akıllı sıralama

            // 🔒 SADECE VİZYON
            FilmleriGetir(VIZYON_FILTRE, "ORDER BY FilmID DESC");

            panel2.Visible = true;
            panel3.Visible = false;

            ButonlariGuncelle();
        }

        public void VarsayilanVizyondakiFilmleriGoster()
        {
            comboBox1.SelectedIndex = 0;
            panel2.Visible = true;
            panel3.Visible = false;
            FilmleriGetir("WHERE Durum = 1", "ORDER BY FilmID DESC");
        }

        private void Anasayfa_Activated(object sender, EventArgs e)
        {
            ButonlariGuncelle();
        }

        // --- ANA FİLME GETİRME VE LİSTELEME MANTIĞI ---

        private void FilmleriGetir(string filtreSorgusu = "", string siralamaSorgusu = "ORDER BY FilmID DESC")
        {
            flowLayoutPanel1.Controls.Clear();
            // LocalDB bağlantısı
            string adres = @"Data Source=.\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True";

            // Uygulamanın çalıştığı yer (bin\Debug)
            string anaYol = Application.StartupPath;

            try
            {
                using (SqlConnection baglanti = new SqlConnection(adres))
                {
                    baglanti.Open();
                    string sql = "SELECT * FROM Filmler " + filtreSorgusu + " " + siralamaSorgusu;
                    SqlCommand komut = new SqlCommand(sql, baglanti);
                    SqlDataReader oku = komut.ExecuteReader();

                    while (oku.Read())
                    {
                        string filmAdi = oku["FilmAdi"].ToString().Trim();
                        string sure = oku["Sure"].ToString().Trim();
                        string tur = oku["Tur"].ToString().Trim();
                        string puan = oku["Puan"] != DBNull.Value ? oku["Puan"].ToString() : "-";
                        string dbdenGelenYol = oku["AfisYolu"].ToString().Trim();

                        // Yol düzeltmesi
                        string temizYol = dbdenGelenYol.Replace("/", "\\");
                        string tamYol = Path.Combine(anaYol, temizYol);

                        Image afisResmi = null;

                        if (File.Exists(tamYol))
                        {
                            using (FileStream fs = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                            {
                                afisResmi = Image.FromStream(fs);
                            }
                        }
                        else
                        {
                            // --- SORUNU YAKALAYAN KOD BLOĞU ---
                            // Sadece gelmeyen filmlerden biri için (Örn: Mickey 17) rapor verelim ki ekran donmasın.
                            if (filmAdi.Contains("Mickey") || filmAdi.Contains("Superman"))
                            {
                                string posterKlasoru = Path.Combine(anaYol, "Posterler");
                                string rapor = "PROGRAMIN BAKTIĞI YER:\n" + tamYol + "\n\n";

                                if (Directory.Exists(posterKlasoru))
                                {
                                    rapor += "BU KLASÖRDEKİ DOSYALAR:\n";
                                    string[] dosyalar = Directory.GetFiles(posterKlasoru);
                                    foreach (string dosya in dosyalar)
                                    {
                                        rapor += Path.GetFileName(dosya) + "\n";
                                    }
                                }
                                else
                                {
                                    rapor += "HATA: Program 'Posterler' diye bir klasör bile bulamadı!\n";
                                    rapor += "Baktığı yer: " + posterKlasoru;
                                }

                                MessageBox.Show(rapor, "Hata Ayıklama Raporu");
                            }
                            // -----------------------------------
                        }

                        MovieCard kart = new MovieCard();
                        kart.SetData(filmAdi, sure, tur, puan, afisResmi);
                        kart.Click += Kart_Click;
                        flowLayoutPanel1.Controls.Add(kart);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Film Listeleme Hatası: " + ex.Message);
            }
        }
        // --- YENİ EKLENECEK METOT ---
        // Bu metodu class'ın içine, diğer metotların yanına ekleyin.
        private void ProfilResminiYukle()
        {
            // 1. Eğer kimse giriş yapmamışsa işlem yapma ve var olan resmi temizle
            if (string.IsNullOrEmpty(Oturum.AktifKullaniciMail))
            {
                btnProfil.Image = null;
                // Eğer projenizde varsayılan bir kullanıcı resmi varsa onu da atayabilirsiniz:
                // btnProfil.Image = Properties.Resources.user_default; 
                return;
            }

            try
            {
                // 2. Resim yolunu oluştur (Ayarlar sayfasındaki mantığın aynısı)
                string klasorYolu = Path.Combine(Application.StartupPath, "Posterler");
                // Maildeki @ ve . işaretlerini _ ile değiştirip dosya adını buluyoruz
                string dosyaAdi = Oturum.AktifKullaniciMail.Replace("@", "_").Replace(".", "_") + ".png";
                string tamYol = Path.Combine(klasorYolu, dosyaAdi);

                // 3. Dosya var mı kontrol et
                if (File.Exists(tamYol))
                {
                    // FileStream kullanarak dosyayı kilitlemeden okuyoruz
                    using (FileStream fs = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                    {
                        // Resmi butona ata.
                        // Not: Akış kapandığında sorun çıkmaması için yeni bir Bitmap oluşturuyoruz.
                        btnProfil.Image = new Bitmap(Image.FromStream(fs));
                    }

                    // GÖRSEL AYARLAR (İsteğe bağlı)
                    // Resmin ve yazının nasıl duracağını ayarlayabilirsiniz.
                    // Örneğin resim solda, yazı sağda:
                    btnProfil.ImageAlign = ContentAlignment.MiddleLeft;
                    btnProfil.TextAlign = ContentAlignment.MiddleRight;
                    // Veya resim üstte, yazı altta:
                    // btnProfil.ImageAlign = ContentAlignment.TopCenter;
                    // btnProfil.TextAlign = ContentAlignment.BottomCenter;
                }
                else
                {
                    // Resim dosyası yoksa (henüz yüklememişse) varsayılan bir durum
                    btnProfil.Image = null;
                    // btnProfil.Image = Properties.Resources.user_default; // Varsayılan resim varsa
                }
            }
            catch (Exception ex)
            {
                // Olası bir hata durumunda (örn: resim bozuksa) sessizce resmi temizle
                // MessageBox.Show("Resim yüklenirken hata: " + ex.Message); // Test için açılabilir
                btnProfil.Image = null;
            }
        }
        // --- KART TIKLAMA VE DETAY GÖSTERME ---

        private void Kart_Click(object sender, EventArgs e)
        {
            // 1. Tıklanan kartı yakala
            MovieCard tiklananKart = (MovieCard)sender;

            // 2. Detay sayfasını hazırla
            forumDetaylar detaySayfasi = new forumDetaylar();

            // 3. Verileri gönder
            detaySayfasi.VeriAl(tiklananKart.TasinanFilmAdi, tiklananKart.TasinanResim);

            // 4. Göster
            detaySayfasi.ShowDialog();
        }

        // --- ARAYÜZ GÜNCELLEME VE YARDIMCI METOTLAR ---

        public void ButonlariGuncelle()
        {
            // Giriş yapılıp yapılmadığını kontrol et
            bool girisYapildiMi = Oturum.KullaniciID > 0;

            // --- GİRİŞ YAPILINCA GÖRÜNECEKLER ---
            btnProfil.Visible = girisYapildiMi;        // Profil Butonu
            btnDeğerlendir.Visible = girisYapildiMi;   // Değerlendir Butonu
            button5.Visible = girisYapildiMi;          // Çıkış butonu

            // --- GİRİŞ YAPILINCA GİZLENECEKLER ---
            button3.Visible = !girisYapildiMi;         // Giriş Yap / Kayıt Ol butonu

            // --- 🔥 YENİ EKLENEN KISIM 🔥 ---
            if (girisYapildiMi)
            {
                // Giriş yapıldıysa profil resmini yükle
                ProfilResminiYukle();
            }
            else
            {
                // Çıkış yapıldıysa butondaki resmi temizle
                btnProfil.Image = null;
            }
        }

        public string IsmiTemizle(string metin)
        {
            if (string.IsNullOrEmpty(metin)) return "";

            // 1. Boşlukları sil
            metin = metin.Replace(" ", "");

            // 2. Türkçe harfleri İngilizceye çevir
            string eski = "ığüşöçİĞÜŞÖÇ";
            string yeni = "igusocIGUSOC";

            for (int i = 0; i < eski.Length; i++)
            {
                metin = metin.Replace(eski[i], yeni[i]);
            }

            // 3. Dosya isminde yasaklı olabilecek karakterleri temizle
            metin = metin.Replace(":", "").Replace("*", "").Replace("?", "");

            return metin;
        }

        // --- FİLTRELEME VE SIRALAMA OLAYLARI ---

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            panel2.Visible = false;
            panel3.Visible = false;

            string siralama = "ORDER BY FilmID DESC"; // Akıllı (en yeni üstte)
            string secilen = comboBox1.SelectedItem.ToString();

            if (secilen == "Vizyon Tarihine Göre")
            {
                // GERÇEK TARİH YOK → ID'ye göre sırala
                siralama = "ORDER BY FilmID ASC";
            }
            else if (secilen == "Puana Göre")
            {
                siralama = "ORDER BY Puan DESC";
            }

            // 🔒 SADECE VİZYONDAKİLER
            FilmleriGetir("WHERE Durum = 1", siralama);
        }

        private void button1_Click(object sender, EventArgs e) // Vizyondakiler Butonu
        {
            panel2.Visible = true;  // Panel 2'yi göster
            panel3.Visible = false; // Panel 3'ü gizle
            FilmleriGetir("WHERE Durum = 1");
        }

        private void button2_Click(object sender, EventArgs e) // Yakında Butonu
        {
            panel3.Visible = true;  // Panel 3'ü göster
            panel2.Visible = false; // Panel 2'yi gizle
            FilmleriGetir("WHERE Durum = 0");
        }

        // --- BUTON İŞLEMLERİ (GİRİŞ, ÇIKIŞ, PROFİL) ---

        private void button3_Click(object sender, EventArgs e) // Giriş Yap / Kayıt Ol
        {
            Giris_Kayit yeniPencere = new Giris_Kayit();
            yeniPencere.Show();
            this.Hide();
        }

        private void button5_Click_3(object sender, EventArgs e) // Çıkış Yap
        {
            DialogResult cevap = MessageBox.Show(
                "Oturumu kapatmak istediğinize emin misiniz?",
                "Çıkış Yap",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                Oturum.KullaniciID = 0;
                Oturum.AktifKullaniciMail = "";
                Oturum.RolID = 0;

                MessageBox.Show("Başarıyla çıkış yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ButonlariGuncelle();
            }
        }

        private void btnProfil_Click(object sender, EventArgs e) // Profil
        {
            this.Hide();
            Profil yeniPencere = new Profil();
            yeniPencere.Show();
        }

        private void button4_Click(object sender, EventArgs e) // Biletlerim (veya başka form)
        {
            Form1 yeniPencere = new Form1();
            yeniPencere.Show();
            this.Hide();
        }

        private void btnDeğerlendir_Click(object sender, EventArgs e) // Değerlendir
        {
            FilmDegerlendirme frm = new FilmDegerlendirme();
            frm.Show();
        }

        private void pbClose_Click(object sender, EventArgs e) // Uygulamayı Kapat
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

        // --- GÖRSEL DÜZENLEMELER (PAINT EVENTS) ---

        private void btnDeğerlendir_Paint(object sender, PaintEventArgs e)
        {
            int radius = 20; // Köşe yumuşaklığı
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddArc(0, 0, radius, radius, 180, 90);
            gp.AddArc(btnDeğerlendir.Width - radius, 0, radius, radius, 270, 90);
            gp.AddArc(btnDeğerlendir.Width - radius, btnDeğerlendir.Height - radius, radius, radius, 0, 90);
            gp.AddArc(0, btnDeğerlendir.Height - radius, radius, radius, 90, 90);
            btnDeğerlendir.Region = new Region(gp);
        }

        // --- BOŞ EVENTLER (TASARIMCI İÇİN SAKLANDI) ---
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void button4_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void button1_Paint(object sender, PaintEventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void panel5_Paint(object sender, PaintEventArgs e) { }
        private void button5_Click(object sender, EventArgs e) { }
        private void button5_Click_1(object sender, EventArgs e) { }
        private void button5_Click_2(object sender, EventArgs e) { }
        private void button5_Click3(object sender, EventArgs e) { }
    }
}