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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;

namespace HomeService.Areas.Admin.Pages.Customers
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
        public Customer customer { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            try
            {
                customer = await _context.Customer
                    .Include(c=>c.Area) 
                    .Include(c=>c.Area.City)
                    .Include(c=>c.Nationality)
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


        public async Task<IActionResult> OnPostAsync(int id)
        {


            try
            {
                if (_context.Contract.Any(c=>c.CustomerId==id)||_context.Request.Any(c=>c.CustomerId==id))
                {
                    var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                    //if (BrowserCulture == "en-US")
                    _toastNotification.AddErrorToastMessage("You cannot delete this Customer");

                    //else
                    //_toastNotification.AddErrorToastMessage("لا يمكنك حذف هذا العميل ");

                    customer = await _context.Customer
              .Include(c => c.Area)
              .Include(c => c.Area.City)
              .Include(c => c.Nationality)
              .FirstOrDefaultAsync(m => m.CustomerId == id);
                    return Page();


                }
                var model = _context.Customer.Find(id);

                if (model == null)
                {
                    return Redirect("../Error");
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                _context.Customer.Remove(model);
                await _context.SaveChangesAsync();
                if (user!=null)
                {
                    _db.Users.Remove(user);
                    await _db.SaveChangesAsync();

                }
                    _toastNotification.AddSuccessToastMessage("Customer Deleted successfully");

               
            }
            catch (Exception)

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
                _toastNotification.AddErrorToastMessage("Something went wrong");


            }

            return RedirectToPage("./Index");
        }


    }
}
