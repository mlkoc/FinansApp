//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinansApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Faturalar
    {
        public int Id { get; set; }
        public Nullable<int> SatisId { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<decimal> Tutar { get; set; }
    
        public virtual Satislar Satislar { get; set; }
    }
}
