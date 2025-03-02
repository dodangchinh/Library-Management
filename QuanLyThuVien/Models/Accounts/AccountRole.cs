using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("AccountRole")]
    public class AccountRole
    {
        [Key]
        public int No { get; set; }
        public int AccountID { get; set; }
        public string RoleID { get; set; }
        public virtual Account account { get; set; }
        public virtual Role role { get; set; }

        
    }
}