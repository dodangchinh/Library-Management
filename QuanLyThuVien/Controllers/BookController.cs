using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace QuanLyThuVien
{
    [Authorize(Roles = "Librarian")]
    public class BookController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult BookManagerView(int? page, string tab = "book")
        {
            ViewBag.CurrentPage = page;
            ViewBag.CurrentTab = tab;
            return View();
        }

        // CATEGORY BOOK
        public ActionResult CategoryBookView()
        {
            var categoryBooks = unitOfWork.CategoryBookRepository.Gets() // Giả sử bạn có repository này
                .OrderBy(c => c.Id); // Sắp xếp theo ID
            return PartialView("CategoryBookView", categoryBooks);
        }
        [HttpGet]
        public ActionResult AddCategoryBookView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategoryBookView(CategoryBook categoryBook)
        {
            if (!ModelState.IsValid)
                return View(categoryBook);

            foreach (var item in unitOfWork.CategoryBookRepository.Gets())
                if (item.Name.CompareTo(categoryBook.Name) == 0)
                    return Json(new { success = false, message = "Thể loại đã tồn tại!" });

            categoryBook.Id = $"C{unitOfWork.CategoryBookRepository.Gets().Count() + 1}";

            unitOfWork.CategoryBookRepository.Insert(categoryBook);
            return Json(new { success = true, message = "Thêm thành công!" });
        }

        [HttpGet]
        public ActionResult EditCategoryBookView(string id)
        {
            var categoryBook = unitOfWork.CategoryBookRepository.GetByID(id);
            InitDropDown(categoryBook.Status);
            Session["Save"] = categoryBook;
            return View(categoryBook);
        }

        [HttpPost]
        public ActionResult EditCategoryBookView(CategoryBook categoryBook)
        {
            InitDropDown(categoryBook.Status);
            if (!ModelState.IsValid)
                return View(categoryBook);

            var saveCategoryBook = Session["Save"] as CategoryBook;
            foreach (var item in unitOfWork.CategoryBookRepository.Gets())
            {
                if(item.Name.CompareTo(categoryBook.Name) == 0 && item.Name.CompareTo(saveCategoryBook.Name) != 0)
                    return Json(new { success = false, message = "Thể loại đã tồn tại!" });
            }

            if (categoryBook.Name.CompareTo(saveCategoryBook.Name) == 0 &&
                categoryBook.Status == saveCategoryBook.Status)
            {               
                ModelState.Clear();
                return Json(new { success = false, message = "Không có gì thay đổi!" });
            }

            unitOfWork.CategoryBookRepository.Update(categoryBook);
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpGet]
        public ActionResult DeleteCategoryBookView(string id)
        {
            var categoryBook = unitOfWork.CategoryBookRepository.GetByID(id);
            InitDropDown(categoryBook.Status);
            return View(categoryBook);
        }
        [HttpPost]
        public ActionResult DeleteCategoryBookView(CategoryBook categoryBook)
        {
            if(isExist(categoryBook))
                return Json(new { success = true, message = "Thể loại này đang được sử dụng!" });

            unitOfWork.CategoryBookRepository.Delete(categoryBook.Id);
            return Json(new { success = true, message = "Xóa thành công!" });
        }


        // AUTHOR
        public ActionResult AuthorView()
        {
            var authors = unitOfWork.AuthorRepository.Gets()
                .OrderBy(a => a.Id); // Sắp xếp theo ID
            return PartialView("AuthorView", authors);
        }

        [HttpGet]
        public ActionResult AddAuthorView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAuthorView(Author author)
        {
            if (!ModelState.IsValid)
                return View(author);

            foreach (var item in unitOfWork.AuthorRepository.Gets())
            {
                if(item.Name.CompareTo(author.Name) == 0)
                    return Json(new { success = false, message = "Tác giả đã tồn tại!" });
            }

            author.Id = $"A{unitOfWork.AuthorRepository.Gets().Count() + 1}";
            unitOfWork.AuthorRepository.Insert(author);
            return Json(new { success = true, message = "Thêm thành công!" });
        }

        [HttpGet]
        public ActionResult EditAuthorView(string id)
        {
            var author = unitOfWork.AuthorRepository.GetByID(id);
            InitDropDown(author.Status);
            Session["Save"] = author;
            return View(author);
        }
        [HttpPost]
        public ActionResult EditAuthorView(Author author)
        {
            InitDropDown(author.Status);
            if (!ModelState.IsValid)
                return View(author);

            var saveAuthor = Session["Save"] as Author;
            if (author.BOF == null)
                author.BOF = saveAuthor.BOF;
            foreach (var item in unitOfWork.AuthorRepository.Gets())
            {
                if (item.Name.CompareTo(author.Name) == 0 && item.Name.CompareTo(saveAuthor.Name) != 0)
                    return Json(new { success = false, message = "Tác giả đã tồn tại!" });
            }
            if(author.Name.CompareTo(saveAuthor.Name) == 0 &&
                author.BOF.Value.Date == saveAuthor.BOF.Value.Date &&
                author.Status == saveAuthor.Status)
            {
                ModelState.Clear();
                return Json(new { success = false, message = "Không có gì thay đổi!" });
            }

            unitOfWork.AuthorRepository.Update(author);
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpGet]
        public ActionResult DeleteAuthorView(string id)
        {
            var author = unitOfWork.AuthorRepository.GetByID(id);
            InitDropDown(author.Status);
            return View(author);
        }
        [HttpPost]
        public ActionResult DeleteAuthorView(Author author)
        {
            if (isExist(author))
                return Json(new { success = true, message = "Tác giả này đang được sử dụng!" });
            unitOfWork.AuthorRepository.Delete(author.Id);
            return Json(new { success = true, message = "Xóa thành công!" });
        }

        // PUBLISHER
        public ActionResult PublisherView()
        {
            var publishers = unitOfWork.PublisherRepository.Gets() // Giả sử bạn có repository này
                .OrderBy(p => p.Id); // Sắp xếp theo ID
            return PartialView("PublisherView", publishers);
        }
        [HttpGet]
        public ActionResult AddPublisherView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPublisherView(Publisher publisher)
        {
            if (!ModelState.IsValid)
                return View(publisher);

            foreach (var item in unitOfWork.PublisherRepository.Gets())
            {
                if (item.Name.CompareTo(publisher.Name) == 0)
                    return Json(new { success = false, message = "Nhà xuất bản đã tồn tại!" });
            }

            publisher.Id = $"P{unitOfWork.PublisherRepository.Gets().Count() + 1}";
            unitOfWork.PublisherRepository.Insert(publisher);
            return Json(new { success = true, message = "Thêm thành công!" });
        }

        [HttpGet]
        public ActionResult EditPublisherView(string id)
        {
            var publisher = unitOfWork.PublisherRepository.GetByID(id);
            Session["Save"] = publisher;
            return View(publisher);
        }
        [HttpPost]
        public ActionResult EditPublisherView(Publisher publisher)
        {
            if (!ModelState.IsValid)
                return View(publisher);

            var savePublisher = Session["Save"] as Publisher;

            foreach (var item in unitOfWork.PublisherRepository.Gets())
            {
                if (item.Name.CompareTo(publisher.Name) == 0 && item.Name.CompareTo(savePublisher.Name) != 0)
                    return Json(new { success = false, message = "Nhà xuất bản đã tồn tại!" });
            }
            if (publisher.Name.CompareTo(savePublisher.Name) == 0 &&
                publisher.Phone.CompareTo(savePublisher.Phone) == 0 &&
                publisher.Address.CompareTo(savePublisher.Address) == 0)
            {
                ModelState.Clear();
                return Json(new { success = false, message = "Không có gì thay đổi!" });
            }

            unitOfWork.PublisherRepository.Update(publisher);
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpGet]
        public ActionResult DeletePublisherView(string id)
        {
            return View(unitOfWork.PublisherRepository.GetByID(id));
        }
        [HttpPost]
        public ActionResult DeletePublisherView(Publisher publisher)
        {
            if (isExist(publisher))
                return Json(new { success = true, message = "Nhà xuất bản này đang được sử dụng!" });

            unitOfWork.PublisherRepository.Delete(publisher.Id);
            return Json(new { success = true, message = "Xóa thành công!" });
        }

        // LANGUAGE
        public ActionResult LanguageView()
        {
            var languages = unitOfWork.LanguageRepository.Gets() // Giả sử bạn có repository này
                .OrderBy(l => l.Id); // Sắp xếp theo ID
            return PartialView("LanguageView", languages);
        }
        [HttpGet]
        public ActionResult AddLanguageView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddLanguageView(Language language)
        {
            if (language.Name == null)
                return Json(new { success = false, message = "Ngôn ngữ không được để trống!" });

            foreach (var item in unitOfWork.LanguageRepository.Gets())
            {
                if (item.Name.CompareTo(language.Name) == 0)
                    return Json(new { success = false, message = "Ngôn ngữ đã tồn tại!" });
            }

            language.Id = $"L{unitOfWork.LanguageRepository.Gets().Count() + 1}";
            unitOfWork.LanguageRepository.Insert(language);
            return Json(new { success = true, message = "Thêm thành công!" });
        }

        [HttpGet]
        public ActionResult EditLanguageView(string id)
        {
            var language = unitOfWork.LanguageRepository.GetByID(id);
            Session["Save"] = language;
            return View(language);
        }
        [HttpPost]
        public ActionResult EditLanguageView(Language language)
        {
            if (!ModelState.IsValid)
                return View(language);

            var saveLanguage = Session["Save"] as Language;

            foreach (var item in unitOfWork.LanguageRepository.Gets())
            {
                if (item.Name.CompareTo(language.Name) == 0 && item.Name.CompareTo(saveLanguage.Name) != 0)
                    return Json(new { success = false, message = "Ngôn ngữ đã tồn tại!" });
            }
            if (language.Name.CompareTo(saveLanguage.Name) == 0)
            {
                ModelState.Clear();
                return Json(new { success = false, message = "Không có gì thay đổi!" });
            }

            unitOfWork.LanguageRepository.Update(language);
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpGet]
        public ActionResult DeleteLanguageView(string id)
        {
            return View(unitOfWork.LanguageRepository.GetByID(id));
        }
        [HttpPost]
        public ActionResult DeleteLanguageView(Language language)
        {
            if (isExist(language))
                return Json(new { success = true, message = "Ngôn ngữ này đang được sử dụng!" });

            unitOfWork.LanguageRepository.Delete(language.Id);
            return Json(new { success = true, message = "Xóa thành công!" });
        }

        // TRANSLATOR
        public ActionResult TranslatorView()
        {
            var translators = unitOfWork.TranslatorRepository.Gets() // Giả sử bạn có repository này
                .OrderBy(t => t.Id); // Sắp xếp theo ID
            return PartialView("TranslatorView", translators);
        }

        [HttpGet]
        public ActionResult AddTranslatorView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTranslatorView(Translator translator)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Người dịch không được để trống!" });

            foreach (var item in unitOfWork.TranslatorRepository.Gets())
            {
                if (item.Name.CompareTo(translator.Name) == 0)
                    return Json(new { success = false, message = "Người dịch đã tồn tại!" });
            }

            translator.Id = $"T{unitOfWork.TranslatorRepository.Gets().Count() + 1}";
            unitOfWork.TranslatorRepository.Insert(translator);
            return Json(new { success = true, message = "Thêm thành công!" });
        }

        [HttpGet]
        public ActionResult EditTranslatorView(string id)
        {
            var translator = unitOfWork.TranslatorRepository.GetByID(id);
            InitDropDown(translator.Status);
            Session["Save"] = translator;
            return View(translator);
        }
        [HttpPost]
        public ActionResult EditTranslatorView(Translator translator)
        {
            InitDropDown(translator.Status);
            if (!ModelState.IsValid)
                return View(translator);

            var saveTranslator = Session["Save"] as Translator;
            if (translator.BOF == null)
                translator.BOF = saveTranslator.BOF;
            foreach (var item in unitOfWork.TranslatorRepository.Gets())
            {
                if (item.Name.CompareTo(translator.Name) == 0 && item.Name.CompareTo(saveTranslator.Name) != 0)
                    return Json(new { success = false, message = "Người dịch đã tồn tại!" });
            }
            if (translator.Name.CompareTo(saveTranslator.Name) == 0 &&
                translator.BOF.Value.Date == saveTranslator.BOF.Value.Date &&
                translator.Status == saveTranslator.Status)
            {
                ModelState.Clear();
                return Json(new { success = false, message = "Không có gì thay đổi!" });
            }

            unitOfWork.TranslatorRepository.Update(translator);
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpGet]
        public ActionResult DeleteTranslatorView(string id)
        {
            var translator = unitOfWork.TranslatorRepository.GetByID(id);
            InitDropDown(translator.Status);
            return View(translator);
        }
        [HttpPost]
        public ActionResult DeleteTranslatorView(Translator translator)
        {
            if (isExist(translator))
                return Json(new { success = true, message = "Dịch giả này đang được sử dụng!" });

            unitOfWork.TranslatorRepository.Delete(translator.Id);
            return Json(new { success = true, message = "Xóa thành công!" });
        }

        // BOOKTITLE
        public ActionResult BookTitleView()
        {
            var bookTitles = unitOfWork.BookTitleRepository.Gets() // Giả sử bạn có repository này
                .OrderBy(b => b.Id); // Sắp xếp theo ID
            return PartialView("BookTitleView", bookTitles);
        }
        [HttpGet]
        public ActionResult AddBookTitleView()
        {
            InitDropDown();
            return View();
        }
        [HttpPost]
        public JsonResult AddBookTitleView(BookTitle bookTitle, HttpPostedFileBase ImageFile)
        {
            InitDropDown();

            switch (SaveImage(bookTitle, ImageFile))
            {
                case 1:
                    return Json(new { success = false, message = "Ảnh đã tồn tại!" });
                case 2:
                    return Json(new { success = false, message = "Vui lòng chọn ảnh hợp lệ!" });
            }

            foreach (var item in unitOfWork.BookTitleRepository.Gets())
            {
                if (item.Name.CompareTo(bookTitle.Name) == 0)
                    return Json(new { success = false, message = "Tiêu đề sách đã tồn tại!" });
            }

            bookTitle.Id = $"BT{unitOfWork.BookTitleRepository.Gets().Count() + 1}";
            unitOfWork.BookTitleRepository.Insert(bookTitle);
            return Json(new { success = true, message = "Thêm thành công!" });
        }

        [HttpGet]
        public ActionResult EditBookTitleView(string id)
        {
            InitDropDown();
            var bookTitle = unitOfWork.BookTitleRepository.GetByID(id);
            Session["Save"] = bookTitle;
            return View(bookTitle);
        }
        [HttpPost]
        public ActionResult EditBookTitleView(BookTitle bookTitle, HttpPostedFileBase ImageFile)
        {
            InitDropDown();
            if (!ModelState.IsValid)
                return View(bookTitle);

            var saveBookTitle = Session["Save"] as BookTitle;

            foreach (var item in unitOfWork.BookTitleRepository.Gets())
            {
                if (item.Name.CompareTo(bookTitle.Name) == 0 && item.Name.CompareTo(saveBookTitle.Name) != 0)
                    return Json(new { success = false, message = "Tiêu đề sách đã tồn tại!" });
            }

            if(ImageFile != null)
            {
                switch (SaveImage(bookTitle, ImageFile))
                {
                    case 1:
                        return Json(new { success = false, message = "Ảnh đã tồn tại!" });
                    case 2:
                        return Json(new { success = false, message = "Vui lòng chọn ảnh hợp lệ!" });
                }
            }

            if (bookTitle.Name.CompareTo(saveBookTitle.Name) == 0 &&
                bookTitle.UrlImage.CompareTo(saveBookTitle.UrlImage) == 0 &&
                bookTitle.CategoryBookID.CompareTo(saveBookTitle.CategoryBookID) == 0 &&
                bookTitle.Summary.CompareTo(saveBookTitle.Summary) == 0)
            {
                ModelState.Clear();
                return Json(new { success = false, message = "Không có gì thay đổi!" });
            }

            unitOfWork.BookTitleRepository.Update(bookTitle);
            CleanupUnusedImages();
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpGet]
        public ActionResult DeleteBookTitleView(string id)
        {
            InitDropDown();
            var boolTitle = unitOfWork.BookTitleRepository.GetByID(id);
            return View(boolTitle);
        }
        [HttpPost]
        public ActionResult DeleteBookTitleView(BookTitle bookTitle)
        {
            InitDropDown();
            if (isExist(bookTitle))
                return Json(new { success = true, message = "Tiêu đề này đang được sử dụng!" });

            unitOfWork.BookTitleRepository.Delete(bookTitle.Id);
            CleanupUnusedImages();
            return Json(new { success = true, message = "Xóa thành công!" });
        }

        // BOOKISBN
        public ActionResult BookISBNView()
        {
            var bookISBNs = unitOfWork.BookISBNRepository.Gets() // Giả sử bạn có repository này
                .OrderBy(b => b.ISBN); // Sắp xếp theo ISBN
            return PartialView("BookISBNView", bookISBNs);
        }
        [HttpGet]
        public ActionResult AddBookISBNView()
        {
            InitDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult AddBookISBNView(BookISBN bookISBN)
        {
            InitDropDown();
            if (!ModelState.IsValid)
                return View(bookISBN);

            foreach (var item in unitOfWork.BookISBNRepository.Gets())
            {
                if (item.BookTitleID.CompareTo(bookISBN.BookTitleID) == 0 &&
                    item.AuthorID.CompareTo(bookISBN.AuthorID) == 0 &&
                    item.LanguageID.CompareTo(bookISBN.LanguageID) == 0)
                    return Json(new { success = false, message = "Đầu sách đã tồn tại!" });
            }

            bookISBN.ISBN = $"ISBN{unitOfWork.BookISBNRepository.Gets().Count() + 1}";
            unitOfWork.BookISBNRepository.Insert(bookISBN);
            return Json(new { success = true, message = "Thêm thành công!" });
        }

        [HttpGet]
        public ActionResult EditBookISBNView(string isbn)
        {
            var bookISBN = unitOfWork.BookISBNRepository.GetByID(isbn);
            Session["Save"] = bookISBN;
            InitDropDown(bookISBN);
            InitDropDown(bookISBN.Status);
            return View(bookISBN);
        }
        [HttpPost]
        public ActionResult EditBookISBNView(BookISBN bookISBN)
        {
            InitDropDown(bookISBN);
            InitDropDown(bookISBN.Status);
            if (!ModelState.IsValid)
                return View(bookISBN);

            var saveBookISBN = Session["Save"] as BookISBN;

            foreach (var item in unitOfWork.BookISBNRepository.Gets())
            {
                if (item.BookTitleID.CompareTo(bookISBN.BookTitleID) == 0 &&
                    item.AuthorID.CompareTo(bookISBN.AuthorID) == 0 &&
                    item.LanguageID.CompareTo(bookISBN.LanguageID) == 0)
                    return Json(new { success = false, message = "Đầu sách đã tồn tại!" });
            }

            if (bookISBN.BookTitleID.CompareTo(saveBookISBN.BookTitleID) == 0 &&
                bookISBN.AuthorID.CompareTo(saveBookISBN.AuthorID) == 0 &&
                bookISBN.LanguageID.CompareTo(saveBookISBN.LanguageID) == 0 &&
                bookISBN.Status.CompareTo(saveBookISBN.Status) == 0)
            {
                ModelState.Clear();
                return Json(new { success = false, message = "Không có gì thay đổi!" });
            }

            unitOfWork.BookISBNRepository.Update(bookISBN);
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpGet]
        public ActionResult DeleteBookISBNView(string isbn)
        {
            var bookISBN = unitOfWork.BookISBNRepository.GetByID(isbn);
            Session["Save"] = bookISBN;
            InitDropDown(bookISBN);
            InitDropDown(bookISBN.Status);
            return View(bookISBN);
        }
        [HttpPost]
        public ActionResult DeleteBookISBNView(BookISBN item)
        {
            if (isExist(item))
                return Json(new { success = true, message = "Đầu sách này đang được sử dụng!" });

            var bookISBN = Session["Save"] as BookISBN;
            unitOfWork.BookISBNRepository.Delete(bookISBN.ISBN);
            return Json(new { success = true, message = "Xóa thành công!" });
        }


        // BOOK
        public ActionResult BookView(int? page)
        {
            int pageSize = 12; // Hiển thị 12 sách mỗi trang
            int pageNumber = (page ?? 1);

            var books = unitOfWork.BookRepository.Gets()
                .OrderBy(b => b.Id)
                .ToPagedList(pageNumber, pageSize);

            return PartialView("BookView", books);
        }

        [HttpGet]
        public ActionResult AddBookView()
        {
            InitDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult AddBookView(Book book)
        {
            InitDropDown();
            if (!ModelState.IsValid)
                return View(book);

            unitOfWork.BookRepository.Insert(book);
            return Json(new { success = true, message = "Thêm thành công!" });
        }

        [HttpGet]
        public ActionResult EditBookView(int id)
        {
            var book = unitOfWork.BookRepository.GetByID(id);
            Session["Save"] = book.Price;
            InitDropDown();
            InitDropDown(book.Status);
            ModelState.Clear();
            return View(book);
        }
        [HttpPost]
        public ActionResult EditBookView(Book book)
        {
            InitDropDown();
            InitDropDown(book.Status);
            if (!ModelState.IsValid)
                return View(Session["Save"] as Book);

            var saveBook = Session["Save"] as Book;


            if (book.ISBN.CompareTo(saveBook.ISBN) == 0 &&
                book.PublisherID.CompareTo(saveBook.PublisherID) == 0 &&
                book.TranslatorID.CompareTo(saveBook.TranslatorID) == 0 &&
                book.LanguageID.CompareTo(saveBook.LanguageID) == 0 &&
                book.PublishDate == saveBook.PublishDate &&
                book.Price == saveBook.Price &&
                book.Status.CompareTo(saveBook.Status) == 0)
            {
                ModelState.Clear();
                return Json(new { success = false, message = "Không có gì thay đổi!" });
            }
            unitOfWork.BookRepository.Update(book);
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpGet]
        public ActionResult DeleteBookView(int id)
        {
            var book = unitOfWork.BookRepository.GetByID(id);
            InitDropDown(book.Status);
            return View(book);
        }
        [HttpPost]
        public JsonResult DeleteBookView(Book item)
        {
            if (isExist(item))
                return Json(new { success = true, message = "Sách này đang được sử dụng!" });

            unitOfWork.BookRepository.Delete(item.Id);
            return Json(new { success = true, message = "Xóa sách thành công!" });
        }

        private void InitDropDown(bool Status)
        {
            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Text="Kích hoạt", Value="true", Selected = Status == true },
                new SelectListItem { Text="Khóa", Value ="false", Selected = Status == false },
            };
        }

        private void InitDropDown()
        {
            ViewBag.BookISBNList = new List<SelectListItem>();
            foreach (var item in unitOfWork.BookISBNRepository.Gets())
                (ViewBag.BookISBNList as List<SelectListItem>).Add(new SelectListItem { Text = $"Tên Sách: {item.BookTitle.Name} - Tác giả: {item.Author.Name} - Ngôn ngữ: {item.Language.Name}", Value = item.ISBN });

            ViewBag.TranslatorList = new List<SelectListItem>();
            foreach (var item in unitOfWork.TranslatorRepository.Gets())
                (ViewBag.TranslatorList as List<SelectListItem>).Add(new SelectListItem { Text = $"Tên: {item.Name} - Ngày sinh: {item.BOF.Value.ToString("dd/mm/yyyy")}", Value = item.Id });

            ViewBag.BookTitleList = new List<SelectListItem>();
            foreach (var item in unitOfWork.BookTitleRepository.Gets())
                (ViewBag.BookTitleList as List<SelectListItem>).Add(new SelectListItem { Text = item.Name, Value = item.Id });

            ViewBag.CategoryBookList = new List<SelectListItem>();
            foreach (var item in unitOfWork.CategoryBookRepository.Gets())
                (ViewBag.CategoryBookList as List<SelectListItem>).Add(new SelectListItem { Text = item.Name, Value = item.Id });

            ViewBag.LanguageList = new List<SelectListItem>();
            foreach (var item in unitOfWork.LanguageRepository.Gets())
                (ViewBag.LanguageList as List<SelectListItem>).Add(new SelectListItem { Text = item.Name, Value = item.Id });

            ViewBag.AuthorList = new List<SelectListItem>();
            foreach (var item in unitOfWork.AuthorRepository.Gets())
                (ViewBag.AuthorList as List<SelectListItem>).Add(new SelectListItem { Text = item.Name, Value = item.Id });

            ViewBag.PublisherList = new List<SelectListItem>();
            foreach (var item in unitOfWork.PublisherRepository.Gets())
                (ViewBag.PublisherList as List<SelectListItem>).Add(new SelectListItem { Text = item.Name, Value = item.Id });
        }

        private void InitDropDown(BookISBN bookISBN)
        {            
            ViewBag.BookTitleList = unitOfWork.BookTitleRepository.Gets()
               .Select(bookTitle => new SelectListItem
               {
                   Value = bookTitle.Id,
                   Text = bookTitle.Name,
                   Selected = (bookTitle.Id.CompareTo(bookISBN.BookTitleID) == 0)
               });


            ViewBag.AuthorList = unitOfWork.AuthorRepository.Gets()
              .Select(author => new SelectListItem
              {
                  Value = author.Id.ToString(),
                  Text = author.Name,
                  Selected = (author.Id.CompareTo(bookISBN.AuthorID) == 0)
              });

            ViewBag.LanguageList = unitOfWork.LanguageRepository.Gets()
              .Select(language => new SelectListItem
              {
                  Value = language.Id.ToString(),
                  Text = language.Name,
                  Selected = (language.Id.CompareTo(bookISBN.LanguageID) == 0)
              });
        }

        private int SaveImage(BookTitle bookTitle, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string xPath = Server.MapPath("~/Content/Images/");
                if (!Directory.Exists(xPath))
                    Directory.CreateDirectory(xPath);

                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(xPath, fileName);

                if (IsImageExists(filePath))
                    return 1;

                ImageFile.SaveAs(filePath);
                bookTitle.UrlImage = "~/Content/Images/" + fileName;
                return 0;
            }
            return 2;
        }

        private bool IsImageExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        private void CleanupUnusedImages()
        {
            string imageDirectory = HostingEnvironment.MapPath("~/Content/Images/");
            if (string.IsNullOrEmpty(imageDirectory) || !Directory.Exists(imageDirectory))
                return;

            var files = Directory.GetFiles(imageDirectory);
            var usedImages = unitOfWork.BookTitleRepository
                .Gets()
                .Select(bookTitle => bookTitle.UrlImage)
                .Where(url => !string.IsNullOrEmpty(url))
                .Select(url => Path.GetFileName(url))
                .ToHashSet();

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (!usedImages.Contains(fileName))
                {
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch { }
                }
            }
        }

        private bool isExist(CategoryBook categoryBook)
        {
            foreach (var item in unitOfWork.BookTitleRepository.Gets())
                if (item.CategoryBookID.CompareTo(categoryBook.Id) == 0)
                    return true;
            return false;
        }

        private bool isExist(Author author)
        {
            foreach (var item in unitOfWork.BookISBNRepository.Gets())
                if (item.AuthorID.CompareTo(author.Id) == 0)
                    return true;
            return false;
        }

        private bool isExist(BookISBN bookISBN)
        {
            foreach (var item in unitOfWork.BookRepository.Gets())
                if (item.ISBN.CompareTo(bookISBN.ISBN) == 0)
                    return true;
            return false;
        }

        private bool isExist(BookTitle bookTitle)
        {
            foreach (var item in unitOfWork.BookISBNRepository.Gets())
                if (item.BookTitleID.CompareTo(bookTitle.Id) == 0)
                    return true;
            return false;
        }

        private bool isExist(Book book)
        {
            foreach (var item in unitOfWork.LoanSLipDetailsRepository.Gets())
                if (item.BookID.CompareTo(book.Id) == 0)
                    return true;
            return false;
        }

        private bool isExist(Language language)
        {
            foreach (var item in unitOfWork.BookISBNRepository.Gets())
                if (item.LanguageID.CompareTo(language.Id) == 0)
                    return true;
            foreach (var item in unitOfWork.BookRepository.Gets())
                if (item.LanguageID.CompareTo(language.Id) == 0)
                    return true;
            return false;
        }

        private bool isExist(Publisher publisher)
        {
            foreach (var item in unitOfWork.BookRepository.Gets())
                if (item.PublisherID.CompareTo(publisher.Id) == 0)
                    return true;
            return false;
        }

        private bool isExist(Translator translator)
        {
            foreach (var item in unitOfWork.BookRepository.Gets())
                if (item.TranslatorID.CompareTo(translator.Id) == 0)
                    return true;
            return false;
        }
    }
}

