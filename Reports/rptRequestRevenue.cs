using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace HomeService.Reports
{
    public partial class rptRequestRevenue : DevExpress.XtraReports.UI.XtraReport
    {
        public double totalcost { get; set; }
        public rptRequestRevenue(double cost)
        {
            InitializeComponent();
            totalcost = cost;
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void rptRequestRevenue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel2.Text = totalcost.ToString();
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.ImageUrl = "https://localhost:44383/Public/images/logo.png";

        }
    }
}
