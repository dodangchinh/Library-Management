using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Tên vai không được để trống!")]
        public string Name { get; set; }
        public bool Status { get; set; } = true;

        public Role() { }
    }
}