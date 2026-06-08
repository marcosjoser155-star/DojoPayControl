using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DojoPayControl.Clases;

namespace DojoPayControl.Interfaces
{
    public partial class FrmNuevoEstudiante : Form
    {
        public FrmNuevoEstudiante()
        {
            InitializeComponent();

            btnGuardar.Click += new EventHandler(btnGuardar_Click);
            btnCancelar.Click += new EventHandler(btnCancelar_Click);
        }

        private void FrmNuevoEstudiante_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            txtNombre.Clear();
            txtApellido.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();

            textBox3.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtNombre.ForeColor = Color.Black;
            txtApellido.ForeColor = Color.Black;
            txtCedula.ForeColor = Color.Black;
            txtTelefono.ForeColor = Color.Black;
            textBox3.ForeColor = Color.Black;

            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Al día");
            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("Restringido");
            cmbEstado.Items.Add("Ausencia Temporal");
            cmbEstado.Items.Add("Revisar anualidad");
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.SelectedIndex = 1;

            numMensualidad.DecimalPlaces = 2;
            numMensualidad.Minimum = 0;
            numMensualidad.Maximum = 10000;
            numMensualidad.Value = 0;

            numAnualidad.DecimalPlaces = 2;
            numAnualidad.Minimum = 0;
            numAnualidad.Maximum = 10000;
            numAnualidad.Value = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos() == true)
            {
                GuardarEstudiante();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidarCampos()
        {
            DateTime fechaIngreso;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe escribir el nombre del estudiante.");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Debe escribir el apellido del estudiante.");
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCedula.Text))
            {
                MessageBox.Show("Debe escribir la cédula del estudiante.");
                txtCedula.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Debe escribir el teléfono del estudiante.");
                txtTelefono.Focus();
                return false;
            }

            if (DateTime.TryParse(textBox3.Text.Trim(), out fechaIngreso) == false)
            {
                MessageBox.Show("La fecha de ingreso no tiene un formato válido.");
                textBox3.Focus();
                return false;
            }

            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el estado inicial del estudiante.");
                cmbEstado.Focus();
                return false;
            }

            return true;
        }

        private void GuardarEstudiante()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string cedula = txtCedula.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                DateTime fechaIngreso = DateTime.Parse(textBox3.Text.Trim());
                string estado = cmbEstado.SelectedItem.ToString();
                decimal montoMensualidad = numMensualidad.Value;
                decimal montoAnualidad = numAnualidad.Value;

                using (MySqlConnection conexionMySql = conexion.ObtenerConexion())
                {
                    conexionMySql.Open();

                    string consulta = "INSERT INTO Estudiante " +
                                      "(nombre, apellido, cedula, telefono, fechaIngreso, estado, activo, montoMensualidad, montoAnualidad) " +
                                      "VALUES " +
                                      "(@nombre, @apellido, @cedula, @telefono, @fechaIngreso, @estado, @activo, @montoMensualidad, @montoAnualidad)";

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexionMySql))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@apellido", apellido);
                        comando.Parameters.AddWithValue("@cedula", cedula);
                        comando.Parameters.AddWithValue("@telefono", telefono);
                        comando.Parameters.AddWithValue("@fechaIngreso", fechaIngreso);
                        comando.Parameters.AddWithValue("@estado", estado);
                        comando.Parameters.AddWithValue("@activo", 1);
                        comando.Parameters.AddWithValue("@montoMensualidad", montoMensualidad);
                        comando.Parameters.AddWithValue("@montoAnualidad", montoAnualidad);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Estudiante guardado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar estudiante: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();

            textBox3.Text = DateTime.Now.ToString("dd/MM/yyyy");

            cmbEstado.SelectedIndex = 1;

            numMensualidad.Value = 0;
            numAnualidad.Value = 0;

            txtNombre.Focus();
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}