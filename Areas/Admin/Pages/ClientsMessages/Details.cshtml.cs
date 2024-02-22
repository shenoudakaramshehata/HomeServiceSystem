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

namespace HomeService.Areas.Admin.Pages.ClientsMessages
{
    public class DetailsModel : PageModel
    {
        private HomeServiceContext _context;
        private readonly IToastNotification _toastNotification;

        public DetailsModel(HomeServiceContext context,  IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;

        }

        public ContactUs ContactUsMessages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                

                ContactUsMessages = await _context.ContactUs.FirstOrDefaultAsync(m => m.ContactId == id);

                if (ContactUsMessages == null)
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
