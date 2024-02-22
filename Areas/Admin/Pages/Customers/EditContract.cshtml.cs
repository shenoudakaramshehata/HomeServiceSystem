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
    public class EditContractModel : PageModel
    {

        private readonly HomeServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;




        public EditContractModel(HomeServiceContext context, IWebHostEnvironment hostEnvironment
            , IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            _hostEnvironment = hostEnvironment;

        }

        [BindProperty]
        public Contract contract { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<int> serviceSelected { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                contract = await _context.Contract.FirstOrDefaultAsync(m => m.ContractId == id);
                if (contract == null)
                {
                    return Redirect("../Error");
                }
                serviceSelected = _context.ContractService.Where(c => c.ContractId == id).Select(c => c.ServiceId).ToList();

            }

            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");

            }


            return Page();
        }


        public IActionResult OnPost(Contract model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {

                var modelExist = _context.Contract.Any(e => e.ContractId == model.ContractId);
                if (modelExist == false)
                {
                    return Redirect("../Error");

                }


                _context.Attach(model).State = EntityState.Modified;
                var list= _context.ContractService.Where(c => c.ContractId == model.ContractId).ToList();
                _context.ContractService.RemoveRange(list);
                _context.SaveChanges();
                List<int> ServiceList = new List<int>();
                string Ids = Request.Form["ServiceIds"];
                ServiceList = Ids.Split(',').Select(Int32.Parse).ToList();
                for (int i = 0; i < ServiceList.Count; i++)
                {
                    var ins = new ContractService();
                    ins.ContractId = model.ContractId;
                    ins.ServiceId = ServiceList[i];
                    _context.ContractService.Add(ins);

                }
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Contract Edited successfully");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();

            }

            return Redirect("./Contracts?id=" + model.CustomerId);

        }

    }
}
