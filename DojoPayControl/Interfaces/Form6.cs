using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DojoPayControl.Clases;

namespace DojoPayControl.Interfaces
{
    public partial class FrmReactivarEstudiante : Form
    {
        public FrmReactivarEstudiante()
        {
            InitializeComponent();

            // Evento que se ejecuta cuando abre el formulario
            this.Load -= FrmReactivarEstudiante_Load;
            this.Load += FrmReactivarEstudiante_Load;

            // Evento del botón para registrar la reactivación
            btnRegistrarAusencia.Click -= btnRegistrarAusencia_Click;
            btnRegistrarAusencia.Click += btnRegistrarAusencia_Click;

            // Evento del botón cancelar
            btnCancelar.Click -= btnCancelar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }

        private void FrmReactivarEstudiante_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarEstudiantesEnAusencia();
        }

        private void ConfigurarFormulario()
        {
            // Centra el formulario al abrirlo
            this.StartPosition = FormStartPosition.CenterScreen;

            // Fecha de reactivación por defecto
            dtpFechaInicioReactivacion.Value = DateTime.Now;

            // Limpia observación
            txtObservacionAusencia.Clear();
        }

        private void CargarEstudiantesEnAusencia()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "SELECT " +
                                  "a.idAusencia, " +
                                  "e.idEstudiante, " +
                                  "CONCAT(e.nombre, ' ', e.apellido) AS nombreCompleto " +
                                  "FROM AusenciaTemporal a " +
                                  "INNER JOIN Estudiante e ON a.idEstudiante = e.idEstudiante " +
                                  "WHERE e.activo = 1 " +
                                  "AND e.estado = 'Ausencia Temporal' " +
                                  "AND a.estadoAusencia = 'Activa' " +
                                  "ORDER BY e.nombre, e.apellido";

                DataTable tabla = conexion.EjecutarConsulta(consulta);

                // Agrega una opción inicial
                DataRow fila = tabla.NewRow();
                fila["idAusencia"] = 0;
                fila["idEstudiante"] = 0;
                fila["nombreCompleto"] = "Seleccione un estudiante";
                tabla.Rows.InsertAt(fila, 0);

                // Configura el ComboBox
                cmbEstudiante.DisplayMember = "nombreCompleto";
                cmbEstudiante.ValueMember = "idAusencia";
                cmbEstudiante.DataSource = tabla;

                // Lo convierte en DropDownList
                cmbEstudiante.DropDownStyle = ComboBoxStyle.DropDownList;

                if (cmbEstudiante.Items.Count > 0)
                {
                    cmbEstudiante.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar estudiantes en ausencia temporal: " + ex.Message,
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
                ReactivarEstudiante();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidarCampos()
        {
            if (cmbEstudiante.SelectedIndex == -1)
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

            if (Convert.ToInt32(cmbEstudiante.SelectedValue) == 0)
            {
                MessageBox.Show(
                    "Debe seleccionar un estudiante válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cmbEstudiante.Focus();
                return false;
            }

            return true;
        }

        private void ReactivarEstudiante()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                int idAusencia = Convert.ToInt32(cmbEstudiante.SelectedValue);
                int idEstudiante = ObtenerIdEstudianteSeleccionado();
                DateTime fechaReactivacion = dtpFechaInicioReactivacion.Value.Date;
                string observacion = txtObservacionAusencia.Text.Trim();

                using (MySqlConnection conexionMySql = conexion.ObtenerConexion())
                {
                    conexionMySql.Open();

                    // Primero finaliza la ausencia temporal
                    string consultaAusencia = "UPDATE AusenciaTemporal SET " +
                                              "fechaFin = @fechaFin, " +
                                              "observacion = @observacion, " +
                                              "estadoAusencia = 'Finalizada' " +
                                              "WHERE idAusencia = @idAusencia";

                    using (MySqlCommand comandoAusencia = new MySqlCommand(consultaAusencia, conexionMySql))
                    {
                        comandoAusencia.Parameters.AddWithValue("@fechaFin", fechaReactivacion);
                        comandoAusencia.Parameters.AddWithValue("@observacion", observacion);
                        comandoAusencia.Parameters.AddWithValue("@idAusencia", idAusencia);

                        comandoAusencia.ExecuteNonQuery();
                    }

                    // Luego reactiva al estudiante y lo deja como Pendiente
                    string consultaEstudiante = "UPDATE Estudiante SET " +
                                                "estado = 'Pendiente' " +
                                                "WHERE idEstudiante = @idEstudiante";

                    using (MySqlCommand comandoEstudiante = new MySqlCommand(consultaEstudiante, conexionMySql))
                    {
                        comandoEstudiante.Parameters.AddWithValue("@idEstudiante", idEstudiante);

                        comandoEstudiante.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Estudiante reactivado correctamente.",
                    "Reactivación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al reactivar estudiante: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private int ObtenerIdEstudianteSeleccionado()
        {
            DataRowView fila = cmbEstudiante.SelectedItem as DataRowView;

            if (fila != null)
            {
                return Convert.ToInt32(fila["idEstudiante"]);
            }

            return 0;
        }
    }
}