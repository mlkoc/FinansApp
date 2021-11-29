using FinansApp.Managers;
using FinansApp.Models;
using FinansApp.SessionFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinansApp.Controllers
{
    public class GirisController : Controller
    {
        KullaniciManager mng = new KullaniciManager();
        public ActionResult Index()
        {
            return View();
            
        }
        [HttpPost]
        public ActionResult Index(Kullanicilar kul)
        {
           
            Kullanicilar kull = mng.GetByMailveSifre(kul.Email, kul.Sifre);
            if (kull != null)
            {
                ManagerSession.LoginAdminUserExist(kull);
                return RedirectToAction("Index", "Musteriler");
            }
            TempData["hata"] = "Mail veya şifreniz hatalı.";
            return View();
        }

    }
}