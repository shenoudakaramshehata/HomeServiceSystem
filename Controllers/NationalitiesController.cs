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

    public class NationalitiesController : Controller
    {
        private HomeServiceContext _context;

        public NationalitiesController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var nationality = _context.Nationality.Select(i => new {
                i.NationalityId,
                i.NationalityTlAr,
                i.NationalityTlEn
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "NationalityId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(nationality, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Nationality();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Nationality.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.NationalityId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Nationality.FirstOrDefaultAsync(item => item.NationalityId == key);
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
            if (_context.Customer.Any(c => c.NationalityId == key) || _context.Technician.Any(c => c.NationalityId == key))
            {
                var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                if (BrowserCulture == "en-US")
                    return StatusCode(409, "You cannot delete this Nationality");
                else
                    return StatusCode(409, "لا يمكنك مسح هذة الجنسية");

            }
            var model = await _context.Nationality.FirstOrDefaultAsync(item => item.NationalityId == key);
            _context.Nationality.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();

        }


        private void PopulateModel(Nationality model, IDictionary values) {
            string NATIONALITY_ID = nameof(Nationality.NationalityId);
            string NATIONALITY_TL_AR = nameof(Nationality.NationalityTlAr);
            string NATIONALITY_TL_EN = nameof(Nationality.NationalityTlEn);

            if(values.Contains(NATIONALITY_ID)) {
                model.NationalityId = Convert.ToInt32(values[NATIONALITY_ID]);
            }

            if(values.Contains(NATIONALITY_TL_AR)) {
                model.NationalityTlAr = Convert.ToString(values[NATIONALITY_TL_AR]);
            }

            if(values.Contains(NATIONALITY_TL_EN)) {
                model.NationalityTlEn = Convert.ToString(values[NATIONALITY_TL_EN]);
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