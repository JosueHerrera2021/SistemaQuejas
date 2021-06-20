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
    public partial class FrmDetallesDepartamento : Form
    {
        public SqlConnection con { get; set; }
        public int iddepartamento { get; set; }
        public string nombre { get; set; }
        public string funcion { get; set; }
        public string responsable { get; set; }
        public string modalidad { get; set; }
        public FrmDetallesDepartamento()
        {
            InitializeComponent();
        }

        private void FrmDetallesDepartamento_Load(object sender, EventArgs e)
        {
            try
            {
                TxtIdDepartamento.Text = iddepartamento.ToString();
                TxtNombre.Text = nombre;
                TxtFuncion.Text = funcion;
                TxtResponsable.Text = responsable;

                TxtIdDepartamento.Enabled = modalidad.Equals("C");
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
                    sqlOperacion = "insert into Departamentos values('";
                    sqlOperacion += TxtNombre.Text + "', '" + TxtFuncion.Text + "', '";
                    sqlOperacion += TxtResponsable.Text + "');";
                }
                else
                {
                    sqlOperacion = "update Departamentos set ";
                    sqlOperacion += "Nombre = '" + TxtNombre.Text + "', Funcion = '" + TxtFuncion.Text + "', ";
                    sqlOperacion += "Responsable = '" + TxtResponsable.Text + "'";
                    sqlOperacion += " where Id_Departamento = " + Convert.ToInt32(TxtIdDepartamento.Text) + ";";
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
                string sqlEliminar = "delete Departamentos ";
                sqlEliminar += "where Id_Departamento = " + Convert.ToInt32(TxtIdDepartamento.Text) + ";";
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
