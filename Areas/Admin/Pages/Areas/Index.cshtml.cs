using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Entities;
using HomeService.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Areas.Admin.Pages.Areas
{
    public class IndexModel : PageModel
    {
        private HomeServiceContext _context;
        public IndexModel(HomeServiceContext context)
        {
            _context = context;
        }


        [BindProperty(SupportsGet = true)]
        public List<City> cityLst { get; set; } 
        [BindProperty(SupportsGet = true)]
        public string title { get; set; }
        public void OnGet()
        {
            var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var BrowserCulture = locale.RequestCulture.UICulture.ToString();
            if (BrowserCulture == "en-US")
                title = nameof(City.CityTlEn);
            else
                title = nameof(City.CityTlAr);



            cityLst = _context.City.ToList();
            var temp = new City
            {
                CityTlAr = "الكل",
                CityTlEn = "All",
                CityId = 0
            };
            cityLst.Insert(0, temp);
            
        }
    }
}
