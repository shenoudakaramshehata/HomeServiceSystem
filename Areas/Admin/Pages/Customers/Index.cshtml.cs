using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Areas.Admin.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private HomeServiceContext _context;

        public IndexModel(HomeServiceContext context)
        {
            _context = context;

        }
        
        public async Task<IActionResult> OnGet()
        {
           
            return Page();
        }
    }
}
