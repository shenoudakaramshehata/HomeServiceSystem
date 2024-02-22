using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using HomeService.Data;
using HomeService.Models;
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.ServiceCategories
{
    public class DeleteModel : PageModel
    {

        private HomeServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public DeleteModel(HomeServiceContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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




        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           

            try
            {
                var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images/ServiceCategory/" + serviceCategory.pic);
                serviceCategory = await _context.ServiceCategory.FindAsync(id);
                if (serviceCategory != null)
                {
                    if (_context.Request.Any(c => c.ServiceCategoryId == id))
                    {
                        _toastNotification.AddErrorToastMessage("You cannot delete this ServiceCategory");
                        return Page();
                    }
           

                    _context.ServiceCategory.Remove(serviceCategory);
                    await _context.SaveChangesAsync();
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                    _toastNotification.AddSuccessToastMessage("ServiceCategory Deleted successfully");

                }
            }
            catch (Exception ex)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();

            }

            return RedirectToPage("./Index");
        }

    }
}
