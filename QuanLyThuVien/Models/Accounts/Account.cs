using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("Account")] // Chỉ định bảng tương ứng trong cơ sở dữ liệu
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        public string Username { get; set; }
        public string Password { get; set; }       
        public bool Status { get; set; } = true;

        public Account() { }
    }
}