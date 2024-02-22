using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeService.Data;
using HomeService.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Localization;
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.Customers
{
    public class DeleteContract : PageModel
    {

        private HomeServiceContext _context;
        private readonly IToastNotification _toastNotification;


        public DeleteContract(HomeServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;

        }
        [BindProperty]
        public Customer customer { get; set; }
        [BindProperty]
        public Contract contract { get; set; }
        [BindProperty]
        public List <ContractService> contractService { get; set; }

       
        public IActionResult OnGet(int id)
        {
            try
            {
                contract = _context.Contract.Include(c => c.Unit).Include(c=>c.ContractType).FirstOrDefault(c => c.ContractId == id);

                if (contract == null)
                {
                    return Redirect("../Error");
                }

                customer = _context.Customer
                    .Include(c => c.Area)
                    .Include(c => c.Area.City)
                    .Include(c => c.Nationality)
                    .FirstOrDefault(m => m.CustomerId == contract.CustomerId);
                
            }
            catch (Exception)
            {

                throw;
            }



            return Page();
        }




        public async Task<IActionResult> OnPostAsync(int id)
        {
            int customerId = 0;

            try
            {
                var model = _context.Contract.Find(id);
                if (model == null)
                {
                    return Redirect("../Error");
                }
                customerId = model.CustomerId;
                if (_context.Receipt.Any(c => c.ContractId == id) || _context.Request.Any(c => c.ContractId == id))
                {
                    var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                    //if (BrowserCulture == "en-US")
                   
                        _toastNotification.AddErrorToastMessage("You cannot delete this Contract");

                    //else
                    //    _toastNotification.AddErrorToastMessage("لا يمكنك حذف هذا العقد ");

                    contract = _context.Contract.Include(c => c.Unit).FirstOrDefault(c => c.ContractId == id);


                    customer = _context.Customer
                        .Include(c => c.Area)
                        .Include(c => c.Area.City)
                        .Include(c => c.Nationality)
                        .FirstOrDefault(m => m.CustomerId == contract.CustomerId);
                    return Page();


                }

                var contractServivceLst = _context.ContractService.Where(c => c.ContractId == id).ToList();
                _context.ContractService.RemoveRange(contractServivceLst);
                _context.Contract.Remove(model);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Contrac Deleted successfully");


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

            return Redirect("./Contracts?id=" + customerId);

        }


    }
}
