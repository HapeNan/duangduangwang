using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using duangduangwang.Models.Mapper;
using duangduangwang.Models;
namespace duangduangwang.Controllers
{
    public class UserController : Controller
    {
        UserMapper userMapper = new UserMapper();
        OrderMapper orderMapper = new OrderMapper();
        // GET: User
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult UserPage()
        {
            if (Session["userId"] == null)
                Response.Redirect("/User/LoginPage");
            string userId = Session["userId"].ToString();
            ViewBag.OrderList = orderMapper.SearchOrdersByUserId(userId);
            return View();
        }
        public ActionResult LoginPage()
        {
            if (Session["userName"] == null || Session["userName"].ToString() == "")
            {
                return View();
            }
            return Redirect("/Home/Index");
        }

        public ActionResult Logout()
        {
            Session.Remove("userName");
            Session.Remove("userId");
            return Redirect("/Home/Index");
        }

        public ActionResult RegisterPage(FormCollection collection)
        {
            string isRegister = Request["checkout_register"];
            isRegister = collection["checkout_register"];
            if (isRegister == null)
            {
                return View();
            }
            if (isRegister.Equals("register"))  //注册
            {
                return View();
            }
            else {           //游客身份继续浏览
                return Redirect("/Home/Index");
            }
        }

        public ActionResult Register()  //注册
        {
            List<Customer> templist = userMapper.Alllist();
            for (int i = 0; i < templist.Count; i++)
            {
                if (Request["username"] == templist.ElementAt(i).UserName)
                {   //用户名已被使用
                    TempData["wrongMessage"] = "用户名已被使用";
                    return Redirect("/User/RegisterPage");
                }
            }
            Customer customer = new Customer();
            customer.UserName = Request["username"];
            customer.Password = Request["password"];
            customer.Address = Request["address"];
            customer.PhoneNumber = Request["phonenumber"];
            customer.Receiver = Request["receiver"];
            customer.Member = "false";
            userMapper.Insert(customer);
            TempData["message"] = "注册成功,请登录";
            //return Content(Request["username"] + " " + Request["password"] +" "+ Request["address"] + " "+Request["phonenumber"] +" "+ Request["receiver"] );
            return View("LoginPage");
        }
        public ActionResult Login(string username,string password)
        {
            List<Customer> templist = userMapper.Alllist();
            for (int i = 0; i < templist.Count; i++)
            {
                if (username == templist.ElementAt(i).UserName && password == templist.ElementAt(i).Password)
                {   //验证通过
                    Session["userName"] = username;
                    Session["userId"] = templist.ElementAt(i).UserId;
                    return Redirect("/Home/Index");
                }
            }
            //登录失败，重新登录，提示信息
            TempData["message"] = "用户不存在或密码错误";
            return View("LoginPage");
        }
        public ActionResult UserOrderDetail(string OrderId)
        {
            ViewBag.OrderDetail = orderMapper.getOrderDetail(int.Parse(OrderId)).ElementAt<BookOrder>(0);
            ViewBag.OrderItemList = orderMapper.ListAllOrderItems(int.Parse(OrderId));
            ViewBag.OrderList = orderMapper.SearchOrdersByUserId(Session["userId"].ToString());
            return View("UserPage");
        }

    }
}