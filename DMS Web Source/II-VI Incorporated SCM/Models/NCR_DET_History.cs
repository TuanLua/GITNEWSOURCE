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
    
    public partial class NCR_DET_History
    {
        public string NCR_NUM { get; set; }
        public string SEC { get; set; }
        public string ITEM { get; set; }
        public double QTY { get; set; }
        public string NC_DESC { get; set; }
        public string DEFECT { get; set; }
        public string RESPONSE { get; set; }
        public string DISPOSITION { get; set; }
        public Nullable<System.DateTime> DATEAPPROVAL { get; set; }
        public string REMARK { get; set; }
        public string Id { get; set; }
        public string CRNO { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}