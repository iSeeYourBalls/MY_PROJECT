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
    public partial class add_subcontractor : Form
    {
        SqlDataReader dr;

        public add_subcontractor()
        {
            InitializeComponent();
        }

        private void add_subcontractor_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(GlobalVariable.edit_sub))
                edit();
        }

        private void update_main()
        {
            subcontractor m = this.Owner as subcontractor;
            m.updateSub();
            this.Close();
        }

        private void edit()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT contact_externalId, contactName FROM subcontractor WHERE contact_externalId = '" + GlobalVariable.edit_sub + "'";
                    dr = cmd.ExecuteReader();
                    string code = "";
                    string name = "";
                    while (dr.Read())
                    {
                        code = dr[0].ToString();
                        name = dr[1].ToString();
                    }
                    dr.Close();

                    txtCode.Text = code;
                    txtName.Text = name;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(GlobalVariable.edit_sub))
            {
                string str = txtCode.Text;
                Int64 num;
                bool isNum = Int64.TryParse(str, out num);
                if (isNum)
                {
                    if (!String.IsNullOrEmpty(txtName.Text))
                    {
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;
                                cmd.CommandText = "UPDATE subcontractor SET contact_externalId = N'" + txtCode.Text + "', contactName = N'" + txtName.Text + "' WHERE contact_externalId = '" + GlobalVariable.edit_sub + "';";
                                cmd.ExecuteNonQuery();
                                cn.Close();

                                GlobalVariable.edit_sub = "";
                                MessageBox.Show("Сохранено!", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        update_main();
                    }
                    else
                    {
                        MessageBox.Show("Поле \"Название\" не должно быть пустым!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Поле код должно быть числовым значением!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string str = txtCode.Text;
                Int64 num;
                bool isNum = Int64.TryParse(str, out num);
                if (isNum)
                {
                    if (!String.IsNullOrEmpty(txtName.Text))
                    {
                        using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cn.Open();
                                cmd.Connection = cn;
                                cmd.CommandText = "SELECT count(*) FROM subcontractor WHERE contact_externalId = '" + num + "'";
                                dr = cmd.ExecuteReader();
                                int countRow = 0;
                                while (dr.Read())
                                {
                                    countRow = Convert.ToInt16(dr[0].ToString());
                                }
                                dr.Close();

                                if (countRow == 0)
                                {
                                    cmd.CommandText = "INSERT into subcontractor (contact_externalId, contactName) VALUES(N'" + txtCode.Text + "', N'" + txtName.Text + "');";
                                    cmd.ExecuteNonQuery();

                                    var result = MessageBox.Show("Добавлено, продолжить?", "Добавлено!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        txtCode.Text = "";
                                        txtName.Text = "";
                                    }
                                    else
                                    {
                                        update_main();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Подрядчик с кодом " + num + " уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Поле \"Название\" не должно быть пустым!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Поле код должно быть числовым значением!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void add_subcontractor_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.edit_sub = "";
            subcontractor m = this.Owner as subcontractor;
            m.updateSub();
        }
    }
}
