using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Anasayfa1
{
    public partial class Profilfoto_isim : UserControl
    {
        // ===============================
        // AYARLAR
        // ===============================
        string cnnStr = @"Data Source=.\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True";

        string PosterKlasoru =>
            Path.Combine(Application.StartupPath, "Posterler");

        string DefaultResimYolu =>
            Path.Combine(PosterKlasoru, "userimage.png");

        string KullaniciResimYolu =>
            Path.Combine(PosterKlasoru, $"profile_{Oturum.KullaniciID}.png");

        // ===============================
        // CONSTRUCTOR
        // ===============================
        public Profilfoto_isim()
        {
            InitializeComponent();
            ProfilResminiYukle();
            KullaniciIsminiYukle();
        }

        // ===============================
        // PROFİL RESMİ YÜKLE
        // ===============================
        private void ProfilResminiYukle()
        {
            try
            {
                string yol = DefaultResimYolu;

                if (Oturum.KullaniciID > 0 && File.Exists(KullaniciResimYolu))
                    yol = KullaniciResimYolu;

                if (File.Exists(yol))
                {
                    pictureBox1.Image = Image.FromFile(yol);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch
            {
                // sessiz geç
            }
        }

        // ===============================
        // FOTOĞRAF DEĞİŞTİR
        // ===============================
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Oturum.KullaniciID <= 0)
            {
                MessageBox.Show("Profil fotoğrafı eklemek için giriş yapmalısınız.");
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (!Directory.Exists(PosterKlasoru))
                        Directory.CreateDirectory(PosterKlasoru);

                    File.Copy(ofd.FileName, KullaniciResimYolu, true);
                    ProfilResminiYukle();
                }
                catch
                {
                    MessageBox.Show("Fotoğraf kaydedilemedi.");
                }
            }
        }

        // ===============================
        // KULLANICI İSMİ YÜKLE
        // ===============================
        private void KullaniciIsminiYukle()
        {
            if (Oturum.KullaniciID <= 0)
            {
                label1.Text = "MİSAFİR KULLANICI";
                return;
            }

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ad, soyad FROM Kayit_Omer WHERE id = @id",
                        cnn);
                    cmd.Parameters.AddWithValue("@id", Oturum.KullaniciID);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                        label1.Text = (dr["ad"] + " " + dr["soyad"]).ToString().ToUpper();
                    else
                        label1.Text = "KULLANICI";
                }
                catch
                {
                    label1.Text = "KULLANICI";
                }
            }
        }

        // ===============================
        // BOŞ EVENT
        // ===============================
        private void label1_Click(object sender, EventArgs e) { }
    }
}
