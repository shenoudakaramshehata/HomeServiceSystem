using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Entities;
using HomeService.Entities.Notification;
using HomeService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NToastNotify;

namespace HomeService.Areas.Admin.Pages.Requests
{
    public class EditModel : PageModel
    {
        private HomeServiceContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly INotificationService _notificationService;

        public EditModel(HomeServiceContext context, INotificationService notificationService, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            _notificationService = notificationService;


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
                    .Include(c => c.ServiceCategory)
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
                customerRequest.TechnicianId = request.TechnicianId;

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");

            }



            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int RequestId, int TechnicianId)
        {
           

            try
            {
                var model = _context.Request.FirstOrDefault(e => e.RequestId == RequestId);
                if (model == null)
                {
                    return Redirect("../Error");

                }
                if (model.TechnicianId != TechnicianId)
                {
                    model.TechnicianId = TechnicianId;
                    model.RequestStateId = 2;
                    _context.Attach(model).State = EntityState.Modified;
                    _context.SaveChanges();
                    if (request.TechnicianId > 0)
                    {
                        var requestLog = new RequestLog
                        {
                            RequestId = request.RequestId,
                            RequestStateId = 2,
                            VDate = DateTime.Now,
                        };
                        _context.RequestLog.Add(requestLog);
                        _context.SaveChanges();
                    }
                    var technician = _context.Technician.Find(request.TechnicianId);
                    var notificationModel = new NotificationModel();
                    notificationModel.DeviceId = technician.DeviceId;
                    notificationModel.IsAndroiodDevice = technician.IsAndroiodDevice.Value;
                    notificationModel.Title = "Hello" + " " + technician.FullNameAr;
                    notificationModel.Body = "You Have A New Order";
                    var result = await _notificationService.SendNotification(notificationModel);

                    //HttpClient _httpClient = new HttpClient();
                    //string url = $"{this.Request.Scheme}://{this.Request.Host}/Api/Notification/SendNotification/{request.TechnicianId}";
                    //var _http_response = _httpClient.GetAsync(url);
                    //_http_response.Wait();
                    //var _read_response = _http_response.Result.Content.ReadAsStringAsync();
                    //_read_response.Wait();
                    
                    

                    _toastNotification.AddSuccessToastMessage("Request Edited successfully");
                }
               

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();

            }

            return RedirectToPage("./Index");
        }

    }
}
