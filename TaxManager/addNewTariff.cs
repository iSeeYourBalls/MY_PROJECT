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
    public partial class addNewTariff : Form
    {
        private TextBox[] txtArr = new TextBox[14];
        SqlDataReader dr;

        public addNewTariff()
        {
            InitializeComponent();
        }

        private void addNewTariff_Load(object sender, EventArgs e)
        {
            createArrWithTextBox();

            if (GlobalVariable.editTariff == true)
                fillDataForEdit();
        }

        private void createArrWithTextBox()
        {
            txtArr[0] = txt_code;
            txtArr[1] = txt_fix;
            txtArr[2] = txt_fixRef;
            txtArr[3] = txt_freight;
            txtArr[4] = txt_freightRef;
            txtArr[5] = txt_hour;
            txtArr[6] = txt_hourRef;
            txtArr[7] = txt_identity;
            txtArr[8] = txt_km;
            txtArr[9] = txt_kmRef;
            txtArr[10] = txt_palMax;
            txtArr[11] = txt_palMin;
            txtArr[12] = txt_kgMin;
            txtArr[13] = txt_kgMax;
        }

        private void fillDataForEdit()
        {
            try
            {
                this.Text = "Редактирование тарифа";
                btn_create.Text = "Изменить";

                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = "select TOP 1 *, ISNULL(active,0) as checkBox from tarifComparisonTable where id = @id";
                        cmd.Parameters.AddWithValue("@id", GlobalVariable.idTariff);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtArr[0].Text = dr["typeIdInTariffTable"].ToString();
                            txtArr[1].Text = dr["fixTariff"].ToString();
                            txtArr[2].Text = dr["fixtariffRef"].ToString();
                            txtArr[3].Text = dr["tarifPerKmFreight"].ToString();
                            txtArr[4].Text = dr["tarifPerKmFreightWithRef"].ToString();
                            txtArr[5].Text = dr["tarifInCity"].ToString();
                            txtArr[6].Text = dr["tarifWithRefInCity"].ToString();
                            txtArr[7].Text = dr["typeCodeTariffTable"].ToString();
                            txtArr[8].Text = dr["tarifPerKm"].ToString();
                            txtArr[9].Text = dr["tarifPerKmWithRef"].ToString();
                            txtArr[10].Text = dr["palMax"].ToString();
                            txtArr[11].Text = dr["palMin"].ToString();
                            txtArr[12].Text = dr["kgMin"].ToString();
                            txtArr[13].Text = dr["kgMax"].ToString();
                            txt_comment.Text = dr["comment"].ToString();
                            dateTimePicker_date.Value = DateTime.Parse(dr["dateInsert"].ToString());
                            cb_active.Checked = int.Parse(dr["checkbox"].ToString()) == 0 ? false : true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n fillDataForEdit " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void insertTarif(int active)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        string commandText = "INSERT into tarifComparisonTable (typeIdInTariffTable, typeCodeTariffTable, dateInsert, dateUpdate, id_userModify, [tarifWithRefInCity], [tarifInCity], [tarifPerKmWithRef], [tarifPerKm], [tarifPerKmFreightWithRef], [tarifPerKmFreight], comment, active, fixtariffRef, fixTariff, palMin, palMax, kgMin, kgMax)" +
                        "VALUES (@typeIdInTariffTable, @typeCodeTariffTable, @dateInsert, CURRENT_TIMESTAMP, @id_user, @tarifWithRefInCity, @tarifInCity, @tarifPerKmWithRef, @tarifPerKm, @tarifPerKmFreightWithRef, @tarifPerKmFreight, N'" + txt_comment.Text + "', @active, @fixtariffRef, @fixTariff,@palMin,@palMax,@kgMin,@kgMax)";
                        cmd.Parameters.Add("@typeIdInTariffTable", SqlDbType.Int);
                        cmd.Parameters.Add("@fixTariff", SqlDbType.Decimal);
                        cmd.Parameters.Add("@fixtariffRef", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKmFreight", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKmFreightWithRef", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifInCity", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifWithRefInCity", SqlDbType.Decimal);
                        cmd.Parameters.Add("@typeCodeTariffTable", SqlDbType.VarChar);
                        cmd.Parameters.Add("@tarifPerKm", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKmWithRef", SqlDbType.Decimal);
                        cmd.Parameters.Add("@palMax", SqlDbType.Int);
                        cmd.Parameters.Add("@palMin", SqlDbType.Int);
                        cmd.Parameters.Add("@kgMax", SqlDbType.Int);
                        cmd.Parameters.Add("@kgMin", SqlDbType.Int);
                        cmd.Parameters.Add("@id_user", SqlDbType.Int);
                        cmd.Parameters.Add("@dateInsert", SqlDbType.Date);
                        cmd.Parameters.Add("@active", SqlDbType.Int);

                        cmd.CommandText = commandText;
                        cmd.Parameters["@typeIdInTariffTable"].Value = txtArr[0].Text;
                        cmd.Parameters["@fixTariff"].Value = txtArr[1].Text;
                        cmd.Parameters["@fixtariffRef"].Value = txtArr[2].Text;
                        cmd.Parameters["@tarifPerKmFreight"].Value = txtArr[3].Text;
                        cmd.Parameters["@tarifPerKmFreightWithRef"].Value = txtArr[4].Text;
                        cmd.Parameters["@tarifInCity"].Value = txtArr[5].Text;
                        cmd.Parameters["@tarifWithRefInCity"].Value = txtArr[6].Text;
                        cmd.Parameters["@typeCodeTariffTable"].Value = txtArr[7].Text;
                        cmd.Parameters["@tarifPerKm"].Value = txtArr[8].Text;
                        cmd.Parameters["@tarifPerKmWithRef"].Value = txtArr[9].Text;
                        cmd.Parameters["@palMax"].Value = txtArr[10].Text;
                        cmd.Parameters["@palMin"].Value = txtArr[11].Text;
                        cmd.Parameters["@kgMin"].Value = txtArr[12].Text;
                        cmd.Parameters["@kgMax"].Value = txtArr[13].Text;
                        cmd.Parameters["@id_user"].Value = GlobalVariable.id_user;
                        cmd.Parameters["@dateInsert"].Value = dateTimePicker_date.Value.ToString("yyyy-MM-dd");
                        cmd.Parameters["@active"].Value = active;
                        cmd.ExecuteNonQuery();

                        var result = MessageBox.Show("Добавлено, очистить форму?", "Сохранено", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            for (int i = 0; i < txtArr.Length; i++)
                            {
                                txtArr[i].Text = "";
                                txtArr[i].BackColor = Color.White;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n insertTarif " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateTarif(int active)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        string commandText = "UPDATE tarifComparisonTable SET " +
                            "typeIdInTariffTable = @typeIdInTariffTable, " +
                            "typeCodeTariffTable = @typeCodeTariffTable, " +
                            "dateInsert = @dateInsert, " +
                            "dateUpdate = CURRENT_TIMESTAMP, " +
                            "id_userModify = @id_user, " +
                            "tarifWithRefInCity = @tarifWithRefInCity, " +
                            "tarifInCity = @tarifInCity, " +
                            "tarifPerKmWithRef = @tarifPerKmWithRef, " +
                            "tarifPerKm = @tarifPerKm, " +
                            "tarifPerKmFreightWithRef = @tarifPerKmFreightWithRef, " +
                            "tarifPerKmFreight = @tarifPerKmFreight, " +
                            "comment = N'" + txt_comment.Text + "', " +
                            "active = @active, " +
                            "fixtariffRef = @fixtariffRef, " +
                            "fixTariff = @fixTariff, " +
                            "palMin = @palMin, " +
                            "kgMin = @kgMin, " +
                            "kgMax = @kgMax, " +
                            "palMax = @palMax WHERE id = @id";


                        cmd.Parameters.Add("@typeIdInTariffTable", SqlDbType.Int);
                        cmd.Parameters.Add("@fixTariff", SqlDbType.Decimal);
                        cmd.Parameters.Add("@fixtariffRef", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKmFreight", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKmFreightWithRef", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifInCity", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifWithRefInCity", SqlDbType.Decimal);
                        cmd.Parameters.Add("@typeCodeTariffTable", SqlDbType.VarChar);
                        cmd.Parameters.Add("@tarifPerKm", SqlDbType.Decimal);
                        cmd.Parameters.Add("@tarifPerKmWithRef", SqlDbType.Decimal);
                        cmd.Parameters.Add("@palMax", SqlDbType.Int);
                        cmd.Parameters.Add("@palMin", SqlDbType.Int);
                        cmd.Parameters.Add("@kgMax", SqlDbType.Int);
                        cmd.Parameters.Add("@kgMin", SqlDbType.Int);
                        cmd.Parameters.Add("@id_user", SqlDbType.Int);
                        cmd.Parameters.Add("@dateInsert", SqlDbType.Date);
                        cmd.Parameters.Add("@active", SqlDbType.Int);
                        cmd.Parameters.Add("@id", SqlDbType.Int);

                        cmd.CommandText = commandText;
                        cmd.Parameters["@typeIdInTariffTable"].Value = txtArr[0].Text;
                        cmd.Parameters["@fixTariff"].Value = txtArr[1].Text;
                        cmd.Parameters["@fixtariffRef"].Value = txtArr[2].Text;
                        cmd.Parameters["@tarifPerKmFreight"].Value = txtArr[3].Text;
                        cmd.Parameters["@tarifPerKmFreightWithRef"].Value = txtArr[4].Text;
                        cmd.Parameters["@tarifInCity"].Value = txtArr[5].Text;
                        cmd.Parameters["@tarifWithRefInCity"].Value = txtArr[6].Text;
                        cmd.Parameters["@typeCodeTariffTable"].Value = txtArr[7].Text;
                        cmd.Parameters["@tarifPerKm"].Value = txtArr[8].Text;
                        cmd.Parameters["@tarifPerKmWithRef"].Value = txtArr[9].Text;
                        cmd.Parameters["@palMax"].Value = txtArr[10].Text;
                        cmd.Parameters["@palMin"].Value = txtArr[11].Text;
                        cmd.Parameters["@kgMin"].Value = txtArr[12].Text;
                        cmd.Parameters["@kgMax"].Value = txtArr[13].Text;
                        cmd.Parameters["@id_user"].Value = GlobalVariable.id_user;
                        cmd.Parameters["@dateInsert"].Value = dateTimePicker_date.Value.ToString("yyyy-MM-dd");
                        cmd.Parameters["@active"].Value = active;
                        cmd.Parameters["@id"].Value = GlobalVariable.idTariff;
                        cmd.ExecuteNonQuery();

                        var result = MessageBox.Show("Сохранено, закрыть форму?", "Сохранено", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            GlobalVariable.editTariff = false;
                            this.Close();
                        }
                        else
                        {
                            defaultBackColorTextBox();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n updateTarif " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createNewTariff()
        {
            try
            {
                if (isAllTxtBoxFull())
                {
                    int active = 0;
                    if (cb_active.Checked)
                        active = 1;

                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        cn.Open();

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = cn;
                            if (GlobalVariable.editTariff)
                                updateTarif(active);
                            else
                                insertTarif(active);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Обязательные поля должны быть заполнены, значения должны быть цифрой!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reqTxtToRedColor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n createNewTariff " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isAllTxtBoxFull()
        {
            for (int i = 0; i < txtArr.Length; i++)
            {
                if (String.IsNullOrEmpty(txtArr[i].Text) || !isNumeric(txtArr[i].Text) && !txtArr[i].Equals(txt_identity)) return false;
            }
            return true;
        }

        private void reqTxtToRedColor()
        {
            for (int i = 0; i < txtArr.Length; i++)
            {
                if (String.IsNullOrEmpty(txtArr[i].Text) || !isNumeric(txtArr[i].Text) && !txtArr[i].Equals(txt_identity)) txtArr[i].BackColor = Color.Bisque; else txtArr[i].BackColor = Color.White;
            }
        }

        private void defaultBackColorTextBox()
        {
            for (int i = 0; i < txtArr.Length; i++)
            {
                txtArr[i].BackColor = Color.White;
            }
        }

        private bool isNumeric(String someString)
        {
            int res;
            double result;
            bool isInt = Int32.TryParse(someString, out res);
            bool isDouble = double.TryParse(someString, out result);
            if (!isInt)
                return isDouble;
            else
                return isInt;
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            createNewTariff();
        }

        private void addNewTariff_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.editTariff = false;
            Tariff tariff = this.Owner as Tariff;
            tariff.updateTableTariff();
        }

    }
}
