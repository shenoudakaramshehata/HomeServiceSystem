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

namespace HomeService.Areas.Admin.Pages.Technicians
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
        public TechnicianVM model { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost(TechnicianVM registrationModel)
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
                var technician = new Technician();
               
                if (Response.HttpContext.Request.Form.Files.Count() > 0)
                {
                    var uniqeFileName = "";
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Technician");
                    string ext = Path.GetExtension(Response.HttpContext.Request.Form.Files[0].FileName);
                    uniqeFileName = Guid.NewGuid() + ext;
                    string uploadedImagePath = Path.Combine(uploadFolder, uniqeFileName);
                    using (FileStream fileStream = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        Response.HttpContext.Request.Form.Files[0].CopyTo(fileStream);
                    }
                    technician.Pic = uniqeFileName;
                }
                technician.Mobile = registrationModel.Mobile;
                technician.Tele = registrationModel.Tele;
                technician.FullNameAr = registrationModel.FullNameAr;
                technician.FullNameEn = registrationModel.FullNameEn;
                technician.FullAddress = registrationModel.FullAddress;
                technician.CivilId = registrationModel.CivilId;
                technician.NationalityId = registrationModel.NationalityId;
                technician.PassportNo = registrationModel.PassportNo;
                technician.Remarks = registrationModel.Remarks;
                technician.Email = registrationModel.Email;
                _context.Technician.Add(technician);
                _context.SaveChanges();
                if (!(technician.TechnicianId > 0))
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
                    EntityId = technician.TechnicianId,
                    EntityName = 3

                };

                var result = await _userManager.CreateAsync(user, registrationModel.Password);

                if (!result.Succeeded)
                {
                    _context.Technician.Remove(technician);
                    _context.SaveChanges();
                    _toastNotification.AddErrorToastMessage(result.Errors.First().Description);
                    model = registrationModel;
                    _toastNotification.AddErrorToastMessage("Something went wrong");

                    return Page();
                }
                await _userManager.AddToRoleAsync(user, "Technician");
                _toastNotification.AddSuccessToastMessage("Technician Added successfully");


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
