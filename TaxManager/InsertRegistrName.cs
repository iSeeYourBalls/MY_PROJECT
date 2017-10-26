using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaxManager
{
    public partial class InsertRegistrName : Form
    {
        SqlDataReader dr;

        public InsertRegistrName()
        {
            InitializeComponent();
        }

        private void InsertRegistrName_Load(object sender, EventArgs e)
        {

        }

        private void btn_registrSave_Click(object sender, EventArgs e)
        {
            if (!isSetThisRegistr(txt_registrName.Text))
            {
                GlobalVariable.registrNumber = txt_registrName.Text;
                Main main = this.Owner as Main;
                main.setRegistrName(GlobalVariable.registrNumber);
                this.Close();
            }
            else
            {
                MessageBox.Show("Сегодня Вы уже импортировали реестр с таким кодом(" + txt_registrName.Text + "), пожалуйста внесите другой код во избежания задвоений.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isSetThisRegistr(String registrCode)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.Parameters.Add("@registrCode", SqlDbType.NVarChar);
                    cmd.Parameters["@registrCode"].Value = registrCode;
                    cmd.Parameters.Add("@user", SqlDbType.VarChar);
                    cmd.Parameters["@user"].Value = GlobalVariable.userName;
                    cmd.CommandText = "EXEC isSetThisRegistr @registrCode, @user";
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                        return true;
                }
            }
            return false;
        }
    }
}
