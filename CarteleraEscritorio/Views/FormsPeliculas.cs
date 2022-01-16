using CarteleraLibrary.Controllers;
using CarteleraLibrary.Models;
using System.Net;

namespace CarteleraEscritorio.Views
{
    public partial class FormsPeliculas : Form
    {
        public FormsPeliculas()
        {
            InitializeComponent();
        }

        private async void FormsPeliculas_Load(object sender, EventArgs e)
        {
            CargaPeliculas();
        }

        private async Task CargaPeliculas()
        {
            PeliculasService peliculasService = new PeliculasService();
            List<PeliculaDto> peliculas = new List<PeliculaDto>();
            peliculas = await peliculasService.GetPeliculas(2);
            foreach (PeliculaDto item in peliculas)
            {
                WebRequest req = WebRequest.Create(item.FullPoster());
                WebResponse res = req.GetResponse();
                Stream imgStream = res.GetResponseStream();
                Image img1 = Image.FromStream(imgStream);

                dataGridView1.Rows.Add(item.Titulo, item.Descripcion, img1);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormNuevaPelicula formProductos = new FormNuevaPelicula();
            formProductos.Show();
            this.Close();
        }
    }
}
