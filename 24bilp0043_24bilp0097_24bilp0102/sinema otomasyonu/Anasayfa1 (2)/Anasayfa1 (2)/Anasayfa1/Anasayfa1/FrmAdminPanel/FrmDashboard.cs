using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }

        // Designer'da tanımlı olan Çıkış butonu olayı
        private void button1_Click(object sender, EventArgs e)
        {
           

           // this.Hide();

            Form anaAdminPaneli = Application.OpenForms["Form2"];

            if (anaAdminPaneli != null)
            {
                anaAdminPaneli.Close(); 
                Application.Exit();
            }
        }

        
        private void panelTop_Paint(object sender, PaintEventArgs e)
        {
            // Panel çizim kodları buraya (gerekliyse)
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {

            // Bağlantı adresi (Diğerleriyle aynı)
            string baglantiAdresi = @"Data Source=.\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True";

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                try
                {
                    baglanti.Open();

                    
                  
                }
                catch
                {
                    // Hata olursa boş geç
                }
            }

        }







        private void card1_Paint(object sender, PaintEventArgs e)
        {
            // Kart çizim kodları buraya (gerekliyse)
        }

        private void FrmDashboard_Load_1(object sender, EventArgs e)
        {

        }
    }
}