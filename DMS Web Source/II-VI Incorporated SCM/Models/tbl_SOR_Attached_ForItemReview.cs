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
    
    public partial class tbl_SOR_Attached_ForItemReview
    {
        public int ID { get; set; }
        public string SO_NO { get; set; }
        public System.DateTime Download_Date { get; set; }
        public int Item_Idx { get; set; }
        public string Attached_File { get; set; }
        public string LINE { get; set; }
        public string Attached_By { get; set; }
        public Nullable<System.DateTime> Attached_At { get; set; }
    }
}
