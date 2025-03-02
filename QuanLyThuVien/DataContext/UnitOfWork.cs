using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    public class UnitOfWork
    {
        public static LibraryContext context = new LibraryContext();

        private static BaseRepository<Account> _accountRepository;
        private static BaseRepository<Role> _roleRepository;
        private static BaseRepository<AccountRole> _accountRoleRepository;

        private static BaseRepository<Reader> _readerRepository;
        private static BaseRepository<ReaderInformation> _readerInformationRepository;

        private static BaseRepository<CategoryBook> _categoryBookRepository;
        private static BaseRepository<Author> _authorRepository;
        private static BaseRepository<Publisher> _publisherRepository;
        private static BaseRepository<Language> _languageRepository;
        private static BaseRepository<Translator> _translatorRepository;
        private static BaseRepository<BookTitle> _bookTitleRepository;
        private static BaseRepository<BookISBN> _bookISBNRepository;
        private static BaseRepository<Book> _bookRepository;

        private static BaseRepository<LoanSlip> _loanSlipRepository;
        private static BaseRepository<LoanSlipDetail> _loanSlipDetailsRepository;
        private static BaseRepository<PenaltyReason> _penaltyReasonRepository;
        public static BaseRepository<HistoryLoanSLip> _historyLoanSlipRepository;


        public BaseRepository<Account> AccountRepository
        {
            get 
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new BaseRepository<Account>(context);
                    Parameters.nAccount = _accountRepository.Gets().Count();
                }                   
                return _accountRepository;
            }
        }
        public BaseRepository<Role> RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new BaseRepository<Role>(context);
                    Parameters.nAccount = _roleRepository.Gets().Count();
                }                  
                return _roleRepository;
            }
        }
        public BaseRepository<AccountRole> AccountRoleRepository
        {
            get
            {
                if (_accountRoleRepository == null)
                {
                    _accountRoleRepository = new BaseRepository<AccountRole>(context);
                    Parameters.nAccount = _accountRoleRepository.Gets().Count();
                }                   
                return _accountRoleRepository;
            }
        }

        public BaseRepository<Reader> ReaderRepository
        {
            get
            {
                if(_readerRepository == null)
                {
                    _readerRepository = new BaseRepository<Reader>(context);
                    Parameters.nReader = _readerRepository.Gets().Count();
                }
                return _readerRepository;
            }
        }
        public BaseRepository<ReaderInformation> ReaderInformationRepository
        {
            get
            {
                if(_readerInformationRepository == null)
                    _readerInformationRepository = new BaseRepository<ReaderInformation>(context);
                return _readerInformationRepository;
            }
        }


        public BaseRepository<CategoryBook> CategoryBookRepository
        {
            get
            {
                if(_categoryBookRepository == null)
                    _categoryBookRepository = new BaseRepository<CategoryBook>(context);
                return _categoryBookRepository;
            }
        }
        public BaseRepository<Author> AuthorRepository
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new BaseRepository<Author>(context);
                return _authorRepository;
            }
        }
        public BaseRepository<Publisher> PublisherRepository
        {
            get
            {
                if (_publisherRepository == null)
                    _publisherRepository = new BaseRepository<Publisher>(context);
                return _publisherRepository;
            }
        }
        public BaseRepository<Language> LanguageRepository
        {
            get
            {
                if (_languageRepository == null)
                    _languageRepository = new BaseRepository<Language>(context);
                return _languageRepository;
            }
        }

        public BaseRepository<Translator> TranslatorRepository
        {
            get
            {
                if (_translatorRepository == null)
                    _translatorRepository = new BaseRepository<Translator>(context);
                return _translatorRepository;
            }
        }

        public BaseRepository<BookTitle> BookTitleRepository
        {
            get
            {
                if (_bookTitleRepository == null)
                    _bookTitleRepository = new BaseRepository<BookTitle>(context);
                return _bookTitleRepository;
            }
        }
        public BaseRepository<BookISBN> BookISBNRepository
        {
            get
            {
                if(_bookISBNRepository == null)
                    _bookISBNRepository = new BaseRepository<BookISBN>(context);
                return _bookISBNRepository;
            }
        }

        public BaseRepository<Book> BookRepository
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BaseRepository<Book>(context);
                return _bookRepository;
            }
        }


        public BaseRepository<LoanSlip> LoanSLipRepository
        {
            get
            {
                if(_loanSlipRepository == null)
                    _loanSlipRepository = new BaseRepository<LoanSlip>(context);
                return _loanSlipRepository;
            }
        }
        public BaseRepository<LoanSlipDetail> LoanSLipDetailsRepository
        {
            get
            {
                if(_loanSlipDetailsRepository == null)
                    _loanSlipDetailsRepository = new BaseRepository<LoanSlipDetail>(context);
                return _loanSlipDetailsRepository;
            }
        }
        public BaseRepository<PenaltyReason> PenaltyReasonRepository
        {
            get
            {
                if (_penaltyReasonRepository == null)
                    _penaltyReasonRepository = new BaseRepository<PenaltyReason>(context);
                return _penaltyReasonRepository;
            }
        }
        
        public BaseRepository<HistoryLoanSLip> HistoryLoanSLipRepository
        {
            get
            {
                if (_historyLoanSlipRepository == null)
                    _historyLoanSlipRepository = new BaseRepository<HistoryLoanSLip> (context);
                return _historyLoanSlipRepository;
            }
        }
    }
}