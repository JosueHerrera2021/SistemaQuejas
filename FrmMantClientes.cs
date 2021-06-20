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
    public partial class FrmMantClientes : Form
    {
        SqlConnection con = null;
        DataTable dt = null;
        public FrmMantClientes()
        {
            InitializeComponent();
        }

        private void FrmMantClientes_Load(object sender, EventArgs e)
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

        private void FrmMantClientes_Activated(object sender, EventArgs e) => ejecutarConsulta();

        private void BtnBuscar_Click(object sender, EventArgs e) => ejecutarConsulta();

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            FrmDetallesCliente frm = new FrmDetallesCliente();
            frm.con = con;
            frm.modalidad = "C"; //C para Guardar
            frm.ShowDialog();
        }
                            //Clientes
        private void DataGridEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {}
        private void DataGridClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.DataGridClientes.SelectedRows[0];
                FrmDetallesCliente frm = new FrmDetallesCliente();
                frm.idcliente = Convert.ToInt32(row.Cells[0].Value);
                frm.nombre = row.Cells[1].Value.ToString();
                frm.direccion = row.Cells[2].Value.ToString();
                frm.telefono = row.Cells[3].Value.ToString();
                frm.cedula = row.Cells[4].Value.ToString();
                frm.correo = row.Cells[5].Value.ToString();
                frm.estado = row.Cells[6].Value.ToString();
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
                string sql = "select * from Clientes";
                sql += " where " + CbxCriterio.Text + " like '%" + TxtABuscar.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                dt = new DataTable();
                da.Fill(dt);
                DataGridClientes.DataSource = dt;
                DataGridClientes.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al realizar la consulta. Detalles: \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
