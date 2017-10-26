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
    public partial class rePlanManyShift : Form
    {
        SqlDataReader dr;

        public rePlanManyShift()
        {
            InitializeComponent();
        }

        private void rePlanManyShift_Load(object sender, EventArgs e)
        {
            gb1();
            updateTableOne();
            updateTableTwo();
        }
        private void gb1()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT truck, (select truck from shift where shiftcode = '" + GlobalVariable.replanTwo + "') as truck2 FROM shift WHERE shiftcode = '" + GlobalVariable.replanOne + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        groupBox1.Text = dr[0].ToString();
                        groupBox2.Text = dr[1].ToString();
                    }
                    dr.Close();
                }
            }
        }
        private void updateTableOne()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.CommandText = "select count(*), shift.freight from action join shift on action.shiftcode = shift.shiftcode where action.shiftcode = '" + GlobalVariable.replanOne + "' and action.actionid = '0' Group by shift.freight";
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
                            top = "UNION select top 1 (select top 1 start from action where action.shiftcode = ac.shiftcode order by action.start_finish), '', '','', '','','', (select top 1 start_finish+1 from action where shiftcode = ac.shiftcode order by cast(start_finish as int) desc) from action ac where shiftcode = '" + GlobalVariable.replanOne + "'";
                        dr.Close();

                        cmd.CommandText = "select start, action.id_order as id_order, address.addressName,cityName,orders.kg,orders.pal,product.productName ,action.start_finish from action left join address on action.start = address.addressCode left join orders on action.id_order = orders.order_number left join product on orders.product = product.productCode where shiftcode = '" + GlobalVariable.replanOne + "' " + top + " order by cast(action.start_finish as int);";
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(cmd.ExecuteReader());
                            gridRePlanOne.DataSource = dt;
                        }
                    }
                }
                updateLabelOne();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n update_action" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void updateLabelOne()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT truck, (select sum(cast(kg as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode)  as uploadKg,(select sum(cast(pal as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode)  as uploadPal, capacityKg, capacityPal FROM shift WHERE shiftcode = '" + GlobalVariable.replanOne + "'";
                    dr = cmd.ExecuteReader();
                    string count = "";
                    string upkg = "0";
                    string upPal = "0";
                    string capacityKg = "0";
                    string capacityPal = "0";
                    while (dr.Read())
                    {
                        count = dr[0].ToString();
                        upkg = dr[1].ToString();
                        upPal = dr[2].ToString();
                        capacityKg = dr[3].ToString();
                        capacityPal = dr[4].ToString();
                    }
                    dr.Close();

                    cmd.CommandText = "EXEC GET_KM " + GlobalVariable.replanOne + "";
                    dr = cmd.ExecuteReader();
                    string kmOne = "";
                    while (dr.Read())
                    {
                        kmOne = dr[0].ToString();
                    }
                    dr.Close();
                    uploadOne.Text = count + " КГ - " + capacityKg + "\\" + upkg + " | Паллет - " + capacityPal + "\\" + upPal + " | КМ - " + kmOne;
                }
            }
        }

        private void updateTableTwo()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.CommandText = "select count(*), shift.freight from action join shift on action.shiftcode = shift.shiftcode where action.shiftcode = '" + GlobalVariable.replanTwo + "' and action.actionid = '0' Group by shift.freight";
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
                            top = "UNION select top 1 (select top 1 start from action where action.shiftcode = ac.shiftcode order by action.start_finish), '', '','', '','','', (select top 1 start_finish+1 from action where shiftcode = ac.shiftcode order by cast(start_finish as int) desc) from action ac where shiftcode = '" + GlobalVariable.replanTwo + "'";
                        dr.Close();

                        cmd.CommandText = "select start, action.id_order as id_order, address.addressName,cityName,orders.kg,orders.pal,product.productName ,action.start_finish from action left join address on action.start = address.addressCode left join orders on action.id_order = orders.order_number left join product on orders.product = product.productCode where shiftcode = '" + GlobalVariable.replanTwo + "' " + top + " order by cast(action.start_finish as int);";
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(cmd.ExecuteReader());
                            gridRePlanTwo.DataSource = dt;
                        }
                    }
                }
                updateLabelTwo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n update_action" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void updateLabelTwo()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT truck, (select sum(cast(kg as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode)  as uploadKg,(select sum(cast(pal as int)) from orders join action on orders.order_number = action.id_order where action.shiftcode = shift.shiftcode)  as uploadPal, capacityKg, capacityPal FROM shift WHERE shiftcode = '" + GlobalVariable.replanTwo + "'";
                    dr = cmd.ExecuteReader();
                    string count = "";
                    string upkg = "0";
                    string upPal = "0";
                    string capacityKg = "0";
                    string capacityPal = "0";
                    while (dr.Read())
                    {
                        count = dr[0].ToString();
                        upkg = dr[1].ToString();
                        upPal = dr[2].ToString();
                        capacityKg = dr[3].ToString();
                        capacityPal = dr[4].ToString();
                    }
                    dr.Close();

                    cmd.CommandText = "EXEC GET_KM " + GlobalVariable.replanTwo + "";
                    dr = cmd.ExecuteReader();
                    string kmTwo = "";
                    while (dr.Read())
                    {
                        kmTwo = dr[0].ToString();
                    }
                    dr.Close();
                    uploadTwo.Text = count + " КГ - " + capacityKg + "\\" + upkg + " | Паллет - " + capacityPal + "\\" + upPal + " | КМ - " + kmTwo;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Все заказы автомобиля "+ groupBox1.Text +" будут перемещены в автомобиль "+ groupBox2.Text +", продолжить?", "Перемещение заказов", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT count(*) FROM action WHERE shiftcode = '" + GlobalVariable.replanTwo + "'";
                        dr = cmd.ExecuteReader();
                        int check = 0;
                        if (dr.Read())
                            check = Convert.ToUInt16(dr[0].ToString());
                        dr.Close();

                        if (check == 0)
                        {
                            cmd.CommandText = "UPDATE action SET shiftcode = '" + GlobalVariable.replanTwo + "' WHERE shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "UPDATE action SET shiftcode = '" + GlobalVariable.replanTwo + "' WHERE shiftcode = '" + GlobalVariable.replanOne + "' and id_order is not null";
                            cmd.ExecuteNonQuery();

                            /* Шаг 1 Обновляем порядковый номер*/
                            cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();
                            /* Шаг 2 Обновляем финишный адрес*/
                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanTwo + "'  UNION select top 1 start, start_finish=(SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanTwo + "') from action WHERE shiftcode = '" + GlobalVariable.replanTwo + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();
                            /* Шаг 3 обновляем id действий*/
                            cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();
                            /* Шаг 4 Сортируем для маршрутного листа*/
                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "' and (actionid = '0' or actionid is null)) T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "' and (actionid = '0' or actionid is null)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                updateTableOne();
                updateTableTwo();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Все заказы автомобиля " + groupBox2.Text + " будут перемещены в автомобиль " + groupBox1.Text + ", продолжить?", "Перемещение заказов", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT count(*) FROM action WHERE shiftcode = '" + GlobalVariable.replanOne + "'";
                        dr = cmd.ExecuteReader();
                        int check = 0;
                        if (dr.Read())
                            check = Convert.ToUInt16(dr[0].ToString());
                        dr.Close();

                        if (check == 0)
                        {
                            cmd.CommandText = "UPDATE action SET shiftcode = '" + GlobalVariable.replanOne + "' WHERE shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "UPDATE action SET shiftcode = '" + GlobalVariable.replanOne + "' WHERE shiftcode = '" + GlobalVariable.replanTwo + "' and id_order is not null";
                            cmd.ExecuteNonQuery();

                            /* Шаг 1 Обновляем порядковый номер*/
                            cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();
                            /* Шаг 2 Обновляем финишный адрес*/
                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanOne + "'  UNION select top 1 start, start_finish=(SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanOne + "') from action WHERE shiftcode = '" + GlobalVariable.replanOne + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();
                            /* Шаг 3 обновляем id действий*/
                            cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();
                            /* Шаг 4 Сортируем для маршрутного листа*/
                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "' and (actionid = '0' or actionid is null)) T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "' and (actionid = '0' or actionid is null)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                updateTableOne();
                updateTableTwo();
            }
        }

        private void upOne(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanOne + "'";
                        dr = cmd.ExecuteReader();
                        string count = "";
                        while (dr.Read())
                        {
                            count = dr[0].ToString();
                        }
                        dr.Close();

                        CurrencyManager cmgr = (CurrencyManager)this.gridRePlanOne.BindingContext[this.gridRePlanOne.DataSource];
                        DataRow row = ((DataRowView)cmgr.Current).Row;
                        string up = row["start_finish"].ToString();
                        int row_number = Convert.ToInt16(up) - 2;
                        string shop = row["start"].ToString();
                        if (shop != Convert.ToString(GlobalVariable.depot))
                        {
                            int next = Convert.ToInt32(up) - 1;
  
                            // Меняй сорт той строке на которую меняем
                            cmd.CommandText = "UPDATE action SET start_finish='" + up + "' WHERE  shiftcode ='" + GlobalVariable.replanOne + "' and start_finish =  '" + next + "'";
                            cmd.ExecuteNonQuery();
                            // Меняем сорт той которую выделили 
                            cmd.CommandText = "UPDATE action SET start_finish='" + next + "' WHERE  shiftcode ='" + GlobalVariable.replanOne + "' and start = '" + shop + "' and start_finish =  '" + up + "'";
                            cmd.ExecuteNonQuery();
                            // Обновляем порядковый номер выгрузки
                            cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();

                            // Обновляем финишный адрес доставки
                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanOne + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(count) + "' from action WHERE shiftcode = '" + GlobalVariable.replanOne + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();

                            // для маршрутного листа
                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "' and actionid = '0'";
                            cmd.ExecuteNonQuery();

                            // Обновляем таблицу действий
                            updateTableOne();
                            // Возвращаем курсор на место
                            gridRePlanOne.CurrentCell = gridRePlanOne.Rows[row_number].Cells[0];
                        }
                        else
                        {
                            MessageBox.Show("РЦ двигать запрещено!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch { }
        }

        private void downOne(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanOne + "'";
                        dr = cmd.ExecuteReader();
                        string count = "";
                        while (dr.Read())
                        {
                            count = dr[0].ToString();
                        }
                        dr.Close();

                        CurrencyManager cmgr = (CurrencyManager)this.gridRePlanOne.BindingContext[this.gridRePlanOne.DataSource];
                        DataRow row = ((DataRowView)cmgr.Current).Row;
                        string up = row["start_finish"].ToString();
                        int row_number = Convert.ToInt16(up);
                        string shop = row["start"].ToString();
                        if (shop != Convert.ToString(GlobalVariable.depot))
                        {
                            int next = Convert.ToInt32(up) + 1;

                            // Меняй сорт той строке на которую меняем
                            cmd.CommandText = "UPDATE action SET start_finish='" + up + "' WHERE  shiftcode ='" + GlobalVariable.replanOne + "' and start_finish =  '" + next + "'";
                            cmd.ExecuteNonQuery();
                            // Меняем сорт той которую выделили 
                            cmd.CommandText = "UPDATE action SET start_finish='" + next + "' WHERE  shiftcode ='" + GlobalVariable.replanOne + "'  and start = '" + shop + "' and start_finish =  '" + up + "'";
                            cmd.ExecuteNonQuery();
                            // Обновляем порядковый номер выгрузки
                            cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();
                            // Обновляем финишный адрес доставки
                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanOne + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(count) + "' from action WHERE shiftcode = '" + GlobalVariable.replanOne + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                            cmd.ExecuteNonQuery();

                            // для маршрутного листа
                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "' and actionid = '0'";
                            cmd.ExecuteNonQuery();

                            updateTableOne();
                            gridRePlanOne.CurrentCell = gridRePlanOne.Rows[row_number].Cells[0];
                        }
                        else
                        {
                            MessageBox.Show("РЦ двигать запрещено!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch { }
        }

        private void upTwo(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                        dr = cmd.ExecuteReader();
                        string count = "";
                        while (dr.Read())
                        {
                            count = dr[0].ToString();
                        }
                        dr.Close();

                        CurrencyManager cmgr = (CurrencyManager)this.gridRePlanTwo.BindingContext[this.gridRePlanTwo.DataSource];
                        DataRow row = ((DataRowView)cmgr.Current).Row;
                        string up = row["start_finish"].ToString();
                        int row_number = Convert.ToInt16(up) - 2;
                        string shop = row["start"].ToString();
                        if (shop != Convert.ToString(GlobalVariable.depot))
                        {
                            int next = Convert.ToInt32(up) - 1;

                            // Меняй сорт той строке на которую меняем
                            cmd.CommandText = "UPDATE action SET start_finish='" + up + "' WHERE  shiftcode ='" + GlobalVariable.replanTwo + "' and start_finish =  '" + next + "'";
                            cmd.ExecuteNonQuery();
                            // Меняем сорт той которую выделили 
                            cmd.CommandText = "UPDATE action SET start_finish='" + next + "' WHERE  shiftcode ='" + GlobalVariable.replanTwo + "' and start = '" + shop + "' and start_finish =  '" + up + "'";
                            cmd.ExecuteNonQuery();
                            // Обновляем порядковый номер выгрузки
                            cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();

                            // Обновляем финишный адрес доставки
                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanTwo + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(count) + "' from action WHERE shiftcode = '" + GlobalVariable.replanTwo + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();

                            // для маршрутного листа
                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "' and actionid = '0'";
                            cmd.ExecuteNonQuery();

                            // Обновляем таблицу действий
                            updateTableTwo();
                            // Возвращаем курсор на место
                            gridRePlanTwo.CurrentCell = gridRePlanTwo.Rows[row_number].Cells[0];
                        }
                        else
                        {
                            MessageBox.Show("РЦ двигать запрещено!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch { }
        }

        private void downTwo(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                        dr = cmd.ExecuteReader();
                        string count = "";
                        while (dr.Read())
                        {
                            count = dr[0].ToString();
                        }
                        dr.Close();

                        CurrencyManager cmgr = (CurrencyManager)this.gridRePlanTwo.BindingContext[this.gridRePlanTwo.DataSource];
                        DataRow row = ((DataRowView)cmgr.Current).Row;
                        string up = row["start_finish"].ToString();
                        int row_number = Convert.ToInt16(up);
                        string shop = row["start"].ToString();
                        if (shop != Convert.ToString(GlobalVariable.depot))
                        {
                            int next = Convert.ToInt32(up) + 1;

                            // Меняй сорт той строке на которую меняем
                            cmd.CommandText = "UPDATE action SET start_finish='" + up + "' WHERE  shiftcode ='" + GlobalVariable.replanTwo + "' and start_finish =  '" + next + "'";
                            cmd.ExecuteNonQuery();
                            // Меняем сорт той которую выделили 
                            cmd.CommandText = "UPDATE action SET start_finish='" + next + "' WHERE  shiftcode ='" + GlobalVariable.replanTwo + "'  and start = '" + shop + "' and start_finish =  '" + up + "'";
                            cmd.ExecuteNonQuery();
                            // Обновляем порядковый номер выгрузки
                            cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();
                            // Обновляем финишный адрес доставки
                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanTwo + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(count) + "' from action WHERE shiftcode = '" + GlobalVariable.replanTwo + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                            cmd.ExecuteNonQuery();

                            // для маршрутного листа
                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "' and actionid = '0'";
                            cmd.ExecuteNonQuery();


                            updateTableTwo();
                            gridRePlanTwo.CurrentCell = gridRePlanTwo.Rows[row_number].Cells[0];
                        }
                        else
                        {
                            MessageBox.Show("РЦ двигать запрещено!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch { }
        }

        //Переносим заказ
        private void one_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //Получаем order_number заказа который перетаскиваем
        private void one_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    gridRePlanOne.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    int count = gridRePlanOne.SelectedRows.Count;
                    if (count <= 10)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            gridRePlanOne.DoDragDrop(gridRePlanOne.SelectedRows[i].Cells["idorderDataGridViewTextBoxColumn"].Value, DragDropEffects.Copy);
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    gridRePlanOne.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    gridRePlanOne.CurrentCell = gridRePlanOne.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    gridRePlanOne.Rows[e.RowIndex].Selected = true;
                    gridRePlanOne.Focus();
                }
                catch { }
            }
        }

        //Планируем заказы
        private void action_DragDrop(object sender, DragEventArgs e)
        {
            if (gridRePlanTwo.SelectedRows.Count > 0)
            {
                Cursor = Cursors.WaitCursor;
                int countOrd = gridRePlanTwo.SelectedRows.Count;
                object item;
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        for (int i = 0; i < countOrd; i++)
                        {
                            item = gridRePlanTwo.SelectedRows[i].Cells["idorderDataGridViewTextBoxColumn1"].Value;

                            //Спланирован ли заказ?
                            if (GlobalVariable.replanOne != 0)
                            {
                                if (String.IsNullOrEmpty(item.ToString()))
                                {
                                    MessageBox.Show("Точку старта\\финиша перемещать запрещено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    try
                                    {
                                        //cn.Open();
                                        // Спланирован ли рейс?
                                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanOne + "'";
                                        dr = cmd.ExecuteReader();
                                        string count = "";
                                        while (dr.Read())
                                        {
                                            count = dr[0].ToString();
                                        }
                                        dr.Close();

                                        //Номер авто
                                        cmd.CommandText = "SELECT top 1 truck FROM shift WHERE shift.shiftcode = '" + GlobalVariable.replanOne + "'";
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
                                            cmd.CommandText = "INSERT into action (shiftcode,start,start_finish, actionid) VALUES('" + GlobalVariable.replanOne + "','" + GlobalVariable.depot + "','1', '0')";
                                            cmd.ExecuteNonQuery();
                                            cmd.CommandText = "INSERT into action (id_order,shiftcode,start,start_finish,finish, actionid) VALUES('" + item + "','" + GlobalVariable.replanOne + "','" + finish + "','2','" + GlobalVariable.depot + "', '0')";
                                            cmd.ExecuteNonQuery();
                                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanOne + "' and start <> '" + GlobalVariable.depot + "') T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanOne + "'";
                                            cmd.ExecuteNonQuery();

                                            // для маршрутного листа
                                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "' and (actionid = '0' or actionid is null)) T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "' and (actionid = '0' or actionid is null)";
                                            cmd.ExecuteNonQuery();

                                        }
                                        //если рейс есть
                                        else
                                        {
                                            int sort = (Convert.ToInt16(count) + 1);
                                            cmd.CommandText = "UPDATE action SET shiftcode = '" + GlobalVariable.replanOne + "' WHERE id_order = '" + item + "'";
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
                                MessageBox.Show("Ни одна смена не выбрана! Выберите смену.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanOne + "'";
                        dr = cmd.ExecuteReader();
                        string countOne = "";
                        while (dr.Read())
                        {
                            countOne = dr[0].ToString();
                        }
                        dr.Close();

                        // Обновляем порядковый номер выгрузки первому рейсу
                        cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "'";
                        cmd.ExecuteNonQuery();

                        // Обновляем финишный адрес доставки первому рейсу
                        cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanOne + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(countOne) + "' from action WHERE shiftcode = '" + GlobalVariable.replanOne + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanOne + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                        cmd.ExecuteNonQuery();

                        // для маршрутного листа первому рейсу
                        cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "' and actionid = '0'";
                        cmd.ExecuteNonQuery();


                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                        dr = cmd.ExecuteReader();
                        string countTwo = "";
                        while (dr.Read())
                        {
                            countTwo = dr[0].ToString();
                        }
                        dr.Close();

                        // Обновляем порядковый номер выгрузки Второму рейсу
                        cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "'";
                        cmd.ExecuteNonQuery();

                        // Обновляем финишный адрес доставки Второму рейсу
                        cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanTwo + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(countTwo) + "' from action WHERE shiftcode = '" + GlobalVariable.replanTwo + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanTwo + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                        cmd.ExecuteNonQuery();

                        // для маршрутного листа Второму рейсу
                        cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "' and actionid = '0'";
                        cmd.ExecuteNonQuery();

                    }
                }

                //update таблиц после цикла
                try
                {
                    updateTableOne();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\n update_orders_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    updateTableTwo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\n update_action_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor = Cursors.Default;
            }
        }

        //Переносим заказ
        private void two_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //Получаем order_number заказа который перетаскиваем
        private void two_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    gridRePlanTwo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    int count = gridRePlanTwo.SelectedRows.Count;
                    if (count <= 10)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            gridRePlanTwo.DoDragDrop(gridRePlanTwo.SelectedRows[i].Cells["idorderDataGridViewTextBoxColumn1"].Value, DragDropEffects.Copy);
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    gridRePlanTwo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    gridRePlanTwo.CurrentCell = gridRePlanTwo.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    gridRePlanTwo.Rows[e.RowIndex].Selected = true;
                    gridRePlanTwo.Focus();
                }
                catch { }
            }
        }
        //Планируем заказы
        private void action_DragDropTwo(object sender, DragEventArgs e)
        {
            if (gridRePlanOne.SelectedRows.Count > 0)
            {
                Cursor = Cursors.WaitCursor;
                int countOrd = gridRePlanOne.SelectedRows.Count;
                object item;
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        for (int i = 0; i < countOrd; i++)
                        {
                            item = gridRePlanOne.SelectedRows[i].Cells["idorderDataGridViewTextBoxColumn"].Value;

                            //Спланирован ли заказ?
                            if (GlobalVariable.replanTwo != 0)
                            {
                                if (String.IsNullOrEmpty(item.ToString()))
                                {
                                    MessageBox.Show("Точку старта\\финиша перемещать запрещено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    try
                                    {
                                        // Спланирован ли рейс?
                                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                                        dr = cmd.ExecuteReader();
                                        string count = "";
                                        while (dr.Read())
                                        {
                                            count = dr[0].ToString();
                                        }
                                        dr.Close();

                                        //Номер авто
                                        cmd.CommandText = "SELECT top 1 truck FROM shift WHERE shift.shiftcode = '" + GlobalVariable.replanTwo + "'";
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
                                            cmd.CommandText = "INSERT into action (shiftcode,start,start_finish, actionid) VALUES('" + GlobalVariable.replanTwo + "','" + GlobalVariable.depot + "','1', '0')";
                                            cmd.ExecuteNonQuery();
                                            cmd.CommandText = "INSERT into action (id_order,shiftcode,start,start_finish,finish, actionid) VALUES('" + item + "','" + GlobalVariable.replanTwo + "','" + finish + "','2','" + GlobalVariable.depot + "', '0')";
                                            cmd.ExecuteNonQuery();
                                            cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanTwo + "' and start <> '" + GlobalVariable.depot + "') T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanTwo + "'";
                                            cmd.ExecuteNonQuery();

                                            // для маршрутного листа
                                            cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "' and (actionid = '0' or actionid is null)) T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "' and (actionid = '0' or actionid is null)";
                                            cmd.ExecuteNonQuery();

                                        }

                                        //если рейс есть

                                        else
                                        {
                                            int sort = (Convert.ToInt16(count) + 1);
                                            cmd.CommandText = "UPDATE action SET shiftcode = '" + GlobalVariable.replanTwo + "' WHERE id_order = '" + item + "'";
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
                                MessageBox.Show("Ни одна смена не выбрана! Выберите смену.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                        dr = cmd.ExecuteReader();
                        string countOne = "";
                        while (dr.Read())
                        {
                            countOne = dr[0].ToString();
                        }
                        dr.Close();

                        // Обновляем порядковый номер выгрузки первому рейсу
                        cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "'";
                        cmd.ExecuteNonQuery();

                        // Обновляем финишный адрес доставки первому рейсу
                        cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanTwo + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(countOne) + "' from action WHERE shiftcode = '" + GlobalVariable.replanTwo + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanTwo + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanTwo + "'";
                        cmd.ExecuteNonQuery();

                        // для маршрутного листа первому рейсу
                        cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanTwo + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanTwo + "' and actionid = '0'";
                        cmd.ExecuteNonQuery();


                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanOne + "'";
                        dr = cmd.ExecuteReader();
                        string countTwo = "";
                        while (dr.Read())
                        {
                            countTwo = dr[0].ToString();
                        }
                        dr.Close();

                        // Обновляем порядковый номер выгрузки Второму рейсу
                        cmd.CommandText = "UPDATE action SET start_finish = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "'";
                        cmd.ExecuteNonQuery();

                        // Обновляем финишный адрес доставки Второму рейсу
                        cmd.CommandText = "UPDATE action SET finish = T2.start FROM action T1 INNER JOIN (SELECT start, start_finish-1 as start_finish FROM action where shiftcode = '" + GlobalVariable.replanOne + "'  UNION select top 1 start, start_finish='" + Convert.ToInt16(countTwo) + "' from action WHERE shiftcode = '" + GlobalVariable.replanOne + "' order by cast(start_finish as int)) T2 ON T1.start_finish = T2.start_finish where shiftcode = '" + GlobalVariable.replanOne + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET actionid = '1' WHERE action.start = action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET actionid = '0' WHERE action.start <> action.finish and action.shiftcode = '" + GlobalVariable.replanOne + "'";
                        cmd.ExecuteNonQuery();

                        // для маршрутного листа Второму рейсу
                        cmd.CommandText = "UPDATE action SET sort = T2.rownum FROM action T1 INNER JOIN (SELECT ROW_NUMBER() OVER (ORDER BY cast(start_finish as int) ASC) AS rownum, id FROM action where shiftcode = '" + GlobalVariable.replanOne + "' and actionid = '0') T2 ON T1.id = T2.id where shiftcode = '" + GlobalVariable.replanOne + "' and actionid = '0'";
                        cmd.ExecuteNonQuery();

                    }
                }

                //update таблиц после цикла
                try
                {
                    updateTableOne();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\n update_orders_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    updateTableTwo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\n update_action_DragDrop" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor = Cursors.Default;
            }

        }
        private void rePlanManyShift_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.replanOne = 0;
            GlobalVariable.replanTwo = 0;
            main m = this.Owner as main;
            m.update_action();
            m.update_shift();
            m.update_orders("");
        }

        private void gridRePlanOne_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            gridRePlanTwo.ClearSelection();
            gridRePlanTwo.AllowDrop = true;
            gridRePlanOne.AllowDrop = false;
        }

        private void gridRePlanTwo_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            gridRePlanOne.ClearSelection();
            gridRePlanOne.AllowDrop = true;
            gridRePlanTwo.AllowDrop = false;
        }

        private void optimizatorOne_Click(object sender, EventArgs e)
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

                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanOne + "' and actionid = 0";
                        dr = cmd.ExecuteReader();
                        int count = 0;
                        while (dr.Read())
                        {
                            count = Convert.ToInt32(dr[0].ToString());
                        }
                        dr.Close();

                        cmd.CommandText = "SELECT homeaddress FROM shift WHERE shift.shiftcode = '" + GlobalVariable.replanOne + "'";
                        dr = cmd.ExecuteReader();
                        string homeAddress = GlobalVariable.depot.ToString();
                        while (dr.Read())
                        {
                            homeAddress = dr[0].ToString();
                        }
                        dr.Close();

                        for (int i = 0; i < count; i++)
                        {
                            cmd.CommandText = "EXEC newOptimization " + GlobalVariable.replanOne + ", " + homeAddress + "";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "EXEC updateFinishAddress " + GlobalVariable.replanOne + "";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE checkOptimization SET offOn = 0";
                        cmd.ExecuteNonQuery();

                        updateTableOne();
                    }
                    else
                    {
                        MessageBox.Show("Оптимизация не возможна! Оптимизатор занят другим процессом, повторите попытку через несколько секунд.", "Оптимизация остановлена", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        private void optimizatorTwo_Click(object sender, EventArgs e)
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

                        cmd.CommandText = "SELECT Count(*) FROM action WHERE action.shiftcode = '" + GlobalVariable.replanTwo + "' and actionid = 0";
                        dr = cmd.ExecuteReader();
                        int count = 0;
                        while (dr.Read())
                        {
                            count = Convert.ToInt32(dr[0].ToString());
                        }
                        dr.Close();

                        cmd.CommandText = "SELECT homeaddress FROM shift WHERE shift.shiftcode = '" + GlobalVariable.replanTwo + "'";
                        dr = cmd.ExecuteReader();
                        string homeAddress = GlobalVariable.depot.ToString();
                        while (dr.Read())
                        {
                            homeAddress = dr[0].ToString();
                        }
                        dr.Close();

                        for (int i = 0; i < count; i++)
                        {
                            cmd.CommandText = "EXEC newOptimization " + GlobalVariable.replanTwo + ", " + homeAddress + "";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "EXEC updateFinishAddress " + GlobalVariable.replanTwo + "";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE checkOptimization SET offOn = 0";
                        cmd.ExecuteNonQuery();

                        updateTableTwo();
                    }
                    else
                    {
                        MessageBox.Show("Оптимизация не возможна! Оптимизатор занят другим процессом, повторите попытку через несколько секунд.", "Оптимизация остановлена", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        private void gridRePlanOne_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                try
                {
                    int summ = 0;
                    int summPal = 0;
                    int count = gridRePlanOne.SelectedRows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        summ += Convert.ToInt32(gridRePlanOne.SelectedRows[i].Cells["kgDataGridViewTextBoxColumn"].Value);
                        summPal += Convert.ToInt32(gridRePlanOne.SelectedRows[i].Cells["palDataGridViewTextBoxColumn"].Value);
                    }
                    uploadOne.Text = "Выбрано: КГ: " + summ.ToString() + " Пал: " + summPal.ToString();
                }
                catch { }
            }
        }

        private void gridRePlanOne_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == false)
                updateLabelOne();
        }

        private void gridRePlanTwo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                try
                {
                    int summ = 0;
                    int summPal = 0;
                    int count = gridRePlanTwo.SelectedRows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        summ += Convert.ToInt32(gridRePlanTwo.SelectedRows[i].Cells["kgDataGridViewTextBoxColumn1"].Value);
                        summPal += Convert.ToInt32(gridRePlanTwo.SelectedRows[i].Cells["palDataGridViewTextBoxColumn1"].Value);
                    }
                    uploadTwo.Text = "Выбрано: КГ: " + summ.ToString() + " Пал: " + summPal.ToString();
                }
                catch { }
            }
        }

        private void gridRePlanTwo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == false)
                updateLabelTwo();
        }
    }
}
