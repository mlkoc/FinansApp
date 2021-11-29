using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinansApp.Managers
{
    public class UrunSatisManager:ManagerBase
    {
        public IEnumerable<UrunSatis> GetList()
        {
            List<UrunSatis> lst = db.UrunSatis.ToList();
            return lst;
        }
        public IEnumerable<UrunSatis> GetListBySatisId(int SatisId)
        {
            List<UrunSatis> lst = db.UrunSatis.Where(x=>x.SatisId== SatisId).ToList();
            return lst;
        }
        public IEnumerable<UrunSatis> GetListByUrunId(int UrunId)
        {
            List<UrunSatis> lst = db.UrunSatis.Where(x => x.UrunId == UrunId).ToList();
            return lst;
        }
        public IEnumerable<UrunSatis> GetListByMusteriId(int MusteriId)
        {
            List<UrunSatis> lst = db.UrunSatis.Where(x => x.Satislar.MusteriId == MusteriId).ToList();
            return lst;
        }
        public UrunSatis InsertOrUpdate(UrunSatis UrunSatis)
        {
            try
            {
                UrunSatis yeni = db.UrunSatis.FirstOrDefault(x => x.Id == UrunSatis.Id);
                if (yeni != null)
                {
                    yeni.SatisId = UrunSatis.SatisId;
                    yeni.UrunId = UrunSatis.UrunId;
                    yeni.Adet = UrunSatis.Adet;
                   
                    
                   
                }
                else
                {
                  
                    UrunSatis =db.UrunSatis.Add(UrunSatis);
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
            

            return UrunSatis;
        }
        public UrunSatis GetById(int id)
        {
            UrunSatis borc = db.UrunSatis.FirstOrDefault(x => x.Id == id);
            return borc;
        }
        public bool Delete(int id)
        {
            try
            {
                UrunSatis yeni = db.UrunSatis.FirstOrDefault(x => x.Id == id);
                if (yeni != null)
                {
                    db.UrunSatis.Remove(yeni);
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