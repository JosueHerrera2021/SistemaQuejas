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
    public partial class FrmLogin : Form
    {
        QuejasDBEntities1 db = new QuejasDBEntities1();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            UsuariosLogin usuario = (from u in db.UsuariosLogins
                                     where u.UsuarioL.Equals(TxtUsuario.Text) &&
                                           u.Contrasena.Equals(TxtContrasena.Text)
                                     select u).FirstOrDefault();
            if (usuario == null)
            {
                MessageBox.Show("Datos ingresados incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!usuario.Estado.Equals("A"))
            {
                MessageBox.Show("Datos ingresados incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FrmMenu frm = new FrmMenu();
                frm.nombreUsuario = TxtUsuario.Text;
                this.ShowInTaskbar = false;
                frm.ShowDialog();
                TxtContrasena.Clear();
                TxtUsuario.Clear();
                this.Hide();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
