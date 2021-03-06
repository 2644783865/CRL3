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
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Model
{
    /// <summary>
    /// 商家
    /// </summary>
    public class Supplier : CRL.Package.Person.Person
    {
        public CRL.Set.DbSet<Product> Products
        {
            get
            {
                return GetDbSet<Product>(b => b.SupplierId, Id);
            }
        }
        /// <summary>
        /// 初始数据
        /// </summary>
        /// <returns></returns>
        protected override System.Collections.IList GetInitData()
        {
            var list = new List<Supplier>();
            //123456
            list.Add(new Supplier() { Name = "Supplier1", AccountNo = "Supplier1", PassWord = "E10ADC3949BA59ABBE56E057F20F883E" });
            return list;
        }
        public CRL.Set.EntityRelation<CRL.Package.Account.AccountDetail> Account
        {
            get
            {
                return GetEntityRelation<CRL.Package.Account.AccountDetail>(b => b.Account, Id, b => b.AccountType == 1);
            }
        }
        /// <summary>
        /// 简称
        /// </summary>
        public string NickName
        {
            get;
            set;
        }
        /// <summary>
        /// 帐期(天)
        /// </summary>
        public int DayOfPayment
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人
        /// </summary>
        [CRL.Attribute.Field(Length = 50)]
        public string Manage { get; set; }

        

    }
}
