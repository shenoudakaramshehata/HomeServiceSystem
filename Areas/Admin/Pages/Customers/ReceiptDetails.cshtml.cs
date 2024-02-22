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
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.Customers
{
    public class ReceiptDetails : PageModel
    {

        private readonly HomeServiceContext _context;
        private readonly IToastNotification _toastNotification;
        public ReceiptDetails(HomeServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        [BindProperty]
        public Customer customer { get; set; }
        [BindProperty]
        public Contract contract { get; set; }
        [BindProperty]
        public  Receipt receipt { get; set; }

        public IActionResult OnGet(int id)
        {
            try
            {
                receipt = _context.Receipt.Find(id);

                if (receipt == null)
                {
                    return Redirect("../Error");
                }
                contract = _context.Contract.Include(c => c.Unit).FirstOrDefault(c => c.ContractId == receipt.ContractId);

                customer = _context.Customer
                    .Include(c => c.Area)
                    .Include(c => c.Area.City)
                    .Include(c => c.Nationality)
                    .FirstOrDefault(m => m.CustomerId == contract.CustomerId);
                
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");

            }



            return Page();
        }

    }
}
