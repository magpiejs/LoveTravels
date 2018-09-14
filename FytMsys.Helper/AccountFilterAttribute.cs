﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;


namespace FytMsys.Helper
{
    /// <summary>
    /// 前台用户登录过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class AccountFilterAttribute : ActionFilterAttribute
    {
        public string Message { get; set; }
        //FilterContextInfo _fcinfo;
        // OnActionExecuted 在执行操作方法后由 ASP.NET MVC 框架调用。
        // OnActionExecuting 在执行操作方法之前由 ASP.NET MVC 框架调用。
        // OnResultExecuted 在执行操作结果后由 ASP.NET MVC 框架调用。
        // OnResultExecuting 在执行操作结果之前由 ASP.NET MVC 框架调用。
        /// <summary>
        /// 在执行操作方法之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //_fcinfo = new FilterContextInfo(filterContext);
            if (HttpContext.Current.Session["userShop"] == null)
            {
                //filterContext.Result = new HttpUnauthorizedResult();//直接URL输入的页面地址跳转到登陆页
                var bases = (HttpRequestBase)filterContext.HttpContext.Request;
                string url = bases.RawUrl.ToLower();
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { Controller = "Users", action = "Login",backurl=url}));
            }
        }

        /// <summary>
        /// 在执行操作方法后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        ///  OnResultExecuted 在执行操作结果后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        /// <summary>
        /// OnResultExecuting 在执行操作结果之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
        public class FilterContextInfo
        {
            public FilterContextInfo(ActionExecutingContext filterContext)
            {
                #region 获取链接中的字符
                // 获取域名
                if (filterContext.HttpContext.Request.Url != null)
                    DomainName = filterContext.HttpContext.Request.Url.Authority;

                //获取模块名称
                //  module = filterContext.HttpContext.Request.Url.Segments[1].Replace('/', ' ').Trim();

                //获取 controllerName 名称
                ControllerName = filterContext.RouteData.Values["controller"].ToString();

                //获取ACTION 名称
                ActionName = filterContext.RouteData.Values["action"].ToString();

                #endregion
            }

            /// <summary>
            /// 获取域名
            /// </summary>
            public string DomainName { get; set; }

            /// <summary>
            /// 获取模块名称
            /// </summary>
            public string Module { get; set; }
            /// <summary>
            /// 获取 controllerName 名称
            /// </summary>
            public string ControllerName { get; set; }
            /// <summary>
            /// 获取ACTION 名称
            /// </summary>
            public string ActionName { get; set; }

        }
    }
}