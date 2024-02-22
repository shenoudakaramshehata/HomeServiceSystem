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

    public class UnitGroupsController : Controller
    {
        private HomeServiceContext _context;

        public UnitGroupsController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var unitgroup = _context.UnitGroup.Select(i => new {
                i.UnitGroupId,
                i.UnitGroupTlAr,
                i.UnitGroupTlEn,
                i.UnitGroupOrderIndex
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "UnitGroupId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(unitgroup, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new UnitGroup();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.UnitGroup.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.UnitGroupId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.UnitGroup.FirstOrDefaultAsync(item => item.UnitGroupId == key);
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
            if (_context.Unit.Any(c => c.UnitGroupId == key))
            {
                var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                if (BrowserCulture == "en-US")
                    return StatusCode(409, "You cannot delete this Units Group");
                else
                    return StatusCode(409, "لا يمكنك مسح مجموعة الوحدة");

            }
            var model = await _context.UnitGroup.FirstOrDefaultAsync(item => item.UnitGroupId == key);

            _context.UnitGroup.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();

        }


        private void PopulateModel(UnitGroup model, IDictionary values) {
            string UNIT_GROUP_ID = nameof(UnitGroup.UnitGroupId);
            string UNIT_GROUP_TL_AR = nameof(UnitGroup.UnitGroupTlAr);
            string UNIT_GROUP_TL_EN = nameof(UnitGroup.UnitGroupTlEn);
            string UNIT_GROUP_ORDER_INDEX = nameof(UnitGroup.UnitGroupOrderIndex);

            if(values.Contains(UNIT_GROUP_ID)) {
                model.UnitGroupId = Convert.ToInt32(values[UNIT_GROUP_ID]);
            }

            if(values.Contains(UNIT_GROUP_TL_AR)) {
                model.UnitGroupTlAr = Convert.ToString(values[UNIT_GROUP_TL_AR]);
            }

            if(values.Contains(UNIT_GROUP_TL_EN)) {
                model.UnitGroupTlEn = Convert.ToString(values[UNIT_GROUP_TL_EN]);
            }

            if(values.Contains(UNIT_GROUP_ORDER_INDEX)) {
                model.UnitGroupOrderIndex = values[UNIT_GROUP_ORDER_INDEX] != null ? Convert.ToInt32(values[UNIT_GROUP_ORDER_INDEX]) : (int?)null;
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