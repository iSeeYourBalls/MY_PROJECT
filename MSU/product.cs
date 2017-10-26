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
    public partial class product : Form
    {
        SqlDataReader dr;
        ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();

        public int editProduct {set;get;}
        
        public product()
        {
            InitializeComponent();
        }

        private void product_Load(object sender, EventArgs e)
        {
            combobox_upload();
            update();
            editProduct = 0;

            my_menu.Items.Add("Редактировать").Name = "edit";
            my_menu.Items.Add("Удалить").Name = "del";
            my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
            my_menu.Items["del"].Image = MSU.Properties.Resources.delete;
            my_menu.Items["edit"].Image = MSU.Properties.Resources.edit;
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
                        cmd.CommandText = "SELECT productCode, productName, productKindName FROM product left join productKind ON product.id_productKind = productKind.productKind";
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
                MessageBox.Show("Error\n update " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void combobox_upload()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "select * from productKind";
                        using (DataTable productKind = new DataTable())
                        {
                            productKind.Load(cmd.ExecuteReader());
                            cb_changeTypeProduct.DataSource = productKind;
                            cb_changeTypeProduct.DisplayMember = "productKindName";
                            cb_changeTypeProduct.ValueMember = "productKind";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_addNewProduct_Click(object sender, EventArgs e)
        {
            if (editProduct == 0)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txt_productName.Text) && !String.IsNullOrEmpty(txt_productCode.Text))
                    {
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;
                                cmd.CommandText = "INSERT into product (product_externalId, productCode, productName, id_productKind)" +
                                    "VALUES(N'" + txt_productCode.Text + "', N'" + txt_productCode.Text + "', N'" + txt_productName.Text + "', '" + cb_changeTypeProduct.SelectedValue + "')";
                                cmd.ExecuteNonQuery();
                            }
                        }
                        update();
                    }
                    else
                    {
                        MessageBox.Show("Обязательные поля должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (editProduct > 0)
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "UPDATE product SET productCode='" + txt_productCode.Text + "', product_externalId = '" + txt_productCode.Text + "', productName = N'" + txt_productName.Text + "', id_productKind = '" + cb_changeTypeProduct.SelectedValue + "' WHERE productCode = '" + editProduct + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                update();
                editProduct = 0;
            }
        }
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
                    int order_number = Convert.ToInt16(row["productCode"].ToString());
                    var result = MessageBox.Show("Склад " + order_number + ", будет удален. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;
                                cmd.CommandText = "DELETE FROM product WHERE productCode = '" + order_number + "'";
                                cmd.ExecuteNonQuery();
                            }
                        }
                        update();
                    }
                }
                if (e.ClickedItem.Name.ToString() == "edit")
                {
                    try
                    {
                        
                        CurrencyManager cmgr = (CurrencyManager)this.dataGridView1.BindingContext[this.dataGridView1.DataSource];
                        DataRow row = ((DataRowView)cmgr.Current).Row;
                        int edit_product = Convert.ToInt16(row["productCode"].ToString());
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;
                                cmd.CommandText = "SELECT productCode, productName, productKindName FROM product left join productKind ON product.id_productKind = productKind.productKind WHERE productCode = '" + edit_product + "'";
                                dr = cmd.ExecuteReader();

                                while (dr.Read())
                                {
                                    txt_productCode.Text = dr[0].ToString();
                                    txt_productName.Text = dr[1].ToString();
                                    editProduct = Convert.ToInt32(dr[0].ToString());
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n my_menu_ItemClicked" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
