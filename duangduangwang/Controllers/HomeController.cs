using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using duangduangwang.Models.Mapper;
using duangduangwang.Models;
namespace duangduangwang.Controllers
{
    public class HomeController : Controller
    {
        private BookMapper bookMapper = new BookMapper();
        private OrderMapper orderMapper = new OrderMapper();
        private UserMapper userMapper = new UserMapper();

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

        //跳转到用户管理页面/显示全部用户信息
        public ActionResult ManageUser()
        {
            ViewBag.UserList = userMapper.ListAllUsers();
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

        //查询用户
        public ActionResult SearchUsers()
        {
            string userId = Request["userId"];  //如果为空则id为""
            string username = Request["userName"];
            string member = Request["member"];
            string phonenumber = Request["phoneNumber"];
            string[] query = new string[] { userId, username, member, phonenumber };
            ViewBag.UserList = userMapper.SearchUsers(query);
            return View("ManageUser");
        }

        //查看某个订单详情
        public ActionResult OrderDetail(int OrderId)
        {
            ViewBag.OrderItemList = orderMapper.ListAllOrderItems(OrderId);
            ViewBag.Order = orderMapper.getOrderDetail(OrderId);
           
            return View("ManageOrder");
        }

        //跳转到图书管理界面/显示所有图书
        public ActionResult ManageBook()
        {
            return View();
        }
        //管理员添加书籍
        public ActionResult ManageAddBook()
        {
            Book book=new Book();
            book.BookName = Request["BookName"];
            book.BookAbstract = Request["BookAbstract"];
            book.BookWriter = Request["BookWriter"];
            book.BookPublisher = Request["BookPublisher"];
            book.PublishTime = Convert.ToDateTime(Request["PublishTime"]);
            book.BookPrice = Convert.ToDouble(Request["BookPrice"]);
            book.Picture1 = Request["Picture1"];
            book.Picture2 = Request["Picture2"];
            book.Picture3 = Request["Picture3"];
            book.BookType = Request["BookType"];
            book.Tag = Request["Tag"];
            book.Coupon = Convert.ToInt32(Request["Coupon"]);
            book.CouponDetail = Request["CouponDetail"];
            bookMapper.AddBook(book);
            return View();
        }
        public ActionResult ManageAddBookInit()
        {

            return View();
        }

        public ActionResult ManageBulkEditing()
        {
            return View();
        }
    }
}