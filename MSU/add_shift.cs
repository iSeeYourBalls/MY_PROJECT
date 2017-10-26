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
    public partial class add_shift : Form
    {
        SqlDataReader dr;
        public add_shift()
        {
            InitializeComponent();
        }

        private void add_shift_Load(object sender, EventArgs e)
        {
            if (GlobalVariable.edit_shift == null || GlobalVariable.edit_shift == "0")
            {
                this.Text = "Создание смены";
                string where = "from subcontractor";
                string whereHome = ",case when addressCode = '"+GlobalVariable.depot+"' then 1 else 0 end 's' from address order by s desc";
                combobox_upload(where);
                combobox_upload_homeaddress(whereHome);
            }
            else
            {
                this.Text = "Редактирование смены";
                edit_shift_form();
            }

        }
        private void new_shift()
        {
            try
            {
                DateTime creat_date = Convert.ToDateTime(dtp_shift.Text);
                int freight;
                if (ch_freight.Checked == true)
                {
                    freight = 1;
                }
                else
                {
                    freight = 0;
                }
                if (txt_truck_number.Text != "" || txt_cap_kg.Text != "" || txt_cap_pal.Text != "")
                {
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            cmd.CommandText = "INSERT into shift (truck,trailer,capacityKg,capacityPal,comment,datework,driver,subcontractor,freight,status, tripNumber, homeAddress) VALUES('" + txt_truck_number.Text + "','" + txt_trailer_number.Text + "','" + txt_cap_kg.Text + "','" + txt_cap_pal.Text + "', N'" + txt_shift_com.Text + "','" + creat_date.ToString("yyyy-MM-dd") + "', N'" + txt_DriverName.Text + "','" + cb_sub.SelectedValue.ToString() + "','" + freight + "', '1', '1', '" + homeaddress.SelectedValue.ToString() + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    var question = MessageBox.Show("Добавлено. Очистить форму и продолжить добавлять?", "Сохранено", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (question == DialogResult.Yes)
                    {
                        txt_truck_number.Text = "";
                        txt_cap_kg.Text = "";
                        txt_cap_pal.Text = "";
                        txt_shift_com.Text = "";
                        txt_DriverName.Text = "";
                        txt_trailer_number.Text = "";
                    }
                    else
                    {
                        update_shift_main();
                    }
                }
                else
                {
                    MessageBox.Show("Поля: номер магазина, кг, паллет должны быть заполнены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void update_shift_main()
        {
            main m = this.Owner as main;
            m.update_shift();
            GlobalVariable.edit_shift = null;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (GlobalVariable.edit_shift == null || GlobalVariable.edit_shift == "0")
            {
                new_shift();
            }
            else
            {
                update_shift();
            }
            GlobalVariable.homeAddress = int.Parse(homeaddress.SelectedValue.ToString());
            Cursor = Cursors.Default;
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
                        cmd.CommandText = "select * " + where + "";
                        DataTable subcontractor = new DataTable();
                        subcontractor.Load(cmd.ExecuteReader());
                        cb_sub.DataSource = subcontractor;
                        cb_sub.DisplayMember = "contactName";
                        cb_sub.ValueMember = "contact_externalId";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void combobox_upload_homeaddress(string whereHome)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "select * " + whereHome + "";
                        DataTable address = new DataTable();
                        address.Load(cmd.ExecuteReader());
                        homeaddress.DataSource = address;
                        homeaddress.DisplayMember = "addressName";
                        homeaddress.ValueMember = "addressCode";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void add_shift_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.edit_shift = null;
            main m = this.Owner as main;
            m.update_shift();
            m.update_action();
        }
        private void edit_shift_form()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT * FROM shift WHERE shiftcode = '" + GlobalVariable.edit_shift + "'";
                        cmd.ExecuteNonQuery();
                        dr = cmd.ExecuteReader();
                        string product = "";
                        string dateNow = "";
                        string freight = "";
                        string startInstantString = "";
                        string finishInstantString = "";
                        if (dr.Read())
                        {
                            txt_truck_number.Text = dr["truck"].ToString();
                            txt_cap_kg.Text = dr["capacityKg"].ToString();
                            txt_cap_pal.Text = dr["capacityPal"].ToString();
                            txt_shift_com.Text = dr["comment"].ToString();
                            txt_DriverName.Text = dr["driver"].ToString();
                            txt_trailer_number.Text = dr["trailer"].ToString();
                            product = dr["subcontractor"].ToString();
                            dateNow = dr["datework"].ToString();
                            freight = dr["freight"].ToString();
                            startInstantString = dr["startInstant"].ToString();
                            finishInstantString = dr["finishInstant"].ToString();
                        }
                        dr.Close();

                        if (freight == "1")
                        {
                            ch_freight.Checked = true;
                        }
                        string where = ",case when contact_externalId = '" + product + "' then 1 else 0 end 's' from subcontractor order by s desc";
                        combobox_upload(where);

                        string whereHome = ",case when addressCode = (select top 1 homeAddress from shift where shift.shiftcode = '" + GlobalVariable.edit_shift + "') then 1 else 0 end 's' from address order by s desc";
                        combobox_upload_homeaddress(whereHome);

                        dtp_shift.Text = dateNow;
                        if (!String.IsNullOrEmpty(startInstantString) || !String.IsNullOrEmpty(finishInstantString))
                        {
                            startInstant.Value = Convert.ToDateTime(startInstantString);
                            finishInstant.Value = Convert.ToDateTime(finishInstantString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_shift()
        {
            try
            {
                int freight;
                if (ch_freight.Checked == true)
                {
                    freight = 1;
                }
                else
                {
                    freight = 0;
                }
                DateTime creat_date = Convert.ToDateTime(dtp_shift.Text);
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "UPDATE shift SET truck='" + txt_truck_number.Text + "', capacityKg = '" + txt_cap_kg.Text + "', capacityPal = '" + txt_cap_pal.Text + "', subcontractor = '" + cb_sub.SelectedValue.ToString() + "', datework = '" + creat_date.ToString("yyyy-MM-dd") + "', comment = N'" + txt_shift_com.Text + "', trailer = '" + txt_trailer_number.Text + "', driver = N'" + txt_DriverName.Text + "', freight='" + freight + "', homeAddress = '" + homeaddress.SelectedValue.ToString() + "', startInstant = '" + DateTime.Now.ToString("yyyy-MM-dd") + " " + startInstant.Value.ToString("HH:mm") + "', finishinstant = '" + DateTime.Now.ToString("yyyy-MM-dd") + " " + finishInstant.Value.ToString("HH:mm") + "' WHERE shiftcode = '" + GlobalVariable.edit_shift + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET start = '" + homeaddress.SelectedValue.ToString() + "' WHERE shiftcode = '" + GlobalVariable.edit_shift + "' and start_finish = '1' and sort = '1'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE action SET finish = '" + homeaddress.SelectedValue.ToString() + "' WHERE shiftcode = '" + GlobalVariable.edit_shift + "' and start_finish = (select top 1 start_finish from action WHERE shiftcode = '" + GlobalVariable.edit_shift + "' order by CAST(start_finish as int) desc) and sort = (select top 1 sort from action WHERE shiftcode = '" + GlobalVariable.edit_shift + "' order by CAST(sort as int) desc)";
                        cmd.ExecuteNonQuery();
                    }
                }
                update_shift_main();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
