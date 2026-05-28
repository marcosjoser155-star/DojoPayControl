using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DojoPayControl.Clases
{
    public class Pago
    {
        // Atributos
        private int idPago;
        private string numeroRecibo;
        private int idEstudiante;
        private decimal monto;
        private DateTime fechaPago;
        private string metodoPago;
        private string tipoPago;
        private int mesCorrespondiente;
        private int anioCorrespondiente;
        private string observaciones;

        // Propiedades
        public int IdPago
        {
            get { return idPago; }
            set { idPago = value; }
        }

        public string NumeroRecibo
        {
            get { return numeroRecibo; }
            set { numeroRecibo = value; }
        }

        public int IdEstudiante
        {
            get { return idEstudiante; }
            set { idEstudiante = value; }
        }

        public decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public DateTime FechaPago
        {
            get { return fechaPago; }
            set { fechaPago = value; }
        }

        public string MetodoPago
        {
            get { return metodoPago; }
            set { metodoPago = value; }
        }

        public string TipoPago
        {
            get { return tipoPago; }
            set { tipoPago = value; }
        }

        public int MesCorrespondiente
        {
            get { return mesCorrespondiente; }
            set { mesCorrespondiente = value; }
        }

        public int AnioCorrespondiente
        {
            get { return anioCorrespondiente; }
            set { anioCorrespondiente = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        // Constructor vacío
        public Pago()
        {
            this.fechaPago = DateTime.Now;
        }

        // Constructor con parámetros
        public Pago(int idPago, string numeroRecibo, int idEstudiante,
                    decimal monto, DateTime fechaPago, string metodoPago,
                    string tipoPago, int mesCorrespondiente,
                    int anioCorrespondiente, string observaciones)
        {
            this.idPago = idPago;
            this.numeroRecibo = numeroRecibo;
            this.idEstudiante = idEstudiante;
            this.monto = monto;
            this.fechaPago = fechaPago;
            this.metodoPago = metodoPago;
            this.tipoPago = tipoPago;
            this.mesCorrespondiente = mesCorrespondiente;
            this.anioCorrespondiente = anioCorrespondiente;
            this.observaciones = observaciones;
        }

        // Método para registrar un pago
        public bool RegistrarPago()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "INSERT INTO Pago " +
                                  "(numeroRecibo, idEstudiante, monto, fechaPago, metodoPago, tipoPago, mesCorrespondiente, anioCorrespondiente, observaciones) " +
                                  "VALUES (@numeroRecibo, @idEstudiante, @monto, @fechaPago, @metodoPago, @tipoPago, @mesCorrespondiente, @anioCorrespondiente, @observaciones)";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@numeroRecibo", string.IsNullOrEmpty(this.numeroRecibo) ? DBNull.Value : (object)this.numeroRecibo),
                    new MySqlParameter("@idEstudiante", this.idEstudiante),
                    new MySqlParameter("@monto", this.monto),
                    new MySqlParameter("@fechaPago", this.fechaPago),
                    new MySqlParameter("@metodoPago", this.metodoPago),
                    new MySqlParameter("@tipoPago", this.tipoPago),
                    new MySqlParameter("@mesCorrespondiente", this.tipoPago == "Mensualidad" ? (object)this.mesCorrespondiente : DBNull.Value),
                    new MySqlParameter("@anioCorrespondiente", this.anioCorrespondiente),
                    new MySqlParameter("@observaciones", string.IsNullOrEmpty(this.observaciones) ? DBNull.Value : (object)this.observaciones)
                };

                conexion.EjecutarComando(consulta, parametros);

                if (this.tipoPago == "Mensualidad")
                {
                    RegistrarMensualidadPagada();
                }

                if (this.tipoPago == "Anualidad")
                {
                    RegistrarAnualidadPagada();
                }

                ActualizarEstadoEstudiante();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar pago: " + ex.Message);
                return false;
            }
        }

        // Método privado para registrar la mensualidad como pagada
        private void RegistrarMensualidadPagada()
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "INSERT INTO Mensualidad " +
                              "(idEstudiante, mesCorrespondiente, anioCorrespondiente, monto, fechaLimite, estado) " +
                              "VALUES (@idEstudiante, @mesCorrespondiente, @anioCorrespondiente, @monto, @fechaLimite, 'Pagada') " +
                              "ON DUPLICATE KEY UPDATE " +
                              "monto = @monto, " +
                              "fechaLimite = @fechaLimite, " +
                              "estado = 'Pagada'";

            DateTime fechaLimite = new DateTime(this.anioCorrespondiente, this.mesCorrespondiente, 7);

            MySqlParameter[] parametros = new MySqlParameter[]
            {
                new MySqlParameter("@idEstudiante", this.idEstudiante),
                new MySqlParameter("@mesCorrespondiente", this.mesCorrespondiente),
                new MySqlParameter("@anioCorrespondiente", this.anioCorrespondiente),
                new MySqlParameter("@monto", this.monto),
                new MySqlParameter("@fechaLimite", fechaLimite)
            };

            conexion.EjecutarComando(consulta, parametros);
        }

        // Método privado para registrar la anualidad como pagada
        private void RegistrarAnualidadPagada()
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "INSERT INTO Anualidad " +
                              "(idEstudiante, anioCorrespondiente, monto, fechaLimite, estado) " +
                              "VALUES (@idEstudiante, @anioCorrespondiente, @monto, @fechaLimite, 'Pagada') " +
                              "ON DUPLICATE KEY UPDATE " +
                              "monto = @monto, " +
                              "fechaLimite = @fechaLimite, " +
                              "estado = 'Pagada'";

            DateTime fechaLimite = new DateTime(this.anioCorrespondiente, 1, 7);

            MySqlParameter[] parametros = new MySqlParameter[]
            {
                new MySqlParameter("@idEstudiante", this.idEstudiante),
                new MySqlParameter("@anioCorrespondiente", this.anioCorrespondiente),
                new MySqlParameter("@monto", this.monto),
                new MySqlParameter("@fechaLimite", fechaLimite)
            };

            conexion.EjecutarComando(consulta, parametros);
        }

        // Método privado para actualizar el estado general del estudiante
        private void ActualizarEstadoEstudiante()
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "UPDATE Estudiante SET estado = " +
                              "CASE " +
                              "WHEN estado = 'Pausado' THEN 'Pausado' " +
                              "WHEN NOT EXISTS (SELECT 1 FROM Mensualidad WHERE idEstudiante = @idEstudiante AND mesCorrespondiente = MONTH(CURDATE()) AND anioCorrespondiente = YEAR(CURDATE()) AND estado = 'Pagada') AND DAY(CURDATE()) > 7 THEN 'Restringido' " +
                              "WHEN NOT EXISTS (SELECT 1 FROM Mensualidad WHERE idEstudiante = @idEstudiante AND mesCorrespondiente = MONTH(CURDATE()) AND anioCorrespondiente = YEAR(CURDATE()) AND estado = 'Pagada') THEN 'Pendiente' " +
                              "WHEN NOT EXISTS (SELECT 1 FROM Anualidad WHERE idEstudiante = @idEstudiante AND anioCorrespondiente = YEAR(CURDATE()) AND estado = 'Pagada') THEN 'Revisar anualidad' " +
                              "ELSE 'Al día' " +
                              "END " +
                              "WHERE idEstudiante = @idEstudiante";

            MySqlParameter[] parametros = new MySqlParameter[]
            {
                new MySqlParameter("@idEstudiante", this.idEstudiante)
            };

            conexion.EjecutarComando(consulta, parametros);
        }

        // Método para listar pagos en el Form Pagos
        public DataTable ListarPagos()
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT " +
                              "p.idPago, " +
                              "e.nombre AS estudiante, " +
                              "p.tipoPago, " +
                              "CASE p.mesCorrespondiente " +
                              "WHEN 1 THEN 'Enero' " +
                              "WHEN 2 THEN 'Febrero' " +
                              "WHEN 3 THEN 'Marzo' " +
                              "WHEN 4 THEN 'Abril' " +
                              "WHEN 5 THEN 'Mayo' " +
                              "WHEN 6 THEN 'Junio' " +
                              "WHEN 7 THEN 'Julio' " +
                              "WHEN 8 THEN 'Agosto' " +
                              "WHEN 9 THEN 'Septiembre' " +
                              "WHEN 10 THEN 'Octubre' " +
                              "WHEN 11 THEN 'Noviembre' " +
                              "WHEN 12 THEN 'Diciembre' " +
                              "ELSE '-' END AS mes, " +
                              "p.anioCorrespondiente AS anio, " +
                              "p.monto, " +
                              "p.metodoPago, " +
                              "p.numeroRecibo, " +
                              "p.fechaPago " +
                              "FROM Pago p " +
                              "INNER JOIN Estudiante e ON p.idEstudiante = e.idEstudiante " +
                              "WHERE e.activo = 1 " +
                              "ORDER BY p.fechaPago DESC";

            return conexion.EjecutarConsulta(consulta);
        }

        // Método para filtrar pagos por estudiante, mes, año y tipo
        public DataTable FiltrarPagos(string texto, int mes, int anio, string tipo)
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT " +
                              "p.idPago, " +
                              "e.nombre AS estudiante, " +
                              "p.tipoPago, " +
                              "CASE p.mesCorrespondiente " +
                              "WHEN 1 THEN 'Enero' " +
                              "WHEN 2 THEN 'Febrero' " +
                              "WHEN 3 THEN 'Marzo' " +
                              "WHEN 4 THEN 'Abril' " +
                              "WHEN 5 THEN 'Mayo' " +
                              "WHEN 6 THEN 'Junio' " +
                              "WHEN 7 THEN 'Julio' " +
                              "WHEN 8 THEN 'Agosto' " +
                              "WHEN 9 THEN 'Septiembre' " +
                              "WHEN 10 THEN 'Octubre' " +
                              "WHEN 11 THEN 'Noviembre' " +
                              "WHEN 12 THEN 'Diciembre' " +
                              "ELSE '-' END AS mes, " +
                              "p.anioCorrespondiente AS anio, " +
                              "p.monto, " +
                              "p.metodoPago, " +
                              "p.numeroRecibo, " +
                              "p.fechaPago " +
                              "FROM Pago p " +
                              "INNER JOIN Estudiante e ON p.idEstudiante = e.idEstudiante " +
                              "WHERE e.activo = 1 " +
                              "AND e.nombre LIKE @texto ";

            if (mes > 0)
            {
                consulta += "AND p.mesCorrespondiente = @mes ";
            }

            if (anio > 0)
            {
                consulta += "AND p.anioCorrespondiente = @anio ";
            }

            if (tipo != "Todos los tipos")
            {
                consulta += "AND p.tipoPago = @tipo ";
            }

            consulta += "ORDER BY p.fechaPago DESC";

            MySqlParameter[] parametros = new MySqlParameter[]
            {
                new MySqlParameter("@texto", "%" + texto + "%"),
                new MySqlParameter("@mes", mes),
                new MySqlParameter("@anio", anio),
                new MySqlParameter("@tipo", tipo)
            };

            return conexion.EjecutarConsulta(consulta, parametros);
        }

        // Método para validar datos antes de guardar el pago
        public bool ValidarPago()
        {
            if (this.idEstudiante > 0 &&
                this.monto > 0 &&
                !string.IsNullOrEmpty(this.metodoPago) &&
                !string.IsNullOrEmpty(this.tipoPago) &&
                this.anioCorrespondiente > 0)
            {
                if (this.tipoPago == "Mensualidad" && this.mesCorrespondiente <= 0)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}