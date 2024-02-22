﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HomeService.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Contract = new HashSet<Contract>();
            Request = new HashSet<Request>();
        }

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
        public string Remarks { get; set; }
        public string FullNameEn { get; set; }
        public string Pic { get; set; }
        public string Email { get; set; }

        public virtual Area Area { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual ICollection<Contract> Contract { get; set; }
        public virtual ICollection<Request> Request { get; set; }
    }
}