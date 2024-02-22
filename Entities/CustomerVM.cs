using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Entities
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        [StringLength(50)]
        public string FullNameAr { get; set; }
        [StringLength(50)]

        public string CivilId { get; set; }
        public int? AreaId { get; set; }
        [StringLength(50)]

        public string Block { get; set; }
        [StringLength(50)]

        public string Avenue { get; set; }
        [StringLength(50)]

        public string Street { get; set; }
        [StringLength(50)]

        public string BuildingNo { get; set; }
        [StringLength(50)]

        public string Floor { get; set; }
        [StringLength(50)]

        public string Flat { get; set; }

        public int? NationalityId { get; set; }
        [StringLength(50)]

        public string PassportNo { get; set; }
        [StringLength(50)]

        public string Mobile { get; set; }
        [StringLength(50)]

        public string Tele1 { get; set; }
        [StringLength(50)]

        public string Tele2 { get; set; }
        public string Remarks { get; set; }
        [StringLength(50)]
        public string FullNameEn { get; set; }
        public string Pic { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
