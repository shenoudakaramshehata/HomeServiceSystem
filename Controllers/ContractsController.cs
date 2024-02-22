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
using Microsoft.AspNetCore.Localization;

namespace HomeService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ContractsController : Controller
    {
        private HomeServiceContext _context;

        public ContractsController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var contract = _context.Contract.Select(i => new {
                i.ContractId,
                i.ContractSerial,
                i.CustomerId,
                i.ContractTypeId,
                i.UnitId,
                i.StartDate,
                i.EndDate,
                i.Amount,
                i.Remarks
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ContractId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(contract, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Contract();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Contract.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ContractId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Contract.FirstOrDefaultAsync(item => item.ContractId == key);
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
            var model = await _context.Contract.FirstOrDefaultAsync(item => item.ContractId == key);

            _context.Contract.Remove(model);
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IActionResult> CustomerLookup(DataSourceLoadOptions loadOptions)
        {
            var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var BrowserCulture = locale.RequestCulture.UICulture.ToString();
            if (BrowserCulture == "en-US")
            {
                var lookupEn = from i in _context.Customer
                               orderby i.FullNameEn
                               select new
                               {
                                   Value = i.CustomerId,
                                   Text = i.FullNameEn
                               };
                return Json(await DataSourceLoader.LoadAsync(lookupEn, loadOptions));
            }
            var lookupAr = from i in _context.Customer
                           orderby i.FullNameAr
                           select new
                           {
                               Value = i.CustomerId,
                               Text = i.FullNameAr
                           };
            return Json(await DataSourceLoader.LoadAsync(lookupAr, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ContractTypeLookup(DataSourceLoadOptions loadOptions)
        {
            var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var BrowserCulture = locale.RequestCulture.UICulture.ToString();
            if (BrowserCulture == "en-US")
            {
                var lookupEn = from i in _context.ContractType
                               orderby i.ContractTypeTlEn
                               select new
                               {
                                   Value = i.ContractTypeId,
                                   Text = i.ContractTypeTlEn
                               };
                return Json(await DataSourceLoader.LoadAsync(lookupEn, loadOptions));
            }
            var lookupAr = from i in _context.ContractType
                           orderby i.ContractTypeTlAr
                           select new
                           {
                               Value = i.ContractTypeId,
                               Text = i.ContractTypeTlAr
                           };
            return Json(await DataSourceLoader.LoadAsync(lookupAr, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> UnitLookup(DataSourceLoadOptions loadOptions)
        {

            var lookup = from i in _context.Unit
                         orderby i.UnitTile
                         select new
                         {
                             Value = i.UnitId,
                             Text = i.UnitTile
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> ServiceLookup(DataSourceLoadOptions loadOptions)
        {
            var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var BrowserCulture = locale.RequestCulture.UICulture.ToString();
            if (BrowserCulture == "en-US")
            {
                var lookupEn = from i in _context.Service
                               orderby i.ServiceTlEn
                               select new
                               {
                                   Value = i.ServiceId,
                                   Text = i.ServiceTlEn
                               };
                return Json(await DataSourceLoader.LoadAsync(lookupEn, loadOptions));
            }
            var lookupAr = from i in _context.Service
                           orderby i.ServiceTlAr
                           select new
                           {
                               Value = i.ServiceId,
                               Text = i.ServiceTlAr
                           };
            return Json(await DataSourceLoader.LoadAsync(lookupAr, loadOptions));
        }

        private void PopulateModel(Contract model, IDictionary values) {
            string CONTRACT_ID = nameof(Contract.ContractId);
            string CONTRACT_SERIAL = nameof(Contract.ContractSerial);
            string CUSTOMER_ID = nameof(Contract.CustomerId);
            string CONTRACT_TYPE_ID = nameof(Contract.ContractTypeId);
            string UNIT_ID = nameof(Contract.UnitId);
            string START_DATE = nameof(Contract.StartDate);
            string END_DATE = nameof(Contract.EndDate);
            string AMOUNT = nameof(Contract.Amount);
            string REMARKS = nameof(Contract.Remarks);

            if(values.Contains(CONTRACT_ID)) {
                model.ContractId = Convert.ToInt32(values[CONTRACT_ID]);
            }

            if(values.Contains(CONTRACT_SERIAL)) {
                model.ContractSerial = Convert.ToString(values[CONTRACT_SERIAL]);
            }

            if(values.Contains(CUSTOMER_ID)) {
                model.CustomerId = Convert.ToInt32(values[CUSTOMER_ID]);
            }

            if(values.Contains(CONTRACT_TYPE_ID)) {
                model.ContractTypeId = values[CONTRACT_TYPE_ID] != null ? Convert.ToInt32(values[CONTRACT_TYPE_ID]) : (int?)null;
            }

            if(values.Contains(UNIT_ID)) {
                model.UnitId = Convert.ToInt32(values[UNIT_ID]);
            }

            if(values.Contains(START_DATE)) {
                model.StartDate = values[START_DATE] != null ? Convert.ToDateTime(values[START_DATE]) : (DateTime?)null;
            }

            if(values.Contains(END_DATE)) {
                model.EndDate = values[END_DATE] != null ? Convert.ToDateTime(values[END_DATE]) : (DateTime?)null;
            }

            if(values.Contains(AMOUNT)) {
                model.Amount = values[AMOUNT] != null ? Convert.ToDouble(values[AMOUNT], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(REMARKS)) {
                model.Remarks = Convert.ToString(values[REMARKS]);
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