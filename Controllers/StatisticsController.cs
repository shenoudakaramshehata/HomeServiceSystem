using DevExtreme.AspNet.Mvc;
using HomeService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using DevExtreme.AspNet.Data;

namespace HomeService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class StatisticsController : Controller
    {
        private HomeServiceContext _context;
        public StatisticsController(HomeServiceContext context)
        {
            _context = context;
        }
        public List<string> months = new List<string>{ "January", "February", "March", "April", "May", "June", "Jule", "August", "September", "October", "November", "December" };

        [HttpGet]
        public object GetDailyRequests(DataSourceLoadOptions loadOptions)
        {
            var dailyRequests = _context.Request
                .Where(o => o.RequestDate.Value.Year == DateTime.Now.Year)
                .GroupBy(c => c.RequestDate.Value.Date).

                Select(g => new
                {

                    day = g.Key,

                    request = g.Count()

                }).OrderBy(r => r.day).ThenBy(r => DateTime.Now.Month);

            return dailyRequests;


        }

        [HttpGet]

        public object GetMonthlyRequests(DataSourceLoadOptions loadOptions)
        {
            var monthlyRequests = _context.Request
                .Where(o => o.RequestDate.Value.Year == DateTime.Now.Year)
                .GroupBy(c => c.RequestDate.Value.Date.Month).

                Select(g => new
                {
                    month = months[g.Key],
                    request = g.Count()

                });

            return monthlyRequests;


        }
        [HttpGet]

        public object GetDailyContracts(DataSourceLoadOptions loadOptions)
        {
            var dailyOrder = _context.Contract
                .Where(o => o.StartDate.Value.Year == DateTime.Now.Year)
                .GroupBy(c => c.StartDate.Value.Date).

                Select(g => new
                {

                    day = g.Key,

                    contract = g.Count()

                }).OrderBy(r => r.day).ThenBy(r => DateTime.Now.Month);

            return dailyOrder;


        }

        [HttpGet]

        public object GetMonthlyContracts(DataSourceLoadOptions loadOptions)
        {
            var monthlyContracts = _context.Contract
                .Where(o => o.StartDate.Value.Year == DateTime.Now.Year)
                .GroupBy(c => c.StartDate.Value.Date.Month).

                Select(g => new
                {
                    month = months[g.Key],
                    contract = g.Count()

                });

            return monthlyContracts;


        }


        [HttpGet]

        public object GetDailyReceipts(DataSourceLoadOptions loadOptions)
        {
            var dailyReceipt = _context.Receipt
                .Where(o => o.VDate.Value.Year == DateTime.Now.Year)
                .GroupBy(c => c.VDate.Value.Date).

                Select(g => new
                {

                    day = g.Key,

                    receipt = g.Count()

                }).OrderBy(r => r.day).ThenBy(r => DateTime.Now.Month);

            return dailyReceipt;


        }

        [HttpGet]

        public object GetMonthlyReceipts(DataSourceLoadOptions loadOptions)
        {
            var monthlyReceipts = _context.Receipt
                .Where(o => o.VDate.Value.Year == DateTime.Now.Year)
                .GroupBy(c => c.VDate.Value.Date.Month).

                Select(g => new
                {
                    month = months[g.Key],
                    receipt = g.Count()

                });

            return monthlyReceipts;


        }

        [HttpGet]
        public async Task<IActionResult> RequestStatesLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.RequestState
                         select new
                         {
                             Value = i.RequestStateId,
                             Text = i.RequestStateEn
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }


    }
}
