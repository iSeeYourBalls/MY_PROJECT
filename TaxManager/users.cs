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
    public partial class users : Form 
    {
        BindingSource bs = new BindingSource();

        public users()
        {
            InitializeComponent();
        }

        private void editDepot_Load(object sender, EventArgs e)
        {
            takeAllUsers();
        }

        private void takeAllUsers()
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
                            cmd.CommandText = "select * from users";
                            dt.Load(cmd.ExecuteReader());
                            bs.DataSource = dt;
                            dgv_editUsers.DataSource = bs;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n takeAllUsers " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void insertNewUser()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (String.IsNullOrEmpty(txt_login.Text) || String.IsNullOrEmpty(txt_pass.Text) || String.IsNullOrEmpty(txt_name.Text))
                        {
                            MessageBox.Show("Все поля должны быть заполнены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            cmd.Connection = cn;
                            cmd.CommandText = "INSERT into users (login, pass, name) VALUES ('" + txt_login.Text + "', '" + txt_pass.Text + "', N'" + txt_name.Text + "')";
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Добавлено!", "Сохранено!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            takeAllUsers();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n insertNewUser " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            insertNewUser();
        }

        private void dgv_editDepot_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();
                    this.TopMost = true;

                    int position_x_y_mouse = dgv_editUsers.HitTest(e.X, e.Y).RowIndex;

                    if (position_x_y_mouse >= 0)
                    {
                        my_menu.Items.Add("Удалить").Name = "delete";
                    }

                    my_menu.Show(dgv_editUsers, new Point(e.X, e.Y));

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
                    my_menu.Items["delete"].Image = TaxManager.Properties.Resources.delete;

                    this.TopMost = false;
                }
                catch
                {

                }
            }
        }

        void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.ToString() == "delete")
            {
                try
                {
                    var result = MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;

                                string selectRow = dgv_editUsers.SelectedRows[0].Cells["id"].Value.ToString();

                                cmd.CommandText = "DELETE FROM users WHERE id = '" + selectRow + "'";
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Удалено!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                takeAllUsers();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\n my_menu_ItemClicked " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgv_editDepot_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgv_editUsers.CurrentCell = dgv_editUsers.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            catch { }
        }

    }
}
