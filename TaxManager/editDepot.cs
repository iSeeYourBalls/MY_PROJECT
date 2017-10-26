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
    public partial class editDepot : Form 
    {
        BindingSource bs = new BindingSource();

        public editDepot()
        {
            InitializeComponent();
        }

        private void editDepot_Load(object sender, EventArgs e)
        {
            takeAllDepot();
        }

        private void takeAllDepot()
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
                            cmd.CommandText = "select * from region";
                            dt.Load(cmd.ExecuteReader());
                            bs.DataSource = dt;
                            dgv_editDepot.DataSource = bs;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n takeAllDepot " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void insertNewDepot()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (String.IsNullOrEmpty(txt_depotCode.Text) || String.IsNullOrEmpty(txt_depotName.Text) || String.IsNullOrEmpty(txt_regionName.Text))
                        {
                            MessageBox.Show("Все поля должны быть заполнены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (isNumeric(txt_depotCode.Text))
                        {
                            cmd.Connection = cn;
                            cmd.CommandText = "INSERT into region (regionCode, regionName, depotName) VALUES ('" + txt_depotCode.Text + "', N'" + txt_regionName.Text + "', N'" + txt_depotName.Text + "')";
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Добавлено!", "Сохранено!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            takeAllDepot();
                        }
                        else
                        {
                            MessageBox.Show("Код РЦ обязательно должен быть цифрой!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n insertNewDepot " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            insertNewDepot();
        }

        private bool isNumeric(String someString)
        {
            int res;
            bool isInt = Int32.TryParse(someString, out res);
            if (isInt)
                return true;
            else
                return false;
        }

        private void dgv_editDepot_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();
                    this.TopMost = true;

                    int position_x_y_mouse = dgv_editDepot.HitTest(e.X, e.Y).RowIndex;

                    if (position_x_y_mouse >= 0)
                    {
                        my_menu.Items.Add("Удалить").Name = "delete";
                    }

                    my_menu.Show(dgv_editDepot, new Point(e.X, e.Y));

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

                                string selectRow = dgv_editDepot.SelectedRows[0].Cells["regionCode"].Value.ToString();

                                cmd.CommandText = "DELETE FROM region WHERE regionCode = '" + selectRow + "'";
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Удалено!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                takeAllDepot();

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
                dgv_editDepot.CurrentCell = dgv_editDepot.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            catch { }
        }

    }
}
