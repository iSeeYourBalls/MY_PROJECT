using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaxManager
{
    class ImportCSV
    {
        private DataTable GetDataOrderFromFile(string FileName)
        {
            using (DataTable importedData = new DataTable())
            {
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
        }
// Импорт реестра
        public void takeFileForImport()
        {
            try
            {
                callinsertRegistrNameWindow();

                if (String.IsNullOrEmpty(GlobalVariable.registrNumber))
                {
                    MessageBox.Show("Имя регистра не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OpenFileDialog ofd = new OpenFileDialog(); ofd.DefaultExt = ".csv"; ofd.Filter = "Comma Separated (*.csv)|*.csv";
                    var result = ofd.ShowDialog();
                    if (result != DialogResult.Cancel)
                    {
                        string FileName = ofd.FileName;
                        DataTable imported_data = GetDataOrderFromFile(FileName);

                        if (imported_data == null)
                            return;

                        if (SaveImportDataOrderToDatabase(imported_data))
                        {
                            MessageBox.Show("Импорт реестра завершен!");
                            GlobalVariable.registrNumber = "";
                        }
                        FileName = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n btnimportOrder_Click" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SaveImportDataOrderToDatabase(DataTable imported_data)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.Parameters.Add("@depotName", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@truckNumber", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@dateTTN", SqlDbType.Date);
                        cmd.Parameters.Add("@shiftcode", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@countTTN", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@trip", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@subcontractor", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@registrNumber", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@userName", SqlDbType.VarChar);

                        cmd.Parameters["@registrNumber"].Value = GlobalVariable.registrNumber;
                        cmd.Parameters["@userName"].Value = GlobalVariable.userName;

                        foreach (DataRow importRow in imported_data.Rows)
                        {
                            cmd.Parameters["@depotName"].Value = importRow[1];
                            cmd.Parameters["@truckNumber"].Value = importRow[2];
                            cmd.Parameters["@dateTTN"].Value = importRow[5];
                            cmd.Parameters["@shiftcode"].Value = importRow[6];
                            cmd.Parameters["@countTTN"].Value = importRow[7];
                            cmd.Parameters["@trip"].Value = importRow[8];
                            cmd.Parameters["@subcontractor"].Value = importRow[9];
                            cmd.CommandText = "INSERT INTO registrNonTaxTTN (depotName, truckNumber, dateTTN, shiftcode, countTTN, trip, subcontractor, registrNumber, dateInsret, userName) " +
                                                            "VALUES (@depotName, @truckNumber, @dateTTN, @shiftcode, @countTTN, @trip, @subcontractor, @registrNumber, CAST(CURRENT_TIMESTAMP as date), @userName)";
                            cmd.ExecuteNonQuery();
                        }
                     
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n SaveImportDataOrderToDatabase" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void callinsertRegistrNameWindow()
        {
            using (InsertRegistrName f10 = new InsertRegistrName())
            {
                f10.ShowDialog(Main.ActiveForm);
            }
        }
//Импорт тарифов
        public void takeFileForImportTariff()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog(); ofd.DefaultExt = ".csv"; ofd.Filter = "Comma Separated (*.csv)|*.csv";
                var result = ofd.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    string FileName = ofd.FileName;
                    DataTable imported_data = GetDataOrderFromFile(FileName);

                    if (imported_data == null)
                        return;

                    if (SaveImportDataTariffToDatabase(imported_data))
                    {
                        MessageBox.Show("Импорт тарифов завершен!");
                    }
                    FileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n btnimportOrder_Click" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SaveImportDataTariffToDatabase(DataTable imported_data)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters.Add("@code", SqlDbType.VarChar);
                        cmd.Parameters.Add("@date", SqlDbType.Date);
                        cmd.Parameters.Add("@tarifWithRefInCity", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifInCity", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKmWithRef", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKm", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKmFreight", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKmFreightWithRef", SqlDbType.Decimal);
                        cmd.Parameters.Add("@fixTariffRef", SqlDbType.Decimal);
                        cmd.Parameters.Add("@fixTariff", SqlDbType.Decimal);
                        cmd.Parameters.Add("@comment", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@userName", SqlDbType.Int);
                        cmd.Parameters["@userName"].Value = GlobalVariable.id_user;

                        foreach (DataRow importRow in imported_data.Rows)
                        {
                            cmd.Parameters["@id"].Value = importRow["id"];
                            cmd.Parameters["@code"].Value = importRow["code"];
                            cmd.Parameters["@date"].Value = importRow["date"];
                            cmd.Parameters["@tarifWithRefInCity"].Value = importRow["tarifWithRefInCity"];
                            cmd.Parameters["@tarifInCity"].Value = importRow["tarifInCity"];
                            cmd.Parameters["@tarifPerKmWithRef"].Value = importRow["tarifPerKmWithRef"];
                            cmd.Parameters["@tarifPerKm"].Value = importRow["tarifPerKm"];
                            cmd.Parameters["@tarifPerKmFreight"].Value = importRow["tarifPerKmFreight"];
                            cmd.Parameters["@tarifPerKmFreightWithRef"].Value = importRow["tarifPerKmFreightWithRef"];
                            cmd.Parameters["@fixTariffRef"].Value = importRow["fixTariffRef"];
                            cmd.Parameters["@fixTariff"].Value = importRow["fixTariff"];
                            cmd.Parameters["@comment"].Value = importRow["comment"];
                            cmd.CommandText = "EXEC insertNewTariffForImport @id, @code, @date, @tarifWithRefInCity, @tarifInCity, @tarifPerKmWithRef, @tarifPerKm, @tarifPerKmFreight, @tarifPerKmFreightWithRef, @fixTariffRef, @fixTariff, @comment, @userName";
                            cmd.ExecuteNonQuery();
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n SaveImportDataOrderToDatabase" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
