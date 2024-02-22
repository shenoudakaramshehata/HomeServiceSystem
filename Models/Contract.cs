﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HomeService.Models
{
    public partial class Contract
    {
        public Contract()
        {
            ContractService = new HashSet<ContractService>();
            Receipt = new HashSet<Receipt>();
            Request = new HashSet<Request>();

        }

        public int ContractId { get; set; }
        public string ContractSerial     { get; set; }

        public int CustomerId { get; set; }
        public int? ContractTypeId { get; set; }
        public int UnitId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Amount { get; set; }
        public string Remarks { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ContractType ContractType { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<ContractService> ContractService { get; set; }
        public virtual ICollection<Receipt> Receipt { get; set; }
        public virtual ICollection<Request> Request { get; set; }
    }
}