namespace Anasayfa1
{
    partial class SalonCard
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
            this.lblSalonAd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSalonAd
            // 
            this.lblSalonAd.BackColor = System.Drawing.Color.Transparent;
            this.lblSalonAd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSalonAd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSalonAd.Location = new System.Drawing.Point(73, 14);
            this.lblSalonAd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSalonAd.Name = "lblSalonAd";
            this.lblSalonAd.Size = new System.Drawing.Size(106, 22);
            this.lblSalonAd.TabIndex = 0;
            this.lblSalonAd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SalonCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblSalonAd);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "SalonCard";
            this.Size = new System.Drawing.Size(246, 51);
            this.Load += new System.EventHandler(this.SalonCard_Load);
            this.Click += new System.EventHandler(this.SalonCard_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSalonAd;
    }
}
