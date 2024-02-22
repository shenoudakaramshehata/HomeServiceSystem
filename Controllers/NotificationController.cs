using HomeService.Data;
using HomeService.Models;
using HomeService.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Entities.Notification;

namespace HomeService.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]

    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly HomeServiceContext _context;

        public NotificationController(INotificationService notificationService, HomeServiceContext context)
        {
            _notificationService = notificationService;
            _context = context;
        }

        [Route("SendNotification/{technicianId}")]
        [HttpGet]
        public async Task<IActionResult> SendNotification(int technicianId)
        {
            var technician = _context.Technician.FirstOrDefault(d => d.TechnicianId == technicianId);

            var notificationModel = new NotificationModel();
            notificationModel.DeviceId = technician.DeviceId;
            if (technician.IsAndroiodDevice.HasValue)
            {
                notificationModel.IsAndroiodDevice = technician.IsAndroiodDevice.Value;

            }
            notificationModel.Title = "Hello" + " " + technician.FullNameAr;
            notificationModel.Body = "You Have A New Order";


            var result = await _notificationService.SendNotification(notificationModel);
            if (result.IsSuccess)
            {
                return Ok(true);

            }
            return Ok(false);

        }
    
    }
}
