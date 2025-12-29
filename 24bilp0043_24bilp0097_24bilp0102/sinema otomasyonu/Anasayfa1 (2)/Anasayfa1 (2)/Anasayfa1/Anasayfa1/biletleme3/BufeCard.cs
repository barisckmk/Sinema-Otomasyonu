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
    public partial class BufeCard : UserControl
    {
        public int Adet { get; private set; } = 0;
        public decimal UrunFiyati { get; set; }

        // Form1'e haber göndermemizi sağlayan event
        public event EventHandler AdetDegisti;


        public BufeCard()
        {
            InitializeComponent();

            lblAdett.Text = "0";
           
        }

        private void BufeCard_Load(object sender, EventArgs e)
        {

        }

        private void btnPluss_Click(object sender, EventArgs e)
        {
            Adet++;
            lblAdett.Text = Adet.ToString();
            AdetDegisti?.Invoke(this, EventArgs.Empty);
        }

        private void btnMinuss_Click(object sender, EventArgs e)
        {
            if (Adet > 0)
            {
                Adet--;
                lblAdett.Text = Adet.ToString();
                AdetDegisti?.Invoke(this, EventArgs.Empty);
            }
        }
        private void lblFiyat_Click(object sender, EventArgs e)
        {

        }
    }
}
