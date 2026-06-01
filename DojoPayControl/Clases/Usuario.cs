using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

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

        public string NombreUsuario
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
        public Usuario(int idUsuario, string usuario, string contrasena, string rol)
        {
            this.idUsuario = idUsuario;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.rol = rol;
        }

        // Método para iniciar sesión
        public bool IniciarSesion()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                string consulta = "SELECT idUsuario, usuario, contrasena, rol " +
                                  "FROM Usuario " +
                                  "WHERE usuario = @usuario " +
                                  "AND contrasena = @contrasena";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@usuario", this.usuario),
                    new MySqlParameter("@contrasena", this.contrasena)
                };

                DataTable tabla = conexion.EjecutarConsulta(consulta, parametros);

                if (tabla.Rows.Count > 0)
                {
                    this.idUsuario = Convert.ToInt32(tabla.Rows[0]["idUsuario"]);
                    this.usuario = tabla.Rows[0]["usuario"].ToString();
                    this.contrasena = tabla.Rows[0]["contrasena"].ToString();
                    this.rol = tabla.Rows[0]["rol"].ToString();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        // Método para validar que los campos del login no estén vacíos
        public bool ValidarUsuario()
        {
            if (!string.IsNullOrEmpty(this.usuario) &&
                !string.IsNullOrEmpty(this.contrasena))
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