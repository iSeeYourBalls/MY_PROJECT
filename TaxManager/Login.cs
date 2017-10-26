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

namespace TaxManager
{
 
    public partial class Login : Form
    {
        SqlDataReader dr;
        string windowsUser;

        public bool status = false;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            windowsUser = Environment.UserName.ToString();
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

            if (cb_userWindows.Checked)
            {
                using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                {
                    cn.Open();
                    using (SqlCommand c = new SqlCommand("SELECT * FROM users WHERE login = '" + windowsUser + "' ", cn))
                    {
                        dr = c.ExecuteReader();
                        bool count_row = dr.HasRows;

                        if (count_row == true)
                        {
                            this.Hide();
                            GlobalVariable.userName = login.ToString();
                            if (dr.Read())
                                GlobalVariable.id_user = int.Parse(dr["id"].ToString());
                            Main form3 = new Main();
                            form3.Show();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден!", "Ошибка");
                        }
                    }
                }
            }
            else
            {
                if (login != "" || pass != "")
                {
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        cn.Open();
                        using (SqlCommand c = new SqlCommand("SELECT * FROM users WHERE login = '" + login + "' and pass = '" + pass + "'", cn))
                        {
                            dr = c.ExecuteReader();
                            bool count_row = dr.HasRows;

                            if (count_row == true)
                            {
                                this.Hide();
                                GlobalVariable.userName = login.ToString();
                                if (dr.Read())
                                    GlobalVariable.id_user = int.Parse(dr["id"].ToString());
                                Main form3 = new Main();
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
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
             if(e.KeyCode == Keys.Enter)
                 entry();
        }

        private void cb_userWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_userWindows.Checked)
            {
                txt_login.Enabled = false;
                txt_pass.Enabled = false;

                txt_login.Text = windowsUser;
                txt_pass.Text = "**********";
            }
            else
            {
                txt_login.Enabled = true;
                txt_pass.Enabled = true;
                txt_login.Text = "";
            }
        }
    }
}
