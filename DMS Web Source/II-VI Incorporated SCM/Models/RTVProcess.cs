//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace II_VI_Incorporated_SCM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RTVProcess
    {
        public string NCR_NUMBER { get; set; }
        public bool Shipped { get; set; }
        public string TypeRTV { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string Remark { get; set; }
        public string CreditNote { get; set; }
        public string CreditFile { get; set; }
        public string RTVStatus { get; set; }
    }
}