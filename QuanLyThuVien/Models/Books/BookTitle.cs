using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("BookTitle")]
    public class BookTitle
    {
        [Key]
        public string Id { get; set; }
        public string CategoryBookID { get; set; }  
        public virtual CategoryBook CategoryBook { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        public string Name { get; set; }
        public string Summary { get; set; } 
        public string UrlImage { get; set; }
    }
}