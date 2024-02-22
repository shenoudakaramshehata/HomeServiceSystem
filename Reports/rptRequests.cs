using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace HomeService.Reports
{
    public partial class rptRequests : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRequests()
        {
            InitializeComponent();
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.ImageUrl = "https://localhost:44383/Public/images/logo.png";

        }
    }
}
