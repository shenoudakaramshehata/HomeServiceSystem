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

    public class RequestSparePartsController : Controller
    {
        private HomeServiceContext _context;

        public RequestSparePartsController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var requestspareparts = _context.RequestSpareParts.Select(i => new {
                i.RequestSparePartsId,
                i.RequestId,
                i.SparePartId,
                i.QTY,
                i.Price,
                i.Total
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "RequestSparePartsId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(requestspareparts, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new RequestSpareParts();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.RequestSpareParts.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.RequestSparePartsId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.RequestSpareParts.FirstOrDefaultAsync(item => item.RequestSparePartsId == key);
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
            var model = await _context.RequestSpareParts.FirstOrDefaultAsync(item => item.RequestSparePartsId == key);

            _context.RequestSpareParts.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> RequestLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Request
                         orderby i.IssueDescription
                         select new {
                             Value = i.RequestId,
                             Text = i.IssueDescription
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> SparePartLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.SparePart
                         orderby i.SparePartTlEn
                         select new {
                             Value = i.SparePartId,
                             Text = i.SparePartTlEn
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(RequestSpareParts model, IDictionary values) {
            string REQUEST_SPARE_PARTS_ID = nameof(RequestSpareParts.RequestSparePartsId);
            string REQUEST_ID = nameof(RequestSpareParts.RequestId);
            string SPARE_PART_ID = nameof(RequestSpareParts.SparePartId);
            string QTY = nameof(RequestSpareParts.QTY);
            string PRICE = nameof(RequestSpareParts.Price);
            string TOTAL = nameof(RequestSpareParts.Total);

            if(values.Contains(REQUEST_SPARE_PARTS_ID)) {
                model.RequestSparePartsId = Convert.ToInt32(values[REQUEST_SPARE_PARTS_ID]);
            }

            if(values.Contains(REQUEST_ID)) {
                model.RequestId = Convert.ToInt32(values[REQUEST_ID]);
            }

            if(values.Contains(SPARE_PART_ID)) {
                model.SparePartId = Convert.ToInt32(values[SPARE_PART_ID]);
            }

            if(values.Contains(QTY)) {
                model.QTY = Convert.ToInt32(values[QTY]);
            }

            if(values.Contains(PRICE)) {
                model.Price = values[PRICE] != null ? Convert.ToDouble(values[PRICE], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(TOTAL)) {
                model.Total = values[TOTAL] != null ? Convert.ToDouble(values[TOTAL], CultureInfo.InvariantCulture) : (double?)null;
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