using dominio;
using Microsoft.Reporting.WinForms;
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
    public partial class FrmReporte : Form
    {
        public FrmReporte()
        {
            InitializeComponent();
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            // Instancia del TableAdapter correspondiente
            DSreporteTableAdapters.DataTable1TableAdapter adapter = new DSreporteTableAdapters.DataTable1TableAdapter();

            // Crear una instancia del DataTable asociado
            DSreporte.DataTable1DataTable dt = new DSreporte.DataTable1DataTable();

            // Llenar el DataTable con los datos desde la base de datos
            adapter.Filldescripcion(dt);

            // Verificar si hay datos en el DataTable antes de asignarlo al ReportViewer
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos disponibles para mostrar en el reporte.");
                return; // Sale del método para evitar errores
            }

            // Limpiar y agregar la fuente de datos al ReportViewer
            reportViewer1.LocalReport.DataSources.Clear();

            // Asegurar que el nombre del dataset coincida con el definido en el RDLC
            ReportDataSource fuente = new ReportDataSource("DataSet1", (DataTable)dt);
            reportViewer1.LocalReport.DataSources.Add(fuente);

            // Refrescar el ReportViewer
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();
        }
    }
}
