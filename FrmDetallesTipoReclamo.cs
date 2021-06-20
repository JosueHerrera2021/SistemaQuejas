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
    public partial class FrmDetallesTipoReclamo : Form
    {
        public SqlConnection con { get; set; }
        public int idtiporeclamo { get; set; }
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public string iddepartamento { get; set; }
        public string estado { get; set; }
        public string modalidad { get; set; }
        public FrmDetallesTipoReclamo()
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

        private void FrmDetallesTipoReclamo_Load(object sender, EventArgs e)
        {
            try
            {
                TxtIdTiReclamo.Text = idtiporeclamo.ToString();
                TxtTipo.Text = tipo;
                TxtDescripcion.Text = descripcion;
                CbxEstado.Text = estado;
                llenarComboBoxDepartamentos();

                TxtIdTiReclamo.Enabled = modalidad.Equals("C");
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
                string sqlOperacion;
                if (modalidad.Equals("C"))
                {
                    sqlOperacion = "insert into TipoReclamo values('";
                    sqlOperacion += TxtTipo.Text + "', '" + TxtDescripcion.Text + "', ";
                    sqlOperacion += "(select Id_Departamento from Departamentos where Nombre = '" + CbxDepartamento.Text + "'), '";
                    sqlOperacion += CbxEstado.Text + "');";
                }
                else
                {
                    sqlOperacion = "update TipoReclamo set ";
                    sqlOperacion += "Tipo = '" + TxtTipo.Text + "', Descripcion = '" + TxtDescripcion.Text + "', ";
                    sqlOperacion += "Id_Departamento = (select Id_Departamento from Departamentos where Nombre = '" + CbxDepartamento.Text + "'), ";
                    sqlOperacion += "Estado = '" + CbxEstado.Text + "' ";
                    sqlOperacion += "where Id_Tipo = " + Convert.ToInt32(TxtIdTiReclamo.Text) + ";";
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
                string sqlEliminar = "delete TipoReclamo ";
                sqlEliminar += "where Id_Tipo = " + Convert.ToInt32(TxtIdTiReclamo.Text) + ";";
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
