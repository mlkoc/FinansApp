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
    public class FaturalarController : Controller
    {
        // GET: Faturalar
        FaturalarManager mng = new FaturalarManager();
        SatislarManager mngSatis = new SatislarManager();
        MusteriManager mngMusteri = new MusteriManager();
        public ActionResult Index(int MusteriId=0,string adi="")
        {
            Musteriler musteri= mngMusteri.GetById(MusteriId);
            TempData["adi"] = adi;
            ViewData["MusteriId"] = MusteriId;
            if (musteri != null)
                TempData["AdiSoyadi"] = musteri.AdiSoyadi;
            IEnumerable<Faturalar> lstMus = null;
            if(MusteriId!=0)
            {
            lstMus = mng.GetByMusteriId(MusteriId);
            }
            else if(adi!="")
            {
                lstMus = mng.GetByMusteriAdi(adi);
            }
            else
            {
                lstMus = mng.GetList();
            }
                
            return View(lstMus);
        }
        public ActionResult FaturaKes(int MusteriId, int SatisId)
        {
            Musteriler musteri = mngMusteri.GetById(MusteriId);
            Satislar satis = mngSatis.GetById(SatisId);
            Faturalar fatura = new Faturalar();
            fatura.SatisId = SatisId;
            fatura.Tarih = DateTime.Now;
            fatura.Tutar = satis.Odenen;
            Faturalar fat = mng.GetBySatisld(SatisId);
            if (fat == null)
                fatura = mng.InsertOrUpdate(fatura);
            else
                fatura = fat;

            //ViewData["MusteriId"] = MusteriId;
            //ViewData["BorcId"] = BorcId;
            //TempData["AdiSoyadi"] = musteri.AdiSoyadi;
            //if (musteri == null)
            //    return RedirectToAction("Index", "Musteriler");
            //Faturalar borc = mng.GetById(id);
            return RedirectToAction("FaturaDetay", new { id= fatura.Id });
        }
        public ActionResult FaturaDetay(int id)
        {
            Faturalar fatura = mng.GetById(id);
            List<UrunSatis> lst = fatura.Satislar.UrunSatis.ToList();
            TempData["SatisId"] = fatura.SatisId;
            TempData["AdiSoyadi"] = fatura.Satislar.Musteriler.AdiSoyadi;
            return View(lst);
        }
        //public ActionResult Kayit(int MusteriId,int BorcId,int id = 0)
        //{
        //    Musteriler musteri = mngMusteri.GetById(MusteriId);
        //    ViewData["MusteriId"] = MusteriId;
        //    ViewData["BorcId"] = BorcId;
        //    TempData["AdiSoyadi"] = musteri.AdiSoyadi;
        //    if (musteri == null)
        //        return RedirectToAction("Index", "Musteriler");
        //    Faturalar borc = mng.GetById(id);
        //    return View(borc);
        //}
        //[HttpPost]
        //public ActionResult Kayit(Faturalar Faturalar)
        //{
        //    Faturalar = mng.InsertOrUpdate(Faturalar);
        //    return RedirectToAction("Index");
        //}
        public ActionResult Sil(int id)
        {
            mng.Delete(id);
            return RedirectToAction("Index");
        }
    }
}