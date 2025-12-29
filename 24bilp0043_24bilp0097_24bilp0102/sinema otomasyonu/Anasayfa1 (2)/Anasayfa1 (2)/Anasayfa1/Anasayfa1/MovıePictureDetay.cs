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
    public partial class MovıePictureDetay : UserControl
    {
        public MovıePictureDetay()
        {
            InitializeComponent();
        }
        public void BilgiVer(string filmAdi, Image resim)
        {
            // İsim Ayarı
            label1.Text = filmAdi;

            // Resim Ayarı
            if (resim != null)
            {
                pictureBox1.Image = resim;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Resmi sığdır
            }
        }
        // ----------------------

        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
