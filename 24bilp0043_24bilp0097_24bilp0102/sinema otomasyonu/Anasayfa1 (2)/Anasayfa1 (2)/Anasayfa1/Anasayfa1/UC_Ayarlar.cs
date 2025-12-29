using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class UC_Ayarlar : UserControl
    {
        private string secilenResimYolu = "";
        string baglantiAdresi = @"Data Source=.\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True";

        public UC_Ayarlar()
        {
            InitializeComponent();
        }

        private void UC_Ayarlar_Load(object sender, EventArgs e)
        {
            if (Oturum.KullaniciID > 0)
            {
                KullaniciBilgileriniGetir();
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            // 1. ADIM: Zorunlu Alan Kontrolü (Sadece E-Posta ve Mevcut Şifre)
            string eposta = VeriAl(textBox6, "E-Posta");
            string mevcutSifre = VeriAl(textBox2, "Mevcut Şifre");

            if (string.IsNullOrEmpty(eposta) || string.IsNullOrEmpty(mevcutSifre))
            {
                MessageBox.Show("E-Posta ve Mevcut Şifre alanlarını doldurmanız zorunludur!", "Uyarı");
                return;
            }

            // 2. ADIM: Resim Kopyalama (Client-Side)
            if (!string.IsNullOrEmpty(secilenResimYolu))
            {
                try
                {
                    string klasorYolu = Path.Combine(Application.StartupPath, "Posterler");
                    if (!Directory.Exists(klasorYolu)) Directory.CreateDirectory(klasorYolu);

                    string dosyaAdi = eposta.Replace("@", "_").Replace(".", "_") + ".png";
                    string tamHedefYolu = Path.Combine(klasorYolu, dosyaAdi);

                    File.Copy(secilenResimYolu, tamHedefYolu, true);
                }
                catch (Exception ex) { MessageBox.Show("Resim kopyalama hatası: " + ex.Message); }
            }

            // 3. ADIM: Veritabanı Güncelleme
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                try
                {
                    baglanti.Open();
                    // Takma Ad ve Profil Resim sütunları sorgudan çıkarıldı
                    string sorgu = @"UPDATE Kayit_Omer 
                                     SET ad=@ad, soyad=@soyad, tel=@tel, sifre=@yeniSifre 
                                     WHERE eposta=@eposta AND sifre=@eskiSifre";

                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@ad", VeriAl(textBox1, "Ad"));
                    komut.Parameters.AddWithValue("@soyad", VeriAl(textBox3, "Soyad"));
                    komut.Parameters.AddWithValue("@tel", VeriAl(textBox4, "Telefon"));

                    string yeniSifreKutusu = VeriAl(textBox7, "Yeni Şifre");
                    komut.Parameters.AddWithValue("@yeniSifre", string.IsNullOrEmpty(yeniSifreKutusu) ? mevcutSifre : yeniSifreKutusu);
                    komut.Parameters.AddWithValue("@eposta", eposta);
                    komut.Parameters.AddWithValue("@eskiSifre", mevcutSifre);

                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Bilgileriniz başarıyla güncellendi!");

                        // Üst paneli (Profilfoto_isim) yenile
                        Form anaForm = this.FindForm();
                        var profilPaneli = anaForm.Controls.Find("profilfoto_isim1", true).FirstOrDefault() as Profilfoto_isim;
                        
                    }
                    else
                    {
                        MessageBox.Show("Hata: Şifre veya E-Posta yanlış!");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            }
        }

        // --- İPTAL BUTONU ---
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; textBox3.Text = ""; textBox4.Text = "";
            textBox6.Text = ""; textBox2.Text = ""; textBox7.Text = "";

            textBox1_Leave(null, null); textBox3_Leave(null, null);
            textBox4_Leave(null, null); textBox6_Leave(null, null);
            textBox2_Leave(null, null); textBox7_Leave(null, null);
        }

        // --- YARDIMCI METODLAR ---
        private string VeriAl(TextBox kutu, string placeholder)
        {
            if (kutu.Text == placeholder || string.IsNullOrWhiteSpace(kutu.Text)) return "";
            return kutu.Text.Trim();
        }

        private void KutuDoldur(TextBox kutu, string veri, string placeholder)
        {
            if (!string.IsNullOrEmpty(veri))
            {
                kutu.Text = veri;
                kutu.ForeColor = Color.Black;
                kutu.Font = new Font(kutu.Font, FontStyle.Regular);
            }
            else
            {
                kutu.Text = placeholder;
                kutu.ForeColor = Color.Silver;
                kutu.Font = new Font(kutu.Font, FontStyle.Italic);
            }
        }

        // --- TEXTBOX OLAYLARI (Enter/Leave) ---
        private void textBox1_Enter(object sender, EventArgs e) { if (textBox1.Text == "Ad") { textBox1.Text = ""; textBox1.ForeColor = Color.Black; textBox1.Font = new Font(textBox1.Font, FontStyle.Regular); } }
        private void textBox1_Leave(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(textBox1.Text)) { textBox1.Text = "Ad"; textBox1.ForeColor = Color.Silver; textBox1.Font = new Font(textBox1.Font, FontStyle.Italic); } }

        private void textBox3_Enter(object sender, EventArgs e) { if (textBox3.Text == "Soyad") { textBox3.Text = ""; textBox3.ForeColor = Color.Black; textBox3.Font = new Font(textBox3.Font, FontStyle.Regular); } }
        private void textBox3_Leave(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(textBox3.Text)) { textBox3.Text = "Soyad"; textBox3.ForeColor = Color.Silver; textBox3.Font = new Font(textBox3.Font, FontStyle.Italic); } }

        private void textBox6_Enter(object sender, EventArgs e) { if (textBox6.Text == "E-Posta") { textBox6.Text = ""; textBox6.ForeColor = Color.Black; textBox6.Font = new Font(textBox6.Font, FontStyle.Regular); } }
        private void textBox6_Leave(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(textBox6.Text)) { textBox6.Text = "E-Posta"; textBox6.ForeColor = Color.Silver; textBox6.Font = new Font(textBox6.Font, FontStyle.Italic); } }

        private void textBox4_Enter(object sender, EventArgs e) { if (textBox4.Text == "Telefon") { textBox4.Text = ""; textBox4.ForeColor = Color.Black; textBox4.Font = new Font(textBox4.Font, FontStyle.Regular); } }
        private void textBox4_Leave(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(textBox4.Text)) { textBox4.Text = "Telefon"; textBox4.ForeColor = Color.Silver; textBox4.Font = new Font(textBox4.Font, FontStyle.Italic); } }

        private void textBox2_Enter(object sender, EventArgs e) { if (textBox2.Text == "Mevcut Şifre") { textBox2.Text = ""; textBox2.ForeColor = Color.Black; textBox2.Font = new Font(textBox2.Font, FontStyle.Regular); textBox2.PasswordChar = '*'; } }
        private void textBox2_Leave(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(textBox2.Text)) { textBox2.PasswordChar = '\0'; textBox2.Text = "Mevcut Şifre"; textBox2.ForeColor = Color.Silver; textBox2.Font = new Font(textBox2.Font, FontStyle.Italic); } }

        private void textBox7_Enter(object sender, EventArgs e) { if (textBox7.Text == "Yeni Şifre") { textBox7.Text = ""; textBox7.ForeColor = Color.Black; textBox7.Font = new Font(textBox7.Font, FontStyle.Regular); textBox7.PasswordChar = '*'; } }
        private void textBox7_Leave(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(textBox7.Text)) { textBox7.PasswordChar = '\0'; textBox7.Text = "Yeni Şifre"; textBox7.ForeColor = Color.Silver; textBox7.Font = new Font(textBox7.Font, FontStyle.Italic); } }

        // --- RESİM SEÇME BUTONU ---
        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                secilenResimYolu = ofd.FileName;
                MessageBox.Show("Resim seçildi, Kaydet'e bastığınızda güncellenecektir.");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void KullaniciBilgileriniGetir()
        {
            using (SqlConnection cnn = new SqlConnection(baglantiAdresi))
            {
                string sql = "SELECT ad, soyad, eposta, tel, sifre FROM Kayit_Omer WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@id", Oturum.KullaniciID);

                try
                {
                    cnn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        // AD
                        textBox1.Text = dr["ad"].ToString();
                        textBox1.ForeColor = Color.Black;
                        textBox1.Font = new Font(textBox1.Font, FontStyle.Regular);

                        // SOYAD
                        textBox3.Text = dr["soyad"].ToString();
                        textBox3.ForeColor = Color.Black;
                        textBox3.Font = new Font(textBox3.Font, FontStyle.Regular);

                        // E-POSTA
                        textBox6.Text = dr["eposta"].ToString();
                        textBox6.ForeColor = Color.Black;
                        textBox6.Font = new Font(textBox6.Font, FontStyle.Regular);

                        // TELEFON
                        textBox4.Text = dr["tel"].ToString();
                        textBox4.ForeColor = Color.Black;
                        textBox4.Font = new Font(textBox4.Font, FontStyle.Regular);

                        // MEVCUT ŞİFRE
                        textBox2.Text = dr["sifre"].ToString();
                        textBox2.ForeColor = Color.Black;
                        textBox2.Font = new Font(textBox2.Font, FontStyle.Regular);
                        textBox2.PasswordChar = '\0';

                        // YENİ ŞİFRE (HER ZAMAN PLACEHOLDER)
                        textBox7.Text = "Yeni Şifre";
                        textBox7.ForeColor = Color.Silver;
                        textBox7.Font = new Font(textBox7.Font, FontStyle.Italic);
                        textBox7.PasswordChar = '\0';
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kullanıcı bilgileri alınamadı: " + ex.Message);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}