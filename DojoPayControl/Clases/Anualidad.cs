using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DojoPayControl.Clases
{
    public class Anualidad
    {
        // Atributos
        private int idAnualidad;
        private int idEstudiante;
        private int anioCorrespondiente;
        private decimal monto;
        private DateTime fechaLimite;
        private string estado;

        // Propiedades
        public int IdAnualidad
        {
            get { return idAnualidad; }
            set { idAnualidad = value; }
        }

        public int IdEstudiante
        {
            get { return idEstudiante; }
            set { idEstudiante = value; }
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
        public Anualidad()
        {
            this.estado = "Pendiente";
        }

        // Constructor con parámetros
        public Anualidad(int idAnualidad, int idEstudiante,
                         int anioCorrespondiente, decimal monto,
                         DateTime fechaLimite, string estado)
        {
            this.idAnualidad = idAnualidad;
            this.idEstudiante = idEstudiante;
            this.anioCorrespondiente = anioCorrespondiente;
            this.monto = monto;
            this.fechaLimite = fechaLimite;
            this.estado = estado;
        }

        // Método para registrar o actualizar una anualidad
        public bool RegistrarAnualidad()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "INSERT INTO Anualidad " +
                                  "(idEstudiante, anioCorrespondiente, monto, fechaLimite, estado) " +
                                  "VALUES (@idEstudiante, @anioCorrespondiente, @monto, @fechaLimite, @estado) " +
                                  "ON DUPLICATE KEY UPDATE " +
                                  "monto = @monto, " +
                                  "fechaLimite = @fechaLimite, " +
                                  "estado = @estado";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante),
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
                Console.WriteLine("Error al registrar anualidad: " + ex.Message);
                return false;
            }
        }

        // Método para buscar una anualidad por estudiante y año
        public bool BuscarAnualidad()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "SELECT idAnualidad, idEstudiante, anioCorrespondiente, monto, fechaLimite, estado " +
                                  "FROM Anualidad " +
                                  "WHERE idEstudiante = @idEstudiante " +
                                  "AND anioCorrespondiente = @anioCorrespondiente";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante),
                    new MySqlParameter("@anioCorrespondiente", this.anioCorrespondiente)
                };

                DataTable tabla = conexion.EjecutarConsulta(consulta, parametros);

                if (tabla.Rows.Count > 0)
                {
                    this.idAnualidad = Convert.ToInt32(tabla.Rows[0]["idAnualidad"]);
                    this.idEstudiante = Convert.ToInt32(tabla.Rows[0]["idEstudiante"]);
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
                Console.WriteLine("Error al buscar anualidad: " + ex.Message);
                return false;
            }
        }

        // Método para validar los datos de anualidad
        public bool ValidarAnualidad()
        {
            if (this.idEstudiante > 0 &&
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