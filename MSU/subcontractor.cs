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
    public partial class subcontractor : Form
    {
        BindingSource bsshift = new BindingSource();
        ContextMenuStrip sub_my_menu = new System.Windows.Forms.ContextMenuStrip();

        public subcontractor()
        {
            InitializeComponent();
        }

        private void subcontractor_Load(object sender, EventArgs e)
        {
            updateSub();

            sub_my_menu.Items.Add("Редактировать").Name = "edit";
            sub_my_menu.Items.Add("Удалить").Name = "del";
            sub_my_menu.ItemClicked += new ToolStripItemClickedEventHandler(sub_my_menu_ItemClicked);
            sub_my_menu.Items["del"].Image = MSU.Properties.Resources.delete;
            sub_my_menu.Items["edit"].Image = MSU.Properties.Resources.edit;
        }

        public void updateSub()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    DataTable dtshift = new DataTable();

                    cmd.CommandText = "select * from subcontractor";
                    dtshift.Load(cmd.ExecuteReader());

                    bsshift.DataSource = dtshift;
                    int index2 = dataGridView1.FirstDisplayedScrollingRowIndex;
                    dataGridView1.DataSource = bsshift;
                }
            }
        }

        private void subcontractor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.TopMost = true;
                sub_my_menu.Show(dataGridView1, new Point(e.X, e.Y));
                this.TopMost = false;
            }
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                    dataGridView1.Focus();
                }
                catch { }
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.TopMost = true;
                sub_my_menu.Show(dataGridView1, new Point(e.X, e.Y));
                this.TopMost = false;
            }
        }
        // Обработчик меню правой кнопки мыши
        void sub_my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.ToString() == "del")
            {
                var result = MessageBox.Show("Выделенные подрядчики будут удалены. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;
                    int countOrd = dataGridView1.SelectedRows.Count;
                    object item;
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            for (int i = 0; i < countOrd; i++)
                            {
                                item = dataGridView1.SelectedRows[i].Cells["code"].Value;
                                cmd.CommandText = "DELETE FROM subcontractor WHERE contact_externalId = '" + item + "'";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    updateSub();
                    Cursor = Cursors.Default;
                }
            }
            if (e.ClickedItem.Name.ToString() == "edit")
            {
                GlobalVariable.edit_sub = dataGridView1.SelectedRows[0].Cells["code"].Value.ToString();
                callAddNewSub();
            }
        }

        private void addNewSubcontractor_Click(object sender, EventArgs e)
        {
            callAddNewSub();
        }

        private void callAddNewSub()
        {
            using (add_subcontractor f1 = new add_subcontractor())
            {
                f1.ShowDialog(this);
            }
        }
    }
}
