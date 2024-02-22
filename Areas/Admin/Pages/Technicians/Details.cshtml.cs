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

namespace HomeService.Areas.Admin.Pages.Technicians
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
        public Technician technician { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
           
            try
            {
                technician = await _context.Technician
                    .Include(c=>c.Nationality)
                    .FirstOrDefaultAsync(m => m.TechnicianId == id);
                if (technician == null)
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
