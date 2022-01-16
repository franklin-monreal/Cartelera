using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarteleraLibrary.Controllers;
using CarteleraLibrary.Models;

namespace CarteleraEscritorio.Views
{
    public partial class FormTopPeliculas : Form
    {
        public FormTopPeliculas()
        {
            InitializeComponent();
        }

        private async void FormTopPeliculas_Load(object sender, EventArgs e)
        {

            await CargaPeliculas();


        }

        private   async Task   CargaPeliculas()
        {
            PeliculasService peliculasService = new PeliculasService();
            List<PeliculaApiDto> peliculas = new List<PeliculaApiDto>();
            peliculas = await peliculasService.GetPeliculasPopularesApi();
            foreach (PeliculaApiDto item in peliculas)
            {
                WebRequest req = WebRequest.Create(item.FullPoster());
                WebResponse res = req.GetResponse();
                Stream imgStream = res.GetResponseStream();
                Image img1 = Image.FromStream(imgStream);

                dataGridView1.Rows.Add(item.original_title, item.overview,img1);

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
