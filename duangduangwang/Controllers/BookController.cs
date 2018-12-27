using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using duangduangwang.Models;
using duangduangwang.Models.Mapper;
using PagedList;

namespace duangduangwang.Controllers
{
    public class BookController : Controller
    {

        DataClasses1DataContext db = new DataClasses1DataContext();
        private BookMapper mapper= new BookMapper();

        //book search
        public ActionResult Search(string book_name , int? page)
        {
            var books = mapper.Search(book_name);

            //page list
            int pageNumber = page ?? 1;
            int pageSize = 6;
            //books = books.OrderBy(x => x.BookId);
            IPagedList<Book> pagedList = books.ToPagedList(pageNumber, pageSize);

            HashSet<string> tags = mapper.GetTagsOfBook(books);
            ViewBag.books = pagedList;
            ViewBag.tags = tags;

            return View("SearchResult",pagedList);
        }

        //search book by Coupon
        public ActionResult SearchByCoupon(int Coupon, int? page)
        {
            var books = mapper.SearchByCoupon(Coupon);
            int pageNumber = page ?? 1;
            int pageSize = 6;
            IPagedList<Book> pagedList = books.ToPagedList(pageNumber, pageSize);
            HashSet<string> tags = mapper.GetTagsOfBook(books);
            ViewBag.books = pagedList;
            ViewBag.tags = tags;
          
            return View("SearchResult",pagedList);
        }
            // book details
            public ActionResult Details(int id)
        {

            IList<Book> books = mapper.Details(id);
            HashSet<string> tags = mapper.GetTagsOfBook(books);
            ViewBag.book = books;
            ViewBag.tags = tags;
            return View("BookDetail");
        }
        //books of tag
        public ActionResult GetBooksOfTag(string tag)
        {
            IList<Book> books = mapper.GetBooksOfTag(tag);
            HashSet<string> tags = new HashSet<string>();
            foreach (Book bk in books)
            {
                string[] tagOfBook = bk.Tag.Split(',');
                foreach (var t in tagOfBook)
                {
                    tags.Add(t);
                }
            }
            ViewBag.books = books;
            ViewBag.tags = tags;
            return View("SearchResult");
        }

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        
    }
}
