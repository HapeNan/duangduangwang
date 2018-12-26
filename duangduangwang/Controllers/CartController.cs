using duangduangwang.Models;
using duangduangwang.Models.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace duangduangwang.Controllers
{
    public class CartController : Controller
    {
        public ActionResult View1()
        {
            return View();
        }
        public ActionResult ToCart()
        {
            List<Book> BookList = (List<Book>)Session["Cart"];
            return View("Cart",BookList);
        }
        // GET: Cart
        DataClasses1DataContext db = new DataClasses1DataContext();
        BookMapper bookmapper = new BookMapper();
        public ActionResult Cart(int Id, int qty)
        {

            var resList = bookmapper.GetBookById(Id);
            var res = resList[0];
            var BookId = res.BookId.ToString();
            if (Session["Cart"] != null)
            {
                List<Book> BookList = (List<Book>)Session["Cart"];
                Session[BookId] = qty;
                foreach (Book item in BookList)
                {
                    if (item.BookId == Id)
                        return View(BookList);
                }
                BookList.Add(res);
                Session["Cart"] = BookList;
                if (Session["num"] == null)
                {
                    Session["num"] = 1;
                }
                else
                {
                    Session["num"] = (int)Session["num"] + 1;
                }
                return View(BookList);
            }
            else
            {
                if (Session["num"] == null)
                {
                    Session["num"] = 1;
                }
                else
                {
                    Session["num"] = (int)Session["num"] + 1;
                }
                List<Book> BookList = new List<Book>();
                Session[BookId] = qty;
                BookList.Add(res);
                Session["Cart"] = BookList;
                return View(BookList);
            }
        }

        public ActionResult AjaxTest(int Id, int qty)
        {
            if (qty != 0)
            {
                Session[Id.ToString()] = qty;
            }
            else
            {
                Session[Id.ToString()] = 0;
                Session["num"] = (int)Session["num"] - 1;
                List<Book> BookList = (List<Book>)Session["Cart"];
                foreach (Book item in BookList)
                {
                    if (item.BookId == Id)
                    {
                        BookList.Remove(item);
                        break;
                    }
                }

            }
            return new EmptyResult();
        }
        public ActionResult Changetot(int tot)
        {
            if (Session["sum"] == null)
            {
                Session["sum"] = tot;
            }
            else
            {
                Session["sum"] = tot;
            }
            return new EmptyResult();
        }

        public ActionResult Select(int Id, String selected)
        {

            if (selected == "true")
            {
                Session[Id.ToString() + "select"] = "true";
            }
            else
            {
                Session[Id.ToString() + "select"] = "false";
            }
            return new EmptyResult();
        }
        public ActionResult ConfirmOrder()
        {
            return Redirect("~/Pay/ConfirmOrder");
        }
    }
}