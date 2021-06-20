using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SistemaQuejas
{
    public partial class FrmDetallesCliente : Form
    {
        public SqlConnection con { get; set; }
        public int idcliente { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string cedula { get; set; }
        public string correo { get; set; }
        public string estado { get; set; }
        public string modalidad { get; set; }


        public FrmDetallesCliente()
        {
            InitializeComponent();
        }

        private void FrmDetallesCliente_Load(object sender, EventArgs e)
        {
            try
            {
                //CbxEstado.SelectedIndex = 0;
                TxtIdCliente.Text = idcliente.ToString();
                TxtNombre.Text = nombre;
                TxtTelefono.Text = telefono;
                TxtDireccion.Text = direccion;
                TxtCedula.Text = cedula;
                TxtCorreo.Text = correo;
                CbxEstado.Text = estado;

                TxtIdCliente.Enabled = modalidad.Equals("C");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asignar valores. Detalles: \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlOperacion = "";
                if (modalidad.Equals("C"))
                {
                    sqlOperacion = "insert into Clientes values('";
                    sqlOperacion += TxtNombre.Text + "', '" + TxtDireccion.Text + "', '" + TxtTelefono.Text + "', '";
                    sqlOperacion += TxtCedula.Text + "', '" + TxtCorreo.Text + "', '" + CbxEstado.Text + "');";
                }
                else
                {
                    sqlOperacion = "update Clientes set ";
                    sqlOperacion += "Nombre = '" + TxtNombre.Text + "', Direccion = '" + TxtDireccion.Text + "', ";
                    sqlOperacion += "Telefono = '" + TxtTelefono.Text + "',  Cedula = '" + TxtCedula.Text + "', ";
                    sqlOperacion += "Correo = '" + TxtCorreo.Text + "', Estado = '" + CbxEstado.Text + "' ";
                    sqlOperacion += "where Id_Cliente = " + Convert.ToInt32(TxtIdCliente.Text) + ";";
                }
                SqlCommand cmd = new SqlCommand(sqlOperacion, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro guardado exitosamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar registro. Detalles: \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlEliminar = "delete Clientes ";
                sqlEliminar += "where Id_Cliente = " + Convert.ToInt32(TxtIdCliente.Text) + ";";
                SqlCommand cmd = new SqlCommand(sqlEliminar, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro eliminado exitosamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar registro. Detalles: \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCedula_Leave(object sender, EventArgs e)
        {
            if (validarCedula(TxtCedula.Text)) { }
            else
            {
                MessageBox.Show("Formato de cédula inválido. \n Favor revisar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtCedula.Focus();
            }
        }

        private void TxtCorreo_Leave(object sender, EventArgs e)
        {
            /*if (validarCedula(TxtCorreo.Text)) { }
            else
            {
                MessageBox.Show("Formato de correo inválido. \n Favor revisar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtCorreo.Focus();
            }*/
        }

        public static bool validarCedula(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (pLongCed < 11 || pLongCed > 11)
                return false;

            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }

            if (vnTotal % 10 == 0)
                return true;
            else
                return false;
        }

        bool validarEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
