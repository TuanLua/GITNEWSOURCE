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
    
    public partial class DETAILQUESTION
    {
        public int ID { get; set; }
        public Nullable<int> IDQUESTION { get; set; }
        public string CONTENT { get; set; }
        public Nullable<System.DateTime> WRTIEDATE { get; set; }
        public string WRITEBY { get; set; }
    
        public virtual MAINQUESTION MAINQUESTION { get; set; }
    }
}