using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinansApp.Models;

namespace FinansApp.Managers
{
    public class ManagerBase
    {
        protected FinansDBEntities db = new FinansDBEntities();
        protected ManagerError error = new ManagerError();
        public ManagerError GetError()
        {
            return error;
        }

    }
}