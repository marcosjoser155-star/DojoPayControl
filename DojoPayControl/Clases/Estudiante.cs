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
        private string apellido;
        private string cedula;
        private string telefono;
        private DateTime fechaIngreso;
        private string estado;
        private int activo;
        private decimal montoMensualidad;
        private decimal montoAnualidad;

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

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
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

        public decimal MontoMensualidad
        {
            get { return montoMensualidad; }
            set { montoMensualidad = value; }
        }

        public decimal MontoAnualidad
        {
            get { return montoAnualidad; }
            set { montoAnualidad = value; }
        }

        // Constructor vacío
        public Estudiante()
        {
            this.idEstudiante = 0;
            this.nombre = "";
            this.apellido = "";
            this.cedula = "";
            this.telefono = "";
            this.fechaIngreso = DateTime.Now;
            this.estado = "Pendiente";
            this.activo = 1;
            this.montoMensualidad = 0;
            this.montoAnualidad = 0;
        }

        // Constructor con parámetros
        public Estudiante(int idEstudiante, string nombre, string apellido,
                          string cedula, string telefono, DateTime fechaIngreso,
                          string estado, int activo, decimal montoMensualidad,
                          decimal montoAnualidad)
        {
            this.idEstudiante = idEstudiante;
            this.nombre = nombre;
            this.apellido = apellido;
            this.cedula = cedula;
            this.telefono = telefono;
            this.fechaIngreso = fechaIngreso;
            this.estado = estado;
            this.activo = activo;
            this.montoMensualidad = montoMensualidad;
            this.montoAnualidad = montoAnualidad;
        }

        // Método para registrar un nuevo estudiante en la base de datos
        public int Registrar()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "INSERT INTO Estudiante " +
                                  "(nombre, apellido, cedula, telefono, fechaIngreso, estado, activo, montoMensualidad, montoAnualidad) " +
                                  "VALUES " +
                                  "(@nombre, @apellido, @cedula, @telefono, @fechaIngreso, @estado, @activo, @montoMensualidad, @montoAnualidad)";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@nombre", this.nombre),
                    new MySqlParameter("@apellido", this.apellido),
                    new MySqlParameter("@cedula", this.cedula),
                    new MySqlParameter("@telefono", this.telefono),
                    new MySqlParameter("@fechaIngreso", this.fechaIngreso),
                    new MySqlParameter("@estado", this.estado),
                    new MySqlParameter("@activo", this.activo),
                    new MySqlParameter("@montoMensualidad", this.montoMensualidad),
                    new MySqlParameter("@montoAnualidad", this.montoAnualidad)
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

        // Método para editar los datos de un estudiante existente
        public bool Editar()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "UPDATE Estudiante SET " +
                                  "nombre = @nombre, " +
                                  "apellido = @apellido, " +
                                  "cedula = @cedula, " +
                                  "telefono = @telefono, " +
                                  "fechaIngreso = @fechaIngreso, " +
                                  "estado = @estado, " +
                                  "activo = @activo, " +
                                  "montoMensualidad = @montoMensualidad, " +
                                  "montoAnualidad = @montoAnualidad " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante),
                    new MySqlParameter("@nombre", this.nombre),
                    new MySqlParameter("@apellido", this.apellido),
                    new MySqlParameter("@cedula", this.cedula),
                    new MySqlParameter("@telefono", this.telefono),
                    new MySqlParameter("@fechaIngreso", this.fechaIngreso),
                    new MySqlParameter("@estado", this.estado),
                    new MySqlParameter("@activo", this.activo),
                    new MySqlParameter("@montoMensualidad", this.montoMensualidad),
                    new MySqlParameter("@montoAnualidad", this.montoAnualidad)
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
                string consulta = "SELECT idEstudiante, nombre, apellido, cedula, telefono, " +
                                  "fechaIngreso, estado, activo, montoMensualidad, montoAnualidad " +
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
                    this.apellido = tabla.Rows[0]["apellido"].ToString();
                    this.cedula = tabla.Rows[0]["cedula"].ToString();
                    this.telefono = tabla.Rows[0]["telefono"].ToString();
                    this.fechaIngreso = Convert.ToDateTime(tabla.Rows[0]["fechaIngreso"]);
                    this.estado = tabla.Rows[0]["estado"].ToString();
                    this.activo = Convert.ToInt32(tabla.Rows[0]["activo"]);
                    this.montoMensualidad = Convert.ToDecimal(tabla.Rows[0]["montoMensualidad"]);
                    this.montoAnualidad = Convert.ToDecimal(tabla.Rows[0]["montoAnualidad"]);

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

        // Método para listar todos los estudiantes activos
        public DataTable ListarEstudiantes()
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT idEstudiante, nombre, apellido, cedula, telefono, " +
                              "fechaIngreso, estado, montoMensualidad, montoAnualidad " +
                              "FROM Estudiante " +
                              "WHERE activo = 1 " +
                              "ORDER BY nombre, apellido";

            return conexion.EjecutarConsulta(consulta);
        }

        // Método para buscar estudiantes por nombre, apellido o cédula
        public DataTable BuscarEstudiantes(string texto)
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT idEstudiante, nombre, apellido, cedula, telefono, " +
                              "fechaIngreso, estado, montoMensualidad, montoAnualidad " +
                              "FROM Estudiante " +
                              "WHERE activo = 1 " +
                              "AND (nombre LIKE @texto OR apellido LIKE @texto OR cedula LIKE @texto) " +
                              "ORDER BY nombre, apellido";

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

            string consulta = "SELECT idEstudiante, nombre, apellido, cedula, telefono, " +
                              "fechaIngreso, estado, montoMensualidad, montoAnualidad " +
                              "FROM Estudiante " +
                              "WHERE activo = 1 " +
                              "AND estado = @estado " +
                              "ORDER BY nombre, apellido";

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

        // Método para marcar al estudiante en ausencia temporal
        public bool MarcarAusenciaTemporal()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "UPDATE Estudiante SET estado = 'Ausencia Temporal' " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@idEstudiante", this.idEstudiante)
                };

                conexion.EjecutarComando(consulta, parametros);

                this.estado = "Ausencia Temporal";

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

        // Método para cambiar manualmente el estado del estudiante
        public bool CambiarEstado(string nuevoEstado)
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "UPDATE Estudiante SET estado = @estado " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@estado", nuevoEstado),
                    new MySqlParameter("@idEstudiante", this.idEstudiante)
                };

                conexion.EjecutarComando(consulta, parametros);

                this.estado = nuevoEstado;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cambiar estado del estudiante: " + ex.Message);
                return false;
            }
        }

        // Método para validar los datos antes de registrar o editar
        public bool ValidarEstudiante()
        {
            if (!string.IsNullOrEmpty(this.nombre) &&
                !string.IsNullOrEmpty(this.apellido) &&
                !string.IsNullOrEmpty(this.cedula) &&
                !string.IsNullOrEmpty(this.telefono) &&
                !string.IsNullOrEmpty(this.estado) &&
                this.montoMensualidad >= 0 &&
                this.montoAnualidad >= 0)
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