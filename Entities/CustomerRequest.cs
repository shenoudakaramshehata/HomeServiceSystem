using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Entities
{
    public class CustomerRequest
    {
        public int CustomerId { get; set; }
        public string FullNameAr { get; set; }
        public string CivilId { get; set; }
        public int? AreaId { get; set; }
        public string Block { get; set; }
        public string Avenue { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public int? NationalityId { get; set; }
        public string PassportNo { get; set; }
        public string Mobile { get; set; }
        public string Tele1 { get; set; }
        public string Tele2 { get; set; }
        public string CustomerRemarks { get; set; }
        public string FullNameEn { get; set; }
        public string Pic { get; set; }
        public string Email { get; set; }

        public int RequestId { get; set; }

        public DateTime? RequestDate { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public int RequestStateId { get; set; }
        public int? ContractId { get; set; }
        public string IssueDescription { get; set; }
        public string RequestRemarks { get; set; }
        public int? TechnicianId { get; set; }
        public string TechDescription { get; set; }
        public string TechDiagnosis { get; set; }
        public string TechFixes { get; set; }
        public string SparePartsDescription { get; set; }
        public bool? IsClosd { get; set; }
        public Double? VisitCost { get; set; }
        public Double? SparePartsCost { get; set; }
        public int? ServiceCategoryId { get; set; }


        public string ServiceCategoryTlEn { get; set; }
        public string ServiceCategoryTlAr { get; set; }
        public string RequestStateEn { get; set; }
        public string RequestStateAr { get; set; }


        public string TechnicianFullNameEn { get; set; }
        public string TechnicianCivilId { get; set; }
        public string TechnicianFullAddress { get; set; }
        public int? TechnicianNationalityId { get; set; }
        public string TechnicianPassportNo { get; set; }
        public string TechnicianMobile { get; set; }
        public string TechnicianTele { get; set; }
        public string TechnicianEmail { get; set; }
        public string TechnicianRemarks { get; set; }
        public string TechnicianFullNameAr { get; set; }
        public string TechnicianPic { get; set; }





    }
}
