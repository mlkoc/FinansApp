using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinansApp.Managers
{
    public class SatislarManager:ManagerBase
    {
        public IEnumerable<Satislar> GetList()
        {
            List<Satislar> lst = db.Satislar.ToList();
            return lst;
        }
        public IEnumerable<Satislar> GetListByAdi(string adi)
        {
            List<Satislar> lst = db.Satislar.Where(x=>x.Musteriler.AdiSoyadi.Contains(adi)).ToList();
            return lst;
        }
        public IEnumerable<Satislar> GetListByMusteriId(int MusteriId)
        {
            List<Satislar> lst = db.Satislar.Where(x => x.MusteriId == MusteriId).ToList();
            return lst;
        }
        public Satislar InsertOrUpdate(Satislar borc)
        {
            try
            {
                Satislar yeni = db.Satislar.FirstOrDefault(x => x.Id == borc.Id);
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
                    borc =db.Satislar.Add(borc);
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
        public Satislar GetById(int id)
        {
            Satislar borc = db.Satislar.FirstOrDefault(x => x.Id == id);
            return borc;
        }
        public bool Delete(int id)
        {
            try
            {
                Satislar yeni = db.Satislar.FirstOrDefault(x => x.Id == id);
                if (yeni != null)
                {
                    db.Satislar.Remove(yeni);
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