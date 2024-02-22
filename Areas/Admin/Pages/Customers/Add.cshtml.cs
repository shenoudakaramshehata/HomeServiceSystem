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
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.Customers
{
    public class AddModel : PageModel
    {

        private readonly HomeServiceContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;




        public AddModel(HomeServiceContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
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
        public CustomerVM model { get; set; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost(CustomerVM registrationModel)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(registrationModel.Email);
                if (userExists != null)
                {
                    model = registrationModel;
                    _toastNotification.AddWarningToastMessage("User already exists!");
                    return Page();
                }

                if (registrationModel.Password != registrationModel.ConfirmPassword)
                {
                    model = registrationModel;
                    _toastNotification.AddErrorToastMessage("Password and Confirm Password not matched!");
                    return Page();
                }
                var customer = new Customer();
               
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
                    customer.Pic = uniqeFileName;
                }
                customer.Mobile = registrationModel.Mobile;
                customer.Tele1 = registrationModel.Tele1;
                customer.Tele2 = registrationModel.Tele2;
                customer.FullNameAr = registrationModel.FullNameAr;
                customer.FullNameEn = registrationModel.FullNameEn;
                customer.AreaId = registrationModel.AreaId;
                customer.CivilId = registrationModel.CivilId;
                customer.Block = registrationModel.Block;
                customer.Avenue = registrationModel.Avenue;
                customer.Street = registrationModel.Street;
                customer.BuildingNo = registrationModel.BuildingNo;
                customer.Floor = registrationModel.Floor;
                customer.Flat = registrationModel.Flat;
                customer.NationalityId = registrationModel.NationalityId;
                customer.PassportNo = registrationModel.PassportNo;
                customer.Remarks = registrationModel.Remarks;
                customer.Email = registrationModel.Email;
                _context.Customer.Add(customer);
                _context.SaveChanges();
                if (!(customer.CustomerId > 0))
                {
                    model = registrationModel;
                    _toastNotification.AddErrorToastMessage("Something went wrong");
                    return Page();
                }
              
                var user = new ApplicationUser
                {
                    UserName = registrationModel.Email,
                    Email = registrationModel.Email,
                    PhoneNumber = registrationModel.Mobile,
                    EntityId = customer.CustomerId,
                    EntityName = 2

                };

                var result = await _userManager.CreateAsync(user, registrationModel.Password);

                if (!result.Succeeded)
                {
                    _context.Customer.Remove(customer);
                    _context.SaveChanges();
                    _toastNotification.AddErrorToastMessage(result.Errors.First().Description);
                    model = registrationModel;

                    return Page();
                }
                await _userManager.AddToRoleAsync(user, "Customer");
                _toastNotification.AddSuccessToastMessage("Customer Added successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();
            }
            return Redirect("./Index");
        }
    }
}
