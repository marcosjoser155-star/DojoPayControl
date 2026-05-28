using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DojoPayControl.Clases
{
    public class Estudiante
    {
        // Atributos
        private int idEstudiante;
        private string nombre;
        private string cedula;
        private string telefono;
        private DateTime fechaIngreso;
        private string estado;
        private int activo;

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

        public int Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        // Constructor vacío
        public Estudiante()
        {
            this.estado = "Pendiente";
            this.activo = 1;
        }

        // Constructor con parámetros
        public Estudiante(int idEstudiante, string nombre, string cedula,
                          string telefono, DateTime fechaIngreso,
                          string estado, int activo)
        {
            this.idEstudiante = idEstudiante;
            this.nombre = nombre;
            this.cedula = cedula;
            this.telefono = telefono;
            this.fechaIngreso = fechaIngreso;
            this.estado = estado;
            this.activo = activo;
        }

        // Método para registrar un estudiante nuevo
        public int Registrar()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "INSERT INTO Estudiante " +
                                  "(nombre, cedula, telefono, fechaIngreso, estado, activo) " +
                                  "VALUES (@nombre, @cedula, @telefono, @fechaIngreso, @estado, @activo)";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@nombre", this.nombre),
                    new MySqlParameter("@cedula", this.cedula),
                    new MySqlParameter("@telefono", this.telefono),
                    new MySqlParameter("@fechaIngreso", this.fechaIngreso),
                    new MySqlParameter("@estado", this.estado),
                    new MySqlParameter("@activo", this.activo)
                };

                this.idEstudiante = conexion.EjecutarComandoRetornaId(consulta, parametros);

                return this.idEstudiante;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar estudiante: " + ex.Message);
                return 0;
            }
        }

        // Método para editar los datos de un estudiante
        public bool Editar()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "UPDATE Estudiante SET " +
                                  "nombre = @nombre, " +
                                  "cedula = @cedula, " +
                                  "telefono = @telefono, " +
                                  "fechaIngreso = @fechaIngreso, " +
                                  "estado = @estado, " +
                                  "activo = @activo " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante),
                    new MySqlParameter("@nombre", this.nombre),
                    new MySqlParameter("@cedula", this.cedula),
                    new MySqlParameter("@telefono", this.telefono),
                    new MySqlParameter("@fechaIngreso", this.fechaIngreso),
                    new MySqlParameter("@estado", this.estado),
                    new MySqlParameter("@activo", this.activo)
                };

                conexion.EjecutarComando(consulta, parametros);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al editar estudiante: " + ex.Message);
                return false;
            }
        }

        // Método para buscar un estudiante por ID
        public bool Buscar(int id)
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "SELECT idEstudiante, nombre, cedula, telefono, fechaIngreso, estado, activo " +
                                  "FROM Estudiante " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", id)
                };

                DataTable tabla = conexion.EjecutarConsulta(consulta, parametros);

                if (tabla.Rows.Count > 0)
                {
                    this.idEstudiante = Convert.ToInt32(tabla.Rows[0]["idEstudiante"]);
                    this.nombre = tabla.Rows[0]["nombre"].ToString();
                    this.cedula = tabla.Rows[0]["cedula"].ToString();
                    this.telefono = tabla.Rows[0]["telefono"].ToString();
                    this.fechaIngreso = Convert.ToDateTime(tabla.Rows[0]["fechaIngreso"]);
                    this.estado = tabla.Rows[0]["estado"].ToString();
                    this.activo = Convert.ToInt32(tabla.Rows[0]["activo"]);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar estudiante: " + ex.Message);
                return false;
            }
        }

        // Método para listar estudiantes activos en el Form Estudiantes
        public DataTable ListarEstudiantes()
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT idEstudiante, nombre, cedula, telefono, fechaIngreso, estado " +
                              "FROM Estudiante " +
                              "WHERE activo = 1 " +
                              "ORDER BY nombre";

            return conexion.EjecutarConsulta(consulta);
        }

        // Método para buscar estudiantes por nombre o cédula
        public DataTable BuscarEstudiantes(string texto)
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT idEstudiante, nombre, cedula, telefono, fechaIngreso, estado " +
                              "FROM Estudiante " +
                              "WHERE activo = 1 " +
                              "AND (nombre LIKE @texto OR cedula LIKE @texto) " +
                              "ORDER BY nombre";

            MySqlParameter[] parametros = new MySqlParameter[]
            {
                new MySqlParameter("@texto", "%" + texto + "%")
            };

            return conexion.EjecutarConsulta(consulta, parametros);
        }

        // Método para filtrar estudiantes por estado
        public DataTable FiltrarPorEstado(string estadoFiltro)
        {
            ConexionDB conexion = new ConexionDB();

            if (estadoFiltro == "Todos los estados")
            {
                return ListarEstudiantes();
            }

            string consulta = "SELECT idEstudiante, nombre, cedula, telefono, fechaIngreso, estado " +
                              "FROM Estudiante " +
                              "WHERE activo = 1 " +
                              "AND estado = @estado " +
                              "ORDER BY nombre";

            MySqlParameter[] parametros = new MySqlParameter[]
            {
                new MySqlParameter("@estado", estadoFiltro)
            };

            return conexion.EjecutarConsulta(consulta, parametros);
        }

        // Método para eliminar de forma lógica un estudiante
        public bool Eliminar()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "UPDATE Estudiante SET activo = 0 " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante)
                };

                conexion.EjecutarComando(consulta, parametros);

                this.activo = 0;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar estudiante: " + ex.Message);
                return false;
            }
        }

        // Método para marcar al estudiante como pausado por ausencia temporal
        public bool MarcarAusenciaTemporal()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "UPDATE Estudiante SET estado = 'Pausado' " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante)
                };

                conexion.EjecutarComando(consulta, parametros);

                this.estado = "Pausado";

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al marcar ausencia temporal: " + ex.Message);
                return false;
            }
        }

        // Método para reactivar al estudiante después de una ausencia temporal
        public bool Reactivar()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "UPDATE Estudiante SET estado = 'Pendiente' " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante)
                };

                conexion.EjecutarComando(consulta, parametros);

                this.estado = "Pendiente";

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reactivar estudiante: " + ex.Message);
                return false;
            }
        }

        // Método para validar los datos antes de registrar
        public bool ValidarEstudiante()
        {
            if (!string.IsNullOrEmpty(this.nombre) &&
                !string.IsNullOrEmpty(this.cedula) &&
                !string.IsNullOrEmpty(this.telefono))
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