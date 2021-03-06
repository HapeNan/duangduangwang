﻿using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using duangduangwang.Models;
using duangduangwang.Models.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace duangduangwang.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay
        OrderMapper orderMapper = new OrderMapper();
        public ActionResult ConfirmOrder()
        {
            if (Session["userName"] == null)
            {
                Response.Redirect("/User/LoginPage");
            }
            double discountPrice = 0;
            double discountPrice2 = 0;
            double totalprice = 0;
            double total = 0;
            if (Session["Cart"] != null)
            {
                List<Book> BookList = (List<Book>)Session["Cart"];
                List<Book> Coupon2List = new List<Book>();
                foreach (Book item in BookList)
                {
                   
                    string fg = Session[item.BookId.ToString() + "select"].ToString();
                    int number = (int)Session[item.BookId.ToString()];
                    if (fg == "true")
                    {
                        totalprice +=(double) item.BookPrice * number;
                        if (item.Coupon == 1)
                        {
                            discountPrice += (double)item.BookPrice * number * int.Parse(item.CouponDetail) / 10;
                        }
                        else if (item.Coupon == 2)
                        {
                            Coupon2List.Add(item);
                        }
                    }

                }
              
                string[] i= {""};
                foreach (Book item in Coupon2List)
                {
                    int number = (int)Session[item.BookId.ToString()];
                    i = item.CouponDetail.Split(',');

                    total +=(double) item.BookPrice*number;
                }
                if(Coupon2List.Count!=0)
                if (total > int.Parse(i[0]))
                {
                    total -= int.Parse(i[1]);
                    discountPrice2= int.Parse(i[1]);
                }
            }

            //Session["userId"]=1;
            ViewBag.finalTotalPrice = discountPrice + total;
            ViewBag.discountPrice = totalprice - discountPrice - total;
            return View();
        }
        // coupon orderId(流水账号?)
        public void SubmitOrder()
        {
           
            int num = 0;
            string finalTotalPrice= (string)Request.Form["finalTotalPrice"];
            BookOrder bookOrder = new BookOrder();
            bookOrder.UserId = (int)Session["userId"];
            bookOrder.TotalPrice = Double.Parse(finalTotalPrice);
            bookOrder.Status = 0;
            bookOrder.createDate =System.DateTime.Now;
            int orderId=orderMapper.addBookOrder(bookOrder);
             if (Session["Cart"] != null)
            {
                List<Book> BookList = (List<Book>)Session["Cart"];
                List<Book> newBookList = new List<Book>();//CartRemain
                List<Book> cartItemList = new List<Book>();//CartSelected
                foreach (Book item in BookList)
                {
                    string fg = Session[item.BookId.ToString() + "select"].ToString();
                    if (fg == "true")
                    {
                        Models.OrderItem orderItem = new Models.OrderItem();
                        orderItem.OrderId = orderId;
                        orderItem.BookId = item.BookId;
                        orderItem.quantity = (int)Session[orderItem.BookId.ToString()];
                        orderMapper.addOrderItem(orderItem);
                        Session[item.BookId.ToString() + "select"] = null;
                        cartItemList.Add(item);
                    }
                    else
                    {
                        num += 1;
                        newBookList.Add(item);
                    }
                }
                Session["num"] = num;
                Session["cartItemList"] = cartItemList;
                Session["Cart"] = newBookList;
            }




            DefaultAopClient client = new DefaultAopClient(config.gatewayUrl, config.app_id, config.private_key, "json", "1.0", config.sign_type, config.alipay_public_key, config.charset, false);
        
            // 外部订单号，商户网站订单系统中唯一的订单号!!!!不能重复
            string out_trade_no = orderId.ToString()+"-"+DateTime.Now.ToString("yyyyMMddHHmmss");
            // 订单名称
            string subject = "DuangDuangBook";
            // 付款金额
            string total_amout = finalTotalPrice;
            // 商品描述
            string body = "";


            // 组装业务参数model
            AlipayTradePagePayModel model = new AlipayTradePagePayModel();
            model.Body = body;
            model.Subject = subject;
            model.TotalAmount = total_amout;
            model.OutTradeNo = out_trade_no;
            model.ProductCode = "FAST_INSTANT_TRADE_PAY";

            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();
            // 设置同步回调地址
            request.SetReturnUrl("http://localhost:51372/Pay/ReturnUrl");
            // 设置异步通知接收地址
            request.SetNotifyUrl("");
            // 将业务model载入到request
            request.SetBizModel(model);

            AlipayTradePagePayResponse response = null;
            try
            {
                response = client.pageExecute(request, null, "post");
                Response.Write(response.Body);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public ActionResult ReturnUrl()
        {
            return View();
        }
        public ActionResult ReturnOrder()
        {
            return Redirect("~/Home/Index");
        }
    }
}