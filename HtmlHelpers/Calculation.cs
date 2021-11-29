using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinansApp.HtmlHelpers
{
    public static class Calculation
    {
        public static decimal ToplamTutar(decimal? a,int? b)
        {
            if (a == null || b == null)
                return 0;
            return a.Value * b.Value;
        }
    }
}