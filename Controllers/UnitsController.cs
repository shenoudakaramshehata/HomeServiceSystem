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

    public class UnitsController : Controller
    {
        private HomeServiceContext _context;

        public UnitsController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var unit = _context.Unit.Select(i => new {
                i.UnitId,
                i.UnitGroupId,
                i.AreaId,
                i.UnitTile,
                i.Block,
                i.Avenue,
                i.Street,
                i.BuildingNo,
                i.Floor,
                i.Flat,
                i.Remarks
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "UnitId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(unit, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Unit();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Unit.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.UnitId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Unit.FirstOrDefaultAsync(item => item.UnitId == key);
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
            if (_context.Contract.Any(c => c.UnitId == key))
            {
                var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                if (BrowserCulture == "en-US")
                    return StatusCode(409, "You cannot delete this Unit");
                else
                    return StatusCode(409, "لا يمكنك مسح هذةالوحدة");

            }   
            var model = await _context.Unit.FirstOrDefaultAsync(item => item.UnitId == key);

            _context.Unit.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();

        }


        [HttpGet]
        public async Task<IActionResult> AreaLookup(DataSourceLoadOptions loadOptions) {
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
                         select new {
                             Value = i.AreaId,
                             Text = i.AreaTlAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookupAr, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> UnitGroupLookup(DataSourceLoadOptions loadOptions) {
            var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var BrowserCulture = locale.RequestCulture.UICulture.ToString();

            if (BrowserCulture == "en-US")
            {
                var lookupEn = from i in _context.UnitGroup
                               orderby i.UnitGroupTlEn
                               select new
                               {
                                   Value = i.UnitGroupId,
                                   Text = i.UnitGroupTlEn
                               };
                return Json(await DataSourceLoader.LoadAsync(lookupEn, loadOptions));
            }
            var lookupAr = from i in _context.UnitGroup
                         orderby i.UnitGroupTlAr
                         select new {
                             Value = i.UnitGroupId,
                             Text = i.UnitGroupTlAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookupAr, loadOptions));
        }

        private void PopulateModel(Unit model, IDictionary values) {
            string UNIT_ID = nameof(Unit.UnitId);
            string UNIT_GROUP_ID = nameof(Unit.UnitGroupId);
            string AREA_ID = nameof(Unit.AreaId);
            string UNIT_TILE = nameof(Unit.UnitTile);
            string BLOCK = nameof(Unit.Block);
            string AVENUE = nameof(Unit.Avenue);
            string STREET = nameof(Unit.Street);
            string BUILDING_NO = nameof(Unit.BuildingNo);
            string FLOOR = nameof(Unit.Floor);
            string FLAT = nameof(Unit.Flat);
            string REMARKS = nameof(Unit.Remarks);

            if(values.Contains(UNIT_ID)) {
                model.UnitId = Convert.ToInt32(values[UNIT_ID]);
            }

            if(values.Contains(UNIT_GROUP_ID)) {
                model.UnitGroupId = Convert.ToInt32(values[UNIT_GROUP_ID]);
            }

            if(values.Contains(AREA_ID)) {
                model.AreaId = values[AREA_ID] != null ? Convert.ToInt32(values[AREA_ID]) : (int?)null;
            }

            if(values.Contains(UNIT_TILE)) {
                model.UnitTile = Convert.ToString(values[UNIT_TILE]);
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