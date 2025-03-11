using dominio;
using MaterialSkin.Controls;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class FrmMenu : MaterialForm
    {
        private List<Articulo> articulolista;
        private ImageList imageList; // Declaramos ImageList
        public FrmMenu()
        {
            InitializeComponent();
            CargarIconos(); // Llamamos al método que carga los íconos
        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmArticulos mostrar= new FrmArticulos();
            mostrar.ShowDialog();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            articulolista = negocio.Listar();
            dgvInicio.DataSource = articulolista;

            // Cambiar fuente y tamaño del DataGridView
            dgvInicio.DefaultCellStyle.Font = new Font("Arial", 12);  // Fuente principal
            dgvInicio.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold); // Fuente del encabezado
            ocultarColumnas();
            cargarImagen(articulolista[0].UrlImagen);

        }
        private void ocultarColumnas()
        {
            dgvInicio.Columns["Id"].Visible = false;
            dgvInicio.Columns["UrlImagen"].Visible = false;
        }



        private void cargarImagen(string imagen)
        {
            try
            {
                pbxInicio.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxInicio.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTxdAOY_-vITFVI-ej84s2U_ErxhOly-z3y_Q&s");
            }

        }
        private void CargarIconos()
        {
            // Inicializamos el ImageList
            imageList = new ImageList();
            imageList.ImageSize = new Size(30, 30); // Tamaño de los iconos

            // Cargar imágenes desde archivos (coloca las rutas correctas)
            imageList.Images.Add("articulos", Image.FromFile(@"C:\Users\const\Downloads\shopping-cart-check.png"));
            imageList.Images.Add("categorias", Image.FromFile(@"C:\Users\const\Downloads\memo.png"));
            imageList.Images.Add("marcas", Image.FromFile(@"C:\Users\const\Downloads\brand.png"));

            // Asignamos las imágenes a los elementos del menú
            articulosToolStripMenuItem.Image = imageList.Images["articulos"];
            categoriasToolStripMenuItem.Image = imageList.Images["categorias"];
            marcasToolStripMenuItem.Image = imageList.Images["marcas"];
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void lblArticuloDisponible_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvInicio_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInicio.CurrentRow != null)
            {
                Articulo Seleccionado = (Articulo)dgvInicio.CurrentRow.DataBoundItem;
                cargarImagen(Seleccionado.UrlImagen);
            }
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategoria mostrar = new FrmCategoria();
            mostrar.ShowDialog();
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarca mostrar = new FrmMarca();  
            mostrar.ShowDialog();
        }
    }
}
