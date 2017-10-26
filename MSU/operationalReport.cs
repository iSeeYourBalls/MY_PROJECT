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
    public partial class operationalReport : Form
    {
        public operationalReport()
        {
            InitializeComponent();
        }

        private void operationalReport_Load(object sender, EventArgs e)
        {

        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                cn.Open();
                using (DandDDataSet SGR = new DandDDataSet())
                {
                    using (SqlDataAdapter SGRda = new SqlDataAdapter("EXEC OperationalReport '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'", cn))
                    {
                        SGRda.Fill(SGR, SGR.Tables["operationalReport"].TableName);

                        ReportDataSource SGRrds = new ReportDataSource("DataSet1", SGR.Tables["operationalReport"]);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(SGRrds);
                        this.reportViewer1.LocalReport.Refresh();

                        this.reportViewer1.RefreshReport();
                    }
                }
            }
        }

        private void operationalReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.Reset();
            this.reportViewer1.Dispose();
            GC.SuppressFinalize(reportViewer1);
            GC.Collect();
        }
    }
}
