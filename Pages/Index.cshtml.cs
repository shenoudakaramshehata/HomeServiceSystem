using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using HomeService.Data;
using HomeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Pages
{
    public class IndexModel : PageModel


    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HomeServiceContext _context;
          
        public IndexModel(HomeServiceContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _context = context;

        }
        [BindProperty]
        public Newsletter newsletter { get; set; }
        [BindProperty]
        public ContactUs contactUs { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPostAddNewsletter()

        {
            if (!ModelState.IsValid)
            {
                return Redirect("./Index");
            }
            try
            {
                 newsletter.Date = DateTime.Now;
                _context.Newsletter.Add(newsletter);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return Redirect("./Index");
            }

            return Redirect("./Index");

        }
        public IActionResult OnPostAddContactUs()
        {
            if (!ModelState.IsValid)
            {
                return Redirect("./Index");
            }
            try
            {

                contactUs.TransDate = DateTime.Now;
                _context.ContactUs.Add(contactUs);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return Redirect("./Index");

            }

            return Redirect("./Index");

        }
  
    }
}
