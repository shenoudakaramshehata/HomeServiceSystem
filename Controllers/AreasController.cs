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

    public class AreasController : Controller
    {
        private HomeServiceContext _context;

        public AreasController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var area = _context.Area.Select(i => new {
                i.AreaId,
                i.AreaTlAr,
                i.CityId,
                i.AreaTlEn
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "AreaId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(area, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Area();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Area.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.AreaId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Area.FirstOrDefaultAsync(item => item.AreaId == key);
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
        public async Task<IActionResult> Delete(int key) {
            if (_context.Customer.Any(c=>c.AreaId== key) || _context.Unit.Any(c => c.AreaId == key))
            {
                var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                if (BrowserCulture == "en-US")
                    return StatusCode(409, "You cannot delete this Area");
                else
                    return StatusCode(409, "لا يمكنك مسح هذة المنطقة");
                    
            }
            var model = await _context.Area.FirstOrDefaultAsync(item => item.AreaId == key);

            _context.Area.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> CityLookup(DataSourceLoadOptions loadOptions) {
            var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var BrowserCulture = locale.RequestCulture.UICulture.ToString();
            if (BrowserCulture == "en-US")
            {
                var lookupEn = from i in _context.City
                               orderby i.CityTlEn
                               select new
                               {
                                   Value = i.CityId,
                                   Text = i.CityTlEn
                               };
                return Json(await DataSourceLoader.LoadAsync(lookupEn, loadOptions));
            }
            var lookupAr = from i in _context.City
                         orderby i.CityTlAr
                         select new {
                             Value = i.CityId,
                             Text = i.CityTlAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookupAr, loadOptions));
        }

        private void PopulateModel(Area model, IDictionary values) {
            string AREA_ID = nameof(Area.AreaId);
            string AREA_TL_AR = nameof(Area.AreaTlAr);
            string CITY_ID = nameof(Area.CityId);
            string AREA_TL_EN = nameof(Area.AreaTlEn);

            if(values.Contains(AREA_ID)) {
                model.AreaId = Convert.ToInt32(values[AREA_ID]);
            }

            if(values.Contains(AREA_TL_AR)) {
                model.AreaTlAr = Convert.ToString(values[AREA_TL_AR]);
            }

            if(values.Contains(CITY_ID)) {
                model.CityId = values[CITY_ID] != null ? Convert.ToInt32(values[CITY_ID]) : (int?)null;
            }

            if(values.Contains(AREA_TL_EN)) {
                model.AreaTlEn = Convert.ToString(values[AREA_TL_EN]);
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