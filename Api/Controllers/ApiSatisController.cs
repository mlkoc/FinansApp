using FinansApp.Managers;
using FinansApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinansApp.Api.Controllers
{
    [Authorize]
    public class ApiSatisController : ApiController
    {
        SatislarManager mng = new SatislarManager();
        [HttpGet]
        
        public List<Satislar> GetSatislar()
        {
            return mng.GetList().ToList();
        }
        [HttpPost]
       
        public List<Satislar> GetSatislarByAdi(string adi="")
        {
            return mng.GetListByAdi(adi).ToList();
        }
        [HttpPost]
        public Satislar InsertSatis(Satislar satis)
        {
            return mng.InsertOrUpdate(satis);
        }
    }
}
