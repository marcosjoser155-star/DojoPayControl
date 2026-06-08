using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DojoPayControl.Clases
{
    public class ConexionDB
    {
        // Atributos
        private string cadenaConexion;

        // Propiedades
        public string CadenaConexion
        {
            get { return cadenaConexion; }
            set { cadenaConexion = value; }
        }

        // Constructor
        public ConexionDB()
        {
            this.cadenaConexion = "server=localhost;database=Dojo_Control;user=root;password=ArthurSar27*;";
        }

        // Método para obtener una nueva conexión a la base de datos
        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(this.cadenaConexion);
        }

        // Método para probar si la conexión funciona
        public bool ProbarConexion()
        {
            try
            {
                using (MySqlConnection conexion = ObtenerConexion())
                {
                    conexion.Open();
                    conexion.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Método para ejecutar consultas SELECT sin parámetros
        public DataTable EjecutarConsulta(string consulta)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        // Método para ejecutar consultas SELECT con parámetros
        public DataTable EjecutarConsulta(string consulta, MySqlParameter[] parametros)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddRange(parametros);

                    using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        // Método para ejecutar INSERT, UPDATE o DELETE
        public int EjecutarComando(string consulta, MySqlParameter[] parametros)
        {
            int filasAfectadas = 0;

            using (MySqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddRange(parametros);
                    filasAfectadas = comando.ExecuteNonQuery();
                }
            }

            return filasAfectadas;
        }

        // Método para insertar datos y devolver el ID generado
        public int EjecutarComandoRetornaId(string consulta, MySqlParameter[] parametros)
        {
            int idGenerado = 0;

            using (MySqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddRange(parametros);
                    comando.ExecuteNonQuery();

                    idGenerado = Convert.ToInt32(comando.LastInsertedId);
                }
            }

            return idGenerado;
        }
    }
}