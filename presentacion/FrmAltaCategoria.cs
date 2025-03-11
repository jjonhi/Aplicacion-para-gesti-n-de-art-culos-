using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class FrmAltaCategoria : Form
    {
        private Categoria categoria = null;
        public FrmAltaCategoria()
        {
            InitializeComponent();
        }
        public FrmAltaCategoria(Categoria categoria)
        {
            InitializeComponent();
            this.categoria = categoria;
        }

        private void btnCancelarCategoria_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptarCategoria_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            bool camposValidos = true;

            // Reiniciar indicadores de error
            lblDescripcionCategoria.ForeColor = Color.Black;
           
            try
            {
                if ( categoria == null)
                    categoria= new Categoria();

                // Validaciones visuales con asteriscos en etiquetas
                if (string.IsNullOrWhiteSpace(txtDescripcionCategoria.Text))
                {
                    lblDescripcionCategoria.Text = "* Descripción:";
                    lblDescripcionCategoria.ForeColor = Color.Red;
                    camposValidos = false;
                }

                if (!camposValidos)
                {
                    MessageBox.Show("Por favor, complete los campos obligatorios.");
                    return;
                }

                // Asignar valores a la entidad categoría
                
                categoria.Descripcion = txtDescripcionCategoria.Text.Trim();
            
                // Guardar o modificar categoria
                if (categoria.Id != 0)
                {
                    negocio.modificar(categoria);
                    MessageBox.Show("Categoría modificada con éxito.");
                }
                else
                {
                    negocio.agregar(categoria);
                    MessageBox.Show("Categoría agregada con éxito.");
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
    }
}
