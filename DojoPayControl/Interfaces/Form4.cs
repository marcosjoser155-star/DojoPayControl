using DojoPayControl.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DojoPayControl.Interfaces
{
    public partial class FrmRegistrarPago : Form
    {
        public FrmRegistrarPago()
        {
            InitializeComponent();
        }

        private void FrmRegistrarPago_Load(object sender, EventArgs e)
        {
            // designer uses specific controls: cmbEstudiante, cmbMetodoPago, comboBox2 (mes), numAnio
            cmbMetodoPago.Items.Clear();
            cmbMetodoPago.Items.Add("Efectivo");
            cmbMetodoPago.Items.Add("Tarjeta");
            cmbMetodoPago.Items.Add("Transferencia");
            if (cmbMetodoPago.Items.Count > 0) cmbMetodoPago.SelectedIndex = 0;

            CargarEstudiantes();
        }

        private void CargarEstudiantes()
        {
            Estudiante obj = new Estudiante();
            DataTable tabla = obj.ObtenerTodos();

            if (!tabla.Columns.Contains("NombreCompleto"))
                tabla.Columns.Add("NombreCompleto", typeof(string));

            foreach (DataRow fila in tabla.Rows)
                fila["NombreCompleto"] = string.Format("{0}, {1}", fila["Apellido"], fila["Nombre"]);

            cmbEstudiante.DisplayMember = "NombreCompleto";
            cmbEstudiante.ValueMember = "IdEstudiante";
            cmbEstudiante.DataSource = tabla;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbEstudiante.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un estudiante");
                return;
            }
            // Determine id and payment method
            int idEstudiante = Convert.ToInt32(cmbEstudiante.SelectedValue);
            string metodoPago = cmbMetodoPago.SelectedItem?.ToString() ?? "Efectivo";

            // Try to parse monto from label8 (designer displays amount there) as fallback
            decimal monto = 0;
            if (!decimal.TryParse(label8.Text, out monto))
            {
                MessageBox.Show("Ingrese un monto válido");
                return;
            }

            bool ok = false;

            // Default to Mensualidad; if user clicked Anualidad button in UI, that should call different handler.
            Mensualidad mObj = new Mensualidad();
            mObj.IdEstudiante = idEstudiante;
            mObj.Monto = monto;
            mObj.MetodoPago = metodoPago;
            // use comboBox2 (mes) and numAnio for description
            mObj.MesCorrespondiente = (comboBox2.SelectedItem?.ToString() ?? string.Empty) + " " + numAnio.Value.ToString();

            if (mObj.ValidarMensualidad())
            {
                mObj.RegistrarMensualidad();
                ok = true;
            }

            if (ok)
            {
                MessageBox.Show("Mensualidad registrada correctamente");
                CargarHistorial(idEstudiante);
            }
        }

        private void btnVerHistorial_Click(object sender, EventArgs e)
        {
            if (cmbEstudiante.SelectedValue == null) return;
            CargarHistorial(Convert.ToInt32(cmbEstudiante.SelectedValue));
        }

        private void CargarHistorial(int idEstudiante)
        {
            Pago obj = new Pago();
            var tabla = obj.MostrarHistorial(idEstudiante);
            // designer has no dgvHistorial in this form; simply show count
            MessageBox.Show($"Historial cargado: {tabla.Rows.Count} registros.");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {
            // designer placeholder
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // designer placeholder
        }

        private void pnlFooter_Paint(object sender, PaintEventArgs e)
        {
            // designer placeholder
        }
    }
}
