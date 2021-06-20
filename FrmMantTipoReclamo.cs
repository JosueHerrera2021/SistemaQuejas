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
    public partial class FrmMantTipoReclamo : Form
    {
        SqlConnection con = null;
        DataTable dt = null;
        public FrmMantTipoReclamo()
        {
            InitializeComponent();
        }

        private void FrmMantTipoReclamo_Load(object sender, EventArgs e)
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

        private void FrmMantTipoReclamo_Activated(object sender, EventArgs e) => ejecutarConsulta();

        private void BtnBuscar_Click(object sender, EventArgs e) => ejecutarConsulta();

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            FrmDetallesTipoReclamo frm = new FrmDetallesTipoReclamo();
            frm.con = con;
            frm.modalidad = "C"; //C para Guardar
            frm.ShowDialog();
        }

        private void DataGridTiReclamo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.DataGridTiReclamo.SelectedRows[0];
                FrmDetallesTipoReclamo frm = new FrmDetallesTipoReclamo();
                frm.idtiporeclamo = Convert.ToInt32(row.Cells[0].Value);
                frm.tipo = row.Cells[1].Value.ToString();
                frm.descripcion = row.Cells[2].Value.ToString();
                frm.iddepartamento = row.Cells[3].Value.ToString();
                frm.estado = row.Cells[4].Value.ToString();
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
                string sql = "select tr.Id_Tipo as 'ID Tipo Reclamo', tr.Tipo, tr.Descripcion, dp.Nombre as 'Departamento', tr.Estado from TipoReclamo tr ";
                sql += "inner join Departamentos dp on tr.Id_Departamento = dp.Id_Departamento";
                sql += " where tr." + CbxCriterio.Text + " like '%" + TxtABuscar.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                dt = new DataTable();
                da.Fill(dt);
                DataGridTiReclamo.DataSource = dt;
                DataGridTiReclamo.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al realizar la consulta. Detalles: \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
