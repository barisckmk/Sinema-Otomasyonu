using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Anasayfa1
{
    public partial class SalonCard : UserControl
    {
        public int SalonID { get; set; }

        public string SalonAdi
        {
            get => lblSalonAd.Text;
            set => lblSalonAd.Text = value;
        }

        public event EventHandler SalonClicked;
        public SalonCard()
        {
            InitializeComponent();

            // Tıklanabilir hale getir
            this.Click += SalonCard_Click;
            lblSalonAd.Click += SalonCard_Click;
        }


        


        public void SetSelected(bool selected)
        {
            if (selected)
            {
                this.BackColor = Color.FromArgb(154, 205, 50); // yeşil
                lblSalonAd.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = Color.White;
                lblSalonAd.ForeColor = Color.Black;
            }
        }

       

        

        private void SalonCard_Load(object sender, EventArgs e)
        {

        }

        private void SalonCard_Click(object sender, EventArgs e)
        {
            SalonClicked?.Invoke(this, e);
            //SalonCard card = (SalonCard)sender;

           
        }
    }
}
