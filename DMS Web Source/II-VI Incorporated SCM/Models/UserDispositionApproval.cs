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
    
    public partial class UserDispositionApproval
    {
        public string Id { get; set; }
        public string NCR_DIS_ID { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> DateApprove { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Comment { get; set; }
        public string NCRNUM { get; set; }
        public string DET_Item { get; set; }
        public string ReAssignUserId { get; set; }
        public string NCR_STATUS { get; set; }
    }
}
