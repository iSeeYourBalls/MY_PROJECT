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
    public partial class Waybill : Form
    {
        public Waybill()
        {
            InitializeComponent();

        }

        private void Waybill_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(GlobalVariable.connectionString))
            {
                cn.Open();
                using (DandDDataSet m = new DandDDataSet())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("select distinct  shift.shiftcode, case when exists (select * from product where product.id_productKind IN ('8') and product.productCode IN (select product from orders where orders.order_number IN (select id_order from action ac3 where ac3.shiftcode = shift.shiftcode))) THEN 1 ELSE case when exists (select * from product where product.id_productKind IN ('10') and product.productCode IN (select product from orders join ref on orders.datework = Ref.createDate  where Ref.frigo = '1' and orders.order_number IN (select id_order from action ac3 where ac3.shiftcode = shift.shiftcode))) THEN 1 ELSE 0 end end as ref, case when exists (select * from product where product.id_productKind IN ('4') and product.productCode IN (select product from orders where orders.order_number IN (select id_order from action ac3 where ac3.shiftcode = shift.shiftcode))) THEN 1 ELSE 0 end as frozen, shift.truck, shift.driver, shift.trailer, subcontractor.contactName,case when (action.start = 8850) THEN N'РЦ13' ELSE action.start end as start,case when (action.finish = 8850) THEN N'РЦ13' ELSE action.finish end as finish, action.sort  'Delivery_sequence',(select sum(cast(orders.kg as int)) / 1000.00 from orders join action ac on orders.order_number = ac.id_order where ac.shiftcode = shift.shiftcode) as allKg,(select sum(cast(orders.pal as int)) from orders join action ac on orders.order_number = ac.id_order where ac.shiftcode = shift.shiftcode) as allPall,(select sum(cast(orders.kg as int)) / 1000.00 from orders left join action ac on orders.order_number = ac.id_order where ac.start_finish -1 >= action.start_finish and ac.shiftcode = shift.shiftcode) as shopKg,(select sum(cast(orders.pal as int)) from orders join action ac on orders.order_number = ac.id_order where ac.start_finish -1 >= action.start_finish and ac.shiftcode = shift.shiftcode) as shopPal,(select top 1 cast(ditance / 1000.00 as decimal(10,2)) from distance where addressfromcode = (select top 1 start from action ac where ac.shiftcode = shift.shiftcode and ac.start = action.start) and addresstocode = action.finish) as distance,address.cityName,address.streetName,Ref.frigo from shift	left join subcontractor on shift.subcontractor = subcontractor.contact_externalId join action on shift.shiftcode = action.shiftcode left join address on action.finish = address.addressCode left join Ref on cast(shift.datework as date) = Ref.createDate left join action ac_ord on action.finish = ac_ord.start where shift.shiftcode IN(" + string.Join(",", GlobalVariable.edit_shift_waybill) + ") and action.actionid = '0' and action.id NOT IN (select top 1 id from action JOIN shift sh ON action.shiftcode = sh.shiftcode where action.shiftcode = shift.shiftcode and sh.freight = 1  order by id DESC) order by shiftcode, Delivery_sequence", cn))
                    {
                        da.Fill(m, m.Tables[0].TableName);
                        ReportDataSource rds = new ReportDataSource("DataSet1", m.Tables[0]);
                        this.reportViewer1.LocalReport.DataSources.Clear();
                        this.reportViewer1.LocalReport.DataSources.Add(rds);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport(); 
                    }
                }
            }
        }

        private void Waybill_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.Reset();
            this.reportViewer1.Dispose();
            GC.SuppressFinalize(reportViewer1);
            GC.Collect();
        }
    }
}
