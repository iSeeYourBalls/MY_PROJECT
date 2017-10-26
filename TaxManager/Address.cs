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
    public partial class Address : Form
    {
        BindingSource bs = new BindingSource();

        public Address()
        {
            InitializeComponent();
        }

        private void Address_Load(object sender, EventArgs e)
        {

        }

        private void takeInfo()
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
                            cmd.CommandText = "EXEC Address N'" + txt_search.Text + "'";
                            dt.Load(cmd.ExecuteReader());
                            bs.DataSource = dt;
                            dgv_address.DataSource = bs;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n Address_Load " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            takeInfo();
        }

        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                takeInfo();
        }
    }
}
