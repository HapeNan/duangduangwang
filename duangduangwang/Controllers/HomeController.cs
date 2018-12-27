using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using duangduangwang.Models.Mapper;
using duangduangwang.Models;
using System.Globalization;

namespace duangduangwang.Controllers
{
    public class HomeController : Controller
    {
        private BookMapper bookMapper = new BookMapper();
        private OrderMapper orderMapper = new OrderMapper();

        //跳转到主页
        public ActionResult Index()
        {
            ViewBag.OfferBookList = bookMapper.OfferBookList();
            ViewBag.NewBookList = bookMapper.NewBookList();
            return View();
        }

        //跳转到管理员界面
        public ActionResult Manager()
        {
            return Redirect("/Home/ManageOrder");
        }

        //跳转到订单管理页面/显示全部订单
        public ActionResult ManageOrder()
        {
             ViewBag.OrderList=orderMapper.ListAllOrders();
            return View();
        }

        //查询订单
        public ActionResult SearchOrders()
        {
            string orderId = Request["orderId"];  //如果为空则id为""
            string userId = Request["userId"]; 
            string status = Request["status"];
            string createDate = Request["createDate"];
            string orderItemBookId = Request["orderItemBookId"];
            string orderItemBookName = Request["orderItemBookName"];
            string[] query = new string[] { orderId, userId, status, createDate, orderItemBookId, orderItemBookName }; 
            ViewBag.OrderList = orderMapper.SearchOrders(query);

            return View("ManageOrder");

        }

        //查看某个订单详情
        public ActionResult OrderDetail(int OrderId)
        {
            ViewBag.OrderItemList = orderMapper.ListAllOrderItems(OrderId);
            ViewBag.Order = orderMapper.getOrderDetail(OrderId);
           
            return View("ManageOrder");
        }

        //跳转到用户界面/显示所有用户
        public ActionResult ManageUser()
        {
            return View();
        }

        //跳转到图书管理界面/显示所有图书
        public ActionResult ManageBook()
        {
            ViewBag.BookList = bookMapper.ListAllBooks();
            return View();
        }
        //查询图书
        public ActionResult SearchBooks()
        {
            string bookId = Request["BookId"];  //如果为空则id为""
            string bookName = Request["BookName"];
            string bookPublisher = Request["BookPublisher"];
            string bookType = Request["BookType"];
            string bookWriter = Request["BookWriter"];
            string publishTime = Request["PublishTime"];
            string coupon = Request["Coupon"];
            string Tag = Request["Tag"];
            if (publishTime != "")
            {
                DateTime dt;
                DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy-MM-dd";
                dt = Convert.ToDateTime(publishTime, dtFormat);
                publishTime = dt.Year + "/" + dt.Month + "/" + dt.Day;
            }
            string[] query = new string[] { bookId, bookName, bookPublisher, bookType, bookWriter,publishTime,coupon,Tag };
            ViewBag.BookList = bookMapper.SearchBooks(query);
            return View("ManageBook");
            //return Content(publishTime);
        }
        //查看图书详情
        public ActionResult BookDetail(int BookId)
        {
            ViewBag.BookDetail = bookMapper.GetBookById(BookId).ElementAt<Book>(0);
            DateTime dt = ViewBag.BookDetail.PublishTime;
            //return Content(dt.ToString());
            return View("ManageBook");
  
        }
        //修改图书信息
        public ActionResult UpdateBook()
        {
            string bookId = Request["BookId"];  //如果为空则id为""
            string bookName = Request["BookName"];
            string bookAbstract = Request["BookAbstract"];
            string bookWriter = Request["BookWriter"];
            string bookPublisher = Request["BookPublisher"];
            string publishTime = Request["PublishTime"];
            DateTime dt;
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy-MM-dd";
            dt = Convert.ToDateTime(publishTime, dtFormat);
            publishTime = dt.Year + "/" + dt.Month + "/" + dt.Day;
            string Picture1 = Request["Picture1"] == "" ? Request["originPicture1"] : "/Images/" + Request["Picture1"];
            string Picture2 = Request["Picture2"] == "" ? Request["originPicture2"] : "/Images/" + Request["Picture2"];
            string Picture3 = Request["Picture3"] == "" ? Request["originPicture3"] : "/Images/" + Request["Picture3"];
            string bookType = Request["BookType"];
            string coupon = Request["Coupon"];
            string price = Request["BookPrice"];
            string CouponDetail = Request["CouponDetail"];
            string Tag = Request["Tag"];
            string[] query = new string[] { bookId, bookName,bookAbstract,bookWriter,
                bookPublisher,publishTime,Picture1,Picture2,Picture3,bookType,coupon,price,CouponDetail,Tag};

            bookMapper.UpdateBook(query);

            return View("ManageBook");

        }
        public ActionResult ManageAddBook()
        {
            return View();
        }
        public ActionResult ManageBulkEditing()
        {
            return View();
        }
    }
}