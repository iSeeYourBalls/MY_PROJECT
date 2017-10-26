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
    public partial class divided : Form
    {
        SqlDataReader dr;
        public divided()
        {
            InitializeComponent();
        }

        private void btn_divided_Click(object sender, EventArgs e)
        {
            int res;
            bool isInt = Int32.TryParse(txt_divided.Text, out res);
            if (isInt == true)
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT * FROM orders WHERE order_number = '" + GlobalVariable.edit_order + "'";
                        cmd.ExecuteNonQuery();
                        dr = cmd.ExecuteReader();
                        string address = "";
                        string kg = "";
                        string pal = "";
                        string com = "";
                        string product = "";
                        string dateNow = "";
                        int orderStatus = 0;
                        while (dr.Read())
                        {
                            address = dr["address"].ToString();
                            kg = dr["kg"].ToString();
                            pal = dr["pal"].ToString();
                            com = dr["comment"].ToString();
                            product = dr["product"].ToString();
                            dateNow = dr["datework"].ToString();
                            orderStatus = Convert.ToInt16(dr["orderStatus"].ToString());
                        }
                        dr.Close();

                        if (res < Convert.ToInt32(pal) & res != 0)
                        {
                            // сколько остается паллет в основном заказе
                            int howLastPal = Convert.ToInt32(pal) - res;

                            if (howLastPal != 0)
                            {
                                // сколько отделяем кг
                                int howGetKg = Convert.ToInt32(kg) / Convert.ToInt32(pal) * res;
                                //сколько остается кг в основном заказе
                                int howLastKg = Convert.ToInt32(kg) - howGetKg;
                                DateTime creat_date = Convert.ToDateTime(dateNow);
                                DateTime ToDay = DateTime.Now;
                                System.Random rand = new System.Random(ToDay.Millisecond);
                                int shiftcode = rand.Next(100000, 10000000);
                                string id_dt = DateTime.Now.ToString("yyyyMMddHHmmssms");
                                string order_number = shiftcode + id_dt;

                                cmd.CommandText = "UPDATE orders SET kg = '" + howLastKg + "', pal = '" + howLastPal + "' WHERE order_number = '" + GlobalVariable.edit_order + "'";
                                cmd.ExecuteNonQuery();
                                cmd.CommandText = "INSERT into orders (address,product,kg,pal,comment,datework, orderStatus) VALUES('" + address + "','" + product + "','" + howGetKg + "','" + res + "','" + com + "','" + creat_date.ToString("yyyy-MM-dd") + "', '" + orderStatus + "');";
                                cmd.ExecuteNonQuery();

                                update_order_main();
                            }
                            else
                            {
                                MessageBox.Show("Укажите сколько нужно паллет отделить от заказа, указанное вами значение не верное!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Указанное значение больше чем существующие кол-во паллет!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Указывать можно только целые числа!", "Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void divided_Load(object sender, EventArgs e)
        {

        }
        private void update_order_main()
        {
            main m = this.Owner as main;
            m.update_orders("");
            GlobalVariable.edit_order = null;
            this.Close();
        }
    }
}
