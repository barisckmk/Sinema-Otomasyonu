using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Anasayfa1.FrmAdminPanel;

namespace Anasayfa1
{
    public partial class FrmBiletSatis : Form
    {
        string cnnStr = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        List<int> _secilenKoltukIdleri = new List<int>();
        List<string> _secilenKoltukIsimleri = new List<string>();

        decimal _tamFiyat = 0;
        decimal _ogrenciFiyat = 0;
        decimal _toplamTutar = 0;
        int _toplamBiletSayisi = 0;

        public FrmBiletSatis() => InitializeComponent();

        private void BiletSatisFormu_Load(object sender, EventArgs e)
        {
            FilmGetir();
            FiyatlariGetir(); // Veritabanından (BiletTipleri) fiyatları çekiyoruz
            cmbSalon.Enabled = false;
            cmbSeans.Enabled = false;
            dtpTarih.Value = DateTime.Now;
        }

        void FiyatlariGetir()
        {
            using (SqlConnection conn = new SqlConnection(cnnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Ad, Fiyat FROM BiletTipleri", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string ad = dr["Ad"].ToString().ToLower();
                    if (ad.Contains("tam")) _tamFiyat = Convert.ToDecimal(dr["Fiyat"]);
                    else if (ad.Contains("öğrenci") || ad.Contains("ogrenci")) _ogrenciFiyat = Convert.ToDecimal(dr["Fiyat"]);
                }
            }
        }

        // NumericUpDown değerleri değiştiğinde otomatik çalışır
        private void HesaplaToplamTutar(object sender, EventArgs e)
        {
            _toplamBiletSayisi = (int)(nudTam.Value + nudOgrenci.Value);
            _toplamTutar = (nudTam.Value * _tamFiyat) + (nudOgrenci.Value * _ogrenciFiyat);
            lblToplamTutar.Text = $"Toplam: {_toplamTutar:N2} TL";
        }

        // --- VERİ GETİRME METODLARI ---
        void FilmGetir()
        {
            using (SqlConnection conn = new SqlConnection(cnnStr))
            {
                // DEĞİŞİKLİK BURADA YAPILDI: "WHERE Durum = 1" eklendi.
                SqlDataAdapter da = new SqlDataAdapter("SELECT FilmID, FilmAdi FROM Filmler WHERE Durum = 1", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbFilm.DisplayMember = "FilmAdi";
                cmbFilm.ValueMember = "FilmID";
                cmbFilm.DataSource = dt;
                cmbFilm.SelectedIndex = -1;
            }
        }

        void SalonGetir(int filmId)
        {
            using (SqlConnection conn = new SqlConnection(cnnStr))
            {
                string query = "SELECT DISTINCT s.SalonID, s.SalonAdi FROM FilmSeanslari fs JOIN Salonlar s ON fs.SalonID = s.SalonID WHERE fs.FilmID=@id";
                SqlDataAdapter da = new SqlDataAdapter(query, conn); da.SelectCommand.Parameters.AddWithValue("@id", filmId);
                DataTable dt = new DataTable(); da.Fill(dt);
                cmbSalon.DisplayMember = "SalonAdi"; cmbSalon.ValueMember = "SalonID";
                cmbSalon.DataSource = dt; cmbSalon.SelectedIndex = -1;
            }
        }

        void SeansGetir(int filmId, int salonId)
        {
            using (SqlConnection conn = new SqlConnection(cnnStr))
            {
                // ESKİ SORGU: "SELECT SeansID, Seans FROM FilmSeanslari WHERE FilmID=@f AND SalonID=@s"
                // Bu sorgu çift kayıt varsa hepsini getiriyordu.

                // YENİ SORGU:
                // GROUP BY ile aynı olan saatleri grupluyoruz.
                // MAX(SeansID) diyerek o saatin ID'lerinden sadece birini alıyoruz.
                string query = "SELECT MAX(SeansID) as SeansID, Seans FROM FilmSeanslari WHERE FilmID=@f AND SalonID=@s GROUP BY Seans ORDER BY Seans";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@f", filmId);
                da.SelectCommand.Parameters.AddWithValue("@s", salonId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbSeans.DisplayMember = "Seans";
                cmbSeans.ValueMember = "SeansID";
                cmbSeans.DataSource = dt;
                cmbSeans.SelectedIndex = -1;
            }
        }

        private void cmbFilm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilm.SelectedValue != null && !(cmbFilm.SelectedValue is DataRowView))
            {
                SalonGetir(Convert.ToInt32(cmbFilm.SelectedValue));
                cmbSalon.Enabled = true; cmbSeans.DataSource = null; cmbSeans.Enabled = false;
            }
        }

        private void cmbSalon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSalon.SelectedValue != null && !(cmbSalon.SelectedValue is DataRowView))
            {
                SeansGetir(Convert.ToInt32(cmbFilm.SelectedValue), Convert.ToInt32(cmbSalon.SelectedValue));
                cmbSeans.Enabled = true;
            }
        }

        private void btnKoltukSeç_Click(object sender, EventArgs e)
        {
            if (cmbSeans.SelectedValue == null || _toplamBiletSayisi <= 0)
            {
                MessageBox.Show("Lütfen seans ve bilet adedi seçin!"); return;
            }

            FrmAdminKoltukSecimi frm = new FrmAdminKoltukSecimi(Convert.ToInt32(cmbSalon.SelectedValue), Convert.ToInt32(cmbSeans.SelectedValue), _toplamBiletSayisi);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _secilenKoltukIdleri = frm.SecilenKoltukIdleri;
                _secilenKoltukIsimleri = frm.SecilenKoltukIsimleri;
                lblSecilenKoltuklar.Text = "Seçilenler: " + string.Join(", ", _secilenKoltukIsimleri);
            }
        }

        // --- SATIŞ VE MUHASEBE KAYIT ---
        private void btnSatis_Click(object sender, EventArgs e)
        {
            if (_secilenKoltukIdleri.Count != _toplamBiletSayisi)
            {
                MessageBox.Show("Koltuk seçimi ile bilet adedi uyuşmuyor!"); return;
            }

            using (SqlConnection conn = new SqlConnection(cnnStr))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();
                try
                {
                    string biletDetay = $"{nudTam.Value} Tam, {nudOgrenci.Value} Öğrenci";

                    // 1. Satislar Kaydı
                    SqlCommand cmdS = new SqlCommand("INSERT INTO Satislar (KullaniciID, Tarih, ToplamTutar, BiletDetay) VALUES (@kId, @t, @tutar, @detay); SELECT SCOPE_IDENTITY();", conn, tr);
                    cmdS.Parameters.AddWithValue("@kId", Oturum.KullaniciID);
                    cmdS.Parameters.AddWithValue("@t", DateTime.Now);
                    cmdS.Parameters.AddWithValue("@tutar", _toplamTutar);
                    cmdS.Parameters.AddWithValue("@detay", "Admin Satışı - " + biletDetay);
                    int satisId = Convert.ToInt32(cmdS.ExecuteScalar());

                    // 2. Muhasebe Kaydı (TblMuhasebe)
                    SqlCommand cmdM = new SqlCommand("INSERT INTO TblMuhasebe (Aciklama, Tutar, Tarih, Kategori, IslemiYapan, Durum) VALUES (@a,@t,@tar,'Bilet Satış','Admin','Ödendi')", conn, tr);
                    cmdM.Parameters.AddWithValue("@a", $"Admin Satışı #{satisId} ({biletDetay})");
                    cmdM.Parameters.AddWithValue("@t", _toplamTutar);
                    cmdM.Parameters.AddWithValue("@tar", DateTime.Now);
                    cmdM.ExecuteNonQuery();

                    // 3. Bilet ve Koltuk Kayıtları
                    foreach (int kid in _secilenKoltukIdleri)
                    {
                        SqlCommand c1 = new SqlCommand("INSERT INTO Biletler (FilmID, SalonID, SeansID, Tarih, KoltukNo, KullaniciID) VALUES (@f,@s,@se,@t,@k,@uId)", conn, tr);
                        c1.Parameters.AddRange(new[] {
                            new SqlParameter("@f", cmbFilm.SelectedValue), new SqlParameter("@s", cmbSalon.SelectedValue),
                            new SqlParameter("@se", cmbSeans.SelectedValue), new SqlParameter("@t", dtpTarih.Value),
                            new SqlParameter("@k", kid.ToString()), new SqlParameter("@uId", Oturum.KullaniciID)
                        });
                        c1.ExecuteNonQuery();

                        SqlCommand c2 = new SqlCommand("INSERT INTO SeansKoltuklari (SeansID, KoltukID, Durum) VALUES (@se, @kid, 1)", conn, tr);
                        c2.Parameters.AddWithValue("@se", cmbSeans.SelectedValue);
                        c2.Parameters.AddWithValue("@kid", kid);
                        c2.ExecuteNonQuery();
                    }

                    tr.Commit();
                    MessageBox.Show("Satış Başarılı ve Muhasebeye İşlendi!");

                    FormuSifirla();

                }
                catch (Exception ex) { tr.Rollback(); MessageBox.Show("Hata: " + ex.Message); }
            }
        }




        void FormuSifirla()
        {
            // ComboBox'lar
            cmbFilm.SelectedIndex = -1;
            cmbSalon.DataSource = null;
            cmbSeans.DataSource = null;

            cmbSalon.Enabled = false;
            cmbSeans.Enabled = false;

            // NumericUpDown
            nudTam.Value = 0;
            nudOgrenci.Value = 0;

            // Tarih
            dtpTarih.Value = DateTime.Now;

            // Label'lar
            lblToplamTutar.Text = "Toplam: 0,00 TL";
            lblSecilenKoltuklar.Text = "Seçilenler:";

            // Listeler
            _secilenKoltukIdleri.Clear();
            _secilenKoltukIsimleri.Clear();

            // Sayılar
            _toplamTutar = 0;
            _toplamBiletSayisi = 0;
        }






        private void lblSecilenKoltuklar_Click(object sender, EventArgs e)
        {

        }
    }
}