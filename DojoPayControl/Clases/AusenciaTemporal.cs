using MySql.Data.MySqlClient;
using System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Constructor vacío
        public AusenciaTemporal()
        {

        }

        // Constructor con parámetros
        public AusenciaTemporal(int idAusencia, int idEstudiante,
                                DateTime fechaInicio, DateTime fechaFin,
                                string motivo)
        {
            this.idAusencia = idAusencia;
            this.idEstudiante = idEstudiante;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.motivo = motivo;
        }

        // Método RegistrarAusencia
        public void RegistrarAusencia()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                conexion.AbrirConexion();

                string consulta = "INSERT INTO AusenciaTemporal " +
                                  "(idEstudiante, fechaInicio, fechaFin, motivo) " +
                                  "VALUES (@idEstudiante, @fechaInicio, @fechaFin, @motivo)";

                MySqlCommand comando = new MySqlCommand(consulta, conexion.Conexion);

                comando.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);
                comando.Parameters.AddWithValue("@motivo", motivo);

                comando.ExecuteNonQuery();

                Console.WriteLine("Ausencia temporal registrada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar ausencia: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Método FinalizarAusencia
        public void FinalizarAusencia()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                conexion.AbrirConexion();

                string consulta = "UPDATE AusenciaTemporal SET " +
                                  "fechaFin = @fechaFin " +
                                  "WHERE idAusencia = @idAusencia";

                MySqlCommand comando = new MySqlCommand(consulta, conexion.Conexion);

                comando.Parameters.AddWithValue("@fechaFin", fechaFin);
                comando.Parameters.AddWithValue("@idAusencia", idAusencia);

                comando.ExecuteNonQuery();

                Console.WriteLine("Ausencia temporal finalizada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al finalizar ausencia: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}