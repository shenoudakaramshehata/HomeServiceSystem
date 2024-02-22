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

namespace HomeService.Areas.Admin.Pages.Contracts
{
    public class AddModel : PageModel
    {

        private readonly HomeServiceContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;




        public AddModel(HomeServiceContext context, IWebHostEnvironment hostEnvironment
            , IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            _hostEnvironment = hostEnvironment;

        }

        [BindProperty]
        public Contract model { get; set; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost(Contract contractModel)
        {
            try
            {
                List<int> ServiceList = new List<int>();
                string Ids = Request.Form["ServiceIds"];
                ServiceList = Ids.Split(',').Select(Int32.Parse).ToList();
                _context.Contract.Add(contractModel);
                _context.SaveChanges();

                for (int i = 0; i < ServiceList.Count; i++)
                {
                    var ins = new ContractService();
                    ins.ContractId = contractModel.ContractId;
                    ins.ServiceId = ServiceList[i];
                    _context.ContractService.Add(ins);

                }
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Contract Added successfully");


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
