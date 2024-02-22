using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Models;
using HomeService.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Areas.Admin.Pages.ReportManagement
{
    public class RequestRevenueModel : PageModel
    {
        private readonly HomeServiceContext context;

        public rptRequestRevenue Report { get; set; }
        public RequestRevenueModel(HomeServiceContext context)
        {
            this.context = context;
        }
         public double TotalCost { get; set; }

        [BindProperty]
        public FilterModel filterModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            TotalCost = context.Request.Where(a=>a.isPaid==true).Sum(a => a.Cost);
            List<Request> ds = context.Request.Include(i => i.Customer).Include(i => i.Technician).Where(a=>a.isPaid==true).ToList();

            if (filterModel.startdate != null && filterModel.enddate == null)
            {
                ds = null;
                TotalCost = 0;
            }
            if (filterModel.startdate == null && filterModel.enddate != null)
            {
                ds = null;
                TotalCost = 0;
            }
            if (filterModel.startdate != null && filterModel.enddate != null)
            {
                ds = ds.Where(i => i.RequestDate <= filterModel.enddate && i.RequestDate >= filterModel.startdate).ToList();
                TotalCost = ds.Sum(e => e.Cost);
            }
            Report = new rptRequestRevenue(TotalCost);
            Report.DataSource = ds;
            return Page();
        }
    }
}
