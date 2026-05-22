using System;
using MySql.Data.MySqlClient;

namespace DojoPayControl.Clases
{
    internal class Estudiante
    {
        // Atributos
        private int idEstudiante;
        private string nombre;
        private string cedula;
        private string telefono;
        private DateTime fechaIngreso;
        private string estado;

        // Propiedades
        public int IdEstudiante
        {
            get { return idEstudiante; }
            set { idEstudiante = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public DateTime FechaIngreso
        {
            get { return fechaIngreso; }
            set { fechaIngreso = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        // Constructor vacío
        public Estudiante()
        {

        }

        // Constructor con parámetros
        public Estudiante(int idEstudiante, string nombre, string cedula,
                           string telefono, DateTime fechaIngreso, string estado)
        {
            this.idEstudiante = idEstudiante;
            this.nombre = nombre;
            this.cedula = cedula;
            this.telefono = telefono;
            this.fechaIngreso = fechaIngreso;
            this.estado = estado;
        }

        // Método Registrar
        public void Registrar()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                // Conectar C# con MySQL
                conexion.AbrirConexion();

                string consulta = "INSERT INTO Estudiante " +
                                  "(nombre, cedula, telefono, fechaIngreso, estado) " +
                                  "VALUES (@nombre, @cedula, @telefono, @fechaIngreso, @estado)";

                // Crear un comando MySQL
                MySqlCommand comando = new MySqlCommand(consulta, conexion.Conexion);

                // Agregar parámetros al comando
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@cedula", cedula);
                comando.Parameters.AddWithValue("@telefono", telefono);
                comando.Parameters.AddWithValue("@fechaIngreso", fechaIngreso);
                comando.Parameters.AddWithValue("@estado", estado);

                // Ejecutar el comando
                comando.ExecuteNonQuery();

                Console.WriteLine("Estudiante registrado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar estudiante: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Método Editar
        public void Editar()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                conexion.AbrirConexion();

                string consulta = "UPDATE Estudiante SET " +
                                  "nombre = @nombre, " +
                                  "cedula = @cedula, " +
                                  "telefono = @telefono, " +
                                  "fechaIngreso = @fechaIngreso, " +
                                  "estado = @estado " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlCommand comando = new MySqlCommand(consulta, conexion.Conexion);

                comando.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@cedula", cedula);
                comando.Parameters.AddWithValue("@telefono", telefono);
                comando.Parameters.AddWithValue("@fechaIngreso", fechaIngreso);
                comando.Parameters.AddWithValue("@estado", estado);

                comando.ExecuteNonQuery();

                Console.WriteLine("Estudiante editado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al editar estudiante: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Método Buscar
        public void Buscar(int id)
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                conexion.AbrirConexion();

                string consulta = "SELECT idEstudiante, nombre, cedula, telefono, fechaIngreso, estado " +
                                  "FROM Estudiante " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlCommand comando = new MySqlCommand(consulta, conexion.Conexion);
                comando.Parameters.AddWithValue("@idEstudiante", id);

                MySqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    idEstudiante = Convert.ToInt32(lector["idEstudiante"]);
                    nombre = lector["nombre"].ToString();
                    cedula = lector["cedula"].ToString();
                    telefono = lector["telefono"].ToString();
                    fechaIngreso = Convert.ToDateTime(lector["fechaIngreso"]);
                    estado = lector["estado"].ToString();

                    Console.WriteLine("Estudiante encontrado.");
                }
                else
                {
                    Console.WriteLine("No se encontró el estudiante.");
                }

                lector.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar estudiante: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Método CalcularEstado
        public string CalcularEstado()
        {
            return estado;
        }

        // Método Pausar
        public void Pausar()
        {
            estado = "Pausado";
        }

        // Método Reactivar
        public void Reactivar()
        {
            estado = "Pendiente";
        }
    }
}