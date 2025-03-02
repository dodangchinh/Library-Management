using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("BookISBN")]
    public class BookISBN
    {
        [Key]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        public string BookTitleID { get; set; }
        public virtual BookTitle BookTitle { get; set; }
        public string AuthorID {  get; set; }
        public virtual Author Author { get; set; }
        public string LanguageID { get; set; }
        public virtual Language Language { get; set; }
        public bool Status { get; set; } = true;
    }
}