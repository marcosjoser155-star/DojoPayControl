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
        private FrmDashboard dashboard;

        public FrmNuevoEstudiante()
        {
            InitializeComponent();

            this.Load += FrmNuevoEstudiante_Load;
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;
        }

        public FrmNuevoEstudiante(FrmDashboard frmDashboard) : this()
        {
            dashboard = frmDashboard;
        }

        // Designer paint handler required by Form3.Designer.cs
        private void Panel2_Paint(object sender, PaintEventArgs e) { }

        private void FrmNuevoEstudiante_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("Al día");
            cmbEstado.Items.Add("Restringido");
            cmbEstado.Items.Add("Ausencia temporal");

            cmbEstado.SelectedIndex = 0;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Estudiante estudiante = new Estudiante();

            estudiante.Nombre = txtNombre.Text.Trim();
            estudiante.Cedula = txtCedula.Text.Trim();
            estudiante.Telefono = txtTelefono.Text.Trim();
            estudiante.FechaIngreso = DateTime.Now;
            estudiante.Estado = cmbEstado.Text;

            if (!estudiante.ValidarEstudiante())
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }

            int idEstudiante = estudiante.Registrar();

            if (idEstudiante > 0)
            {
                if (numMensualidad.Value > 0)
                {
                    Pago pagoMensual = new Pago();

                    pagoMensual.IdEstudiante = idEstudiante;
                    pagoMensual.Monto = numMensualidad.Value;
                    pagoMensual.FechaPago = DateTime.Now;
                    pagoMensual.MetodoPago = "Efectivo";
                    pagoMensual.TipoPago = "Mensualidad";
                    pagoMensual.MesCorrespondiente = DateTime.Now.Month;
                    pagoMensual.AnioCorrespondiente = DateTime.Now.Year;

                    pagoMensual.RegistrarPago();
                }

                if (numAnualidad.Value > 0)
                {
                    Pago pagoAnual = new Pago();

                    pagoAnual.IdEstudiante = idEstudiante;
                    pagoAnual.Monto = numAnualidad.Value;
                    pagoAnual.FechaPago = DateTime.Now;
                    pagoAnual.MetodoPago = "Efectivo";
                    pagoAnual.TipoPago = "Anualidad";
                    pagoAnual.AnioCorrespondiente = DateTime.Now.Year;

                    pagoAnual.RegistrarPago();
                }

                MessageBox.Show("Estudiante registrado");

                dashboard.CargarDashboard();

                this.Close();
            }
            else
            {
                MessageBox.Show("Error al registrar");
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
