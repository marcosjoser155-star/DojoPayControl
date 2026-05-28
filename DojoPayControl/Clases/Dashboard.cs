using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DojoPayControl.Clases
{
    public class Dashboard
    {
        // Atributos para los contadores del dashboard
        private int alDia;
        private int pendiente;
        private int restringido;
        private int pausados;
        private int revisarAnualidad;

        // Propiedades
        public int AlDia
        {
            get { return alDia; }
            set { alDia = value; }
        }

        public int Pendiente
        {
            get { return pendiente; }
            set { pendiente = value; }
        }

        public int Restringido
        {
            get { return restringido; }
            set { restringido = value; }
        }

        public int Pausados
        {
            get { return pausados; }
            set { pausados = value; }
        }

        public int RevisarAnualidad
        {
            get { return revisarAnualidad; }
            set { revisarAnualidad = value; }
        }

        // Constructor vacío
        public Dashboard()
        {

        }

        // Método para actualizar estados automáticamente según pagos y fecha
        public void ActualizarEstadosAutomaticos()
        {
            ConexionDB conexion = new ConexionDB();

            string consulta = "UPDATE Estudiante e SET e.estado = " +
                              "CASE " +
                              "WHEN e.estado = 'Pausado' THEN 'Pausado' " +
                              "WHEN NOT EXISTS (SELECT 1 FROM Mensualidad m WHERE m.idEstudiante = e.idEstudiante AND m.mesCorrespondiente = MONTH(CURDATE()) AND m.anioCorrespondiente = YEAR(CURDATE()) AND m.estado = 'Pagada') AND DAY(CURDATE()) > 7 THEN 'Restringido' " +
                              "WHEN NOT EXISTS (SELECT 1 FROM Mensualidad m WHERE m.idEstudiante = e.idEstudiante AND m.mesCorrespondiente = MONTH(CURDATE()) AND m.anioCorrespondiente = YEAR(CURDATE()) AND m.estado = 'Pagada') THEN 'Pendiente' " +
                              "WHEN NOT EXISTS (SELECT 1 FROM Anualidad a WHERE a.idEstudiante = e.idEstudiante AND a.anioCorrespondiente = YEAR(CURDATE()) AND a.estado = 'Pagada') THEN 'Revisar anualidad' " +
                              "ELSE 'Al día' " +
                              "END " +
                              "WHERE e.activo = 1";

            MySqlParameter[] parametros = new MySqlParameter[] { };

            conexion.EjecutarComando(consulta, parametros);
        }

        // Método para listar la información principal del dashboard
        public DataTable ListarDashboard(int mes, int anio)
        {
            ActualizarEstadosAutomaticos();

            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT " +
                              "e.idEstudiante, " +
                              "e.estado, " +
                              "e.nombre AS estudiante, " +
                              "CASE WHEN e.estado = 'Pausado' THEN 'Pausado' ELSE IFNULL(m.estado, 'Pendiente') END AS mensualidad, " +
                              "CASE WHEN e.estado = 'Pausado' THEN 'Pausado' ELSE IFNULL(a.estado, 'Pendiente') END AS anualidad " +
                              "FROM Estudiante e " +
                              "LEFT JOIN Mensualidad m ON e.idEstudiante = m.idEstudiante " +
                              "AND m.mesCorrespondiente = @mes " +
                              "AND m.anioCorrespondiente = @anio " +
                              "LEFT JOIN Anualidad a ON e.idEstudiante = a.idEstudiante " +
                              "AND a.anioCorrespondiente = @anio " +
                              "WHERE e.activo = 1 " +
                              "ORDER BY e.nombre";

            MySqlParameter[] parametros = new MySqlParameter[]
            {
                new MySqlParameter("@mes", mes),
                new MySqlParameter("@anio", anio)
            };

            return conexion.EjecutarConsulta(consulta, parametros);
        }

        // Método para buscar estudiantes en el dashboard
        public DataTable BuscarDashboard(string texto, int mes, int anio)
        {
            ActualizarEstadosAutomaticos();

            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT " +
                              "e.idEstudiante, " +
                              "e.estado, " +
                              "e.nombre AS estudiante, " +
                              "CASE WHEN e.estado = 'Pausado' THEN 'Pausado' ELSE IFNULL(m.estado, 'Pendiente') END AS mensualidad, " +
                              "CASE WHEN e.estado = 'Pausado' THEN 'Pausado' ELSE IFNULL(a.estado, 'Pendiente') END AS anualidad " +
                              "FROM Estudiante e " +
                              "LEFT JOIN Mensualidad m ON e.idEstudiante = m.idEstudiante " +
                              "AND m.mesCorrespondiente = @mes " +
                              "AND m.anioCorrespondiente = @anio " +
                              "LEFT JOIN Anualidad a ON e.idEstudiante = a.idEstudiante " +
                              "AND a.anioCorrespondiente = @anio " +
                              "WHERE e.activo = 1 " +
                              "AND e.nombre LIKE @texto " +
                              "ORDER BY e.nombre";

            MySqlParameter[] parametros = new MySqlParameter[]
            {
                new MySqlParameter("@texto", "%" + texto + "%"),
                new MySqlParameter("@mes", mes),
                new MySqlParameter("@anio", anio)
            };

            return conexion.EjecutarConsulta(consulta, parametros);
        }

        // Método para cargar los contadores del dashboard
        public void CargarContadores()
        {
            ActualizarEstadosAutomaticos();

            ConexionDB conexion = new ConexionDB();

            string consulta = "SELECT " +
                              "SUM(CASE WHEN estado = 'Al día' THEN 1 ELSE 0 END) AS AlDia, " +
                              "SUM(CASE WHEN estado = 'Pendiente' THEN 1 ELSE 0 END) AS Pendiente, " +
                              "SUM(CASE WHEN estado = 'Restringido' THEN 1 ELSE 0 END) AS Restringido, " +
                              "SUM(CASE WHEN estado = 'Pausado' THEN 1 ELSE 0 END) AS Pausados, " +
                              "SUM(CASE WHEN estado = 'Revisar anualidad' THEN 1 ELSE 0 END) AS RevisarAnualidad " +
                              "FROM Estudiante " +
                              "WHERE activo = 1";

            DataTable tabla = conexion.EjecutarConsulta(consulta);

            if (tabla.Rows.Count > 0)
            {
                this.alDia = Convert.ToInt32(tabla.Rows[0]["AlDia"] == DBNull.Value ? 0 : tabla.Rows[0]["AlDia"]);
                this.pendiente = Convert.ToInt32(tabla.Rows[0]["Pendiente"] == DBNull.Value ? 0 : tabla.Rows[0]["Pendiente"]);
                this.restringido = Convert.ToInt32(tabla.Rows[0]["Restringido"] == DBNull.Value ? 0 : tabla.Rows[0]["Restringido"]);
                this.pausados = Convert.ToInt32(tabla.Rows[0]["Pausados"] == DBNull.Value ? 0 : tabla.Rows[0]["Pausados"]);
                this.revisarAnualidad = Convert.ToInt32(tabla.Rows[0]["RevisarAnualidad"] == DBNull.Value ? 0 : tabla.Rows[0]["RevisarAnualidad"]);
            }
        }
    }
}