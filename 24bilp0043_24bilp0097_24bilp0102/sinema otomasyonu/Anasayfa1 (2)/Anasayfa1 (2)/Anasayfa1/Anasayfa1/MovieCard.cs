using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class MovieCard : UserControl
    {
        // --- YENİ EKLENEN KISIMLAR (Verileri tutmak için) ---
        public string TasinanFilmAdi { get; private set; }
        public Image TasinanResim { get; private set; }
        // -
        public MovieCard()
        {
            InitializeComponent();
            // 1. Label'ın "ebeveyni"ni yıldız resmi yapıyoruz.
            // (Lütfen 'pictureBox1' yerine kendi yıldız resminin adını yaz)
            label2.Parent = pictureBox1;

            // 2. Arka planı şeffaf yapıyoruz
            label2.BackColor = Color.Transparent;

            // 3. Konum Ayarı (ÖNEMLİ!)
            // Parent değiştiği için label'ın koordinatları artık Form'a göre değil, 
            // resmin sol üst köşesine (0,0) göre hesaplanır.
            // Label'ı resmin tam ortasına getirmek için:
            label2.Location = new Point(
                (pictureBox1.Width - label2.Width) / 2,
                (pictureBox1.Height - label2.Height) / 2 + 2 // +2 biraz aşağı ortalamak için ince ayar
            );

            // --- ŞEFFAFLIK AYARI BİTİŞ ---
        }

        // Bu fonksiyonu çağırarak kartı dolduracağız
        public void SetData(string gelenAd, string gelenSure, string gelenTur, string gelenPuan, Image gelenResim)
        {
            // 1. Yazı ve Görüntü Ayarları
            lblTitle.Text = gelenAd;
            lblTime.Text = "Süre: " + gelenSure;
            lblGenre.Text = gelenTur;

            // --- YENİ EKLENEN KISIM: PUAN ---
            label2.Text = gelenPuan; // Puanı label2'ye yazdırıyoruz
                                     // --------------------------------

            pictureBoxPoster.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPoster.Image = gelenResim;

            // 2. Tıklama Verilerini Hafızaya Al
            TasinanFilmAdi = gelenAd;
            TasinanResim = gelenResim;

            // 3. Tıklama Olaylarını Alt Elemanlara Bağla
            pictureBoxPoster.Click += (s, e) => this.OnClick(e);
            lblTitle.Click += (s, e) => this.OnClick(e);
            lblTime.Click += (s, e) => this.OnClick(e);
            lblGenre.Click += (s, e) => this.OnClick(e);
            label2.Click += (s, e) => this.OnClick(e); // Puanın üzerine tıklanınca da detay açılsın
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MovieCard_Load(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
