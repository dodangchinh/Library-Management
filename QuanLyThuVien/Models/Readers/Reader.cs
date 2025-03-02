using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace QuanLyThuVien
{
    [Table("Reader")]
    public class Reader
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }    
        public bool Status { get; set; } = true;    
    }
}