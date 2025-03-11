using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace presentacion
{
    public partial class FrmAltaArticulo : Form
    {
        private Articulo articulo = null;
        OpenFileDialog archivo = null;
        public FrmAltaArticulo()
        {
            InitializeComponent();
        }
        public FrmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void FrmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                List<Categoria> categorias = categoriaNegocio.Listar();

                cbxCategoria.DataSource = categorias;
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";

                List<Marca> marcas = marcaNegocio.Listar();
                cbxMarca.DataSource = marcas;
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";

                // Si se está editando un Articulo, cargar sus datos
                if ( articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtUrlImagen.Text = articulo.UrlImagen;
                    txtPrecio.Text=articulo.Precio.ToString("N2");

                    cargarImagen(articulo.UrlImagen);

                    cbxMarca.SelectedValue = articulo.Marca.Id;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los datos: " + ex.Message);
            }


        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxAltaArticulo.SizeMode = PictureBoxSizeMode.Zoom; // Asegura que la imagen se ajuste sin recortes
               
                pbxAltaArticulo.Load(imagen);

            }
            catch (Exception ex)
            {

                pbxAltaArticulo.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/1280px-Placeholder_view_vector.svg.png");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUrlImagen.Text))
            {
                cargarImagen(txtUrlImagen.Text);
            }
            else
            {
                // Cargar una imagen por defecto si el campo está vacío
                cargarImagen("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/1280px-Placeholder_view_vector.svg.png");
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            bool camposValidos = true;

            // Reiniciar indicadores de error
            lblCodigo.ForeColor = Color.Black;
            lblNombre.ForeColor = Color.Black;
            lblDescripcion.ForeColor = Color.Black;
            lblPrecio.ForeColor = Color.Black;

            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                // Validaciones visuales con asteriscos en etiquetas
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    lblCodigo.Text = "* Código:";
                    lblCodigo.ForeColor = Color.Red;
                    camposValidos = false;
                }
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    lblNombre.Text = "* Nombre:";
                    lblNombre.ForeColor = Color.Red;
                    camposValidos = false;
                }
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    lblDescripcion.Text = "* Descripción:";
                    lblDescripcion.ForeColor = Color.Red;
                    camposValidos = false;
                }
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
                {
                    lblPrecio.Text = "* Precio:";
                    lblPrecio.ForeColor = Color.Red;
                    camposValidos = false;
                }

                if (!camposValidos)
                {
                    MessageBox.Show("Por favor, complete los campos obligatorios.");
                    return;
                }

                // Asignar valores a la entidad Articulo
                articulo.Codigo = txtCodigo.Text.Trim();
                articulo.Nombre = txtNombre.Text.Trim();
                articulo.Descripcion = txtDescripcion.Text.Trim();
                articulo.UrlImagen = txtUrlImagen.Text.Trim();
                articulo.Precio = precio;

                articulo.Marca = cbxMarca.SelectedItem as Marca;
                articulo.Categoria = cbxCategoria.SelectedItem as Categoria;

                // Guardar o modificar artículo
                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Artículo modificado con éxito.");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Artículo agregado con éxito.");
                }

                // Manejo seguro del archivo
                if (archivo != null && !string.IsNullOrWhiteSpace(txtUrlImagen.Text) && !txtUrlImagen.Text.ToUpper().Contains("HTTP"))
                {
                    try
                    {
                        string rutaDestino = ConfigurationManager.AppSettings["Images-folder"] + archivo.SafeFileName;
                        File.Copy(archivo.FileName, rutaDestino, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al copiar el archivo: " + ex.Message);
                    }
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
            finally
            {
                negocio = null; // Liberar memoria
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; // Ampliamos el filtro para más formatos

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrlImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
        }
    }
}
