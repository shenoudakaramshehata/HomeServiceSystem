using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.Customers
{
    public class ReceiptsModel : PageModel
    {

        private readonly HomeServiceContext _context;
        private readonly IToastNotification _toastNotification;
        public ReceiptsModel(HomeServiceContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        [BindProperty]
        public Customer customer { get; set; }
        [BindProperty]
        public Contract contract { get; set; }
        [BindProperty]
        public List<ContractService> contractService { get; set; }

        [BindProperty]
        public double receiptsTotal { get; set; }

        public IActionResult OnGet(int id)
        {
            try
            {
                contract = _context.Contract.Include(c => c.Unit).FirstOrDefault(c => c.ContractId == id);

                if (contract == null)
                {
                    return Redirect("../Error");
                }

                customer = _context.Customer
                    .Include(c => c.Area)
                    .Include(c => c.Area.City)
                    .Include(c => c.Nationality)
                    .FirstOrDefault(m => m.CustomerId == contract.CustomerId);
                receiptsTotal = _context.Receipt.Where(c => c.ContractId == id).ToList().Sum(c => c.Amount);
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");

            }



            return Page();
        }

    }
}

