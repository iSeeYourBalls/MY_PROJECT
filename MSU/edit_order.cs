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
    public partial class edit_order : Form
    {
        SqlDataReader dr;

        public string palcheck = "";

        public edit_order()
        {
            InitializeComponent();
        }

        private void edit_order_Load(object sender, EventArgs e)
        {
            string[] arrOrderStatus = {"План", "Факт", "Долг"};
            cbEditOrderStatus.Items.AddRange(arrOrderStatus);

            if (GlobalVariable.edit_order == null || GlobalVariable.edit_order == "0")
            {
                this.Text = "Создание заказа";
                string where = "from product";
                combobox_upload(where);
                btn_save.Text = "Создать заказ";
            }
            else
            {
                this.Text = "Редактирование заказа";
                btn_save.Text = "Сохранить";
                edit_orders();
            }
        }
        private void new_order()
        {
            DateTime ToDay = DateTime.Now;
            System.Random rand = new System.Random(ToDay.Millisecond);
            int shiftcode = rand.Next(100000, 10000000);
            string id_dt = DateTime.Now.ToString("yyyyMMddHHmmssms");
            string order_number = shiftcode+id_dt;
            DateTime creat_date = Convert.ToDateTime(dtp_order.Text);
            
            int statusOrder = cbEditOrderStatus.SelectedIndex;

            if (txt_address.Text != "" || txt_kg.Text != "" || txt_pal.Text != "")
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "INSERT into orders (address,product,kg,pal,comment,datework, orderStatus) VALUES('" + txt_address.Text + "','" + cb_product.SelectedValue.ToString() + "','" + txt_kg.Text + "','" + txt_pal.Text + "',N'" + txt_com.Text + "','" + creat_date.ToString("yyyy-MM-dd") + "', '" + statusOrder + "')";
                        cmd.ExecuteNonQuery();
                    }
                }
                var question = MessageBox.Show("Добавлено. Очистить форму и продолжить добавлять?", "Сохранено",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (question == DialogResult.Yes)
                {
                    txt_address.Text = "";
                    txt_com.Text = "";
                    txt_kg.Text = "";
                    txt_pal.Text = "";
                }
                else
                {
                    update_order_main();
                }
            }
            else
            {
                MessageBox.Show("Поля: номер магазина, кг, паллет должны быть заполнены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void edit_orders()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT * FROM orders WHERE order_number = '" + GlobalVariable.edit_order + "'";
                    dr = cmd.ExecuteReader();
                    string product = "";
                    string dateNow = "";
                    int statusOrder = 0;
                    if (dr.Read())
                    {
                        txt_address.Text = dr["address"].ToString();
                        txt_kg.Text = dr["kg"].ToString();
                        txt_pal.Text = dr["pal"].ToString();
                        palcheck = dr["pal"].ToString();
                        txt_com.Text = dr["comment"].ToString();
                        product = dr["product"].ToString();
                        dateNow = dr["datework"].ToString();
                        statusOrder = Convert.ToInt16(dr["orderStatus"].ToString());
                    }
                    cn.Close();

                    cbEditOrderStatus.SelectedIndex = statusOrder;

                    string where = ",case when productCode = '" + product + "' then 1 else 0 end 's' from product order by s desc";
                    combobox_upload(where);
                    dtp_order.Text = dateNow;
                }
            }
        }
        private void update_order()
        {
            int statusOrder = cbEditOrderStatus.SelectedIndex;

            DateTime creat_date = Convert.ToDateTime(dtp_order.Text);
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "UPDATE orders SET address='" + txt_address.Text + "', kg = '" + txt_kg.Text + "', pal = '" + txt_pal.Text + "', product = '" + cb_product.SelectedValue.ToString() + "', datework = '" + creat_date.ToString("yyyy-MM-dd") + "', comment = N'" + txt_com.Text + "', orderStatus = '" + statusOrder + "' WHERE order_number = '" + GlobalVariable.edit_order + "'";
                    cmd.ExecuteNonQuery();
                }
            }
            update_order_main();
            
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
                        cmd.CommandText = "select productCode, productName " + where + "";
                        DataTable product = new DataTable();
                        product.Load(cmd.ExecuteReader());
                        cb_product.DataSource = product;
                        cb_product.DisplayMember = "productName";
                        cb_product.ValueMember = "productCode";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (GlobalVariable.edit_order == null || GlobalVariable.edit_order == "0")
            {
                new_order();
            }
            else
            {
                update_order();
            }
            
        }
        private void update_order_main()
        {
            main m = this.Owner as main;
            m.update_orders("");
            GlobalVariable.edit_order = null;
            this.Close();
        }

        private void edit_order_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.edit_order = null;
            main m = this.Owner as main;
            m.update_orders("");
            m.update_shift();
            m.update_action();
        }
    }
}
