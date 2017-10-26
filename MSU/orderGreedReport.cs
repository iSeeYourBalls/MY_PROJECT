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
    public partial class orderGreedReport : Form
    {
        public orderGreedReport()
        {
            InitializeComponent();
        }

        private void orderGreedReport_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(GlobalVariable.general_date);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                cn.Open();
                using (DandDDataSet ORR = new DandDDataSet())
                {
                    using (SqlDataAdapter SGRda = new SqlDataAdapter("SELECT order_number, orders.address, address.stopFromDay as stopFromDay, address.stopTillDay as stopTillDay,  address.comment as address_com, orders.orderStatus, address.cityName,orders.comment,orders.kg,orders.pal,product.productName,shift.truck as truck, orders.datework, distance.ditance / 1000 as KM FROM orders left join address on orders.address = address.addressCode left join product on orders.product = product.productCode left join action ON orders.order_number = action.id_order left join shift on action.shiftcode = shift.shiftcode left join distance ON orders.address = distance.addressToCode and distance.addressFromCode = '" + GlobalVariable.depot + "'  WHERE orders.datework = '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "'", cn))
                    {
                        SGRda.Fill(ORR, ORR.Tables["orderGreed"].TableName);

                        ReportDataSource ORRrds = new ReportDataSource("DataSet1", ORR.Tables["orderGreed"]);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(ORRrds);
                        this.reportViewer1.LocalReport.Refresh();

                        this.reportViewer1.RefreshReport();
                    }
                }
            }
        }

        private void orderGreedReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.Reset();
            this.reportViewer1.Dispose();
            GC.SuppressFinalize(reportViewer1);
            GC.Collect();
        }
    }
}
