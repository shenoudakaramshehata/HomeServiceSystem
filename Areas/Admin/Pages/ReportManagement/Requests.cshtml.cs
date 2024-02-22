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
    public class RequestsModel : PageModel
    {
        private readonly HomeServiceContext context;

        public rptRequests Report { get; set; }
        public RequestsModel(HomeServiceContext context)
        {
            this.context = context;
        }
     

        [BindProperty]
        public FilterModel filterModel { get; set; }

        public IActionResult OnGet()
        {
            
            return Page();
        }
        public IActionResult OnPost()
        {
            List<Request> ds = context.Request.Include(i => i.Customer).Include(i => i.Technician).Include(a=>a.RequestState).ToList();

            if (filterModel.requeststatusid==1)
            {
                ds = ds.Where(a=>a.RequestStateId==filterModel.requeststatusid).ToList();
            }
            if (filterModel.requeststatusid==2)
            {
                ds = ds.Where(a=>a.RequestStateId==filterModel.requeststatusid).ToList();
            }
            if (filterModel.requeststatusid==3)
            {
                ds = ds.Where(a=>a.RequestStateId==filterModel.requeststatusid).ToList();
            }
            if (filterModel.requeststatusid==4)
            {
                ds = ds.Where(a=>a.RequestStateId==filterModel.requeststatusid).ToList();
            }
            if (filterModel.requeststatusid==5)
            {
                ds = ds.Where(a=>a.RequestStateId==filterModel.requeststatusid).ToList();
            }
            if (filterModel.startdate != null && filterModel.enddate != null)
            {
                ds = ds.Where(i => i.RequestDate <= filterModel.enddate && i.RequestDate >= filterModel.startdate).ToList();
            }
             if (filterModel.startdate == null && filterModel.enddate == null&&filterModel.requeststatusid==0)
            {
                ds = null;
            }


            Report = new rptRequests();
            Report.DataSource = ds;
            return Page();
        }
    }
}
