using Microsoft.Reporting.WinForms;
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
    public partial class ReportCountTripInDayForm : Form
    {
        public ReportCountTripInDayForm()
        {
            InitializeComponent();
        }

        private void ReportCountTripInDayForm_Load(object sender, EventArgs e)
        {
            List<months> monthsArr = new List<months>
            {
                new months {name = "Январь", value = 01},
                new months {name = "Февраль", value = 02},
                new months {name = "Март", value = 03},
                new months {name = "Апрель", value = 04},
                new months {name = "Май", value = 05},
                new months {name = "Июнь", value = 06},
                new months {name = "Июль", value = 07},
                new months {name = "Август", value = 08},
                new months {name = "Сентябрь", value = 09},
                new months {name = "Октябрь", value = 10},
                new months {name = "Ноябрь", value = 11},
                new months {name = "Декабрь", value = 12},
            };

            comboBox1.DataSource = monthsArr;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "value";
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            string numberMonth = comboBox1.SelectedValue.ToString();
            string currentYear = cb_lastYear.Checked ? DateTime.Now.AddYears(-1).Year.ToString() : DateTime.Now.Year.ToString();
            int countDayInSelectedMonth = DateTime.DaysInMonth(int.Parse(currentYear), int.Parse(numberMonth));
            string startDate = currentYear + "-" + numberMonth + "-01";
            string finishDate = currentYear + "-" + numberMonth + "-" + countDayInSelectedMonth;

            Cursor = Cursors.WaitCursor;
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        cmd.CommandText = "EXEC reportCountTripInDay '" + startDate + "', '" + finishDate + "' ";
                        cmd.CommandTimeout = 300;

                        using (DataSet1 l = new DataSet1())
                        {
                            l.EnforceConstraints = false;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(l, l.Tables["reportCountTripInDay"].TableName);
                                ReportDataSource rds = new ReportDataSource("DataSet1", l.Tables["reportCountTripInDay"]);
                                this.reportViewer1.LocalReport.DataSources.Clear();
                                this.reportViewer1.LocalReport.DataSources.Add(rds);
                                ReportParameter monthName = new ReportParameter("monthName", comboBox1.Text);
                                this.reportViewer1.LocalReport.SetParameters(monthName);
                                this.reportViewer1.LocalReport.Refresh();
                                this.reportViewer1.RefreshReport();
                                Cursor = Cursors.Default;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Дата введена не верно или за выбранный период данных не существует. Подробности : " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Cursor = Cursors.Default;
        }
    }


}
