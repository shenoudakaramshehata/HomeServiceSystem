﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HomeService.Models
{
    public partial class Receipt
    {
        public int ReceiptId { get; set; }
        
        public int? ContractId { get; set; }
        public DateTime? VDate { get; set; }
        public double Amount { get; set; }
        public int? ReceiptServiceId { get; set; }
        public int? PaymentMethodId { get; set; }
        public string Remarks { get; set; }
        public string ReceiptSerial { get; set; }

        public virtual Contract Contract { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ReceiptService ReceiptService { get; set; }
    }
}