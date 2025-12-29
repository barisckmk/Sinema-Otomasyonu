using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
    
namespace Anasayfa1
{
    public partial class MevcutKartlar : UserControl
    {
        public MevcutKartlar()
        {
            InitializeComponent();
            // Tasarım ekranında hata vermemesi için koruma
            if (!this.DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ResmiYukle();
            }
        }
        private void ResmiYukle()
        {
            try
            {
                // Artık "Posterler" klasörünün içine bakıyoruz
                string anaKlasor = Application.StartupPath; // bin\Debug
                string posterKlasoru = Path.Combine(anaKlasor, "Posterler");

                string dosyaAdi = "kredikartkapak"; // Resim ismi
                string yuklenecekDosya = "";

                // 1. İhtimal: PNG mi?
                if (File.Exists(Path.Combine(posterKlasoru, dosyaAdi + ".png")))
                {
                    yuklenecekDosya = Path.Combine(posterKlasoru, dosyaAdi + ".png");
                }
                // 2. İhtimal: JPG mi?
                else if (File.Exists(Path.Combine(posterKlasoru, dosyaAdi + ".jpg")))
                {
                    yuklenecekDosya = Path.Combine(posterKlasoru, dosyaAdi + ".jpg");
                }

                // Dosya bulunduysa yükle
                if (!string.IsNullOrEmpty(yuklenecekDosya))
                {
                    pictureBox1.Image = Image.FromFile(yuklenecekDosya);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch
            {
                // Sessizce geç
            }
        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
