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
    public partial class users : Form
    {
        ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();
        SqlDataReader dr;

        public string idUser {set; get;}
        public int editUser { set; get; }

        public users()
        {
            InitializeComponent();
        }

        private void users_Load(object sender, EventArgs e)
        {
            update();

            my_menu.Items.Add("Удалить").Name = "del";
            my_menu.Items.Add("Редактировать").Name = "edit";
            my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
            my_menu.Items["del"].Image = MSU.Properties.Resources.delete;
            my_menu.Items["edit"].Image = MSU.Properties.Resources.edit;
        }
        private void update()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT * FROM users WHERE userName <> 'admin' order by id";
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(cmd.ExecuteReader());
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }
        private void insertUser()
        {
            if (txt_login.Text != "" & txt_pass.Text != "")
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;

                            cmd.CommandText = "SELECT count(*) FROM users WHERE userName = '" + txt_login.Text + "'";
                            dr = cmd.ExecuteReader();

                            int countUser = 0;

                            if (dr.Read())
                                countUser = int.Parse(dr[0].ToString());
                            dr.Close();

                            if (countUser == 0)
                            {
                                Cursor = Cursors.WaitCursor;

                                cmd.CommandText = "INSERT into users (userName, pass) VALUES('" + txt_login.Text + "', '" + txt_pass.Text + "')";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "INSERT into interface (orders_shop, orders_city, orders_kg, orders_pal, orders_product, orders_typeCar, orders_com, orders_truck, orders_date, orders_orderNumber, shift_truck, shift_trailer, shift_driver, shift_travel, shift_status, shift_capacityKg, shift_capacityPal, shift_upKg, shift_upPal, shift_subcontractor, shift_date, shift_TTH, shift_com, action_travel, action_shop, action_city, action_kg, action_pal, action_product, userName, orders_stopFromDay, orders_stopFillDay, orders_km, shift_startinstant, shift_finishInstant) VALUES('1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '" + txt_login.Text + "', '1', '1', '1', '1', '1')";
                                cmd.ExecuteNonQuery();


                                cmd.CommandText = "select DatagridColumnName, tableType from fields WHERE tableType is NOT NULL";
                                dr = cmd.ExecuteReader();

                                DataTable schemaTable = new DataTable();
                                schemaTable.Load(dr);

                                dr.Close();

                                foreach (DataRow importRow in schemaTable.Rows)
                                {
                                    cmd.CommandText = "INSERT INTO userColumn (userName, tableName, tableType) VALUES ('" + txt_login.Text + "', '" + importRow["DatagridColumnName"] + "', '" + importRow["tableType"] + "')";
                                    cmd.ExecuteNonQuery();
                                }

                                update();
                                Cursor = Cursors.Default;
                                MessageBox.Show("Пользователь добавлен!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Добавляемый пользователь существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\n insertUser" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Вызываем меню правой кнопки мыши таблица заказов
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    this.TopMost = true;
                    my_menu.Show(dataGridView1, new Point(e.X, e.Y));
                    this.TopMost = false;
                }
                catch { }
            }
        }
        // Обработчик меню правой кнопки мыши orderGrid
        void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                dataGridView1.CurrentCell.Selected = true;
                if (e.ClickedItem.Name.ToString() == "del")
                {
                    CurrencyManager cmgr = (CurrencyManager)this.dataGridView1.BindingContext[this.dataGridView1.DataSource];
                    DataRow row = ((DataRowView)cmgr.Current).Row;
                    string order_number = row["id"].ToString();
                    string username = row["userName"].ToString();
                    //TopMost = true;
                    var result = MessageBox.Show("Пользователь " + username + ", будет удален. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // TopMost = false;
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;
                                cmd.CommandText = "DELETE FROM users WHERE id = '" + order_number + "'";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "DELETE FROM interface WHERE userName = '" + username + "'";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "delete from usercolumn WHERE userName = '" + username + "'";
                                cmd.ExecuteNonQuery();

                            }
                        }
                        update();
                    }
                }
                if (e.ClickedItem.Name.ToString() == "edit")
                {
                    editUser = 1;
                    btn_add.Text = "Сохранить";

                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            cmd.CommandText = "SELECT TOP 1 userName, pass, ISNULL(rules,0) as rules FROM users WHERE id = '" + idUser + "'";
                            dr = cmd.ExecuteReader();

                            int checkAdmin = 0;

                            if (dr.Read())
                            {
                                txt_login.Text = dr["userName"].ToString();
                                txt_pass.Text = dr["pass"].ToString();
                                checkAdmin = int.Parse(dr["rules"].ToString());
                            }

                            if (checkAdmin > 0)
                                cb_rules.Checked = true;
                            else
                                cb_rules.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n my_menu_ItemClicked" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                }
                catch { }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (editUser == 1)
                edit();
            else
                insertUser();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    this.TopMost = true;
                    my_menu.Show(dataGridView1, new Point(e.X, e.Y));
                    this.TopMost = false;
                }
                catch { }
            }
        }

        private void updateCheckBox(string iduser)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT count(*) FROM users WHERE rules = 1 and id = '" + iduser + "'";
                    dr = cmd.ExecuteReader();
                    int userAdmin = 0;
                    if (dr.Read())
                        userAdmin = Int16.Parse(dr[0].ToString());
                    dr.Close();

                    if (userAdmin > 0)
                        cb_rules.Checked = true;
                    else
                        cb_rules.Checked = false;
                }
            }
        }

        private void edit()
        {
            if (editUser == 1)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            int cbAdmin = 0;
                            if (cb_rules.Checked == true)
                                cbAdmin = 1;
                            cn.Open();
                            cmd.Connection = cn;
                            cmd.CommandText = "UPDATE users SET userName = '" + txt_login.Text + "', pass = '" + txt_pass.Text + "', rules = '" + cbAdmin + "' WHERE id = '" + idUser + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    editUser = 0;
                    btn_add.Text = "Добавить";
                    txt_login.Text = "";
                    txt_pass.Text = "";
                    cb_rules.Checked = false;
                    update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка, пользователь не найден. " + ex, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    editUser = 0;
                    btn_add.Text = "Добавить";
                }
            }
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            idUser = dataGridView1.SelectedRows[0].Cells["idDataGridViewTextBoxColumn"].Value.ToString();
            updateCheckBox(idUser);
        }
    }
}
