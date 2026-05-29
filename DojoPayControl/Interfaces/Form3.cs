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
    public partial class FrmNuevoEstudiante : Form
    {
        public FrmNuevoEstudiante()
        {
            InitializeComponent();
        }

        private void FrmNuevoEstudiante_Load(object sender, EventArgs e)
        {
            // populate estado combo safely at runtime
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Pausado");
            cmbEstado.Items.Add("Inactivo");
            cmbEstado.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtCedula.Text))
            {
                MessageBox.Show("Nombre, apellido y cédula son obligatorios");
                return;
            }

            Estudiante estudiante = new Estudiante();
            estudiante.Nombre = txtNombre.Text;
            estudiante.Apellido = txtApellido.Text;
            estudiante.Cedula = txtCedula.Text;
            estudiante.Telefono = txtTelefono.Text;
            estudiante.Estado = cmbEstado.SelectedItem?.ToString() ?? "Activo";

            bool ok = estudiante.Registrar();

            if (!ok)
            {
                MessageBox.Show("Error al registrar estudiante");
                return;
            }

            MessageBox.Show("Estudiante registrado correctamente");

            if (numMensualidad.Value > 0)
            {
                Mensualidad m = new Mensualidad();
                m.IdEstudiante = estudiante.IdEstudiante;
                m.Monto = numMensualidad.Value;
                m.MetodoPago = "Efectivo";
                m.MesCorrespondiente = DateTime.Now.ToString("MMMM yyyy");
                m.RegistrarMensualidad();
            }

            if (numAnualidad.Value > 0)
            {
                Anualidad a = new Anualidad();
                a.IdEstudiante = estudiante.IdEstudiante;
                a.Monto = numAnualidad.Value;
                a.MetodoPago = "Efectivo";
                a.AñoCorrespondiente = DateTime.Now.Year;
                a.RegistrarAnualidad();
            }

            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtCedula.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            numMensualidad.Value = 0;
            numAnualidad.Value = 0;
            cmbEstado.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {
            // placeholder for designer paint event
        }
    }
}
