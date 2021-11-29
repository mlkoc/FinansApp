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
    public class SatislarController : Controller
    {
        // GET: Satislar
        SatislarManager mngSatislar = new SatislarManager();
        MusteriManager mngMusteri = new MusteriManager();
        UrunSatisManager mngUrunSatis = new UrunSatisManager();
        UrunlerManager mngUrunler = new UrunlerManager();
        public ActionResult Index(int MusteriId=0,string tum1Borc2="1")
        {
            TempData["Musteriler"] = mngMusteri.GetList();
            Musteriler musteri = null;
            if(MusteriId!=0)
            {
                musteri = mngMusteri.GetById(MusteriId);
                ViewData["MusteriId"] = MusteriId;
            }
          
            if (musteri != null)
                TempData["AdiSoyadi"] = musteri.AdiSoyadi;
            IEnumerable<Satislar> lstMus = null;
            if(MusteriId==0)
            {
                lstMus = mngSatislar.GetList();
                
            }
            
            else
                lstMus = mngSatislar.GetListByMusteriId(MusteriId);
            if (tum1Borc2 == "2")
            {
                lstMus=lstMus.Where(x => x.Odenen < x.ToplamBorc).ToList();
            }
            else if(tum1Borc2=="3")
            {
                lstMus = lstMus.Where(x => x.Odenen == x.ToplamBorc).ToList();
            }
            TempData["borc"] = tum1Borc2;
            return View(lstMus);
        }
        public ActionResult Kayit(int MusteriId)
        {
            Musteriler musteri = mngMusteri.GetById(MusteriId);
            TempData["MusteriId"] = MusteriId;
            TempData["AdiSoyadi"] = musteri.AdiSoyadi;
            Satislar satis = new Satislar();
            satis.Tarih = DateTime.Now;
            satis.MusteriId = MusteriId;
            satis.Odenen = 0;
            satis.ToplamBorc = 0;
            
            satis = mngSatislar.InsertOrUpdate(satis);
            return RedirectToAction("Satis",new { SatisId=satis.Id });
            //Musteriler musteri = mngMusteri.GetById(MusteriId);
            //TempData["MusteriId"] = MusteriId;
            //TempData["AdiSoyadi"] = musteri.AdiSoyadi;
            //if (musteri == null)
            //    return RedirectToAction("Index", "Musteriler");
            //Satislar borc = mng.GetById(id);
            //return View(borc);
        }
        [HttpPost]
        public ActionResult Kayit(Satislar Satislar)
        {
            Satislar = mngSatislar.InsertOrUpdate(Satislar);
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id,int SatisId = 0)
        {
            TempData["hata"] = "";
            TempData["sonuc"] = "";
            mngSatislar.Delete(id);
            if(mngSatislar.GetError().HasError)
            {
                TempData["hata"] = "Bu satışa ait bit çok ürün var.Satıs Detay sayfasından eklenmiş ürünleri silmelisiniz!";
             
            }
           // mngUrunSatis.Delete(id);
            if(SatisId == 0)
            return RedirectToAction("Index");
            return RedirectToAction("Satis",new { SatisId = SatisId });
        }
        public ActionResult UrunSil(int id, int SatisId = 0)
        {
            //mngSatislar.Delete(id);
            mngUrunSatis.Delete(id);
            if (SatisId == 0)
                return RedirectToAction("Index");
            return RedirectToAction("Satis", new { SatisId = SatisId });
        }
        public ActionResult Satis(int SatisId=0)
        {
            if (SatisId == 0)return RedirectToAction("Index");
            ViewData["Urunler"] = mngUrunler.GetList();
           
            TempData["SatisId"] = SatisId;
            Satislar satislar = mngSatislar.GetById(SatisId);
            ViewData["Odenen"] = satislar.Odenen;
            TempData["MusteriId"] = satislar.MusteriId;
            TempData["AdiSoyadi"] = satislar.Musteriler.AdiSoyadi;
            IEnumerable<UrunSatis> lst = mngUrunSatis.GetListBySatisId(SatisId);
            return View(lst);
        }
        [HttpPost]
        public ActionResult Odeme(decimal Odenen,int SatisID)
        {
            Satislar satis = mngSatislar.GetById(SatisID);
            TempData["hata"] = "";
            TempData["sonuc"] = "";
            if (satis==null)
            {
                TempData["hata"] = "Satıs işleminde bir sorun oluştu";
                return RedirectToAction("Index");
            }
            if (satis.Odenen == null) satis.Odenen = 0;
            satis.Odenen += Odenen;
            if(satis.Odenen>satis.ToplamBorc)
            {
                TempData["hata"] = "Ödeme toplam tutarı gecemez!";
                return RedirectToAction("Satis", new { SatisId = SatisID });
            }
            mngSatislar.InsertOrUpdate(satis);
            if(!mngSatislar.GetError().HasError)
            TempData["sonuc"] = "Ödeme işlemi Başarılı";
            else
                TempData["hata"] = mngSatislar.GetError().ErrorMessage;
            return RedirectToAction("Satis", new { SatisId = SatisID });
        }
        [HttpPost]
        public ActionResult UrunSatis(UrunSatis uSatis)
        {
            //ViewData["Urunler"] = mngUrunler.GetList();
            //ViewData["SatisId"] = satisSatisId;
            //IEnumerable<UrunSatis> lst = mngUrunSatis.GetListBySatisId(SatisId);
            uSatis=mngUrunSatis.InsertOrUpdate(uSatis);
            return RedirectToAction("Satis",new { SatisId=uSatis.SatisId });
        }
    }
}