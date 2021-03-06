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

namespace Shopping.Web.Controllers
{
    public class ProxyController : Core.Mvc.BaseController
    {
        //
        // GET: /Proxy/

        public ActionResult Index(int page = 1, int pageSize = 100)
        {
            var query = BLL.ProxyPool.IpProxyManage.Instance.GetLambdaQuery();
            query.Page(pageSize, page);
            query.OrderBy(b => b.Id, true);
            var result = query.ToList();
            int count = query.RowCount;
            var pageObj = new PageObj<BLL.ProxyPool.IpProxy>(result, page, count, pageSize);
            return View(pageObj);
        }
        public ActionResult Check(int id)
        {
            var item = BLL.ProxyPool.IpProxyManage.Instance.QueryItem(id);
            string error;
            var a = BLL.ProxyPool.PoolManageService.Test(item,out error);
            return JsonResult(a, error, item.FullAddress);
        }
    }
}
