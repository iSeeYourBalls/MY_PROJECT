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
using System.Security;
using System.Reflection;


namespace MSU
{
    public partial class loadingList : Form
    {
        public loadingList()
        {
            InitializeComponent();
        }

        private void loadingList_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                cn.Open();
                using (DandDDataSet r = new DandDDataSet())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT action.finish, action.start, action.id_order,action.shiftcode, shift.truck, shift.driver, address.addressName, address.cityName, orders.kg, orders.pal, product.productName, cast(action.start_finish AS int) as start_finish FROM action left join shift on action.shiftcode = shift.shiftcode LEFT OUTER JOIN address ON action.start = address.addressCode LEFT OUTER JOIN orders ON action.id_order = orders.order_number LEFT OUTER JOIN product ON orders.product = product.productCode WHERE cast(action.shiftcode as int) IN (" + string.Join(",", GlobalVariable.edit_shift_waybill) + ") ORDER BY  action.shiftcode, cast(start_finish as int) DESC", cn))
                    {
                        da.Fill(r, r.Tables[0].TableName);

                        ReportDataSource mds = new ReportDataSource("DataSet1", r.Tables[0]);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(mds);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();
                    }
                }
            }
        }

        private void loadingList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.Reset();
            this.reportViewer1.Dispose();
            GC.SuppressFinalize(reportViewer1);
            GC.Collect();
        }
    }
}
