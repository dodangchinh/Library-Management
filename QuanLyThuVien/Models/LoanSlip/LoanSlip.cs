using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("LoanSlip")]
    public class LoanSlip
    {
        [Key]
        public string Id { get; set; }
        public virtual Account Account { get; set; }
        public int AccountID { get; set; }
        public string ReaderID { get; set; }
        public virtual ReaderInformation ReaderInformation { get; set; }
        [NotMapped]
        public virtual List<LoanSlipDetail> ListLoanSlipDetail { get; set; }
        [NotMapped]
        public DateTime? BorrowDate { get; set; }
        [NotMapped]
        public DateTime? DueDate { get; set; }
        [NotMapped]
        public decimal TotalDepositPrice { get; set; }
        [NotMapped]
        public decimal TotalRentalPrice { get; set; } 
        [NotMapped]
        public decimal TotalPrice { get; set; }
        public bool Status { get; set; } = true;
    }
}