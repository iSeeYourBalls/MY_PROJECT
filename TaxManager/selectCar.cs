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
    public partial class selectCar : Form
    {
        BindingSource bs = new BindingSource();
       // Main main = new Main();

        public selectCar()
        {
            InitializeComponent();
        }

        private void selectCar_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;

                    using (DataTable dt = new DataTable())
                    {
                        cmd.CommandText = "EXEC selectResourceForManualy '"+GlobalVariable.resourceName+"', 0";
                        dt.Load(cmd.ExecuteReader());
                        bs.DataSource = dt;
                        listBox1.DataSource = bs;
                    }
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            GlobalVariable.resourceName = listBox1.SelectedValue.ToString();
            this.Close();
        }
    }
}
