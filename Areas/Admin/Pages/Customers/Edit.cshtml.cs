using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Entities;
using HomeService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.Customers
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
        public Customer customer { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
           
            try
            {

                customer = await _context.Customer.FirstOrDefaultAsync(m => m.CustomerId == id);
                if (customer == null)
                {
                    return Redirect("../Error");
                }
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();
            }



            return Page();
        }
        public async Task<IActionResult> OnPostAsync(Customer model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {

                var modelExist = _context.Customer.Any(e => e.CustomerId == model.CustomerId);
                if (modelExist == false)
                {
                    return Redirect("../Error");

                }

                if (Response.HttpContext.Request.Form.Files.Count() > 0)
                {
                    var uniqeFileName = "";
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Customer");
                    string ext = Path.GetExtension(Response.HttpContext.Request.Form.Files[0].FileName);
                    uniqeFileName = Guid.NewGuid() + ext;
                    string uploadedImagePath = Path.Combine(uploadFolder, uniqeFileName);
                    using (FileStream fileStream = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        Response.HttpContext.Request.Form.Files[0].CopyTo(fileStream);
                    }
                    model.Pic = uniqeFileName;
                }
                _context.Attach(model).State = EntityState.Modified;
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Customer Edited successfully");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();

            }

            return RedirectToPage("./Index");
        }

    }
}
