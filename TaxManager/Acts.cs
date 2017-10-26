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
    public partial class Acts : Form
    {
        SqlDataReader dr;
        MoneyToStr grn = new MoneyToStr("UAH", "UKR", "980");
        DateTime reportDateFrom;
        DateTime reportDateTo;

        public Acts()
        {
            InitializeComponent();
        }

        private void Acts_Load(object sender, EventArgs e)
        {
            if (GlobalVariable.isPrintManyActs)
            {
                dateFrom.Enabled = false;
                dateTo.Enabled = false;
                txt_actNumber.Text = dateTimePicker_actDate.Value.ToString("yyyyMMddHHmm");
                printManyActs();
            }
        }

        private void printManyActs()
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "select SUM(allsum) as allsum FROM dataRegistr WHERE id IN (" + string.Join(",", GlobalVariable.acts) + ") and isTax = 1";

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        double allSum = double.Parse(dr[0].ToString());
                        dr.Close();

                        string sumInString = grn.convertValue(allSum);


                        using (DataSet1 l = new DataSet1())
                        {
                            l.EnforceConstraints = false;

                            using (SqlDataAdapter da = new SqlDataAdapter("EXEC reportManyActs '" + string.Join(",", GlobalVariable.acts) + "'", cn))
                            {
                                da.Fill(l, l.Tables["ForReportActs"].TableName);
                                ReportDataSource rds = new ReportDataSource("DataSet1", l.Tables["ForReportActs"]);
                                this.reportViewer1.LocalReport.DataSources.Clear();
                                this.reportViewer1.LocalReport.DataSources.Add(rds);
                                ReportParameter sumAsString = new ReportParameter("sumAsString", sumInString);
                                ReportParameter actDate = new ReportParameter("dateReport", dateTimePicker_actDate.Value.ToString("dd.MM.yyyy"));
                                ReportParameter actNumber = new ReportParameter("actNumber", txt_actNumber.Text);
                                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { sumAsString, actDate, actNumber });
                                this.reportViewer1.LocalReport.Refresh();
                                this.reportViewer1.RefreshReport();
                                Cursor = Cursors.Default;
                            }
                        }

                        btn_changeStatus.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Дата введена не верно или за выбранный период актов не существует." + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_result_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                reportDateFrom = dateFrom.Value;
                reportDateTo = dateTo.Value;
                int typeReportDate = rB_ttnDate.Checked ? 0 : 1;
                string dateTypeInCommandText = typeReportDate == 0 ? "dateDocument":"CAST(dateInsert as date)";

                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandText = "select SUM(allsum) as allsum FROM dataRegistr WHERE " + dateTypeInCommandText + " between @dateOne and @dateTwo and isTax = 1";
                        cmd.Parameters.Add("@dateOne", SqlDbType.Date);
                        cmd.Parameters.Add("@dateTwo", SqlDbType.Date);
                        cmd.Parameters["@dateOne"].Value = reportDateFrom;
                        cmd.Parameters["@dateTwo"].Value = reportDateTo;

                        dr = cmd.ExecuteReader();
                        dr.Read();
                        double allSum = double.Parse(dr[0].ToString());
                        dr.Close();

                        string sumInString = grn.convertValue(allSum);


                        using (DataSet1 l = new DataSet1())
                        {
                            l.EnforceConstraints = false;

                            using (SqlDataAdapter da = new SqlDataAdapter("exec reportActs '" + reportDateFrom.ToString("yyyy-MM-dd") + "', '" + reportDateTo.ToString("yyyy-MM-dd") + "', '"+ typeReportDate +"'", cn))
                            {
                                da.Fill(l, l.Tables["ForReportActs"].TableName);
                                ReportDataSource rds = new ReportDataSource("DataSet1", l.Tables["ForReportActs"]);
                                this.reportViewer1.LocalReport.DataSources.Clear();
                                this.reportViewer1.LocalReport.DataSources.Add(rds);
                                ReportParameter sumAsString = new ReportParameter("sumAsString", sumInString);
                                ReportParameter actDate = new ReportParameter("dateReport", dateTimePicker_actDate.Value.ToString("dd.MM.yyyy"));
                                ReportParameter actNumber = new ReportParameter("actNumber", txt_actNumber.Text);
                                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { sumAsString, actDate, actNumber });
                                this.reportViewer1.LocalReport.Refresh();
                                this.reportViewer1.RefreshReport();
                                
                            }
                        }

                        Cursor = Cursors.Default;
                        btn_changeStatus.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Дата введена не верно или за выбранный период актов не существует." + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void Acts_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalVariable.isPrintManyActs)
                GlobalVariable.isPrintManyActs = false;
            
            this.reportViewer1.Reset();
            this.reportViewer1.Dispose();
            GC.SuppressFinalize(reportViewer1);
            GC.Collect();
        }

        private void btn_changeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Применить статус <Распечатан> к выбранным поездкам в сформированном акте?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
                    {
                        cn.Open();

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = cn;
                            cmd.CommandText = @"EXEC updateTaxStatus @dateOne, @dateTwo, @month, @type, @actNumber";
                            cmd.Parameters.Add("@dateOne", SqlDbType.Date);
                            cmd.Parameters.Add("@dateTwo", SqlDbType.Date);
                            cmd.Parameters.Add("@month", SqlDbType.Int);
                            cmd.Parameters.Add("@type", SqlDbType.Int);
                            cmd.Parameters.Add("@actNumber", SqlDbType.NVarChar);
                            cmd.Parameters["@dateOne"].Value = reportDateFrom;
                            cmd.Parameters["@dateTwo"].Value = reportDateTo;
                            cmd.Parameters["@type"].Value = rB_createDate.Checked ? 1 : 2;
                            cmd.Parameters["@month"].Value = int.Parse(dateTimePicker_actDate.Value.ToString("MM"));
                            cmd.Parameters["@actNumber"].Value = txt_actNumber.Text;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n btn_changeStatus_Click " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            dateTo.Value = dateFrom.Value;
        }

    }
}
