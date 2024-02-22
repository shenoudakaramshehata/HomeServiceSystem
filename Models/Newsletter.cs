using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Models
{
    public partial class Newsletter
    {
        public int NewsletterId { get; set; }
        public string Email { get; set; }
        public DateTime? Date { get; set; }


    }
}