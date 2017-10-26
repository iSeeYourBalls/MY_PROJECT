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
using DataGridViewAutoFilter;

namespace TaxManager
{
    public partial class Registry : Form
    {
        BindingSource bs = new BindingSource();
        DateTime reportDateFrom;
        DateTime reportDateTo;
        object shiftCode;
        public Registry()
        {
            InitializeComponent();
        }

        private void Registry_Load(object sender, EventArgs e)
        {
            reportDateFrom = dateFrom.Value;
            reportDateTo = dateTo.Value;
            shiftCode = DBNull.Value;
            takeInfo();
        }

        private void takeInfo()
        {
            try
            {
                int typeReportDate = rB_ttnDate.Checked ? 0 : 1;

                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;

                        using (DataTable dt = new DataTable())
                        {
                            
                            cmd.CommandText = "EXEC tableRegistry @dateOne, @dateTwo, @shiftCode, @type";
                            cmd.Parameters.Add("@dateOne", SqlDbType.Date);
                            cmd.Parameters.Add("@dateTwo", SqlDbType.Date);
                            cmd.Parameters.Add("@shiftCode", SqlDbType.VarChar);
                            cmd.Parameters.Add("@type", SqlDbType.Int);
                            cmd.Parameters["@dateOne"].Value = reportDateFrom;
                            cmd.Parameters["@dateTwo"].Value = reportDateTo;
                            cmd.Parameters["@shiftCode"].Value = shiftCode;
                            cmd.Parameters["@type"].Value = typeReportDate;
                            dt.Load(cmd.ExecuteReader());
                            bs.DataSource = dt;
                            dgv_registry.DataSource = bs;

                            if (cb_user.Checked)
                                bs.Filter = "managerName = '" + GlobalVariable.userName + "'";
                            else
                                bs.Filter = "";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n takeInfo " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printManyActs()
        {
            int countSelectString = dgv_registry.SelectedRows.Count;
            GlobalVariable.acts = new int[countSelectString];
            for (int i = 0; i < countSelectString; i++)
            {
                GlobalVariable.acts[i] = int.Parse(dgv_registry.SelectedRows[i].Cells["id"].Value.ToString());
            }
            GlobalVariable.isPrintManyActs = true;
            callActsWindow();
        }

        private void callActsWindow()
        {
            using (Acts f1 = new Acts())
            {
                f1.ShowDialog(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            reportDateFrom = dateFrom.Value;
            reportDateTo = dateTo.Value;
            shiftCode = txt_shiftcode.Text;

            if (txt_shiftcode.TextLength > 3)
            {
                reportDateFrom = DateTime.Now.AddDays(-60);
                reportDateTo = DateTime.Now;
            }

            takeInfo();
            Cursor = Cursors.Default;
        }

        private void dgv_registry_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();
                    this.TopMost = true;

                    int position_x_y_mouse = dgv_registry.HitTest(e.X, e.Y).RowIndex;

                    if (position_x_y_mouse >= 0)
                    {
                        my_menu.Items.Add("Изменить статус в \"Протаксирован\"").Name = "changeStatusIsTax";
                        my_menu.Items.Add("Изменить статус в \"Распечатан\"").Name = "changeStatusIsPrinted";
                        my_menu.Items.Add("Сформировать акт").Name = "acts";
                        my_menu.Items.Add("Изменить дату таксировки").Name = "changeDate";
                        my_menu.Items.Add("Пересчитать").Name = "reCalc";
                        my_menu.Items.Add("Удалить").Name = "delete";
                    }

                    my_menu.Show(dgv_registry, new Point(e.X, e.Y));

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
                    my_menu.Items["changeStatusIsTax"].Image = TaxManager.Properties.Resources.subcontractor_icon;
                    my_menu.Items["changeStatusIsPrinted"].Image = TaxManager.Properties.Resources.orders;
                    my_menu.Items["acts"].Image = TaxManager.Properties.Resources.fact;
                    my_menu.Items["delete"].Image = TaxManager.Properties.Resources.delete;
                    my_menu.Items["changeDate"].Image = TaxManager.Properties.Resources.changeDate;
                    my_menu.Items["reCalc"].Image = TaxManager.Properties.Resources.replan;

                    this.TopMost = false;
                }
                catch
                {

                }
            }
        }

        void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.ToString() == "changeStatusIsTax")
            {
                Cursor = Cursors.WaitCursor;
                updateTripStatus(1);
                takeInfo();
                Cursor = Cursors.Default;
            }
            else if (e.ClickedItem.Name.ToString() == "changeStatusIsPrinted")
            {
                Cursor = Cursors.WaitCursor;
                updateTripStatus(2);
                takeInfo();
                Cursor = Cursors.Default;
            }
            else if (e.ClickedItem.Name.ToString() == "delete")
            {
                Cursor = Cursors.WaitCursor;
                deleteFromRegistr();
                takeInfo();
                Cursor = Cursors.Default;
            }
            else if (e.ClickedItem.Name.ToString() == "acts")
            {
                Cursor = Cursors.WaitCursor;
                printManyActs();
                Cursor = Cursors.Default;
            }
            else if (e.ClickedItem.Name.ToString() == "changeDate")
            {
                callWindowChangeDate();
            }
            else if (e.ClickedItem.Name.ToString() == "reCalc")
            {
                massRecalcShiftCode();
            }
        }

        private void callWindowChangeDate()
        {
            using (ChangeDateForRegistr f1 = new ChangeDateForRegistr())
            {
                f1.ShowDialog(this);
            }
        }

        private void massRecalcShiftCode()
        {
            Cursor = Cursors.WaitCursor;
            using (Main main = new Main())
            {
                for (int i = 0; i < dgv_registry.SelectedRows.Count; i++)
                {
                    main.updateDataRegistrForMaxRecalc(dgv_registry.SelectedRows[i].Cells["shiftCode2"].Value.ToString(), dgv_registry.SelectedRows[i].Cells["refDataGridViewTextBoxColumn"].Value.ToString(), int.Parse(dgv_registry.SelectedRows[i].Cells["idTariffType"].Value.ToString()), dgv_registry.SelectedRows[i].Cells["addPointDataGridViewTextBoxColumn"].Value.ToString(), dgv_registry.SelectedRows[i].Cells["isReturns"].Value.ToString());
                }
            }
            Cursor = Cursors.Default;
        }


        public void updateDate(DateTime newDate)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.Parameters.Add("@date", SqlDbType.DateTime);
                        cmd.Parameters["@date"].Value = newDate;

                        int id_trip;

                        for (int i = 0; i < dgv_registry.SelectedRows.Count; i++)
                        {
                            id_trip = int.Parse(dgv_registry.SelectedRows[i].Cells["id"].Value.ToString());
                            cmd.CommandText = "UPDATE dataRegistr SET dateInsert = @date WHERE id = '" + id_trip + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                takeInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateDate " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void updateTripStatus(int id_status)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        int id_trip;

                        for (int i = 0; i < dgv_registry.SelectedRows.Count; i++)
                        {
                            id_trip = int.Parse(dgv_registry.SelectedRows[i].Cells["id"].Value.ToString());
                            cmd.CommandText = "UPDATE dataRegistr SET isTax = '"+ id_status +"' WHERE id = '"+ id_trip +"'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateTripStatus " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_registry_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && Control.ModifierKeys != Keys.Control)
                {
                    dgv_registry.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv_registry.CurrentCell = dgv_registry.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                else if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control || e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.Control)
                {
                    dgv_registry.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
                else
                {
                    dgv_registry.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                }
            }
            catch { }
        }

        private void deleteFromRegistr()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        int id_trip;
                        int idStatus;

                        for (int i = 0; i < dgv_registry.SelectedRows.Count; i++)
                        {
                            id_trip = int.Parse(dgv_registry.SelectedRows[i].Cells["shiftCode2"].Value.ToString());
                            idStatus = int.Parse(dgv_registry.SelectedRows[i].Cells["id_Status"].Value.ToString());

                            if (idStatus == 2)
                            {
                                MessageBox.Show("Акты рейса " + dgv_registry.SelectedRows[i].Cells["shiftCode2"].Value.ToString() + " уже напечатаны, удаление невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                cmd.CommandText = "DELETE FROM dataRegistr WHERE shiftCode = '" + id_trip + "'";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "DELETE FROM newRegistr WHERE shiftCode = '" + id_trip + "'";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n deleteFromRegistr " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            dateTo.Value = dateFrom.Value;
        }

        private void dgv_registry_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            label_countTTN.Text = dgv_registry.RowCount.ToString();
        }
    }
}
