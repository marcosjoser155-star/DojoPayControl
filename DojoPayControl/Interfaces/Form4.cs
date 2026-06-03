using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DojoPayControl.Clases;

namespace DojoPayControl.Interfaces
{
    public partial class FrmRegistrarPago : Form
    {
        // Atributo para saber si el pago será mensualidad o anualidad
        private string tipoPagoSeleccionado;

        public FrmRegistrarPago()
        {
            InitializeComponent();

            // Evita duplicar eventos si el Designer ya los tiene conectados
            this.Load -= FrmRegistrarPago_Load;
            this.Load += FrmRegistrarPago_Load;

            btnMensualidad.Click -= btnMensualidad_Click;
            btnMensualidad.Click += btnMensualidad_Click;

            btnAnualidad.Click -= btnAnualidad_Click;
            btnAnualidad.Click += btnAnualidad_Click;

            btnGuardar.Click -= btnGuardar_Click;
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.Click -= btnCancelar_Click;
            btnCancelar.Click += btnCancelar_Click;

            cmbEstudiante.SelectedIndexChanged -= cmbEstudiante_SelectedIndexChanged;
            cmbEstudiante.SelectedIndexChanged += cmbEstudiante_SelectedIndexChanged;

            cmbMes.SelectedIndexChanged -= cmbMes_SelectedIndexChanged;
            cmbMes.SelectedIndexChanged += cmbMes_SelectedIndexChanged;
        }

        private void FrmRegistrarPago_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarEstudiantes();
            SeleccionarTipoPago("Mensualidad");
        }

        private void ConfigurarFormulario()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            // Configurar meses
            cmbMes.Items.Clear();
            cmbMes.Items.Add("Enero");
            cmbMes.Items.Add("Febrero");
            cmbMes.Items.Add("Marzo");
            cmbMes.Items.Add("Abril");
            cmbMes.Items.Add("Mayo");
            cmbMes.Items.Add("Junio");
            cmbMes.Items.Add("Julio");
            cmbMes.Items.Add("Agosto");
            cmbMes.Items.Add("Septiembre");
            cmbMes.Items.Add("Octubre");
            cmbMes.Items.Add("Noviembre");
            cmbMes.Items.Add("Diciembre");
            cmbMes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMes.SelectedIndex = DateTime.Now.Month - 1;

            // Configurar año
            numAnio.Minimum = 2000;
            numAnio.Maximum = 2100;
            numAnio.Value = DateTime.Now.Year;

            // Configurar fecha de pago
            dateTimePicker1.Value = DateTime.Now;

            // Configurar métodos de pago
            cmbMetodoPago.Items.Clear();
            cmbMetodoPago.Items.Add("Efectivo");
            cmbMetodoPago.Items.Add("Transferencia");
            cmbMetodoPago.Items.Add("Yappy");
            cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodoPago.SelectedIndex = 0;

            // Limpiar recibo
            txtNumRecibo.Clear();

            // Monto inicial
            lblMontoAPagar.Text = "Monto a pagar: B/. 0.00";
        }

        private void CargarEstudiantes()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "SELECT " +
                                  "idEstudiante, " +
                                  "CONCAT(nombre, ' ', apellido) AS nombreCompleto, " +
                                  "montoMensualidad, " +
                                  "montoAnualidad " +
                                  "FROM Estudiante " +
                                  "WHERE activo = 1 " +
                                  "ORDER BY nombre, apellido";

                DataTable tabla = conexion.EjecutarConsulta(consulta);

                cmbEstudiante.DataSource = tabla;
                cmbEstudiante.DisplayMember = "nombreCompleto";
                cmbEstudiante.ValueMember = "idEstudiante";
                cmbEstudiante.DropDownStyle = ComboBoxStyle.DropDownList;

                if (cmbEstudiante.Items.Count > 0)
                {
                    cmbEstudiante.SelectedIndex = 0;
                }
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

        private void SeleccionarTipoPago(string tipoPago)
        {
            this.tipoPagoSeleccionado = tipoPago;

            if (tipoPago == "Mensualidad")
            {
                cmbMes.Enabled = true;
            }
            else
            {
                cmbMes.Enabled = false;
            }

            ActualizarMontoAPagar();
        }

        private void ActualizarMontoAPagar()
        {
            decimal monto = ObtenerMontoAPagar();

            lblMontoAPagar.Text = "Monto a pagar: B/. " + monto.ToString("0.00");
        }

        private decimal ObtenerMontoAPagar()
        {
            if (cmbEstudiante.SelectedItem == null)
            {
                return 0;
            }

            DataRowView fila = cmbEstudiante.SelectedItem as DataRowView;

            if (fila == null)
            {
                return 0;
            }

            if (this.tipoPagoSeleccionado == "Mensualidad")
            {
                if (fila["montoMensualidad"] != DBNull.Value)
                {
                    return Convert.ToDecimal(fila["montoMensualidad"]);
                }
            }

            if (this.tipoPagoSeleccionado == "Anualidad")
            {
                if (fila["montoAnualidad"] != DBNull.Value)
                {
                    return Convert.ToDecimal(fila["montoAnualidad"]);
                }
            }

            return 0;
        }

        private void btnMensualidad_Click(object sender, EventArgs e)
        {
            SeleccionarTipoPago("Mensualidad");
        }

        private void btnAnualidad_Click(object sender, EventArgs e)
        {
            SeleccionarTipoPago("Anualidad");
        }

        private void cmbEstudiante_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarMontoAPagar();
        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarMontoAPagar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos() == true)
            {
                GuardarPago();
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
                MessageBox.Show("Debe seleccionar un estudiante.");
                cmbEstudiante.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.tipoPagoSeleccionado))
            {
                MessageBox.Show("Debe seleccionar si el pago es mensualidad o anualidad.");
                return false;
            }

            if (this.tipoPagoSeleccionado == "Mensualidad" && cmbMes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el mes correspondiente.");
                cmbMes.Focus();
                return false;
            }

            if (cmbMetodoPago.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un método de pago.");
                cmbMetodoPago.Focus();
                return false;
            }

            if (ObtenerMontoAPagar() <= 0)
            {
                MessageBox.Show("El monto a pagar debe ser mayor que cero.");
                return false;
            }

            return true;
        }

        private void GuardarPago()
        {
            try
            {
                int idEstudiante = Convert.ToInt32(cmbEstudiante.SelectedValue);
                decimal monto = ObtenerMontoAPagar();
                DateTime fechaPago = dateTimePicker1.Value;
                string metodoPago = cmbMetodoPago.SelectedItem.ToString();
                string numeroRecibo = txtNumRecibo.Text.Trim();
                int anioCorrespondiente = Convert.ToInt32(numAnio.Value);
                int mesCorrespondiente = 0;

                if (this.tipoPagoSeleccionado == "Mensualidad")
                {
                    mesCorrespondiente = cmbMes.SelectedIndex + 1;
                }

                Pago pago = new Pago();

                pago.IdEstudiante = idEstudiante;
                pago.Monto = monto;
                pago.FechaPago = fechaPago;
                pago.MetodoPago = metodoPago;
                pago.TipoPago = this.tipoPagoSeleccionado;
                pago.NumeroRecibo = numeroRecibo;
                pago.MesCorrespondiente = mesCorrespondiente;
                pago.AnioCorrespondiente = anioCorrespondiente;
                pago.Observaciones = "";

                if (pago.ValidarPago() == false)
                {
                    MessageBox.Show(
                        "Los datos del pago no son válidos.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (pago.RegistrarPago() == true)
                {
                    MessageBox.Show(
                        "Pago registrado correctamente.",
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
                        "No se pudo registrar el pago.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar el pago: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}