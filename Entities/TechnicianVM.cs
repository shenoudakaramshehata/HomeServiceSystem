using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Entities
{
    public class TechnicianVM
    {
        public int TechnicianId { get; set; }
        [StringLength(50)]

        public string FullNameEn { get; set; }
        [StringLength(50)]
        public string CivilId { get; set; }
        [StringLength(50)]
        public string FullAddress { get; set; }
        
        public int? NationalityId { get; set; }
        [StringLength(50)]
        public string PassportNo { get; set; }
        [StringLength(50)]
        public string Mobile { get; set; }
        [StringLength(50)]
        public string Tele { get; set; }
        
        public string Remarks { get; set; }
        [StringLength(50)]
        public string FullNameAr { get; set; }
        public string Pic { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
