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
        public ActionResult QueryOrder()
        {

            return View("ManageOrder");
        }

        //查看某个订单详情
        public ActionResult OrderDetail(int OrderId)
        {
            ViewBag.OrderItemList = orderMapper.ListAllOrderItems(OrderId);
            ViewBag.Order = orderMapper.getOrderDetail(OrderId);
            BookOrder orderDetail=new BookOrder();
            if (ViewBag.Order.Count!=0)
            orderDetail = orderMapper.getOrderDetail(OrderId).ElementAt(0);
            DateTime date =(DateTime) orderDetail.createDate;
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
            return View();
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