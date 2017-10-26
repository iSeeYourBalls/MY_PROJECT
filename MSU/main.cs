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
using System.IO;
using System.Data.Linq.Mapping;
using System.Linq.Expressions;
using System.Deployment.Application;
using DataGridViewAutoFilter;
using System.Xml.Linq;
using System.Diagnostics;

namespace MSU
{
    public partial class main : Form
    {
        SqlDataReader dr;
        BindingSource bsshift = new BindingSource();
        BindingSource bs = new BindingSource();
        BindingSource bs_action = new BindingSource();
        ToolTip t = new ToolTip();
        Timer timer = new Timer();

        public string queryInterface = "EXEC takeInterfaceColumn '" + GlobalVariable.userName + "'";
        public int newVersion { get; set; }
        public int currentVersion { get; set;}
        public string orderByOrder { get; set; }
        public string orderByOrderSQL { get; set; }
        public string orderByShift { get; set; }
        public string orderByShiftSQL { get; set; }
        public string allPallKgString { get; set; }

        int actionDragDrop;

        int gridActionHeight;

        public main()
        {
            
            InitializeComponent();
            List<Phone> phones = new List<Phone>
            {
            new Phone { Code="truck", Name="Номер авто"},
            new Phone { Code="shiftcode", Name="Номер ТТН"},
            };
            cb_columnName.DataSource = phones;
            cb_columnName.DisplayMember = "Name";
            cb_columnName.ValueMember = "Code";
        }

        private void main_Load(object sender, EventArgs e)
        {
            GlobalVariable.depot = 8850;
            GlobalVariable.nameColumn = "cityName";
            DateTime gen_date = Convert.ToDateTime(general_date.Text);
            GlobalVariable.general_date = gen_date.ToString("yyyy-MM-dd");

            update_orders("");
            update_shift();
            updateColumnOrders();
            updateColumnShift();
            updateColumnAction();


            string[] cb_plan = { "Все", "Не спланированные", "Спланированные", "Выбранная смена" };
            cb_notPlanned.Items.AddRange(cb_plan);
            cb_notPlanned.SelectedIndex = 0;

            string[] cb_planShift = { "Все", "Не спланированные", "Спланированные"};
            cb_ShiftNotPlanned.Items.AddRange(cb_planShift);
            cb_ShiftNotPlanned.SelectedIndex = 0;

            string[] cb_order = { "Все", "Факт", "План", "Долг"};
            cb_order_fact.Items.AddRange(cb_order);
            cb_order_fact.SelectedIndex = 0;

            frigo();

            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT count(*) FROM users WHERE rules = 1 and userName = '" + GlobalVariable.userName + "'";
                    dr = cmd.ExecuteReader();
                    int userAdmin = 0;
                    if (dr.Read())
                        userAdmin = Int16.Parse(dr[0].ToString());
                    dr.Close();

                    if (userAdmin > 0)
                    {
                        edit_matrix.Enabled = true;
                        newUser.Enabled = true;
                        matrix.Enabled = true;
                        control_Product.Enabled = true;
                    }
                    else
                    {
                        edit_matrix.Enabled = false;
                        newUser.Enabled = false;
                        matrix.Enabled = false;
                        control_Product.Enabled = false;
                    }
                }
            }
            gridActionHeight = gridAction.Size.Height;

            timer.Interval = 1000; //интервал между срабатываниями 1000 миллисекунд
            timer.Tick += new EventHandler(timer_Tick); //подписываемся на события Tick
        }

        // Получаем глобально shiftcode
        private void gridShift_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    GlobalVariable.sh = Convert.ToInt32(gridShift.Rows[e.RowIndex].Cells["shiftcode"].Value);
                    GlobalVariable.homeAddress = Convert.ToInt32(gridShift.Rows[e.RowIndex].Cells["homeAddress"].Value);
                    update_action();
                    if (cb_notPlanned.SelectedIndex == 3)
                    {
                        update_orders("");
                    }
                }
                else
                {
                    try
                    {
                        if (gridShift.SelectedRows.Count <= 1)
                        {
                            gridShift.CurrentCell = gridShift.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            GlobalVariable.index_shift = gridShift.CurrentRow.Index;
                            GlobalVariable.sh = Convert.ToInt32(gridShift.Rows[e.RowIndex].Cells["shiftcode"].Value);
                            GlobalVariable.homeAddress = Convert.ToInt32(gridShift.Rows[e.RowIndex].Cells["homeAddress"].Value);
                            update_action();
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        //обновляем таблицу shift
        public void update_shift()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        int index2 = gridShift.FirstDisplayedScrollingRowIndex;

                        string notPlanned = "";
                        string searchShift = " and " + cb_columnName.SelectedValue.ToString() + " LIKE '%" + txt_searchShift.Text + "%'"; ;

                        if (cb_ShiftNotPlanned.SelectedIndex == 1)
                            notPlanned = "and (select sum(cast(kg as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode) is null";
                        else if (cb_ShiftNotPlanned.SelectedIndex == 2)
                            notPlanned = "and (select sum(cast(kg as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode) is not null";

                        using (DataTable dtshift = new DataTable())
                        {
                            cmd.CommandText = "select shift.truck, shift.tripNumber, shift.homeaddress, case when exists(select * from shift sh1 WHERE cast(sh1.capacityKg as int) < (select sum(cast(kg as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = sh1.shiftcode) and sh1.shiftcode = shift.shiftcode) THEN 1 ELSE 0 end as ErrUpload, case when exists(select * from shift sh1 WHERE cast(sh1.capacityPal as int) < (select sum(cast(pal as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = sh1.shiftcode) and sh1.shiftcode = shift.shiftcode) THEN 1 ELSE 0 end as ErrUploadPal, shift.comment, case when exists (SELECT * FROM action WHERE shiftcode = shift.shiftcode and id_order is not null) THEN 1 ELSE 0 end as planned, shift.datework,driver, statusName, trailer,subcontractor.contactName, shift.shiftcode, shift.capacityKg, shift.capacityPal, (select sum(cast(kg as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode)  as uploadKg,(select sum(cast(pal as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode)  as uploadPal, (select top 1 address.cityName from address join action on address.addressCode = action.start where action.shiftcode = shift.shiftcode and action.sort = '2' order by cast(action.start_finish as int)) as firstCity, cast(shift.startInstant as time(0)) as startInstant, cast(shift.finishInstant as time(0)) as finishInstant from shift  left join dbo.subcontractor ON shift.subcontractor = dbo.subcontractor.contact_externalId left join shiftStatus ON shift.status = shiftStatus.id_status WHERE shift.datework = '" + GlobalVariable.general_date + "' " + notPlanned + " " + searchShift + " " + orderByShiftSQL + " ";
                            dtshift.Load(cmd.ExecuteReader());
                            bsshift.DataSource = dtshift;
                            gridShift.DataSource = bsshift;

                            try
                            {
                                gridShift.FirstDisplayedScrollingRowIndex = index2;
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n update_shift" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //обновляем таблицу действий
        public void update_action()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.CommandText = "select count(*), shift.freight from action join shift on action.shiftcode = shift.shiftcode where action.shiftcode = '" + GlobalVariable.sh + "' and action.actionid = '0' Group by shift.freight";
                        dr = cmd.ExecuteReader();
                        int count_row = 0;
                        string freight = "";
                        while (dr.Read())
                        {
                            count_row = Convert.ToInt32(dr[0].ToString());
                            freight = dr[1].ToString();
                        }

                        string top = "";
                        if (freight == "1")
                            top = "";
                        else
                            top = "UNION select top 1 ac.start, 0, '', adr.addressname, adr.cityName,'','','', (select top 1 start_finish+1 from action where shiftcode = ac.shiftcode order by cast(start_finish as int) desc), '' from action ac left join address adr on ac.start = adr.addresscode where shiftcode = '" + GlobalVariable.sh + "' and ac.start = (select top 1 start from action where action.shiftcode = ac.shiftcode order by action.start_finish)";
                        dr.Close();

                        using (DataTable dt = new DataTable())
                        {
                            cmd.CommandText = "select start, action.id, action.id_order as id_order, address.addressName,cityName,orders.kg,orders.pal,product.productName ,action.start_finish, orders.orderStatus from action left join address on action.start = address.addressCode left join orders on action.id_order = orders.order_number left join product on orders.product = product.productCode where shiftcode = '" + GlobalVariable.sh + "' " + top + " order by cast(action.start_finish as int);";
                            dt.Load(cmd.ExecuteReader());
                            bs_action.DataSource = dt;
                            gridAction.DataSource = bs_action;
                        }

                        cmd.CommandText = "SELECT truck, (select sum(cast(kg as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode)  as uploadKg,(select sum(cast(pal as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode)  as uploadPal FROM shift WHERE shiftcode = '" + GlobalVariable.sh + "'";
                        dr = cmd.ExecuteReader();
                        string count = "";
                        string upkg = "0";
                        string upPal = "0";
                        while (dr.Read())
                        {
                            count = dr[0].ToString();
                            upkg = dr[1].ToString();
                            upPal = dr[2].ToString();
                        }
                        dr.Close();

                        cmd.CommandText = "EXEC GET_KM " + GlobalVariable.sh + "";
                        dr = cmd.ExecuteReader();
                        string kmOne = "";
                        while (dr.Read())
                        {
                            kmOne = dr[0].ToString();
                        }
                        dr.Close();

                        label_TruckName.Text = count + " | Загружено кг - " + upkg + " | Загружено Пал - " + upPal + " | КМ - " + kmOne;
                        gridAction.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n update_action" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridAction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridAction.RowCount; i++)
            {
                if (gridAction.Rows[i].Cells["dataGridViewTextBoxColumn2"].Value.ToString() == "1")
                {
                    gridAction.Rows[i].Cells["kgDataGridViewTextBoxColumn1"].Style.BackColor = Color.Orange;
                    gridAction.Rows[i].Cells["palDataGridViewTextBoxColumn1"].Style.BackColor = Color.Orange;
                }
            }
        }

        //обновляем таблицу заказов
        public void update_orders(string whereOrders)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        int index2 = gridOrder.FirstDisplayedScrollingRowIndex;
                        string notPlanned = "";
                        string fact_plan = "";

                        if (cb_notPlanned.SelectedIndex == 1)
                        {
                            notPlanned = "and action.shiftcode is null";
                        }
                        else if (cb_notPlanned.SelectedIndex == 2)
                        {
                            notPlanned = "and action.shiftcode is not null";
                        }
                        else if (cb_notPlanned.SelectedIndex == 3)
                        {
                            notPlanned = "and shift.shiftcode = '" + GlobalVariable.sh + "'";
                        }
                        else
                        {
                            notPlanned = "";
                        }

                        if (cb_order_fact.SelectedIndex == 1)
                            fact_plan = "and orderStatus = '1'";
                        else if (cb_order_fact.SelectedIndex == 2)
                            fact_plan = "and orderStatus = '0'";
                        else if(cb_order_fact.SelectedIndex == 3)
                            fact_plan = "and orderStatus = '2'";
                        else
                            fact_plan = "";

                        if (txt_search_order.Text.ToString().Length > 1 && GlobalVariable.nameColumn.ToString().Length > 2)
                            whereOrders = " and " + GlobalVariable.nameColumn + " LIKE N'%" + txt_search_order.Text + "%'";

                        using (DataTable dt = new DataTable())
                        {
                            cmd.CommandText = "SELECT order_number, orders.address, address.stopFromDay as stopFromDay, address.stopTillDay as stopTillDay,  address.comment as address_com, orders.orderStatus, address.cityName,orders.comment,orders.kg,orders.pal,product.productName,shift.truck as truck, orders.datework, distance.ditance / 1000 as KM FROM orders left join address on orders.address = address.addressCode left join product on orders.product = product.productCode left join action ON orders.order_number = action.id_order left join shift on action.shiftcode = shift.shiftcode left join distance ON orders.address = distance.addressToCode and distance.addressFromCode = '" + GlobalVariable.depot + "'  WHERE orders.datework = '" + GlobalVariable.general_date + "' " + notPlanned + " " + whereOrders + " " + fact_plan + " " + orderByOrderSQL + " ";
                            dt.Load(cmd.ExecuteReader());
                            bs.DataSource = dt;
                            gridOrder.DataSource = bs;
                        }

                        try
                        {
                            gridOrder.FirstDisplayedScrollingRowIndex = index2;
                        }
                        catch { }

                        orderByOrder = "";

                        GC.WaitForPendingFinalizers();
                        GC.Collect();

                        allKgPall();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n update_orders" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void allKgPall()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.CommandText = "select sum(kg) as kg, sum(cast(pal as int)) as pal from orders where datework = '" + GlobalVariable.general_date + "'";
                        dr = cmd.ExecuteReader();
                        string allPall = "";
                        string allKg = "";
                        if (dr.Read())
                        {
                            allKg = dr["kg"].ToString();
                            allPall = dr["pal"].ToString();

                            allPallAllKg.Visible = true;

                            allPallKgString = "Общий : КГ - " + allKg + " Пал - " + allPall;
                            allPallAllKg.Text = allPallKgString;
                        }
                        else
                        {
                            allPallAllKg.Text = "";
                        }
                        dr.Close();
                    }
                }
            }
            catch { }
        }
        private void updateAllKGPall()
        {
            try
            {
                int summ = 0;
                int summPal = 0;
                int count = gridOrder.SelectedRows.Count;
                for (int i = 0; i < count; i++)
                {
                    summ += Convert.ToInt32(gridOrder.SelectedRows[i].Cells["kg"].Value);
                    summPal += Convert.ToInt32(gridOrder.SelectedRows[i].Cells["pal"].Value);
                }
                allPallAllKg.Text = "Заказы: КГ: " + summ.ToString() + " Пал: " + summPal.ToString() + " Строк: " + count.ToString();
            }
            catch { }
        }

        //Обновляем столбцы
        public void updateColumnOrders()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        for (int i = 0; i < 13; i++)
                        {
                            cmd.CommandText = queryInterface;
                            dr = cmd.ExecuteReader();
                            string columnName = dr.GetName(i).ToString();
                            dr.Close();

                            string gridColumnName = "";
                            cmd.CommandText = "SELECT DatagridColumnName FROM fields WHERE tb_InterfaceColumnName = '" + columnName + "'";
                            dr = cmd.ExecuteReader();

                            if (dr.Read())
                                gridColumnName = dr[0].ToString();

                            dr.Close();

                            cmd.CommandText = queryInterface;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                if (Convert.ToInt16(dr[columnName].ToString()) == 1)
                                    gridOrder.Columns[gridColumnName.ToString()].Visible = true;
                                else
                                    gridOrder.Columns[gridColumnName.ToString()].Visible = false;
                            }
                            dr.Close();
                        }

                        cmd.CommandText = "select * from usercolumn WHERE userName = '" + GlobalVariable.userName + "' and tableType = 1 order by id desc";
                        dr = cmd.ExecuteReader();

                        string checkColumnNull = "";
                        if (dr.Read())
                        {
                            checkColumnNull = dr["columnWidth"].ToString();
                        }
                        dr.Close();

                        using (DataTable schemaTable = new DataTable())
                        {
                            schemaTable.Load(cmd.ExecuteReader());

                            if (!String.IsNullOrEmpty(checkColumnNull))
                            {
                                foreach (DataRow importRow in schemaTable.Rows)
                                {
                                    gridOrder.Columns[importRow["tableName"].ToString()].DisplayIndex = int.Parse(importRow["columnindex"].ToString());
                                    if (!String.IsNullOrEmpty(importRow["columnWidth"].ToString()))
                                        gridOrder.Columns[importRow["tableName"].ToString()].Width = int.Parse(importRow["columnWidth"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateColumnOrders" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateColumnShift()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        for (int i = 13; i < 28; i++)
                        {
                            cmd.CommandText = queryInterface;
                            dr = cmd.ExecuteReader();
                            string columnName = dr.GetName(i).ToString();
                            dr.Close();

                            string gridColumnName = "";
                            cmd.CommandText = "SELECT DatagridColumnName FROM fields WHERE tb_InterfaceColumnName = '" + columnName + "'";
                            dr = cmd.ExecuteReader();

                            if (dr.Read())
                                gridColumnName = dr[0].ToString();

                            dr.Close();

                            cmd.CommandText = queryInterface;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                if (Convert.ToInt16(dr[columnName].ToString()) == 1)
                                    gridShift.Columns[gridColumnName.ToString()].Visible = true;
                                else
                                    gridShift.Columns[gridColumnName.ToString()].Visible = false;
                            }
                            dr.Close();
                        }

                        cmd.CommandText = "select * from usercolumn WHERE userName = '" + GlobalVariable.userName + "' and tableType = 2";
                        dr = cmd.ExecuteReader();

                        string checkColumnNull = "";
                        if (dr.Read())
                            checkColumnNull = dr["columnWidth"].ToString();

                        dr.Close();

                        using (DataTable schemaTable = new DataTable())
                        {
                            schemaTable.Load(cmd.ExecuteReader());

                            if (!String.IsNullOrEmpty(checkColumnNull))
                            {
                                foreach (DataRow importRow in schemaTable.Rows)
                                {
                                    gridShift.Columns[importRow["tableName"].ToString()].DisplayIndex = int.Parse(importRow["columnindex"].ToString());
                                    if (!String.IsNullOrEmpty(importRow["columnWidth"].ToString()))
                                        gridShift.Columns[importRow["tableName"].ToString()].Width = int.Parse(importRow["columnWidth"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateColumnShift" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateColumnAction()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        for (int i = 28; i < 34; i++)
                        {
                            cmd.CommandText = queryInterface;
                            dr = cmd.ExecuteReader();
                            string columnName = dr.GetName(i).ToString();
                            dr.Close();

                            string gridColumnName = "";
                            cmd.CommandText = "SELECT DatagridColumnName FROM fields WHERE tb_InterfaceColumnName = '" + columnName + "'";
                            dr = cmd.ExecuteReader();

                            if (dr.Read())
                                gridColumnName = dr[0].ToString();

                            dr.Close();

                            cmd.CommandText = queryInterface;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                if (Convert.ToInt16(dr[columnName].ToString()) == 1)
                                    gridAction.Columns[gridColumnName.ToString()].Visible = true;
                                else
                                    gridAction.Columns[gridColumnName.ToString()].Visible = false;
                            }
                            dr.Close();
                        }

                        cmd.CommandText = "select * from usercolumn WHERE userName = '" + GlobalVariable.userName + "' and tableType = 3";
                        dr = cmd.ExecuteReader();

                        string checkColumnNull = "";
                        if (dr.Read())
                            checkColumnNull = dr["columnWidth"].ToString();

                        dr.Close();

                        using (DataTable schemaTable = new DataTable())
                        {
                            schemaTable.Load(cmd.ExecuteReader());

                            if (!String.IsNullOrEmpty(checkColumnNull))
                            {
                                foreach (DataRow importRow in schemaTable.Rows)
                                {
                                    gridAction.Columns[importRow["tableName"].ToString()].DisplayIndex = int.Parse(importRow["columnindex"].ToString());
                                    if (!String.IsNullOrEmpty(importRow["columnWidth"].ToString()))
                                        gridAction.Columns[importRow["tableName"].ToString()].Width = int.Parse(importRow["columnWidth"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateColumnAction" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void updateStatus(string id_status)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;

                    int countString = gridShift.SelectedRows.Count;

                    string selectShiftcode = "";

                    for (int i = 0; i < countString; i++)
                    {
                        selectShiftcode = gridShift.SelectedRows[i].Cells["shiftcode"].Value.ToString();
                        cmd.CommandText = "UPDATE shift set status = '" + id_status + "' WHERE shiftcode = '" + selectShiftcode + "'";
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            update_shift();
        }

        //Переносим заказ
        private void action_DragEnter(object sender, DragEventArgs e)
        {
            if (actionDragDrop == 1)
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.Copy;
        }

        //Планируем заказы
        private void action_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                int rowIndexOfItemUnderMouseToDrop;
                Point clientPoint = gridAction.PointToClient(new Point(e.X, e.Y));
                rowIndexOfItemUnderMouseToDrop = gridAction.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (gridAction.Rows[rowIndexOfItemUnderMouseToDrop].Index != gridAction.SelectedRows[0].Index)
                {
                    if (e.Effect == DragDropEffects.Move)
                    {
                        Cursor = Cursors.WaitCursor;

                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;

                                cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.sh + "'";
                                dr = cmd.ExecuteReader();
                                string count = "";
                                while (dr.Read())
                                {
                                    count = dr[0].ToString();
                                }
                                dr.Close();

                                int countOrd = gridAction.SelectedRows.Count;

                                for (int i = 0; i < countOrd; i++)
                                {
                                    int idActionOn = Convert.ToInt32(gridAction.Rows[rowIndexOfItemUnderMouseToDrop].Cells["id"].Value); // На что ложим

                                    int idActionWhat = Convert.ToInt32(gridAction.SelectedRows[i].Cells["id"].Value); // Что ложим

                                    cmd.CommandText = "SELECT start_finish FROM action WHERE id = '" + idActionOn + "'";
                                    dr = cmd.ExecuteReader();

                                    int startFinishOn = 1;

                                    if (dr.Read())
                                        startFinishOn = Convert.ToInt32(dr[0].ToString());
                                    dr.Close();

                                    cmd.CommandText = "update action set start_finish = start_finish+1 where shiftcode = " + GlobalVariable.sh + " and cast(start_finish as int) > " + startFinishOn + "";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "update action set start_finish = " + startFinishOn + " + " + (i + 1) + " where id = " + idActionWhat + "";
                                    cmd.ExecuteNonQuery();

                                    // Обновляем порядковый номер выгрузки
                                    cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.sh + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.sh + "'";
                                    cmd.ExecuteNonQuery();

                                    // Обновляем финишный адрес доставки
                                    cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.sh + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(count) + "' from action WHERE shiftcode = '" + GlobalVariable.sh + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.sh + "'";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.sh + "'";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.sh + "'";
                                    cmd.ExecuteNonQuery();

                                    // для маршрутного листа
                                    cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.sh + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.sh + "' and actionid = '0'";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        update_action();
                        update_shift();
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch { }

            try
            {
                if (e.Effect == DragDropEffects.Copy)
                {
                    Cursor = Cursors.WaitCursor;
                    int countOrd = gridOrder.SelectedRows.Count;
                    object item;
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            if (GlobalVariable.sh != 0)
                            {
                                for (int i = 0; i < countOrd; i++)
                                {
                                    item = gridOrder.SelectedRows[i].Cells["order_number"].Value;

                                    // Спланирован ли рейс?
                                    cmd.CommandText = "SELECT Count(*) FROM action WHERE action.id_order = '" + item + "'";
                                    dr = cmd.ExecuteReader();
                                    string count_ord = "";
                                    while (dr.Read())
                                    {
                                        count_ord = dr[0].ToString();
                                    }
                                    dr.Close();

                                    if (count_ord == "0" || count_ord == null)
                                    {

                                        if (item != null)
                                        {
                                            try
                                            {
                                                //cn.Open();
                                                // Спланирован ли рейс?
                                                cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.sh + "'";
                                                dr = cmd.ExecuteReader();
                                                string count = "";
                                                while (dr.Read())
                                                {
                                                    count = dr[0].ToString();
                                                }
                                                dr.Close();

                                                //Номер авто
                                                cmd.CommandText = "SELECT top 1 truck FROM shift WHERE shift.shiftcode = '" + GlobalVariable.sh + "'";
                                                dr = cmd.ExecuteReader();
                                                string truck = "";
                                                while (dr.Read())
                                                {
                                                    truck = dr[0].ToString();
                                                }
                                                dr.Close();

                                                // Получаем куда ехать
                                                cmd.CommandText = "SELECT top 1 address FROM orders WHERE order_number = '" + item + "'";
                                                dr = cmd.ExecuteReader();
                                                string finish = "";
                                                while (dr.Read())
                                                {
                                                    finish = dr[0].ToString();
                                                }
                                                dr.Close();

                                                // если рейса еще не было значит старт с РЦ
                                                if (count == null || count == "0")
                                                {
                                                    cmd.CommandText = "INSERT into action (shiftcode,start,start_finish, actionid) VALUES('" + GlobalVariable.sh + "','" + GlobalVariable.homeAddress + "','1', '0')";
                                                    cmd.ExecuteNonQuery();
                                                    cmd.CommandText = "INSERT into action (id_order,shiftcode,start,start_finish,finish, actionid) VALUES('" + item + "','" + GlobalVariable.sh + "','" + finish + "','2','" + GlobalVariable.homeAddress + "', '0')";
                                                    cmd.ExecuteNonQuery();
                                                    cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.sh + "' and start <> '" + GlobalVariable.homeAddress + "') T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.sh + "'";
                                                    cmd.ExecuteNonQuery();

                                                    // для маршрутного листа
                                                    cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.sh + "' and (actionid = '0' or actionid is null)) T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.sh + "' and (actionid = '0' or actionid is null)";
                                                    cmd.ExecuteNonQuery();

                                                }
                                                //если рейс есть
                                                else
                                                {
                                                    int sort = (Convert.ToInt16(count) + 1);
                                                    dr.Close();
                                                    cmd.CommandText = "INSERT into action (id_order,shiftcode,start,start_finish,finish) VALUES('" + item + "','" + GlobalVariable.sh + "','" + finish + "','" + sort + "','" + GlobalVariable.homeAddress + "')";
                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error\n action_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Этот " + item + " заказ уже спланирован", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        dr.Close();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ни одна смена не выбрана! Выберите смену.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dr.Close();
                            }


                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.sh + "' and start <> (SELECT TOP 1 homeAddress FROM shift WHERE shiftcode = '" + GlobalVariable.sh + "')) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.sh + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.sh + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.sh + "'";
                            cmd.ExecuteNonQuery();

                            // для маршрутного листа
                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.sh + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.sh + "' and actionid = '0'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //update таблиц после цикла
                    try
                    {
                        update_orders("");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n update_orders_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    try
                    {
                        update_action();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n update_action_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    try
                    {
                        update_shift();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n update_shift_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // 
                    Cursor = Cursors.Default;
                }
            }
            catch { }
        }

        //Получаем order_number заказа который перетаскиваем
        private void gridOrder_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    if (gridOrder.Rows[e.RowIndex].Cells[e.ColumnIndex].RowIndex >= -1 && (Control.ModifierKeys & Keys.Shift) != Keys.Shift && (Control.ModifierKeys & Keys.Control) != Keys.Control)
                    {
                        allPallAllKg.Text = allPallKgString;

                        int count = gridOrder.SelectedRows.Count;
                        if (count <= 15)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                gridOrder.DoDragDrop(gridOrder.SelectedRows[i].Cells["order_number"].Value, DragDropEffects.Copy);
                            }
                        }
                    }
                    else
                    {
                        gridOrder.MultiSelect = true;
                    }
                }
                catch { }
            }
            else if (e.Button == MouseButtons.Right)
            {
                try
                {
                    if (gridOrder.SelectedRows.Count <= 1)
                        gridOrder.CurrentCell = gridOrder.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                catch { };
            }
        }

        // Вызываем меню правой кнопки мыши таблица заказов
        private void gridOrder_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                GlobalVariable.index_order = gridOrder.CurrentRow.Index;
            }
            catch { }
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();
                    this.TopMost = true;

                    int position_x_y_mouse = gridOrder.HitTest(e.X, e.Y).RowIndex;
                    if (position_x_y_mouse >= 0)
                    {
                        my_menu.Items.Add("Удалить").Name = "del";
                        my_menu.Items.Add("Редактировать").Name = "edit";
                        my_menu.Items.Add("Отделить часть заказа").Name = "divided";
                        my_menu.Items.Add("Распланировать").Name = "unPlanned";
                        my_menu.Items.Add("В факт").Name = "fact";
                        my_menu.Items.Add("В план").Name = "plan";
                        my_menu.Items.Add("В долг").Name = "duty";
                        my_menu.Items.Add("Создать новый").Name = "createNewOrder";
                    }

                    my_menu.Show(gridOrder, new Point(e.X, e.Y));

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
                    my_menu.Items["del"].Image = MSU.Properties.Resources.delete;
                    my_menu.Items["edit"].Image = MSU.Properties.Resources.edit;
                    my_menu.Items["divided"].Image = MSU.Properties.Resources.diveded;
                    my_menu.Items["unPlanned"].Image = MSU.Properties.Resources.unplan;
                    my_menu.Items["fact"].Image = MSU.Properties.Resources.fact;
                    my_menu.Items["plan"].Image = MSU.Properties.Resources.plan;
                    my_menu.Items["createNewOrder"].Image = MSU.Properties.Resources.add_newOrder;
                    my_menu.Items["duty"].Image = MSU.Properties.Resources.duty;
                    
                    this.TopMost = false;

                    my_menu.Closing += my_menu_Closing;
                }
                catch
                {

                }
            }
        }

        void my_menu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            gridOrder.MultiSelect = false;
        }

        // Обработчик меню правой кнопки мыши orderGrid
        void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
               // gridOrder.CurrentCell.Selected = true;
                if (e.ClickedItem.Name.ToString() == "del")
                {
                    var result = MessageBox.Show("Выделенные заказы будут удалены. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                   
                    if (result == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;
                        int count_ord=0;
                        int countOrd = gridOrder.SelectedRows.Count;
                        object item;

                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;

                                for (int i = 0; i < countOrd; i++)
                                {
                                    item = gridOrder.SelectedRows[i].Cells["order_number"].Value;
                                    // Спланированы ли заказы?
                                    cmd.CommandText = "SELECT Count(*) FROM action WHERE action.id_order = '" + item + "'";
                                    dr = cmd.ExecuteReader();
                                    if (dr.Read())
                                    {
                                        count_ord += Convert.ToInt16(dr[0].ToString());
                                    }
                                    dr.Close();
                                }

                                if (count_ord == 0)
                                {
                                    for (int i = 0; i < countOrd; i++)
                                    {
                                        item = gridOrder.SelectedRows[i].Cells["order_number"].Value;
                                        cmd.CommandText = "DELETE FROM orders WHERE order_number = '" + item + "'";
                                        cmd.ExecuteNonQuery();
                                    }
                                    update_orders("");
                                }
                                else
                                {
                                    MessageBox.Show("Один из выделенных заказов спланирован!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                                }
                            }
                        }
                        Cursor = Cursors.Default;
                    }
                }
                if (e.ClickedItem.Name.ToString() == "edit")
                {
                    string order_number = gridOrder.SelectedRows[0].Cells["order_number"].Value.ToString();
                    GlobalVariable.edit_order = order_number;
                    callAddOrder();
                }
                if (e.ClickedItem.Name.ToString() == "createNewOrder")
                {
                    callAddOrder();
                }
                if (e.ClickedItem.Name.ToString() == "divided")
                {
                    string order_number = gridOrder.SelectedRows[0].Cells["order_number"].Value.ToString();
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;

                            cmd.CommandText = "SELECT Count(*) FROM action WHERE action.id_order = '" + order_number + "'";
                            dr = cmd.ExecuteReader();

                            string count_ord = "";
                            while (dr.Read())
                            {
                                count_ord = dr[0].ToString();
                            }
                            dr.Close();

                            if (count_ord == null || count_ord == "0" || count_ord == "")
                            {
                                GlobalVariable.edit_order = order_number;
                                callDividedOrder();
                            }
                            else
                            {
                                MessageBox.Show("Нельзя разделить спланированный заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                if (e.ClickedItem.Name.ToString() == "fact")
                {
                    Cursor = Cursors.WaitCursor;
                    int countOrd = gridOrder.SelectedRows.Count;
                    object item;

                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;

                            for (int i = 0; i < countOrd; i++)
                            {
                                item = gridOrder.SelectedRows[i].Cells["order_number"].Value;

                                cmd.CommandText = "UPDATE orders SET orderStatus = '1' WHERE orders.order_number = '" + item + "'";
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }

                    update_orders("");
                    update_action();
                    Cursor = Cursors.Default;
                }
                if (e.ClickedItem.Name.ToString() == "duty")
                {
                    Cursor = Cursors.WaitCursor;
                    int countOrd = gridOrder.SelectedRows.Count;
                    object item;

                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;

                            for (int i = 0; i < countOrd; i++)
                            {
                                item = gridOrder.SelectedRows[i].Cells["order_number"].Value;

                                cmd.CommandText = "UPDATE orders SET orderStatus = '2' WHERE orders.order_number = '" + item + "'";
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    update_orders("");
                    Cursor = Cursors.Default;
                }
                if (e.ClickedItem.Name.ToString() == "plan")
                {
                    var result = MessageBox.Show("Изменить статус заказам в план?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;
                        int countOrd = gridOrder.SelectedRows.Count;
                        object item;
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;

                                for (int i = 0; i < countOrd; i++)
                                {
                                    item = gridOrder.SelectedRows[i].Cells["order_number"].Value;

                                    cmd.CommandText = "UPDATE orders SET orderStatus = '0' WHERE orders.order_number = '" + item + "'";
                                    cmd.ExecuteNonQuery();

                                }
                            }
                        }
                        update_orders("");
                        Cursor = Cursors.Default;
                    }
                }

                if (e.ClickedItem.Name.ToString() == "unPlanned")
                {
                    var result = MessageBox.Show("Распланировать выбранные заказы?", "Распланировать?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;

                        int count_ord = 0;
                        int countOrd = gridOrder.SelectedRows.Count;
                        object item;

                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;

                                if (count_ord == 0)
                                {
                                    for (int i = 0; i < countOrd; i++)
                                    {
                                        item = gridOrder.SelectedRows[i].Cells["order_number"].Value;

                                        cmd.CommandText = "DELETE FROM action WHERE id_order = '" + item + "'";
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.sh + "'";
                                dr = cmd.ExecuteReader();
                                string count = "";
                                while (dr.Read())
                                {
                                    count = dr[0].ToString();
                                }
                                dr.Close();

                                // Обновляем порядковый номер выгрузки
                                cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.sh + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.sh + "'";
                                cmd.ExecuteNonQuery();
                                // Обновляем финишный адрес доставки
                                cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.sh + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(count) + "' from action WHERE shiftcode = '" + GlobalVariable.sh + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.sh + "'";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.sh + "'";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.sh + "'";
                                cmd.ExecuteNonQuery();

                                // для маршрутного листа
                                cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.sh + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.sh + "' and actionid = '0'";
                                cmd.ExecuteNonQuery();
                            }
                        }

                        update_orders("");
                        update_action();
                        update_shift();

                        Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n my_menu_ItemClicked" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void callAddOrder()
        {
            using (edit_order f2 = new edit_order())
            {
                f2.ShowDialog(this);
            }
        }
        private void callDividedOrder()
        {
            using (divided f3 = new divided())
            {
                f3.ShowDialog(this);
            }
        }
        private void callAddShift()
        {
            using (add_shift f4 = new add_shift())
            {
                f4.ShowDialog(this);
            }
        }
        private void callChangeStatus()
        {
            using (status f5 = new status())
            {
                f5.ShowDialog(this);
            }
        }
        private void callWaybill()
        {
            using (Waybill f6 = new Waybill())
            {
                f6.ShowDialog(this);
            }
        }
        private void callLoading()
        {
            using (loadingList f7 = new loadingList())
            {
                f7.ShowDialog();
            }
        }
        private void callMatrix()
        {
            using (edit_matrix f8 = new edit_matrix())
            {
                f8.ShowDialog();
            }
        }
        private void callnewUser()
        {
            using (users f9 = new users())
            {
                f9.ShowDialog();
            }
        }
        private void callnewAddress()
        {
            using (address f10 = new address())
            {
                f10.ShowDialog();
            }
        }
        private void callProduct()
        {
            using (product f11 = new product())
            {
                f11.ShowDialog(this);
            }
        }
        private void callInterface()
        {
            using (edit_interface f12 = new edit_interface())
            {
                f12.ShowDialog(this);
            }
        }
        private void callrePlan()
        {
            using (rePlanManyShift f13 = new rePlanManyShift())
            {
                f13.ShowDialog(this);
            }
        }
        private void callLogist()
        {
            using (logist f14 = new logist())
            {
                f14.ShowDialog(this);
            }
        }
        private void callSubcontractor()
        {
            using (subcontractor f15 = new subcontractor())
            {
                f15.ShowDialog(this);
            }
        }
        private void callshiftGridReport()
        {
            using (shiftGridReport f16 = new shiftGridReport())
            {
                f16.ShowDialog(this);
            }
        }

        private void callOrderGreed()
        {
            using (orderGreedReport f17 = new orderGreedReport())
            {
                f17.ShowDialog();
            }
        }

        private void calloperationalReport()
        {
            using (operationalReport f18 = new operationalReport())
            {
                f18.ShowDialog();
            }
        }

        private void callCheckOutTimeWarehouseReport()
        {
            using (checkOutTimeWareHouse f19 = new checkOutTimeWareHouse())
            {
                f19.ShowDialog();
            }
        }
        // Вызываем меню правой кнопки мыши таблица action
        private void gridAction_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Control.ModifierKeys != Keys.Control)
            {
                try
                {
                    ContextMenuStrip action_my_menu = new System.Windows.Forms.ContextMenuStrip();

                    int position_x_y_mouse = gridAction.HitTest(e.X, e.Y).RowIndex;
                    gridAction.Rows[position_x_y_mouse].Selected = true;

                    if (position_x_y_mouse >= 0)
                        action_my_menu.Items.Add("Распланировать").Name = "unPlanned";

                    action_my_menu.Show(gridAction, new Point(e.X, e.Y));
                    action_my_menu.ItemClicked += new ToolStripItemClickedEventHandler(action_my_menu_ItemClicked);
                    action_my_menu.Items["unPlanned"].Image = MSU.Properties.Resources.unplan;

                }
                catch {}
            }
        }
        // Обработчик меню правой кнопки мыши action
        void action_my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Name.ToString() == "unPlanned")
                {
                    Cursor = Cursors.WaitCursor;
                    int count_ord = 0;
                    int countOrd = gridAction.SelectedRows.Count;
                    object item;
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            if (count_ord == 0)
                            {
                                for (int i = 0; i < countOrd; i++)
                                {
                                    item = gridAction.SelectedRows[i].Cells["id_order"].Value;

                                    cmd.CommandText = "DELETE FROM action WHERE id_order = '" + item + "'";
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.sh + "'";
                            dr = cmd.ExecuteReader();
                            string count = "";
                            while (dr.Read())
                            {
                                count = dr[0].ToString();
                            }
                            dr.Close();

                            // Обновляем порядковый номер выгрузки
                            cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.sh + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.sh + "'";
                            cmd.ExecuteNonQuery();
                            // Обновляем финишный адрес доставки
                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.sh + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(count) + "' from action WHERE shiftcode = '" + GlobalVariable.sh + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.sh + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.sh + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.sh + "'";
                            cmd.ExecuteNonQuery();

                            // для маршрутного листа
                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.sh + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.sh + "' and actionid = '0'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    update_orders("");
                    update_action();
                    update_shift();
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n action_my_menu_ItemClicked" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true || e.Shift == true)
                gridOrder.MultiSelect = true;

            if (e.Control && e.KeyCode == Keys.F)
            {
                try
                {
                    string indexColumn = gridOrder.CurrentCell.ColumnIndex.ToString();              
                    GlobalVariable.nameColumn = gridOrder.Columns[Convert.ToInt32(indexColumn)].DataPropertyName;
                    string columnString = gridOrder.Columns[Convert.ToInt32(indexColumn)].HeaderText;
                    label_columnName.Text = "Столбец : " + columnString;
                }
                catch { }
            }
            if (e.Control && e.KeyCode == Keys.A)
                gridOrder.MultiSelect = true;
        }

        private void txt_search_order_TextChanged(object sender, EventArgs e)
        {
            update_orders("");
        }

        // Вызываем меню правой кнопки мыши таблица shift
        private void gridShift_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                GlobalVariable.index_shift = gridShift.CurrentRow.Index;
            }
            catch { }
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    ContextMenuStrip shift_my_menu = new System.Windows.Forms.ContextMenuStrip();
                    this.TopMost = true;
                    int position_x_y_mouse = gridShift.HitTest(e.X, e.Y).RowIndex;
                    if (position_x_y_mouse >= 0)
                    {
                        shift_my_menu.Items.Add("Создать новый рейс").Name = "add_new_shift";
                        shift_my_menu.Items.Add("Оптимизировать рейс").Name = "optimization";
                        shift_my_menu.Items.Add("Редактировать").Name = "edit";
                        shift_my_menu.Items.Add("Изменить статус").Name = "changeStatus";
                        shift_my_menu.Items.Add("Маршрутный лист").Name = "waybill";
                        shift_my_menu.Items.Add("Отгрузочный лист").Name = "loading";
                        shift_my_menu.Items.Add("Распланировать").Name = "unPlanned";
                        shift_my_menu.Items.Add("Распланировать и удалить").Name = "del";
                        if (gridShift.SelectedRows.Count == 2)
                            shift_my_menu.Items.Add("Перепланировать").Name = "replan";
                        shift_my_menu.Items.Add("Создать смену").Name = "createNewShift";
                    }
                    shift_my_menu.Show(gridShift, new Point(e.X, e.Y));
                    shift_my_menu.ItemClicked += new ToolStripItemClickedEventHandler(shift_my_menu_ItemClicked);
                    shift_my_menu.Items["del"].Image = MSU.Properties.Resources.delete;
                    shift_my_menu.Items["edit"].Image = MSU.Properties.Resources.edit;
                    shift_my_menu.Items["changeStatus"].Image = MSU.Properties.Resources.changestatus;
                    shift_my_menu.Items["waybill"].Image = MSU.Properties.Resources.waybill;
                    shift_my_menu.Items["loading"].Image = MSU.Properties.Resources.loading;
                    shift_my_menu.Items["unPlanned"].Image = MSU.Properties.Resources.unplan;
                    shift_my_menu.Items["add_new_shift"].Image = MSU.Properties.Resources._new;
                    shift_my_menu.Items["optimization"].Image = MSU.Properties.Resources.optimizator;
                    shift_my_menu.Items["createNewShift"].Image = MSU.Properties.Resources.add_newShift;
                    if (gridShift.SelectedRows.Count == 2)
                        shift_my_menu.Items["replan"].Image = MSU.Properties.Resources.replan;
                    this.TopMost = false;
                }
                catch{}
            }
        }
        // Обработчик меню правой кнопки мыши shift
        void shift_my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Name.ToString() == "replan")
                {
                    int countOrd = gridShift.SelectedRows.Count;
                    if (countOrd == 2)
                    {
                        GlobalVariable.replanOne =  Convert.ToInt32(gridShift.SelectedRows[0].Cells["shiftcode"].Value);
                        GlobalVariable.replanTwo = Convert.ToInt32(gridShift.SelectedRows[1].Cells["shiftcode"].Value);
                        callrePlan();
                    }
                }
                if (e.ClickedItem.Name.ToString() == "createNewShift")
                {
                    callAddShift();
                }
                if (e.ClickedItem.Name.ToString() == "del")
                {
                    var result = MessageBox.Show("Выделенные смены будут удалены. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;

                                Cursor = Cursors.WaitCursor;
                                int countOrd = gridShift.SelectedRows.Count;
                                object item;
                                object truck_number;

                                for (int i = 0; i < countOrd; i++)
                                {
                                    item = gridShift.SelectedRows[i].Cells["shiftcode"].Value;
                                    truck_number = gridShift.SelectedRows[i].Cells["truck"].Value;

                                    cmd.CommandText = "DELETE FROM shift WHERE shiftcode = '" + item + "'";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "DELETE FROM action WHERE shiftcode = '" + item + "'";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "UPDATE shift SET tripNumber = T2.rownum FROM shift T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(tripNumber as int) ASC) AS rownum, shiftcode FROM shift where truck = '" + truck_number + "' and datework = '" + GlobalVariable.general_date + "') T2 ON T1.shiftcode = T2.shiftcode where T1.truck = '" + truck_number + "' and T1.datework = '" + GlobalVariable.general_date + "'";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        update_shift();
                        update_orders("");
                        Cursor = Cursors.Default;
                    }
                }

                string truck = gridShift.SelectedRows[0].Cells["truck"].Value.ToString();
                string order_number = gridShift.SelectedRows[0].Cells["shiftcode"].Value.ToString();
                gridShift.CurrentRow.Selected = true;

                if (e.ClickedItem.Name.ToString() == "add_new_shift")
                {
                    var result = MessageBox.Show("Создать следующий рейс этой смены?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;
                                cmd.CommandText = "SELECT Count(*) FROM shift WHERE shift.truck = '" + truck + "' and datework = '" + GlobalVariable.general_date + "'";
                                dr = cmd.ExecuteReader();
                                string count = "";
                                if (dr.Read())
                                {
                                    count = dr[0].ToString();
                                }
                                dr.Close();

                                int tripNumber = Convert.ToInt16(count) + 1;

                                cmd.CommandText = "INSERT INTO shift (truck, capacityKg, capacityPal, datework, trailer, driver, subcontractor, tripNumber, status, homeAddress) SELECT truck, capacityKg, capacityPal, datework, trailer, driver, subcontractor, " + tripNumber + ", '1', homeAddress FROM shift where shiftcode = '" + order_number + "'";
                                cmd.ExecuteNonQuery();

                            }
                        }

                        update_shift();
                        Cursor = Cursors.Default;
                    }
                }
                if (e.ClickedItem.Name.ToString() == "optimization")
                {
                    Cursor = Cursors.WaitCursor;
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;

                            cmd.CommandText = "SELECT top 1 offOn FROM checkOptimization";
                            dr = cmd.ExecuteReader();
                            int checkOffOn = 0;
                            while (dr.Read())
                            {
                                checkOffOn = Convert.ToInt32(dr[0].ToString());
                            }
                            dr.Close();

                            if (checkOffOn == 0)
                            {
                                cmd.CommandText = "UPDATE checkOptimization SET offOn = 1";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.sh + "' and actionid = 0";
                                dr = cmd.ExecuteReader();
                                int count = 0;
                                while (dr.Read())
                                {
                                    count = Convert.ToInt32(dr[0].ToString());
                                }
                                dr.Close();

                                for (int i = 0; i < count; i++)
                                {
                                    cmd.CommandText = "EXEC newOptimization " + GlobalVariable.sh + ", " + GlobalVariable.homeAddress + "";
                                    cmd.ExecuteNonQuery();
                                }

                                cmd.CommandText = "EXEC updateFinishAddress " + GlobalVariable.sh + "";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "UPDATE checkOptimization SET offOn = 0";
                                cmd.ExecuteNonQuery();

                                update_action();
                                update_shift();
                            }
                            else
                            {
                                MessageBox.Show("Оптимизация не возможна! Оптимизатор занят другим процессом, повторите попытку через несколько секунд.", "Оптимизация остановлена", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                    }
                    Cursor = Cursors.Default;
                }

                if (e.ClickedItem.Name.ToString() == "edit")
                {
                    GlobalVariable.edit_shift = order_number;
                    callAddShift();
                }
                if (e.ClickedItem.Name.ToString() == "changeStatus")
                {
                    GlobalVariable.edit_shift = order_number;
                    callChangeStatus();
                }
                if (e.ClickedItem.Name.ToString() == "waybill")
                {
                    Cursor = Cursors.WaitCursor;
                    printManyWayBill();
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            cmd.CommandText = "UPDATE action SET timeCreateDoc = CURRENT_TIMESTAMP WHERE action.shiftcode IN (" + string.Join(",", GlobalVariable.edit_shift_waybill) + ")";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    callWaybill();
                    Cursor = Cursors.Default;
                }
                if (e.ClickedItem.Name.ToString() == "loading")
                {
                    Cursor = Cursors.WaitCursor;
                    printManyWayBill();
                    callLoading();
                    Cursor = Cursors.Default;
                }
                if (e.ClickedItem.Name.ToString() == "unPlanned")
                {
                    Cursor = Cursors.WaitCursor;
                    var result = MessageBox.Show("Распланировать выделенное?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;
                                int countOrd = gridShift.SelectedRows.Count;
                                object item;
                                for (int i = 0; i < countOrd; i++)
                                {
                                    item = gridShift.SelectedRows[i].Cells["shiftcode"].Value;
                                    cmd.CommandText = "DELETE FROM action WHERE shiftcode = '" + item + "'";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        update_shift();
                        update_orders("");
                        update_action();
                        Cursor = Cursors.Default;
                    }
                }
            }
            catch { }
        }

        private void printManyWayBill()
        {
            int countSelectString = gridShift.SelectedRows.Count;
            GlobalVariable.edit_shift_waybill = new int[countSelectString];
            for (int i = 0; i < countSelectString; i++)
            {
                GlobalVariable.edit_shift_waybill[i] = int.Parse(gridShift.SelectedRows[i].Cells["shiftcode"].Value.ToString());
            }
        }

        private void cb_notPlanned_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_orders("");
        }

        private void txt_searchShift_TextChanged(object sender, EventArgs e)
        {
           update_shift();
        }

        private void cb_ShiftNotPlanned_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_shift();
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveColumns();
            var result = MessageBox.Show("Закрыть программу?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void saveColumns()
        {
            var resultCol = MessageBox.Show("Сохранить порядок столбцов?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultCol == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;

                int countGridOrderColumn;
                countGridOrderColumn = gridOrder.ColumnCount;

                int countGridShiftColumn;
                countGridShiftColumn = gridShift.ColumnCount;

                int countGridActionColumn;
                countGridActionColumn = gridAction.ColumnCount;

                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        for (int i = 0; i < countGridOrderColumn; i++)
                        {
                            cmd.CommandText = "UPDATE usercolumn SET columnindex = '" + gridOrder.Columns[i].DisplayIndex + "', columnWidth = '" + gridOrder.Columns[i].Width + "' WHERE tableName = '" + gridOrder.Columns[i].Name + "' and userName = '" + GlobalVariable.userName + "' ";
                            cmd.ExecuteNonQuery();
                        }

                        for (int i = 0; i < countGridShiftColumn; i++)
                        {
                            cmd.CommandText = "UPDATE usercolumn SET columnindex = '" + gridShift.Columns[i].DisplayIndex + "', columnWidth = '" + gridShift.Columns[i].Width + "' WHERE tableName = '" + gridShift.Columns[i].Name + "' and userName = '" + GlobalVariable.userName + "' ";
                            cmd.ExecuteNonQuery();
                        }

                        for (int i = 0; i < countGridActionColumn; i++)
                        {
                            cmd.CommandText = "UPDATE usercolumn SET columnindex = '" + gridAction.Columns[i].DisplayIndex + "', columnWidth = '" + gridAction.Columns[i].Width + "' WHERE tableName = '" + gridAction.Columns[i].Name + "' and userName = '" + GlobalVariable.userName + "' ";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                Cursor = Cursors.Default;
            }
        }

        private void general_date_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalVariable.sh = 0;
                DateTime gen_date = Convert.ToDateTime(general_date.Text);
                GlobalVariable.general_date = gen_date.ToString("yyyy-MM-dd");
                update_orders("");
                update_shift();
                update_action();
                frigo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n general_date_ValueChanged" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cb_frigo_date_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        // Включен ли фриго?
                        cmd.CommandText = "SELECT Count(*) FROM Ref WHERE createDate = '" + GlobalVariable.general_date + "' and frigo = '1'";
                        dr = cmd.ExecuteReader();
                        string refrigerator = "";
                        while (dr.Read())
                        {
                            refrigerator = dr[0].ToString();
                        }
                        dr.Close();

                        if (Convert.ToInt16(refrigerator) == 0)
                        {
                            if (cb_frigo_date.Checked == true)
                            {
                                cmd.CommandText = "INSERT into Ref (createDate, frigo) VALUES('" + GlobalVariable.general_date + "', '1');";
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (cb_frigo_date.Checked == false)
                        {
                            cmd.CommandText = "DELETE FROM Ref WHERE createDate = '" + GlobalVariable.general_date + "' and frigo = '1';";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n cb_frigo_date_CheckedChanged" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frigo()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        // Включен ли фриго?
                        cmd.CommandText = "SELECT Count(*) FROM Ref WHERE createDate = '" + GlobalVariable.general_date + "' and frigo = 1";
                        dr = cmd.ExecuteReader();
                        string refrigerator = "";
                        while (dr.Read())
                        {
                            refrigerator = dr[0].ToString();
                        }
                        if (Convert.ToInt16(refrigerator) == 1)
                        {
                            cb_frigo_date.Checked = true;
                        }
                        else
                        {
                            cb_frigo_date.Checked = false;
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n frigo() " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void edit_matrix_Click(object sender, EventArgs e)
        {
            callMatrix();
        }

        private void newUser_Click(object sender, EventArgs e)
        {
            callnewUser();
        }

        private void addAddress_Click(object sender, EventArgs e)
        {
            callnewAddress();
        }

        //Импорт смен CSV
        private void btnimportShift_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog(); ofd.DefaultExt = ".csv"; ofd.Filter = "Comma Separated (*.csv)|*.csv";
                var result = ofd.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    Cursor = Cursors.WaitCursor;
                    string FileName = ofd.FileName;
                    DataTable imported_data = GetDataFromFile(FileName);

                    if (imported_data == null)
                        return;

                    SaveImportDataToDatabase(imported_data);

                    MessageBox.Show("Импорт смен завершен!"); FileName = string.Empty;

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n btnimportShift_Click" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetDataFromFile(string FileName)
        {
            DataTable importedData = new DataTable();

            try
            {
                using (StreamReader sr = new StreamReader(FileName, System.Text.Encoding.Default))
                {
                    string header = sr.ReadLine();

                    if (string.IsNullOrEmpty(header))
                    {
                        MessageBox.Show("no file data");
                        return null;
                    }

                    string[] headerColumns = header.Split(';');

                    foreach (string headerColumn in headerColumns)
                    {
                        importedData.Columns.Add(headerColumn);
                    }

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (string.IsNullOrEmpty(line))
                            continue;

                        string[] fields = line.Split(';');


                        DataRow importedRow = importedData.NewRow();

                        for (int i = 0; i < fields.Count(); i++)
                        {
                            importedRow[i] = fields[i];
                        }

                        importedData.Rows.Add(importedRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n GetDataFromFile" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return importedData;
        }
        private void SaveImportDataToDatabase(DataTable imported_data)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        foreach (DataRow importRow in imported_data.Rows)
                        {
                            cmd.CommandText = "INSERT INTO shift (truck, capacityKg, capacityPal, datework, trailer, driver, subcontractor, comment, tripNumber, status, homeAddress) " +
                                                            "VALUES ('" + importRow["truck"] + "', '" + importRow["capacityKg"] + "', '" + importRow["capacityPal"] + "', '"+ Convert.ToDateTime(importRow["datework"]).ToString("yyyy-MM-dd") + "', '" + importRow["trailer"] + "', N'" + importRow["driver"] + "', '" + importRow["subcontractor"] + "', N'" + importRow["comment"] + "', '1', '1', '" + importRow["homeAddress"] + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n SaveImportDataToDatabase" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            update_shift();
        }

        /******************/
        //Импорт Заказов CSV
        private void btnimportOrder_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog(); ofd.DefaultExt = ".csv"; ofd.Filter = "Comma Separated (*.csv)|*.csv";
                var result = ofd.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    Cursor = Cursors.WaitCursor;
                    string FileName = ofd.FileName;
                    DataTable imported_data = GetDataOrderFromFile(FileName);

                    if (imported_data == null)
                        return;

                    SaveImportDataOrderToDatabase(imported_data);

                    MessageBox.Show("Импорт заказов завершен!"); FileName = string.Empty;

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n btnimportOrder_Click" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetDataOrderFromFile(string FileName)
        {
            DataTable importedData = new DataTable();

            try
            {
                using (StreamReader sr = new StreamReader(FileName, System.Text.Encoding.Default))
                {
                    string header = sr.ReadLine();

                    if (string.IsNullOrEmpty(header))
                    {
                        MessageBox.Show("no file data");
                        return null;
                    }

                    string[] headerColumns = header.Split(';');

                    foreach (string headerColumn in headerColumns)
                    {
                        importedData.Columns.Add(headerColumn);
                    }

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (string.IsNullOrEmpty(line))
                            continue;

                        string[] fields = line.Split(';');


                        DataRow importedRow = importedData.NewRow();

                        for (int i = 0; i < fields.Count(); i++)
                        {
                            importedRow[i] = fields[i];
                        }

                        importedData.Rows.Add(importedRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n GetDataOrderFromFile" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return importedData;
        }
        private void SaveImportDataOrderToDatabase(DataTable imported_data)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        foreach (DataRow importRow in imported_data.Rows)
                        {
                            cmd.CommandText = "INSERT INTO orders (address, product, kg, pal, comment, datework, orderStatus) " +
                                                            "VALUES ('" + importRow["address"] + "', '" + importRow["product"] + "', '" + Convert.ToInt32(importRow["kg"]) + "', '" + importRow["pal"] + "', N'" + importRow["comment"] + "', '" + importRow["datework"] + "', '0')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                update_orders("");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n SaveImportDataOrderToDatabase" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /******************/
        //Импорт расстояний CSV
        private void btnimportMatrix_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog(); ofd.DefaultExt = ".csv"; ofd.Filter = "Comma Separated (*.csv)|*.csv";
                var result = ofd.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    Cursor = Cursors.WaitCursor;
                    string FileName = ofd.FileName;
                    DataTable imported_data = GetDataMatrixFromFile(FileName);

                    if (imported_data == null)
                        return;

                    SaveImportDataMatrixToDatabase(imported_data);

                    MessageBox.Show("Импорт расстояний завершен!"); FileName = string.Empty;

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n btnimportMatrix_Click" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetDataMatrixFromFile(string FileName)
        {
            DataTable importedData = new DataTable();

            try
            {
                using (StreamReader sr = new StreamReader(FileName, System.Text.Encoding.Default))
                {
                    string header = sr.ReadLine();

                    if (string.IsNullOrEmpty(header))
                    {
                        MessageBox.Show("no file data");
                        return null;
                    }

                    string[] headerColumns = header.Split(';');

                    foreach (string headerColumn in headerColumns)
                    {
                        importedData.Columns.Add(headerColumn);
                    }

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (string.IsNullOrEmpty(line))
                            continue;

                        string[] fields = line.Split(';');


                        DataRow importedRow = importedData.NewRow();

                        for (int i = 0; i < fields.Count(); i++)
                        {
                            importedRow[i] = fields[i];
                        }

                        importedData.Rows.Add(importedRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n GetDataMatrixFromFile" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return importedData;
        }
        private void SaveImportDataMatrixToDatabase(DataTable imported_data)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        foreach (DataRow importRow in imported_data.Rows)
                        {
                            cmd.CommandText = "SELECT count(*) FROM distance WHERE addressFromCode = " + importRow["addressFromCode"] + " and addressToCode = " + importRow["addressToCode"] + "";
                            dr = cmd.ExecuteReader();
                            int countRows = 0;
                            while (dr.Read())
                            {
                                countRows = Convert.ToInt16(dr[0].ToString());
                            }
                            dr.Close();

                            if (countRows == 0)
                            {
                                cmd.CommandText = "INSERT INTO distance (addressFromCode, addressToCode, ditance) " +
                                                                "VALUES ('" + importRow["addressFromCode"] + "', '" + importRow["addressToCode"] + "', '" + Convert.ToInt32(importRow["ditance"]) + "')";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd.CommandText = "UPDATE distance SET ditance = '" + Convert.ToInt32(importRow["ditance"]) + "' WHERE addressFromCode = " + importRow["addressFromCode"] + " and addressToCode = " + importRow["addressToCode"] + "";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n SaveImportDataMatrixToDatabase" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /******************/
        //Импорт Адресов CSV
        private void btnimportAddress_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog(); ofd.DefaultExt = ".csv"; ofd.Filter = "Comma Separated (*.csv)|*.csv";
                var result = ofd.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    Cursor = Cursors.WaitCursor;
                    string FileName = ofd.FileName;
                    DataTable imported_data = GetDataAddressFromFile(FileName);

                    if (imported_data == null)
                        return;

                    SaveImportDataAddressToDatabase(imported_data);

                    MessageBox.Show("Импорт адресов завершен!"); FileName = string.Empty;

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n btnimportAddress_Click" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetDataAddressFromFile(string FileName)
        {
            DataTable importedData = new DataTable();

            try
            {
                using (StreamReader sr = new StreamReader(FileName, System.Text.Encoding.Default))
                {
                    string header = sr.ReadLine();

                    if (string.IsNullOrEmpty(header))
                    {
                        MessageBox.Show("no file data");
                        return null;
                    }

                    string[] headerColumns = header.Split(';');

                    foreach (string headerColumn in headerColumns)
                    {
                        importedData.Columns.Add(headerColumn);
                    }

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (string.IsNullOrEmpty(line))
                            continue;

                        string[] fields = line.Split(';');


                        DataRow importedRow = importedData.NewRow();

                        for (int i = 0; i < fields.Count(); i++)
                        {
                            importedRow[i] = fields[i];
                        }

                        importedData.Rows.Add(importedRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n GetDataAddressFromFile" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return importedData;
        }
        private void SaveImportDataAddressToDatabase(DataTable imported_data)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        foreach (DataRow importRow in imported_data.Rows)
                        {
                            cmd.CommandText = "SELECT count(*) FROM address WHERE addresscode = " + importRow["addressCode"] + "";
                            dr = cmd.ExecuteReader();
                            int countRows = 0;
                            while (dr.Read())
                            {
                                countRows = Convert.ToInt16(dr[0].ToString());
                            }
                            dr.Close();

                            if (countRows == 0)
                            {
                                cmd.CommandText = "INSERT INTO address (addressCode, addressName, cityName, id_addressKind, streetName, doorNumber, phone, email, comment, stopFromDay, stopTillDay) " +
                                                            "VALUES ('" + importRow["addressCode"] + "', N'" + importRow["addressName"] + "', N'" + importRow["cityName"] + "', '" + importRow["id_addressKind"] + "', N'" + importRow["streetName"] + "', N'" + importRow["doorNumber"] + "', '" + importRow["phone"] + "', '" + importRow["email"] + "', N'" + importRow["comment"] + "', N'" + importRow["stopFromDay"] + "', N'" + importRow["stopTillDay"] + "')";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd.CommandText = "UPDATE address SET addressName =  N'" + importRow["addressName"] + "', cityName = N'" + importRow["cityName"] + "', id_addressKind = '" + importRow["id_addressKind"] + "', streetName = N'" + importRow["streetName"] + "', doorNumber = N'" + importRow["doorNumber"] + "', phone = '" + importRow["phone"] + "', email = '" + importRow["email"] + "', comment = N'" + importRow["comment"] + "', stopFromDay = N'" + importRow["stopFromDay"] + "', stopTillDay = N'" + importRow["stopTillDay"] + "' WHERE addressCode = '" + importRow["addressCode"] + "'";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error\n SaveImportDataAddressToDatabase" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridShift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                try
                {
                    int summ = 0;
                    int summPal = 0;
                    int count = gridShift.SelectedRows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (Convert.ToString(gridShift.SelectedRows[i].Cells["uploadKg"].Value).Length > 1)
                        {
                            summ += Convert.ToInt32(gridShift.SelectedRows[i].Cells["uploadKg"].Value);
                            summPal += Convert.ToInt32(gridShift.SelectedRows[i].Cells["uploadPal"].Value);
                        }
                    }
                    label6.Visible = true;
                    label6.Text = "Смены: КГ: " + summ.ToString() + " Пал: " + summPal.ToString() + " Строк: " + count.ToString();
                }
                catch { }
            }
        }

        private void gridShift_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == false)
            {
                label6.Text = "";
            }
            if (e.KeyValue == 38)
            {
                timer.Start();
            }
            else if (e.KeyValue == 40)
            {
                timer.Start();
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            GlobalVariable.sh = Convert.ToInt32(gridShift.SelectedRows[0].Cells["shiftcode"].Value);
            GlobalVariable.homeAddress = Convert.ToInt32(gridShift.SelectedRows[0].Cells["homeAddress"].Value);
            GlobalVariable.index_shift = gridShift.CurrentRow.Index;
            update_action();
            timer.Stop();
        }

        private void cb_order_fact_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_orders("");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Версия ПО: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridOrder_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridOrder.RowCount; i++)
            {
                string count_symbol = Convert.ToString(gridOrder.Rows[i].Cells["truckDataGridViewTextBoxColumn1"].Value);
                if (count_symbol.Length > 0)
                    gridOrder.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;

                if (gridOrder.Rows[i].Cells["orderStatus"].Value.ToString() == "1")
                {
                    gridOrder.Rows[i].Cells["kg"].Style.BackColor = Color.Orange;
                    gridOrder.Rows[i].Cells["pal"].Style.BackColor = Color.Orange;
                }
                if (gridOrder.Rows[i].Cells["orderStatus"].Value.ToString() == "2")
                {
                    gridOrder.Rows[i].Cells["kg"].Style.BackColor = Color.Red;
                    gridOrder.Rows[i].Cells["pal"].Style.BackColor = Color.Red;
                    gridOrder.Rows[i].Cells["kg"].Style.ForeColor = Color.White;
                    gridOrder.Rows[i].Cells["pal"].Style.ForeColor = Color.White;
                }
            }

            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(gridOrder);

            if (String.IsNullOrEmpty(filterStatus))
            {
                deleteFilterGridOrder.Visible = false;
                filterStatusOrder.Visible = false;
            }
            else
            {
                deleteFilterGridOrder.Visible = true;
                filterStatusOrder.Visible = true;
                filterStatusOrder.Text = filterStatus;
            }
        }

        private void gridShift_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridShift.RowCount; i++)
            {
                if (Convert.ToInt32(gridShift.Rows[i].Cells["planned"].Value) == 1)
                    gridShift.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;

                if ((int.Parse(gridShift.Rows[i].Cells["ErrUpload"].Value.ToString()) + int.Parse(gridShift.Rows[i].Cells["ErrUploadPal"].Value.ToString())) > 0)
                {
                    gridShift.Rows[i].DefaultCellStyle.BackColor = Color.LightCoral;
                    gridShift.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
            }

            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(gridShift);

            if (String.IsNullOrEmpty(filterStatus))
            {
                deleteFilterGridShift.Visible = false;
                filterStatusLabel.Visible = false;
            }
            else
            {
                deleteFilterGridShift.Visible = true;
                filterStatusLabel.Visible = true;
                filterStatusLabel.Text = filterStatus;
            }
        }

        private void gridShift_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gridShift_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Point dscreen = new Point(e.X, e.Y);
                Point dclient = gridShift.PointToClient(dscreen);
                DataGridView.HitTestInfo hitTest = gridShift.HitTest(dclient.X, dclient.Y);

                object shiftCode_insert = gridShift.Rows[hitTest.RowIndex].Cells["shiftcode"].Value;
                object truckNumber = gridShift.Rows[hitTest.RowIndex].Cells["truck"].Value;

                var dialog = MessageBox.Show("Спланировать в " + truckNumber + "?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;
                    int countOrd = gridOrder.SelectedRows.Count;
                    object item;
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            for (int i = 0; i < countOrd; i++)
                            {
                                item = gridOrder.SelectedRows[i].Cells["order_number"].Value;

                                //Спланирован ли заказ?
                                // Спланирован ли рейс?
                                cmd.CommandText = "SELECT Count(*) FROM action WHERE action.id_order = '" + item + "'";
                                dr = cmd.ExecuteReader();
                                string count_ord = "";
                                while (dr.Read())
                                {
                                    count_ord = dr[0].ToString();
                                }
                                dr.Close();

                                if (count_ord == "0" || count_ord == null)
                                {
                                    if (item != null)
                                    {
                                        try
                                        {
                                            //cn.Open();
                                            // Спланирован ли рейс?
                                            cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + shiftCode_insert + "'";
                                            dr = cmd.ExecuteReader();
                                            string count = "";
                                            while (dr.Read())
                                            {
                                                count = dr[0].ToString();
                                            }
                                            dr.Close();

                                            //Номер авто
                                            cmd.CommandText = "SELECT top 1 truck FROM shift WHERE shift.shiftcode = '" + shiftCode_insert + "'";
                                            dr = cmd.ExecuteReader();
                                            string truck = "";
                                            while (dr.Read())
                                            {
                                                truck = dr[0].ToString();
                                            }
                                            dr.Close();

                                            // Получаем куда ехать
                                            cmd.CommandText = "SELECT top 1 address FROM orders WHERE order_number = '" + item + "'";
                                            dr = cmd.ExecuteReader();
                                            string finish = "";
                                            while (dr.Read())
                                            {
                                                finish = dr[0].ToString();
                                            }
                                            dr.Close();
                                            // если рейса еще не было значит старт с РЦ
                                            if (count == null || count == "0")
                                            {
                                                cmd.CommandText = "INSERT into action (shiftcode,start,start_finish, actionid) VALUES('" + shiftCode_insert + "','" + GlobalVariable.depot + "','1', '0')";
                                                cmd.ExecuteNonQuery();
                                                cmd.CommandText = "INSERT into action (id_order,shiftcode,start,start_finish,finish, actionid) VALUES('" + item + "','" + shiftCode_insert + "','" + finish + "','2','" + GlobalVariable.depot + "', '0')";
                                                cmd.ExecuteNonQuery();
                                                cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + shiftCode_insert + "' and start <> '" + GlobalVariable.depot + "') T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + shiftCode_insert + "'";
                                                cmd.ExecuteNonQuery();

                                                // для маршрутного листа
                                                cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + shiftCode_insert + "' and (actionid = '0' or actionid is null)) T2 ON T1.id = T2.id where shiftcode = '" + shiftCode_insert + "' and (actionid = '0' or actionid is null)";
                                                cmd.ExecuteNonQuery();

                                            }
                                            //если рейс есть
                                            else
                                            {
                                                int sort = (Convert.ToInt16(count) + 1);
                                                dr.Close();
                                                cmd.CommandText = "INSERT into action (id_order,shiftcode,start,start_finish,finish) VALUES('" + item + "','" + shiftCode_insert + "','" + finish + "','" + sort + "','" + GlobalVariable.depot + "')";
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error\n gridShift_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Этот " + item + " заказ уже спланирован", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    dr.Close();
                                }
                            }

                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + shiftCode_insert + "' and start <> '" + GlobalVariable.depot + "') T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + shiftCode_insert + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + shiftCode_insert + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + shiftCode_insert + "'";
                            cmd.ExecuteNonQuery();

                            // для маршрутного листа
                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + shiftCode_insert + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + shiftCode_insert + "' and actionid = '0'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //update таблиц после цикла
                    try
                    {
                        update_orders("");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n update_orders_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    try
                    {
                        update_action();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n update_action_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    try
                    {
                        update_shift();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n update_shift_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // 
                    Cursor = Cursors.Default;
                }
            }
            catch { }
        }

        private void control_Product_Click(object sender, EventArgs e)
        {
            callProduct();
        }

        private void btn_Instruction_Click(object sender, EventArgs e)
        {
            //help();
        }

        private void help()
        {
            //string tempPath = System.IO.Path.GetTempPath() + "MSU.chm";
            //File.WriteAllBytes(tempPath, MSU.Properties.Resources.MSU);
            //System.Diagnostics.Process.Start(tempPath).WaitForExit();
            //File.Delete(tempPath);
        }

        private void main_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
                help();
        }

        private void interface_orders_Click(object sender, EventArgs e)
        {
            GlobalVariable.tb_interface = 1;
            callInterface();
        }

        private void gridOrder_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gridOrder.ClearSelection();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(gridShift);
        }

        private void deleteFilterGridOrder_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(gridOrder);
        }

        private void deleteFilterGridOrder_MouseHover(object sender, EventArgs e)
        {
            t.SetToolTip(deleteFilterGridOrder, "Снять все фильтры с таблицы заказов");
        }

        private void deleteFilterGridShift_MouseHover(object sender, EventArgs e)
        {
            t.SetToolTip(deleteFilterGridShift, "Снять все фильтры с таблицы рейсов");
        }

        private void отчетЛогистаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callLogist();
        }

        private void создатьРейсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callAddShift();
        }

        private void создатьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callAddOrder();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gridAction_MouseEnter(object sender, EventArgs e)
        {
            actionDragDrop = 1;

            if (gridAction.RowCount > 10)
            {
                gridOrder.Visible = false;

                label_search.Visible = false;

                txt_search_order.Visible = false;

                label_columnName.Visible = false;

                cb_order_fact.Visible = false;

                cb_notPlanned.Visible = false;

                int row = (gridAction.RowCount - 10);

                int rowHeight = gridAction.Rows[0].Height;

                gridAction.Size = new Size(gridAction.Size.Width, gridAction.Size.Height + (row * rowHeight));
            }
        }

        private void gridAction_MouseLeave(object sender, EventArgs e)
        {
            autoMaxiActionGrid();
        }

        private void gridAction_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.Control)
            {
                var img = new Bitmap(MSU.Properties.Resources.drag2);
                Icon icon = Icon.FromHandle(img.GetHicon());
                Cursor cur = new Cursor(icon.Handle);
                Cursor.Current = cur;
            }
        }

        private void autoMaxiActionGrid()
        {
            actionDragDrop = 0;

            if (gridOrder.Visible == false)
            {
                gridOrder.Visible = true;

                label_search.Visible = true;

                txt_search_order.Visible = true;

                label_columnName.Visible = true;

                cb_order_fact.Visible = true;

                cb_notPlanned.Visible = true;

                gridAction.Size = new Size(gridAction.Size.Width, gridActionHeight);
            }
        }

        private void gridAction_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.Control)
            {
                try
                {
                    int count = gridAction.SelectedRows.Count;
                    Cursor = Cursors.Default;
                    if (count <= 10)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (gridAction.SelectedRows[i].Cells["startDataGridViewTextBoxColumn"].Value.ToString() != GlobalVariable.depot.ToString())
                                gridAction.DoDragDrop(gridAction.SelectedRows[i].Cells["id"].Value, DragDropEffects.Move);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void main_MouseEnter(object sender, EventArgs e)
        {
            autoMaxiActionGrid();
        }

        private void gridOrder_MouseUp(object sender, MouseEventArgs e)
        {
            if (gridOrder.SelectedRows.Count > 1)
                updateAllKGPall();

            if (e.Button == MouseButtons.Left)
            {
                if (Control.ModifierKeys == Keys.Control)
                    gridOrder.MultiSelect = true;
                else if (Control.ModifierKeys == Keys.Shift)
                    gridOrder.MultiSelect = true;
                else
                    gridOrder.MultiSelect = false;
            }
        }

        private void управлениеПодрядчикамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callSubcontractor();
        }

        private void gridShift_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                object shiftcode = gridShift.SelectedRows[0].Cells["shiftcode"].Value;
                GlobalVariable.edit_shift = shiftcode.ToString();
                callAddShift();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка двойного клика таблица gridShift" + ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
        }

        private void отчетТаблицаРейсовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callshiftGridReport();
        }

        private void сохранитьСтолбцыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveColumns();
        }

        private void gridOrder_Sorted(object sender, EventArgs e)
        {
            string columnName = gridOrder.SortedColumn.Name.ToString();
            string sortType = "";

            if (gridOrder.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                sortType = "ASC";
            else if (gridOrder.SortOrder == System.Windows.Forms.SortOrder.Descending)
                sortType = "DESC";

            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;

                    cmd.CommandText = "select db_columnName FROM fields where DatagridColumnName = '" + columnName + "'";
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        orderByOrderSQL = "order by " + dr[0].ToString() + " " + sortType + ", order_number " + sortType + "";
                        orderByOrder = "" + dr[0].ToString() + " " + sortType + ", order_number " + sortType + "";
                        bs.Sort = orderByOrder;
                    }
                    dr.Close();

                }
            }
        }

        private void gridShift_Sorted(object sender, EventArgs e)
        {
            string columnName = gridShift.SortedColumn.Name.ToString();
            string sortType = "";

            if (gridShift.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                sortType = "ASC";
            else if (gridShift.SortOrder == System.Windows.Forms.SortOrder.Descending)
                sortType = "DESC";

            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;

                    cmd.CommandText = "select db_columnName FROM fields where DatagridColumnName = '" + columnName + "'";
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        orderByShiftSQL = "order by " + dr[0].ToString() + " " + sortType + ", shiftCode " + sortType + "";
                        orderByShift = "" + dr[0].ToString() + " " + sortType + ", shiftCode " + sortType + "";
                        bsshift.Sort = orderByShift;
                    }
                    dr.Close();

                }
            }
        }

        private void таблицаЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callOrderGreed();
        }

        private void оперативныйОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calloperationalReport();
        }

        private void reportAboutDepartedTimeOrder_Click(object sender, EventArgs e)
        {
            callCheckOutTimeWarehouseReport();
        }
    }
}
