using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("Publisher")]
    public class Publisher
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống!")]
        public string Address { get; set; }
    }
}