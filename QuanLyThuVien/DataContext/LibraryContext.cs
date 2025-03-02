using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;


namespace QuanLyThuVien
{
    public class LibraryContext:DbContext
    {
        public LibraryContext() : base("name=LibraryDBContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<AccountRole> AccountRole { get; set; }

        public DbSet<Reader> Reader { get; set; }
        public DbSet<ReaderInformation> ReaderInformation { get; set; }

        public DbSet<CategoryBook> CategoryBook { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; } 
        public DbSet<Language> Language { get; set; }
        public DbSet<Translator> Translator { get; set; }
        public DbSet<BookTitle> BookTitle { get; set; }
        public DbSet<BookISBN> BookEdition { get; set; }
        public DbSet<Book> Book { get; set; }

        public DbSet<LoanSlip> LoanSlip { get; set; }
        public DbSet<LoanSlipDetail> LoanSlipDetails { get; set; }
        public DbSet<PenaltyReason> PenaltyReason { get; set; }
        public DbSet<HistoryLoanSLip> HistoryLoanSLip { get; set; }
    }
}