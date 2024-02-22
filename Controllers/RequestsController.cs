using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Models;

namespace HomeService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class RequestsController : Controller
    {
        private HomeServiceContext _context;

        public RequestsController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var request = _context.Request.Select(i => new {
                i.RequestId,
                i.CustomerId,
                i.RequestDate,
                i.ScheduleDate,
                i.RequestStateId,
                i.ContractId,
                i.IssueDescription,
                i.Remarks,
                i.TechnicianId,
                i.Technician,
                i.TechDescription,
                i.TechDiagnosis,
                i.TechFixes,
                i.SparePartsDescription,
                i.IsClosd,
                i.VisitCost,
                i.ServiceCategoryId,
                i.Customer,
                i.RequestState,
                i.ServiceCategory
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "RequestId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(request, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Request();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Request.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.RequestId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Request.FirstOrDefaultAsync(item => item.RequestId == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Request.FirstOrDefaultAsync(item => item.RequestId == key);

            _context.Request.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> CustomerLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Customer
                         orderby i.FullNameAr
                         select new {
                             Value = i.CustomerId,
                             Text = i.FullNameAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> TechnicianLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Technician
                         orderby i.FullNameEn
                         select new {
                             Value = i.TechnicianId,
                             Text = i.FullNameEn
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> RequestStateLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.RequestState
                        
                         select new {
                             Value = i.RequestStateId,
                             Text = i.RequestStateEn
                         };
            var a = new
            {
                Value = 0,
                Text = "All"
            };
            lookup.Append(a);
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ServiceCategoryLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.ServiceCategory
                         orderby i.ServiceCategoryTlEn
                         select new {
                             Value = i.ServiceCategoryId,
                             Text = i.ServiceCategoryTlEn
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ContractLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Contract
                         orderby i.ContractSerial
                         select new {
                             Value = i.ContractId,
                             Text = i.ContractSerial
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Request model, IDictionary values) {
            string REQUEST_ID = nameof(Models.Request.RequestId);
            string CUSTOMER_ID = nameof(Models.Request.CustomerId);
            string REQUEST_DATE = nameof(Models.Request.RequestDate);
            string SCHEDULE_DATE = nameof(Models.Request.ScheduleDate);
            string REQUEST_STATE_ID = nameof(Models.Request.RequestStateId);
            string CONTRACT_ID = nameof(Models.Request.ContractId);
            string ISSUE_DESCRIPTION = nameof(Models.Request.IssueDescription);
            string REMARKS = nameof(Models.Request.Remarks);
            string TECHNICIAN_ID = nameof(Models.Request.TechnicianId);
            string TECH_DESCRIPTION = nameof(Models.Request.TechDescription);
            string TECH_DIAGNOSIS = nameof(Models.Request.TechDiagnosis);
            string TECH_FIXES = nameof(Models.Request.TechFixes);
            string SPARE_PARTS_DESCRIPTION = nameof(Models.Request.SparePartsDescription);
            string IS_CLOSD = nameof(Models.Request.IsClosd);
            string VISIT_COST = nameof(Models.Request.VisitCost);
            string SERVICE_CATEGORY_ID = nameof(Models.Request.ServiceCategoryId);

            if(values.Contains(REQUEST_ID)) {
                model.RequestId = Convert.ToInt32(values[REQUEST_ID]);
            }

            if(values.Contains(CUSTOMER_ID)) {
                model.CustomerId = Convert.ToInt32(values[CUSTOMER_ID]);
            }

            if(values.Contains(REQUEST_DATE)) {
                model.RequestDate = values[REQUEST_DATE] != null ? Convert.ToDateTime(values[REQUEST_DATE]) : (DateTime?)null;
            }

            if(values.Contains(SCHEDULE_DATE)) {
                model.ScheduleDate = values[SCHEDULE_DATE] != null ? Convert.ToDateTime(values[SCHEDULE_DATE]) : (DateTime?)null;
            }

            if(values.Contains(REQUEST_STATE_ID)) {
                model.RequestStateId = Convert.ToInt32(values[REQUEST_STATE_ID]);
            }

            if(values.Contains(CONTRACT_ID)) {
                model.ContractId = values[CONTRACT_ID] != null ? Convert.ToInt32(values[CONTRACT_ID]) : (int?)null;
            }

            if(values.Contains(ISSUE_DESCRIPTION)) {
                model.IssueDescription = Convert.ToString(values[ISSUE_DESCRIPTION]);
            }

            if(values.Contains(REMARKS)) {
                model.Remarks = Convert.ToString(values[REMARKS]);
            }

            if(values.Contains(TECHNICIAN_ID)) {
                model.TechnicianId = values[TECHNICIAN_ID] != null ? Convert.ToInt32(values[TECHNICIAN_ID]) : (int?)null;
            }

            if(values.Contains(TECH_DESCRIPTION)) {
                model.TechDescription = Convert.ToString(values[TECH_DESCRIPTION]);
            }

            if(values.Contains(TECH_DIAGNOSIS)) {
                model.TechDiagnosis = Convert.ToString(values[TECH_DIAGNOSIS]);
            }

            if(values.Contains(TECH_FIXES)) {
                model.TechFixes = Convert.ToString(values[TECH_FIXES]);
            }

            if(values.Contains(SPARE_PARTS_DESCRIPTION)) {
                model.SparePartsDescription = Convert.ToString(values[SPARE_PARTS_DESCRIPTION]);
            }

            if(values.Contains(IS_CLOSD)) {
                model.IsClosd = values[IS_CLOSD] != null ? Convert.ToBoolean(values[IS_CLOSD]) : (bool?)null;
            }

            if(values.Contains(VISIT_COST)) {
                model.VisitCost = values[VISIT_COST] != null ? Convert.ToDouble(values[VISIT_COST], CultureInfo.InvariantCulture) : (double?)null;
            }

           

            if(values.Contains(SERVICE_CATEGORY_ID)) {
                model.ServiceCategoryId = values[SERVICE_CATEGORY_ID] != null ? Convert.ToInt32(values[SERVICE_CATEGORY_ID]) : (int?)null;
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}