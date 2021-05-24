using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace II_VI_Incorporated_SCM.Models.SOReview
{
    public class ListSOItemReviewModel
    {
        public long ID { get; set; }
        public string SONO { get; set; }

        public DateTime DateDownLoad { get; set; }
        public string ItemReview { get; set; }

        public string DeptReview { get; set; }

        public bool? ReviewResult { get; set; }

        public string ReviewResultText { get; set; }

        public string Comment { get; set; }

        public bool? LastReview { get; set; }

        public string LastComment { get; set; }

        public string PlanShipDate { get; set; }

        public bool TBD { get; set; }
        public bool? IsLock { get; set; }
        public string Item { get; set; }
        public string Line { get; set; }

        public string ResolutionOwner { get; set; }

        #region Item Review List
        public string CoCofRoHS { get; set; }

        public string Capacity { get; set; }
        public string RawMaterial { get; set; }
        public string Builtless { get; set; }
        public string Carrier { get; set; }
        public string ServiceTypeShipping { get; set; }
        public string Special { get; set; }

        #endregion
    }
}