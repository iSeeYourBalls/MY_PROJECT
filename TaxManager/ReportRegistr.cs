using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxManager
{
    public partial class ReportRegistr : Form
    {
        int allColumns;
        int columnRegistr;
        int shortReg;

        public ReportRegistr()
        {
            InitializeComponent();
        }

        private void ReportRegistr_Load(object sender, EventArgs e)
        {

        }

        private void btn_result_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                   

                    int typeReportDate = rB_ttnDate.Checked ? 0 : 1;

                    if (radioButton_allColums.Checked)
                    {
                        allColumns = 1;
                        columnRegistr = 1;
                        shortReg = 1;
                    }
                    else if (radioButton_short.Checked)
                    {
                        allColumns = 0;
                        columnRegistr = 1;
                        shortReg = 2;
                    }
                    else
                    {
                        allColumns = 0;
                        columnRegistr = 0;
                        shortReg = 2;
                    }

                    
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.CommandText = "EXEC reportRegistry '" + dateFrom.Value.ToString("yyyy-MM-dd") + "', '" + dateTo.Value.ToString("yyyy-MM-dd") + "', '', '" + typeReportDate + "'";
                        cmd.CommandTimeout = 180;

                        using (DataSet1 l = new DataSet1())
                        {
                            l.EnforceConstraints = false;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(l, l.Tables["ForReportActs"].TableName);
                                ReportDataSource rds = new ReportDataSource("DataSet1", l.Tables["ForReportActs"]);
                                this.reportViewer1.LocalReport.DataSources.Clear();
                                this.reportViewer1.LocalReport.DataSources.Add(rds);
                                ReportParameter colums = new ReportParameter("allColums", allColumns.ToString());
                                ReportParameter regColums = new ReportParameter("columnRegistr", columnRegistr.ToString());
                                ReportParameter forShortReg = new ReportParameter("shortReg", shortReg.ToString());
                                ReportParameter registrName = new ReportParameter("registrName", txt_registrName.Text);
                                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { colums, regColums, forShortReg, registrName });
                                this.reportViewer1.LocalReport.Refresh();
                                this.reportViewer1.RefreshReport();
                                Cursor = Cursors.Default;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Дата введена не верно или за выбранный период актов не существует." + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ReportRegistr_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.Reset();
            this.reportViewer1.Dispose();
            GC.SuppressFinalize(reportViewer1);
            GC.Collect();
        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            dateTo.Value = dateFrom.Value;
        }
    }
}
