using DojoPayControl.Clases;
using DojoPayControl.Interfaces;
using System;
using System.Windows.Forms;

namespace DojoPayControl
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();

            // Limpia los textos de ejemplo para que no se usen como datos reales
            txtUsuario.Text = "";
            txtPassword.Text = "";

            // Oculta lo que se escribe en el campo de contraseña
            txtPassword.UseSystemPasswordChar = true;

            // Permite iniciar sesión presionando Enter
            this.AcceptButton = btnIngresar;

            // Conecta el botón con su evento Click
            btnIngresar.Click += new EventHandler(btnIngresar_Click);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            Usuario usuario = new Usuario();

            usuario.NombreUsuario = nombreUsuario;
            usuario.Contrasena = password;

            if (usuario.ValidarUsuario() == false)
            {
                MessageBox.Show(
                    "Debe ingresar usuario y contraseña.",
                    "Campos vacíos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtUsuario.Focus();
                return;
            }

            ConexionDB conexion = new ConexionDB();

            if (conexion.ProbarConexion() == false)
            {
                MessageBox.Show(
                    "No se pudo conectar con la base de datos.",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (usuario.IniciarSesion() == true)
            {
                MessageBox.Show(
                    "Bienvenido al sistema.",
                    "Inicio de sesión correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                FrmDashboard dashboard = new FrmDashboard();

                dashboard.FormClosed += (s, args) => this.Close();

                dashboard.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show(
                    "Usuario o contraseña incorrectos.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}