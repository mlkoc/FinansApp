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
    public class MusterilerController : Controller
    {
        // GET: Musteriler
        UrunlerManager mngUrunler = new UrunlerManager();
        MusteriManager mngMusteri = new MusteriManager();
        UrunSatisManager mngSatis = new UrunSatisManager();
        public ActionResult Index(string adi="")
        {
            ViewData["adi"] = adi;
            IEnumerable<Musteriler> lstMus = null;
            if (adi=="")
                lstMus = mngMusteri.GetList();
            else
                lstMus = mngMusteri.GetListByAdi(adi);
            return View(lstMus);
        }
        public ActionResult Kayit(int id=0)
        {
            Musteriler musteri = mngMusteri.GetById(id);
            return View(musteri);
        }
        [HttpPost]
        public ActionResult Kayit(Musteriler musteri)
        {
            musteri=mngMusteri.InsertOrUpdate(musteri);
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            TempData["hata"] = "";
            TempData["sonuc"] = "";
            mngMusteri.Delete(id);
            if(mngMusteri.GetError().HasError)
            {
                TempData["hata"] = "Bu müşteriye ait bir çok kayıt var. Bu kayıtlar silinmeden müşteri silinemez.";
            }
            return RedirectToAction("Index");
        }
       
    }
}