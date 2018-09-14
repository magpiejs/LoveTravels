﻿using log4net;
using QuartzTaks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FytMsys.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            //启动定时任务
            JobScheduler.Start();
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception exception = Server.GetLastError().GetBaseException();
            //string requestUrl = Request.Url != null ? Request.Url.AbsoluteUri : String.Empty;
            //string errorInfo = "出现异常的页面或文件:" + requestUrl + "\r\n异常信息:"
            //    + exception.Message + "\r\n异常的堆栈信息:" + exception.StackTrace;
            //AddErrorInfo("ErrorLog.txt", errorInfo);
            //Server.ClearError();
            //Response.Redirect(CreateHtml.webset.weburl + "/404.html");
        }
        protected void AddErrorInfo(string fileName, string errorInfo)
        {
            string filePath = HttpRuntime.AppDomainAppPath + fileName;
            if (!String.IsNullOrEmpty(filePath) && !String.IsNullOrEmpty(errorInfo))
            {
                FileStream fs = null;
                if (!File.Exists(filePath))
                {
                    fs = File.Create(filePath);
                    fs.Close();
                    fs.Dispose();
                }
                using (fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(DateTime.Now.ToString() + "\r\n");
                    sw.Write(errorInfo + "\r\n\r\n");
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
    }
}