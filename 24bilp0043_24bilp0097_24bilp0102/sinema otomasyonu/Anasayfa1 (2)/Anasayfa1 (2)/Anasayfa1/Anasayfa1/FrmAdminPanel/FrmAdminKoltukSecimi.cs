using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

using System.Configuration;

namespace Anasayfa1.FrmAdminPanel
{
    public partial class FrmAdminKoltukSecimi : Form
    {
        // Bağlantı dizesini App.config'den çekiyoruz
        string cnnStr = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

        // Seçilen koltukların ID'lerini ve isimlerini ana forma geri döndüreceğiz
        public List<int> SecilenKoltukIdleri { get; private set; } = new List<int>();
        public List<string> SecilenKoltukIsimleri { get; private set; } = new List<string>();

        private int _salonId;
        private int _seansId;
        private int _biletSayisiLimit; // Seçilebilecek maksimum koltuk sayısı

        // DÜZELTİLEN KISIM: Parantez içine "int biletSayisi" eklendi
        public FrmAdminKoltukSecimi(int salonId, int seansId, int biletSayisi)
        {
            InitializeComponent();
            _salonId = salonId;
            _seansId = seansId;
            _biletSayisiLimit = biletSayisi; // Artık hata vermeyecek
        }

        private void FrmAdminKoltukSecimi_Load(object sender, EventArgs e)
        {
            KoltukYapisiOlustur();
            KoltuklariGetir();
        }

        private void KoltukYapisiOlustur()
        {
            tblKoltuklar.Controls.Clear();
            tblKoltuklar.RowStyles.Clear();
            tblKoltuklar.ColumnStyles.Clear();

            tblKoltuklar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
            for (int i = 0; i < 12; i++)
            {
                tblKoltuklar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
            }
            tblKoltuklar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
        }

        private void KoltuklariGetir()
        {
            string sql = @"
                SELECT k.KoltukID, k.SatirHarf, k.KoltukNo,
                       ISNULL(sk.Durum, 0) AS Durum
                FROM Koltuklar k
                LEFT JOIN SeansKoltuklari sk 
                    ON k.KoltukID = sk.KoltukID AND sk.SeansID = @seansId
                WHERE k.SalonID = @salonId
                ORDER BY k.SatirHarf, k.KoltukNo";

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@salonId", _salonId);
                cmd.Parameters.AddWithValue("@seansId", _seansId);

                try
                {
                    cnn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        string currentRow = null;
                        int currentRowIndex = -1;

                        while (dr.Read())
                        {
                            int kId = dr.GetInt32(0);
                            string harf = dr.GetString(1);
                            int no = dr.GetInt32(2);
                            byte durum = dr.IsDBNull(3) ? (byte)0 : dr.GetByte(3);

                            if (currentRow != harf)
                            {
                                currentRow = harf;
                                currentRowIndex++;
                                tblKoltuklar.Controls.Add(CreateLabel(harf), 0, currentRowIndex);
                                tblKoltuklar.Controls.Add(CreateLabel(harf), 13, currentRowIndex);
                            }

                            Button btn = CreateButton(kId, harf, no, durum);
                            tblKoltuklar.Controls.Add(btn, no, currentRowIndex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koltuklar yüklenirken hata: " + ex.Message);
                }
            }
        }

        private Label CreateLabel(string text)
        {
            return new Label { Text = text, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, ForeColor = Color.Black, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
        }

        private Button CreateButton(int id, string harf, int no, byte durum)
        {
            Button btn = new Button { Size = new Size(45, 35), FlatStyle = FlatStyle.Flat, Text = no.ToString() };
            btn.Tag = new { ID = id, FullName = harf + no };
            btn.Click += Btn_Click;

            if (durum == 1) // Dolu koltuk
            {
                btn.BackColor = Color.DarkRed;
                btn.ForeColor = Color.White;
                btn.Enabled = false;
            }
            else // Boş koltuk
            {
                btn.BackColor = Color.LightGreen;
            }
            return btn;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            dynamic tag = btn.Tag;
            int id = tag.ID;
            string name = tag.FullName;

            if (btn.BackColor == Color.Orange) // Seçimi kaldır
            {
                btn.BackColor = Color.LightGreen;
                SecilenKoltukIdleri.Remove(id);
                SecilenKoltukIsimleri.Remove(name);
            }
            else // Yeni koltuk seç
            {
                // SINIRLAMA: Bilet adedi dolduysa seçtirmez
                if (SecilenKoltukIdleri.Count >= _biletSayisiLimit)
                {
                    MessageBox.Show($"En fazla {_biletSayisiLimit} adet koltuk seçebilirsiniz!", "Sınır", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btn.BackColor = Color.Orange;
                SecilenKoltukIdleri.Add(id);
                SecilenKoltukIsimleri.Add(name);
            }
        }

        // TAMAM / ONAY BUTONU (İsim tasarımınızda neyse onu kullanın, örneğin btnTamam veya btnOnayla)
        private void btnTamam_Click(object sender, EventArgs e)
        {
            if (SecilenKoltukIdleri.Count != _biletSayisiLimit)
            {
                MessageBox.Show($"Lütfen bilet adediniz kadar ({_biletSayisiLimit}) koltuk seçimi yapın.", "Eksik Seçim");
                return;
            }
            this.DialogResult = DialogResult.OK;
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlKoltukAlan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}





