using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Configuration;

namespace Anasayfa1
{
    public partial class Form1 : Form
    {

        private string seciliSalonAdi = "";  
        SalonCard seciliSalon = null;
        FilmCard seciliFilm = null;
        
        string cnnStr = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
        public string OtomatikSecilecekFilm { get; set; } = "";
        public Form1()
        {
            InitializeComponent();

        }
        /*

        private void MakeOvalButton(Button btn)
        {
            
            int radius = btn.Height;  // 40 → oval için ideal

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 90, 180);
            path.AddArc(new Rectangle(btn.Width - radius, 0, radius, radius), 270, 180);
            path.CloseFigure();

            btn.Region = new Region(path);

        }
        */


        private void DevamEtButonDurumunuAyarla(bool aktif)
        {
            btnDevamEt.Enabled = aktif;

            if (aktif)
            {
                btnDevamEt.BackColor = Color.FromArgb(154, 205, 50); // yeşil
                btnDevamEt.ForeColor = Color.White;
                btnDevamEt.Cursor = Cursors.Hand;
            }
            else
            {
                btnDevamEt.BackColor = Color.DarkRed;
                btnDevamEt.ForeColor = Color.White;
                btnDevamEt.Cursor = Cursors.Default;
            }
        }


        private void FilmleriYukle()
        {

            flpFilmler.Controls.Clear();

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("SELECT FilmID, FilmAdi, Sure, Tur, AfisYolu FROM Filmler WHERE Durum = 1", cnn);
                SqlDataReader dr = cmd.ExecuteReader();

                // --- SEÇİM MANTIĞI (Burayı ayırdık) ---
                // basaTasi: true ise kartı en başa alır, false ise yerinde bırakır.
                Action<FilmCard, bool> FilmSecimIslemi = (card, basaTasi) =>
                {
                    if (seciliFilm != null)
                        seciliFilm.SetSelected(false);

                    seciliFilm = card;
                    seciliFilm.SetSelected(true);

                    // ⭐ KRİTİK NOKTA: Sadece 'basaTasi' true gelirse (Detaydan gelince) başa al.
                    // Elle tıklayınca burası çalışmaz.
                    if (basaTasi)
                    {
                        flpFilmler.Controls.SetChildIndex(card, 0);
                        // Kaydırma işlemi bazen hata verdirebilir, güvenli olması için try-catch içinde:
                        try { flpFilmler.ScrollControlIntoView(card); } catch { }
                    }

                    // --- Diğer Temizlik İşlemleri ---
                    if (seciliTarihBtn != null)
                    {
                        seciliTarihBtn.BackColor = Color.White;
                        seciliTarihBtn.ForeColor = Color.LawnGreen;
                    }
                    seciliTarihBtn = null;
                    seciliTarihID = 0;

                    btnTarih1.Visible = false;
                    btnTarih2.Visible = false;
                    btnTarih3.Visible = false;
                    btnTarih1.Text = "";
                    btnTarih2.Text = "";
                    btnTarih3.Text = "";

                    flpSeanslar.Controls.Clear();
                    DevamEtButonDurumunuAyarla(false);
                    seciliSeansButon = null;
                    seciliSeans = "";

                    SalonlariYukle(seciliFilm.FilmID);
                };
                // -------------------------------------

                while (dr.Read())
                {
                    FilmCard card = new FilmCard();

                    card.FilmID = dr.GetInt32(0);
                    card.FilmAdi = dr.GetString(1);
                    card.FilmInfo = dr.GetString(2) + " - " + dr.GetString(3);

                    string afisYolu = dr.GetString(4);
                    string tamYol = Path.Combine(Application.StartupPath, afisYolu);

                    if (File.Exists(tamYol))
                        card.Poster = Image.FromFile(tamYol);

                    // 👇 SEN ELİNLE TIKLARSAN: false gönderiyoruz (YERİNDEN OYNAMAZ)
                    card.CardClicked += (s, e) => FilmSecimIslemi(card, false);

                    flpFilmler.Controls.Add(card);

                    // 🔥 DETAY SAYFASINDAN GELİNCE: true gönderiyoruz (EN BAŞA TAŞINIR)
                    if (!string.IsNullOrEmpty(OtomatikSecilecekFilm) && card.FilmAdi == OtomatikSecilecekFilm)
                    {
                        FilmSecimIslemi(card, true);
                    }
                }
            }
        }

        private void SalonlariYukle(int filmID)
        {
            
            flpSalonlar.Controls.Clear();

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand(@"
                SELECT DISTINCT s.SalonID, s.SalonAdi
                FROM FilmSeanslari fs
                INNER JOIN Salonlar s ON fs.SalonID = s.SalonID
                WHERE fs.FilmID = @filmID
                ", cnn);

                cmd.Parameters.AddWithValue("@filmID", filmID); 


                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int salonID = dr.GetInt32(0);
                    string salonAdi = dr.GetString(1);
                                 
                   
                    SalonCard sc = new SalonCard();
                    sc.SalonID = salonID;
                    sc.SalonAdi = salonAdi;

                    sc.SetSelected(false); 

                    sc.SalonClicked += (s, e) =>
                    {
                        // 🔥 TARİH RESET
                        if (seciliTarihBtn != null)
                        {
                            seciliTarihBtn.BackColor = Color.White;
                            seciliTarihBtn.ForeColor = Color.LawnGreen;
                        }
                        seciliTarihBtn = null;
                        seciliTarihID = 0;

                        // 🔥 SEANS RESET
                        if (seciliSeansButon != null)
                        {
                            seciliSeansButon.BackColor = Color.LightGray;
                            seciliSeansButon.ForeColor = Color.Black;
                        }
                        seciliSeansButon = null;
                        seciliSeans = "";

                        // 🔥 SEANS PANELİ TEMİZLE
                        flpSeanslar.Controls.Clear();

                        // 🔴 DEVAM ET PASİF
                        DevamEtButonDurumunuAyarla(false);

                        // =========================
                        // ⬇️ SENİN KODLARIN (AYNEN)
                        // =========================

                        if (seciliSalon != null)
                            seciliSalon.SetSelected(false);

                        seciliSalon = sc;
                        seciliSalon.SetSelected(true);
                        seciliSalonAdi = sc.SalonAdi; // ✔ DB’den gelen gerçek salon adı

                        btnTarih1.Visible = true;
                        btnTarih2.Visible = true;
                        btnTarih3.Visible = true;

                        DevamEtButonDurumunuAyarla(false); // 🔴 seans yok → kapalı

                        // Tarihleri tekrar yükle
                        TarihleriYukle();
                    };

                    flpSalonlar.Controls.Add(sc);
                }
            } 
            
        }

        private void TarihleriYukle(int salonID)
        {
            
        }

        private void MakeCircle(Label lbl)
        {
            
        }

        private void lblLogoText_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //MakeOvalButton(btnDevamEt);

            FilmleriYukle();
            pbBack.Image = Properties.Resources.geri;
            btnTarih1.Visible = false;
            btnTarih2.Visible = false;
            btnTarih3.Visible = false;

            btnTarih1.Click += TarihButonu_Click;
            btnTarih2.Click += TarihButonu_Click;
            btnTarih3.Click += TarihButonu_Click;

            DevamEtButonDurumunuAyarla(false); // 🔴 başlangıç: pasif + darkred

            pbClose.Image = Properties.Resources.cikis;

        }

        private string GunAdi(DateTime tarih) 
        {
            return tarih.ToString("dddd", new CultureInfo("tr-TR"));
        }




        private List<(int ID, DateTime Tarih)> tarihler = new List<(int, DateTime)>();

        private void TarihleriYukle()
        {
            tarihler.Clear();

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("SELECT TarihID, Tarih FROM SeansTarihleri ORDER BY Tarih", cnn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    tarihler.Add((dr.GetInt32(0), dr.GetDateTime(1)));
                }
            }

            // Butonlara tarih yaz
            btnTarih1.Text = tarihler[0].Tarih.ToString("dd MMMM") + "\n" + GunAdi(tarihler[0].Tarih);
            btnTarih1.Tag = tarihler[0].ID;

            btnTarih2.Text = tarihler[1].Tarih.ToString("dd MMMM") + "\n" + GunAdi(tarihler[1].Tarih);
            btnTarih2.Tag = tarihler[1].ID;

            btnTarih3.Text = tarihler[2].Tarih.ToString("dd MMMM") + "\n" + GunAdi(tarihler[2].Tarih);
            btnTarih3.Tag = tarihler[2].ID;

            TarihButonlariniVarsayilanYap();
        }


        private Button seciliTarihBtn = null;
        private int seciliTarihID = 0;

        private void TarihButonu_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            

            // 🔁 Önce HERKESİ default renge al
            TarihButonlariniVarsayilanYap();

            // 🔥 Seçilen buton
            seciliTarihBtn = btn;
            btn.BackColor = Color.FromArgb(154, 205, 50);
            btn.ForeColor = Color.White;

            DevamEtButonDurumunuAyarla(false);

            seciliTarihID = (int)btn.Tag;

            SeanslariYukle(seciliSalon.SalonID, seciliFilm.FilmID, seciliTarihID);

            // Eski filmin tarihi seçili kalmasın diye
            seciliTarihBtn = null;
            seciliTarihID = 0;

            // Yeni seçilen buton
            seciliTarihBtn = btn;
            btn.BackColor = Color.FromArgb(154, 205, 50);
            btn.ForeColor = Color.White;

            //btnDevamEt.Enabled = false;
            DevamEtButonDurumunuAyarla(false);

            // Seçilen TarihID
            seciliTarihID = (int)btn.Tag;

            // Bu tarih için seansları yükle
            SeanslariYukle(seciliSalon.SalonID, seciliFilm.FilmID, seciliTarihID);
           
        }

        private Button seciliSeansButon = null;
        private string seciliSeans = "";

        private void SeanslariYukle(int salonID, int filmID, int tarihID)
        {
            flpSeanslar.Controls.Clear();

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand(@"
                SELECT Seans 
                FROM FilmSeanslari
                WHERE SalonID = @salonID AND FilmID = @filmID AND TarihID = @tarihID
                ORDER BY Seans
            ", cnn);

                cmd.Parameters.AddWithValue("@salonID", salonID);
                cmd.Parameters.AddWithValue("@filmID", filmID);
                cmd.Parameters.AddWithValue("@tarihID", tarihID);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string saat = dr.GetString(0);

                    Button btn = new Button();
                    btn.Width = 80;
                    btn.Height = 40;
                    btn.Text = saat;
                    btn.BackColor = Color.LightGray;
                    btn.ForeColor = Color.Black;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Tag = saat;
                    btn.Cursor = Cursors.Hand;

                    btn.Click += SeansButonu_Click;

                    flpSeanslar.Controls.Add(btn);
                }
            }

        }

        private void SeansButonu_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (seciliSeansButon != null)
            {
                seciliSeansButon.BackColor = Color.LightGray;
                seciliSeansButon.ForeColor = Color.Black;
            }

            seciliSeansButon = btn;
            seciliSeansButon.BackColor = Color.FromArgb(154, 205, 50); // yeşil
            seciliSeansButon.ForeColor = Color.White;

            seciliSeans = btn.Tag.ToString();



            DevamEtButonDurumunuAyarla(true);


        }

        private int GetSeansID(int salonID, int filmID, int tarihID, string saat)
        {
            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand(@"
            SELECT SeansID
            FROM FilmSeanslari
            WHERE SalonID = @salonID
              AND FilmID = @filmID
              AND TarihID = @tarihID
              AND Seans = @saat
        ", cnn);

                cmd.Parameters.AddWithValue("@salonID", salonID);
                cmd.Parameters.AddWithValue("@filmID", filmID);
                cmd.Parameters.AddWithValue("@tarihID", tarihID);
                cmd.Parameters.AddWithValue("@saat", saat);

                return (int)cmd.ExecuteScalar();
            }
        }
        
        private void btnTarih1_Click(object sender, EventArgs e)
        {

        }

        private void btnDevamEt_Click(object sender, EventArgs e)
        {
            int seansId = GetSeansID(seciliSalon.SalonID, seciliFilm.FilmID, seciliTarihID, seciliSeans);

            // SIRALAMA DÜZELTİLDİ: Önce FilmID, sonra SalonID
            biletAlımVeBufe frm = new biletAlımVeBufe(
                seciliFilm.FilmID,    // 1. filmId
                seciliSalon.SalonID,  // 2. salonId
                seciliSalonAdi,       // 3. salonAdi
                seansId               // 4. seansId
            );

            frm.Owner = this;
            frm.Show();
            this.Hide();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
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

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            // HATALI OLAN KISIM: Anasayfa anaSayfaFormu = new Anasayfa(); (BUNU YAPMA)

            // DOĞRUSU: Arka planda zaten açık (gizli) olan Anasayfayı bul
            Form anaSayfa = Application.OpenForms["Anasayfa"];

            if (anaSayfa != null)
            {
                anaSayfa.Show(); // Gizli olanı göster
            }
            else
            {
                // Çok düşük ihtimal ama eğer anasayfa kazara kapandıysa yenisini oluştur
                Anasayfa yeniAnaSayfa = new Anasayfa();
                yeniAnaSayfa.Show();
            }

            // ÖNEMLİ: Kendini Hide() yapma, Close() yap.
            // Close yapmazsan RAM'de birikir ve tekrar açtığında eski seçimler kalır.
            this.Close();
        }

        private void btnTarih2_Click(object sender, EventArgs e)
        {

        }

        private void btnTarih3_Click(object sender, EventArgs e)
        {

        }

        private void flpFilmler_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TarihButonlariniVarsayilanYap()
        {
            Button[] butonlar = { btnTarih1, btnTarih2, btnTarih3 };

            foreach (Button btn in butonlar)
            {
                btn.BackColor = Color.FromArgb(230, 245, 230); // açık yeşil
                btn.ForeColor = Color.FromArgb(70, 130, 70);   // koyu yeşil yazı
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = Color.FromArgb(154, 205, 50);
                btn.FlatAppearance.BorderSize = 1;
                btn.Cursor = Cursors.Hand;
            }
        }

        private void flpSeanslar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
