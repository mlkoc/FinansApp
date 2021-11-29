using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinansApp.Managers
{
    public class MusteriManager:ManagerBase
    {
        public IEnumerable<Musteriler> GetList()
        {
            List<Musteriler> lstMusteriler = db.Musteriler.ToList();
            return lstMusteriler;
        }
        public IEnumerable<Musteriler> GetListByAdi(string adi)
        {
            List<Musteriler> lstMusteriler = db.Musteriler.Where(x=>x.AdiSoyadi.Contains(adi)).ToList();
            return lstMusteriler;
        }
        public Musteriler GetById(int id)
        {
            Musteriler must = db.Musteriler.FirstOrDefault(x => x.Id == id);
            return must;
        }
        public Musteriler InsertOrUpdate(Musteriler musteri)
        {
            try
            {
                Musteriler yeni = db.Musteriler.FirstOrDefault(x => x.Id == musteri.Id);
                if (yeni != null)
                {
                    yeni.AdiSoyadi = musteri.AdiSoyadi;
                    yeni.Satislar = musteri.Satislar;
                    yeni.KayitTarihi = musteri.KayitTarihi;
                    yeni.Mail = musteri.Mail;
                    yeni.Telefon = musteri.Telefon;
                }
                else
                {
                    musteri.KayitTarihi = DateTime.Now;
                    musteri =db.Musteriler.Add(musteri);
                }
                db.SaveChanges();
                error.ErrorMessage = "";
                error.HasError = false;
            }
            catch (Exception ex)
            {
                error.ErrorMessage = ex.Message;
                error.HasError = true;
            }
            

            return musteri;
        }
        public bool Delete(int id)
        {
            try
            {
                Musteriler yeni = db.Musteriler.FirstOrDefault(x => x.Id == id);
                if (yeni != null)
                {
                    db.Musteriler.Remove(yeni);
                    db.SaveChanges();
                    error.ErrorMessage = "";
                    error.HasError = false;
                }
                else
                {
                    error.ErrorMessage = id+" Id 'li herhangi bir kayda rastlanmadı";
                    error.HasError = true;
                }
                
            }
            catch (Exception ex)
            {
                error.ErrorMessage = ex.Message;
                error.HasError = true;
            }


            return !error.HasError;
        }
    }
}