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

    public class ReceiptsController : Controller
    {
        private HomeServiceContext _context;

        public ReceiptsController(HomeServiceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int contractId,DataSourceLoadOptions loadOptions) {
            var receipt = _context.Receipt.Where(c=>c.ContractId== contractId).Select(i => new {
                i.ReceiptId,
                i.ContractId,
              
                i.Contract,
                i.VDate,    
                i.Amount,
                i.ReceiptServiceId,
                i.PaymentMethodId,
                i.Remarks,
                i.ReceiptSerial
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ReceiptId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(receipt, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Receipt();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Receipt.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ReceiptId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Receipt.FirstOrDefaultAsync(item => item.ReceiptId == key);
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
            var model = await _context.Receipt.FirstOrDefaultAsync(item => item.ReceiptId == key);

            _context.Receipt.Remove(model);
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
        public async Task<IActionResult> PaymentMethodLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.PaymentMethod
                         orderby i.PaymentMethodTlEn
                         select new {
                             Value = i.PaymentMethodId,
                             Text = i.PaymentMethodTlEn
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ReceiptServiceLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.ReceiptService
                         orderby i.ReceiptServiceTlEn
                         select new {
                             Value = i.ReceiptServiceId,
                             Text = i.ReceiptServiceTlEn
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> CustomerLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Customer
                         orderby i.FullNameAr
                         select new
                         {
                             Value = i.CustomerId,
                             Text = i.FullNameAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        private void PopulateModel(Receipt model, IDictionary values) {
            string RECEIPT_ID = nameof(Receipt.ReceiptId);
            string CONTRACT_ID = nameof(Receipt.ContractId);
            string VDATE = nameof(Receipt.VDate);
            string AMOUNT = nameof(Receipt.Amount);
            string RECEIPT_SERVICE_ID = nameof(Receipt.ReceiptServiceId);
            string PAYMENT_METHOD_ID = nameof(Receipt.PaymentMethodId);
            string REMARKS = nameof(Receipt.Remarks);
            string RECEIPT_SERIAL = nameof(Receipt.ReceiptSerial);

            if(values.Contains(RECEIPT_ID)) {
                model.ReceiptId = Convert.ToInt32(values[RECEIPT_ID]);
            }

            if(values.Contains(CONTRACT_ID)) {
                model.ContractId = values[CONTRACT_ID] != null ? Convert.ToInt32(values[CONTRACT_ID]) : (int?)null;
            }

            if(values.Contains(VDATE)) {
                model.VDate = values[VDATE] != null ? Convert.ToDateTime(values[VDATE]) : (DateTime?)null;
            }

            if(values.Contains(AMOUNT)) {
                model.Amount = Convert.ToDouble(values[AMOUNT], CultureInfo.InvariantCulture);
            }

            if(values.Contains(RECEIPT_SERVICE_ID)) {
                model.ReceiptServiceId = values[RECEIPT_SERVICE_ID] != null ? Convert.ToInt32(values[RECEIPT_SERVICE_ID]) : (int?)null;
            }

            if(values.Contains(PAYMENT_METHOD_ID)) {
                model.PaymentMethodId = values[PAYMENT_METHOD_ID] != null ? Convert.ToInt32(values[PAYMENT_METHOD_ID]) : (int?)null;
            }

            if(values.Contains(REMARKS)) {
                model.Remarks = Convert.ToString(values[REMARKS]);
            }

            if(values.Contains(RECEIPT_SERIAL)) {
                model.ReceiptSerial = Convert.ToString(values[RECEIPT_SERIAL]);
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