/**
* CRL 快速开发框架 V4.5
* Copyright (c) 2016 Hubro All rights reserved.
* GitHub https://github.com/hubro-xx/CRL3
* 主页 http://www.cnblogs.com/hubro
* 在线文档 http://crl.changqidongli.com/
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Model;
using Shopping.BLL;
using CRL;
namespace Shopping.Web.Controllers
{
    [Authorize(Roles = "Member")]
    public class OrderController : Core.Mvc.BaseController
    {
        #region 订单
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateOrder()
        {
            var order = new Model.Order.OrderMain();
            order.UserId = CurrentUser.Id;
            string error;
            var a = OrderManage.Instance.SubmitFromCart(order, out error);
            if(!a)
            {
                return Content(error);
            }
            return RedirectToAction("PayOrder", new { orderId = order.OrderId });
        }
        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ActionResult PayOrder(string orderId)
        {
            var order = OrderManage.Instance.QueryItem(b => b.OrderId == orderId);
            return View(order);
        }
        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PayOrder(string orderId, int c)
        {
            string error;
            var order = OrderManage.Instance.QueryItem(b => b.OrderId == orderId);
            var a = OrderManage.Instance.PayOrder(order, out error);
            if (!a)
            {
                return Content(error);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //var orders = OrderManage.Instance.GetLambdaQuery().Where(b => b.UserId == CurrentUser.Id).Page(20,1).ToList();
            var user = MemberManage.Instance.GetCurrent();
            var sum = user.Orders.Sum(b => b.TotalAmount > 0, b => b.TotalAmount);
            var q = user.Orders.GetProvider().QueryList();
            var orders = user.Orders.Page(20, 1).ToList();
            var account = user.Account.Value;
            return View(q);
        }
        /// <summary>
        /// 订单详细
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ActionResult OrderDetail(string orderId)
        {
            var order = OrderManage.Instance.QueryItem(b => b.OrderId == orderId);
            var details = order.Details.ToList();
            ViewBag.Order = order;
            return View(details);
        }
        #endregion

    }
}
