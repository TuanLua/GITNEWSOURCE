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
    
    public partial class sp_DOC_by_ITEM5_Result
    {
        public long ID { get; set; }
        public string Item { get; set; }
        public string VendorName { get; set; }
        public string VendorNo { get; set; }
        public string CCN { get; set; }
        public string RequiredDOC { get; set; }
        public string PathDOC { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public Nullable<System.DateTime> UploadDocDate { get; set; }
        public string UploadUser { get; set; }
        public string Verified { get; set; }
        public Nullable<System.DateTime> VerifiedDocDate { get; set; }
        public string VerifiedDocUser { get; set; }
    }
}
