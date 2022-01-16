using CarteleraLibrary.Controllers;
using CarteleraLibrary.Models;
using System.Net;

namespace CarteleraEscritorio.Views
{
    public partial class FormNuevaPelicula : Form
    {
        private List<PeliculaApiDto> peliculas;
        private readonly PeliculasService peliculasService = new();

        private readonly PeliculaDto pelicula = new();
        public FormNuevaPelicula()
        {
            InitializeComponent();
            pelicula = new PeliculaDto();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private async Task CargaDatos()
        {
            peliculas = new List<PeliculaApiDto>();
            peliculas = await peliculasService.GetPeliculasSearchApi(txtTitulo.Text);
            dataGridView1.Rows.Clear();
            foreach (PeliculaApiDto item in peliculas)
            {

                WebRequest req = WebRequest.Create(item.FullPoster());
                WebResponse res = req.GetResponse();
                Stream imgStream = res.GetResponseStream();
                Image img1 = Image.FromStream(imgStream);

                dataGridView1.Rows.Add(item.id, item.original_title, img1);

            }
        }

        private void FormNuevaPelicula_Load(object sender, EventArgs e)
        {
        }

        private void TxtTitulo_Leave(object sender, EventArgs e)
        {
            CargaDatos();
            int Index = dataGridView1.CurrentCell.RowIndex;
            if (Index > 0)
            {
                dataGridView1.Rows[Index].Selected = false;
            }

        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int Index = dataGridView1.CurrentCell.RowIndex;
                int id = (int)dataGridView1.Rows[Index].Cells[0].Value;
                PeliculaApiDto pel = peliculas.FirstOrDefault(x => x.id == id);
                pelicula.ExternalId = pel.id;
                pelicula.Titulo = pel.original_title;
                pelicula.Descripcion = pel.overview;
                pelicula.ImagenPath = pel.poster_path;
                pelicula.PuntuacionPromedio = pel.vote_average;

                txtTitulo.Text = pelicula.Titulo;
                txtDescripcion.Text = pelicula.Descripcion;
            }
        }

        private async Task Guardar()
        {
            this.pelicula.Titulo = txtTitulo.Text;
            this.pelicula.Descripcion = txtDescripcion.Text;
            PeliculaDto pelicula = await peliculasService.CreatePelicula(this.pelicula, 2);

            MessageBox.Show("Registro guardado correctamente con el identificador " + pelicula.Id.ToString());
            this.Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();

        }
    }
}
