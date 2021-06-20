using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaQuejas
{
    public partial class FrmMenu : Form
    {
        public string nombreUsuario { get; set; }
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void tiposDeReclamosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMantTipoReclamo frm = new FrmMantTipoReclamo();
            frm.ShowDialog();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMantEmpleados frm = new FrmMantEmpleados();
            frm.ShowDialog();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMantClientes frm = new FrmMantClientes();
            frm.ShowDialog();
        }

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMantDepartamentos frm = new FrmMantDepartamentos();
            frm.ShowDialog();
        }

        private void FrmMenu_Load(object sender, EventArgs e) => verificarPrivilegio();

        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.Show();
            frm.ShowInTaskbar = true;
        }

        private void verificarPrivilegio()
        {
            QuejasDBEntities1 db = new QuejasDBEntities1();
            UsuariosLogin usuario = (from u in db.UsuariosLogins
                                     where u.UsuarioL.Equals(nombreUsuario)
                                     select u).FirstOrDefault();

            if (usuario.Privilegio.Equals("U"))
            {
                clientesToolStripMenuItem.Visible = false;
                empleadosToolStripMenuItem.Visible = false;
                departamentosToolStripMenuItem.Visible = false;
            }
            else if (usuario.Privilegio.Equals("E"))
            {
                clientesToolStripMenuItem.Visible = false;
                empleadosToolStripMenuItem.Visible = false;
            }
        }
    }
}
