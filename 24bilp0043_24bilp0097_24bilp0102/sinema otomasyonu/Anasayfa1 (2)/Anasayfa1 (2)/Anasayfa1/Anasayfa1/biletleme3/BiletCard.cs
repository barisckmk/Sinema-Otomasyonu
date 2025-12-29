using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class BiletCard : UserControl
    {
        public string BiletAdi
        {
            get => lblBiletAdi.Text;
            set => lblBiletAdi.Text = value;
        }

        public string BiletFiyati
        {
            get => lblBiletFiyati.Text;
            set => lblBiletFiyati.Text = value;
        }

        public int Adet { get; private set; } = 0;

        public BiletCard()
        {
            InitializeComponent();

           
        }

        private void BiletCard_Load(object sender, EventArgs e)
        {

        }

        public event EventHandler AdetDegisti;

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (Adet > 0)
            {
                Adet--;
                lblAdet.Text = Adet.ToString();

                // Form1'e haber gönder
                AdetDegisti?.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
           
     
        
        }

        private void btnPlus_Click_1(object sender, EventArgs e)
        {
            Adet++;
            lblAdet.Text = Adet.ToString();

            // Form1'e haber gönder
            AdetDegisti?.Invoke(this, EventArgs.Empty);
        }

        private void lblBiletFiyati_Click(object sender, EventArgs e)
        {

        }
    }
}
