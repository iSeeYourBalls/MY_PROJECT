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
    public partial class shiftGridReport : Form
    {        
        public shiftGridReport()
        {
            InitializeComponent();
        }

        private void shiftGridReport_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(GlobalVariable.general_date);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                cn.Open();
                using (DandDDataSet SGR = new DandDDataSet())
                {
                    using (SqlDataAdapter SGRda = new SqlDataAdapter("EXEC shiftgrid '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "'", cn))
                    {
                        SGRda.Fill(SGR, SGR.Tables["shiftGridReport"].TableName);

                        ReportDataSource SGRrds = new ReportDataSource("DataSet1", SGR.Tables["shiftGridReport"]);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(SGRrds);
                        this.reportViewer1.LocalReport.Refresh();

                        this.reportViewer1.RefreshReport();
                    }
                }
            }
        }

        private void shiftGridReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.Reset();
            this.reportViewer1.Dispose();
            GC.SuppressFinalize(reportViewer1);
            GC.Collect();
        }
    }
}
