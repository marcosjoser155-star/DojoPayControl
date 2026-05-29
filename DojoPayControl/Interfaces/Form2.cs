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
        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void btnNuevoEstudiante_Click(object sender, EventArgs e)
        {
            FrmNuevoEstudiante frm = new FrmNuevoEstudiante();
            frm.Show();
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            FrmRegistrarPago frm = new FrmRegistrarPago();
            frm.Show();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            // placeholder for designer event
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            // placeholder for designer event
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {
            // placeholder for designer event
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // placeholder for designer event
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // placeholder for designer event
        }
    }
}
