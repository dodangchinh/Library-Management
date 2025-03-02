using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("Language")]
    public class Language
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }    
    }
}