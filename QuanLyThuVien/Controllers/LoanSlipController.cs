using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using QRCoder;
using ZXing;
using PagedList;

namespace QuanLyThuVien
{
    [Authorize(Roles = "Librarian")]
    public class LoanSlipController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: LoanSlip
        public ActionResult LoanSlipManagerView()
        {
            unitOfWork.LoanSLipRepository.Gets();
            unitOfWork.LoanSLipDetailsRepository.Gets();
            return View();
        }

        public ActionResult LoanSlipDetailView(int? page)
        {
            LoadDataLoanSlip();
            int pageSize = 10; // Số lượng bản ghi trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là 1 nếu không có tham số

            var loanSlips = unitOfWork.LoanSLipRepository.Gets()
                .OrderBy(l => l.Id) // Sắp xếp theo ID
                .ToPagedList(pageNumber, pageSize);

            return PartialView("LoanSlipDetailView", loanSlips);
        }

        [HttpGet]
        public ActionResult AddLoanSlipView()
        {
            ViewBag.ReaderInfoList = unitOfWork.ReaderInformationRepository.Gets();
            ViewBag.Account = AddHelpers.GetAccount().Name;
            TempData["Reader"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult AddLoanSlipView(string ReaderID)
        {
            var AccountID = AddHelpers.GetAccount().Id;
            LoanSlip newLoanSlip = new LoanSlip();
            newLoanSlip.Id = $"LS0{unitOfWork.LoanSLipRepository.Gets().Count() + 1}";
            newLoanSlip.AccountID = AccountID;
            newLoanSlip.ReaderID = ReaderID;

            return RedirectToAction("ChooseBookView", newLoanSlip);
        }

        [HttpGet]
        public ActionResult ChooseBookView(LoanSlip loanSlip)
        {
            var ls = unitOfWork.LoanSLipRepository.Gets();
            ViewBag.Account = AddHelpers.GetAccount().Name;
            Session["LoanSLip"] = loanSlip;
            ViewBag.AllBooks = unitOfWork.BookTitleRepository.Gets();
            ViewBag.ListBook = unitOfWork.BookRepository.Gets();
            ViewBag.SelectedBooks = Session["SelectedBooks"] as List<int> ?? new List<int>();
            return View();
        }
        [HttpPost]
        public JsonResult ChooseBookView(List<int> selectedBooks)
        {
            var ls = unitOfWork.LoanSLipRepository.Gets();
            LoanSlip loanSlip = Session["LoanSLip"] as LoanSlip;
            TempData["LoanSlip"] = loanSlip;

            List<BookISBN> lstBookISBN = new List<BookISBN>();
            bool flag = true;
            foreach (var BooID in selectedBooks)
            {
                var tempBook = unitOfWork.BookRepository.GetByID(BooID);
                if(flag)
                    lstBookISBN.Add(tempBook.BookISBN);
                else
                {
                    foreach (var BookISBN in lstBookISBN)
                    {
                        if(tempBook.ISBN.CompareTo(BookISBN.ISBN) == 0)
                            return Json(new { success = false, message = "Chỉ được mượn 1 Sách của 1 ISBN!" });
                    }
                    lstBookISBN.Add(tempBook.BookISBN);
                }
                flag = false;
            }

            List<LoanSlipDetail> lstLoanSlipDetails = new List<LoanSlipDetail>();

            foreach (var item in selectedBooks)
            {
                LoanSlipDetail loanSlipDetail = new LoanSlipDetail();

                loanSlipDetail.LoanSlipID = loanSlip.Id;
                loanSlipDetail.BookID = item;
                loanSlipDetail.Book = unitOfWork.BookRepository.GetByID(item);
                loanSlipDetail.BorrowDate = DateTime.Now;
                loanSlipDetail.DueDate = DateTime.Now.AddDays(7);
                loanSlipDetail.DepositPrice = loanSlipDetail.Book.Price * Parameters.DepositRercentage;
                lstLoanSlipDetails.Add(loanSlipDetail);
            }

            Session["SelectedBooks"] = selectedBooks;
            TempData["ListLoanSlipDetail"] = lstLoanSlipDetails;

            return Json(new { success = true, redirectUrl = Url.Action("AddLoanSlipDetailView", "LoanSlip") });
        }
        
        [HttpGet]
        public ActionResult AddLoanSlipDetailView()
        {
            if (TempData["QRCodeImage"] != null)
                return View();
            List<LoanSlipDetail> lstLoanSlipDetail = (TempData["ListLoanSlipDetail"] as IEnumerable<LoanSlipDetail>).ToList();

            ViewBag.AllBooks = unitOfWork.BookTitleRepository.Gets();
            ViewBag.ListBook = GetListBook(Session["SelectedBooks"] as List<int>);
            ViewBag.ReaderName = unitOfWork.ReaderInformationRepository.GetByID((TempData["LoanSLip"] as LoanSlip).ReaderID).Reader.Name;
            ViewBag.ReaderBOF = unitOfWork.ReaderInformationRepository.GetByID((TempData["LoanSLip"] as LoanSlip).ReaderID).BOF.Value.Date;
            ViewBag.ReaderCitizenID = unitOfWork.ReaderInformationRepository.GetByID((TempData["LoanSLip"] as LoanSlip).ReaderID).CitizenID;
            ViewBag.ReaderEmail = unitOfWork.ReaderInformationRepository.GetByID((TempData["LoanSLip"] as LoanSlip).ReaderID).Email;
            ViewBag.BorrowDate = lstLoanSlipDetail[0].BorrowDate;
            ViewBag.DueDate = lstLoanSlipDetail[0].DueDate;

            decimal DepositPrice = 0;
            foreach (var item in lstLoanSlipDetail)
                DepositPrice += item.DepositPrice;

            ViewBag.DepositPrice = DepositPrice;
            ViewBag.RentalPrice = Parameters.RentalPrice * lstLoanSlipDetail.Count();

            TempData["ListLoanSlipDetail"] = lstLoanSlipDetail;
            TempData["LoanSlip"] = TempData["LoanSLip"] as LoanSlip;
            return View();
        }

        [HttpPost]
        public JsonResult AddLoanSlipDetailView(string id)
        {
            List<LoanSlipDetail> lstLoanSlipDetail = TempData["ListLoanSlipDetail"] as List<LoanSlipDetail>;

            LoanSlip loanSlip = TempData["LoanSLip"] as LoanSlip;
            loanSlip.ListLoanSlipDetail = lstLoanSlipDetail;

            unitOfWork.LoanSLipRepository.Insert(loanSlip);
            foreach (var item in lstLoanSlipDetail)
                unitOfWork.LoanSLipDetailsRepository.Insert(item);

            GetTotalPriceLoanSlip();
            SetBookBorrowedStatus(lstLoanSlipDetail);
            SetReaderStatus(loanSlip);
            string userDownloadsFolder = CreateQRCode((TempData["LoanSLip"] as LoanSlip).Id);

            return Json(new { success = true, redirectUrl = Url.Action("AddLoanSlipDetailView", "LoanSlip") });
        }


        public ActionResult HistoryLoanSlipManagerView()
        {
            unitOfWork.PenaltyReasonRepository.Gets();
            unitOfWork.HistoryLoanSLipRepository.Gets();
            return View();
        }
        public ActionResult HistoryLoanSlipDetailView(int? page)
        {
            LoadDataLoanSlip();
            int pageSize = 10; // Số lượng bản ghi trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là 1 nếu không có tham số

            var historyLoanSlips = unitOfWork.HistoryLoanSLipRepository.Gets() // Giả sử bạn có repository này
                .OrderBy(h => h.Id) // Sắp xếp theo ID
                .ToPagedList(pageNumber, pageSize);

            return PartialView("HistoryLoanSlipDetailView", historyLoanSlips);
        }

        [HttpGet]
        public ActionResult GetQRLoanSlipView()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetQRLoanSlipView(HttpPostedFileBase qrImage)
        {
            if (qrImage != null && qrImage.ContentLength > 0)
            {
                try
                {
                    // Đọc ảnh từ bộ nhớ
                    using (var memoryStream = new MemoryStream())
                    {
                        qrImage.InputStream.CopyTo(memoryStream); // Chép dữ liệu ảnh vào MemoryStream
                        using (var bitmap = new Bitmap(memoryStream)) // Tạo đối tượng Bitmap từ MemoryStream
                        {
                            var barcodeReader = new BarcodeReader();
                            var result = barcodeReader.Decode(bitmap);

                            if (result != null)
                            {
                                var loanSlip = GetByIdMD5(result.Text);
                                if (loanSlip != null)
                                    if(loanSlip.Status)
                                    {
                                        TempData["LoanSlipID"] = loanSlip.Id;
                                        return Json(new { success = true, message = $"Lấy thông tin thành công!", redirectUrl = Url.Action("ReturnBookView", "LoanSlip") });
                                    }                                       
                                return Json(new { success = false, message = "Không tìm thấy thông tin Phiếu mượn" });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Không thể đọc mã QR từ ảnh" });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "Vui lòng chọn một ảnh hợp lệ." });
            }
        }

        private LoanSlip GetByIdMD5(string id)
        {
            foreach (var item in unitOfWork.LoanSLipRepository.Gets())
                if(id.CompareTo(Ulti.Md5Hash(item.Id)) == 0)
                    return item;
            return null;
        }

        [HttpGet]
        public ActionResult ReturnBookView()
        {
            LoadDataLoanSlip();
            string id = TempData["LoanSLipID"] as string;
            ViewBag.PenaltyReason = unitOfWork.PenaltyReasonRepository.Gets();
            return View(unitOfWork.LoanSLipRepository.GetByID(id));
        }

        [HttpPost]
        public ActionResult ReturnBookView(string PenaltyId, string LoanSLipId, string RentalPrice)
        {
            var itemLS = unitOfWork.LoanSLipRepository.GetByID(LoanSLipId);
            if (PenaltyId.CompareTo("undefined") == 0)
            {
                ViewBag.PenaltyReason = unitOfWork.PenaltyReasonRepository.Gets();
                return Json(new { success = false, 
                    message = $"Vui lòng chọn mức phạt!",
                    data = new
                    {
                        itemLS
                    }
                });
            }
            else if (PenaltyId.CompareTo("PR1") != 0 && DateTime.Now < itemLS.DueDate)
            {
                ViewBag.PenaltyReason = unitOfWork.PenaltyReasonRepository.Gets();
                return Json(new
                {
                    success = false,
                    message = $"Độc giả này trả đúng hạn! Hãy hủy bỏ mức phạt",
                    data = new
                    {
                        itemLS
                    }
                });
            }
                          
            var itemPR = new PenaltyReason();
            HistoryLoanSLip historyLoanSLip = new HistoryLoanSLip();
            historyLoanSLip.Id = $"HLS0{unitOfWork.HistoryLoanSLipRepository.Gets().Count() + 1}";
            historyLoanSLip.LoanSlipID = LoanSLipId;
            itemPR = unitOfWork.PenaltyReasonRepository.GetByID(PenaltyId);
            historyLoanSLip.PenaltyReasonID = String.Copy(PenaltyId);
            historyLoanSLip.LoanPaid = itemLS.TotalPrice;
            if(itemPR.Price > 0)
            {
                TimeSpan timeSpan = historyLoanSLip.CreateAt.Date - itemLS.DueDate.Value;
                historyLoanSLip.FineMoney = (timeSpan.Days) + itemPR.Price;
            }               
            historyLoanSLip.TotalPaid = historyLoanSLip.LoanPaid + historyLoanSLip.FineMoney;
            
            itemLS.Status = false;

            ChangeStatusBook(itemLS.ListLoanSlipDetail);
            unitOfWork.LoanSLipRepository.Update(itemLS);
            unitOfWork.HistoryLoanSLipRepository.Insert(historyLoanSLip);

            return Json(new { success = true, message = $"Trả sách thành công!", redirectUrl = Url.Action("HistoryLoanSlipManagerView", "LoanSlip"), JsonRequestBehavior.AllowGet });
        }

        private void ChangeStatusBook(List<LoanSlipDetail> loanSlipDetails)
        {
            foreach (var item in loanSlipDetails)
            {
                item.Book.Status = true;
                unitOfWork.BookRepository.Update(item.Book);
            }
        }

        public string CreateQRCode(string LoanSlipID)
        {
            string userDownloadsFolder;
            // Tạo MemoryStream để lưu ảnh
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator codeGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = codeGenerator.CreateQrCode(Ulti.Md5Hash(LoanSlipID), QRCodeGenerator.ECCLevel.Q); // Correct usage
                QRCode qrCode = new QRCode(qrCodeData); // Instantiate QRCode object with QRCodeData

                // Tạo ảnh QR Code
                using (Bitmap image = qrCode.GetGraphic(10)) // Generate the QR Code graphic
                {
                    // Lưu vào MemoryStream
                    image.Save(ms, ImageFormat.Png);

                    // Chuyển đổi thành Base64 để hiển thị trong view
                    TempData["QRCodeImage"] = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());

                    // Lưu ảnh vào thư mục Downloads của người dùng
                    userDownloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                    string filePath = Path.Combine(userDownloadsFolder, $"QRCode{LoanSlipID}.png");

                    // Lưu ảnh ra file
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
            }
            return userDownloadsFolder;
        }

        public ActionResult Filter(string id)
        {
            ViewBag.ListBook = GetListBookByBookTitle(id);
            ViewBag.AllBooks = unitOfWork.BookTitleRepository.Gets();
            return View("ChooseBookView");  
        }
        public ActionResult FilterReader(string id)
        {
            TempData["Reader"] = id;
            ViewBag.LoanSlip = GetLoanSLipByReader(id);
            ViewBag.ReaderInfoList = unitOfWork.ReaderInformationRepository.Gets();
            return View("AddLoanSlipView", GetLoanSLipByReader(id));
        }

        private IEnumerable<Book> GetListBookByBookTitle(string id)
        {
            var lstBook = new List<Book>();
            foreach (var item in unitOfWork.BookRepository.Gets())
            {
                if(item.BookISBN.BookTitle.Id.CompareTo(id) == 0)
                    lstBook.Add(item);
            }
            return lstBook;
        }

        private LoanSlip GetLoanSLipByReader(string id)
        {
            var lstLoanSlip = new LoanSlip();
            foreach (var item in unitOfWork.LoanSLipRepository.Gets())
            {
                if (item.ReaderID.CompareTo(id) == 0 && item.Status)
                    lstLoanSlip = item;
            }
            return lstLoanSlip;
        }

        private IEnumerable<Book> GetListBook(List<int> lstID)
        {
            var lstBook = new List<Book>();
            foreach (var id in lstID)
            {
                lstBook.Add(unitOfWork.BookRepository.GetByID(id));
            }       
            return lstBook;
        }

        private void SetBookBorrowedStatus(List<LoanSlipDetail> lstLoanSlipDetail)
        {
            var lstBook = unitOfWork.BookRepository.Gets();
            foreach (var loanSlip in lstLoanSlipDetail)
                foreach (var book in lstBook)
                    if(loanSlip.Book.Id == book.Id)
                    {
                        book.Status = false;
                        unitOfWork.BookRepository.Update(book);
                    }               
        }

        private void SetReaderStatus(LoanSlip loanSlip)
        {
            var Reader = unitOfWork.ReaderRepository.GetByID(loanSlip.ReaderID);
            Reader.Status = false;
            unitOfWork.ReaderRepository.Update(Reader);
        }

        private void InitDropDown()
        {
            ViewBag.ReaderList = new List<SelectListItem>();
            foreach (var item in unitOfWork.ReaderRepository.Gets())
                if(item.Status)
                    (ViewBag.ReaderList as List<SelectListItem>).Add(new SelectListItem { Text = item.Name, Value = item.Id });          
        }

        private void GetTotalPriceLoanSlip()
        {
            foreach (var loanSLip in unitOfWork.LoanSLipRepository.Gets())
            {
                foreach (var loanSLipDetail in unitOfWork.LoanSLipDetailsRepository.Gets())
                {
                    if (loanSLip.Id.CompareTo(loanSLipDetail.LoanSlipID) == 0)
                        loanSLip.TotalDepositPrice += loanSLipDetail.DepositPrice;
                    else if (loanSLip.TotalDepositPrice != 0)
                        break;
                }

                int count = 0;
                foreach (var loanSLipDetail in unitOfWork.LoanSLipDetailsRepository.Gets())
                {
                    if (loanSLip.Id.CompareTo(loanSLipDetail.LoanSlipID) == 0)
                        count++;
                    else if (count != 0)
                        break;
                }

                foreach (var loanSLipDetail in unitOfWork.LoanSLipDetailsRepository.Gets())
                {
                    if (loanSLip.Id.CompareTo(loanSLipDetail.LoanSlipID) == 0)
                    {
                        loanSLip.BorrowDate = loanSLipDetail.BorrowDate;
                        loanSLip.DueDate = loanSLipDetail.DueDate;
                        break;
                    }
                }
                loanSLip.TotalRentalPrice = Parameters.RentalPrice * count;
                loanSLip.TotalPrice = loanSLip.TotalRentalPrice + loanSLip.TotalDepositPrice;
            }
        }
        private void LoadDataLoanSlip()
        {
            if (!Parameters.flagLoadLoanSlipDetail)
            {
                Parameters.flagLoadLoanSlipDetail = true;
                foreach (var loanSlip in unitOfWork.LoanSLipRepository.Gets())
                {
                    foreach (var loanSLipDetail in unitOfWork.LoanSLipDetailsRepository.Gets().ToList())
                    {
                        if (loanSlip.ListLoanSlipDetail == null)
                            loanSlip.ListLoanSlipDetail = new List<LoanSlipDetail>();
                        if (loanSlip.Id.CompareTo(loanSLipDetail.LoanSlipID) == 0)
                            loanSlip.ListLoanSlipDetail.Add(loanSLipDetail);
                        else if (loanSlip.ListLoanSlipDetail.Count > 0)
                            break;
                    }

                }

                GetTotalPriceLoanSlip();
            }
        }
    }
}