using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLyThuVien
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            unitOfWork.AccountRepository.Gets();
            unitOfWork.RoleRepository.Gets();
            unitOfWork.AccountRoleRepository.Gets();
            unitOfWork.ReaderRepository.Gets();
            unitOfWork.ReaderInformationRepository.Gets();

            if(AddHelpers.GetAccount() == null)
                return RedirectToAction("LoginView", "Account");

            Statistical statistical = new Statistical();
            statistical.nBorrowedBooks = GetTotalBorrowedBooks();
            statistical.nUnborrowedBooks = GetTotalUnborrowedBooks();
            statistical.nISBNBooks = unitOfWork.BookISBNRepository.Gets().Count();
            statistical.nLoanSlips = GetTotalLoanSLip();
            statistical.nHistoryLoanSlips = unitOfWork.HistoryLoanSLipRepository.Gets().Count();
            statistical.nReaders = unitOfWork.ReaderInformationRepository.Gets().Count();

            return View(statistical);
        }

        [HttpGet]
        public ActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginView(AccountLogin accountLogin)
        {
            if (ModelState.IsValid)
            {
                var password = Ulti.Md5Hash(accountLogin.Password);

                using (var context = new LibraryContext())
                {
                    var account = context.Account.Where(x => x.Username == accountLogin.Username && x.Password == password).FirstOrDefault();
                    if (account != null && account.Status)
                    {
                        Session["Logged"] = account;
                        FormsAuthentication.SetAuthCookie(accountLogin.Username, accountLogin.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        Session["Error"] = "Tên đăng nhập hoặc Mật khẩu sai!";
                }
                return RedirectToAction("LoginView");
            }
            return View("LoginView", accountLogin);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private int GetTotalBorrowedBooks()
        {
            int n = 0;
            foreach (var item in unitOfWork.BookRepository.Gets())
                if (!item.Status)
                    n++;
            return n;
        }
        private int GetTotalUnborrowedBooks()
        {
            int n = 0;
            foreach (var item in unitOfWork.BookRepository.Gets())
                if (item.Status)
                    n++;
            return n;
        }
        private int GetTotalLoanSLip()
        {
            int n = 0;
            foreach (var item in unitOfWork.LoanSLipRepository.Gets())
                if (item.Status)
                    n++;
            return n;
        }
    }
}