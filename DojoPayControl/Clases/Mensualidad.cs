using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DojoPayControl.Clases
{
    public class Mensualidad
    {
        // Atributos
        private int idMensualidad;
        private int idEstudiante;
        private int mesCorrespondiente;
        private int anioCorrespondiente;
        private decimal monto;
        private DateTime fechaLimite;
        private string estado;

        // Propiedades
        public int IdMensualidad
        {
            get { return idMensualidad; }
            set { idMensualidad = value; }
        }

        public int IdEstudiante
        {
            get { return idEstudiante; }
            set { idEstudiante = value; }
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

        public decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public DateTime FechaLimite
        {
            get { return fechaLimite; }
            set { fechaLimite = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        // Constructor vacío
        public Mensualidad()
        {
            this.estado = "Pendiente";
        }

        // Constructor con parámetros
        public Mensualidad(int idMensualidad, int idEstudiante,
                           int mesCorrespondiente, int anioCorrespondiente,
                           decimal monto, DateTime fechaLimite, string estado)
        {
            this.idMensualidad = idMensualidad;
            this.idEstudiante = idEstudiante;
            this.mesCorrespondiente = mesCorrespondiente;
            this.anioCorrespondiente = anioCorrespondiente;
            this.monto = monto;
            this.fechaLimite = fechaLimite;
            this.estado = estado;
        }

        // Método para registrar o actualizar una mensualidad
        public bool RegistrarMensualidad()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "INSERT INTO Mensualidad " +
                                  "(idEstudiante, mesCorrespondiente, anioCorrespondiente, monto, fechaLimite, estado) " +
                                  "VALUES (@idEstudiante, @mesCorrespondiente, @anioCorrespondiente, @monto, @fechaLimite, @estado) " +
                                  "ON DUPLICATE KEY UPDATE " +
                                  "monto = @monto, " +
                                  "fechaLimite = @fechaLimite, " +
                                  "estado = @estado";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante),
                    new MySqlParameter("@mesCorrespondiente", this.mesCorrespondiente),
                    new MySqlParameter("@anioCorrespondiente", this.anioCorrespondiente),
                    new MySqlParameter("@monto", this.monto),
                    new MySqlParameter("@fechaLimite", this.fechaLimite),
                    new MySqlParameter("@estado", this.estado)
                };

                conexion.EjecutarComando(consulta, parametros);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar mensualidad: " + ex.Message);
                return false;
            }
        }

        // Método para buscar una mensualidad por estudiante, mes y año
        public bool BuscarMensualidad()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "SELECT idMensualidad, idEstudiante, mesCorrespondiente, anioCorrespondiente, monto, fechaLimite, estado " +
                                  "FROM Mensualidad " +
                                  "WHERE idEstudiante = @idEstudiante " +
                                  "AND mesCorrespondiente = @mesCorrespondiente " +
                                  "AND anioCorrespondiente = @anioCorrespondiente";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante),
                    new MySqlParameter("@mesCorrespondiente", this.mesCorrespondiente),
                    new MySqlParameter("@anioCorrespondiente", this.anioCorrespondiente)
                };

                DataTable tabla = conexion.EjecutarConsulta(consulta, parametros);

                if (tabla.Rows.Count > 0)
                {
                    this.idMensualidad = Convert.ToInt32(tabla.Rows[0]["idMensualidad"]);
                    this.idEstudiante = Convert.ToInt32(tabla.Rows[0]["idEstudiante"]);
                    this.mesCorrespondiente = Convert.ToInt32(tabla.Rows[0]["mesCorrespondiente"]);
                    this.anioCorrespondiente = Convert.ToInt32(tabla.Rows[0]["anioCorrespondiente"]);
                    this.monto = Convert.ToDecimal(tabla.Rows[0]["monto"]);
                    this.fechaLimite = Convert.ToDateTime(tabla.Rows[0]["fechaLimite"]);
                    this.estado = tabla.Rows[0]["estado"].ToString();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar mensualidad: " + ex.Message);
                return false;
            }
        }

        // Método para validar los datos de mensualidad
        public bool ValidarMensualidad()
        {
            if (this.idEstudiante > 0 &&
                this.mesCorrespondiente >= 1 &&
                this.mesCorrespondiente <= 12 &&
                this.anioCorrespondiente > 0 &&
                this.monto >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}