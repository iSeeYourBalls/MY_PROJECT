using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaxManager
{
    public partial class ChangeDateForRegistr : Form
    {
        public ChangeDateForRegistr()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Registry registry = this.Owner as Registry;
            registry.updateDate(dateTimePicker_changeDate.Value);
            this.Close();
        }

        private void ChangeDateForRegistr_Load(object sender, EventArgs e)
        {

        }
    }
}
