using dominio;
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
    public partial class FrmMarca : Form
    {
        private List<Marca> marcalista;
        public FrmMarca()
        {
            InitializeComponent();
           
        }

        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            FrmAltaMarca altaMarca = new FrmAltaMarca();
            altaMarca.ShowDialog();
            cargar();
        }
        private void cargar()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            marcalista = negocio.Listar();
            dgvMarcas.DataSource = marcalista;

            // Cambiar fuente y tamaño del DataGridView
            dgvMarcas.DefaultCellStyle.Font = new Font("Arial", 12);  // Fuente principal
            dgvMarcas.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold); // Fuente del encabezado


        }

        private void FrmMarca_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnModificarMarca_Click(object sender, EventArgs e)
        {
            Marca seleccionado;
            seleccionado = (Marca)dgvMarcas.CurrentRow.DataBoundItem;

            FrmAltaMarca alta = new FrmAltaMarca(seleccionado);
            alta.ShowDialog();
            cargar();
        }

        private void btnEliminarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que se haya seleccionado un artículo
                if (dgvMarcas.CurrentRow == null || dgvMarcas.CurrentRow.DataBoundItem == null)
                {
                    MessageBox.Show("Por favor, seleccione una marca para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirmación de eliminación
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la marca?",
                                                           "Eliminar",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    // Obtener la marca seleccionada
                    Marca seleccionado = (Marca)dgvMarcas.CurrentRow.DataBoundItem;

                    // Eliminar la marca usando la capa de negocio
                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.eliminar(seleccionado.Id);

                    // Actualizar la grilla de datos
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar la marca:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrarMarca_Click(object sender, EventArgs e)
        {
            Close();    
        }
    }
}
