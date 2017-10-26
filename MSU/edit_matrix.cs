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
    public partial class edit_matrix : Form
    {
        SqlDataReader dr;
        BindingSource bs = new BindingSource();
        ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();

        public int edit { set; get; }
        public edit_matrix()
        {
            InitializeComponent();
        }

        private void edit_matrix_Load(object sender, EventArgs e)
        {
            update();
            edit = 0;

            my_menu.Items.Add("Удалить").Name = "del";
            my_menu.Items.Add("Редактировать").Name = "edit";
            my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
            my_menu.Items["del"].Image = MSU.Properties.Resources.delete;
            my_menu.Items["edit"].Image = MSU.Properties.Resources.edit;
        }
        private void insertMatrix()
        {
            if (txt_from.Text != "" & txt_to.Text != "" & txt_km.Text != "")
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            cmd.CommandText = "INSERT into distance (addressFromcode, addressToCode, ditance) VALUES('" + txt_from.Text + "', '" + txt_to.Text + "', '" + txt_km.Text + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    update();
                    MessageBox.Show("Добавлено!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\n update_orders" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void update()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;

                    using (DataTable dt = new DataTable())
                    {
                        cmd.CommandText = "SELECT * FROM distance order by id";
                        dt.Load(cmd.ExecuteReader());
                        bs.DataSource = dt;
                        int index2 = dataGridView1.FirstDisplayedScrollingRowIndex;
                        dataGridView1.DataSource = bs;

                        try
                        {
                            dataGridView1.ClearSelection();
                            int index = GlobalVariable.index_shift;

                            if (index < dataGridView1.Rows.Count)
                            {
                                dataGridView1.Rows[index].Selected = true;
                            }
                            else
                            {
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
                            }
                            dataGridView1.FirstDisplayedScrollingRowIndex = index2;
                        }
                        catch { }
                    }
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (edit == 0)
            {
                insertMatrix();
            }
            else
            {
                updateMatrix();
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
                    int countOrd = dataGridView1.SelectedRows.Count;
                    object item;
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            var result = MessageBox.Show("Выделенные расстояния будут удалены. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            for (int i = 0; i < countOrd; i++)
                            {
                                item = dataGridView1.SelectedRows[i].Cells["id"].Value;
                                if (result == DialogResult.Yes)
                                {
                                    cmd.CommandText = "DELETE FROM distance WHERE id = '" + item + "'";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    update();
                }
                if (e.ClickedItem.Name.ToString() == "edit")
                {
                    CurrencyManager cmgr = (CurrencyManager)this.dataGridView1.BindingContext[this.dataGridView1.DataSource];
                    DataRow row = ((DataRowView)cmgr.Current).Row;
                    string order_number = row["id"].ToString();
                    edit = Convert.ToInt32(order_number);
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            cmd.CommandText = "SELECT * FROM distance WHERE id = '" + order_number + "'";
                            cmd.ExecuteNonQuery();
                            dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                txt_from.Text = dr["addressFromCode"].ToString();
                                txt_to.Text = dr["addressToCode"].ToString();
                                txt_km.Text = dr["ditance"].ToString();
                            }

                            dr.Close();
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
        private void updateMatrix()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "UPDATE distance SET addressfromcode = '" + txt_from.Text + "', addresstocode = '" + txt_to.Text + "', ditance = '" + Convert.ToInt32(txt_km.Text) + "' WHERE id = '" + edit + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                update();
                edit = 0;
                MessageBox.Show("Сохранено!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_from.Text = "";
                txt_km.Text = "";
                txt_to.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateMatrix" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
