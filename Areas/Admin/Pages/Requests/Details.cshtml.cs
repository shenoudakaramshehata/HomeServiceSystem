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

namespace HomeService.Areas.Admin.Pages.Requests
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
        [BindProperty(SupportsGet = true)]

        public Request request { get; set; }
        [BindProperty(SupportsGet = true)]

        public CustomerRequest customerRequest { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                     request = await _context.Request
                    .Include(c=>c.RequestState)
                    .Include(c => c.Customer)
                    .Include(c=>c.Technician)
                    .Include(c=>c.ServiceCategory)
                    .FirstOrDefaultAsync(m => m.RequestId == id);
                if (request == null)
                {
                    return Redirect("../Error");
                }
                customerRequest = new CustomerRequest();
                customerRequest.RequestId = request.RequestId;
                customerRequest.RequestDate = request.RequestDate;
                customerRequest.ScheduleDate = request.ScheduleDate;
                customerRequest.FullNameAr = request.Customer.FullNameAr;
                customerRequest.FullNameEn = request.Customer.FullNameEn;
                customerRequest.Email = request.Customer.Email;
                customerRequest.Tele1 = request.Customer.Tele1;
                customerRequest.Tele2 = request.Customer.Tele2;
                customerRequest.Mobile = request.Customer.Mobile;
                customerRequest.CustomerRemarks = request.Customer.Remarks;
                customerRequest.Pic = request.Customer.Pic;
                customerRequest.Street = request.Customer.Street;
                customerRequest.Block = request.Customer.Block;
                customerRequest.Avenue = request.Customer.Avenue;
                customerRequest.BuildingNo = request.Customer.BuildingNo;
                customerRequest.Flat = request.Customer.Flat;
                customerRequest.Floor = request.Customer.Floor;

                customerRequest.RequestStateAr = request.RequestState.RequestStateAr;
                customerRequest.RequestStateEn = request.RequestState.RequestStateEn;
                customerRequest.ServiceCategoryTlEn = request.ServiceCategory.ServiceCategoryTlEn;
                customerRequest.ServiceCategoryTlAr = request.ServiceCategory.ServiceCategoryTlAr;


                if (request.Technician!=null)
                {
                    customerRequest.TechnicianId = request.TechnicianId;
                    customerRequest.TechnicianFullNameAr = request.Technician.FullNameAr;
                    customerRequest.TechnicianFullNameEn = request.Technician.FullNameEn;
                    customerRequest.TechnicianFullAddress = request.Technician.FullAddress;
                    customerRequest.TechnicianMobile = request.Technician.Mobile;
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
