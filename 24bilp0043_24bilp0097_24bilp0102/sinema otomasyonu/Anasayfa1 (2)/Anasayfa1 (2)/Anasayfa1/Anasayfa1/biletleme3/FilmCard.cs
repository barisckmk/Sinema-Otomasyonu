using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Anasayfa1
{
    public partial class FilmCard : UserControl
    {

        public int FilmID { get; set; }

        public string FilmAdi
        {
            get => lblFilmAdi.Text;
            set => lblFilmAdi.Text = value;
        }

        public string FilmInfo
        {
            get => lblFilmInfo.Text;
            set => lblFilmInfo.Text = value;
        }

        public Image Poster
        {
            get => pbPoster.Image;
            set => pbPoster.Image = value;
        }

        public FilmCard()
        {
            InitializeComponent();
            this.Click += FilmCard_Click;
            foreach (Control c in this.Controls)
                c.Click += FilmCard_Click;
        }

        private void FilmCard_Load(object sender, EventArgs e)
        {

        }


        public event EventHandler FilmSecildi;
        private void FilmCard_Click(object sender, EventArgs e)
        {
            CardClicked?.Invoke(this, EventArgs.Empty);
            
        }

        public event EventHandler CardClicked;

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                this.BackColor = Color.FromArgb(154, 205, 50); // yeşil
                lblFilmAdi.ForeColor = Color.White;
                lblFilmInfo.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = Color.White;
                lblFilmAdi.ForeColor = Color.Black;
                lblFilmInfo.ForeColor = Color.Black;
            }
        }

        private void pbPoster_Click(object sender, EventArgs e)
        {

        }
    }
}







        

        
