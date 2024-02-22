using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Models
{
    public class FilterModel
    {
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public int requeststatusid { get; set; }
    }
}
