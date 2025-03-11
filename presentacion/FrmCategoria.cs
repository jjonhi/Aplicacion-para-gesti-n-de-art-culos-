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
    public partial class FrmCategoria : Form
    {
        private List<Categoria> categorialista;
        public FrmCategoria()
        {
            InitializeComponent();
        }
        
        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            FrmAltaCategoria altaCategoria = new FrmAltaCategoria();
            altaCategoria.ShowDialog();
            cargar();
        }
        private void cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            categorialista = negocio.Listar();
            dgvCategoria.DataSource = categorialista;

            // Cambiar fuente y tamaño del DataGridView
            dgvCategoria.DefaultCellStyle.Font = new Font("Arial", 12);  // Fuente principal
            dgvCategoria.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold); // Fuente del encabezado
           

        }

        private void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que se haya seleccionado la categortía
                if (dgvCategoria.CurrentRow == null || dgvCategoria.CurrentRow.DataBoundItem == null)
                {
                    MessageBox.Show("Por favor, seleccione una categoría para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirmación de eliminación
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la categoría?",
                                                           "Eliminar",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    // Obtener la categoría seleccionado
                    Categoria seleccionado = (Categoria)dgvCategoria.CurrentRow.DataBoundItem;

                    // Eliminar la categoría usando la capa de negocio
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.eliminar(seleccionado.Id);

                    // Actualizar la grilla de datos
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar la categoría:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificarCategoria_Click(object sender, EventArgs e)
        {
            Categoria seleccionado;
            seleccionado = (Categoria)dgvCategoria.CurrentRow.DataBoundItem;

            FrmAltaCategoria alta = new FrmAltaCategoria(seleccionado);
            alta.ShowDialog();
            cargar();
        }

        private void btnCerrarCategoria_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
