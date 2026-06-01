using DojoPayControl.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DojoPayControl.Interfaces
{
    public partial class FrmDashboard : Form
    {
        Dashboard dashboard = new Dashboard();

        public FrmDashboard()
        {
            InitializeComponent();

            this.Load += FrmDashboard_Load;
            btnNuevoEstudiante.Click += BtnNuevoEstudiante_Click;
            btnRegistrarPago.Click += BtnRegistrarPago_Click;
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            CargarDashboard();
        }

        public void CargarDashboard()
        {
            DataTable tabla = dashboard.ListarDashboard(DateTime.Now.Month, DateTime.Now.Year);

            // The designer defines dataGridView1 on the dashboard tab - bind to that control
            dataGridView1.DataSource = tabla;

            dashboard.CargarContadores();
        }

        private void BtnNuevoEstudiante_Click(object sender, EventArgs e)
        {
            FrmNuevoEstudiante frm = new FrmNuevoEstudiante();

            frm.ShowDialog();
        }

        private void BtnRegistrarPago_Click(object sender, EventArgs e)
        {
            FrmRegistrarPago frm = new FrmRegistrarPago();

            frm.ShowDialog();
        }

       
    }
}
