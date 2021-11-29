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
    public class ApiMusterilerController : ApiController
    {
        MusteriManager mng = new MusteriManager();
        [HttpGet]
        
        public List<Musteriler> GetMusteriler()
        {
            return mng.GetList().ToList();
        }
        [HttpPost]
       
        public List<Musteriler> GetMusterilerByAdi(string adi="")
        {
            return mng.GetListByAdi(adi).ToList();
        }
        [HttpPost]
        public Musteriler InsertMusteri(Musteriler musteri)
        {
            return mng.InsertOrUpdate(musteri);
        }
    }
}
