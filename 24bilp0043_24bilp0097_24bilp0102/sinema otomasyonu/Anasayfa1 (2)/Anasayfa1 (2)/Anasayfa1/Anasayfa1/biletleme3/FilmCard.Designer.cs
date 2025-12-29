namespace Anasayfa1
{
    partial class FilmCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbPoster = new System.Windows.Forms.PictureBox();
            this.lblFilmAdi = new System.Windows.Forms.Label();
            this.lblFilmInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPoster
            // 
            this.pbPoster.Location = new System.Drawing.Point(0, 2);
            this.pbPoster.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbPoster.Name = "pbPoster";
            this.pbPoster.Size = new System.Drawing.Size(77, 133);
            this.pbPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPoster.TabIndex = 0;
            this.pbPoster.TabStop = false;
            this.pbPoster.Click += new System.EventHandler(this.pbPoster_Click);
            // 
            // lblFilmAdi
            // 
            this.lblFilmAdi.AutoSize = true;
            this.lblFilmAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFilmAdi.ForeColor = System.Drawing.Color.Black;
            this.lblFilmAdi.Location = new System.Drawing.Point(82, 35);
            this.lblFilmAdi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFilmAdi.Name = "lblFilmAdi";
            this.lblFilmAdi.Size = new System.Drawing.Size(35, 20);
            this.lblFilmAdi.TabIndex = 1;
            this.lblFilmAdi.Text = "text";
            // 
            // lblFilmInfo
            // 
            this.lblFilmInfo.AutoSize = true;
            this.lblFilmInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFilmInfo.ForeColor = System.Drawing.Color.Silver;
            this.lblFilmInfo.Location = new System.Drawing.Point(82, 65);
            this.lblFilmInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFilmInfo.Name = "lblFilmInfo";
            this.lblFilmInfo.Size = new System.Drawing.Size(30, 17);
            this.lblFilmInfo.TabIndex = 2;
            this.lblFilmInfo.Text = "text";
            // 
            // FilmCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblFilmInfo);
            this.Controls.Add(this.lblFilmAdi);
            this.Controls.Add(this.pbPoster);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FilmCard";
            this.Size = new System.Drawing.Size(247, 135);
            this.Load += new System.EventHandler(this.FilmCard_Load);
            this.Click += new System.EventHandler(this.FilmCard_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pbPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPoster;
        private System.Windows.Forms.Label lblFilmAdi;
        private System.Windows.Forms.Label lblFilmInfo;
    }
}
