using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLyThuVien
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult AccountView(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var accounts = unitOfWork.AccountRoleRepository.Gets() // Giả sử bạn có repository này
                .OrderBy(a => a.No)
                .ToPagedList(pageNumber, pageSize);

            return View(accounts);
        }

        [HttpGet]
        public ActionResult EditAccountView(int no)
        {
            var accountRole = unitOfWork.AccountRoleRepository.GetByID(no);
            Session["Account"] = accountRole;
            InitDropDown(accountRole);
           
            return View(accountRole);
        }

        [HttpPost]
        public JsonResult EditAccountView(AccountRole newAccountRole)
        {
            InitDropDown(Session["Account"] as AccountRole);
            var accountRole = unitOfWork.AccountRoleRepository.GetByID(newAccountRole.No);

            bool flag = false;

            if (newAccountRole.account.Name != accountRole.account.Name ||
                newAccountRole.account.Username != accountRole.account.Username ||
                newAccountRole.RoleID != accountRole.RoleID ||
                newAccountRole.account.Status != accountRole.account.Status)
            {
                flag = true;
            }

            if (!flag)
                return Json(new { success = true, message = "Không có gì thay đổi!" });
           

            if (ModelState.IsValid)
            {
                newAccountRole.AccountID = accountRole.AccountID;
                newAccountRole.account.Id = accountRole.AccountID;
                newAccountRole.account.Password = accountRole.account.Password;

                unitOfWork.AccountRepository.Update(newAccountRole.account);
                unitOfWork.AccountRoleRepository.Update(newAccountRole);

                return Json(new { success = true, message = "Cập nhật tài khoản thành công!" });
            }
            else
                return Json(new { success = true, message = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại!" });
                
        }

        public ActionResult RoleView(int? page)
        {
            int pageSize = 10; // Số lượng bản ghi trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là 1

            var roles = unitOfWork.RoleRepository.Gets() // Giả sử bạn có repository này
                .OrderBy(r => r.Id)
                .ToPagedList(pageNumber, pageSize);

            return View(roles);
        }

        [HttpGet]
        public ActionResult AddRoleView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddRoleView(Role role)
        {
            if (ModelState.IsValid)
            {
                bool flag = false;
                foreach (var isRole in unitOfWork.RoleRepository.Gets())
                    if (isRole.Name.CompareTo(role.Name) == 0)
                        flag = true;

                if (flag)
                {
                    return Json(new { success = true, message = "Đã tồn tại!" });
                }

                role.Id = $"R{++Parameters.nRole}";
                role.Status = true;
                unitOfWork.RoleRepository.Insert(role);
                return Json(new { success = true, message = "Thêm thành công!" });
            }
            return Json(new { success = false, message = "Tên vai trò không được để trống!" });
        }
                

        // GET: Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginView()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginView(AccountLogin accountLogin)
        {
            if(ModelState.IsValid)
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

        //GET: Account/AddAccountView
        [HttpGet]
        public ActionResult AddAccountView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAccountView(AccountRegister accountRegister)
        {
            if(ModelState.IsValid)
            {
                var encodePass = Ulti.Md5Hash(accountRegister.Password);
                var newAccount = new Account
                {
                    Name = accountRegister.Name,
                    Username = accountRegister.Username,
                    Password = encodePass,
                    Status = true,
                };

                var roleUser = unitOfWork.RoleRepository.GetByID("R3");

                var newAccountRole = new AccountRole
                {
                    AccountID = newAccount.Id,
                    RoleID = roleUser.Id,
                    account = newAccount,
                    role = roleUser,
                };

                unitOfWork.AccountRepository.Insert(newAccount);
                unitOfWork.AccountRoleRepository.Insert(newAccountRole);

                return RedirectToAction("LoginView");
            }
            return View("AddAccountView", accountRegister);
        }

        [HttpPost]
        public ActionResult LogOutView()
        {
            Session["Logged"] = null;
            Response.Cookies["AccountId"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult StatisticalView()
        {
            Statistical statistical = new Statistical();
            statistical.nBorrowedBooks = GetTotalBorrowedBooks();
            statistical.nUnborrowedBooks = GetTotalUnborrowedBooks();
            statistical.nISBNBooks = unitOfWork.BookISBNRepository.Gets().Count();
            statistical.nLoanSlips = GetTotalLoanSLip();
            statistical.nHistoryLoanSlips = unitOfWork.HistoryLoanSLipRepository.Gets().Count();
            statistical.nReaders = unitOfWork.ReaderInformationRepository.Gets().Count();
            return View(statistical);
        }

        private int GetTotalBorrowedBooks()
        {
            int n = 0;
            foreach (var item in unitOfWork.LoanSLipRepository.Gets())
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

        private void InitDropDown(AccountRole accountRole)
        {
            ViewBag.RoleList = unitOfWork.RoleRepository.Gets()
               .Select(role => new SelectListItem
               {
                   Value = role.Id.ToString(),
                   Text = role.Name,
                   Selected = (role.Id.CompareTo(accountRole.RoleID) == 0)
               });

            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Text="Kích hoạt", Value="true", Selected = accountRole.account.Status == true },
                new SelectListItem { Text="Khóa", Value ="false", Selected = accountRole.account.Status == false },
            };
        }

    }
}