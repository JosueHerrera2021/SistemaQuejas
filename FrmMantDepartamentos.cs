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
using System.IO;
using System.Diagnostics;

namespace SistemaQuejas
{
    public partial class FrmMantDepartamentos : Form
    {
        SqlConnection con = null;
        DataTable dt = null;
        public FrmMantDepartamentos()
        {
            InitializeComponent();
        }

        private void FrmMantDepartamentos_Load(object sender, EventArgs e)
        {
            try
            {
                CbxCriterio.SelectedIndex = 0;
                con = new SqlConnection("Data Source=LAPTOP-K6A8UIRV;Initial Catalog=QuejasDB;Integrated Security=True");
                con.Open();
                ejecutarConsulta();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos. Detalles: \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmMantDepartamentos_Activated(object sender, EventArgs e) => ejecutarConsulta();

        private void BtnBuscar_Click(object sender, EventArgs e) => ejecutarConsulta();

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            FrmDetallesDepartamento frm = new FrmDetallesDepartamento();
            frm.con = con;
            frm.modalidad = "C"; //C para Guardar
            frm.ShowDialog();
        }

        private void DataGridDepartamentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.DataGridDepartamentos.SelectedRows[0];
                FrmDetallesDepartamento frm = new FrmDetallesDepartamento();
                frm.iddepartamento = Convert.ToInt32(row.Cells[0].Value);
                frm.nombre = row.Cells[1].Value.ToString();
                frm.funcion = row.Cells[2].Value.ToString();
                frm.responsable = row.Cells[3].Value.ToString();
                frm.modalidad = "U"; //U de Update
                frm.con = con;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el registro. Detalles: \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ejecutarConsulta()
        {
            try
            {
                string sql = "select * from Departamentos";
                sql += " where " + CbxCriterio.Text + " like '%" + TxtABuscar.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                dt = new DataTable();
                da.Fill(dt);
                DataGridDepartamentos.DataSource = dt;
                DataGridDepartamentos.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al realizar la consulta. Detalles: \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
