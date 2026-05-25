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
    public class Usuario
    {
        // Atributos
        private int idUsuario;
        private string usuario;
        private string contrasena;
        private string rol;

        // Propiedades
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }

        public string Rol
        {
            get { return rol; }
            set { rol = value; }
        }

        // Constructor vacío
        public Usuario()
        {

        }

        // Constructor con parámetros
        public Usuario(int idUsuario, string usuario,
                       string contrasena, string rol)
        {
            this.idUsuario = idUsuario;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.rol = rol;
        }

        // Método IniciarSesion
        public bool IniciarSesion()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                conexion.AbrirConexion();

                string consulta = "SELECT * FROM Usuario " +
                                  "WHERE usuario = @usuario " +
                                  "AND contrasena = @contrasena";

                MySqlCommand comando = new MySqlCommand(consulta, conexion.Conexion);

                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@contrasena", contrasena);

                MySqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    rol = lector["rol"].ToString();

                    lector.Close();

                    return true;
                }
                else
                {
                    lector.Close();

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al iniciar sesión: " + ex.Message);

                return false;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Método ValidarUsuario
        public bool ValidarUsuario()
        {
            if (!string.IsNullOrEmpty(usuario) &&
                !string.IsNullOrEmpty(contrasena))
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