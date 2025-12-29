using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
namespace Anasayfa1
{
    public partial class FrmYönetim : Form
    {
        // ---------------------------------------------------------
        // 1. SQL BAĞLANTISI 
        // ---------------------------------------------------------
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True");

        // Seçilen seansın ID'sini hafızada tutmak için bu değişkeni kullanacağız
        int secilenSeansID = 0;

        public FrmYönetim()
        {
            InitializeComponent();
        }

        // ---------------------------------------------------------
        // 2. FORM YÜKLENİRKEN ÇALIŞACAK KODLAR
        // ---------------------------------------------------------
        private void FrmYonetim_Load(object sender, EventArgs e)
        {
            // Olayları (Events) Manuel Bağlıyoruz
            cmbFilm.SelectedIndexChanged += new EventHandler(cmbFilm_SelectedIndexChanged);
            cmbSalon.SelectedIndexChanged += new EventHandler(cmbSalon_SelectedIndexChanged);

            ComboboxlariDoldur(); // Açılır kutuları doldur
            SeansListele();       // Aşağıdaki listeyi doldur

            // Başlangıçta sıralı seçim için kutuları kapatıyoruz
            KutulariPasifYap();
        }

        // Başlangıç durumu ve temizleme sonrası için yardımcı metot
        void KutulariPasifYap()
        {
            cmbSalon.Enabled = false;
            cmbSeans.Enabled = false;
            cmbTarih.Enabled = false;
        }

        // ---------------------------------------------------------
        // 3. YARDIMCI METOTLAR
        // ---------------------------------------------------------

        // ComboBox'lara Veritabanından Veri Çekme Metodu
        void ComboboxlariDoldur()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                // Olay tetiklenmesini geçici durdur (gereksiz çalışmasın)
                cmbFilm.SelectedIndexChanged -= cmbFilm_SelectedIndexChanged;
                cmbSalon.SelectedIndexChanged -= cmbSalon_SelectedIndexChanged;

                // Filmleri Çek
                SqlDataAdapter daFilm = new SqlDataAdapter("SELECT * FROM Filmler", baglanti);
                DataTable dtFilm = new DataTable();
                daFilm.Fill(dtFilm);
                cmbFilm.DisplayMember = "FilmAdi";
                cmbFilm.ValueMember = "FilmID";
                cmbFilm.DataSource = dtFilm;
                cmbFilm.SelectedIndex = -1; // Seçimi temizle

                // Salonları Çek (Admin panelinde TÜM salonları görmeliyiz ki atama yapabilelim)
                SqlDataAdapter daSalon = new SqlDataAdapter("SELECT * FROM Salonlar", baglanti);
                DataTable dtSalon = new DataTable();
                daSalon.Fill(dtSalon);
                cmbSalon.DisplayMember = "SalonAdi";
                cmbSalon.ValueMember = "SalonID";
                cmbSalon.DataSource = dtSalon;
                cmbSalon.SelectedIndex = -1;

                // Seans Saatlerini Çek
                SqlDataAdapter daSeans = new SqlDataAdapter("SELECT * FROM SeansSaatleri ORDER BY SeansSaati", baglanti);
                DataTable dtSeans = new DataTable();
                daSeans.Fill(dtSeans);
                cmbSeans.DisplayMember = "SeansSaati";
                cmbSeans.ValueMember = "SeansID";
                cmbSeans.DataSource = dtSeans;
                cmbSeans.SelectedIndex = -1;
                cmbSeans.Text = ""; // Text kısmını da temizle

                // Tarihleri Çek
                SqlDataAdapter daTarih = new SqlDataAdapter("SELECT * FROM SeansTarihler ORDER BY Tarih DESC", baglanti);
                DataTable dtTarih = new DataTable();
                daTarih.Fill(dtTarih);
                cmbTarih.DisplayMember = "Tarih";
                cmbTarih.ValueMember = "TarihID";
                cmbTarih.DataSource = dtTarih;
                cmbTarih.SelectedIndex = -1;

                // Olayları geri aç
                cmbFilm.SelectedIndexChanged += cmbFilm_SelectedIndexChanged;
                cmbSalon.SelectedIndexChanged += cmbSalon_SelectedIndexChanged;
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            finally { if (baglanti.State == ConnectionState.Open) baglanti.Close(); }
        }

        // --- SIRALI SEÇİM MANTIĞI (EVENTLER) ---

        // Film Seçilince -> Salon Kutusu Açılsın
        private void cmbFilm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilm.SelectedIndex != -1)
            {
                cmbSalon.Enabled = true; // Filmi seçti, şimdi salon seçebilir

                // Eğer film değişirse alt seçimleri sıfırla
                cmbSalon.SelectedIndex = -1;
                cmbSeans.Enabled = false;
                cmbSeans.SelectedIndex = -1;
                cmbSeans.Text = "";
                cmbTarih.Enabled = false;
                cmbTarih.SelectedIndex = -1;
            }
            else
            {
                KutulariPasifYap();
            }
        }

        // Salon Seçilince -> Tarih ve Saat Kutusu Açılsın
        private void cmbSalon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSalon.SelectedIndex != -1 && cmbFilm.SelectedIndex != -1)
            {
                cmbSeans.Enabled = true;
                cmbTarih.Enabled = true;
            }
            else
            {
                cmbSeans.Enabled = false;
                cmbTarih.Enabled = false;
            }
        }

        // ---------------------------------------------------------

        // Oluşan Seansları Listeleme Metodu (JOIN İşlemi)
        void SeansListele()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                string sorgu = @"
            SELECT 
                FS.SeansID as 'Kayıt No', 
                F.FilmAdi as 'Film',
                S.SalonAdi as 'Salon',
                T.Tarih as 'Tarih',
                FS.Seans as 'Saat' 
            FROM FilmSeanslari FS
            INNER JOIN Filmler F ON FS.FilmID = F.FilmID
            INNER JOIN Salonlar S ON FS.SalonID = S.SalonID
            INNER JOIN SeansTarihler T ON FS.TarihID = T.TarihID
            ORDER BY T.Tarih DESC, FS.SeansID DESC";

                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSeanslar.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme Hatası: " + ex.Message);
            }
            finally { baglanti.Close(); }
        }

        // ---------------------------------------------------------
        // 4. BUTON İŞLEMLERİ
        // ---------------------------------------------------------

        // A) Film Kaydetme
        private void btnFilmKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string sorgu = "INSERT INTO Filmler (FilmAdi, Tur, Sure, AfisYolu) VALUES (@p1, @p2, @p3, @p4)";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", txtFilmAdi.Text);
                komut.Parameters.AddWithValue("@p2", txtTur.Text);
                komut.Parameters.AddWithValue("@p3", txtSure.Text);
                komut.Parameters.AddWithValue("@p4", ""); // Afiş yolu boş

                komut.ExecuteNonQuery();
                MessageBox.Show("Film Başarıyla Eklendi");

                baglanti.Close();
                ComboboxlariDoldur();
                KutulariPasifYap(); // Listeyi yenileyince resetle
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        // B) Salon Ekleme
        private void btnSalonEkle_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Salonlar (SalonAdi) VALUES (@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtSalonAdi.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Salon Eklendi");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                baglanti.Close();
                ComboboxlariDoldur();
                KutulariPasifYap();
            }
        }

        // C) Saat Ekleme
        private void btnSaatEkle_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO SeansSaatleri (SeansSaati) VALUES (@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtSeansSaati.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Saat Eklendi");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                baglanti.Close();
                ComboboxlariDoldur();
                KutulariPasifYap();
            }
        }

        // D) Tarih Ekleme
        private void btnTarihEkle_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO SeansTarihler (Tarih) VALUES (@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", dtpTarih.Value.ToString("dd.MM.yyyy"));
                komut.ExecuteNonQuery();
                MessageBox.Show("Tarih Eklendi");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { baglanti.Close(); ComboboxlariDoldur(); KutulariPasifYap(); }
        }

        // E) ANA İŞLEM: SEANS OLUŞTURMA
        private void btnSeansOlustur_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbFilm.SelectedValue == null || cmbSalon.SelectedValue == null || cmbTarih.SelectedValue == null || cmbSeans.Text == "")
                {
                    MessageBox.Show("Lütfen tüm seçimleri yapınız.");
                    return;
                }

                baglanti.Open();

                string sorgu = "INSERT INTO FilmSeanslari (FilmID, SalonID, TarihID, Seans) VALUES (@p1, @p2, @p3, @p4)";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", cmbFilm.SelectedValue);
                komut.Parameters.AddWithValue("@p2", cmbSalon.SelectedValue);
                komut.Parameters.AddWithValue("@p3", cmbTarih.SelectedValue);
                komut.Parameters.AddWithValue("@p4", cmbSeans.Text); // Text olarak kaydet

                komut.ExecuteNonQuery();
                MessageBox.Show("Seans Başarıyla Oluşturuldu!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
                SeansListele();
                Temizle(); // Kayıt sonrası formu temizle
            }
        }

        private void btnFilmSil_Click(object sender, EventArgs e)
        {
            if (cmbFilm.SelectedValue == null)
            {
                MessageBox.Show("Lütfen silinecek filmi seçiniz.");
                return;
            }

            DialogResult onay = MessageBox.Show(cmbFilm.Text + " filmini silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Filmler WHERE FilmID = @p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", cmbFilm.SelectedValue);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Film başarıyla silindi.");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Bu film şu anda seanslarda kayıtlı olduğu için silinemez! Önce seansları silmelisiniz.");
                    }
                    else
                    {
                        MessageBox.Show("Silme Hatası: " + ex.Message);
                    }
                }
                finally
                {
                    baglanti.Close();
                    ComboboxlariDoldur();
                    KutulariPasifYap();
                }
            }
        }

        // Boş metot
        private void cmbSeans_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            SeansListele();
        }

        private void dgvSeanslar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    secilenSeansID = int.Parse(dgvSeanslar.Rows[e.RowIndex].Cells[0].Value.ToString());

                    // Listeden seçince güncelleme yapmak için tüm kutuları aktif etmeliyiz
                    cmbSalon.Enabled = true;
                    cmbSeans.Enabled = true;
                    cmbTarih.Enabled = true;

                    cmbFilm.Text = dgvSeanslar.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cmbSalon.Text = dgvSeanslar.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cmbTarih.Text = dgvSeanslar.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cmbSeans.Text = dgvSeanslar.Rows[e.RowIndex].Cells[4].Value.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        void Temizle()
        {
            cmbFilm.SelectedIndex = -1;
            cmbSalon.SelectedIndex = -1;
            cmbTarih.SelectedIndex = -1;
            cmbSeans.Text = "";
            cmbSeans.SelectedIndex = -1;
            secilenSeansID = 0;

            // Temizleyince tekrar pasif moda geç
            KutulariPasifYap();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (secilenSeansID == 0)
            {
                MessageBox.Show("Lütfen listeden güncellenecek bir seans seçiniz.");
                return;
            }

            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                string sorgu = @"UPDATE FilmSeanslari 
                         SET FilmID = @p1, SalonID = @p2, TarihID = @p3, Seans = @p4 
                         WHERE SeansID = @p5";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", cmbFilm.SelectedValue);
                komut.Parameters.AddWithValue("@p2", cmbSalon.SelectedValue);
                komut.Parameters.AddWithValue("@p3", cmbTarih.SelectedValue);
                komut.Parameters.AddWithValue("@p4", cmbSeans.Text);
                komut.Parameters.AddWithValue("@p5", secilenSeansID);

                komut.ExecuteNonQuery();

                MessageBox.Show("Seans başarıyla güncellendi!");
                SeansListele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme Hatası: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (secilenSeansID == 0)
            {
                MessageBox.Show("Lütfen silinecek seansı listeden seçiniz.");
                return;
            }

            DialogResult onay = MessageBox.Show("Bu seans kaydını kalıcı olarak silmek istiyor musunuz?",
                                                "Silme Onayı",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                    SqlCommand komut = new SqlCommand("DELETE FROM FilmSeanslari WHERE SeansID = @p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", secilenSeansID);

                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kayıt veritabanından silindi.");
                    SeansListele();
                    Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme Hatası: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }
        }

        private void btnSalonSil_Click(object sender, EventArgs e)
        {
            if (cmbSalon.SelectedValue == null)
            {
                MessageBox.Show("Lütfen silinecek salonu aşağıdan seçiniz.");
                return;
            }

            DialogResult onay = MessageBox.Show(cmbSalon.Text + " salonunu silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Salonlar WHERE SalonID = @p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", cmbSalon.SelectedValue);
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Salon başarıyla silindi.");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Bu salonda atanmış seanslar var! Önce o seansları silmelisiniz.");
                    }
                    else
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
                finally
                {
                    baglanti.Close();
                    ComboboxlariDoldur();
                    KutulariPasifYap();
                }
            }
        }

        private void btnSaatSil_Click(object sender, EventArgs e)
        {
            if (cmbSeans.SelectedValue == null)
            {
                MessageBox.Show("Lütfen silinecek saati aşağıdan seçiniz.");
                return;
            }

            DialogResult onay = MessageBox.Show(cmbSeans.Text + " saatini silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM SeansSaatleri WHERE SeansID = @p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", cmbSeans.SelectedValue);
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Saat başarıyla silindi.");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Bu saate kayıtlı film seansları var! Önce o seansları silmelisiniz.");
                    }
                    else
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
                finally
                {
                    baglanti.Close();
                    ComboboxlariDoldur();
                    KutulariPasifYap();
                }
            }
        }

        private void btnTarihSil_Click(object sender, EventArgs e)
        {
            if (cmbTarih.SelectedValue == null)
            {
                MessageBox.Show("Lütfen silinecek tarihi aşağıdan seçiniz.");
                return;
            }

            DialogResult onay = MessageBox.Show(cmbTarih.Text + " tarihini silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM SeansTarihler WHERE TarihID = @p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", cmbTarih.SelectedValue);
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Tarih başarıyla silindi.");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Bu tarihe kayıtlı film seansları var! Önce o seansları silmelisiniz.");
                    }
                    else
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
                finally
                {
                    baglanti.Close();
                    ComboboxlariDoldur();
                    KutulariPasifYap();
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbTarih_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}