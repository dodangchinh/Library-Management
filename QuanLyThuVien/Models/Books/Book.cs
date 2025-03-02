using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; } 
        public string ISBN { get; set; }
        public virtual BookISBN BookISBN { get; set; }
        public string PublisherID { get; set; }
        public virtual Publisher Publisher { get; set; }
        public string TranslatorID { get; set; }
        public virtual Translator Translator { get; set; }
        public string LanguageID { get; set; }  
        public virtual Language Language { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; } = true;
    }
}