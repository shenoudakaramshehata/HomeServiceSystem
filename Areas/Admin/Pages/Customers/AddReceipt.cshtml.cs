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
    public class AddReceiptModel : PageModel
    {

        private readonly HomeServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;




        public AddReceiptModel(HomeServiceContext context, IWebHostEnvironment hostEnvironment
            , IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            _hostEnvironment = hostEnvironment;

        }

        [BindProperty]
        public Contract contract { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {

                contract = await _context.Contract.Include(c=>c.Customer).FirstOrDefaultAsync(m => m.ContractId == id);
                if (contract == null)
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
        public IActionResult OnPost(Receipt receiptModel)
        {
            try
            {
                var serial = _context.Receipt.OrderByDescending(p => p.ContractId).FirstOrDefault()?.ReceiptSerial;
                if (serial==null)
                {
                    receiptModel.ReceiptSerial = "Rec1";
                }
                else
                receiptModel.ReceiptSerial = "Rec"+ (int.Parse(serial.Substring(3)) + 1).ToString();
                _context.Receipt.Add(receiptModel);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Receipt Added successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();
            }
            return Redirect("./Receipts?id=" + receiptModel.ContractId);

        }
    }
}
