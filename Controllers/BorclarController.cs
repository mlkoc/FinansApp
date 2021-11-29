using FinansApp.Managers;
using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinansApp.Controllers
{
    public class BorclarController : Controller
    {
        // GET: Borclar
        BorclarManager mng = new BorclarManager();
        MusteriManager mngMusteri = new MusteriManager();
        public ActionResult Index(int MusteriId=0)
        {
            Musteriler musteri= mngMusteri.GetById(MusteriId);
            ViewData["MusteriId"] = MusteriId;
            if (musteri != null)
                TempData["AdiSoyadi"] = musteri.AdiSoyadi;
            IEnumerable<Borclar> lstMus = null;
            if(MusteriId!=0)
            lstMus = mng.GetList();
            else
                lstMus = mng.GetListByMusteriId(MusteriId);
            return View(lstMus);
        }
        public ActionResult Kayit(int MusteriId,int id = 0)
        {
            Musteriler musteri = mngMusteri.GetById(MusteriId);
            ViewData["MusteriId"] = MusteriId;
            TempData["AdiSoyadi"] = musteri.AdiSoyadi;
            if (musteri == null)
                return RedirectToAction("Index", "Musteriler");
            Borclar borc = mng.GetById(id);
            return View(borc);
        }
        [HttpPost]
        public ActionResult Kayit(Borclar borclar)
        {
            borclar = mng.InsertOrUpdate(borclar);
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            mng.Delete(id);
            return RedirectToAction("Index");
        }
    }
}