using DojoPayControl.Clases;
using DojoPayControl.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DojoPayControl
{
    public partial class FrmDashboard : Form
    {
        // Objeto para usar la conexión con MySQL
        private ConexionDB conexion;

        public FrmDashboard()
        {
            InitializeComponent();

            conexion = new ConexionDB();

            // Eventos principales del formulario
            this.Load += FrmDashboard_Load;

            // Eventos de botones principales
            btnNuevoEstudiante.Click += BtnNuevoEstudiante_Click;
            btnRegistrarPago.Click += BtnRegistrarPago_Click;
            btnAusenciaTemporal.Click += BtnAusenciaTemporal_Click;
            btnReactivarEstudiante.Click += BtnReactivarEstudiante_Click;

            // Eventos de filtros del dashboard
            dtpFechaFiltro.ValueChanged += FiltrosDashboard_Changed;
            txtBusquedaEstudiante.TextChanged += FiltrosDashboard_Changed;

            // Eventos de filtros de la pestaña estudiantes
            txtBuscarEstudiante.TextChanged += FiltrosEstudiantes_Changed;
            cmbFiltroEstado.SelectedIndexChanged += FiltrosEstudiantes_Changed;

            // Eventos de filtros de la pestaña pagos
            txtBuscarEstudiantePago.TextChanged += FiltrosPagos_Changed;
            cmbFiltroMes.SelectedIndexChanged += FiltrosPagos_Changed;
            cmbFiltroAnio.SelectedIndexChanged += FiltrosPagos_Changed;
            cmbFiltroTipoPago.SelectedIndexChanged += FiltrosPagos_Changed;

            // Evento para el botón eliminar dentro del DataGridView de estudiantes
            dgvEstudiantes.CellContentClick += DgvEstudiantes_CellContentClick;
        }

        // Se ejecuta cuando abre el dashboard
        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            ConfigurarCombos();
            ConfigurarTablas();
            CargarTodo();
        }

        // Configura los ComboBox de filtros
        private void ConfigurarCombos()
        {
            cmbFiltroEstado.Items.Clear();
            cmbFiltroEstado.Items.Add("Todos los estados");
            cmbFiltroEstado.Items.Add("Al día");
            cmbFiltroEstado.Items.Add("Pendiente");
            cmbFiltroEstado.Items.Add("Restringido");
            cmbFiltroEstado.Items.Add("Ausente");
            cmbFiltroEstado.Items.Add("Revisar anualidad");
            cmbFiltroEstado.SelectedIndex = 0;

            cmbFiltroMes.Items.Clear();
            cmbFiltroMes.Items.Add("Todos los meses");
            cmbFiltroMes.Items.Add("Enero");
            cmbFiltroMes.Items.Add("Febrero");
            cmbFiltroMes.Items.Add("Marzo");
            cmbFiltroMes.Items.Add("Abril");
            cmbFiltroMes.Items.Add("Mayo");
            cmbFiltroMes.Items.Add("Junio");
            cmbFiltroMes.Items.Add("Julio");
            cmbFiltroMes.Items.Add("Agosto");
            cmbFiltroMes.Items.Add("Septiembre");
            cmbFiltroMes.Items.Add("Octubre");
            cmbFiltroMes.Items.Add("Noviembre");
            cmbFiltroMes.Items.Add("Diciembre");
            cmbFiltroMes.SelectedIndex = 0;

            cmbFiltroAnio.Items.Clear();
            cmbFiltroAnio.Items.Add("Todos los años");
            cmbFiltroAnio.Items.Add(DateTime.Now.Year.ToString());
            cmbFiltroAnio.Items.Add((DateTime.Now.Year + 1).ToString());
            cmbFiltroAnio.SelectedIndex = 0;

            cmbFiltroTipoPago.Items.Clear();
            cmbFiltroTipoPago.Items.Add("Todos los tipos");
            cmbFiltroTipoPago.Items.Add("Mensualidad");
            cmbFiltroTipoPago.Items.Add("Anualidad");
            cmbFiltroTipoPago.SelectedIndex = 0;
        }

        // Configura detalles básicos de las tablas
        private void ConfigurarTablas()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvEstudiantes.ReadOnly = true;
            dgvEstudiantes.AllowUserToAddRows = false;
            dgvEstudiantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvPagos.ReadOnly = true;
            dgvPagos.AllowUserToAddRows = false;
            dgvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Carga toda la información del dashboard
        private void CargarTodo()
        {
            CargarDashboard();
            CargarContadores();
            CargarEstudiantes();
            CargarPagos();
        }

        // Carga la tabla principal del dashboard
        private void CargarDashboard()
        {
            dataGridView1.Rows.Clear();

            int mes = dtpFechaFiltro.Value.Month;
            int anio = dtpFechaFiltro.Value.Year;
            string busqueda = "%" + txtBusquedaEstudiante.Text.Trim() + "%";

            string consulta =
                "SELECT " +
                "e.idEstudiante, " +
                "e.nombre, " +
                "e.estado, " +
                "IFNULL(m.estado, 'Pendiente') AS estadoMensualidad, " +
                "IFNULL(a.estado, 'Pendiente') AS estadoAnualidad " +
                "FROM Estudiante e " +
                "LEFT JOIN Mensualidad m ON e.idEstudiante = m.idEstudiante " +
                "AND m.mesCorrespondiente = @mes " +
                "AND m.anioCorrespondiente = @anio " +
                "LEFT JOIN Anualidad a ON e.idEstudiante = a.idEstudiante " +
                "AND a.anioCorrespondiente = @anio " +
                "WHERE e.activo = 1 " +
                "AND e.nombre LIKE @busqueda " +
                "ORDER BY e.nombre ASC";

            try
            {
                using (MySqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    using (MySqlCommand comando = new MySqlCommand(consulta, cn))
                    {
                        comando.Parameters.AddWithValue("@mes", mes);
                        comando.Parameters.AddWithValue("@anio", anio);
                        comando.Parameters.AddWithValue("@busqueda", busqueda);

                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                string nombre = lector["nombre"].ToString();
                                string estadoEstudiante = lector["estado"].ToString();
                                string estadoMensualidad = lector["estadoMensualidad"].ToString();
                                string estadoAnualidad = lector["estadoAnualidad"].ToString();

                                string estadoGeneral = CalcularEstadoVisual(
                                    estadoEstudiante,
                                    estadoMensualidad,
                                    estadoAnualidad
                                );

                                int fila = dataGridView1.Rows.Add(
                                    "●",
                                    nombre,
                                    estadoMensualidad,
                                    estadoAnualidad
                                );

                                AplicarColorFila(dataGridView1.Rows[fila], estadoGeneral);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar dashboard: " + ex.Message);
            }
        }

        // Carga los contadores de estados
        private void CargarContadores()
        {
            int alDia = 0;
            int pendiente = 0;
            int restringido = 0;
            int ausente = 0;
            int revisarAnualidad = 0;

            string consulta =
                "SELECT estado, COUNT(*) AS total " +
                "FROM Estudiante " +
                "WHERE activo = 1 " +
                "GROUP BY estado";

            try
            {
                using (MySqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    using (MySqlCommand comando = new MySqlCommand(consulta, cn))
                    {
                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                string estado = lector["estado"].ToString();
                                int total = Convert.ToInt32(lector["total"]);

                                if (estado == "Al día")
                                {
                                    alDia = total;
                                }
                                else if (estado == "Pendiente")
                                {
                                    pendiente = total;
                                }
                                else if (estado == "Restringido")
                                {
                                    restringido = total;
                                }
                                else if (estado == "Ausente" || estado == "Pausado")
                                {
                                    ausente = total;
                                }
                                else if (estado == "Revisar anualidad")
                                {
                                    revisarAnualidad = total;
                                }
                            }
                        }
                    }
                }

                lblCountAlDia.Text = "Al día: " + alDia;
                lblCountPendiente.Text = "Pendiente: " + pendiente;
                lblCountRestringido.Text = "Restringido: " + restringido;
                lblCountPausado.Text = "Ausente: " + ausente;
                lblCountRevision.Text = "Revisar anualidad: " + revisarAnualidad;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar contadores: " + ex.Message);
            }
        }

        // Carga la pestaña de estudiantes
        private void CargarEstudiantes()
        {
            dgvEstudiantes.Rows.Clear();

            string busqueda = "%" + txtBuscarEstudiante.Text.Trim() + "%";
            string estadoFiltro = "";

            if (cmbFiltroEstado.SelectedItem != null)
            {
                estadoFiltro = cmbFiltroEstado.SelectedItem.ToString();
            }

            string consulta =
                "SELECT idEstudiante, nombre, cedula, telefono, fechaIngreso, estado " +
                "FROM Estudiante " +
                "WHERE activo = 1 " +
                "AND (nombre LIKE @busqueda OR cedula LIKE @busqueda) ";

            if (estadoFiltro != "" && estadoFiltro != "Todos los estados")
            {
                consulta += "AND estado = @estado ";
            }

            consulta += "ORDER BY nombre ASC";

            try
            {
                using (MySqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    using (MySqlCommand comando = new MySqlCommand(consulta, cn))
                    {
                        comando.Parameters.AddWithValue("@busqueda", busqueda);

                        if (estadoFiltro != "" && estadoFiltro != "Todos los estados")
                        {
                            comando.Parameters.AddWithValue("@estado", estadoFiltro);
                        }

                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                int idEstudiante = Convert.ToInt32(lector["idEstudiante"]);
                                string nombre = lector["nombre"].ToString();
                                string cedula = lector["cedula"].ToString();
                                string telefono = lector["telefono"].ToString();
                                DateTime fechaIngreso = Convert.ToDateTime(lector["fechaIngreso"]);
                                string estado = lector["estado"].ToString();

                                int fila = dgvEstudiantes.Rows.Add(
                                    "●",
                                    nombre,
                                    cedula,
                                    telefono,
                                    fechaIngreso.ToString("dd/MM/yyyy"),
                                    estado,
                                    "Eliminar"
                                );

                                dgvEstudiantes.Rows[fila].Tag = idEstudiante;

                                AplicarColorFila(dgvEstudiantes.Rows[fila], estado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estudiantes: " + ex.Message);
            }
        }

        // Carga la pestaña de pagos
        private void CargarPagos()
        {
            dgvPagos.Rows.Clear();

            string busqueda = "%" + txtBuscarEstudiantePago.Text.Trim() + "%";

            string filtroMes = "";
            string filtroAnio = "";
            string filtroTipo = "";

            if (cmbFiltroMes.SelectedItem != null)
            {
                filtroMes = cmbFiltroMes.SelectedItem.ToString();
            }

            if (cmbFiltroAnio.SelectedItem != null)
            {
                filtroAnio = cmbFiltroAnio.SelectedItem.ToString();
            }

            if (cmbFiltroTipoPago.SelectedItem != null)
            {
                filtroTipo = cmbFiltroTipoPago.SelectedItem.ToString();
            }

            string consulta =
                "SELECT " +
                "e.nombre AS estudiante, " +
                "p.tipoPago, " +
                "m.mesCorrespondiente, " +
                "IFNULL(m.anioCorrespondiente, a.anioCorrespondiente) AS anioCorrespondiente, " +
                "p.monto, " +
                "p.metodoPago, " +
                "p.numeroRecibo, " +
                "p.fechaPago " +
                "FROM Pago p " +
                "INNER JOIN Estudiante e ON p.idEstudiante = e.idEstudiante " +
                "LEFT JOIN Mensualidad m ON p.idMensualidad = m.idMensualidad " +
                "LEFT JOIN Anualidad a ON p.idAnualidad = a.idAnualidad " +
                "WHERE e.nombre LIKE @busqueda ";

            if (filtroMes != "" && filtroMes != "Todos los meses")
            {
                consulta += "AND m.mesCorrespondiente = @mes ";
            }

            if (filtroAnio != "" && filtroAnio != "Todos los años")
            {
                consulta += "AND IFNULL(m.anioCorrespondiente, a.anioCorrespondiente) = @anio ";
            }

            if (filtroTipo != "" && filtroTipo != "Todos los tipos")
            {
                consulta += "AND p.tipoPago = @tipoPago ";
            }

            consulta += "ORDER BY p.fechaPago DESC";

            try
            {
                using (MySqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    using (MySqlCommand comando = new MySqlCommand(consulta, cn))
                    {
                        comando.Parameters.AddWithValue("@busqueda", busqueda);

                        if (filtroMes != "" && filtroMes != "Todos los meses")
                        {
                            comando.Parameters.AddWithValue("@mes", ObtenerNumeroMes(filtroMes));
                        }

                        if (filtroAnio != "" && filtroAnio != "Todos los años")
                        {
                            comando.Parameters.AddWithValue("@anio", Convert.ToInt32(filtroAnio));
                        }

                        if (filtroTipo != "" && filtroTipo != "Todos los tipos")
                        {
                            comando.Parameters.AddWithValue("@tipoPago", filtroTipo);
                        }

                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                string estudiante = lector["estudiante"].ToString();
                                string tipoPago = lector["tipoPago"].ToString();

                                string mes = "-";

                                if (lector["mesCorrespondiente"] != DBNull.Value)
                                {
                                    mes = ObtenerNombreMes(Convert.ToInt32(lector["mesCorrespondiente"]));
                                }

                                string anio = "";

                                if (lector["anioCorrespondiente"] != DBNull.Value)
                                {
                                    anio = lector["anioCorrespondiente"].ToString();
                                }

                                decimal monto = Convert.ToDecimal(lector["monto"]);
                                string metodoPago = lector["metodoPago"].ToString();
                                string numeroRecibo = lector["numeroRecibo"].ToString();
                                DateTime fechaPago = Convert.ToDateTime(lector["fechaPago"]);

                                dgvPagos.Rows.Add(
                                    estudiante,
                                    tipoPago,
                                    mes,
                                    anio,
                                    "B/. " + monto.ToString("0.00"),
                                    metodoPago,
                                    numeroRecibo,
                                    fechaPago.ToString("dd/MM/yyyy")
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pagos: " + ex.Message);
            }
        }

        // Calcula el estado visual del estudiante en el dashboard
        private string CalcularEstadoVisual(string estadoEstudiante, string estadoMensualidad, string estadoAnualidad)
        {
            if (estadoEstudiante == "Ausente" || estadoEstudiante == "Pausado")
            {
                return "Ausente";
            }

            if (estadoEstudiante == "Restringido" || estadoMensualidad == "Vencida")
            {
                return "Restringido";
            }

            if (estadoMensualidad == "Pagada" && estadoAnualidad != "Pagada")
            {
                return "Revisar anualidad";
            }

            if (estadoMensualidad == "Pendiente")
            {
                return "Pendiente";
            }

            if (estadoMensualidad == "Pagada" && estadoAnualidad == "Pagada")
            {
                return "Al día";
            }

            return estadoEstudiante;
        }

        // Aplica color visual a una fila según su estado
        private void AplicarColorFila(DataGridViewRow fila, string estado)
        {
            Color colorTexto = Color.Black;
            Color colorFondo = Color.White;

            if (estado == "Al día")
            {
                colorTexto = Color.FromArgb(35, 170, 95);
                colorFondo = Color.FromArgb(235, 255, 245);
            }
            else if (estado == "Pendiente")
            {
                colorTexto = Color.FromArgb(220, 170, 20);
                colorFondo = Color.FromArgb(255, 252, 235);
            }
            else if (estado == "Restringido")
            {
                colorTexto = Color.FromArgb(230, 50, 60);
                colorFondo = Color.FromArgb(255, 240, 242);
            }
            else if (estado == "Ausente" || estado == "Pausado")
            {
                colorTexto = Color.FromArgb(60, 130, 230);
                colorFondo = Color.FromArgb(238, 245, 255);
            }
            else if (estado == "Revisar anualidad")
            {
                colorTexto = Color.FromArgb(230, 130, 0);
                colorFondo = Color.FromArgb(255, 247, 235);
            }

            fila.DefaultCellStyle.BackColor = colorFondo;

            if (fila.Cells.Count > 0)
            {
                fila.Cells[0].Style.ForeColor = colorTexto;
                fila.Cells[0].Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            }
        }

        // Convierte nombre del mes a número
        private int ObtenerNumeroMes(string mes)
        {
            if (mes == "Enero") return 1;
            if (mes == "Febrero") return 2;
            if (mes == "Marzo") return 3;
            if (mes == "Abril") return 4;
            if (mes == "Mayo") return 5;
            if (mes == "Junio") return 6;
            if (mes == "Julio") return 7;
            if (mes == "Agosto") return 8;
            if (mes == "Septiembre") return 9;
            if (mes == "Octubre") return 10;
            if (mes == "Noviembre") return 11;
            if (mes == "Diciembre") return 12;

            return 0;
        }

        // Convierte número del mes a nombre
        private string ObtenerNombreMes(int mes)
        {
            if (mes == 1) return "Enero";
            if (mes == 2) return "Febrero";
            if (mes == 3) return "Marzo";
            if (mes == 4) return "Abril";
            if (mes == 5) return "Mayo";
            if (mes == 6) return "Junio";
            if (mes == 7) return "Julio";
            if (mes == 8) return "Agosto";
            if (mes == 9) return "Septiembre";
            if (mes == 10) return "Octubre";
            if (mes == 11) return "Noviembre";
            if (mes == 12) return "Diciembre";

            return "-";
        }

        // Abre el formulario para registrar un nuevo estudiante
        private void BtnNuevoEstudiante_Click(object sender, EventArgs e)
        {
            FrmNuevoEstudiante formulario = new FrmNuevoEstudiante();
            formulario.ShowDialog();

            CargarTodo();
        }

        // Abre el formulario para registrar pago
        private void BtnRegistrarPago_Click(object sender, EventArgs e)
        {
            FrmRegistrarPago formulario = new FrmRegistrarPago();
            formulario.ShowDialog();

            CargarTodo();
        }

        // Abre el formulario para registrar ausencia temporal
        private void BtnAusenciaTemporal_Click(object sender, EventArgs e)
        {
            FrmAusenciaTemporal formulario = new FrmAusenciaTemporal();
            formulario.ShowDialog();

            CargarTodo();
        }

        // Abre el formulario para reactivar estudiante
        private void BtnReactivarEstudiante_Click(object sender, EventArgs e)
        {
            FrmReactivarEstudiante formulario = new FrmReactivarEstudiante();
            formulario.ShowDialog();

            CargarTodo();
        }

        // Recarga dashboard cuando cambian filtros
        private void FiltrosDashboard_Changed(object sender, EventArgs e)
        {
            CargarDashboard();
            CargarContadores();
        }

        // Recarga estudiantes cuando cambian filtros
        private void FiltrosEstudiantes_Changed(object sender, EventArgs e)
        {
            CargarEstudiantes();
        }

        // Recarga pagos cuando cambian filtros
        private void FiltrosPagos_Changed(object sender, EventArgs e)
        {
            CargarPagos();
        }

        // Elimina lógicamente un estudiante
        private void DgvEstudiantes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dgvEstudiantes.Columns[e.ColumnIndex].Name == "colAcciones")
            {
                int idEstudiante = Convert.ToInt32(dgvEstudiantes.Rows[e.RowIndex].Tag);

                DialogResult respuesta = MessageBox.Show(
                    "¿Deseas eliminar este estudiante?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    EliminarEstudiante(idEstudiante);
                    CargarTodo();
                }
            }
        }

        // No borra el historial; solo desactiva al estudiante
        private void EliminarEstudiante(int idEstudiante)
        {
            string consulta =
                "UPDATE Estudiante SET activo = 0 " +
                "WHERE idEstudiante = @idEstudiante";

            try
            {
                using (MySqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();

                    using (MySqlCommand comando = new MySqlCommand(consulta, cn))
                    {
                        comando.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Estudiante eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar estudiante: " + ex.Message);
            }
        }

        // Estos métodos quedan porque el Designer los está llamando
        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}