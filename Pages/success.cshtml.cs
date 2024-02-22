using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeService.Data;
using HomeService.Models;

namespace HomeService.Pages
{
    public class successModel : PageModel
    {
        private HomeServiceContext _context;
        public Request request { get; set; }


        public successModel(HomeServiceContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string payment_type,string PaymentID,string Result,int OrderID, DateTime? PostDate,string TranID,
        string Ref, string TrackID, string Auth)
        {
            try
            {
                if (OrderID == 0)
                {
                    return Redirect("NotFound");
                }

                if (OrderID != 0)
                {
                    request = _context.Request.FirstOrDefault(e => e.RequestId == OrderID);
                    if (request != null)
                    {

                        request.isPaid = true;
                        request.payment_type = payment_type;
                        request.PaymentID = PaymentID;
                        request.Result = Result;
                        request.PostDate = DateTime.Now;
                        request.TranID = TranID;
                        request.Ref = Ref;
                        request.TrackID = TrackID;
                        request.Auth = Auth;
                        var UpdatedOrder = _context.Request.Attach(request);
                        UpdatedOrder.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                return Redirect("SomethingwentError");
            }
            return Page();
        }
            

        }
    }

