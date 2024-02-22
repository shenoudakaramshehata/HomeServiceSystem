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

    public class ContactUsController : Controller
    {
        private HomeServiceContext _context;

        public ContactUsController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var contactus = _context.ContactUs.Select(i => new {
                i.ContactId,
                i.Email,
                i.FullName,
                i.Mobile,
                i.Msg,
                i.TransDate
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ContactId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(contactus, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ContactUs();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ContactUs.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ContactId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.ContactUs.FirstOrDefaultAsync(item => item.ContactId == key);
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
            var model = await _context.ContactUs.FirstOrDefaultAsync(item => item.ContactId == key);

            _context.ContactUs.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(ContactUs model, IDictionary values) {
            string CONTACT_ID = nameof(ContactUs.ContactId);
            string EMAIL = nameof(ContactUs.Email);
            string FULL_NAME = nameof(ContactUs.FullName);
            string MOBILE = nameof(ContactUs.Mobile);
            string MSG = nameof(ContactUs.Msg);
            string TRANS_DATE = nameof(ContactUs.TransDate);

            if(values.Contains(CONTACT_ID)) {
                model.ContactId = Convert.ToInt32(values[CONTACT_ID]);
            }

            if(values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(FULL_NAME)) {
                model.FullName = Convert.ToString(values[FULL_NAME]);
            }

            if(values.Contains(MOBILE)) {
                model.Mobile = Convert.ToString(values[MOBILE]);
            }

            if(values.Contains(MSG)) {
                model.Msg = Convert.ToString(values[MSG]);
            }

            if(values.Contains(TRANS_DATE)) {
                model.TransDate = values[TRANS_DATE] != null ? Convert.ToDateTime(values[TRANS_DATE]) : (DateTime?)null;
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