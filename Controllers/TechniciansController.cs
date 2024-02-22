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

    public class TechniciansController : Controller
    {
        private HomeServiceContext _context;

        public TechniciansController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var technician = _context.Technician.Select(i => new {
                i.TechnicianId,
                i.FullNameEn,
                i.CivilId,
                i.FullAddress,
                i.NationalityId,
                i.Nationality,
                i.PassportNo,
                i.Mobile,
                i.Tele,
                i.Email,
                i.Remarks,
                i.FullNameAr,
                i.Pic
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "TechnicianId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(technician, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Technician();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Technician.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.TechnicianId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Technician.FirstOrDefaultAsync(item => item.TechnicianId == key);
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
            var model = await _context.Technician.FirstOrDefaultAsync(item => item.TechnicianId == key);

            _context.Technician.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> NationalityLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Nationality
                         orderby i.NationalityTlAr
                         select new {
                             Value = i.NationalityId,
                             Text = i.NationalityTlAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Technician model, IDictionary values) {
            string TECHNICIAN_ID = nameof(Technician.TechnicianId);
            string FULL_NAME_EN = nameof(Technician.FullNameEn);
            string CIVIL_ID = nameof(Technician.CivilId);
            string FULL_ADDRESS = nameof(Technician.FullAddress);
            string NATIONALITY_ID = nameof(Technician.NationalityId);
            string PASSPORT_NO = nameof(Technician.PassportNo);
            string MOBILE = nameof(Technician.Mobile);
            string TELE = nameof(Technician.Tele);
            string EMAIL = nameof(Technician.Email);
            string REMARKS = nameof(Technician.Remarks);
            string FULL_NAME_AR = nameof(Technician.FullNameAr);
            string PIC = nameof(Technician.Pic);

            if(values.Contains(TECHNICIAN_ID)) {
                model.TechnicianId = Convert.ToInt32(values[TECHNICIAN_ID]);
            }

            if(values.Contains(FULL_NAME_EN)) {
                model.FullNameEn = Convert.ToString(values[FULL_NAME_EN]);
            }

            if(values.Contains(CIVIL_ID)) {
                model.CivilId = Convert.ToString(values[CIVIL_ID]);
            }

            if(values.Contains(FULL_ADDRESS)) {
                model.FullAddress = Convert.ToString(values[FULL_ADDRESS]);
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

            if(values.Contains(TELE)) {
                model.Tele = Convert.ToString(values[TELE]);
            }

            if(values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(REMARKS)) {
                model.Remarks = Convert.ToString(values[REMARKS]);
            }

            if(values.Contains(FULL_NAME_AR)) {
                model.FullNameAr = Convert.ToString(values[FULL_NAME_AR]);
            }

            if(values.Contains(PIC)) {
                model.Pic = Convert.ToString(values[PIC]);
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