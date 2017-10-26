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
    public partial class Tariff : Form
    {
        BindingSource bs = new BindingSource();

        public Tariff()
        {
            InitializeComponent();
        }

        private void Tariff_Load(object sender, EventArgs e)
        {

        }

        public void updateTableTariff()
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
                            cmd.CommandText = "select * from tarifComparisonTable where cast(dateInsert as date) between @dateFrom and @dateTo";
                            cmd.Parameters.AddWithValue("@dateFrom", dateTimePicker_periodTariffFrom.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@dateto", dateTimePicker_periodTariffTo.Value.ToString("yyyy-MM-dd"));
                            dt.Load(cmd.ExecuteReader());
                            bs.DataSource = dt;
                            dgv_tariffTable.DataSource = bs;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateTableTariff " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_toSeeTariff_Click(object sender, EventArgs e)
        {
            updateTableTariff();
        }

        private void callAddTariffWindow()
        {
            using (addNewTariff f1 = new addNewTariff())
            {
                f1.ShowDialog(this);
            }
        }

        private void btn_newTariff_Click(object sender, EventArgs e)
        {
            GlobalVariable.editTariff = false;
            callAddTariffWindow();
        }

        private void dgv_tariffTable_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();
                    this.TopMost = true;

                    int position_x_y_mouse = dgv_tariffTable.HitTest(e.X, e.Y).RowIndex;
                    
                    if (position_x_y_mouse >= 0)
                    {
                        my_menu.Items.Add("Удалить").Name = "del";
                        my_menu.Items.Add("Редактировать").Name = "edit";
                    }

                    my_menu.Show(dgv_tariffTable, new Point(e.X, e.Y));

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
                    my_menu.Items["del"].Image = TaxManager.Properties.Resources.delete;
                    my_menu.Items["edit"].Image = TaxManager.Properties.Resources.edit;

                    this.TopMost = false;
                }
                catch
                {

                }
            }
        }
        // Обработчик меню правой кнопки мыши orderGrid
        void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Name.ToString() == "del")
                {
                    var result = MessageBox.Show("Удалить выделенные тарифы? Тарифы имеющие статус активных не будут удалены.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            cn.Open();

                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = cn;

                                object item;

                                for (int i = 0; i < dgv_tariffTable.SelectedRows.Count; i++)
                                {
                                    item = dgv_tariffTable.SelectedRows[i].Cells["id"].Value;

                                    cmd.CommandText = "DELETE FROM tarifComparisonTable WHERE id = '" + item + "' and ISNULL(active, 0) <> 1";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        updateTableTariff();
                    }
                }
                if (e.ClickedItem.Name.ToString() == "edit")
                {
                    editTariff();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n my_menu_ItemClicked " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_tariffTable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && dgv_tariffTable.SelectedRows.Count <= 1)
                dgv_tariffTable.CurrentCell = dgv_tariffTable.Rows[e.RowIndex].Cells[e.ColumnIndex];
        }

        private void dgv_tariffTable_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            editTariff();
        }

        private void editTariff()
        {
            GlobalVariable.editTariff = true;
            GlobalVariable.idTariff = int.Parse(dgv_tariffTable.SelectedRows[0].Cells["id"].Value.ToString());
            callAddTariffWindow();
        }
    }
}
