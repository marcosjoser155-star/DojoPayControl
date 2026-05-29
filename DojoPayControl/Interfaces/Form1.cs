
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DojoPayControl.Clases;
using DojoPayControl.Interfaces;

namespace DojoPayControl
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // design-time safe: avoid executing runtime-only code here
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Trim inputs and ignore placeholder-like values
            string usuario = txtUsuario.Text?.Trim();
            string contrasena = txtPassword.Text?.Trim();

            // Treat obvious placeholders as empty
            if (!string.IsNullOrEmpty(usuario) && (usuario.StartsWith("Ej", StringComparison.OrdinalIgnoreCase) || usuario.Contains(" ")))
            {
                // if the designer placeholder remains, ensure user actually typed
                if (usuario.IndexOf(' ') == 0 || usuario.StartsWith("Ej", StringComparison.OrdinalIgnoreCase))
                    usuario = usuario.Trim();
            }

            if (!string.IsNullOrEmpty(contrasena) && contrasena.Contains("*"))
            {
                // placeholder password like "********" should be treated as empty
                contrasena = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Ingrese usuario y contraseña");
                return;
            }

            Usuario objUsuario = new Usuario { NombreUsuario = usuario, Contrasena = contrasena };

            try
            {
                if (objUsuario.IniciarSesion())
                {
                    FrmDashboard frm = new FrmDashboard();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                // show unexpected exceptions for debugging
                MessageBox.Show("Error al iniciar sesión: " + ex.Message);
            }
        }
    }
}
