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
    [ApiExplorerSettings(IgnoreApi =true)]
    public class CustomersController : Controller
    {
        private HomeServiceContext _context;

        public CustomersController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var customer = _context.Customer.Select(i => new {
                i.CustomerId,
                i.FullNameAr,
                i.CivilId,
                i.AreaId,
                i.Area,
                i.Area.City,
                i.Block,
                i.Avenue,
                i.Street,
                i.BuildingNo,
                i.Floor,
                i.Flat,
                i.NationalityId,
                i.Nationality,
                i.PassportNo,
                i.Mobile,
                i.Tele1,
                i.Tele2,
                i.Remarks,
                i.FullNameEn,
                i.Pic,
                i.Email
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CustomerId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(customer, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Customer();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Customer.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CustomerId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Customer.FirstOrDefaultAsync(item => item.CustomerId == key);
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
            var model = await _context.Customer.FirstOrDefaultAsync(item => item.CustomerId == key);

            _context.Customer.Remove(model);
            await _context.SaveChangesAsync();
        }



        [HttpGet]
        public async Task<IActionResult> AreaLookup(DataSourceLoadOptions loadOptions)
        {
            var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var BrowserCulture = locale.RequestCulture.UICulture.ToString();
            if (BrowserCulture == "en-US")
            {
                var lookupEn = from i in _context.Area
                               orderby i.AreaTlEn
                               select new
                               {
                                   Value = i.AreaId,
                                   Text = i.AreaTlAr
                               };
                return Json(await DataSourceLoader.LoadAsync(lookupEn, loadOptions));
            }
            var lookupAr = from i in _context.Area
                           orderby i.AreaTlAr
                           select new
                           {
                               Value = i.AreaId,
                               Text = i.AreaTlAr
                           };
            return Json(await DataSourceLoader.LoadAsync(lookupAr, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> NationalityLookup(DataSourceLoadOptions loadOptions) {
            var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var BrowserCulture = locale.RequestCulture.UICulture.ToString();
            if (BrowserCulture == "en-US")
            {
                var lookupEn = from i in _context.Nationality
                               orderby i.NationalityTlEn
                               select new
                               {
                                   Value = i.NationalityId,
                                   Text = i.NationalityTlEn
                               };
                return Json(await DataSourceLoader.LoadAsync(lookupEn, loadOptions));
            }
            var lookupAr = from i in _context.Nationality
                         orderby i.NationalityTlAr
                         select new {
                             Value = i.NationalityId,
                             Text = i.NationalityTlAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookupAr, loadOptions));
        }

        private void PopulateModel(Customer model, IDictionary values) {
            string CUSTOMER_ID = nameof(Customer.CustomerId);
            string FULL_NAME_AR = nameof(Customer.FullNameAr);
            string CIVIL_ID = nameof(Customer.CivilId);
            string AREA_ID = nameof(Customer.AreaId);
            string BLOCK = nameof(Customer.Block);
            string AVENUE = nameof(Customer.Avenue);
            string STREET = nameof(Customer.Street);
            string BUILDING_NO = nameof(Customer.BuildingNo);
            string FLOOR = nameof(Customer.Floor);
            string FLAT = nameof(Customer.Flat);
            string NATIONALITY_ID = nameof(Customer.NationalityId);
            string PASSPORT_NO = nameof(Customer.PassportNo);
            string MOBILE = nameof(Customer.Mobile);
            string TELE1 = nameof(Customer.Tele1);
            string TELE2 = nameof(Customer.Tele2);
            string REMARKS = nameof(Customer.Remarks);
            string FULL_NAME_EN = nameof(Customer.FullNameEn);
            string PIC = nameof(Customer.Pic);
            string EMAIL = nameof(Customer.Email);

            if(values.Contains(CUSTOMER_ID)) {
                model.CustomerId = Convert.ToInt32(values[CUSTOMER_ID]);
            }

            if(values.Contains(FULL_NAME_AR)) {
                model.FullNameAr = Convert.ToString(values[FULL_NAME_AR]);
            }

            if(values.Contains(CIVIL_ID)) {
                model.CivilId = Convert.ToString(values[CIVIL_ID]);
            }

            if(values.Contains(AREA_ID)) {
                model.AreaId = values[AREA_ID] != null ? Convert.ToInt32(values[AREA_ID]) : (int?)null;
            }

            if(values.Contains(BLOCK)) {
                model.Block = Convert.ToString(values[BLOCK]);
            }

            if(values.Contains(AVENUE)) {
                model.Avenue = Convert.ToString(values[AVENUE]);
            }

            if(values.Contains(STREET)) {
                model.Street = Convert.ToString(values[STREET]);
            }

            if(values.Contains(BUILDING_NO)) {
                model.BuildingNo = Convert.ToString(values[BUILDING_NO]);
            }

            if(values.Contains(FLOOR)) {
                model.Floor = Convert.ToString(values[FLOOR]);
            }

            if(values.Contains(FLAT)) {
                model.Flat = Convert.ToString(values[FLAT]);
            }

            if(values.Contains(NATIONALITY_ID)) {
                model.NationalityId = values[NATIONALITY_ID] != null ? Convert.ToInt32(values[NATIONALITY_ID]) : (int?)null;
            }

            if(values.Contains(PASSPORT_NO)) {
                model.PassportNo = Convert.ToString(values[PASSPORT_NO]);
            }

            if(values.Contains(MOBILE)) {
                model.Mobile = Convert.ToString(values[MOBILE]);
            }

            if(values.Contains(TELE1)) {
                model.Tele1 = Convert.ToString(values[TELE1]);
            }

            if(values.Contains(TELE2)) {
                model.Tele2 = Convert.ToString(values[TELE2]);
            }

            if(values.Contains(REMARKS)) {
                model.Remarks = Convert.ToString(values[REMARKS]);
            }

            if(values.Contains(FULL_NAME_EN)) {
                model.FullNameEn = Convert.ToString(values[FULL_NAME_EN]);
            }

            if(values.Contains(PIC)) {
                model.Pic = Convert.ToString(values[PIC]);
            }

            if(values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
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