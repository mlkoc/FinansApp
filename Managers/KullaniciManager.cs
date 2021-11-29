using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinansApp.Managers
{
    public class KullaniciManager : ManagerBase
    {
        public IEnumerable<Kullanicilar> GetList()
        {
            List<Kullanicilar> lstKullanicilar = db.Kullanicilar.ToList();
            return lstKullanicilar;
        }
        public Kullanicilar GetById(int id)
        {
            Kullanicilar must = db.Kullanicilar.FirstOrDefault(x => x.Id == id);
            return must;
        }
        public Kullanicilar GetByMailveSifre(string  mail,string sifre)
        {
            Kullanicilar must = db.Kullanicilar.FirstOrDefault(x => x.Email==mail&&x.Sifre==sifre);
            return must;
        }
        public Kullanicilar InsertOrUpdate(Kullanicilar musteri)
        {
            try
            {
                Kullanicilar yeni = db.Kullanicilar.FirstOrDefault(x => x.Id == musteri.Id);
                if (yeni != null)
                {
                    yeni.Email = musteri.Email;
                    yeni.Sifre = musteri.Sifre;
                  
                }
                else
                {
                   
                    musteri =db.Kullanicilar.Add(musteri);
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
                Kullanicilar yeni = db.Kullanicilar.FirstOrDefault(x => x.Id == id);
                if (yeni != null)
                {
                    db.Kullanicilar.Remove(yeni);
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