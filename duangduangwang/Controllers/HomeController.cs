﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using duangduangwang.Models.Mapper;
namespace duangduangwang.Controllers
{
    public class HomeController : Controller
    {
        private BookMapper bookMapper = new BookMapper();
        public ActionResult Index()
        {
            ViewBag.OfferBookList = bookMapper.OfferBookList();
            ViewBag.NewBookList = bookMapper.NewBookList();
            return View();
        }
        public ActionResult Manager()
        {
            return View("ManageOrder");
        }
        public ActionResult ManageOrder()
        {
            return View();
        }
        public ActionResult ManageUser()
        {
            return View();
        }
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