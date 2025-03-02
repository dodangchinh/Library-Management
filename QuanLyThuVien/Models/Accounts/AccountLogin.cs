using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    public class AccountLogin
    {
        [Required(ErrorMessage = "Vui lòng nhập Tài khoản")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}