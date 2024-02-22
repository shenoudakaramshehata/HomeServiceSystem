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

    public class ServicesController : Controller
    {
        private HomeServiceContext _context;

        public ServicesController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var service = _context.Service.Select(i => new {
                i.ServiceId,
                i.ServiceTlEn,
                i.ServiceTlAr
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ServiceId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(service, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Service();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Service.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ServiceId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Service.FirstOrDefaultAsync(item => item.ServiceId == key);
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
            if (_context.ContractService.Any(c => c.ServiceId == key))
            {
                var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                if (BrowserCulture == "en-US")
                    return StatusCode(409, "You cannot delete this Service");
                else
                    return StatusCode(409, "لا يمكنك مسح هذة الخدمة");

            }
            var model = await _context.Service.FirstOrDefaultAsync(item => item.ServiceId == key);

            _context.Service.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();

        }


        private void PopulateModel(Service model, IDictionary values) {
            string SERVICE_ID = nameof(Service.ServiceId);
            string SERVICE_TL_EN = nameof(Service.ServiceTlEn);
            string SERVICE_TL_AR = nameof(Service.ServiceTlAr);

            if(values.Contains(SERVICE_ID)) {
                model.ServiceId = Convert.ToInt32(values[SERVICE_ID]);
            }

            if(values.Contains(SERVICE_TL_EN)) {
                model.ServiceTlEn = Convert.ToString(values[SERVICE_TL_EN]);
            }

            if(values.Contains(SERVICE_TL_AR)) {
                model.ServiceTlAr = Convert.ToString(values[SERVICE_TL_AR]);
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