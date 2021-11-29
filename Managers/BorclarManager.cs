using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinansApp.Managers
{
    public class BorclarManager:ManagerBase
    {
        public IEnumerable<Borclar> GetList()
        {
            List<Borclar> lst = db.Borclar.ToList();
            return lst;
        }
        public IEnumerable<Borclar> GetListByMusteriId(int MusteriId)
        {
            List<Borclar> lst = db.Borclar.Where(x => x.MusteriId == MusteriId).ToList();
            return lst;
        }
        public Borclar InsertOrUpdate(Borclar borc)
        {
            try
            {
                Borclar yeni = db.Borclar.FirstOrDefault(x => x.Id == borc.Id);
                if (yeni != null)
                {
                    yeni.MusteriId = borc.MusteriId;
                    yeni.Faturalar = borc.Faturalar;
                    yeni.Odenen = borc.Odenen;
                    yeni.Tarih = borc.Tarih;
                    yeni.ToplamBorc = borc.ToplamBorc;
                }
                else
                {
                    borc.Tarih = DateTime.Now;
                    borc =db.Borclar.Add(borc);
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
            

            return borc;
        }
        public Borclar GetById(int id)
        {
            Borclar borc = db.Borclar.FirstOrDefault(x => x.Id == id);
            return borc;
        }
        public bool Delete(int id)
        {
            try
            {
                Borclar yeni = db.Borclar.FirstOrDefault(x => x.Id == id);
                if (yeni != null)
                {
                    db.Borclar.Remove(yeni);
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