using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using duangduangwang.Models;

namespace duangduangwang.Controllers
{
    public class BookController : Controller
    {

        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult Search(string book_name,int page=1)
        {
      
            var books = from b in db.Book
                        select b;
            
            if(!string.IsNullOrEmpty(book_name))
            {
                books = books.Where(s => s.BookName.Contains(book_name));
            }
            
            //分页
            //int pageCount = (int)book.LongCount();
            //var books = book.ToPagedList(page, pageCount);
            //分页

            HashSet<string> tags = new HashSet<string>();
           foreach(Book bk in books)
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

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = from b in db.Book
                       where b.BookId == id
                       select b;
            ViewBag.book = book;
            return View("BookDetail");
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
