using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeService.Data;
using HomeService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using NToastNotify;
using Microsoft.AspNetCore.Localization;

namespace HomeService.Areas.Admin.Pages.Technicians
{
    public class DeleteModel : PageModel
    {

        private readonly HomeServiceContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;




        public DeleteModel(HomeServiceContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            ApplicationDbContext db, RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostEnvironment
            , IToastNotification toastNotification)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
            _roleManager = roleManager;
            _toastNotification = toastNotification;
            _hostEnvironment = hostEnvironment;

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

                throw;
            }



            return Page();
        }






        public async Task<IActionResult> OnPostAsync(int id)
        {


            try
            {
                if ( _context.Request.Any(c => c.TechnicianId == id))
                {
                    var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                    //if (BrowserCulture == "en-US")
                    _toastNotification.AddErrorToastMessage("You cannot delete this Technician");

                    //else
                    //_toastNotification.AddErrorToastMessage("لا يمكنك حذف هذا العميل ");

                    technician = await _context.Technician
                      .Include(c => c.Nationality)
                      .FirstOrDefaultAsync(m => m.TechnicianId == id);

                    return Page();


                }
                var model = _context.Technician.Find(id);

                if (model == null)
                {
                    return Redirect("../Error");
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                _context.Technician.Remove(model);
                await _context.SaveChangesAsync();
                if (user != null)
                {
                    _db.Users.Remove(user);
                    await _db.SaveChangesAsync();

                }
                _toastNotification.AddSuccessToastMessage("Technician Deleted successfully");


            }
            catch (Exception)

            {
                technician = await _context.Technician
                      .Include(c => c.Nationality)
                      .FirstOrDefaultAsync(m => m.TechnicianId == id);
                _toastNotification.AddErrorToastMessage("Something went wrong");


            }

            return RedirectToPage("./Index");
        }


    }
}
