using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinansApp.Managers
{
    public class FaturalarManager:ManagerBase
    {
        public IEnumerable<Faturalar> GetList()
        {
            List<Faturalar> lst = db.Faturalar.ToList();
            return lst;
        }
        public Faturalar GetBySatisld(int Satisld)
        {
            Faturalar fat = db.Faturalar.FirstOrDefault(x=>x.SatisId==Satisld);
            return fat;
        }
        public List<Faturalar> GetByMusteriId(int MusteriId)
        {
            List<Faturalar> fat = db.Faturalar.Where(x => x.Satislar.MusteriId == MusteriId).ToList();
            return fat;
        }
        public List<Faturalar> GetByMusteriAdi(string adi)
        {
            List<Faturalar> fat = db.Faturalar.Where(x => x.Satislar.Musteriler.AdiSoyadi.Contains(adi)).ToList();
            return fat;
        }
        public Faturalar InsertOrUpdate(Faturalar fatura)
        {
            try
            {
                Faturalar yeni = db.Faturalar.FirstOrDefault(x => x.Id == fatura.Id);
                if (yeni != null)
                {
                    yeni.SatisId = fatura.SatisId;
                    yeni.Tarih = fatura.Tarih;
                    yeni.Tutar = fatura.Tutar;
                   
                }
                else
                {
                    fatura=db.Faturalar.Add(fatura);
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
            

            return fatura;
        }
        public Faturalar GetById(int id)
        {
            Faturalar borc = db.Faturalar.FirstOrDefault(x => x.Id == id);
            return borc;
        }
        public bool Delete(int id)
        {
            try
            {
                Faturalar yeni = db.Faturalar.FirstOrDefault(x => x.Id == id);
                if (yeni != null)
                {
                    db.Faturalar.Remove(yeni);
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