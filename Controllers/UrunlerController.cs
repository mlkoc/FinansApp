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
    [AdminSessionControlFilter]
    public class UrunlerController : Controller
    {
        // GET: Urunler
        UrunlerManager mng = new UrunlerManager();
        public ActionResult Index(string adi="")
        {
            IEnumerable<Urunler> lstUrun = null;
            if(adi=="")
            {
                lstUrun = mng.GetList();
            }
            else
            {
                lstUrun = mng.GetListByAdi(adi);
            }
            return View(lstUrun);
        }
        public ActionResult Kayit(int id = 0)
        {
            Urunler musteri = mng.GetById(id);
            return View(musteri);
        }
        [HttpPost]
        public ActionResult Kayit(Urunler musteri)
        {
            musteri = mng.InsertOrUpdate(musteri);
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            mng.Delete(id);
            return RedirectToAction("Index");
        }
    }
}