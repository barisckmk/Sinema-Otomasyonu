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
    public partial class Profil : Form
    {
        public Profil()
        {
            InitializeComponent();

           
        }


        private void ProfilFormu_Load(object sender, EventArgs e)
        {
           
        }
        private void SayfaGetir(UserControl sayfa)
        {
            // 1. Panelin içini temizle (Eski sayfayı kaldır)
            pnlAnaEkran.Controls.Clear();

            // 2. Yeni sayfayı panele yapıştır (Sığdırma Ayarı BURADA)
            sayfa.Dock = DockStyle.Fill;

            // 3. Sayfayı ekle
            pnlAnaEkran.Controls.Add(sayfa);
        }
        private void Profil_Load(object sender, EventArgs e)
        {
            // Eklenen
            UC_Biletlerim biletSayfasi = new UC_Biletlerim();
            SayfaGetir(biletSayfasi);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            UC_Biletlerim biletSayfasi = new UC_Biletlerim();
            SayfaGetir(biletSayfasi);
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UC_Ayarlar ayarlarSayfasi= new UC_Ayarlar();
            SayfaGetir(ayarlarSayfasi);
        }

        private void pnlAnaEkran_Paint(object sender, PaintEventArgs e)
        {

        }

        private void profilfoto_isim1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new Anasayfa().Show();
            this.Close();
        }

        private void uC_Ayarlar1_Load(object sender, EventArgs e)
        {

        }
    }
}
