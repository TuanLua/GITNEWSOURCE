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
    
    public partial class tbl_SQE_Develop_VisitAudit
    {
        public long ID { get; set; }
        public string Supplier_Code { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Nullable<bool> Visit_Plan { get; set; }
        public string Visit_Actual { get; set; }
        public string Visit_Com { get; set; }
        public Nullable<bool> Audit_Plan { get; set; }
        public string Audit_Actual { get; set; }
        public string Audit_Com { get; set; }
        public string Attachment { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_At { get; set; }
        public string Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_At { get; set; }
    }
}