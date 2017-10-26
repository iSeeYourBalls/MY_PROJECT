using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.IO;


namespace MSU
{
    public partial class logist : Form
    {
        public logist()
        {
            InitializeComponent();
        }

        private void logist_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                cn.Open();
                Cursor = Cursors.WaitCursor;
                DateTime creat_date = Convert.ToDateTime(dateTimePicker1.Text);

                using (DandDDataSet l = new DandDDataSet())
                {
                    l.EnforceConstraints = false;

                    using (SqlDataAdapter da = new SqlDataAdapter("exec logist '" + creat_date.ToString("yyyy-MM-dd") + "'", cn))
                    {
                        da.Fill(l, l.Tables[0].TableName);
                        ReportDataSource rds = new ReportDataSource("DataSet1", l.Tables[0]);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(rds);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void logist_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.Reset();
            this.reportViewer1.Dispose();
            GC.SuppressFinalize(reportViewer1);
            GC.Collect();
        }
    }
}
