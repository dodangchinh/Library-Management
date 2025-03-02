using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("Author")]
    public class Author
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        public string Name { get; set; }
        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh không được để trống!")]
        public DateTime? BOF { get; set; }
        public bool Status { get; set; } = true;
    }
}