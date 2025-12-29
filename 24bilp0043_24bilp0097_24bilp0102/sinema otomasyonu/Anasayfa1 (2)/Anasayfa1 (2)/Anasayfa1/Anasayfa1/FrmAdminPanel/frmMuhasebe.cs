using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class FrmMuhasebe : Form
    {
        SqlConnection cnn = new SqlConnection(
            "Data Source=.\\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True");

        string belgeYolu = "";

        public FrmMuhasebe()
        {
            InitializeComponent();
            KategorileriYukle();
            OdemeTurleriYukle();
            DurumlariYukle();
            PersonelYukle();
            Listele();
        }

        // ---------------------------------------------
        // COMBOBOX DOLDURMALAR
        // ---------------------------------------------
        void KategorileriYukle()
        {
            cmbKategori.Items.Add("Büfe Satış");
            cmbKategori.Items.Add("Bilet Satış");
            cmbKategori.Items.Add("Online Satış");
            cmbKategori.Items.Add("Maaş");
            cmbKategori.Items.Add("Ofis Gideri");
            cmbKategori.Items.Add("Bakım - Onarım");
        }

        void OdemeTurleriYukle()
        {
            cmbOdemeTuru.Items.Add("Nakit");
            cmbOdemeTuru.Items.Add("Kredi Kartı");
            cmbOdemeTuru.Items.Add("Havale/EFT");
        }

        void DurumlariYukle()
        {
            cmbDurum.Items.Add("Ödendi");
            cmbDurum.Items.Add("Beklemede");
            cmbDurum.Items.Add("İptal");
            cmbDurum.Items.Add("İade");
        }

        void PersonelYukle()
        {
            cmbIslemiYapan.Items.Add("Büfe");
            cmbIslemiYapan.Items.Add("Admin");
            cmbIslemiYapan.Items.Add("Umut");
            cmbIslemiYapan.Items.Add("Muhasebe");
            cmbIslemiYapan.Items.Add("Eleman");

        }

        // ---------------------------------------------
        // TEMİZLE
        // ---------------------------------------------
        void Temizle()
        {
            txtAciklama.Clear();
            
            nudTutar.Value = 0;
            cmbKategori.SelectedIndex = -1;
            cmbDurum.SelectedIndex = -1;
            cmbIslemiYapan.SelectedIndex = -1;
            cmbOdemeTuru.SelectedIndex = -1;
            dtTarih.Value = DateTime.Now;
            
            
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        // ---------------------------------------------
        // BELGE YÜKLE
        // ---------------------------------------------


        // ---------------------------------------------
        // KAYDET
        // ---------------------------------------------
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();

                // 🔥 FilmSeanslari tablosundan fatura bilgisi al
                SqlCommand faturaCmd = new SqlCommand(
                    "SELECT TOP 1 SeansID, Seans FROM FilmSeanslari ORDER BY SeansID DESC", cnn);

                SqlDataReader dr = faturaCmd.ExecuteReader();

                string faturaNo = "";

                if (dr.Read())
                {
                    faturaNo = "SN-" + dr["SeansID"].ToString() + "-" + dr["Seans"].ToString();
                }

                dr.Close();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO TblMuhasebe (Aciklama, Tutar, Tarih, Kategori, OdemeTuru, IslemiYapan, FaturaNo, Durum, BelgeYolu) " +
                    "VALUES (@a,@t,@tar,@k,@o,@i,@f,@d,@b)", cnn);

                cmd.Parameters.AddWithValue("@a", txtAciklama.Text);
                cmd.Parameters.AddWithValue("@t", nudTutar.Value);
                cmd.Parameters.AddWithValue("@tar", dtTarih.Value);
                cmd.Parameters.AddWithValue("@k", cmbKategori.Text);
                cmd.Parameters.AddWithValue("@o", cmbOdemeTuru.Text);
                cmd.Parameters.AddWithValue("@i", cmbIslemiYapan.Text);
                cmd.Parameters.AddWithValue("@f", faturaNo);   // ✅ OTOMATİK GELİYOR
                cmd.Parameters.AddWithValue("@d", cmbDurum.Text);
                cmd.Parameters.AddWithValue("@b", belgeYolu);

                cmd.ExecuteNonQuery();
                cnn.Close();

                MessageBox.Show("Kayıt eklendi!", "Bilgi");
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // ---------------------------------------------
        // LİSTELE
        // ---------------------------------------------
        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }
        
        void Listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TblMuhasebe", cnn);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ---------------------------------------------
        // SATIR SEÇ
        // ---------------------------------------------
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtAciklama.Text = dataGridView1.Rows[e.RowIndex].Cells["Aciklama"].Value.ToString();
            nudTutar.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Tutar"].Value);
            dtTarih.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Tarih"].Value);
            cmbKategori.Text = dataGridView1.Rows[e.RowIndex].Cells["Kategori"].Value.ToString();
            cmbOdemeTuru.Text = dataGridView1.Rows[e.RowIndex].Cells["OdemeTuru"].Value.ToString();
            cmbIslemiYapan.Text = dataGridView1.Rows[e.RowIndex].Cells["IslemiYapan"].Value.ToString();
            
            cmbDurum.Text = dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value.ToString();

            belgeYolu = dataGridView1.Rows[e.RowIndex].Cells["BelgeYolu"].Value.ToString();

           
                
        }

        // ---------------------------------------------
        // GÜNCELLE
        // ---------------------------------------------
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

            try
            {
                cnn.Open();

                // 🔥 Güncel seans bilgisini tekrar al
                SqlCommand faturaCmd = new SqlCommand(
                    "SELECT TOP 1 SeansID, Seans FROM FilmSeanslari ORDER BY SeansID DESC", cnn);

                SqlDataReader dr = faturaCmd.ExecuteReader();

                string faturaNo = "";

                if (dr.Read())
                {
                    faturaNo = "SN-" + dr["SeansID"].ToString() + "-" + dr["Seans"].ToString();
                }

                dr.Close();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE TblMuhasebe SET Aciklama=@a, Tutar=@t, Tarih=@tar, Kategori=@k, " +
                    "OdemeTuru=@o, IslemiYapan=@i, FaturaNo=@f, Durum=@d, BelgeYolu=@b WHERE Id=@id", cnn);

                cmd.Parameters.AddWithValue("@a", txtAciklama.Text);
                cmd.Parameters.AddWithValue("@t", nudTutar.Value);
                cmd.Parameters.AddWithValue("@tar", dtTarih.Value);
                cmd.Parameters.AddWithValue("@k", cmbKategori.Text);
                cmd.Parameters.AddWithValue("@o", cmbOdemeTuru.Text);
                cmd.Parameters.AddWithValue("@i", cmbIslemiYapan.Text);
                cmd.Parameters.AddWithValue("@f", faturaNo);   // ✅ BURADAN
                cmd.Parameters.AddWithValue("@d", cmbDurum.Text);
                cmd.Parameters.AddWithValue("@b", belgeYolu);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                cnn.Close();

                MessageBox.Show("Kayıt güncellendi!", "Bilgi");
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void txtAciklama_TextChanged(object sender, EventArgs e)
        {
            // burada bir şey yapmak zorunda değilsin, boş kalabilir
        }

        private void cmbOdemeTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void cmbDurum_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private void cmbIslemiYapan_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtFaturaNo_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            btnGuncelle_Click(sender, e);
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            btnSil_Click(sender, e);
        }

        private void btnTemizle_Click_1(object sender, EventArgs e)
        {
            Temizle();
            MessageBox.Show("Form temizlendi.", "Bilgi");
        }

        private void btnListele_Click_1(object sender, EventArgs e)
        {
            Listele();
            MessageBox.Show("Kayıtlar listelendi.", "Bilgi");

        }




        // ---------------------------------------------
        // SİL
        // ---------------------------------------------
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM TblMuhasebe WHERE Id=@id", cnn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cnn.Close();

                MessageBox.Show("Kayıt silindi!", "Bilgi");
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmMuhasebe_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Listele();
            MessageBox.Show("Kayıtlar listelendi.", "Bilgi");
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
