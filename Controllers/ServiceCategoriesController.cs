﻿using DevExtreme.AspNet.Data;
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

    public class ServiceCategoriesController : Controller
    {
        private HomeServiceContext _context;

        public ServiceCategoriesController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var servicecategory = _context.ServiceCategory.Select(i => new {
                i.ServiceCategoryId,
                i.ServiceCategoryTlEn,
                i.ServiceCategoryTlAr,
                i.pic
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ServiceCategoryId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(servicecategory, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ServiceCategory();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ServiceCategory.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ServiceCategoryId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.ServiceCategory.FirstOrDefaultAsync(item => item.ServiceCategoryId == key);
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
            var model = await _context.ServiceCategory.FirstOrDefaultAsync(item => item.ServiceCategoryId == key);

            _context.ServiceCategory.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(ServiceCategory model, IDictionary values) {
            string SERVICE_CATEGORY_ID = nameof(ServiceCategory.ServiceCategoryId);
            string SERVICE_CATEGORY_TL_EN = nameof(ServiceCategory.ServiceCategoryTlEn);
            string SERVICE_CATEGORY_TL_AR = nameof(ServiceCategory.ServiceCategoryTlAr);
            string PIC = nameof(ServiceCategory.pic);

            if(values.Contains(SERVICE_CATEGORY_ID)) {
                model.ServiceCategoryId = Convert.ToInt32(values[SERVICE_CATEGORY_ID]);
            }

            if(values.Contains(SERVICE_CATEGORY_TL_EN)) {
                model.ServiceCategoryTlEn = Convert.ToString(values[SERVICE_CATEGORY_TL_EN]);
            }

            if(values.Contains(SERVICE_CATEGORY_TL_AR)) {
                model.ServiceCategoryTlAr = Convert.ToString(values[SERVICE_CATEGORY_TL_AR]);
            }

            if(values.Contains(PIC)) {
                model.pic = Convert.ToString(values[PIC]);
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