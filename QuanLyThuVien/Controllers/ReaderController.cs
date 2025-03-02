using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien
{
    [Authorize(Roles = "Librarian")]
    public class ReaderController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Reader
        public ActionResult ReaderView(int? page)
        {
            int pageSize = 10; // Số lượng bản ghi trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có tham số

            // Lấy danh sách và tạo PagedList trực tiếp
            var readersList = unitOfWork.ReaderInformationRepository.Gets()
                .OrderBy(r => r.ReaderID); // Sắp xếp trước khi phân trang

            // Tạo PagedList cụ thể
            PagedList<ReaderInformation> readers = new PagedList<ReaderInformation>(readersList, pageNumber, pageSize);

            return View(readers);
        }

        [HttpGet]
        public ActionResult AddReaderView()
        {
            InitDropDown();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddReaderView(ReaderInformation readerInformation)
        {
            InitDropDown();
            var date = DateTime.Now.AddYears(-18);
            if (readerInformation.BOF > DateTime.Now.AddYears(-18))
                return Json(new { success = false, message = "Độc giả phải trên 18 tuổi!" });
            else if (ModelState.IsValid)
            {
                var isEmailValid = await AddHelpers.IsEmailValid(readerInformation.Email);
                if (!isEmailValid)
                    return Json(new { success = false, message = "Email không tồn tại!" });

                int flag = isExit(readerInformation);
                if (flag != 0)
                {
                    switch (flag)
                    {
                        case 1:
                            return Json(new { success = false, message = "CCCD này đã tồn tại!" });
                        case 2:
                            return Json(new { success = false, message = "Email không tồn tại!" });
                    }                 
                }
                              
                var newReader = new Reader
                {
                    Id = $"RD{++Parameters.nReader}",
                    Name = readerInformation.Reader.Name,
                    Status = true,
                };

                readerInformation.ReaderID = newReader.Id;
                readerInformation.Reader = newReader;

                unitOfWork.ReaderRepository.Insert(newReader);
                unitOfWork.ReaderInformationRepository.Insert(readerInformation);
                return Json(new { success = true, message = "Thêm thành công!" });
            }
            return View();
        }

        private void InitDropDown()
        {
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem { Text="Nam", Value="true" },
                new SelectListItem { Text="Nữ", Value ="false" },
            };
        }

        private int isExit(ReaderInformation readerInformation)
        {
            foreach (var item in unitOfWork.ReaderInformationRepository.Gets())
            {
                if (item.Status == false)
                    continue;
                if (item.CitizenID == readerInformation.CitizenID)
                    return 1;
                if (item.Email.CompareTo(readerInformation.Email) == 0)
                    return 2;
            }
            return 0;
        }

        [HttpGet]
        public ActionResult EditReaderView(string readerID)
        {
            var readerInformation = unitOfWork.ReaderInformationRepository.GetByID(readerID);
            InitDropDown(readerInformation);
            readerInformation.Status = false;
            ViewBag.BOF = readerInformation.BOF.Value.ToString("mm/dd/yyyy");
            return View(readerInformation);
        }

        [HttpPost]
        public async Task<ActionResult> EditReaderView(ReaderInformation readerInformation)
        {
            InitDropDown();
            if (readerInformation.BOF > DateTime.Now.AddYears(-18))
            {
                ModelState.AddModelError("BOF", "Độc giả phải trên 18 tuổi!");
                return View(readerInformation);
            }
            else if (ModelState.IsValid)
            {
                var isEmailValid = await AddHelpers.IsEmailValid(readerInformation.Email);
                if (!isEmailValid)
                {
                    ModelState.AddModelError("Email", "Email này không tồn tại!");
                    return View(readerInformation);
                }

                int flag = isExit(readerInformation);
                if (flag != 0)
                {
                    switch (flag)
                    {
                        case 1:
                            ModelState.AddModelError("CitizenID", "CCCD này đã tồn tại!");
                            break;
                        case 2:
                            ModelState.AddModelError("Email", "Email này đã tồn tại!");
                            break;
                    }
                    return View(readerInformation);
                }

                var newReader = new Reader
                {
                    Id = readerInformation.ReaderID,
                    Name = readerInformation.Reader.Name,
                    Status = true,
                };

                readerInformation.ReaderID = newReader.Id;
                readerInformation.Reader = newReader;

                unitOfWork.ReaderRepository.Update(newReader);
                unitOfWork.ReaderInformationRepository.Update(readerInformation);
                return RedirectToAction("ReaderView", "Reader");
            }
            return View();
        }

        private void InitDropDown(ReaderInformation readerInformation)
        {
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem { Text="Nam", Value="true", Selected = readerInformation.Gender == true },
                new SelectListItem { Text="Nữ", Value ="false", Selected = readerInformation.Gender == false },
            };
            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Text="Kích hoạt", Value="true", Selected = readerInformation.Reader.Status == true },
                new SelectListItem { Text="Khóa", Value ="false", Selected = readerInformation.Reader.Status == false },
            };
        }

        [HttpGet]
        public ActionResult DeleteReaderView(string readerID)
        {
            var readerInformation = unitOfWork.ReaderInformationRepository.GetByID(readerID);
            InitDropDown(readerInformation);
            Session["ReaderInformation"] = readerInformation;
            return View(readerInformation);
        }

        [HttpPost]
        public ActionResult DeleteReaderView()
        {
            var readerInformation = Session["ReaderInformation"] as ReaderInformation;
            InitDropDown(readerInformation);
            if (isExist(readerInformation))
                return Json(new { success = true, message = "Độc giả này đang được sử dụng!" });

            unitOfWork.ReaderRepository.Delete(readerInformation.Reader);
            return Json(new { success = true, message = "Xóa thành công!" });
        }

        private bool isExist(ReaderInformation readerInformation)
        {
            foreach (var item in unitOfWork.LoanSLipRepository.Gets())
                if (item.ReaderID.CompareTo(readerInformation.ReaderID) == 0)
                    return true;
            return false;
        }
    }
}