﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HomeService.Models
{
    public partial class RequestSpareParts
    {
      

        public int RequestSparePartsId { get; set; }
        public int RequestId { get; set; }
        public int SparePartId { get; set; }
        public int QTY { get; set; }
        public double? Price { get; set; }
        public double? Total { get; set; }

        public virtual Request Request { get; set; }
        public virtual SparePart SparePart { get; set; }


    }
}