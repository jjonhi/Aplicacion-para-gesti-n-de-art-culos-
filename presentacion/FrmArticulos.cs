using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class FrmArticulos : Form
    {
        private List<Articulo> articulolista;
        public FrmArticulos()
        {
            InitializeComponent();
        }

        private void FrmArticulos_Load(object sender, EventArgs e)
        {

            cargar();
            cbxCampo.Items.Add("Nombre");
            cbxCampo.Items.Add("Precio");
            cbxCampo.Items.Add("Marca");
            cbxCampo.Items.Add("Categoria");

        }
        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            articulolista = negocio.Listar();
            dgvArticulo.DataSource = articulolista;

            // Cambiar fuente y tamaño del DataGridView
            dgvArticulo.DefaultCellStyle.Font = new Font("Arial", 12);  // Fuente principal
            dgvArticulo.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold); // Fuente del encabezado
            ocultarColumnas();
            cargarImagen(articulolista[0].UrlImagen);

        }
        private void ocultarColumnas()
        {
            dgvArticulo.Columns["Id"].Visible = false;
            dgvArticulo.Columns["UrlImagen"].Visible = false;
        }



        private void cargarImagen(string imagen)
        {
            try
            {
                pbArticulo.Load(imagen);
            }
            catch (Exception ex)
            {

                pbArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTxdAOY_-vITFVI-ej84s2U_ErxhOly-z3y_Q&s");
            }

        }

        private void dgvArticulo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaArticulo modificar = new FrmAltaArticulo();
            modificar.ShowDialog();
            cargar();

        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow != null)
            {
                Articulo Seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                cargarImagen(Seleccionado.UrlImagen);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

            FrmAltaArticulo alta = new FrmAltaArticulo(seleccionado);
            alta.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que se haya seleccionado un artículo
                if (dgvArticulo.CurrentRow == null || dgvArticulo.CurrentRow.DataBoundItem == null)
                {
                    MessageBox.Show("Por favor, seleccione un artículo para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirmación de eliminación
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar el artículo?",
                                                           "Eliminar",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    // Obtener el artículo seleccionado
                    Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

                    // Eliminar el artículo usando la capa de negocio
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminar(seleccionado.Id);

                    // Actualizar la grilla de datos
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar el artículo:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listafiltrada;
            string filtro = txtFiltro.Text;
            if (filtro.Length >= 3)
            {
                listafiltrada = articulolista.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()));

            }

            else
            {
                listafiltrada = articulolista;
            }

            dgvArticulo.DataSource = null;
            dgvArticulo.DataSource = listafiltrada;
            ocultarColumnas();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbxCampo.SelectedItem?.ToString(); //  asegura de que SelectedItem no sea null

            if (string.IsNullOrEmpty(opcion))
                return; // Si no hay selección, se sale del método

            cbxCriterio.Items.Clear(); // Limpiar siempre antes de agregar nuevos ítems

            if (opcion == "Precio")
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Mayor a");
                cbxCriterio.Items.Add("Menor a");
                cbxCriterio.Items.Add("Igual a");

            }
            else
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Comienza con ");
                cbxCriterio.Items.Add("Termina con ");
                cbxCriterio.Items.Add("Contiene ");

            }
        }
        

        
        
        private bool validarFiltro()
        {
            StringBuilder errores = new StringBuilder();

            if (cbxCampo.SelectedIndex < 0)
            {
                errores.AppendLine("Seleccione un campo para filtrar.");
            }

            if (cbxCriterio.SelectedIndex < 0)
            {
                errores.AppendLine("Seleccione un criterio para filtrar.");
            }

            if (cbxCampo.SelectedItem?.ToString() == "Precio")
            {
                if (string.IsNullOrWhiteSpace(txtFiltroAvanzado.Text))
                {
                    errores.AppendLine("Debes cargar el filtro para valores numéricos.");
                }
                else
                {
                    // Validación para números decimales usando TryParse
                    if (!decimal.TryParse(txtFiltroAvanzado.Text, System.Globalization.NumberStyles.Any,
                                          System.Globalization.CultureInfo.InvariantCulture, out decimal dummyDecimal))
                    {
                        errores.AppendLine("Ingrese un valor numérico válido.");
                    }
                }
            }

            if (errores.Length > 0)
            {
                MessageBox.Show(errores.ToString(), "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                    return;

                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cbxCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text.Trim();

                dgvArticulo.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrarArticulo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            FrmReporte reporte = new FrmReporte();
            reporte.ShowDialog();
        }
    }
}
