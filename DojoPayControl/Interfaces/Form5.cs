using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DojoPayControl.Clases;

namespace DojoPayControl.Interfaces
{
    public partial class FrmAusenciaTemporal : Form
    {
        public FrmAusenciaTemporal()
        {
            InitializeComponent();

            // Eventos del formulario
            this.Load -= FrmAusenciaTemporal_Load;
            this.Load += FrmAusenciaTemporal_Load;

            // Eventos de botones
            btnRegistrarAusencia.Click -= btnRegistrarAusencia_Click;
            btnRegistrarAusencia.Click += btnRegistrarAusencia_Click;

            btnCancelar.Click -= btnCancelar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }

        private void FrmAusenciaTemporal_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarEstudiantes();
        }

        private void ConfigurarFormulario()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            // Configurar fechas
            dtpFechaInicioAusencia.Value = DateTime.Now;
            dtpFechaFinAusencia.Value = DateTime.Now;

            // Configurar motivos de ausencia
            cmbMotivoAusencia.Items.Clear();
            cmbMotivoAusencia.Items.Add("Viaje");
            cmbMotivoAusencia.Items.Add("Salud");
            cmbMotivoAusencia.Items.Add("Motivos de estudios");
            cmbMotivoAusencia.Items.Add("Motivos Personales");
            cmbMotivoAusencia.Items.Add("Trabajo");
            cmbMotivoAusencia.Items.Add("Otros");

            cmbMotivoAusencia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMotivoAusencia.SelectedIndex = 0;

            // Limpiar observación
            txtObservacionAusencia.Clear();
        }

        private void CargarEstudiantes()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "SELECT " +
                                  "idEstudiante, " +
                                  "CONCAT(nombre, ' ', apellido) AS nombreCompleto " +
                                  "FROM Estudiante " +
                                  "WHERE activo = 1 " +
                                  "AND estado <> 'Ausencia Temporal' " +
                                  "ORDER BY nombre, apellido";

                DataTable tabla = conexion.EjecutarConsulta(consulta);

                // Agrega una opción inicial al ComboBox
                DataRow fila = tabla.NewRow();
                fila["idEstudiante"] = 0;
                fila["nombreCompleto"] = "Seleccione un estudiante";
                tabla.Rows.InsertAt(fila, 0);

                cmbEstudiante.DataSource = tabla;
                cmbEstudiante.DisplayMember = "nombreCompleto";
                cmbEstudiante.ValueMember = "idEstudiante";
                cmbEstudiante.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbEstudiante.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar estudiantes: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnRegistrarAusencia_Click(object sender, EventArgs e)
        {
            if (ValidarCampos() == true)
            {
                RegistrarAusenciaTemporal();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidarCampos()
        {
            if (cmbEstudiante.SelectedIndex == -1 ||
                Convert.ToInt32(cmbEstudiante.SelectedValue) == 0)
            {
                MessageBox.Show(
                    "Debe seleccionar un estudiante.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cmbEstudiante.Focus();
                return false;
            }

            if (dtpFechaFinAusencia.Value.Date < dtpFechaInicioAusencia.Value.Date)
            {
                MessageBox.Show(
                    "La fecha final no puede ser menor que la fecha de inicio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                dtpFechaFinAusencia.Focus();
                return false;
            }

            if (cmbMotivoAusencia.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Debe seleccionar un motivo de ausencia.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cmbMotivoAusencia.Focus();
                return false;
            }

            return true;
        }

        private void RegistrarAusenciaTemporal()
        {
            try
            {
                int idEstudiante = Convert.ToInt32(cmbEstudiante.SelectedValue);
                DateTime fechaInicio = dtpFechaInicioAusencia.Value.Date;
                DateTime fechaFin = dtpFechaFinAusencia.Value.Date;
                string motivo = cmbMotivoAusencia.SelectedItem.ToString();
                string observacion = txtObservacionAusencia.Text.Trim();

                AusenciaTemporal ausencia = new AusenciaTemporal();

                ausencia.IdEstudiante = idEstudiante;
                ausencia.FechaInicio = fechaInicio;
                ausencia.FechaFin = fechaFin;
                ausencia.Motivo = motivo;
                ausencia.Observacion = observacion;
                ausencia.EstadoAusencia = "Activa";

                if (ausencia.ValidarAusenciaTemporal() == false)
                {
                    MessageBox.Show(
                        "Los datos de la ausencia temporal no son válidos.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (ausencia.RegistrarAusenciaTemporal() == true)
                {
                    MessageBox.Show(
                        "Ausencia temporal registrada correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo registrar la ausencia temporal.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al registrar ausencia temporal: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}