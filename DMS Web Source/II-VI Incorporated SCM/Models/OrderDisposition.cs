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
    
    public partial class OrderDisposition
    {
        public int ID { get; set; }
        public string NCR_NUMBER { get; set; }
        public string ITEM { get; set; }
        public string COST { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string TypeOfDisposition { get; set; }
    }
}
