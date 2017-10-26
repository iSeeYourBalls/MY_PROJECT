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
 
    public partial class Login : Form
    {
        SqlDataReader dr;

        public bool status = false;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_vhod_Click(object sender, EventArgs e)
        {
            entry();
        }
        private void entry()
        {
            string login = txt_login.Text;
            string pass = txt_pass.Text;
            if (login != "" || pass != "")
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();
                    using (SqlCommand c = new SqlCommand("SELECT * FROM users WHERE userName = '" + login + "' and pass = '" + pass + "'", cn))
                    {
                        dr = c.ExecuteReader();
                        bool count_row = dr.HasRows;
                        cn.Close();
                        if (count_row == true)
                        {
                            this.Hide();
                            GlobalVariable.userName = login.ToString();
                            main form3 = new main();
                            form3.Show();
                        }
                        else
                        {
                            MessageBox.Show("Логин или пароль введен не верно!", "Ошибка");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка");
            }
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
             if(e.KeyCode == Keys.Enter)
                 entry();
        }
    }
}
