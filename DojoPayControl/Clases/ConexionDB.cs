using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DojoPayControl.Clases
{
    public class ConexionDB
    {
        private string cadenaConexion = "server=localhost;database=dojo_control;user=root;password=ArthurSar27*;";

        public MySqlConnection Conexion { get; private set; }

        public ConexionDB()
        {
            Conexion = new MySqlConnection(cadenaConexion);
        }

        public void AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
            {
                Conexion.Open();
            }
        }

        public void CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
            {
                Conexion.Close();
            }
        }

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }

        public bool ProbarConexion()
        {
            try
            {
                AbrirConexion();
                CerrarConexion();
                return true;
            }
            catch
            {
                return false;
            }
        }

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

        public void EjecutarComando(string comandoSQL)
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(comandoSQL, conexion))
                {
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
