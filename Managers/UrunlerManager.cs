using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinansApp.Managers
{
    public class UrunlerManager:ManagerBase
    {
        public IEnumerable<Urunler> GetList()
        {
            List<Urunler> lst = db.Urunler.ToList();
            return lst;
        }
        public IEnumerable<Urunler> GetListByAdi(string adi)
        {
            List<Urunler> lst = db.Urunler.Where(x=>x.Ad.Contains(adi)).ToList();
            return lst;
        }
        public Urunler InsertOrUpdate(Urunler urun)
        {
            try
            {
                Urunler yeni = db.Urunler.FirstOrDefault(x => x.Id == urun.Id);
                if (yeni != null)
                {
                    yeni.Ad = urun.Ad;
                    yeni.Fiyat = urun.Fiyat;
                   
                }
                else
                {
                  
                    urun =db.Urunler.Add(urun);
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
            

            return urun;
        }
        public Urunler GetById(int id)
        {
            Urunler borc = db.Urunler.FirstOrDefault(x => x.Id == id);
            return borc;
        }
        public bool Delete(int id)
        {
            try
            {
                Urunler yeni = db.Urunler.FirstOrDefault(x => x.Id == id);
                if (yeni != null)
                {
                    db.Urunler.Remove(yeni);
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