using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeService.Data;
using HomeService.Models;
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.ServiceCategories
{
    public class DetailsModel : PageModel
    {


        private HomeServiceContext _context;
        private readonly IToastNotification _toastNotification;

        public DetailsModel(HomeServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;

        }
        [BindProperty]
        public ServiceCategory serviceCategory { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
           
            try
            {
                serviceCategory = await _context.ServiceCategory.FirstOrDefaultAsync(m => m.ServiceCategoryId == id);
                
                if (serviceCategory == null)
                {
                    return Redirect("../Error");
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
