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

    public class ContractServicesController : Controller
    {
        private HomeServiceContext _context;

        public ContractServicesController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int contractId,DataSourceLoadOptions loadOptions) {
            var contractservice = _context.ContractService.Where(c=>c.ContractId== contractId).Select(i => new {
                i.ContractServiceId,
                i.ContractId,
                i.ServiceId,
                i.Service
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ContractServiceId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(contractservice, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ContractService();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ContractService.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ContractServiceId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.ContractService.FirstOrDefaultAsync(item => item.ContractServiceId == key);
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
            var model = await _context.ContractService.FirstOrDefaultAsync(item => item.ContractServiceId == key);

            _context.ContractService.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> ContractLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Contract
                         orderby i.ContractSerial
                         select new {
                             Value = i.ContractId,
                             Text = i.ContractSerial
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ServiceLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Service
                         orderby i.ServiceTlEn
                         select new {
                             Value = i.ServiceId,
                             Text = i.ServiceTlEn
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(ContractService model, IDictionary values) {
            string CONTRACT_SERVICE_ID = nameof(ContractService.ContractServiceId);
            string CONTRACT_ID = nameof(ContractService.ContractId);
            string SERVICE_ID = nameof(ContractService.ServiceId);

            if(values.Contains(CONTRACT_SERVICE_ID)) {
                model.ContractServiceId = Convert.ToInt32(values[CONTRACT_SERVICE_ID]);
            }

            if(values.Contains(CONTRACT_ID)) {
                model.ContractId = Convert.ToInt32(values[CONTRACT_ID]);
            }

            if(values.Contains(SERVICE_ID)) {
                model.ServiceId = Convert.ToInt32(values[SERVICE_ID]);
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