using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxManager
{
    public partial class Main : Form
    {
        SqlDataReader dr;
        BindingSource bs = new BindingSource();
        bool matrixLoad = false;
        double moreOneDay;
        ArrayList empty = new ArrayList();
        ImportCSV importRegistr = new ImportCSV();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            label_saved.Text = "";

            //Загружаем даты последний матриц
            if (matrixDateUpload())
                matrixLoad = true;


            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE", false);

            //Вычитуем константы из БД
            readConstants();

            info_trailer.Text = "";
            info_truckNumber.Text = "";
            info_tripNumber.Text = "";
            info_region.Text = "";
            info_dateDoc.Text = "";
            info_cityOrNot.Text = "";
            GlobalVariable.matrixDate = DateTime.Parse(dateTimePicker_matrixDate.SelectedValue.ToString()).ToString("yyyy-MM-dd");
            Cursor = Cursors.Default;
        }

        private void takeInfoShiftCode(String shiftcode)
        {
            try
            {
                label_saved.Text = "";

                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;

                        using (DataTable dt = new DataTable())
                        {
                            cmd.CommandText = "EXEC workTransport '"+ shiftcode +"'";
                            dt.Load(cmd.ExecuteReader());
                            dt.Columns["addressCode"].ReadOnly = false;
                            bs.DataSource = dt;
                            dgv_mainWindow.DataSource = bs;
                        }

                        dr = cmd.ExecuteReader();

                        int checkRef = 0;
                        int idTariff = 4;

                        if (dr.Read())
                        {
                            int addPoint = String.IsNullOrEmpty(dr["addPoint"].ToString()) ? 0 : int.Parse(dr["addPoint"].ToString());
                            info_truckNumber.Text = dr["truck"].ToString();
                            info_trailer.Text = String.IsNullOrEmpty(dr["trailer"].ToString()) ? "" : dr["trailer"].ToString();
                            info_tripNumber.Text = dr["tripNumber"].ToString();
                            checkRef = String.IsNullOrEmpty(dr["ref"].ToString()) ? 0 : Int16.Parse(dr["ref"].ToString());
                            info_dateDoc.Value = String.IsNullOrEmpty(dr["dateDocument"].ToString()) ? info_dateDoc.Value : DateTime.Parse(dr["dateDocument"].ToString());
                            info_region.Text = dr["region"].ToString();
                            info_addPoint.Text = addPoint.ToString();
                            info_ttn.Text = shiftcode;
                            idTariff = String.IsNullOrEmpty(dr["id_tariffType"].ToString()) ? 4 : int.Parse(dr["id_tariffType"].ToString());
                            txt_howHour.Text = dr["hourToPay"].ToString();
                            txt_tariffHour.Text = dr["tarifPerHour"].ToString();
                            txt_hourSum.Text = dr["sumHourToPay"].ToString();
                            txt_howKm.Text = dr["kmToPer"].ToString();
                            txt_tariffKm.Text = dr["tarifPerKm"].ToString();
                            txt_sumKm.Text = dr["sumKmToPay"].ToString();
                            txt_fixToPay.Text = dr["tariffPerTrip"].ToString();
                            txt_addPointTariff.Text = GlobalVariable.addPerForPoint.ToString();
                            moreOneDay = String.IsNullOrEmpty(dr["allHour"].ToString()) ? 0 : double.Parse(dr["allHour"].ToString());
                            txt_addPointSum.Text = (GlobalVariable.addPerForPoint * addPoint).ToString();
                            txt_allsum.Text = (double.Parse(dr["sumHourToPay"].ToString()) + double.Parse(dr["sumKmToPay"].ToString()) + double.Parse(dr["tariffPerTrip"].ToString()) + (GlobalVariable.addPerForPoint * addPoint)).ToString();
                        }

                        dr.Close();

                        GlobalVariable.cityOrNot = thisTripIsCity();
                        cb_ref.Checked = false;

                        if (!GlobalVariable.cityOrNot && cB_return.Enabled)
                            cB_return.Enabled = false;
                        else
                            cB_return.Enabled = true;

                        if (checkRef == 1)
                            cb_ref.Checked = true;

                        switch (idTariff)
                        {
                            case 0: info_cityOrNot.Text = "Межгород"; break;
                            case 1: info_cityOrNot.Text = "По городу"; break;
                            case 2: info_cityOrNot.Text = "Фрахт"; break;
                            case 3: info_cityOrNot.Text = "Спец рейс"; break;
                        }

                        if (isTripMoreTwoDay())
                        {
                            MessageBox.Show("Рейс больше двух суток!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (moreOneDay > 24)
                                MessageBox.Show("Рейс больше суток!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                        dgv_mainWindow.Focus();

                        int nextColumn = dgv_mainWindow.Columns["startTime"].Index;

                        dgv_mainWindow.CurrentCell = dgv_mainWindow.Rows[0].Cells[nextColumn];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n takeInfoShiftCode" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isTripMoreTwoDay()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = "EXEC moreTwoDay @shiftcode";
                        cmd.Parameters.AddWithValue("@shiftcode", GlobalVariable.sh);

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        if (int.Parse(dr["result"].ToString()) == 1)
                            return true; // если больше 48 часов рейс
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n isTripMoreTwoDay " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool thisTripIsCity()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = "EXEC thisTripInCity @shiftcode, @maxKM";
                        cmd.Parameters.AddWithValue("@shiftcode", GlobalVariable.sh);
                        cmd.Parameters.AddWithValue("@maxKM", GlobalVariable.maxKmToFirstPoint);

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        if (int.Parse(dr["result"].ToString()) == 0)
                            return true; // если город
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n thisTripIsCity " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private String separator(String someString)
        {
            if (String.IsNullOrEmpty(someString))
                return "";

           return someString.Replace(',', '.');
        }

        private void countPointInTrip()
        {
            try
            {
                if (!GlobalVariable.cityOrNot)
                {
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        cn.Open();

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = cn;
                            cmd.CommandText = "EXEC countPointInTrip @shiftcode";
                            cmd.Parameters.AddWithValue("@shiftcode", GlobalVariable.sh);
                            dr = cmd.ExecuteReader();
                            dr.Read();
                            info_addPoint.Text = dr[0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n countPointInTrip " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void readConstants()
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
                            cmd.CommandText = "select * from constants";
                            dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                switch (int.Parse(dr["id"].ToString()))
                                {
                                    case 1: GlobalVariable.tAndTinCity = double.Parse(dr["uvalue"].ToString()); break;
                                    case 2: GlobalVariable.tInCity = double.Parse(dr["uvalue"].ToString()); break;
                                    case 3: GlobalVariable.tAndTOutCity = double.Parse(dr["uvalue"].ToString()); break;
                                    case 4: GlobalVariable.tOutCity = double.Parse(dr["uvalue"].ToString()); break;
                                    case 5: GlobalVariable.maxKmToFirstPoint = double.Parse(dr["uvalue"].ToString()); break;
                                    case 6: GlobalVariable.addPerForPoint = double.Parse(dr["uvalue"].ToString()); break; 
                                }
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n readConstants" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_import_ttn_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            createNewTrip();
            Cursor = Cursors.Default;
                       
        }

        private void createNewTrip()
        {
            String shiftCode = tb_shiftcode.Text;

            if (!String.IsNullOrEmpty(shiftCode))
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (isSetTripInTaxManager(shiftCode))
                        {
                            if (isSetTripInOrd(shiftCode))
                            {
                                takeTripFromOrd(shiftCode);
                                reCalculateTrip(int.Parse(shiftCode));
                                takeInfoShiftCode(shiftCode);
                                countPointInTrip();
                            }
                            else
                            {
                                MessageBox.Show("Такой рейс не существует в системе TMS.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            var result = MessageBox.Show("Такой номер ТТН уже протаксирован, загрузить еще раз?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                takeInfoShiftCode(shiftCode);
                                countPointInTrip();
                            }
                            
                        }
                        dr.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Поле \"Номер ТТН\" не может быть пустым.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool isSetTripInOrd (String shiftCode)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT shiftcode FROM  COMTECdefault.dbo.shift WHERE shiftCode = '" + shiftCode + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        return true;
                    }
                    dr.Close();
                    return false;
                }
            }
        }

        private bool isSetTripInDataRegistr(int shiftCode)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT shiftcode FROM  dataRegistr WHERE shiftCode = '" + shiftCode + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        return true;
                    }
                    dr.Close();
                    return false;
                }
            }
        }

        private bool isSetTripInTaxManager(String shiftCode)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT shiftcode FROM  newRegistr WHERE shiftCode = '" + shiftCode + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        return false;
                    }
                    dr.Close();
                    return true;
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        private void takeTripFromOrd(String shiftcode)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.CommandText = "EXEC takeTripFromORD '" + shiftcode + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "EXEC takeInfoForTripFromORD '" + shiftcode + "', '"+ GlobalVariable.userName +"'";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n takeTripFromOrd" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void tb_shiftcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                createNewTrip();
        }

        private double convertSeparator(String value)
        {
            value = value.Replace(',', '.');
            CultureInfo ci = new CultureInfo("en-US");
            double d = double.Parse(value, ci.NumberFormat);
            MessageBox.Show("" + d);
            return d;
        }

        private void saveNewTrip()
        {
            try
            {
                string commandText = "UPDATE newRegistr " +
                                "SET newRegistr.departedTime = @startTime, " +
                                "newRegistr.arrivedTime = @finishTime, " +
                                "newRegistr.pal = @pal, " +
                                "newRegistr.kg = @kg, " +
                                "newRegistr.addressCode = CASE WHEN @addressCode in (select depotName from region) THEN (select top 1 cast(regionCode as varchar) from region where depotname = @addressCode) ELSE @addressCode end, " +
                                "newRegistr.addressName = CASE WHEN @addressCode in (select regionCode from region) THEN (select top 1 depotName from region where regionCode = @addressCode) ELSE @addressCode end, " +
                                "newRegistr.addKm = @addKm " +
                                " WHERE newRegistr.shiftCode = @shiftcode and newRegistr.id = @id ";

                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.Parameters.Add("@startTime", SqlDbType.Time);
                        cmd.Parameters.Add("@finishTime", SqlDbType.Time);
                        cmd.Parameters.Add("@pal", SqlDbType.Int);
                        cmd.Parameters.Add("@kg", SqlDbType.Decimal);
                        cmd.Parameters.Add("@addKm", SqlDbType.Decimal);
                        cmd.Parameters.Add("@shiftcode", SqlDbType.VarChar);
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters.Add("@addressCode", SqlDbType.VarChar);
                        
                        cmd.Parameters.AddWithValue("@isTax", 1);

                        for (int i = 0; i < dgv_mainWindow.RowCount; i++)
                        {
                            cmd.CommandText = commandText;
                            cmd.Parameters["@startTime"].Value = dgv_mainWindow.Rows[i].Cells["startTime"].Value;
                            cmd.Parameters["@finishTime"].Value = dgv_mainWindow.Rows[i].Cells["finishTime"].Value;
                            cmd.Parameters["@pal"].Value = dgv_mainWindow.Rows[i].Cells["pal"].Value;
                            cmd.Parameters["@kg"].Value = dgv_mainWindow.Rows[i].Cells["kg"].Value;
                            cmd.Parameters["@addKm"].Value = dgv_mainWindow.Rows[i].Cells["addKm"].Value;
                            cmd.Parameters["@shiftcode"].Value = GlobalVariable.sh;
                            cmd.Parameters["@id"].Value = dgv_mainWindow.Rows[i].Cells["id"].Value;
                            cmd.Parameters["@addressCode"].Value = dgv_mainWindow.Rows[i].Cells["addressCode"].Value;
                            cmd.ExecuteNonQuery();
                        }

                        commandText = "UPDATE dataRegistr SET " +
                            "ref = @ref, " +
                            "isTax = @isTax, " +
                            "registrName = @registrName " +
                            "WHERE shiftcode = @shiftcode";
                        int frigo = 0;
                        if(cb_ref.Checked)
                            frigo = 1;

                        cmd.CommandText = commandText;
                        cmd.Parameters.AddWithValue("@ref", frigo);
                        cmd.Parameters.Add("@registrName", SqlDbType.NVarChar);
                        cmd.Parameters["@registrName"].Value = String.IsNullOrEmpty(txt_registrName.Text) ? "" : txt_registrName.Text;
                        cmd.ExecuteNonQuery();

                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n saveNewTrip" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void reCalculateTrip(int shiftcode)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.CommandText = "EXEC calculateKmForTrip @shiftcode, @matrixDate";
                        cmd.Parameters.AddWithValue("@matrixDate", GlobalVariable.matrixDate);
                        cmd.Parameters.AddWithValue("@shiftcode", shiftcode);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n reCalculateTrip" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_saveTrip_Click(object sender, EventArgs e)
        {
            if (rB_manualy.Checked)
            {
                createTruckFroManualy();
                rB_automatic.Checked = true;
            }
            else
            {
                if (GlobalVariable.cityOrNot)
                {
                    if (!areColumnTimeToType())
                    {
                        var result = MessageBox.Show("Рейс по городу, но время не заполнено, все равно сохранить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            label_saved.Text = "Не сохранено!";
                            return;
                        }
                    }
                }

                if (dgv_mainWindow.RowCount >= 1)
                {
                 
                    saveUpdateAndReCalc();
                    tb_shiftcode.Text = "";
                    label_saved.Text = "Сохранено!";
                    tb_shiftcode.Focus();
                }
                else
                    MessageBox.Show("Таблица пуста, нет данных для сохранения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_mainWindow_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка, введен не верный символ!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void callTariffWindow()
        {
            using (Tariff f1 = new Tariff())
            {
                f1.ShowDialog(this);
            }
        }

        private void callEditDepotWindow()
        {
            using (editDepot f2 = new editDepot())
            {
                f2.ShowDialog(this);
            }
        }

        private void callConstantsWindow()
        {
            using (Constants f3 = new Constants())
            {
                f3.ShowDialog(this);
            }
        }

        private void callActsWindow()
        {
            using (Acts f4 = new Acts())
            {
                f4.ShowDialog(this);
            }
        }

        private void callUsersWindow()
        {
            using (users f5 = new users())
            {
                f5.ShowDialog(this);
            }
        }

        private void callRegistryWindow()
        {
            Registry f6 = new Registry();
            f6.Show();
        }

        private void callRegistryReportWindow()
        {
            using (ReportRegistr f7 = new ReportRegistr())
            {
                f7.ShowDialog(this);
            }
        }

        private void callSelectCarWindow(string resource)
        {
            using (selectCar f8 = new selectCar())
            {
                GlobalVariable.resourceName = resource;
                f8.ShowDialog(this);
            }
        }

        private void callAddressWindow()
        {
            using (Address f9 = new Address())
            {
                f9.ShowDialog(this);
            }
        }

        private void callRegistrTTNWindow()
        {
            RegistrForm f10 = new RegistrForm();
            f10.Show();
        }

        private void callReportCountTripInDay()
        {
            using (ReportCountTripInDayForm f11 = new ReportCountTripInDayForm())
            {
                f11.ShowDialog(this);
            }
        }

        private void topMenuTariffButton_Click(object sender, EventArgs e)
        {
            callTariffWindow();
        }

        private bool matrixDateUpload()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "exec takeMatrixDate";
                        DataTable product = new DataTable();
                        product.Load(cmd.ExecuteReader());
                        dateTimePicker_matrixDate.DataSource = product;
                        dateTimePicker_matrixDate.DisplayMember = "cd";
                        dateTimePicker_matrixDate.ValueMember = "cd";
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void dgv_mainWindow_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    ContextMenuStrip my_menu = new System.Windows.Forms.ContextMenuStrip();
                    this.TopMost = true;

                    int position_x_y_mouse = dgv_mainWindow.HitTest(e.X, e.Y).RowIndex;

                    if (position_x_y_mouse >= 0)
                    {
                            my_menu.Items.Add("Вставить строку над").Name = "insertString";
                            my_menu.Items.Add("Вставить строку под").Name = "insertStringDown";
                            my_menu.Items.Add("Удалить").Name = "delete";
                    }
                    
                    my_menu.Show(dgv_mainWindow, new Point(e.X, e.Y));

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);
                    my_menu.Items["insertString"].Image = TaxManager.Properties.Resources.up;
                    my_menu.Items["insertStringDown"].Image = TaxManager.Properties.Resources.down;
                    my_menu.Items["delete"].Image = TaxManager.Properties.Resources.delete;

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
                if (e.ClickedItem.Name.ToString() == "insertString")
                {
                    try
                    {
                        saveNewTrip();
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;

                                int selectRow = int.Parse(dgv_mainWindow.SelectedRows[0].Cells["sort"].Value.ToString());

                                cmd.CommandText = "UPDATE newRegistr SET sort = sort+1 WHERE shiftcode = '" + GlobalVariable.sh + "' and sort >= '" + selectRow + "' ";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "INSERT into newRegistr (shiftcode, sort) VALUES ('"+ GlobalVariable.sh +"', '" + selectRow + "')";
                                cmd.ExecuteNonQuery();

                                takeInfoShiftCode(GlobalVariable.sh.ToString());

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n insertString " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (e.ClickedItem.Name.ToString() == "insertStringDown")
                {
                    try
                    {
                        saveNewTrip();
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;

                                int selectRow = int.Parse(dgv_mainWindow.SelectedRows[0].Cells["sort"].Value.ToString())+1;

                                cmd.CommandText = "UPDATE newRegistr SET sort = sort+1 WHERE shiftcode = '" + GlobalVariable.sh + "' and sort >= '" + selectRow + "' ";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "INSERT into newRegistr (shiftcode, sort) VALUES ('" + GlobalVariable.sh + "', '" + selectRow + "')";
                                cmd.ExecuteNonQuery();

                                takeInfoShiftCode(GlobalVariable.sh.ToString());

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n insertString " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (e.ClickedItem.Name.ToString() == "delete")
                {
                    try
                    {
                        saveNewTrip();
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;

                                int selectRow = int.Parse(dgv_mainWindow.SelectedRows[0].Cells["sort"].Value.ToString());
                                int id = int.Parse(dgv_mainWindow.SelectedRows[0].Cells["id"].Value.ToString());

                                cmd.CommandText = "UPDATE newRegistr SET sort = sort-1 WHERE shiftcode = '" + GlobalVariable.sh + "' and sort > '" + selectRow + "' ";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "DELETE FROM newRegistr WHERE shiftcode = '" + GlobalVariable.sh + "' and id = '" + id + "'";
                                cmd.ExecuteNonQuery();

                                saveUpdateAndReCalc();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error\n insertString " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n my_menu_ItemClicked" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_mainWindow_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    dgv_mainWindow.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv_mainWindow.CurrentCell = dgv_mainWindow.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                else
                {
                    dgv_mainWindow.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                }
            }
            catch { }
        }

        private void dateTimePicker_matrixDate_ValueChanged(object sender, EventArgs e)
        {
            if (matrixLoad)
            {
                GlobalVariable.matrixDate = DateTime.Parse(dateTimePicker_matrixDate.SelectedValue.ToString()).ToString("yyyy-MM-dd");
            }
        }

        private void saveUpdateAndReCalc()
        {
            Cursor = Cursors.WaitCursor;
            countPointInTrip();
            saveNewTrip();
            reCalculateTrip(GlobalVariable.sh);
            updateDataRegistr();
            takeInfoShiftCode(GlobalVariable.sh.ToString());
            Cursor = Cursors.Default;
        }

        private bool areColumnTimeToType()
        {
            for (int i = 0; i < dgv_mainWindow.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dgv_mainWindow.Rows[i].Cells["finishTime"].Value.ToString()))
                    return true;
            }

            return false;
        }

        public void updateDataRegistrForMaxRecalc(string shiftcode, string isRef, int calcTypeForTariff, string point, string isReturns)
        {
            try
            {
                if (String.IsNullOrEmpty(isRef))
                    isRef = "0";

                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "EXEC sumToPer @shiftcode, @km, @ref, @isCity, @isReturns";
                        cmd.Parameters.Add("@shiftcode", SqlDbType.Int);
                        cmd.Parameters.Add("@km", SqlDbType.Int);
                        cmd.Parameters.Add("@ref", SqlDbType.Int);
                        cmd.Parameters.Add("@isCity", SqlDbType.Int);
                        cmd.Parameters.Add("@isReturns", SqlDbType.Int);
                        cmd.Parameters["@shiftcode"].Value = shiftcode;
                        cmd.Parameters["@km"].Value = GlobalVariable.tInCity;
                        cmd.Parameters["@ref"].Value = isRef == "1" ? 1 : 0;
                        cmd.Parameters["@isReturns"].Value = isReturns == "1" ? 1 : 0;
                        cmd.Parameters["@isCity"].Value = calcTypeForTariff;

                        string[] arrParameters = new string[12];

                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            arrParameters[0] = dr["tarifPerHour"].ToString();
                            arrParameters[1] = dr["tarifPerKM"].ToString();
                            arrParameters[2] = dr["tariffPerTrip"].ToString();
                            arrParameters[3] = dr["allSum"].ToString();

                        }
                        dr.Close();

                        int addPoint;

                        int addPointOne = String.IsNullOrEmpty(point) ? 0 : int.Parse(point);

                        addPoint = addPointOne;


                        cmd.Parameters.Add("@tarifPerHour", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKM", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tariffPerTrip", SqlDbType.Decimal);
                        cmd.Parameters.Add("@allSum", SqlDbType.Decimal);
                        cmd.Parameters.Add("@addPointTariff", SqlDbType.Decimal);
                        cmd.Parameters.Add("@addPoint", SqlDbType.Int);


                        cmd.Parameters["@tarifPerHour"].Value = String.IsNullOrEmpty(arrParameters[0].ToString()) ? "0" : arrParameters[0];
                        cmd.Parameters["@tarifPerKM"].Value = String.IsNullOrEmpty(arrParameters[1].ToString()) ? "0" : arrParameters[1];
                        cmd.Parameters["@tariffPerTrip"].Value = String.IsNullOrEmpty(arrParameters[2].ToString()) ? "0" : arrParameters[2];
                        cmd.Parameters["@allSum"].Value = String.IsNullOrEmpty(arrParameters[3].ToString()) ? "0" : arrParameters[3];
                        cmd.Parameters["@addPoint"].Value = addPointOne;
                        cmd.Parameters["@addPointTariff"].Value = String.IsNullOrEmpty(point) ? 0 : GlobalVariable.addPerForPoint;

                        cmd.CommandText = "UPDATE dataRegistr SET " +
                            "tarifPerHour = @tarifPerHour, " +
                            "tarifPerKM = @tarifPerKM, " +
                            "tariffPerTrip = @tariffPerTrip, " +
                            "allSum = @allSum+(@addPoint * @addPointTariff) " +
                            "WHERE shiftcode = @shiftcode";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка updateDataRegistrForMaxRecalc сохранения в базу данных! " + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_mainWindow_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_mainWindow.Columns["addressCode"].Index == e.ColumnIndex)
            {
                int currRow = dgv_mainWindow.CurrentCell.RowIndex;
                int currColumn = dgv_mainWindow.CurrentCell.ColumnIndex;

                countPointInTrip();
                saveUpdateAndReCalc();

                if (dgv_mainWindow.RowCount <= currRow + 1)
                    dgv_mainWindow.CurrentCell = dgv_mainWindow.Rows[currRow].Cells[currColumn];
                else
                    dgv_mainWindow.CurrentCell = dgv_mainWindow.Rows[currRow+1].Cells[currColumn];
            }
        }

        private int calcTypeForTariff()
        {
            //Для спец рейсов
            string firstPointInTrip = "";

            int maxValue = 4;

            if (dgv_mainWindow.Rows.Count <= maxValue)
                maxValue = dgv_mainWindow.Rows.Count;

            for (int i = 0; i < maxValue; i++)
            {
                firstPointInTrip += dgv_mainWindow.Rows[i].Cells["addressCode"].Value.ToString();
            }

            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.Parameters.Add("@shiftcode", SqlDbType.Int);
                    cmd.Parameters["@shiftcode"].Value = GlobalVariable.sh;
                    cmd.CommandText = "exec takeTariffTypeFromShift @shiftcode";
                    dr = cmd.ExecuteReader();
                    int shiftTariffType = 0;
                    string dateDocument = "";
                    if (dr.Read())
                    {
                        shiftTariffType = int.Parse(dr["tariffType"].ToString());
                        dateDocument = dr["dateDocument"].ToString();
                    }
                    dr.Close();
                    
                    cmd.Parameters.Add("@firstPointInTrip", SqlDbType.VarChar);
                    cmd.Parameters.Add("@shiftTariffType", SqlDbType.Int);
                    cmd.Parameters.Add("@dateDocument", SqlDbType.Date);
                    cmd.Parameters["@firstPointInTrip"].Value = firstPointInTrip;
                    cmd.Parameters["@shiftTariffType"].Value = shiftTariffType;
                    cmd.Parameters["@dateDocument"].Value = dateDocument;
                    cmd.CommandText = "select * from tarifComparisonTable WHERE typeCodeTariffTable = @firstPointInTrip and typeIdInTariffTable = @shiftTariffType";
                    dr = cmd.ExecuteReader();
                    bool isSpecial = false;
                    if (dr.HasRows)
                        isSpecial = true;
                    dr.Close();

                    bool isFreight = false;
                    cmd.CommandText = "EXEC isFreight @shiftcode";
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                        isFreight = int.Parse(dr[0].ToString()) == 1 ? true : false;
                    dr.Close();

                    if (isSpecial)
                        return 3;
                    else if (GlobalVariable.cityOrNot)
                        return 1;
                    else if (isFreight)
                        return 2;
                    else if (!isFreight && !GlobalVariable.cityOrNot)
                        return 0;
                    else
                        return 10;
                       
                }
            }
        }

        private void manually()
        {
            tb_shiftcode.Enabled = false;
            btn_import_ttn.Enabled = false;
            info_trailer.Enabled = true;
            info_tripNumber.Enabled = true;
            info_truckNumber.Enabled = true;
            info_ttn.Enabled = true;
            txt_howHour.Text = "";
            txt_tariffHour.Text = "";
            txt_hourSum.Text = "";
            txt_howKm.Text = "";
            txt_tariffKm.Text = "";
            txt_sumKm.Text = "";
            txt_fixToPay.Text = "";
            txt_addPointTariff.Text = "";
            txt_addPointSum.Text = "";
            txt_allsum.Text = "";
            label_saved.Text = "";
        }

        private void automatic()
        {
            rB_automatic.Checked = true;
            tb_shiftcode.Enabled = true;
            btn_import_ttn.Enabled = true;
            info_trailer.Enabled = false;
            info_tripNumber.Enabled = false;
            info_truckNumber.Enabled = false;
            info_ttn.Enabled = false;
            label_saved.Text = "";
        }

        private void rB_manualy_CheckedChanged(object sender, EventArgs e)
        {
            if (rB_manualy.Checked)
            {
                manually();
            }
            else if (rB_automatic.Checked)
            {
                automatic();
            }
                
        }

        private void info_ttn_TextChanged(object sender, EventArgs e)
        {
            if (rB_manualy.Checked)
                tb_shiftcode.Text = info_ttn.Text;
        }

        private bool isSetTruckInOrd(String truck)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.Parameters.Add("@truck", SqlDbType.VarChar);
                    cmd.Parameters["@truck"].Value = truck;
                    cmd.CommandText = "SELECT * FROM COMTECdefault.dbo.resource WHERE LEFT(resourceName,8) LIKE '%'+@truck+'%'";
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                        return true;
                }
            }
            return false;
        }

        private void createTripManualy(string truckNumber, string trailerNumber, int type)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.Parameters.Add("@truck", SqlDbType.VarChar);
                    cmd.Parameters.Add("@trailer", SqlDbType.VarChar);
                    cmd.Parameters["@truck"].Value = truckNumber;
                    cmd.Parameters["@trailer"].Value = trailerNumber;
                    cmd.Parameters.Add("@shiftCode", SqlDbType.Int);
                    cmd.Parameters.Add("@dateDoc", SqlDbType.Date);
                    cmd.Parameters.Add("@tripNumber", SqlDbType.Int);
                    cmd.Parameters.Add("@ref", SqlDbType.Int);
                    cmd.Parameters.Add("@type", SqlDbType.Int);
                    cmd.Parameters.Add("@userName", SqlDbType.NChar);
                    cmd.Parameters["@userName"].Value = GlobalVariable.userName;
                    cmd.Parameters["@shiftCode"].Value = info_ttn.Text;
                    cmd.Parameters["@dateDoc"].Value = info_dateDoc.Value.ToString("yyyy-MM-dd");
                    cmd.Parameters["@tripNumber"].Value = String.IsNullOrEmpty(info_tripNumber.Text) ? "1" : info_tripNumber.Text;
                    cmd.Parameters["@ref"].Value = cb_ref.Checked ? 1 : 0;
                    cmd.Parameters["@type"].Value = type; // если 0 то инсерт если 1 то апдейт
                    cmd.CommandText = "insertForManually @truck, @trailer, @shiftCode, @dateDoc, @tripNumber, @ref, @type, @userName";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool isOneCar(string resource)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.Parameters.Add("@resource", SqlDbType.VarChar);
                    cmd.Parameters["@resource"].Value = resource;
                    cmd.CommandText = "EXEC selectResourceForManualy @resource, 1";
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    return int.Parse(dr[0].ToString()) <= 1 ? true:false;
                }
            }
        }

        private void createTruckFroManualy()
        {
            try
            {
                if (!String.IsNullOrEmpty(info_truckNumber.Text) && !String.IsNullOrEmpty(info_ttn.Text))
                {
                    Translit translit = new Translit();

                    string truckNumber = translit.TranslitFileName(info_truckNumber.Text);
                    string trailerNumber = String.IsNullOrEmpty(info_trailer.Text) ? "" : translit.TranslitFileName(info_trailer.Text);

                    if (isNumeric(info_ttn.Text))
                    {
                        if (!isSetTripInTaxManager(info_ttn.Text))
                        {
                            var result = MessageBox.Show("Такой номер ТТН уже протаксирован, пересохранить информацию?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                if (isSetTruckInOrd(truckNumber))
                                {
                                    if (isSetTruckInOrd(trailerNumber) || String.IsNullOrEmpty(info_trailer.Text))
                                    {
                                        if (!isOneCar(truckNumber))
                                        {
                                            callSelectCarWindow(truckNumber);
                                            truckNumber = GlobalVariable.resourceName;
                                        }

                                        if (!String.IsNullOrEmpty(trailerNumber) && !isOneCar(trailerNumber))
                                        {
                                            callSelectCarWindow(trailerNumber);
                                            trailerNumber = GlobalVariable.resourceName;
                                        }

                                        createTripManualy(truckNumber, trailerNumber, 1);
                                        saveUpdateAndReCalc();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Введен не корректный номер прицепа или прицеп не существует в базе данных TMS. Попробуйте авто вводить с буквами, например:АЕ0101ЕА", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Введен не корректный номер авто или авто не существует в базе данных TMS. Попробуйте авто вводить с буквами, например:АЕ0101ЕА", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (isSetTruckInOrd(truckNumber))
                            {
                                if (isSetTruckInOrd(trailerNumber) || String.IsNullOrEmpty(info_trailer.Text))
                                {
                                    if (!isOneCar(truckNumber))
                                    {
                                        callSelectCarWindow(truckNumber);
                                        truckNumber = GlobalVariable.resourceName;
                                    }

                                    if (!String.IsNullOrEmpty(trailerNumber) && !isOneCar(trailerNumber))
                                    {
                                        callSelectCarWindow(trailerNumber);
                                        trailerNumber = GlobalVariable.resourceName;
                                    }

                                    createTripManualy(truckNumber, trailerNumber, 0);
                                    takeInfoShiftCode(info_ttn.Text);
                                }
                                else
                                {
                                    MessageBox.Show("Введен не корректный номер прицепа или прицеп не существует в базе данных TMS. Попробуйте авто вводить с буквами, например:АЕ0101ЕА", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введен не корректный номер авто или авто не существует в базе данных TMS. Попробуйте авто вводить с буквами, например:АЕ0101ЕА", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введен не корректный номер ТТН, в поле должны быть только цифры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    info_ttn.BackColor = Color.Bisque;
                    info_truckNumber.BackColor = Color.Bisque;
                    MessageBox.Show("Обязательные поля должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    info_ttn.BackColor = Color.White;
                    info_truckNumber.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n btn_createTripManualy_Click " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        public void setInfoTruckNumber(string truck)
        {
            info_truckNumber.Text = truck;
        }

        private void tb_shiftcode_TextChanged(object sender, EventArgs e)
        {
            if (tb_shiftcode.TextLength > 1 && !isNumeric(tb_shiftcode.Text))
                 MessageBox.Show("Номер ТТН должен быть цифрой!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (tb_shiftcode.TextLength >= 4)
            {
                GlobalVariable.sh = int.Parse(tb_shiftcode.Text);
            }
        }


        private void sumToPerForRingTariff()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "EXEC sumToPer @shiftcode, @km, @ref, @isCity";
                        cmd.Parameters.Add("@shiftcode", SqlDbType.Int);
                        cmd.Parameters.Add("@km", SqlDbType.Int);
                        cmd.Parameters.Add("@ref", SqlDbType.Int);
                        cmd.Parameters.Add("@isCity", SqlDbType.Int);
                        cmd.Parameters["@shiftcode"].Value = GlobalVariable.sh;
                        cmd.Parameters["@km"].Value = GlobalVariable.tInCity;
                        cmd.Parameters["@ref"].Value = cb_ref.Checked ? 1 : 0;
                        cmd.Parameters["@isCity"].Value = calcTypeForTariff();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка sumToPerForRingTariff " + ex, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void updateDataRegistr()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "EXEC sumToPer @shiftcode, @km, @ref, @isCity, @isReturns";
                        cmd.Parameters.Add("@shiftcode", SqlDbType.Int);
                        cmd.Parameters.Add("@km", SqlDbType.Int);
                        cmd.Parameters.Add("@ref", SqlDbType.Int);
                        cmd.Parameters.Add("@isReturns", SqlDbType.Int);
                        cmd.Parameters.Add("@isCity", SqlDbType.Int);
                        cmd.Parameters["@shiftcode"].Value = GlobalVariable.sh;
                        cmd.Parameters["@km"].Value = GlobalVariable.tInCity;
                        cmd.Parameters["@ref"].Value = cb_ref.Checked ? 1 : 0;
                        cmd.Parameters["@isCity"].Value = calcTypeForTariff();
                        cmd.Parameters["@isReturns"].Value = cB_return.Checked ? 1 : 0;

                        string[] arrParameters = new string[12];

                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            arrParameters[0] = dr["sumHour"].ToString();
                            arrParameters[1] = dr["tarifPerHour"].ToString();
                            arrParameters[2] = dr["hourToPer"].ToString();
                            arrParameters[3] = dr["transportKg"].ToString();
                            arrParameters[4] = dr["transportPal"].ToString();
                            arrParameters[5] = dr["sumKm"].ToString();
                            arrParameters[6] = dr["sumKmWithOrd"].ToString();
                            arrParameters[7] = dr["sucTkm"].ToString();
                            arrParameters[8] = dr["tarifPerKM"].ToString();
                            arrParameters[9] = dr["kmToPer"].ToString();
                            arrParameters[10] = dr["tariffPerTrip"].ToString();
                            arrParameters[11] = dr["allSum"].ToString();

                        }
                        dr.Close();

                        int addPoint;

                        int addPointOne = String.IsNullOrEmpty(info_addPoint.Text) ? 0:int.Parse(info_addPoint.Text);

                        addPoint = addPointOne;

                        String updateUserAndDate = "";

                        if (cb_update.Checked)
                            updateUserAndDate = "dateInsert = CURRENT_TIMESTAMP, managerName = N'" + GlobalVariable.userName + "',  ";

                        cmd.Parameters.Add("@sumHour", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerHour", SqlDbType.Decimal);
                        cmd.Parameters.Add("@hourToPer", SqlDbType.Decimal);
                        cmd.Parameters.Add("@transportKg", SqlDbType.Decimal);
                        cmd.Parameters.Add("@transportPal", SqlDbType.Int);
                        cmd.Parameters.Add("@sumKm", SqlDbType.Decimal);
                        cmd.Parameters.Add("@sumKmWithOrd", SqlDbType.Int);
                        cmd.Parameters.Add("@sucTkm", SqlDbType.Int);
                        cmd.Parameters.Add("@tarifPerKM", SqlDbType.Decimal);
                        cmd.Parameters.Add("@kmToPer", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tariffPerTrip", SqlDbType.Decimal);
                        cmd.Parameters.Add("@allSum", SqlDbType.Decimal);
                        cmd.Parameters.Add("@addPoint", SqlDbType.Int);
                        cmd.Parameters.Add("@addPointTariff", SqlDbType.Decimal);
                        cmd.Parameters.Add("@dateDocument", SqlDbType.Date);

                        cmd.Parameters["@sumHour"].Value = String.IsNullOrEmpty(arrParameters[0].ToString()) ? "0" : arrParameters[0];
                        cmd.Parameters["@tarifPerHour"].Value = String.IsNullOrEmpty(arrParameters[1].ToString()) ? "0" : arrParameters[1];
                        cmd.Parameters["@hourToPer"].Value = String.IsNullOrEmpty(arrParameters[2].ToString()) ? "0" : arrParameters[2] ;
                        cmd.Parameters["@transportKg"].Value = String.IsNullOrEmpty(arrParameters[3].ToString()) ? "0" : arrParameters[3];
                        cmd.Parameters["@transportPal"].Value = String.IsNullOrEmpty(arrParameters[4].ToString()) ? "0" : arrParameters[4];
                        cmd.Parameters["@sumKm"].Value = String.IsNullOrEmpty(arrParameters[5].ToString()) ? "0" : arrParameters[5];
                        cmd.Parameters["@sumKmWithOrd"].Value = String.IsNullOrEmpty(arrParameters[6].ToString()) ? "0" : arrParameters[6];
                        cmd.Parameters["@sucTkm"].Value = String.IsNullOrEmpty(arrParameters[7].ToString()) ? "0" : arrParameters[7];
                        cmd.Parameters["@tarifPerKM"].Value = String.IsNullOrEmpty(arrParameters[8].ToString()) ? "0" : arrParameters[8];
                        cmd.Parameters["@kmToPer"].Value = String.IsNullOrEmpty(arrParameters[9].ToString()) ? "0" : arrParameters[9];
                        cmd.Parameters["@tariffPerTrip"].Value = String.IsNullOrEmpty(arrParameters[10].ToString()) ? "0" : arrParameters[10];
                        cmd.Parameters["@allSum"].Value = String.IsNullOrEmpty(arrParameters[11].ToString()) ? "0" : arrParameters[11];
                        cmd.Parameters["@dateDocument"].Value = info_dateDoc.Value.ToString("yyyy-MM-dd");
                        cmd.Parameters["@addPoint"].Value = addPoint;
                        cmd.Parameters["@addPointTariff"].Value = String.IsNullOrEmpty(info_addPoint.Text) ? 0 : GlobalVariable.addPerForPoint;
                        cmd.CommandText = "UPDATE dataRegistr SET " +
                            "allHour = @sumHour, " +
                            "tarifPerHour = @tarifPerHour, " +
                            "hourToPay = @hourToPer, " +
                            "transportKg = @transportKg, " +
                            "transportPal = @transportPal, " +
                            "ref = @ref, " +
                            "sunKm = @sumKm, " +
                            "allDistanceWithOrders = @sumKmWithOrd, " +
                            "successTKm = @sucTkm, " +
                            "tarifPerKM = @tarifPerKM, " +
                            "tariffPerTrip = @tariffPerTrip, " +
                            "allSum = @allSum+(@addPoint * @addPointTariff), " +
                            updateUserAndDate +
                            "idTariffType = @isCity, " +
                            "allDistance = @sumKm, " +
                            "addPoint = @addPoint, " +
                            "dateDocument = @dateDocument, " +
                            "isReturns = @isReturns," +
                            "kmToPer = @kmToPer " +
                            "WHERE shiftcode = @shiftcode";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (dgv_mainWindow.CurrentCell.ColumnIndex == dgv_mainWindow.Columns["addressCode"].Index)
                    MessageBox.Show("В поле \"Номер\" могут быть только цифры!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Ошибка сохранения в базу данных! " + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void info_dateDoc_ValueChanged(object sender, EventArgs e)
        {
            if (cb_matrix.Checked)
            {
                GlobalVariable.matrixDate = info_dateDoc.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                GlobalVariable.matrixDate = DateTime.Parse(dateTimePicker_matrixDate.SelectedValue.ToString()).ToString("yyyy-MM-dd");
            }
        }

        private void cb_matrix_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_matrix.Checked)
            {
                dateTimePicker_matrixDate.Enabled = false;
                GlobalVariable.matrixDate = info_dateDoc.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                dateTimePicker_matrixDate.Enabled = true;
                GlobalVariable.matrixDate = DateTime.Parse(dateTimePicker_matrixDate.SelectedValue.ToString()).ToString("yyyy-MM-dd");
            }
        }
        
        private void dgv_mainWindow_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var txtBox = e.Control as TextBox;

            txtBox.TextChanged -= new EventHandler(ItemTxtBox_TextChanged);
            txtBox.TextChanged += new EventHandler(ItemTxtBox_TextChanged);
        }

        void ItemTxtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt = sender as TextBox;

                if (dgv_mainWindow.CurrentCell.ColumnIndex == dgv_mainWindow.Columns["startTime"].Index || dgv_mainWindow.CurrentCell.ColumnIndex == dgv_mainWindow.Columns["finishTime"].Index)
                {
                    if (txt.Text != null && txt.Text.Trim() != "")
                    {
                        if (txt.Text.Length == 2)
                        {
                            txt.Text = txt.Text.Replace(txt.Text, txt.Text + ":");
                            txt.Text = txt.Text.Replace(",", ":");
                            txt.Text = txt.Text.Replace(".", ":");
                            txt.SelectionStart = txt.Text.Length;
                            txt.SelectionLength = 0;
                        }
                        else if (txt.Text.Length == 5)
                        {
                            if (dgv_mainWindow.CurrentCell.ColumnIndex == dgv_mainWindow.Columns["startTime"].Index)
                            {
                                int currRow = dgv_mainWindow.CurrentCell.RowIndex;
                                int nextColumn = dgv_mainWindow.Columns["finishTime"].Index;

                                dgv_mainWindow.CurrentCell = dgv_mainWindow.Rows[currRow].Cells[nextColumn];
                            }
                            else if (dgv_mainWindow.CurrentCell.ColumnIndex == dgv_mainWindow.Columns["finishTime"].Index)
                            {
                                int currRow = dgv_mainWindow.CurrentCell.RowIndex + 1;
                                int nextColumn = dgv_mainWindow.Columns["startTime"].Index;

                                if (dgv_mainWindow.RowCount >= currRow)
                                    dgv_mainWindow.CurrentCell = dgv_mainWindow.Rows[currRow].Cells[nextColumn];
                            }
                        }
                    }
                }
                else if (dgv_mainWindow.CurrentCell.ColumnIndex == dgv_mainWindow.Columns["kg"].Index || dgv_mainWindow.CurrentCell.ColumnIndex == dgv_mainWindow.Columns["distanceDataGridViewTextBoxColumn"].Index || dgv_mainWindow.CurrentCell.ColumnIndex == dgv_mainWindow.Columns["addkm"].Index)
                {
                    if (txt.Text != null && txt.Text.Trim() != "")
                    {
                        txt.Text = txt.Text.Replace(".", ",");
                        txt.SelectionStart = txt.Text.Length;
                        txt.SelectionLength = 0;
                    }
                }
            }
            catch
            { }
        }

        private void списокРЦToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callEditDepotWindow();
        }

        private void константыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callConstantsWindow();
        }

        private void реестрТТНToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callActsWindow();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callUsersWindow();
        }

        private void dgv_mainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                int columnIndex = dgv_mainWindow.CurrentCell.ColumnIndex;
                int rowIndex = dgv_mainWindow.CurrentRow.Index;
                if (columnIndex != dgv_mainWindow.Columns["addressCode"].Index)
                    dgv_mainWindow[columnIndex, rowIndex].Value = DBNull.Value;
            }
            if (e.KeyCode == Keys.F5)
            {
                if (dgv_mainWindow.RowCount >= 1)
                {
                    if (rB_manualy.Checked)
                    {
                        createTruckFroManualy();
                        rB_automatic.Checked = true;
                    }

                    saveUpdateAndReCalc();
                    tb_shiftcode.Text = "";
                    label_saved.Text = "Сохранено!";
                    tb_shiftcode.Focus();
                }
                else
                    MessageBox.Show("Таблица пуста, нет данных для сохранения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void реестрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callRegistryWindow();
        }

        private void реестрТТНВсеПоляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callRegistryReportWindow();
        }

        private void справочникАдресовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callAddressWindow();
        }

        private void help()
        {
            string tempPath = System.IO.Path.GetTempPath() + "TaxManager.chm";
            File.WriteAllBytes(tempPath, TaxManager.Properties.Resources.TaxManager);
            System.Diagnostics.Process.Start(tempPath).WaitForExit();
            File.Delete(tempPath);
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                help();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Updater.exe");
        }

        private void импортРеестраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importRegistr.takeFileForImport();
        }

        public void setRegistrName(String registrName)
        {
            txt_registrName.Text = registrName;
        }

        private void реестрПереданныхТТНToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVariable.registrNumber = txt_registrName.Text;
            callRegistrTTNWindow();
        }

        private void импортТарифовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importRegistr.takeFileForImportTariff();
        }

        private void версияПОToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Версия ПО: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_delCurTTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(info_ttn.Text))
                {
                    MessageBox.Show("ТТН не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            cmd.Parameters.Add("@shiftcode", SqlDbType.VarChar);
                            cmd.Parameters["@shiftcode"].Value = info_ttn.Text;

                            cmd.CommandText = "DELETE FROM dataRegistr WHERE shiftCode = @shiftcode";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM newRegistr WHERE shiftCode = @shiftcode";
                            cmd.ExecuteNonQuery();

                            label_saved.Text = "Удалена!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка btn_delCurTTN_Click " + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void колвоРейсовВСуткахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callReportCountTripInDay();
        }
    }
}
