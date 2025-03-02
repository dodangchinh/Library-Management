using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    public class AccountRegister
    {
        [Required(ErrorMessage = "Họ tên không được để trống!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập lại mật khẩu!")]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại phải giống với mật khẩu!")]
        public string PasswordRetype { get; set; }

        public AccountRegister() { }
    }
}