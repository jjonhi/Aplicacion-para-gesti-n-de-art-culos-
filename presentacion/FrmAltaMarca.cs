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
    public partial class FrmAltaMarca : Form
    {
        private Marca marca = null;
        public FrmAltaMarca()
        {
            InitializeComponent();
        }
        public FrmAltaMarca(Marca marca)
        {
            InitializeComponent();
            this.marca = marca;
        }
        private void btnCancelarMarca_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptarMarca_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            bool camposValidos = true;

            // Reiniciar indicadores de error
            lblDescripcionMarca.ForeColor = Color.Black;

            try
            {
                if (marca == null)
                    marca = new Marca();

                // Validaciones visuales con asteriscos en etiquetas
                if (string.IsNullOrWhiteSpace(txtDescripcionMarca.Text))
                {
                    lblDescripcionMarca.Text = "* Descripción:";
                    lblDescripcionMarca.ForeColor = Color.Red;
                    camposValidos = false;
                }

                if (!camposValidos)
                {
                    MessageBox.Show("Por favor, complete los campos obligatorios.");
                    return;
                }

                // Asignar valores a la entidad marca

                marca.Descripcion = txtDescripcionMarca.Text.Trim();

                // Guardar o modificar marca
                if (marca.Id != 0)
                {
                    negocio.modificar(marca);
                    MessageBox.Show("Marca modificada con éxito.");
                }
                else
                {
                    negocio.agregar(marca);
                    MessageBox.Show("Marca agregada con éxito.");
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
