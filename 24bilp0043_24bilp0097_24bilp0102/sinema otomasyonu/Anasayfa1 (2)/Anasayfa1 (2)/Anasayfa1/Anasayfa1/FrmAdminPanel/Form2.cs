using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        private void FormGetir(Form frm)
        {
            // 1. Panelde zaten bir form varsa onu temizle (üst üste binmesin)
            panel2.Controls.Clear();

            // 2. Yeni formun ayarlarını "Gömülebilir" yap
            frm.TopLevel = false; // Bu form artık ana pencere değil, bir alt parça
            frm.FormBorderStyle = FormBorderStyle.None; // Kenarlıkları ve X tuşunu kaldır
            frm.Dock = DockStyle.Fill; // Panelin tamamını kaplasın

            // 3. Formu panele ekle ve göster
            panel2.Controls.Add(frm);
            frm.Show();
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Büfe formu 
            FrmBufe frm = new FrmBufe();
            FormGetir(frm);
        }



        private void button3_Click(object sender, EventArgs e)
        {
            //bilet satış 
            FrmBiletSatis frm = new FrmBiletSatis();
            FormGetir(frm);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //yönetim
            FrmYönetim frm = new FrmYönetim();
            FormGetir(frm);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FrmMuhasebe frm = new FrmMuhasebe();
            FormGetir(frm);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelYukle(Form frm)
        {
            panel2.Controls.Clear();       // Sağ taraftaki panel adı seninkine göre
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panel2.Controls.Add(frm);
            frm.Show();
        }





        private void FormGetir2(Form frm)
        {
            panel2.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panel2.Controls.Add(frm);
            frm.Show();
        }






        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            FormGetir2(new FrmDashboard());

            // --- GİŞE MEMURU (RolID = 2) ---
            if (Oturum.RolID == 2)
            {
                // 1. Gizlenecek butonlar
                button1.Visible = false; // Büfe
                button2.Visible = false; // Muhasebe (Yeri lazım ama kendisi görünmesin)
                button4.Visible = false; // Yönetim

                // 2. Konum Ayarlamaları (Kaydırma)

                // Bilet Satış (button3) -> En tepeye, Büfe'nin (button1) yerine geçsin
                button3.Location = button1.Location;

                // Tablolar (button6) -> Muhasebe'nin (button2) yerine geçsin
                button6.Location = button2.Location;
            }
            // --- BÜFE MEMURU (RolID = 3) ---
            else if (Oturum.RolID == 3)
            {
                // Sadece Büfe butonu kalsın, diğerlerini gizle
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button6.Visible = false; // Tablolar da gizlensin
            }
            // --- MUHASEBE MEMURU (RolID = 4) ---
            else if (Oturum.RolID == 4)
            {
                // Gereksizleri gizle
                button1.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button6.Visible = false;

                // Muhasebe butonu en üste kaysın
                button2.Location = button1.Location;
            }
        }





            
        
    




        private void button5_Click(object sender, EventArgs e)
        {

            panel2.Controls.Clear();
            FormGetir2(new FrmDashboard());
        }

        // UserControl'ü panele yerleştiren metot

        // UserControl'ü panele yerleştiren metot
        private void UserControlGetir(UserControl uc)
        {
            panel2.Controls.Clear(); // 1. Panelin içini boşalt (Eski sayfayı sil)
            uc.Dock = DockStyle.Fill;     // 2. UserControl panelin tamamını kaplasın
            panel2.Controls.Add(uc); // 3. Panele ekle
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmDegerlendirmeler uc = new FrmDegerlendirmeler();

            // Metoda gönderiyoruz
            UserControlGetir(uc);
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}

