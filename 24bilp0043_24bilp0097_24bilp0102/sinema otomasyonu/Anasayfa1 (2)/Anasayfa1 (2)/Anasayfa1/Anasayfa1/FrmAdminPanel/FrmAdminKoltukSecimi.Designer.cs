namespace Anasayfa1.FrmAdminPanel
{
    partial class FrmAdminKoltukSecimi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdminKoltukSecimi));
            this.btnOnayla = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlKoltukAlan = new System.Windows.Forms.Panel();
            this.flpLegend = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlBosLegend = new System.Windows.Forms.Panel();
            this.lblBosLegend = new System.Windows.Forms.Label();
            this.pnlDoluLegend = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlEngelliLegend = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlSecimLegend = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlPerde = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tblKoltuklar = new System.Windows.Forms.TableLayoutPanel();
            this.flpSatirSag = new System.Windows.Forms.FlowLayoutPanel();
            this.flpSatirSol = new System.Windows.Forms.FlowLayoutPanel();
            this.button5 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlKoltukAlan.SuspendLayout();
            this.flpLegend.SuspendLayout();
            this.pnlPerde.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOnayla
            // 
            this.btnOnayla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOnayla.FlatAppearance.BorderSize = 0;
            this.btnOnayla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnayla.ForeColor = System.Drawing.Color.LawnGreen;
            this.btnOnayla.Location = new System.Drawing.Point(599, 625);
            this.btnOnayla.Name = "btnOnayla";
            this.btnOnayla.Size = new System.Drawing.Size(144, 66);
            this.btnOnayla.TabIndex = 1;
            this.btnOnayla.Text = "Seçimi Onayla";
            this.btnOnayla.UseVisualStyleBackColor = false;
            this.btnOnayla.Click += new System.EventHandler(this.btnTamam_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.pnlKoltukAlan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1382, 703);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pnlKoltukAlan
            // 
            this.pnlKoltukAlan.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlKoltukAlan.Controls.Add(this.button5);
            this.pnlKoltukAlan.Controls.Add(this.btnOnayla);
            this.pnlKoltukAlan.Controls.Add(this.flpLegend);
            this.pnlKoltukAlan.Controls.Add(this.pnlPerde);
            this.pnlKoltukAlan.Controls.Add(this.tblKoltuklar);
            this.pnlKoltukAlan.Controls.Add(this.flpSatirSag);
            this.pnlKoltukAlan.Controls.Add(this.flpSatirSol);
            this.pnlKoltukAlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKoltukAlan.Location = new System.Drawing.Point(0, 0);
            this.pnlKoltukAlan.Name = "pnlKoltukAlan";
            this.pnlKoltukAlan.Size = new System.Drawing.Size(1382, 703);
            this.pnlKoltukAlan.TabIndex = 4;
            this.pnlKoltukAlan.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlKoltukAlan_Paint);
            // 
            // flpLegend
            // 
            this.flpLegend.BackColor = System.Drawing.Color.Transparent;
            this.flpLegend.Controls.Add(this.label3);
            this.flpLegend.Controls.Add(this.pnlBosLegend);
            this.flpLegend.Controls.Add(this.lblBosLegend);
            this.flpLegend.Controls.Add(this.pnlDoluLegend);
            this.flpLegend.Controls.Add(this.label4);
            this.flpLegend.Controls.Add(this.pnlEngelliLegend);
            this.flpLegend.Controls.Add(this.label5);
            this.flpLegend.Controls.Add(this.pnlSecimLegend);
            this.flpLegend.Controls.Add(this.label6);
            this.flpLegend.Location = new System.Drawing.Point(304, 579);
            this.flpLegend.Name = "flpLegend";
            this.flpLegend.Size = new System.Drawing.Size(740, 40);
            this.flpLegend.TabIndex = 4;
            this.flpLegend.WrapContents = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 0);
            this.label3.TabIndex = 1;
            this.label3.Text = "label3";
            // 
            // pnlBosLegend
            // 
            this.pnlBosLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(176)))), ((int)(((byte)(52)))));
            this.pnlBosLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBosLegend.Location = new System.Drawing.Point(9, 9);
            this.pnlBosLegend.Margin = new System.Windows.Forms.Padding(3, 9, 10, 3);
            this.pnlBosLegend.Name = "pnlBosLegend";
            this.pnlBosLegend.Size = new System.Drawing.Size(24, 24);
            this.pnlBosLegend.TabIndex = 6;
            // 
            // lblBosLegend
            // 
            this.lblBosLegend.AutoSize = true;
            this.lblBosLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBosLegend.ForeColor = System.Drawing.Color.Black;
            this.lblBosLegend.Location = new System.Drawing.Point(43, 11);
            this.lblBosLegend.Margin = new System.Windows.Forms.Padding(0, 11, 20, 5);
            this.lblBosLegend.Name = "lblBosLegend";
            this.lblBosLegend.Size = new System.Drawing.Size(110, 18);
            this.lblBosLegend.TabIndex = 7;
            this.lblBosLegend.Text = "Boş Koltuklar";
            // 
            // pnlDoluLegend
            // 
            this.pnlDoluLegend.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlDoluLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDoluLegend.Location = new System.Drawing.Point(176, 9);
            this.pnlDoluLegend.Margin = new System.Windows.Forms.Padding(3, 9, 10, 3);
            this.pnlDoluLegend.Name = "pnlDoluLegend";
            this.pnlDoluLegend.Size = new System.Drawing.Size(24, 24);
            this.pnlDoluLegend.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(210, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 11, 20, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Dolu Koltuklar";
            // 
            // pnlEngelliLegend
            // 
            this.pnlEngelliLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(180)))), ((int)(((byte)(220)))));
            this.pnlEngelliLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEngelliLegend.Location = new System.Drawing.Point(348, 9);
            this.pnlEngelliLegend.Margin = new System.Windows.Forms.Padding(3, 9, 10, 3);
            this.pnlEngelliLegend.Name = "pnlEngelliLegend";
            this.pnlEngelliLegend.Size = new System.Drawing.Size(24, 24);
            this.pnlEngelliLegend.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(382, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 11, 20, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "Engelli Koltukları";
            // 
            // pnlSecimLegend
            // 
            this.pnlSecimLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(158)))), ((int)(((byte)(32)))));
            this.pnlSecimLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSecimLegend.Location = new System.Drawing.Point(539, 9);
            this.pnlSecimLegend.Margin = new System.Windows.Forms.Padding(3, 9, 10, 3);
            this.pnlSecimLegend.Name = "pnlSecimLegend";
            this.pnlSecimLegend.Size = new System.Drawing.Size(24, 24);
            this.pnlSecimLegend.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(573, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 11, 20, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "Seçiminiz";
            // 
            // pnlPerde
            // 
            this.pnlPerde.Controls.Add(this.label2);
            this.pnlPerde.Location = new System.Drawing.Point(219, 37);
            this.pnlPerde.Name = "pnlPerde";
            this.pnlPerde.Size = new System.Drawing.Size(900, 80);
            this.pnlPerde.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(396, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "PERDE";
            // 
            // tblKoltuklar
            // 
            this.tblKoltuklar.BackColor = System.Drawing.Color.Transparent;
            this.tblKoltuklar.ColumnCount = 14;
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblKoltuklar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblKoltuklar.Location = new System.Drawing.Point(232, 144);
            this.tblKoltuklar.Name = "tblKoltuklar";
            this.tblKoltuklar.RowCount = 8;
            this.tblKoltuklar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblKoltuklar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblKoltuklar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblKoltuklar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblKoltuklar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblKoltuklar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblKoltuklar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblKoltuklar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblKoltuklar.Size = new System.Drawing.Size(875, 417);
            this.tblKoltuklar.TabIndex = 2;
            // 
            // flpSatirSag
            // 
            this.flpSatirSag.Location = new System.Drawing.Point(1130, 144);
            this.flpSatirSag.Name = "flpSatirSag";
            this.flpSatirSag.Size = new System.Drawing.Size(40, 260);
            this.flpSatirSag.TabIndex = 1;
            // 
            // flpSatirSol
            // 
            this.flpSatirSol.BackColor = System.Drawing.Color.Transparent;
            this.flpSatirSol.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSatirSol.Location = new System.Drawing.Point(171, 144);
            this.flpSatirSol.Name = "flpSatirSol";
            this.flpSatirSol.Size = new System.Drawing.Size(40, 260);
            this.flpSatirSol.TabIndex = 0;
            this.flpSatirSol.WrapContents = false;
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(1287, 0);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(95, 69);
            this.button5.TabIndex = 7;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // FrmAdminKoltukSecimi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 703);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAdminKoltukSecimi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAdminKoltukSecimi";
            this.Load += new System.EventHandler(this.FrmAdminKoltukSecimi_Load);
            this.panel1.ResumeLayout(false);
            this.pnlKoltukAlan.ResumeLayout(false);
            this.flpLegend.ResumeLayout(false);
            this.flpLegend.PerformLayout();
            this.pnlPerde.ResumeLayout(false);
            this.pnlPerde.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOnayla;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlKoltukAlan;
        private System.Windows.Forms.TableLayoutPanel tblKoltuklar;
        private System.Windows.Forms.FlowLayoutPanel flpSatirSag;
        private System.Windows.Forms.FlowLayoutPanel flpSatirSol;
        private System.Windows.Forms.Panel pnlPerde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flpLegend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlBosLegend;
        private System.Windows.Forms.Label lblBosLegend;
        private System.Windows.Forms.Panel pnlDoluLegend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlEngelliLegend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlSecimLegend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button5;
    }
}