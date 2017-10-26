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
    public partial class newAddress : Form
    {
        SqlDataReader dr;
        public newAddress()
        {
            InitializeComponent();
        }

        private void newAddress_Load(object sender, EventArgs e)
        {
            if (GlobalVariable.edit_address == 0)
            {
                this.Text = "Создание адреса";
                string where = "from addresskind";
                combobox_upload(where);
                btn_saveNewAddress.Text = "Создать адрес";
            }
            else
            {
                this.Text = "Редактирование адреса";
                btn_saveNewAddress.Text = "Сохранить";
                edit_address();
            }
        }
        private void insertAddress()
        {
            if (txt_AddressName.Text != "" & txt_AdressCode.Text != "" & txt_cityAddress.Text != "")
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT count(*) FROM address WHERE addresscode = '" + txt_AdressCode.Text + "'";
                        dr = cmd.ExecuteReader();
                        int count = 0;
                        if (dr.Read())
                        {
                            count = Convert.ToInt16(dr[0].ToString());
                        }
                        dr.Close();

                        if (count == 0)
                        {
                            try
                            {
                                cmd.CommandText = "INSERT into address (addresscode, addressname, cityname,streetname,doornumber,phone,email, id_addresskind, comment) VALUES('" + txt_AdressCode.Text + "', N'" + txt_AddressName.Text + "', N'" + txt_cityAddress.Text + "', N'" + txt_streetName.Text + "', N'" + txt_numberHouse.Text + "', N'" + txt_Phone.Text + "', N'" + txt_Email.Text + "', N'" + cb_addressKind.SelectedValue.ToString() + "', N'" + txt_addressCom.Text + "')";
                                cmd.ExecuteNonQuery();

                                var result = MessageBox.Show("Адресс добавлен, продолжить добавление?", "Готово", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result == DialogResult.Yes)
                                {
                                    txt_AddressName.Text = "";
                                    txt_AdressCode.Text = "";
                                    txt_cityAddress.Text = "";
                                    txt_Email.Text = "";
                                    txt_numberHouse.Text = "";
                                    txt_Phone.Text = "";
                                    txt_streetName.Text = "";
                                }
                                else
                                {
                                    address m = this.Owner as address;
                                    m.Update();
                                    GlobalVariable.edit_address = 0;
                                    this.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error\n insertAddress" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Адрес " + txt_AdressCode.Text + " уже существует!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Обязательные поля должны быть заполнены!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void combobox_upload(string where)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "select id_addresskind, addresskindname " + where + "";
                        DataTable addresskind = new DataTable();
                        addresskind.Load(cmd.ExecuteReader());
                        cb_addressKind.DataSource = addresskind;
                        cb_addressKind.DisplayMember = "addresskindname";
                        cb_addressKind.ValueMember = "id_addresskind";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n combobox_upload" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void edit_address()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT * FROM address WHERE id_address = '" + GlobalVariable.edit_address + "'";
                        dr = cmd.ExecuteReader();
                        int id_addressKind = 0;
                        if (dr.Read())
                        {
                            txt_AdressCode.Text = dr["addresscode"].ToString();
                            txt_AddressName.Text = dr["addressName"].ToString();
                            txt_cityAddress.Text = dr["cityName"].ToString();
                            txt_streetName.Text = dr["streetName"].ToString();
                            txt_numberHouse.Text = dr["doorNumber"].ToString();
                            txt_Phone.Text = dr["phone"].ToString();
                            txt_Email.Text = dr["email"].ToString();
                            txt_addressCom.Text = dr["comment"].ToString();
                            id_addressKind = Convert.ToInt16(dr["id_addresskind"].ToString());
                        }
                        string where = ",case when id_addresskind = '" + id_addressKind + "' then 1 else 0 end 's' from addresskind order by s desc";
                        combobox_upload(where);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n edit_address" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_saveNewAddress_Click(object sender, EventArgs e)
        {
            if (GlobalVariable.edit_address == 0)
            {
                insertAddress();
            }
            else
            {
                updateAddress();
            }

        }
        private void updateAddress()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "UPDATE address SET addresscode=N'" + txt_AdressCode.Text + "', addressname = N'" + txt_AddressName.Text + "', cityName = N'" + txt_cityAddress.Text + "', id_addresskind = N'" + cb_addressKind.SelectedValue.ToString() + "', streetname = N'" + txt_streetName.Text + "', doorNumber = N'" + txt_numberHouse.Text + "', phone = N'" + txt_Phone.Text + "', email = N'" + txt_Email.Text + "', comment = N'" + txt_addressCom.Text + "' WHERE id_address = '" + GlobalVariable.edit_address + "'";
                    cmd.ExecuteNonQuery();
                }
            }
            address m = this.Owner as address;
            m.Update();
            GlobalVariable.edit_address = 0;
            this.Close();
        }

        private void newAddress_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.edit_address = 0;
            address m = this.Owner as address;
            m.update();
        }
    }
}
