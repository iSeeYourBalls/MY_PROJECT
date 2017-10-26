using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSU
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
            Login form2 = new Login();
            if (form2.status == true)
            {
                Application.Run(new main());
            }
        }
    }
    class GlobalVariable
    {
        public static string userName { get; set; }
        public static int sh { get; set; }
        public static int homeAddress { get; set; }
        public static int replanOne { get; set; }
        public static int replanTwo { get; set; }
        public static int depot { set; get; }
        public static int tb_interface { set; get; }
        public static int test { set; get; }
        public static int index_order { set; get; }
        public static int index_shift { set; get; }
        public static string edit_order { set; get; }
        public static int edit_address { set; get; }
        public static string edit_sub { set; get; }
        public static string edit_shift { set; get; }
        public static int[] edit_shift_waybill { set; get; }
        public static string divided_order { set; get; }
        public static string nameColumn { set; get; }
        public static string general_date { set; get; }
        public static string connectionString = "Data Source=qwerty;Integrated Security=False;Initial Catalog=qwerty;Persist Security Info=True;User ID=qwerty;Password=qwerty";
    }
    class Phone
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    class checkBoxItem
    {
        public string View { get; set; }
        public object Data { get; set; }

        public override string ToString()
        {
            return this.View;
        }
    }
}
