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
    public partial class aktifBiletler : UserControl
    {
        private int _biletID;
        private int _seansID;
        private string _koltukNo;

        string adres = @"Data Source=.\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True";
        string anaYol = Application.StartupPath;

        public aktifBiletler()
        {
            InitializeComponent();
        }

        public void BilgileriDoldur(
            int biletID,
            int seansID,
            string filmAdi,
            string tarih,
            string saat,
            string salon,
            string koltuk,
            Image resim)
        {
            _biletID = biletID;
            _seansID = seansID;
            _koltukNo = koltuk;

            label1.Text = filmAdi;
            label2.Text = tarih;
            label3.Text = saat;
            label4.Text = salon;
            label5.Text = koltuk;

            if (resim != null)
            {
                pictureBox1.Image = resim;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                ResmiVeritabanindanYukle(filmAdi);
            }
        }

        private void ResmiVeritabanindanYukle(string filmAdi)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(adres))
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT AfisYolu FROM Filmler WHERE FilmAdi = @adi",
                        baglanti);
                    cmd.Parameters.AddWithValue("@adi", filmAdi);

                    object yol = cmd.ExecuteScalar();
                    if (yol != null)
                    {
                        string tamYol = Path.Combine(anaYol, yol.ToString());
                        if (File.Exists(tamYol))
                        {
                            pictureBox1.Image = Image.FromFile(tamYol);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                }
            }
            catch { }
        }

        // ===============================
        // DETAY
        // ===============================
        private void button1_Click_1(object sender, EventArgs e)
        {
            forumDetaylar frm = new forumDetaylar();
            frm.VeriAl(label1.Text, pictureBox1.Image);
            frm.button2.Visible = false;
            frm.ShowDialog();
        }

        // ===============================
        // İADE
        // ===============================
        private void button2_Click_1(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(
                "Bileti iade etmek istiyor musunuz?",
                "İade",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
                IadeIsleminiYap();
        }

        private void IadeIsleminiYap()
        {
            using (SqlConnection cnn = new SqlConnection(adres))
            {
                cnn.Open();
                SqlTransaction tr = cnn.BeginTransaction();

                try
                {
                    // 1️⃣ Bilet PASİF
                    SqlCommand cmd1 = new SqlCommand(
                        "UPDATE Biletler SET Durum = 0, KullaniciID = NULL WHERE BiletID = @id",
                        cnn, tr);
                    cmd1.Parameters.AddWithValue("@id", _biletID);
                    cmd1.ExecuteNonQuery();

                    // 2️⃣ Seans koltuk BOŞALT
                    SqlCommand cmd2 = new SqlCommand(
                        "DELETE FROM SeansKoltuklari WHERE SeansID = @s AND KoltukID = @k",
                        cnn, tr);
                    cmd2.Parameters.AddWithValue("@s", _seansID);
                    cmd2.Parameters.AddWithValue("@k", Convert.ToInt32(_koltukNo));
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd3 = new SqlCommand(@"
INSERT INTO TblMuhasebe
(Aciklama, Tutar, Tarih, Kategori, OdemeTuru, IslemiYapan, FaturaNo, Durum)
VALUES
(@a, @t, @tar, @k, @o, @i, @f, @d)
", cnn, tr);

                    cmd3.Parameters.AddWithValue("@a", label1.Text + " - Bilet İadesi");
                    cmd3.Parameters.AddWithValue("@t", 0); // veya iade tutarı
                    cmd3.Parameters.AddWithValue("@tar", DateTime.Now);
                    cmd3.Parameters.AddWithValue("@k", "Bilet Satış");
                    cmd3.Parameters.AddWithValue("@o", "İade");
                    cmd3.Parameters.AddWithValue("@i", "Sistem");
                    cmd3.Parameters.AddWithValue("@f", "IADE-" + _biletID);
                    cmd3.Parameters.AddWithValue("@d", "İade");

                    cmd3.ExecuteNonQuery();

                    tr.Commit();
                    this.Visible = false;
                }
                catch
                {
                    tr.Rollback();
                    MessageBox.Show("İade sırasında hata oluştu.");
                }
            }
        }

        // ===============================
        // BOŞ EVENTLER (AYNEN KORUNDU)
        // ===============================
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void btnIade_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }
}
