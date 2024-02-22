using HomeService.Data;
using HomeService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Entities;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Http;
using System.Text;

namespace HomeService.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class MobileController : Controller
    {

        private readonly HomeServiceContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IEmailSender _emailSender;

        public HttpClient HttpClient { get; }

        public MobileController(HomeServiceContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            ApplicationDbContext db, RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostEnvironment,
            IEmailSender emailSender, HttpClient httpClient)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
            _roleManager = roleManager;
            _hostEnvironment = hostEnvironment;
            _emailSender = emailSender;
            HttpClient = httpClient;
        }




        [HttpGet]
        public async Task<ActionResult<ApplicationUser>> Login([FromQuery] string Email, [FromQuery] string Password)
        {

            var user = await _userManager.FindByEmailAsync(Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, Password, true);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null && roles.FirstOrDefault() == "Customer")
                    {
                        var customer = await _context.Customer.FindAsync(user.EntityId);

                        return Ok(new { Status = true, Message = "User Login successfully!", user, customer });
                    }
                    if (roles != null && roles.FirstOrDefault() == "Technician")
                    {
                        var technician = await _context.Technician.FindAsync(user.EntityId);

                        return Ok(new { Status = true, Message = "User Login successfully!", user, technician });
                    }
                }

                return Ok(new { Status = false });

            }
            return Ok(new { Status = false });

        }

        [HttpPost]
        public async Task<IActionResult> CustomerRegister([FromBody] CustomerVM customerVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new { Status = false, Message = "Enter Correct Data" });

                }
                var userExists = await _userManager.FindByEmailAsync(customerVM.Email);
                if (userExists != null)
                    return Ok(new { Status = false, Message = "User already exists!" });
                var customer = new Customer();
                if (customerVM.Pic != null)
                {
                    var bytes = Convert.FromBase64String(customerVM.Pic);
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Customer");
                    string uniqePictureName = Guid.NewGuid() + ".jpeg";
                    string uploadedImagePath = Path.Combine(uploadFolder, uniqePictureName);
                    using (var imageFile = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                    customer.Pic = uniqePictureName;
                }
                customer.Mobile = customerVM.Mobile;
                customer.Tele1 = customerVM.Tele1;
                customer.Tele2 = customerVM.Tele2;
                customer.FullNameAr = customerVM.FullNameAr;
                customer.FullNameEn = customerVM.FullNameEn;
                customer.AreaId = customerVM.AreaId;
                customer.CivilId = customerVM.CivilId;
                customer.Block = customerVM.Block;
                customer.Avenue = customerVM.Avenue;
                customer.Street = customerVM.Street;
                customer.BuildingNo = customerVM.BuildingNo;
                customer.Floor = customerVM.Floor;
                customer.Flat = customerVM.Flat;
                customer.NationalityId = customerVM.NationalityId;
                customer.PassportNo = customerVM.PassportNo;
                customer.Remarks = customerVM.Remarks;
                customer.Email = customerVM.Email;
                _context.Customer.Add(customer);
                _context.SaveChanges();
                if (!(customer.CustomerId > 0))
                {
                    return Ok(new { Status = false, Message = "User creation failed! Please check user details and try again." });

                }


                var user = new ApplicationUser
                {
                    UserName = customerVM.Email,
                    Email = customerVM.Email,
                    PhoneNumber = customerVM.Mobile,
                    EntityId = customer.CustomerId,
                    EntityName = 2

                };

                var result = await _userManager.CreateAsync(user, customerVM.Password);

                if (!result.Succeeded)
                {
                    _context.Customer.Remove(customer);
                    _context.SaveChanges();
                    return Ok(new { Status = false, Message = "User creation failed! Please check user details and try again." });

                }

                await _userManager.AddToRoleAsync(user, "Customer");
                return Ok(new { Status = "Success", Message = "User created successfully!", user, customer });
            }
            catch (Exception)
            {

                return Ok(new { Status = false, Message = "Something went wrong." });
            }


        }

        [HttpGet]
        public async Task<IActionResult> GetServicesCategoryList()
        {
            try
            {
                var list = await _context.ServiceCategory.ToListAsync();
                return Ok(new { list });
            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAreaList()
        {
            try
            {
                var list = await _context.Area.ToListAsync();
                return Ok(new { list });
            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetNationalityList()
        {
            try
            {
                var list = await _context.Nationality.ToListAsync();
                return Ok(new { list });
            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });
            }

        }

        [HttpPost]
        public IActionResult AddRequest([FromBody] Request request)
        {
            try
            {
                request.RequestStateId = 1;
                request.RequestDate = DateTime.Now;
                _context.Request.Add(request);
                _context.SaveChanges();
                if (request.RequestId > 0)
                {
                    var requestLog = new RequestLog
                    {
                        RequestId = request.RequestId,
                        RequestStateId = 1,
                        VDate = DateTime.Now,
                    };
                    _context.RequestLog.Add(requestLog);
                    _context.SaveChanges();
                    return Ok(new { Status = true, Message = "Request created successfully!", request });

                }


                return Ok(new { Status = false, Message = "Request creation failed!", });

            }
            catch (Exception)
            {

                return Ok(new { Status = false, Message = "Something went wrong" });

            }

        }

        [HttpGet]
        public IActionResult GetCustomerById([FromQuery] int customerId)
        {
            try
            {
                var customer = _context.Customer
        .Include(c => c.Area)
        .Include(c => c.Area.City)
        .Include(c => c.Nationality)
        .FirstOrDefault(m => m.CustomerId == customerId);
                return Ok(new { Status = true, customer });
            }
            catch (Exception)
            {

                return Ok(new { Status = false, Message = "Something went wrong." });

            }

        }


        [HttpPost]
        public IActionResult EditCustomer([FromBody] CustomerVM model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { Status = false, Message = "Enter Correct Data" });

            }

            try
            {

                var customer = _context.Customer.FirstOrDefault(e => e.CustomerId == model.CustomerId);
                if (customer == null)
                {
                    return Ok(new { Status = false, Message = "Model Not Found" });
                }

                if (model.Pic != null && customer.Pic != model.Pic)
                {
                    var bytes = Convert.FromBase64String(model.Pic);
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Customer");
                    string uniqePictureName = Guid.NewGuid() + ".jpeg";
                    string uploadedImagePath = Path.Combine(uploadFolder, uniqePictureName);
                    using (var imageFile = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                    customer.Pic = uniqePictureName;
                }

                customer.Mobile = model.Mobile;
                customer.Tele1 = model.Tele1;
                customer.Tele2 = model.Tele2;
                customer.FullNameAr = model.FullNameAr;
                customer.FullNameEn = model.FullNameEn;
                customer.AreaId = model.AreaId;
                customer.CivilId = model.CivilId;
                customer.Block = model.Block;
                customer.Avenue = model.Avenue;
                customer.Street = model.Street;
                customer.BuildingNo = model.BuildingNo;
                customer.Floor = model.Floor;
                customer.Flat = model.Flat;
                customer.NationalityId = model.NationalityId;
                customer.PassportNo = model.PassportNo;
                customer.Remarks = model.Remarks;
                _context.SaveChanges();
                return Ok(new { Status = true, Message = "customer Edit successfully" });

            }
            catch (Exception)
            {
                return Ok(new { Status = false, Message = "Something went wrong" });


            }

        }
        [HttpGet]
        public async Task<IActionResult> GetRequestList([FromQuery] int requestStateId)
        {
            try
            {
                var list = await _context.Request.Include(c => c.Customer).Where(c => c.RequestStateId == requestStateId).ToListAsync();
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });
            }

        }

        [HttpGet]
        public IActionResult GetSystemConfiguration()

        {
            try
            {
                var list = _context.Configuration.FirstOrDefault();
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }

        }
        [HttpGet]
        public IActionResult GetFAQList()
        {
            try
            {
                var list = _context.FAQ.ToList();
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }

        }
        [HttpGet]
        public IActionResult GetPageContent([FromQuery] int PageContentId)
        {

            try
            {
                var list = _context.PageContent.FirstOrDefault(c => c.PageContentId == PageContentId);
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }
        }
        [HttpGet]
        public IActionResult GetRequestsByCustomerId([FromQuery] int CustomerId)
        {

            try
            {
                var list = _context.Request.Where(c => c.CustomerId == CustomerId).Select(i => new
                {
                    i.RequestId,
                    i.CustomerId,
                    i.Customer,
                    i.RequestDate,
                    i.ScheduleDate,
                    i.RequestStateId,
                    i.RequestState,
                    i.ContractId,
                    i.IssueDescription,
                    i.Remarks,
                    i.TechnicianId,
                    i.TechDescription,
                    i.TechDiagnosis,
                    i.TechFixes,
                    i.SparePartsDescription,
                    i.IsClosd,
                    i.VisitCost,
                    i.isPaid,
                    i.Cost,
                    i.ServiceCategoryId
                });
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }
        }

        [HttpPost]
        public IActionResult ContactUs([FromBody] ContactUs Model)
        {
            try
            {
                Model.TransDate = DateTime.Now;
                _context.ContactUs.Add(Model);
                _context.SaveChanges();
                return Ok(new { status = true, Message = "Message Sent Successfully" });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }

        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new { status = false, Message = "model not Valid" });


                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return Ok(new { status = false, Message = "User not found" });

                }
                var Result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!Result.Succeeded)
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }
                    return Ok(new { status = false, message = ModelState });

                }

                return Ok(new { status = true, Message = "Password Changed" });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }


        }

        [HttpPost]
        public IActionResult EditTechnician([FromBody] TechnicianVM model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { Status = false, Message = "Enter Correct Data" });

            }

            try
            {

                var technician = _context.Technician.FirstOrDefault(e => e.TechnicianId == model.TechnicianId);
                if (technician == null)
                {
                    return Ok(new { Status = false, Message = "Model Not Found" });
                }

                if (model.Pic != null && technician.Pic != model.Pic)
                {
                    var bytes = Convert.FromBase64String(model.Pic);
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Technician");
                    string uniqePictureName = Guid.NewGuid() + ".jpeg";
                    string uploadedImagePath = Path.Combine(uploadFolder, uniqePictureName);
                    using (var imageFile = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                    technician.Pic = uniqePictureName;
                }

                technician.Mobile = model.Mobile;
                technician.Tele = model.Tele;
                technician.FullNameAr = model.FullNameAr;
                technician.FullNameEn = model.FullNameEn;
                technician.FullAddress = model.FullAddress;
                technician.CivilId = model.CivilId;
                technician.NationalityId = model.NationalityId;
                technician.PassportNo = model.PassportNo;
                technician.Remarks = model.Remarks;
                _context.SaveChanges();
                return Ok(new { Status = true, Message = "technician Edit successfully" });

            }
            catch (Exception)
            {
                return Ok(new { Status = false, Message = "Something went wrong" });


            }

        }
        [HttpGet]
        public IActionResult GetTechnicianById([FromQuery] int technicianId)
        {
            try
            {
                var technician = _context.Technician
        .Include(c => c.Nationality)
        .FirstOrDefault(m => m.TechnicianId == technicianId);
                return Ok(new { Status = true, technician });
            }
            catch (Exception)
            {

                return Ok(new { Status = false, Message = "Something went wrong." });

            }

        }
        [HttpGet]
        public IActionResult GetNewRequestsByTechnicianId([FromQuery] int technicianId)
        {

            try
            {
                var list = _context.Request.Where(c => c.TechnicianId == technicianId && c.RequestStateId == 2).Select(i => new
                {
                    i.RequestId,
                    i.CustomerId,
                    i.Customer,
                    i.RequestDate,
                    i.ScheduleDate,
                    i.RequestStateId,
                    i.RequestState,
                    i.ContractId,
                    i.IssueDescription,
                    i.Remarks,
                    i.TechnicianId,
                    i.TechDescription,
                    i.TechDiagnosis,
                    i.TechFixes,
                    i.SparePartsDescription,
                    i.IsClosd,
                    i.VisitCost,

                    i.ServiceCategoryId,
                    i.Technician
                });
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }
        }
        [HttpGet]
        public IActionResult GetAcceptedRequestsByTechnicianId([FromQuery] int technicianId)
        {

            try
            {
                var list = _context.Request.Where(c => c.TechnicianId == technicianId && c.RequestStateId == 3).Select(i => new
                {
                    i.RequestId,
                    i.CustomerId,
                    i.Customer,
                    i.RequestDate,
                    i.ScheduleDate,
                    i.RequestStateId,
                    i.RequestState,
                    i.ContractId,
                    i.IssueDescription,
                    i.Remarks,
                    i.TechnicianId,
                    i.TechDescription,
                    i.TechDiagnosis,
                    i.TechFixes,
                    i.SparePartsDescription,
                    i.IsClosd,
                    i.VisitCost,
                    i.Cost,
                    i.ServiceCategoryId,
                    i.Technician
                });
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }
        }
        [HttpGet]
        public IActionResult GetRejectedRequestsByTechnicianId([FromQuery] int technicianId)
        {

            try
            {
                var list = _context.Request.Where(c => c.TechnicianId == technicianId && c.RequestStateId == 4).Select(i => new
                {
                    i.RequestId,
                    i.CustomerId,
                    i.Customer,
                    i.RequestDate,
                    i.ScheduleDate,
                    i.RequestStateId,
                    i.RequestState,
                    i.ContractId,
                    i.IssueDescription,
                    i.Remarks,
                    i.TechnicianId,
                    i.TechDescription,
                    i.TechDiagnosis,
                    i.TechFixes,
                    i.SparePartsDescription,
                    i.IsClosd,
                    i.VisitCost,

                    i.ServiceCategoryId,
                    i.Technician
                });
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }
        }
        [HttpGet]

        public IActionResult GetCloseRequestsByTechnicianId([FromQuery] int technicianId)
        {

            try
            {
                var list = _context.Request.Where(c => c.TechnicianId == technicianId && c.RequestStateId == 5).Select(i => new
                {
                    i.RequestId,
                    i.CustomerId,
                    i.Customer,
                    i.RequestDate,
                    i.ScheduleDate,
                    i.RequestStateId,
                    i.RequestState,
                    i.ContractId,
                    i.IssueDescription,
                    i.Remarks,
                    i.TechnicianId,
                    i.TechDescription,
                    i.TechDiagnosis,
                    i.TechFixes,
                    i.SparePartsDescription,
                    i.IsClosd,
                    i.VisitCost,

                    i.ServiceCategoryId,
                    i.Technician
                });
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }
        }



        [HttpPost]
        public IActionResult AcceptOrRejectRequestByTechnician([FromBody] RequestVM requestVM)
        {


            if (!ModelState.IsValid)
            {
                return Ok(new { Status = false, Message = "Enter Correct Data" });

            }

            try
            {

                var request = _context.Request.FirstOrDefault(c => c.RequestId == requestVM.RequestId && c.TechnicianId == requestVM.TechnicianId);
                if (request == null)
                {
                    return Ok(new { Status = false, Message = "Request Not Found" });

                }
                if (!requestVM.State.HasValue)
                {
                    return Ok(new { Status = false, Message = "send State" });
                }
                if (requestVM.State.Value)

                    request.RequestStateId = 3;
                else
                    request.RequestStateId = 4;

                _context.Update(request);
                _context.SaveChanges();

                var requestLog = new RequestLog
                {
                    RequestId = request.RequestId,
                    RequestStateId = request.RequestStateId,
                    VDate = DateTime.Now,
                };
                _context.RequestLog.Add(requestLog);
                _context.SaveChanges();
                return Ok(new { Status = true, Message = "Request Edit successfully" });

            }
            catch (Exception)
            {
                return Ok(new { Status = false, Message = "Something went wrong" });


            }

        }

        [HttpPost]
        public IActionResult DescripeRequestByTechnician([FromBody] RequestVM requestVM)
        {

            if (!ModelState.IsValid)
            {
                return Ok(new { Status = false, Message = "Enter Correct Data" });
            }
            try
            {
                var request = _context.Request.FirstOrDefault(c => c.RequestId == requestVM.RequestId && c.TechnicianId == requestVM.TechnicianId);
                if (request == null)
                {
                    return Ok(new { Status = false, Message = "Request Not Found" });
                }
                request.TechDescription = requestVM.TechDescription;
                request.TechDiagnosis = requestVM.TechDiagnosis;
                request.TechFixes = requestVM.TechFixes;
                _context.Update(request);
                foreach (var item in requestVM.RequestSpareParts)
                {
                    var ins = new RequestSpareParts();
                    var sparePart = _context.SparePart.Find(item.SparePartId);
                    ins.RequestId = requestVM.RequestId.Value;
                    ins.SparePartId = sparePart.SparePartId;
                    ins.QTY = item.QTY;
                    ins.Price = sparePart.Price;
                    ins.Total = item.QTY * sparePart.Price;
                    _context.RequestSpareParts.Add(ins);

                }
                _context.SaveChanges();
                return Ok(new { Status = true, Message = "Request Edit successfully" });

            }
            catch (Exception)
            {
                return Ok(new { Status = false, Message = "Something went wrong" });


            }

        }

        [HttpGet]
        public async Task<IActionResult> GetSparePartList()
        {
            try
            {
                var list = await _context.SparePart.ToListAsync();
                return Ok(new { list });
            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });
            }

        }


        [HttpGet]

        public IActionResult AddDeviceIdToTechnician([FromQuery] int technicianId, [FromQuery] string deviceId, [FromQuery] bool IsAndroiodDevice)
        {

            try
            {
                var technician = _context.Technician.Find(technicianId);
                if (technician == null)
                {
                    return Ok(new { status = false, message = "technician not Found" });

                }
                technician.DeviceId = deviceId;
                technician.IsAndroiodDevice = IsAndroiodDevice;
                _context.Update(technician);
                _context.SaveChanges();
                return Ok(new { status = true, technician, message = "deviceId Added To technician " });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }
        }



        [HttpGet]
        public IActionResult CloseRequestByTechnician([FromQuery] int requestId, [FromQuery] int technicianId)
        {
            try
            {
                var request = _context.Request.FirstOrDefault(c => c.RequestId == requestId && c.TechnicianId == technicianId);
                if (request == null)
                {
                    return Ok(new { Status = false, Message = "Request Not Found" });
                }
                request.IsClosd = true;
                request.RequestStateId = 5;
                _context.Update(request);
                _context.SaveChanges();
                var requestLog = new RequestLog
                {
                    RequestId = request.RequestId,
                    RequestStateId = request.RequestStateId,
                    VDate = DateTime.Now,
                };
                _context.RequestLog.Add(requestLog);
                _context.SaveChanges();
                return Ok(new { Status = true, Message = "Request Edit successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = false, Message = "Something went wrong", ex });
            }

        }


        [HttpGet]
        public IActionResult GetRequestSparePartList([FromQuery] int requestId)
        {
            try
            {
                var list = _context.RequestSpareParts.Where(c => c.RequestId == requestId).Select(i => new
                {
                    i.RequestSparePartsId,
                    i.RequestId,
                    i.SparePartId,
                    i.QTY,
                    i.Price,
                    i.Total,
                    i.SparePart
                });
                return Ok(new { list });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });
            }
        }


        [HttpGet]
        public IActionResult GetRequestSparePartTotalCost([FromQuery] int requestId)
        {
            try
            {
                var sum = _context.RequestSpareParts.Where(c => c.RequestId == requestId).Select(i => new
                {
                    i.RequestSparePartsId,
                    i.RequestId,
                    i.SparePartId,
                    i.QTY,
                    i.Price,
                    i.Total,
                    i.SparePart
                }).Sum(c => c.Total);
                return Ok(new { sum });
            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });
            }

        }


        [HttpGet]
        public IActionResult ServiceCategorySearch([FromQuery] string searchText)
        {
            try
            {
                var list = _context.ServiceCategory.Where(c => c.ServiceCategoryTlAr.Contains(searchText) || c.ServiceCategoryTlEn.Contains(searchText)).ToList();
                return Ok(new { list });
            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });
            }

        }
        
        [HttpPut]
        public IActionResult AddRequestCost(int requestId,double cost)
        {
            if (requestId==0)
            {
                return Ok(new { status = false, message = "Enter request ID" });
            }
            if (cost<0)
            {
                return Ok(new { status = false, message = "Cost Must be Grater Than 0" });
            }
            try
            {
                var request = _context.Request.Where(c => c.RequestId==requestId).FirstOrDefault();
                if (request==null)
                {
                    return Ok(new { status = false, message = "Request Not Found..." });

                }
                if (request.RequestStateId!=3)
                {
                    return Ok(new { status = false, message = "Request Must Be Accepted by Technician..." });

                }
                request.Cost = cost;
                _context.Update(request);
                _context.SaveChanges();
                return Ok(new {status=true,message="Request Updated Successfully..." });
            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> RequestPay(int requestId, int paymentmethodId)
        {
            if (requestId != 0)
            {
                var request = _context.Request.Include(a => a.Customer).FirstOrDefault(a => a.RequestId == requestId);
                if (request == null)
                {
                    return Ok(new { status = false, message = "Request object not found..." });
                }
                if (request.Cost == 0)
                {
                    return Ok(new { status = false, message = "Technician must assigned cost..." });
                }
                if (paymentmethodId == 2)
                {
                    try
                    {
                        var requesturl = "https://api.upayments.com/test-payment";
                        var fields = new
                        {
                            merchant_id = "1201",
                            username = "test",
                            password = "test",
                            order_id = requestId,
                            total_price = request.Cost,
                            test_mode = 0,
                            CstFName = request.Customer.FullNameAr,
                            CstEmail = request.Customer.Email,
                            CstMobile = request.Customer.Mobile,
                            api_key = "jtest123",
                            success_url = "http://codewarenet-001-site10.dtempurl.com/success",
                            error_url = "http://codewarenet-001-site10.dtempurl.com/Failed",
                            //success_url = "https://localhost:44383/success",
                            //error_url = "https://localhost:44383/Failed"

                        };
                        var content = new StringContent(JsonConvert.SerializeObject(fields), Encoding.UTF8, "application/json");
                        var task = HttpClient.PostAsync(requesturl, content);
                        var result = await task.Result.Content.ReadAsStringAsync();
                        var paymenturl = JsonConvert.DeserializeObject<paymenturl>(result);
                        if (paymenturl.status == "success")
                        {
                            return Ok(new { Staust = "true", paymenturl = paymenturl.paymentURL, RequestId = request.RequestId });
                        }
                        else
                        {
                            return Ok(new { Staus = "false", reason = paymenturl.error_msg });
                        }

                    }



                    catch (Exception)
                    {

                        return Ok(new { status = false, message = "Something went wrong" });
                    }

                }

            }


            return Ok(new { status = false, message = "Enter RequestId..." });



        }
    }
}


