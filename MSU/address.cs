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
    public partial class address : Form
    {
        ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();

        public address()
        {
            InitializeComponent();
        }

        private void address_Load(object sender, EventArgs e)
        {
            my_menu.Items.Add("Редактировать").Name = "edit";
            my_menu.Items.Add("Удалить").Name = "del";
            my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
            my_menu.Items["del"].Image = MSU.Properties.Resources.delete;
            my_menu.Items["edit"].Image = MSU.Properties.Resources.edit;

            update();

        }
        public void update()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT * FROM address order by id_address";
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(cmd.ExecuteReader());
                            int index2 = dataGridView1.FirstDisplayedScrollingRowIndex;
                            dataGridView1.DataSource = dt;

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
            catch (Exception ex)
            {
                MessageBox.Show("Error\n update" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSaerchAddress_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT * FROM address WHERE address.addresscode LIKE '%" + txtSaerchAddress.Text + "%' order by id_address";
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(cmd.ExecuteReader());
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch { }
        }
        private void callNewAddress()
        {
            using (newAddress f2 = new newAddress())
            {
                f2.ShowDialog(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            callNewAddress();
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
                catch {}
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
                            var result = MessageBox.Show("Выделенные адреса будут удалены. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            for (int i = 0; i < countOrd; i++)
                            {
                                item = dataGridView1.SelectedRows[i].Cells["idaddressDataGridViewTextBoxColumn"].Value;
                                if (result == DialogResult.Yes)
                                {
                                    cmd.CommandText = "DELETE FROM address WHERE id_address = '" + item + "'";
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
                    GlobalVariable.edit_address = Convert.ToInt16(row["id_address"].ToString());
                    callNewAddress();
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

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CurrencyManager cmgr = (CurrencyManager)this.dataGridView1.BindingContext[this.dataGridView1.DataSource];
            DataRow row = ((DataRowView)cmgr.Current).Row;
            GlobalVariable.edit_address = Convert.ToInt16(row["id_address"].ToString());
            callNewAddress();
            Cursor = Cursors.Default;
        }
    }
}
