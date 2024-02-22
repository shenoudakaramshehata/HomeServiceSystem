using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Entities
{
    public class RequestVM
    {
        public int? RequestId { get; set; }

        public int? CustomerId { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public int RequestStateId { get; set; }

        public int? ContractId { get; set; }
        public string IssueDescription { get; set; }
        public string Remarks { get; set; }
        public int? TechnicianId { get; set; }
        public string TechDescription { get; set; }
        public string TechDiagnosis { get; set; }
        public string TechFixes { get; set; }
        public string SparePartsDescription { get; set; }
        public bool? IsClosd { get; set; }
        public bool? State { get; set; }
        public Double? VisitCost { get; set; }
       

        public int? ServiceCategoryId { get; set; }

        public List<SparePartsVM> RequestSpareParts { get; set; }
    }

    public class SparePartsVM
    {
        public int SparePartId { get; set; }
        public int QTY { get; set; }

    }
}
