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
    
    public partial class tbl_SQE_Ass_Vendor
    {
        public long Assessment_ID { get; set; }
        public string Period_Code { get; set; }
        public string Vendor_code { get; set; }
        public byte Category_ID { get; set; }
        public string Ass_Status { get; set; }
        public Nullable<double> Ave_Score { get; set; }
        public string Conclusion1 { get; set; }
        public string Conclusion2 { get; set; }
        public string Conclusion3 { get; set; }
        public Nullable<System.DateTime> Action_Date { get; set; }
        public string Action_User { get; set; }
    }
}
