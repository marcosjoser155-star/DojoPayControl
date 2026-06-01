using DojoPayControl.Clases;
using DojoPayControl.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DojoPayControl
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            btnIngresar.Click += btnIngresar_Click;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtPassword.Text;

            if (usuario == "" || contrasena == "")
            {
                MessageBox.Show("Ingrese usuario y contraseña");
                return;
            }

            Usuario objUsuario = new Usuario();
            objUsuario.NombreUsuario = usuario;
            objUsuario.Contrasena = contrasena;

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
    }
}

