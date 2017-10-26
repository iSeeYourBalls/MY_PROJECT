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


namespace MSU
{
    public partial class status : Form
    {
        public status()
        {
            InitializeComponent();
        }

        private void status_Load(object sender, EventArgs e)
        {
            updateComboboxShiftStatus();
        }
        private void updateComboboxShiftStatus()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "select * from shiftstatus";
                        DataTable shiftstatus = new DataTable();
                        shiftstatus.Load(cmd.ExecuteReader());
                        comboBox1.DataSource = shiftstatus;
                        comboBox1.DisplayMember = "statusName";
                        comboBox1.ValueMember = "id_status";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n combobox_upload" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_changeStatus_Click(object sender, EventArgs e)
        {
            if (GlobalVariable.edit_shift != null && GlobalVariable.edit_shift != "")
            {
                update_shift_main();
            }
        }
        private void update_shift_main()
        {
            main m = this.Owner as main;
            m.updateStatus(comboBox1.SelectedValue.ToString());
            GlobalVariable.edit_shift = null;
            this.Close();
        }
    }
}
