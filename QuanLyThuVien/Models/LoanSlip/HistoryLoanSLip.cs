using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("HistoryLoanSLip")]
    public class HistoryLoanSLip
    {
        public string Id { get; set; }  
        public string LoanSlipID { get; set; }  
        public virtual LoanSlip LoanSlip { get; set; }
        public string PenaltyReasonID { get; set; } 
        public virtual PenaltyReason PenaltyReason { get; set; }
        public decimal LoanPaid { get; set; }
        public decimal FineMoney { get; set; } = 0;
        public decimal TotalPaid { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}