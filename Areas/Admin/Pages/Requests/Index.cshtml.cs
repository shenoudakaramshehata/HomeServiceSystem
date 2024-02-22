using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Entities;
using HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.Requests
{
    public class IndexModel : PageModel
    {
        private HomeServiceContext _context;
        private readonly IToastNotification _toastNotification;
        public IndexModel(HomeServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        [BindProperty(SupportsGet =true)]
        public List<RequestStateVM> statelst { get; set; }
        public IActionResult OnGet()
        {
            try
            {
                var temp = new RequestStateVM
                {
                    RequestStateAr = "All",
                    RequestStateEn = "All",
                    RequestStateId = 0
                };
                statelst.Add(temp);

                var requestStatelst = _context.RequestState.ToList();
                foreach (var item in requestStatelst)
                {
                    temp = new RequestStateVM();
                    temp.RequestStateAr = item.RequestStateAr;
                    temp.RequestStateEn = item.RequestStateEn;
                    temp.RequestStateId = item.RequestStateId;

                    statelst.Add(temp);

                }

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");

            }


            return Page();
        }
    }
}
