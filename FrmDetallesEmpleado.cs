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
    public partial class FrmDetallesEmpleado : Form
    {
        public SqlConnection con { get; set; }
        public int idempleado { get; set; }
        public string nombre { get; set; }
        public string iddepartamento { get; set; }
        public string estado { get; set; }
        public string modalidad { get; set; }

        public FrmDetallesEmpleado()
        {
            InitializeComponent();
        }

        private void llenarComboBoxDepartamentos()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT Nombre from Departamentos", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CbxDepartamento.DataSource = dt;
            CbxDepartamento.DisplayMember = "Nombre";
            CbxDepartamento.ValueMember = "Nombre";
        }

        private void FrmDetallesEmpleado_Load(object sender, EventArgs e)
        {
            try
            {
                TxtIdEmpleado.Text = idempleado.ToString();
                TxtNombre.Text = nombre;
                llenarComboBoxDepartamentos();
                CbxDepartamento.Text = iddepartamento;
                CbxEstado.Text = estado;

                TxtIdEmpleado.Enabled = modalidad.Equals("C");
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
                    sqlOperacion = "insert into Empleados values('";
                    sqlOperacion += TxtNombre.Text + "', ";
                    sqlOperacion += "(select Id_Departamento from Departamentos where Nombre = '" + CbxDepartamento.Text + "'), '";
                    sqlOperacion += CbxEstado.Text + "');";
                }
                else
                {
                    sqlOperacion = "update Empleados set ";
                    sqlOperacion += "Nombre = '" + TxtNombre.Text + "', Id_Departamento = ";
                    sqlOperacion += "(select Id_Departamento from Departamentos where Nombre = '" + CbxDepartamento.Text + "'), ";
                    sqlOperacion += "Estado = '" + CbxEstado.Text + "' ";
                    sqlOperacion += "where Id_Empleado = " + Convert.ToInt32(TxtIdEmpleado.Text) + ";";
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
                string sqlEliminar = "delete Empleados ";
                sqlEliminar += "where Id_Empleado = " + Convert.ToInt32(TxtIdEmpleado.Text) + ";";
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
    }
}
