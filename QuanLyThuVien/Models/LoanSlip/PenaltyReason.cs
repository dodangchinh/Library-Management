﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("PenaltyReason")]
    public class PenaltyReason
    {
        [Key]
        public string Id {  get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}