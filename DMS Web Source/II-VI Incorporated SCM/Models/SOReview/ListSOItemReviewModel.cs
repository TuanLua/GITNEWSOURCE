using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace II_VI_Incorporated_SCM.Models.SOReview
{
    public class ListSOItemReviewModel
    {
        public long ID { get; set; }
        public string SONO { get; set; }
        public string SOLine { get; set; }
        public DateTime DateDownLoad { get; set; }

        public DateTime? PROMISE_DATE { get; set; }
        public string ItemReview { get; set; }

        public string DeptReview { get; set; }

        public bool? ReviewResult { get; set; }
        public bool? ReviewResult1 { get; set; }
        public string ReviewResultText { get; set; }

        public string Comment { get; set; }

        public string SoDel { get; set; }
        public string LastReview { get; set; }

        public string LastComment { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PlanShipDate { get; set; }

        public bool TBD { get; set; }
        public bool? IsLock { get; set; }
        public string Item { get; set; }
        public string Line { get; set; }

        public bool? IsSubmit { get; set; }
        public string Allcomment { get; set; }

        public string ResolutionOwner { get; set; }

        #region Planner
        public string SOHold { get; set; }

        public string LastBuild { get; set; }

        public string LastWeeks { get; set; }

        public string DrawRevision { get; set; }

        public string ShipToLocation { get; set; }

        public string NewSoReviewLW { get; set; }

        public string FAI { get; set; }

        public DateTime? RequiredDate { get; set; }

        public double? OrderQty { get; set; }

        public double? BalanceQty {get; set;}

        public double? BalanceValue { get; set; }
        public string ITEM { get; set; }

        public string Key { get; set; }

        public string Analyst { get; set; }

        public string Status { get; set; }
        #endregion

        #region Item Review List
        public string CoCofRoHS { get; set; }
        public string CoCofRoHSComment { get; set; }

        public string Production { get; set; }
        public string ProductionComment { get; set; }
        public string Capacity { get; set; }
        public string CapacityComment { get; set; }
        public string RawMaterial { get; set; }
        public string RawMaterialComment { get; set; }
        public string Builtless { get; set; }
        public string BuiltlessComment { get; set; }
        public string Carrier { get; set; }
        public string CarrierComment { get; set; }
        public string ServiceTypeShipping { get; set; }
        public string ServiceTypeShippingComment { get; set; }
        public string Special { get; set; }
        public string SpecialComment { get; set; }

        public string AdditionalRequirementsReviewed { get; set; }
        public string AdditionalRequirementsReviewedComment { get; set; }
        public string Drawing { get; set; }
        public string DrawingComment { get; set; }
        #endregion
    }
}