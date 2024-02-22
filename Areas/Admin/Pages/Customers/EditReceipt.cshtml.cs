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
    public class EditReceiptModel : PageModel
    {

        private readonly HomeServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        public EditReceiptModel(HomeServiceContext context, IWebHostEnvironment hostEnvironment
            , IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Receipt  receipt{ get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                receipt = await _context.Receipt.FirstOrDefaultAsync(m => m.ReceiptId == id);
                if (receipt == null)
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
        public IActionResult OnPost(Receipt model)
        {
            

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var modelExist = _context.Receipt.Any(e => e.ReceiptId == model.ReceiptId);
                if (modelExist == false)
                {
                    return Redirect("../Error");
                }
                _context.Attach(model).State = EntityState.Modified;
                var list = _context.ContractService.Where(c => c.ContractId == model.ContractId).ToList();
                _context.ContractService.RemoveRange(list);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Receipt Edited successfully");
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();

            }
            return Redirect("./Receipts?id=" + model.ContractId);

        }
    }
}
