using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeService.Data;
using HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Areas.Admin.Pages.Technicians
{
    public class IndexModel : PageModel
    {
        private HomeServiceContext _context;

        public IndexModel(HomeServiceContext context)
        {
            _context = context;

        }
        
        public void OnGet()
        {
        }
    }
}
