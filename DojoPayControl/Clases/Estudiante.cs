using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //Conectar C# con SQL Server
                conexion.AbrirConexion();

                string consulta = "INSERT INTO Estudiantes " +
                                  "(Nombre, Cedula, Telefono, FechaIngreso, Estado) " +
                                  "VALUES (@Nombre, @Cedula, @Telefono, @FechaIngreso, @Estado)";
                // Crear un comando SQL
                SqlCommand comando = new SqlCommand(consulta, conexion.Conexion);
                // Agregar parámetros al comando
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Cedula", cedula);
                comando.Parameters.AddWithValue("@Telefono", telefono);
                comando.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);
                comando.Parameters.AddWithValue("@Estado", estado);

                // Ejecutar el comando
                comando.ExecuteNonQuery();
                // Mostrar mensaje de éxito
                Console.WriteLine("Estudiante registrado correctamente.");
            }// Manejar excepciones
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Método Editar
        public void Editar()
        {
            Console.WriteLine("Método Editar en construcción.");
        }

        // Método Buscar
        public void Buscar(int id)
        {
            Console.WriteLine("Buscando estudiante con ID: " + id);
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
            estado = "Activo";
        }
    }
}
    

