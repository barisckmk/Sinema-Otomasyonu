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

namespace Anasayfa1
{
    public partial class FrmDegerlendirmeler : UserControl
    {
        public FrmDegerlendirmeler()
        {
            InitializeComponent();
        }

        // GÜNCELLEME 1: Ekran görüntüsündeki Sunucu Adını yazdım.
        // Hata alırsan 'IsikSinemaDB' ismini SQL'deki isminle (Örn: SinemaIsikDB) kontrol et.
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True");

        void Listele()
        {
            // GÜNCELLEME 2: SQL Sorgusunu fotodaki yeni sütunlara göre genişlettim.
            // Ayrıca 'Filmler' tablosunu 'Tbl_Filmler' olarak düzelttim.
            string sorgu = "SELECT " +
                           "D.YorumId, " +
                           "F.FilmAdi, " +             // FilmID yerine Adını getiriyoruz
                           "D.MusteriAd, " +
                           "D.Yorum, " +
                           "D.Puan AS [Genel Puan], " + // Karışmasın diye ismini değiştirdik
                           "D.TemizlikPuan, " +
                           "D.KoltukPuan, " +
                           "D.EkranPuan, " +
                           "D.SesPuan, " +
                           "D.Seans, " +
                           "D.Tarih " +
                           "FROM Tbl_Degerlendirmeler D " +
                           "INNER JOIN Filmler F ON D.FilmID = F.FilmID"; // JOIN düzeltildi

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // Tablo Ayarları
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception hata)
            {
                MessageBox.Show("Veri çekilirken hata oluştu: " + hata.Message);
            }
        }

        private void FrmDegerlendirmeler_Load(object sender, EventArgs e)
        {
            Listele();
            BiletleriListele();
        }



        void BiletleriListele()
        {
            // JOIN kullanarak ID'ler yerine isimleri getiriyoruz
            string sorgu = @"SELECT 
                                B.BiletID, 
                                F.FilmAdi, 
                                S.SalonAdi, 
                                SS.SeansSaati, 
                                B.Tarih, 
                                B.KoltukNo 
                             FROM Biletler B
                             INNER JOIN Filmler F ON B.FilmID = F.FilmID
                             INNER JOIN Salonlar S ON B.SalonID = S.SalonID
                             INNER JOIN SeansSaatleri SS ON B.SeansID = SS.SeansID";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.DataSource = dt;

                // Tablo ayarları
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.RowHeadersVisible = false;
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bilet bilgileri çekilirken hata oluştu: " + hata.Message);
            }
        }






        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Burası şimdilik boş kalabilir
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {




            BiletleriListele();




        }
    }
}