using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxManager
{
    public partial class Constants : Form
    {
        BindingSource bs = new BindingSource();

        public Constants()
        {
            InitializeComponent();
        }

        private void Constants_Load(object sender, EventArgs e)
        {
            takeConstants();
        }

        private void takeConstants()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;

                        using (DataTable dt = new DataTable())
                        {
                            cmd.CommandText = "select * from constants";
                            dt.Load(cmd.ExecuteReader());
                            bs.DataSource = dt;
                            dgv_constants.DataSource = bs;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n takeConstants " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            saveDataDGV();
        }

        private void saveDataDGV()
        {
            try
            {
                string commandText = "UPDATE constants " +
                                "SET uvalue = @uvalue WHERE id = @id";

                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.Parameters.Add("@uvalue", SqlDbType.Decimal);
                        cmd.Parameters.Add("@id", SqlDbType.Int);

                        for (int i = 0; i < dgv_constants.RowCount; i++)
                        {
                            cmd.CommandText = commandText;
                            cmd.Parameters["@uvalue"].Value = dgv_constants.Rows[i].Cells["uvalue"].Value;
                            cmd.Parameters["@id"].Value = dgv_constants.Rows[i].Cells["id"].Value;
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Изменения добавлены!", "Сохранено!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        takeConstants();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n saveDataDGV" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
