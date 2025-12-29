using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class FilmDegerlendirme : Form
    {
        public FilmDegerlendirme()
        {
            InitializeComponent();
        }

        string baglantiAdresi = @"Server=.\SQLEXPRESS01;Database=IsikSinemaDB2;Integrated Security=True;";

        // ================= FORM LOAD =================
        private void Form4_Load(object sender, EventArgs e)
        {
            FilmDegerlendirme_Load(sender, e);
        }



        private void FilmDegerlendirme_Load(object sender, EventArgs e)
        {
            PuanlariDoldur(comboBox1); // Film Puanı
            PuanlariDoldur(cmbTemizlik);
            PuanlariDoldur(cmbKoltuk);
            PuanlariDoldur(cmbEkran);
            PuanlariDoldur(cmbSes);

            FilmleriGetir();

            cmbSalon.Enabled = false;
            cmbSeans.Enabled = false;

            comboBox2.SelectedIndexChanged += comboFilm_SelectedIndexChanged;
            cmbSalon.SelectedIndexChanged += cmbSalon_SelectedIndexChanged;
        }

        // ================= YARDIMCI METOTLAR =================
        void PuanlariDoldur(ComboBox cmb)
        {
            cmb.Items.Clear();
            for (int i = 1; i <= 10; i++) cmb.Items.Add(i);
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void FilmleriGetir()
        {
            comboBox2.Items.Clear();

            using (SqlConnection cnn = new SqlConnection(baglantiAdresi))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(
                      "SELECT FilmID, FilmAdi FROM Filmler WHERE Durum = 1 ORDER BY FilmAdi", cnn);


                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(new ComboBoxItem
                    {
                        Text = dr["FilmAdi"].ToString(),
                        Value = Convert.ToInt32(dr["FilmID"])
                    });
                }
            }

            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void SalonlariGetir(int filmId)
        {
            // Önce Salon combobox'ını temizle ve aktif et
            cmbSalon.Items.Clear();
            cmbSalon.Enabled = true;

            // Seans combobox'ını sıfırla (yeni film seçildi çünkü)
            cmbSeans.Items.Clear();
            cmbSeans.Text = "";

            using (SqlConnection cnn = new SqlConnection(baglantiAdresi))
            {
                cnn.Open();

                // SADECE O FİLMİN OYNADIĞI SALONLARI GETİRİYORUZ
                // NOT: Tablo adlarının (Salonlar, SalonAdi, SalonID) veritabanınla aynı olduğundan emin ol.
                SqlCommand cmd = new SqlCommand(@"
            SELECT DISTINCT S.SalonID, S.SalonAdi
            FROM FilmSeanslari FS
            INNER JOIN Salonlar S ON FS.SalonID = S.SalonID
            WHERE FS.FilmID = @f
            ORDER BY S.SalonAdi", cnn);

                cmd.Parameters.AddWithValue("@f", filmId);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbSalon.Items.Add(new ComboBoxItem
                    {
                        Text = dr["SalonAdi"].ToString(),
                        Value = Convert.ToInt32(dr["SalonID"])
                    });
                }
            }

            cmbSalon.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void SeanslariGetir(int filmId, int salonId)
        {
            cmbSeans.Items.Clear();
            cmbSeans.Enabled = true;

            using (SqlConnection cnn = new SqlConnection(baglantiAdresi))
            {
                cnn.Open();

                // DÜZELTME: JOIN işlemini kaldırdık. 
                // Direkt FilmSeanslari tablosundaki "Seans" sütununu çekiyoruz.
                SqlCommand cmd = new SqlCommand(@"
            SELECT DISTINCT Seans
            FROM FilmSeanslari 
            WHERE FilmID = @f AND SalonID = @s
            ORDER BY Seans", cnn);

                cmd.Parameters.AddWithValue("@f", filmId);
                cmd.Parameters.AddWithValue("@s", salonId);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // Eğer veritabanında bu alan NULL ise hata vermemesi için kontrol ekledik
                    if (dr["Seans"] != DBNull.Value)
                    {
                        cmbSeans.Items.Add(dr["Seans"].ToString());
                    }
                }
            }

            // Eğer hiç seans bulunamazsa veya liste boş kalırsa kullanıcıyı uyarabilirsin (Opsiyonel)
            if (cmbSeans.Items.Count == 0)
            {
                cmbSeans.Items.Add("Seans Bulunamadı");
                cmbSeans.Enabled = false;
            }
            else
            {
                cmbSeans.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        // ================= EVENTLER =================
        private void comboFilm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null) return;
            var film = comboBox2.SelectedItem as ComboBoxItem;
            SalonlariGetir(film.Value);
        }

        private void cmbSalon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null || cmbSalon.SelectedItem == null) return;

            var film = comboBox2.SelectedItem as ComboBoxItem;
            var salon = cmbSalon.SelectedItem as ComboBoxItem;

            SeanslariGetir(film.Value, salon.Value);
        }

        // ================= KAYDET =================
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" ||
                comboBox1.SelectedIndex == -1 ||
                comboBox2.SelectedIndex == -1 ||
                cmbSalon.SelectedIndex == -1 ||
                cmbSeans.SelectedIndex == -1 ||
                cmbTemizlik.SelectedIndex == -1 ||
                cmbKoltuk.SelectedIndex == -1 ||
                cmbEkran.SelectedIndex == -1 ||
                cmbSes.SelectedIndex == -1 ||
                richTextBox1.Text.Trim().Length < 10)
            {
                MessageBox.Show("Kral, tüm alanları eksiksiz doldur ve yorum en az 10 karakter olsun.");
                return;
            }

            using (SqlConnection cnn = new SqlConnection(baglantiAdresi))
            {
                cnn.Open();
                SqlTransaction tran = cnn.BeginTransaction();

                // button1_Click içerisindeki try bloğunun içi:

                try
                {
                    var film = comboBox2.SelectedItem as ComboBoxItem;
                    var salon = cmbSalon.SelectedItem as ComboBoxItem;

                    // 1. Önce daha önce oy vermiş mi kontrol et
                    SqlCommand kontrol = new SqlCommand(@"
        SELECT COUNT(*) FROM Tbl_Degerlendirmeler
        WHERE FilmID=@f AND MusteriAd=@m AND SalonID=@s", cnn, tran);

                    kontrol.Parameters.AddWithValue("@f", film.Value);
                    kontrol.Parameters.AddWithValue("@m", textBox1.Text.Trim());
                    kontrol.Parameters.AddWithValue("@s", salon.Value);

                    if ((int)kontrol.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Bu film ve salon için zaten değerlendirme yapmışsın kral.");
                        tran.Rollback();
                        return;
                    }

                    // 2. TEK TABLOYA TÜM VERİLERİ KAYDET (Insert komutunu birleştirdik)
                    SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Tbl_Degerlendirmeler
        (FilmID, MusteriAd, Yorum, Puan, Tarih, SalonID, Seans, TemizlikPuan, KoltukPuan, EkranPuan, SesPuan)
        VALUES 
        (@f, @m, @y, @p, @t, @s, @se, @p1, @p2, @p3, @p4)", cnn, tran);

                    cmd.Parameters.AddWithValue("@f", film.Value);
                    cmd.Parameters.AddWithValue("@m", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@y", richTextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@p", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@t", DateTime.Now);

                    // Yeni eklediğimiz alanlar
                    cmd.Parameters.AddWithValue("@s", salon.Value);
                    cmd.Parameters.AddWithValue("@se", cmbSeans.Text);
                    cmd.Parameters.AddWithValue("@p1", cmbTemizlik.Text);
                    cmd.Parameters.AddWithValue("@p2", cmbKoltuk.Text);
                    cmd.Parameters.AddWithValue("@p3", cmbEkran.Text);
                    cmd.Parameters.AddWithValue("@p4", cmbSes.Text);

                    cmd.ExecuteNonQuery();

                    tran.Commit();
                    MessageBox.Show("Kral, değerlendirme başarıyla kaydedildi.");
                    Temizle();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        void Temizle()
        {
            textBox1.Clear();
            richTextBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            cmbSalon.Items.Clear();
            cmbSeans.Items.Clear();
            cmbSalon.Enabled = false;
            cmbSeans.Enabled = false;
            cmbTemizlik.SelectedIndex = -1;
            cmbKoltuk.SelectedIndex = -1;
            cmbEkran.SelectedIndex = -1;
            cmbSes.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
            public override string ToString() => Text;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
