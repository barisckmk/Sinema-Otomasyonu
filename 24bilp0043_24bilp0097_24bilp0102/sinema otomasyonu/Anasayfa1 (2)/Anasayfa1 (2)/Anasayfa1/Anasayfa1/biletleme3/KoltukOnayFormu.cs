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
    public partial class KoltukOnayFormu : Form
    {

        private string _salonAdi;
        private string _koltukListesi;
        public KoltukOnayFormu(string salonAdi, string koltukListesi)
        {
            InitializeComponent();

            _salonAdi = salonAdi;
            _koltukListesi = koltukListesi;

            lblSalon.Text = $"🎬 Salon: {salonAdi}";
            lblKoltuklar.Text = $"🎟 Seçilen Koltuklar:\n{koltukListesi}";
        }





        private void pnlBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void KoltukOnayFormu_Load(object sender, EventArgs e)
        {
            lblSalon.UseCompatibleTextRendering = true;
            lblKoltuklar.UseCompatibleTextRendering = true;

            // 🔥 ÇOK ÖNEMLİ AYARLAR
            lblKoltuklar.AutoSize = true;
            lblKoltuklar.MaximumSize = new Size(pnlCenter.Width - 20, 0);

            // Text set
            lblSalon.Text = $" Salon: {_salonAdi}";
            lblKoltuklar.Text = $" Seçilen Koltuklar:\n{_koltukListesi}";




            pnlCenter.Left = (pnlBody.Width - pnlCenter.Width) / 2;
            pnlCenter.Top = (pnlBody.Height - pnlCenter.Height) / 2;
        }
    }
}
