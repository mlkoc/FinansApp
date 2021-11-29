using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FinansApp.SessionFilter
{
    public class ManagerSession
    {
        #region KullaniciYukle
        public static bool LoginAdminUserExist(Kullanicilar kullanici)
        {
            try
            {
                HttpContext.Current.Session["Admin"] = kullanici;
                HttpCookie ck = HttpContext.Current.Request.Cookies["Admin"];
                if (ck == null)
                    ck = new HttpCookie("Admin", kullanici.Id + "");
                if (ck.Expires < DateTime.Now)
                {


                    ck.Value = kullanici.Id + "";
                    ck.Expires = DateTime.Now.AddMinutes(10);
                    HttpContext.Current.Response.Cookies.Add(ck);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool LoginUserExist(Kullanicilar kul)
        {
            try
            {
                HttpContext.Current.Session["User"] = kul;
                HttpCookie ck = HttpContext.Current.Request.Cookies["User"];
                if (ck == null)
                    ck = new HttpCookie("User", kul.Id + "");
                if (ck.Expires < DateTime.Now)
                {


                    ck.Value = kul.Id + "";
                    ck.Expires = DateTime.Now.AddDays(10);
                    HttpContext.Current.Response.Cookies.Add(ck);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Kullanicilar GetUser()
        {
            try
            {
                Kullanicilar cos = (Kullanicilar)HttpContext.Current.Session["User"];
                return cos;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Kullanicilar GetAdminUser()
        {
            try
            {
                Kullanicilar cos = (Kullanicilar)HttpContext.Current.Session["Admin"];
                // EyupSultanDBBLL.Manager.ManagerAdmin mngAdmin = new EyupSultanDBBLL.Manager.ManagerAdmin();

                //if (adm == null)
                //{
                //    HttpCookie ck = HttpContext.Current.Request.Cookies["adm"];
                //    if (ck != null && !string.IsNullOrEmpty(ck.Value))
                //    {
                //        int adminID = Convert.ToInt32(ck.Value);

                //        Admin adm2 = mngAdmin.GetById(adminID);
                //        if (adm2 != null)
                //            return adm2;
                //    }
                //}
                //else
                //    AdminYukle(adm);

                return cos;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        public static string GetUserSessionId()
        {
            if (HttpContext.Current.Session["Admin"] == null && HttpContext.Current.Session["User"] == null)
            {
                HttpContext.Current.Session["User"] = HttpContext.Current.Session;
            }
            HttpSessionState sessionValue = HttpContext.Current.Session;

            return sessionValue.SessionID.ToString();

        }
        public static int GetSessionTimeOut()
        {
            return HttpContext.Current.Session.Timeout;
        }
        public static Boolean Logout()
        {
            try
            {
                HttpContext.Current.Session["User"] = null;
                HttpContext.Current.Session["Admin"] = null;
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}