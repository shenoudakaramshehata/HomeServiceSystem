using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.Customers
{
    public class ContractsModel : PageModel
    {
        private HomeServiceContext _context;
        private readonly IToastNotification _toastNotification;
        public ContractsModel(HomeServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        [BindProperty]
        public Customer customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            try
            {
                customer = await _context.Customer
                   .Include(c => c.Area)
                   .Include(c => c.Area.City)
                   .Include(c => c.Nationality)
                   .FirstOrDefaultAsync(m => m.CustomerId == id);
                if (customer == null)
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

