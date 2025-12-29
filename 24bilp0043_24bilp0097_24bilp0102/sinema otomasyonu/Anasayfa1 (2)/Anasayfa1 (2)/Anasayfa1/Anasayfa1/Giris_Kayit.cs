using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class Giris_Kayit : Form
    {
        string yerTutucuMetin = "Kullanıcı Adı";
        public string GeldigiYer = "Normal";

        // Veritabanı bağlantısı
        string baglantiAdresi = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

        public Giris_Kayit()
        {
            InitializeComponent();
        }

        private void Giris_Kayit_Load(object sender, EventArgs e)
        {
            txtAd.KeyPress += txtAd_KeyPress;
            txtSoyad.KeyPress += txtSoyad_KeyPress;
            txtTel.KeyPress += txtTel_KeyPress;

            txtEmailGiris.ForeColor = Color.White;
            txtSifre.ForeColor = Color.White;

            // pnlKayit
            txtAd.ForeColor = Color.White;
            txtSoyad.ForeColor = Color.White;
            txtEmail.ForeColor = Color.White;
            txtTel.ForeColor = Color.White;
            txtSifreKayit.ForeColor = Color.White;
            txtSifreTekrar.ForeColor = Color.White;

            // Email giriş - yazı rengi beyaz
            txtEmailGiris.ForeColor = Color.White;

            // Şifre giriş - yazı rengi beyaz
            txtSifre.ForeColor = Color.White;





            // ComboBox Ayarları
            cmbGun.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYil.DropDownStyle = ComboBoxStyle.DropDownList;
            pbClose.Image = Properties.Resources.cikis;
            label3.Text = "";

            // Panel Yerleşimi
            pnlAnaTasiyici.Controls.Add(pnlGiris);
            pnlAnaTasiyici.Controls.Add(pnlKayit);
            pnlGiris.Dock = DockStyle.Fill;
            pnlKayit.Dock = DockStyle.Fill;

            // Varsayılan Görünüm
            pnlGiris.Visible = true;
            pnlKayit.Visible = false;

            // Giriş E-posta Placeholder Ayarı
            txtEmailGiris.Text = "E-posta";
            txtEmailGiris.ForeColor = Color.Silver;
            txtEmailGiris.Font = new Font(txtEmailGiris.Font, FontStyle.Italic);

            // Tarih Doldurma
            for (int i = 1; i <= 31; i++) { cmbGun.Items.Add(i.ToString()); }
            string[] aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
            cmbAy.Items.AddRange(aylar);
            for (int i = DateTime.Now.Year; i >= 1950; i--) { cmbYil.Items.Add(i.ToString()); }

            cmbGun.Text = "Gün"; cmbAy.Text = "Ay"; cmbYil.Text = "Yıl";

            // Giriş Şifre Placeholder Ayarı
            txtSifre.Text = "Şifre";
            txtSifre.ForeColor = Color.Silver;
            txtSifre.Font = new Font(txtSifre.Font, FontStyle.Italic);
            txtSifre.PasswordChar = '\0';
        }

        // --- BUTON İŞLEMLERİ ---
        private void button1_Click(object sender, EventArgs e)
        {
            pnlGiris.Visible = true;
            pnlKayit.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnlGiris.Visible = false;
            pnlKayit.Visible = true;
        }

        // --- GİRİŞ YAPMA İŞLEMİ ---
        private void button3_Click(object sender, EventArgs e)
        {
            // 1. Boş Alan Kontrolü
            if (string.IsNullOrWhiteSpace(txtEmailGiris.Text) || txtEmailGiris.Text == "E-posta" ||
                string.IsNullOrWhiteSpace(txtSifre.Text) || txtSifre.Text == "Şifre")
            {
                MessageBox.Show("Lütfen e-posta ve şifre alanlarını doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Format Kontrolü
            if (!IsValidEmail(txtEmailGiris.Text))
            {
                MessageBox.Show("Lütfen geçerli bir e-posta adresi giriniz!", "Geçersiz Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Veritabanı Sorgusu
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                try
                {
                    baglanti.Open();
                    string sql = "SELECT id, eposta, RolID FROM Kayit_Omer WHERE eposta=@p1 AND sifre=@p2";
                    SqlCommand komut = new SqlCommand(sql, baglanti);

                    komut.Parameters.AddWithValue("@p1", txtEmailGiris.Text.Trim());
                    komut.Parameters.AddWithValue("@p2", txtSifre.Text);

                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Oturum.KullaniciID = Convert.ToInt32(dr["id"]);
                            Oturum.AktifKullaniciMail = dr["eposta"].ToString();
                            int gelenRolID = dr["RolID"] != DBNull.Value ? Convert.ToInt32(dr["RolID"]) : 5;
                            Oturum.RolID = gelenRolID;

                            MessageBox.Show("Giriş Başarılı!", "Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();

                            if (gelenRolID == 5) // Müşteri
                            {
                                Anasayfa anaSayfa = (Anasayfa)Application.OpenForms["Anasayfa"];
                                if (anaSayfa != null)
                                {
                                    anaSayfa.VarsayilanVizyondakiFilmleriGoster();
                                    anaSayfa.ButonlariGuncelle();
                                    anaSayfa.Show();
                                }
                                else
                                {
                                    Anasayfa yeniAnaSayfa = new Anasayfa();
                                    yeniAnaSayfa.Show();
                                    yeniAnaSayfa.Shown += (s, ev) =>
                                    {
                                        yeniAnaSayfa.VarsayilanVizyondakiFilmleriGoster();
                                        yeniAnaSayfa.ButonlariGuncelle();
                                    };
                                }
                            }
                            else // Admin/Personel
                            {
                                Form2 f2 = new Form2();
                                f2.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("E-posta veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı Hatası: " + ex.Message);
                }
            }
        }

        // --- KAYIT OLMA İŞLEMİ ---
        private void btnUyeOl_Click(object sender, EventArgs e)
        {

            if (txtTel.Text != "telefon(opsiyonel)" && !string.IsNullOrWhiteSpace(txtTel.Text))
            {
                if (txtTel.Text.Length != 11)
                {
                    MessageBox.Show(
                        "Geçersiz telefon numaranızı gözden geçiriniz!",
                        "Hatalı Telefon",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtTel.Focus();
                    return; // ⛔ KAYIT DURUR
                }
            }


            // 1. Temel Boşluk Kontrolleri
            if (txtAd.Text == "Adın" || string.IsNullOrWhiteSpace(txtAd.Text) ||
                txtSoyad.Text == "Soyadın" || string.IsNullOrWhiteSpace(txtSoyad.Text))
            {
                MessageBox.Show("Lütfen adınızı ve soyadınızı eksiksiz giriniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtEmail.Text == "Eposta" || !txtEmail.Text.Contains("@") || txtEmail.Text.IndexOf("@") <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir e-posta formatı giriniz!", "Hatalı Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Doğum Tarihi ve Yaş (15+) Kontrolü
            if (cmbGun.Text == "Gün" || cmbAy.Text == "Ay" || cmbYil.Text == "Yıl")
            {
                MessageBox.Show("Lütfen doğum tarihinizi tam olarak seçiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int gun = int.Parse(cmbGun.Text);
                int yil = int.Parse(cmbYil.Text);
                int ay = AyIsminiSayiyaCevir(cmbAy.Text);

                DateTime dogumTarihiObj = new DateTime(yil, ay, gun);
                int yas = DateTime.Today.Year - dogumTarihiObj.Year;
                if (dogumTarihiObj > DateTime.Today.AddYears(-yas)) yas--;

                if (yas < 15)
                {
                    MessageBox.Show($"Kayıt olabilmek için en az 15 yaşında olmalısınız. Mevcut yaşınız: {yas}", "Yaş Sınırı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch { MessageBox.Show("Geçersiz bir tarih seçtiniz!", "Hata"); return; }

            // 3. Cinsiyet ve Onay Kontrolü
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Lütfen cinsiyet seçimi yapınız!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!checkBox3.Checked)
            {
                MessageBox.Show("Lütfen üyelik şartlarını onaylayınız!", "Onay Gerekli", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Şifre Kontrolleri (Uzunluk ve Eşleşme)
            if (txtSifreKayit.Text.Length < 4 || txtSifreKayit.Text == "Şifre *")
            {
                MessageBox.Show("Şifreniz en az 4 karakter olmalıdır!", "Güvenlik", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtSifreKayit.Text != txtSifreTekrar.Text)
            {
                MessageBox.Show("Girdiğiniz şifreler birbiriyle uyuşmuyor! Lütfen büyük/küçük harflere dikkat ediniz.", "Şifre Uyuşmazlığı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSifreTekrar.Text = "";
                txtSifreTekrar.Focus();
                return;
            }

            // 5. Veritabanı Kayıt
            string dogumTarihiMetin = cmbGun.Text + " " + cmbAy.Text + " " + cmbYil.Text;
            string cinsiyet = radioButton1.Checked ? "Erkek" : "Kadın";
            string girilenEmail = txtEmail.Text.Trim();

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                try
                {
                    baglanti.Open();

                    // E-Posta Kontrolü
                    string kontrolSorgusu = "SELECT COUNT(*) FROM Kayit_Omer WHERE eposta = @mail";
                    SqlCommand kontrolKomutu = new SqlCommand(kontrolSorgusu, baglanti);
                    kontrolKomutu.Parameters.AddWithValue("@mail", girilenEmail);

                    int kayitSayisi = (int)kontrolKomutu.ExecuteScalar();

                    if (kayitSayisi > 0)
                    {
                        MessageBox.Show("Bu e-posta adresi zaten kayıtlı! Lütfen başka bir adres deneyin.", "Mevcut Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    // Kayıt Ekleme
                    string sorgu = "INSERT INTO Kayit_Omer (ad, soyad, eposta, tel, dogum_tarih, cinsiyet, sifre, RolID) " +
                                   "VALUES (@ad, @soyad, @eposta, @tel, @dtarih, @cinsiyet, @sifre, @rol)";

                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@ad", txtAd.Text.Trim());
                    komut.Parameters.AddWithValue("@soyad", txtSoyad.Text.Trim());
                    komut.Parameters.AddWithValue("@eposta", girilenEmail);

                    if (txtTel.Text == "telefon(opsiyonel)" || string.IsNullOrWhiteSpace(txtTel.Text))
                        komut.Parameters.AddWithValue("@tel", DBNull.Value);
                    else
                        komut.Parameters.AddWithValue("@tel", txtTel.Text.Trim());

                    komut.Parameters.AddWithValue("@dtarih", dogumTarihiMetin);
                    komut.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                    komut.Parameters.AddWithValue("@sifre", txtSifreKayit.Text);
                    komut.Parameters.AddWithValue("@rol", 5);

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarılı! Giriş yapabilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    pnlKayit.Visible = false;
                    pnlGiris.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı Hatası: " + ex.Message);
                }
            }

            
            

        }

        // --- YARDIMCI METOTLAR ---

        private int AyIsminiSayiyaCevir(string ayIsmi)
        {
            switch (ayIsmi)
            {
                case "Ocak": return 1;
                case "Şubat": return 2;
                case "Mart": return 3;
                case "Nisan": return 4;
                case "Mayıs": return 5;
                case "Haziran": return 6;
                case "Temmuz": return 7;
                case "Ağustos": return 8;
                case "Eylül": return 9;
                case "Ekim": return 10;
                case "Kasım": return 11;
                case "Aralık": return 12;
                default: return 1;
            }
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.IndexOf("@") > 0;
        }

        // --- YER TUTUCU (PLACEHOLDER) METOTLARI (DÜZENLENMİŞ HALİ) ---

        // AD KUTUSU
        private void txtAd_Enter(object sender, EventArgs e)
        {
            if (txtAd.Text == "Adın")
            {
                txtAd.Text = "";
                txtAd.ForeColor = Color.White; // ✅
            }

        }


        

        private void txtAd_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                txtAd.Text = "Adın";
                txtAd.ForeColor = Color.Silver;
            }
        }

        // SOYAD KUTUSU
        private void txtSoyad_Enter(object sender, EventArgs e)
        {
            if (txtSoyad.Text == "Soyadın")
            {
                txtSoyad.Text = "";
                txtSoyad.ForeColor = Color.White;
            }
        }

        private void txtSoyad_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoyad.Text))
            {
                txtSoyad.Text = "Soyadın";
                txtSoyad.ForeColor = Color.Silver;
            }
        }

        // E-POSTA KUTUSU
        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Eposta")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.White;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Text = "Eposta";
                txtEmail.ForeColor = Color.Silver;
            }
        }

        // TELEFON KUTUSU
        private void txtTel_Enter(object sender, EventArgs e)
        {
            if (txtTel.Text == "telefon(opsiyonel)")
            {
                txtTel.Text = "";
                txtTel.ForeColor = Color.White;
            }
        }

        private void txtTel_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTel.Text))
            {
                txtTel.Text = "telefon(opsiyonel)";
                txtTel.ForeColor = Color.Silver;
            }
        }


        // --- DİĞER PLACEHOLDER'LAR ---

        private void txtKullaniciAdi_Enter(object sender, EventArgs e)
        {
            if (txtEmailGiris.Text == "E-posta")
            {
                txtEmailGiris.Text = "";
                txtEmailGiris.ForeColor = Color.White; // 🔥 BEYAZ
                txtEmailGiris.Font = new Font(txtEmailGiris.Font, FontStyle.Regular);
            }
        }

        private void txtKullaniciAdi_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmailGiris.Text))
            {
                txtEmailGiris.Text = "E-posta";
                txtEmailGiris.ForeColor = Color.Silver;
                txtEmailGiris.Font = new Font(txtEmailGiris.Font, FontStyle.Italic);
            }
        }

        private void txtSifre_Enter(object sender, EventArgs e)
        {
            if (txtSifre.Text == "Şifre")
            {
                txtSifre.Text = "";
                txtSifre.ForeColor = Color.White; // ❗️SİYAH DEĞİL
                txtSifre.Font = new Font(txtSifre.Font, FontStyle.Regular);
                txtSifre.PasswordChar = chkSifreGosterGiris.Checked ? '\0' : '*';
            }
        }

        private void txtSifre_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                txtSifre.PasswordChar = '\0';
                txtSifre.Text = "Şifre";
                txtSifre.ForeColor = Color.Silver;
                txtSifre.Font = new Font(txtSifre.Font, FontStyle.Italic);
            }
        }

        private void txtSifreKayit_Enter(object sender, EventArgs e)
        {
            if (txtSifreKayit.Text == "Şifre *")
            {
                txtSifreKayit.Text = "";
                txtSifreKayit.ForeColor = Color.White;
                txtSifreKayit.PasswordChar = chkSifreGoster1.Checked ? '\0' : '*';
            }
        }

        private void txtSifreKayit_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSifreKayit.Text))
            {
                txtSifreKayit.PasswordChar = '\0';
                txtSifreKayit.Text = "Şifre *";
                txtSifreKayit.ForeColor = Color.Silver;
            }
        }

        private void txtSifreTekrar_Enter(object sender, EventArgs e)
        {
            if (txtSifreTekrar.Text == "Şifre Tekrar *")
            {
                txtSifreTekrar.Text = "";
                txtSifreTekrar.ForeColor = Color.White;
                txtSifreTekrar.PasswordChar = chkSifreGoster2.Checked ? '\0' : '*';
            }
        }

        private void txtSifreTekrar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSifreTekrar.Text))
            {
                txtSifreTekrar.PasswordChar = '\0';
                txtSifreTekrar.Text = "Şifre Tekrar *";
                txtSifreTekrar.ForeColor = Color.Silver;
            }
        }

        // --- CHECKBOX VE PUANLAMA OLAYLARI ---

        private void txtSifreKayit_TextChanged(object sender, EventArgs e)
        {
            string girilen = txtSifreKayit.Text;
            if (string.IsNullOrEmpty(girilen) || girilen == "Şifre *")
            {
                label3.Text = "";
                return;
            }

            int puan = 0;
            if (girilen.Length >= 4) puan += 10;
            if (girilen.Length >= 8) puan += 20;
            foreach (char c in girilen) { if (char.IsDigit(c)) { puan += 20; break; } }
            foreach (char c in girilen) { if (char.IsLetter(c)) { puan += 10; break; } }

            if (girilen.Length < 4) { label3.Text = "Çok Kısa"; label3.ForeColor = Color.DarkRed; }
            else if (puan < 30) { label3.Text = "Zayıf"; label3.ForeColor = Color.Red; }
            else if (puan < 50) { label3.Text = "Orta"; label3.ForeColor = Color.Orange; }
            else { label3.Text = "Güçlü"; label3.ForeColor = Color.Green; }
        }

        private void chkSifreGoster1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtSifreKayit.Text == "Şifre *" || txtSifreKayit.Text == "") return;
            txtSifreKayit.PasswordChar = chkSifreGoster1.Checked ? '\0' : '*';
        }

        private void chkSifreGoster2_CheckedChanged(object sender, EventArgs e)
        {
            if (txtSifreTekrar.Text == "Şifre Tekrar *" || txtSifreTekrar.Text == "") return;
            txtSifreTekrar.PasswordChar = chkSifreGoster2.Checked ? '\0' : '*';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtSifre.Text == "Şifre" || string.IsNullOrEmpty(txtSifre.Text)) return;
            txtSifre.PasswordChar = chkSifreGosterGiris.Checked ? '\0' : '*';
        }

        // --- KLAVYE OLAYLARI VE ÇIKIŞ ---
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (txtEmailGiris.Focused || txtSifre.Focused)
                {
                    button3.PerformClick();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            if (GeldigiYer == "Detay")
            {
                this.Close();
            }
            else
            {
                Form anaSayfa = Application.OpenForms["Anasayfa"];
                if (anaSayfa != null) anaSayfa.Show();
                else new Anasayfa().Show();
                this.Close();
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamadan çıkmak istiyor musunuz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // --- TASARIMCI (DESIGNER) İÇİN GEREKLİ BOŞ METOTLAR ---
        // Bunları silerseniz tasarım ekranı hata verir.
        private void Giris_Kayit_FormClosed(object sender, FormClosedEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void txtSifre_TextChanged(object sender, EventArgs e) { }
        private void txtSoyad_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtTel_TextChanged(object sender, EventArgs e) { }
        private void txtAd_TextChanged(object sender, EventArgs e) { }
        private void pnlKayit_Paint(object sender, PaintEventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
        private void pnlGiris_Paint(object sender, PaintEventArgs e) { }
        private void txtEmailGiris_KeyDown(object sender, KeyEventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { }
        private void chkSifreGosterGiris_CheckedChanged(object sender, EventArgs e) { }
        private void txtSifreTekrar_TextChanged(object sender, EventArgs e) { }

        private void txtAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Harf, boşluk ve Backspace serbest
            /*
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
            */

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // 11 karakter sınırı
            if (char.IsDigit(e.KeyChar) && txtTel.Text.Length >= 11)
            {
                e.Handled = true;
            }
        }

        private void txtSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}