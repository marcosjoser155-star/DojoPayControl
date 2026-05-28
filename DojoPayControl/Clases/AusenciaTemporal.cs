using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DojoPayControl.Clases
{
    public class AusenciaTemporal
    {
        // Atributos
        private int idAusencia;
        private int idEstudiante;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private string motivo;
        private string observacion;
        private string estadoAusencia;

        // Propiedades
        public int IdAusencia
        {
            get { return idAusencia; }
            set { idAusencia = value; }
        }

        public int IdEstudiante
        {
            get { return idEstudiante; }
            set { idEstudiante = value; }
        }

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }

        public string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }

        public string EstadoAusencia
        {
            get { return estadoAusencia; }
            set { estadoAusencia = value; }
        }

        // Constructor vacío
        public AusenciaTemporal()
        {
            this.estadoAusencia = "Activa";
        }

        // Constructor con parámetros
        public AusenciaTemporal(int idAusencia, int idEstudiante,
                                DateTime fechaInicio, DateTime fechaFin,
                                string motivo, string observacion,
                                string estadoAusencia)
        {
            this.idAusencia = idAusencia;
            this.idEstudiante = idEstudiante;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.motivo = motivo;
            this.observacion = observacion;
            this.estadoAusencia = estadoAusencia;
        }

        // Método para registrar una ausencia temporal
        public bool RegistrarAusenciaTemporal()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "INSERT INTO AusenciaTemporal " +
                                  "(idEstudiante, fechaInicio, fechaFin, motivo, observacion, estadoAusencia) " +
                                  "VALUES (@idEstudiante, @fechaInicio, @fechaFin, @motivo, @observacion, @estadoAusencia)";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante),
                    new MySqlParameter("@fechaInicio", this.fechaInicio),
                    new MySqlParameter("@fechaFin", this.fechaFin),
                    new MySqlParameter("@motivo", this.motivo),
                    new MySqlParameter("@observacion", string.IsNullOrEmpty(this.observacion) ? DBNull.Value : (object)this.observacion),
                    new MySqlParameter("@estadoAusencia", this.estadoAusencia)
                };

                conexion.EjecutarComando(consulta, parametros);

                Estudiante estudiante = new Estudiante();
                estudiante.IdEstudiante = this.idEstudiante;
                estudiante.MarcarAusenciaTemporal();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar ausencia temporal: " + ex.Message);
                return false;
            }
        }

        // Método para buscar una ausencia temporal por ID
        public bool BuscarAusenciaTemporal(int id)
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "SELECT idAusencia, idEstudiante, fechaInicio, fechaFin, motivo, observacion, estadoAusencia " +
                                  "FROM AusenciaTemporal " +
                                  "WHERE idAusencia = @idAusencia";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idAusencia", id)
                };

                DataTable tabla = conexion.EjecutarConsulta(consulta, parametros);

                if (tabla.Rows.Count > 0)
                {
                    this.idAusencia = Convert.ToInt32(tabla.Rows[0]["idAusencia"]);
                    this.idEstudiante = Convert.ToInt32(tabla.Rows[0]["idEstudiante"]);
                    this.fechaInicio = Convert.ToDateTime(tabla.Rows[0]["fechaInicio"]);
                    this.fechaFin = Convert.ToDateTime(tabla.Rows[0]["fechaFin"]);
                    this.motivo = tabla.Rows[0]["motivo"].ToString();
                    this.observacion = tabla.Rows[0]["observacion"].ToString();
                    this.estadoAusencia = tabla.Rows[0]["estadoAusencia"].ToString();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar ausencia temporal: " + ex.Message);
                return false;
            }
        }

        // Método para finalizar una ausencia temporal
        public bool FinalizarAusenciaTemporal()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                if (this.idEstudiante <= 0)
                {
                    BuscarAusenciaTemporal(this.idAusencia);
                }

                string consulta = "UPDATE AusenciaTemporal SET " +
                                  "fechaFin = @fechaFin, " +
                                  "estadoAusencia = 'Finalizada' " +
                                  "WHERE idAusencia = @idAusencia";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@fechaFin", this.fechaFin),
                    new MySqlParameter("@idAusencia", this.idAusencia)
                };

                conexion.EjecutarComando(consulta, parametros);

                Estudiante estudiante = new Estudiante();
                estudiante.IdEstudiante = this.idEstudiante;
                estudiante.Reactivar();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al finalizar ausencia temporal: " + ex.Message);
                return false;
            }
        }

        // Método para listar ausencias temporales activas
        public DataTable ListarAusenciasTemporales()
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT " +
                              "a.idAusencia, " +
                              "e.idEstudiante, " +
                              "e.nombre AS estudiante, " +
                              "a.fechaInicio, " +
                              "a.fechaFin, " +
                              "a.motivo, " +
                              "a.observacion, " +
                              "a.estadoAusencia " +
                              "FROM AusenciaTemporal a " +
                              "INNER JOIN Estudiante e ON a.idEstudiante = e.idEstudiante " +
                              "WHERE a.estadoAusencia = 'Activa' " +
                              "AND e.activo = 1 " +
                              "ORDER BY e.nombre";

            return conexion.EjecutarConsulta(consulta);
        }

        // Método para validar datos antes de registrar la ausencia temporal
        public bool ValidarAusenciaTemporal()
        {
            if (this.idEstudiante > 0 &&
                !string.IsNullOrEmpty(this.motivo) &&
                this.fechaFin >= this.fechaInicio)
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