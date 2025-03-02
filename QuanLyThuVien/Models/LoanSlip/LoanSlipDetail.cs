using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("LoanSlipDetail")]
    public class LoanSlipDetail
    {
        [Key]
        public int Id { get; set; }
        public string LoanSlipID { get; set; }
        public int BookID { get; set; }
        public virtual Book Book { get; set; }
        [Display(Name = "Ngày mượn")]
        [Required(ErrorMessage = "Ngày mượn không được để trống!")]
        public DateTime? BorrowDate { get; set; }
        [Display(Name = "Hạn trả")]
        [Required(ErrorMessage = "Hạn trả không được để trống!")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Tiền cọc không được để trống!")]
        public decimal DepositPrice { get; set; }
        [Required(ErrorMessage = "Tiền mượn không được để trống!")]
        public decimal RentalPrice { get; set; } = Parameters.RentalPrice;

    }
}