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
    public partial class edit_interface : Form
    {
        SqlDataReader dr;

        public static string queryInterface = "EXEC takeInterfaceColumn '" + GlobalVariable.userName + "'";

        public edit_interface()
        {
            InitializeComponent();
        }

        private void edit_interface_Load(object sender, EventArgs e)
        {
            updateCheckBoxOrders();
            updateCheckBoxShift();
            updateCheckBoxAction();

            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = queryInterface;
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            for (int i = 0; i < 13; i++)
                            {
                                if (Convert.ToInt16(dr[i].ToString()) == 1)
                                    checkBoxList_orders.SetItemChecked(i, true);
                            }
                            for (int i = 13; i < 28; i++)
                            {
                                if (Convert.ToInt16(dr[i].ToString()) == 1)
                                    checkBoxList_shift.SetItemChecked(i - 13, true);
                            }
                            for (int i = 28; i < 34; i++)
                            {
                                if (Convert.ToInt16(dr[i].ToString()) == 1)
                                    checkedListBox_action.SetItemChecked(i - 28, true);
                            }
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n edit_interface_Load" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void save()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        int check = 0;



                        for (int i = 0; i < 13; i++)
                        {
                            if (checkBoxList_orders.GetItemChecked(i) == true)
                                check = 1;
                            else
                                check = 0;

                            cmd.CommandText = queryInterface;
                            dr = cmd.ExecuteReader();

                            string nextColumnName = dr.GetName(i);

                            dr.Close();

                            cmd.CommandText = "UPDATE interface SET " + nextColumnName + " = '" + check + "' WHERE userName = '" + GlobalVariable.userName + "'";
                            cmd.ExecuteNonQuery();

                        }

                        for (int i = 13; i < 28; i++)
                        {
                            if (checkBoxList_shift.GetItemChecked(i - 13) == true)
                                check = 1;
                            else
                                check = 0;

                            cmd.CommandText = queryInterface;
                            dr = cmd.ExecuteReader();

                            string nextColumnName = dr.GetName(i);

                            dr.Close();

                            cmd.CommandText = "UPDATE interface set " + nextColumnName + " = '" + check + "' WHERE userName = '" + GlobalVariable.userName + "'";
                            cmd.ExecuteNonQuery();
                        }

                        for (int i = 28; i < 34; i++)
                        {
                            if (checkedListBox_action.GetItemChecked(i - 28) == true)
                                check = 1;
                            else
                                check = 0;

                            cmd.CommandText = queryInterface;
                            dr = cmd.ExecuteReader();

                            string nextColumnName = dr.GetName(i);

                            dr.Close();

                            cmd.CommandText = "UPDATE interface set " + nextColumnName + " = '" + check + "' WHERE userName = '" + GlobalVariable.userName + "'";
                            cmd.ExecuteNonQuery();

                        }

                    }
                }
                update_order_main();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n save" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void update_order_main()
        {
            main m = this.Owner as main;
            m.updateColumnAction();
            m.updateColumnOrders();
            m.updateColumnShift();
            m.update_action();
            m.update_orders("");
            m.update_shift();
            Cursor = Cursors.Default;
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            save();            
        }
        private void updateCheckBoxOrders()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "select * from interfaceColumns where column_type = 1";
                        DataTable interfaceColumns = new DataTable();
                        interfaceColumns.Load(cmd.ExecuteReader());
                        checkBoxList_orders.DataSource = interfaceColumns;
                        checkBoxList_orders.DisplayMember = "columnName";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateCheckBoxOrders" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void updateCheckBoxShift()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "select * from interfaceColumns where column_type = 2";
                        DataTable interfaceColumns = new DataTable();
                        interfaceColumns.Load(cmd.ExecuteReader());
                        checkBoxList_shift.DataSource = interfaceColumns;
                        checkBoxList_shift.DisplayMember = "columnName";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateCheckBoxShift" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void updateCheckBoxAction()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "select * from interfaceColumns where column_type = 3";
                        DataTable interfaceColumns = new DataTable();
                        interfaceColumns.Load(cmd.ExecuteReader());
                        checkedListBox_action.DataSource = interfaceColumns;
                        checkedListBox_action.DisplayMember = "columnName";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateCheckBoxAction" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
