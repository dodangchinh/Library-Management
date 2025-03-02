using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("ReaderInformation")]
    public class ReaderInformation
    {
        [Key]
        [ForeignKey("Reader")]
        public string ReaderID { get; set; }
        public virtual Reader Reader { get; set; }
        public DateTime? BOF {  get; set; }
        public int CitizenID { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool Gender {  get; set; }
        [NotMapped]
        public virtual bool Status { get; set; } = true;
    }
}