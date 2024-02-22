using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeService.Data;
using HomeService.Models;
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.serviceCategory
{
    public class EditModel : PageModel
    {


        private HomeServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public EditModel(HomeServiceContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
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




        public async Task<IActionResult> OnPostAsync(int ?id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try
            {
                var model = _context.ServiceCategory.Where(c => c.ServiceCategoryId == id.Value).FirstOrDefault();
                if (model == null)
                {
                    return Redirect("../Error");

                }
                var uniqeFileName = "";

                if (Response.HttpContext.Request.Form.Files.Count() > 0)
                {
                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images/ServiceCategory/" + model.pic);
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/ServiceCategory");
                    string ext = Path.GetExtension(Response.HttpContext.Request.Form.Files[0].FileName);
                    uniqeFileName = Guid.NewGuid() + ext;
                    string uploadedImagePath = Path.Combine(uploadFolder, uniqeFileName);
                    using (FileStream fileStream = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        Response.HttpContext.Request.Form.Files[0].CopyTo(fileStream);
                    }
                    model.pic = uniqeFileName;
                }

                model.ServiceCategoryTlAr = serviceCategory.ServiceCategoryTlAr;
                model.ServiceCategoryTlEn = serviceCategory.ServiceCategoryTlEn;
                _context.Attach(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("ServiceCategory Edited successfully");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");

            }

            return RedirectToPage("./Index");
        }

    }
}
