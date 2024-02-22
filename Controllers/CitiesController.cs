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

    public class CitiesController : Controller
    {
        private HomeServiceContext _context;

        public CitiesController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var city = _context.City.Select(i => new {
                i.CityId,
                i.CityTlEn,
                i.CityTlAr
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CityId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(city, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new City();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.City.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CityId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.City.FirstOrDefaultAsync(item => item.CityId == key);
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
        public async Task<IActionResult> Delete(int key)
        {
            if (_context.Area.Any(c => c.CityId == key) )
            {
                var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                if (BrowserCulture == "en-US")
                    return StatusCode(409, "You cannot delete this City");
                else
                    return StatusCode(409, "لا يمكنك مسح هذة المدينة");

            }
            var model = await _context.City.FirstOrDefaultAsync(item => item.CityId == key);

            _context.City.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();
        }


        private void PopulateModel(City model, IDictionary values) {
            string CITY_ID = nameof(City.CityId);
            string CITY_TL_EN = nameof(City.CityTlEn);
            string CITY_TL_AR = nameof(City.CityTlAr);

            if(values.Contains(CITY_ID)) {
                model.CityId = Convert.ToInt32(values[CITY_ID]);
            }

            if(values.Contains(CITY_TL_EN)) {
                model.CityTlEn = Convert.ToString(values[CITY_TL_EN]);
            }

            if(values.Contains(CITY_TL_AR)) {
                model.CityTlAr = Convert.ToString(values[CITY_TL_AR]);
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