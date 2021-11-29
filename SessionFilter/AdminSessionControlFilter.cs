
using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinansApp.SessionFilter
{
    public class AdminSessionControlFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Kullanicilar kull = ManagerSession.GetAdminUser();
            if (kull == null)
            {
                string redirectOnSuccess = filterContext.HttpContext.Request.Url.AbsolutePath;
                string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
                try
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                        filterContext.Result = new RedirectResult("/Giris/Index");
                    else
                        filterContext.Result = new RedirectResult("/Giris/Index/" + redirectUrl);
                }
                catch { }
            }
        }
    }
}