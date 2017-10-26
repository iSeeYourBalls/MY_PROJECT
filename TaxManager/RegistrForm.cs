using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaxManager
{
    public partial class RegistrForm : Form
    {
        BindingSource bs = new BindingSource();

        public RegistrForm()
        {
            InitializeComponent();
        }

        private void RegistrForm_Load(object sender, EventArgs e)
        {
            takeInfo(0, GlobalVariable.registrNumber);
        }

        private void takeInfo(int type, string registrCode)
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
                            cmd.CommandText = "EXEC registrTTN '" + registrCode + "', '" + GlobalVariable.userName + "', '" + dateTimePicker_registr.Value.ToString("yyyy-MM-dd") + "', " + type + "";
                            dt.Load(cmd.ExecuteReader());
                            bs.DataSource = dt;
                            dgv_registrData.DataSource = bs;
                            label_countTTN.Text = dgv_registrData.RowCount.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n takeInfo" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_registrSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_registrCode.Text))
            {
                takeInfo(1, "");
            }
            else
            {
                takeInfo(0, txt_registrCode.Text);
            }
        }

        private void dgv_registrData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgv_registrData.RowCount; i++)
            {
                if (Convert.ToInt32(dgv_registrData.Rows[i].Cells["inRegistrOrNot"].Value) == 1)
                    dgv_registrData.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                else if(Convert.ToInt32(dgv_registrData.Rows[i].Cells["inRegistrOrNot"].Value) == 0)
                    dgv_registrData.Rows[i].DefaultCellStyle.BackColor = Color.RosyBrown;
                else if (Convert.ToInt32(dgv_registrData.Rows[i].Cells["inRegistrOrNot"].Value) == 2)
                    dgv_registrData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            }
        }
    }
}
