using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Anasayfa1
{
    public partial class FrmBufe : Form
    {
        SqlConnection cnn = new SqlConnection(
            "Data Source=.\\SQLEXPRESS01;Initial Catalog=IsikSinemaDB2;Integrated Security=True");

        DataTable BufeUrunleri = new DataTable();
        decimal toplamTutar = 0;

        public FrmBufe()
        {
            InitializeComponent();
        }

        private void FrmBufe_Load(object sender, EventArgs e)
        {
            // Sepet listesi ayarları
            dgvSepet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSepet.MultiSelect = false;

            // Adet seçici başlangıçta 1 olsun ki çarpma işlemi 0 çıkmasın
            if (nudAdet.Value == 0) nudAdet.Value = 1;

            UrunleriYukle();
        }

        private void UrunleriYukle()
        {
            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT UrunID, UrunAdi, Fiyat FROM BufeUrunleri", cnn);
                da.Fill(BufeUrunleri);

                cmbUrun.DataSource = BufeUrunleri;
                cmbUrun.DisplayMember = "UrunAdi";
                cmbUrun.ValueMember = "UrunID";

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenemedi: " + ex.Message);
            }
        }

        // --- ORTAK HESAPLAMA METODU ---
        // Hem ürün değişince hem adet değişince bunu çağıracağız
        private void FiyatiGuncelle()
        {
            if (cmbUrun.SelectedIndex >= 0)
            {
                try
                {
                    DataRowView row = (DataRowView)cmbUrun.SelectedItem;
                    decimal birimFiyat = Convert.ToDecimal(row["Fiyat"]);
                    int adet = (int)nudAdet.Value;

                    // Anlık olarak (Birim Fiyat x Adet) hesaplayıp göster
                    decimal guncelTutar = birimFiyat * adet;
                    lblFiyat.Text = guncelTutar.ToString("0.00") + " TL";
                }
                catch
                {
                    // Sayısal hata olursa varsayılan
                    lblFiyat.Text = "0.00 TL";
                }
            }
        }

        private void cmbUrun_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ürün seçilince fiyatı güncelle
            FiyatiGuncelle();
        }

        // --- YENİ EKLENEN KISIM: ADET DEĞİŞİNCE ---
        // Tasarım ekranında nudAdet'e çift tıklayıp bu kodu içine yapıştırabilirsin
        // Veya Events (Şimşek) kısmından ValueChanged olayına bu metodu seçmelisin.
       

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (cmbUrun.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen bir ürün seç, Kral.");
                return;
            }

            if (nudAdet.Value <= 0)
            {
                MessageBox.Show("Lütfen en az 1 adet seç, Kral.");
                return;
            }

            DataRowView row = (DataRowView)cmbUrun.SelectedItem;

            string urunAdi = row["UrunAdi"].ToString();
            decimal birimFiyat = Convert.ToDecimal(row["Fiyat"]);
            int adet = (int)nudAdet.Value;

            decimal tutar = birimFiyat * adet;
            toplamTutar += tutar;

            dgvSepet.Rows.Add(urunAdi, adet, tutar.ToString("0.00"));

            lblToplam.Text = "Toplam: " + toplamTutar.ToString("0.00") + " TL";
        }

        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            if (dgvSepet.Rows.Count == 0)
            {
                MessageBox.Show("Sepet boş Kral.");
                return;
            }

            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();

                foreach (DataGridViewRow row in dgvSepet.Rows)
                {
                    string urunAdi = row.Cells[0].Value.ToString();
                    int adet = Convert.ToInt32(row.Cells[1].Value);
                    decimal tutar = Convert.ToDecimal(row.Cells[2].Value);

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO BufeSatis (UrunAdi, Adet, Tutar, Tarih) VALUES (@UrunAdi, @Adet, @Tutar, @Tarih)",
                        cnn
                    );

                    cmd.Parameters.AddWithValue("@UrunAdi", urunAdi);
                    cmd.Parameters.AddWithValue("@Adet", adet);
                    cmd.Parameters.AddWithValue("@Tutar", tutar);
                    cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }

                string muhasebeSorgu = @"INSERT INTO TblMuhasebe 
                                (Aciklama, Tutar, Tarih, Kategori, OdemeTuru, IslemiYapan, Durum) 
                                VALUES (@a, @t, @tar, @k, @o, @i, @d)";

                SqlCommand cmdMuhasebe = new SqlCommand(muhasebeSorgu, cnn);

                cmdMuhasebe.Parameters.AddWithValue("@a", "Toplu Büfe Satışı");
                cmdMuhasebe.Parameters.AddWithValue("@t", toplamTutar);
                cmdMuhasebe.Parameters.AddWithValue("@tar", DateTime.Now);
                cmdMuhasebe.Parameters.AddWithValue("@k", "Büfe Satış");
                cmdMuhasebe.Parameters.AddWithValue("@o", "Nakit");
                cmdMuhasebe.Parameters.AddWithValue("@i", "Büfe");
                cmdMuhasebe.Parameters.AddWithValue("@d", "Ödendi");

                cmdMuhasebe.ExecuteNonQuery();

                cnn.Close();

                MessageBox.Show("Satış tamamlandı Kral!");

                dgvSepet.Rows.Clear();
                toplamTutar = 0;
                lblToplam.Text = "Toplam: 0 TL";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Satış sırasında hata oluştu: " + ex.Message);
            }
        }

        // --- SİLME BUTONU (button1) ---
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvSepet.SelectedRows.Count > 0)
            {
                DataGridViewRow seciliSatir = dgvSepet.SelectedRows[0];
                decimal silinecekTutar = Convert.ToDecimal(seciliSatir.Cells[2].Value);

                toplamTutar -= silinecekTutar;

                dgvSepet.Rows.Remove(seciliSatir);

                lblToplam.Text = "Toplam: " + toplamTutar.ToString("0.00") + " TL";

                MessageBox.Show("Ürün sepetten kaldırıldı.");
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz satırı yanındaki gri kutucuğa tıklayarak seçin, Kral.");
            }
        }

        private void lblFiyat_Click(object sender, EventArgs e)
        {

        }

        private void nudAdet_ValueChanged_1(object sender, EventArgs e)
        {
            // Adet artıp azaldıkça fiyatı anlık güncelle
            FiyatiGuncelle();
        }
    }
}