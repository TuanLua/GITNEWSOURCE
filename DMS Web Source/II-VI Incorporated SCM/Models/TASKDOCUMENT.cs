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
    
    public partial class TASKDOCUMENT
    {
        public int ID { get; set; }
        public Nullable<int> TASKID_DETAIL { get; set; }
        public string FILENAME { get; set; }
        public string REV { get; set; }
        public Nullable<System.DateTime> DATEMODIFY { get; set; }
        public Nullable<int> SIZE { get; set; }
        public string WRITTENBY { get; set; }
        public Nullable<System.DateTime> WRITEDATE { get; set; }
        public string REVCOMMENT { get; set; }
        public string DESCRIPTION { get; set; }
        public string FILEPATH { get; set; }
    }
}
