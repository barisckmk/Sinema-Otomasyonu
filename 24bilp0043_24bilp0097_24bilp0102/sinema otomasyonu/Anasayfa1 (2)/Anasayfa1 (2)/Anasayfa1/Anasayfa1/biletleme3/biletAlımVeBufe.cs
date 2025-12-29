using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class biletAlımVeBufe : Form
    {
        string cnnStr = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        int bufeOffset = 0;
        int cardWidth = 240;
        private decimal _toplamUcret = 0;
        private string _salonAdi;
        
        private int _seansId;
       
        private void DevamEtButonKontrol()
        {
            bool secimVar = false;

            foreach (BiletCard card in flpBiletTipleri.Controls)
            {
                if (card.Adet > 0)
                {
                    secimVar = true;
                    break;
                }
            }

            if (secimVar)
            {
               
                btnDevamEt.Enabled = true;
                btnDevamEt.BackColor = Color.LimeGreen;
                btnDevamEt.ForeColor = Color.White;
                btnDevamEt.Cursor = Cursors.Hand;
            }
            else
            {
               
                btnDevamEt.Enabled = false;
                btnDevamEt.BackColor = Color.DarkRed;
                btnDevamEt.ForeColor = Color.White;
                btnDevamEt.Cursor = Cursors.Default;
            }
        }

        private int _salonId;
        private int _filmId;
        public biletAlımVeBufe(int filmId, int salonId, string salonAdi, int seansId)
        {
            InitializeComponent();
            this._filmId = filmId; 
            this._salonId = salonId;
            this._salonAdi = salonAdi;
            this._seansId = seansId;
        }


        private void flpBiletTipleri_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BiletTipleriniYukle()
        {
            flpBiletTipleri.Controls.Clear();

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();
                string query = "SELECT BiletTipID, Ad, Fiyat FROM BiletTipleri";

                SqlCommand cmd = new SqlCommand(query, cnn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    BiletCard card = new BiletCard();

                    card.lblBiletAdi.Text = dr["Ad"].ToString();

                    decimal fiyat = Convert.ToDecimal(dr["Fiyat"]);
                    card.lblBiletFiyati.Text = fiyat + " TL";

                    
                    card.AdetDegisti += (s, e) =>
                    {
                        ToplamUcretGuncelle();
                        DevamEtButonKontrol();    
                    };

                    flpBiletTipleri.Controls.Add(card);
                }
            }
        }

        private void biletAlımVeBufe_Load(object sender, EventArgs e)
        {

            BiletTipleriniYukle();
            BufeUrunleriniYukle();

            if (flpBufe.Controls.Count > 0)
            {
                Control first = flpBufe.Controls[0];
                bufeStep = first.Width + first.Margin.Left + first.Margin.Right;
            }

            GuncelleBufeScrollButonlari();

            flpBufe.HorizontalScroll.Maximum = 0;
            flpBufe.HorizontalScroll.Enabled = true;
            flpBufe.HorizontalScroll.Visible = false; 
            flpBufe.VerticalScroll.Visible = false;

            flpBufe.AutoScroll = false;
            

            btnDevamEt.Enabled = false;
            btnDevamEt.BackColor = Color.DarkRed;
            btnDevamEt.ForeColor = Color.White;

            DevamEtButonKontrol();

            pbClose.Image = Properties.Resources.cikis;
            pbBack.Image = Properties.Resources.geri;

        }

        private void BufeUrunleriniYukle()
        {
            flpBufe.Controls.Clear();

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();

                string query = "SELECT UrunID, UrunAdi, Fiyat, ResimYolu FROM BufeUrunleri";
                SqlCommand cmd = new SqlCommand(query, cnn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    BufeCard card = new BufeCard();

                    
                    card.Tag = dr["UrunID"];

                    
                    card.lblUrunAdi.Text = dr["UrunAdi"].ToString();

                    
                    decimal fiyat = Convert.ToDecimal(dr["Fiyat"]);
                    card.UrunFiyati = fiyat;
                    card.lblFiyat.Text = fiyat + " TL";

                    
                    string yol = dr["ResimYolu"].ToString();
                    string tamYol = Path.Combine(Application.StartupPath, yol.Replace("/", "\\"));

                    if (File.Exists(tamYol))
                        card.pbUrunResmi.Image = Image.FromFile(tamYol);

                    
                    card.Margin = new Padding(10, 5, 10, 5);

                    
                    card.AdetDegisti += (s, e) => ToplamUcretGuncelle();

                    flpBufe.Controls.Add(card);
                }
            }
        }

        private void ToplamUcretGuncelle()
        {
            decimal toplam = 0;

            
            foreach (BiletCard card in flpBiletTipleri.Controls)
            {
                if (card.Adet > 0)
                {
                    decimal fiyat = Convert.ToDecimal(card.lblBiletFiyati.Text.Replace(" TL", ""));
                    toplam += fiyat * card.Adet;
                }
            }

            
            foreach (BufeCard card in flpBufe.Controls)
            {
                if (card.Adet > 0)
                {
                    toplam += card.UrunFiyati * card.Adet;
                }
            }

            _toplamUcret = toplam;
            lblToplamUcret.Text = toplam + " TL";
        }

        private void flpBufe_Paint(object sender, PaintEventArgs e)
        {

        }


        int bufeStep = 0;

        private void btnRight_Click(object sender, EventArgs e)
        {
            
            int maxOffset = GetMaxOffset();

            if (bufeOffset < maxOffset)
            {
                bufeOffset += cardWidth;
                if (bufeOffset > maxOffset)
                    bufeOffset = maxOffset;

                flpBufe.AutoScrollPosition = new Point(bufeOffset, 0);
            }

            GuncelleBufeScrollButonlari();

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            
            if (bufeOffset > 0)
            {
                bufeOffset -= cardWidth;
                if (bufeOffset < 0)
                    bufeOffset = 0;

                flpBufe.AutoScrollPosition = new Point(bufeOffset, 0);
            }

            GuncelleBufeScrollButonlari();
        }

        private int GetMaxOffset()
        {
            if (flpBufe.Controls.Count == 0)
                return 0;

            
            int cardWidthWithMargin = flpBufe.Controls[0].Width +
                                      flpBufe.Controls[0].Margin.Left +
                                      flpBufe.Controls[0].Margin.Right;

            int totalWidth = flpBufe.Controls.Count * cardWidthWithMargin;

            int visibleWidth = flpBufe.ClientSize.Width;

            int maxOffset = totalWidth - visibleWidth;

            return maxOffset < 0 ? 0 : maxOffset;
        }



        private void GuncelleBufeScrollButonlari()
        {
            int maxOffset = GetMaxOffset();

            btnLeft.Enabled = bufeOffset > 0;
            btnRight.Enabled = bufeOffset < maxOffset;
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblToplamUcret_Click(object sender, EventArgs e)
        {

        }

        
        private void btnDevamEt_Click(object sender, EventArgs e)
        {
            // 1. Giriş kontrolü
            if (Oturum.KullaniciID <= 0)
            {
                MessageBox.Show("Bilet almak için lütfen önce giriş yapınız!", "Giriş Gerekli", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Giris_Kayit girisForm = new Giris_Kayit();
                girisForm.Show();
                this.Close();
                return;
            }

            
            int biletSayisi = SecilenBiletSayisi(); 
            string bDetay = BiletDetayOlustur();    
            string uDetay = BufeDetayOlustur();    

            
            koltukSecimi frmKoltuk = new koltukSecimi(
                _filmId,
                _salonId,          
                _salonAdi,          
                _seansId,          
                biletSayisi,
                _toplamUcret,       
                bDetay,
                uDetay
            );

            frmKoltuk.Owner = this;

            frmKoltuk.Show();
            this.Hide();
        }


        
        private int SecilenBiletSayisi()
        {
            int toplam = 0;

            foreach (BiletCard card in flpBiletTipleri.Controls)
            {
                toplam += card.Adet;
            }

            return toplam;
        }

        private string BiletDetayOlustur()
        {
            List<string> satirlar = new List<string>();

            foreach (BiletCard card in flpBiletTipleri.Controls)
            {
                if (card.Adet > 0)
                {
                    satirlar.Add($"{card.lblBiletAdi.Text} x{card.Adet}");
                }
            }

            return string.Join(Environment.NewLine, satirlar);
        }

        private string BufeDetayOlustur()
        {
            List<string> satirlar = new List<string>();

            foreach (BufeCard card in flpBufe.Controls)
            {
                if (card.Adet > 0)
                {
                    satirlar.Add($"{card.lblUrunAdi.Text} x{card.Adet}");
                }
            }

            return string.Join(Environment.NewLine, satirlar);
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            var sonuc = MessageBox.Show(
            "Uygulamadan çıkmak istiyor musunuz?",
            "Çıkış",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show(); 
            }

            this.Close();
        }

        private void lblStepText1_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();   
            }

            this.Close();            
        }

        private void lblCircle1_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();   
            }

            this.Close();           
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlToplam_Paint(object sender, PaintEventArgs e)
        {

        }
    }




}
