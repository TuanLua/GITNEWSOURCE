using II_VI_Incorporated_SCM.Models;
using II_VI_Incorporated_SCM.Models.SOReview;
using II_VI_Incorporated_SCM.Models.TaskManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace II_VI_Incorporated_SCM.Services
{
    public interface ISoReviewService
    {
        #region SOReview
        List<SelectListItem> GetDropdownlistUser();
        List<SelectListItem>  GetDropdownlistColumn();
        List<string> GetConditionHideColumn(string Department);
        bool GetIsLockBySo(string SO, DateTime date, string line);
        List<SelectListItem> GetReviewResult();
        List<string> GetAnalyst(string userID);
        List<sp_SOR_GetSoReview_Result> GetListSoReview();

        List<sp_SOR_GetSoOpen_Result> GetListReleaseSoReview();
        bool RealeaseSo();
        List<SelectListItem> GetDropdownlistAnalyst();
        List<sp_SOR_GetSoReviewHist_Result> GetListSoReviewHistory();
        string GetDepart(string userID);
        Result LockSoReview(string SoNo, string item, DateTime date, string islock);
        List<SoReviewDetail> GetSoReviewDetail(string soNo, DateTime dateReview, string status, string item);

        List<tbl_SOR_Attached_ForItemReview> GetListFileItem(DateTime date);

        Result UpdateDataSoReview(SoReviewDetail picData, int picID);

        Result UpdateSoReviewFinish(SoReviewDetail picData);

        Result AddTaskForItemReview(string SoNo, string Date, string itemreview, string userID, string assignee, string item, string taskname, int id);

        Result SaveFileAttachedItemReview(tbl_SOR_Attached_ForItemReview picData, int id);

        tbl_SOR_Attached_ForItemReview GetFileWithFileID(int fileId);

        Result DeleteDataFileofItemReview(string id);

        Result AddTaskForSoReview(string SoNo, string itemreview, string userID, string Assignee, string item, string taskname);

        List<TaskmanagementViewmodel> GetAllListTaskSoreview();
        #endregion

        #region MasterData

        #region PIC Review

        List<PICReviewmodel> GetListPIC();
        Result SaveDataPICReview(PICReviewmodel picData);
        Result UpdateDataPICReview(PICReviewmodel picData, int picID);

        Result DeleteDataPICReview(string id);
        #endregion

        #region ItemReview
        List<ItemReviewmodel> GetListItem();
        Result SaveDataItemReview(ItemReviewmodel picData);
        Result UpdateDataItemReview(ItemReviewmodel picData, int picID);
        Result DeleteDataItemReview(string id);
        #endregion

        #region Family
        List<FamilyReviewmodel> GetListFamily();
        Result SaveDataFamilyReview(FamilyReviewmodel picData);
        Result UpdateDataFamilyReview(FamilyReviewmodel picData, int picID);

        Result DeleteDataFamilyReview(string id);
        #endregion

        #region Analyst
        List<AnalystReviewmodel> GetListAnalyst();
        Result DeleteDataAnalystReview(string id);
        Result SaveDataAnalystReview(AnalystReviewmodel picData);

        Result UpdateDataAnalystReview(AnalystReviewmodel picData, int picID);
        #endregion

        #region PIC Column

        List<PICReviewmodel> GetListPICColumnHide();
        Result SaveDataPICColunnHide(PICReviewmodel picData);
        Result UpdateDataPICHideColumn(PICReviewmodel picData, int picID);
        Result DeleteDataPICColumnHide(string id);
        #endregion

        #endregion

        #region Report
        List<SelectListItem> GetdropdownPart();
        List<SelectListItem> GetdropdownSoReview();

        //List<sp_SOR_OTDFailByLine_Report_Result> SOR_OTDFailByLine_Report();

        //List<sp_SOR_RiskShip_Report_Result> SOR_RiskShip_Report1_Result();
        #endregion

        #region New Requirement
        List<ListSOItemReviewModel> GetListSOReviewByUserLogin(string depart,string isFilter, List<string> analyst);
        List<ListSOItemReviewModel> GetListSOReviewByPlanner(string depart, string isFilter, List<string> analyst);

        List<ListSOItemReviewModel> GetListApproveSOReviewByPlanner(string depart, string isFilter, List<string> analyst);
        List<ListSOItemReviewModel> GetListApproveSOReviewByPlannerExport(string depart, string isFilter, List<string> analyst);
        
        Result UpdateDataSoReviewResult(ListSOItemReviewModel picData, string idUser);

        Result SubmitDataSoReviewResult(ListSOItemReviewModel picData, string idUser);
        List<SelectListItem> GetDropdownlistSOreview();

        List<TaskmanagementViewmodel> GetListTaskSoreview(DateTime date);

        string SORReviewPlanner();

        List<SelectListItem> GetDropdownItembySOreview(string soNo,string line);


        Result UpdateDataPlannerSoReviewResult(ListSOItemReviewModel picData, string idUser);
        Result SubmitDataPlannerSoReviewResult(ListSOItemReviewModel picData, string idUser);

        Result ApproveDataPlannerSoReviewResult(ListSOItemReviewModel picData, string idUser);
        List<SelectListItem> GetDropdownLinebySOreview(string soNo);
        #endregion
    }
    public class SoReviewService : ISoReviewService
    {
        private readonly IIVILocalDB _db;
        public SoReviewService(IDbFactory dbFactory)
        {
            _db = dbFactory.Init();
        }

        #region SOReview
        public List<SelectListItem> GetDropdownlistUser()
        {
            List<SelectListItem> listuser = _db.AspNetUsers.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.FullName.Trim(),
            }).ToList();
            return listuser;
        }
        public List<SelectListItem> GetDropdownlistAnalyst()
        {
            List<SelectListItem> listuser = _db.tbl_SOR_Review_Analyst_Data.Select(x => new SelectListItem
            {
                Value = x.ANALYST.ToString(),
                Text = x.ANALYST .Trim(),
            }).ToList();
            return listuser;
        }
        public List<SelectListItem> GetDropdownlistColumn()
        {
            List<SelectListItem> listuser = _db.tbl_SOR_ColumnName_Data.Select(x => new SelectListItem
            {
                Value = x.ColunmName.ToString(),
                Text = x.ColunmNameShow.Trim(),
            }).ToList();
            return listuser;
        }
        public List<SelectListItem> GetReviewResult()
        {
            List<SelectListItem> lstData = new List<SelectListItem>();
            SelectListItem s1 = new SelectListItem();
            s1.Value = "Y";
            s1.Text = "Y";
            lstData.Add(s1);
            SelectListItem s2 = new SelectListItem();
            s2.Value = "N";
            s2.Text = "N";
            lstData.Add(s2);
            SelectListItem s3 = new SelectListItem();
            s3.Value = "N/A";
            s3.Text = "N/A";
            lstData.Add(s3);
            return lstData;
        }
        public List<string> GetConditionHideColumn(string Department)
        {
            List<string> lstData = _db.tbl_SOR_DeptHideColumn.Where(x=>x.DEPT == Department).OrderBy(x=>x.ORDERNUMBER).Select(x => x.COLUMNSHOW).ToList();
            return lstData;
        }
        public List<sp_SOR_GetSoReview_Result> GetListSoReview()
        {
            List<sp_SOR_GetSoReview_Result> data = _db.sp_SOR_GetSoReview().ToList();
            return data;
        }
        public List<sp_SOR_GetSoReviewHist_Result> GetListSoReviewHistory()
        {
            List<sp_SOR_GetSoReviewHist_Result> data = _db.sp_SOR_GetSoReviewHist().ToList();
            return data;
        }
        public List<sp_SOR_GetSoOpen_Result> GetListReleaseSoReview()
        {
            List<sp_SOR_GetSoOpen_Result> data = _db.sp_SOR_GetSoOpen().ToList();
            return data;
        }

        public bool RealeaseSo()
        {
            var data = _db.sp_SOR_Release();
            return true;
        }
        public bool GetIsLockBySo(string SO, DateTime date, string line)
        {
            var data = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.SO_NO == SO && x.DOWNLOAD_DATE == date && x.LINE == line && x.ISLOCK == true).ToList();
            if (data.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<SoReviewDetail> GetSoReviewDetail(string soNo, DateTime dateReview, string status, string item1)
        {
            var current = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.SO_NO == soNo && x.DOWNLOAD_DATE == dateReview && x.LINE.Trim() == item1)
                      .ToList();
            var top1 = _db.tbl_SOR_His_Review_Detail.Where(x => x.SO_NO == soNo && x.DOWNLOAD_DATE == dateReview && x.LINE.Trim() == item1).ToList().OrderBy(x => x.DOWNLOAD_DATE)?.FirstOrDefault();
            if (top1 != null)
            {
                var history = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.SO_NO == top1.SO_NO && x.DOWNLOAD_DATE == top1.DOWNLOAD_DATE && x.LINE.Trim() == item1).ToList();
                var data = (from c in current
                            join p in history on c.SO_NO equals p.SO_NO into ps
                            from p in ps.DefaultIfEmpty()
                            where (c.SO_NO == soNo && c.DOWNLOAD_DATE == dateReview)
                            select new SoReviewDetail
                            {
                                ID = c.ITEM_REVIEW_ID,
                                SONO = c.SO_NO,
                                ItemReview = c.ITEM_REVIEW,
                                DeptReview = c.DEPT_REVIEW.Trim(),
                                Comment = c.COMMENT == null ? "" : c.COMMENT,
                                ReviewResult = c.RESULT,
                                LastComment = p.COMMENT,
                                //LastReview = p.RESULT,
                                IsLock = c.ISLOCK == true ? "True" : "False"
                            }).ToList();
                //  foreach (var item in data)
                //{
                //    if (item.ReviewResult == null)
                //    {
                //        item.ReviewResult = "";
                //    }

                //}
                return data;
            }
            else
            {
                var datacurrent = current.Where(x => x.SO_NO == soNo && x.DOWNLOAD_DATE == dateReview && x.LINE.Trim() == item1)
                    .Select(x => new SoReviewDetail
                    {
                        ID = x.ITEM_REVIEW_ID,
                        SONO = x.SO_NO,
                        ItemReview = x.ITEM_REVIEW,
                        DeptReview = x.DEPT_REVIEW.Trim(),
                        Comment = x.COMMENT == null ? "" : x.COMMENT,
                        ReviewResult = x.RESULT,
                        LastComment = null,
                        IsLock = x.ISLOCK == true ? "True" : "False"
                    }).ToList();
                //foreach (var item in datacurrent)
                //{
                //    if (item.ReviewResult == null)
                //    {
                //        item.ReviewResult = "";
                //    }

                //}
                return datacurrent;
            }

        }

        public string GetDepart(string userID)
        {
            var user = _db.tbl_SOR_Review_Pic.Where(x => x.Pic_Rv == userID).FirstOrDefault();
            if (user != null)
            {
                return user.Dept_Rv;
            }
            else
            {
                return "";
            }
        }
        public List<string> GetAnalyst(string userID)
        {
            var user = _db.tbl_SOR_Review_Analyst.Where(x => x.PIC == userID).Select(x=>x.ANALYST).ToList();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        public Result LockSoReview(string SoNo, string item, DateTime date, string isLock)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {

                    var data = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.SO_NO == SoNo && x.DOWNLOAD_DATE == date && x.LINE == item).ToList();
                    if (data != null && isLock == "False")
                    {
                        foreach (var item1 in data)
                        {
                            item1.ISLOCK = true;
                        }
                        _db.SaveChanges();
                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                            message = "Lock Data sucess!",
                        };
                    }
                    else if (data != null && isLock == "True")
                    {
                        foreach (var item1 in data)
                        {
                            item1.ISLOCK = false;
                        }
                        _db.SaveChanges();
                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                            message = "UnLock Data sucess!",
                        };
                    }
                    else
                    {
                        return new Result
                        {
                            success = true,
                            message = "No data unlock!",
                        };
                    }
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }
        public List<tbl_SOR_Attached_ForItemReview> GetListFileItem(DateTime date)
        {

            // get date certification
            var data = _db.tbl_SOR_Attached_ForItemReview.Where(x => x.Download_Date == date).ToList();
            return data;
        }

        public tbl_SOR_Attached_ForItemReview GetFileWithFileID(int fileId)
        {
            return _db.tbl_SOR_Attached_ForItemReview.Where(m => m.ID == fileId).FirstOrDefault();
        }

        public Result SaveFileAttachedItemReview(tbl_SOR_Attached_ForItemReview picData, int id)
        {
            var _log = new LogWriter("AddData");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.tbl_SOR_Attached_ForItemReview.Add(picData);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception AddData!",
                        obj = -1
                    };
                }
            }
        }

        public Result AddTaskForItemReview(string SoNo, string Date, string itemreview, string userID, string Assignee, string item, string taskname, int id)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var currentTaskListID = 0;
                    var taskNO = SoNo + "-" + Date + "-" + item;
                    var ckTaskList = _db.TASKLISTs.FirstOrDefault(x => x.Reference.Trim().Equals(taskNO.Trim()) && x.TYPE == "SoReview");

                    if (ckTaskList != null)
                    {
                        TASKDETAIL taskDetail = new TASKDETAIL
                        {
                            TopicID = ckTaskList.TopicID,
                            TASKNAME = itemreview,
                            DESCRIPTION = taskname,
                            OWNER = userID,
                            ASSIGNEE = Assignee,
                            APPROVE = userID,
                            EstimateStartDate = DateTime.Now,
                            EstimateEndDate = DateTime.Now.AddDays(7),
                            ActualStartDate = DateTime.Now,
                            ActualEndDate = DateTime.Now.AddDays(7),
                            CreatedDate = DateTime.Now,
                            STATUS = "Create",
                            Level = 1
                        };
                        _db.TASKDETAILs.Add(taskDetail);
                        var data = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.ITEM_REVIEW_ID == id).FirstOrDefault();
                        if (data != null)
                        {
                            data.RESULT = "";
                        }
                        _db.SaveChanges();
                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                            message = "Create task success!",
                            obj = -1
                        };
                    }
                    return new Result
                    {
                        success = false,
                        message = "Task has created!",
                        obj = -1
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }

        public Result DeleteDataFileofItemReview(string id)
        {
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var checkData = _db.tbl_SOR_Attached_ForItemReview.FirstOrDefault(x => x.ID.ToString().Trim() == id.Trim());
                    _db.tbl_SOR_Attached_ForItemReview.Remove(checkData);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        message = "Delete success!",
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    return new Result
                    {
                        message = ex.ToString(),
                        obj = ex,
                        success = false
                    };
                }
            }
        }
        #endregion

        #region MasterData

        #region PIC Review

        public List<PICReviewmodel> GetListPIC()
        {
            var picData = (from tbl in _db.tbl_SOR_Review_Pic
                           join user in _db.AspNetUsers on tbl.Pic_Rv equals user.Id
                           select (new PICReviewmodel
                           {

                               ID = tbl.Pic_Inx,
                               Dept = tbl.Dept_Rv,
                               Pic = user.FullName,
                               PicID = tbl.Pic_Rv
                           })).ToList();
            return picData;
        }
        public Result SaveDataPICReview(PICReviewmodel picData)
        {
            var _log = new LogWriter("AddData");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var dataInsert = new tbl_SOR_Review_Pic();
                    dataInsert.Dept_Rv = picData.Dept;
                    dataInsert.Pic_Rv = picData.Pic;
                    _db.tbl_SOR_Review_Pic.Add(dataInsert);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception AddData!",
                        obj = -1
                    };
                }
            }
        }
        public Result UpdateSoReviewFinish(SoReviewDetail picData)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var data = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.SO_NO == picData.SONO && x.DOWNLOAD_DATE == picData.DateDownLoad && x.LINE == picData.Item).ToList();
                    if (data != null)
                    {
                        bool isNotUpdate = false;
                        foreach (var item in data)
                        {
                            if (item.RESULT == null)
                            {
                                isNotUpdate = true;
                            }
                        }
                        if (isNotUpdate)
                        {
                            return new Result
                            {
                                success = false,
                                message = "Please review all item !",
                            };
                        }
                        else
                        {
                            var dataSoreview = _db.tbl_SOR_Cur_Review_List.Where(x => x.SO_NO == picData.SONO && x.DOWNLOAD_DATE == picData.DateDownLoad && x.LINE == picData.Item).FirstOrDefault();
                      //      dataSoreview.PLAN_SHIP_DATE = picData.PlanShipDate;
                            dataSoreview.REVIEW_STATUS = "Done";
                            dataSoreview.COMMENT = picData.Comment;
                            _db.SaveChanges();
                            tranj.Commit();
                            return new Result
                            {
                                success = true,
                                message = "Finish sucess!"
                            };
                        }
                    }
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }
        public Result UpdateDataSoReview(SoReviewDetail picData, int picID)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    if (picData.IsLock == "True")
                    {
                        return new Result
                        {
                            success = false,
                            message = "Item review locked by Planner!",
                            obj = -1
                        };
                    }
                    var data = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.ITEM_REVIEW_ID == picID).FirstOrDefault();
                    if (data != null)
                    {

                        data.COMMENT = picData.Comment;
                        data.RESULT = picData.ReviewResult;
                        _db.SaveChanges();
                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                        };
                    }
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }
        public Result DeleteDataPICReview(string id)
        {
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var checkData = _db.tbl_SOR_Review_Pic.FirstOrDefault(x => x.Pic_Inx.ToString().Trim() == id.Trim());
                    _db.tbl_SOR_Review_Pic.Remove(checkData);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        message = "Delete success!",
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    return new Result
                    {
                        message = ex.ToString(),
                        obj = ex,
                        success = false
                    };
                }
            }
        }
        public Result UpdateDataPICReview(PICReviewmodel picData, int picID)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var data = _db.tbl_SOR_Review_Pic.Where(x => x.Pic_Inx == picID).FirstOrDefault();
                    data.Dept_Rv = picData.Dept;
                    data.Pic_Rv = picData.Pic;
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }

        #endregion

        #region Item Review
        public List<ItemReviewmodel> GetListItem()
        {
            var picData = _db.tbl_SOR_Item_Review.
                          Select(x => new ItemReviewmodel
                          {

                              ID = x.Item_Idx,
                              Dept = x.Dept_Rv,
                              ItemReview = x.Item_Rv,
                              Isdefault = x.Default == true ? "Y" : "N"
                          }).ToList();
            return picData;
        }
        public Result SaveDataItemReview(ItemReviewmodel picData)
        {
            var _log = new LogWriter("AddData");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var dataInsert = new tbl_SOR_Item_Review();
                    dataInsert.Dept_Rv = picData.Dept;
                    dataInsert.Item_Rv = picData.ItemReview;
                    //  dataInsert.Default = picData.Isdefault;
                    _db.tbl_SOR_Item_Review.Add(dataInsert);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception AddData!",
                        obj = -1
                    };
                }
            }
        }
        public Result UpdateDataItemReview(ItemReviewmodel picData, int picID)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var data = _db.tbl_SOR_Item_Review.Where(x => x.Item_Idx == picID).FirstOrDefault();
                    data.Dept_Rv = picData.Dept;
                    data.Item_Rv = picData.ItemReview;
                    //  data.Default = picData.Isdefault;
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }
        public Result DeleteDataItemReview(string id)
        {
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var checkData = _db.tbl_SOR_Item_Review.FirstOrDefault(x => x.Item_Idx.ToString().Trim() == id.Trim());
                    _db.tbl_SOR_Item_Review.Remove(checkData);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        message = "Delete success!",
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    return new Result
                    {
                        message = ex.ToString(),
                        obj = ex,
                        success = false
                    };
                }
            }
        }
        #endregion

        #region Family

        public List<FamilyReviewmodel> GetListFamily()
        {
            var picData = _db.tbl_SOR_Family_Setup_Qty.
                          Select(x => new FamilyReviewmodel
                          {
                              ID = x.Family_inx,
                              Family = x.Family,
                              MaxQty = x.Setup_Qty
                          }).ToList();
            return picData;
        }
        public Result SaveDataFamilyReview(FamilyReviewmodel picData)
        {
            var _log = new LogWriter("AddData");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var dataInsert = new tbl_SOR_Family_Setup_Qty();
                    dataInsert.Family = picData.Family;
                    dataInsert.Setup_Qty = picData.MaxQty;
                    _db.tbl_SOR_Family_Setup_Qty.Add(dataInsert);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception AddData!",
                        obj = -1
                    };
                }
            }
        }
        public Result UpdateDataFamilyReview(FamilyReviewmodel picData, int picID)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var data = _db.tbl_SOR_Family_Setup_Qty.Where(x => x.Family_inx == picID).FirstOrDefault();
                    data.Family = picData.Family;
                    data.Setup_Qty = picData.MaxQty;
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }
        public Result DeleteDataFamilyReview(string id)
        {
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var checkData = _db.tbl_SOR_Family_Setup_Qty.FirstOrDefault(x => x.Family.Trim() == id.Trim());
                    _db.tbl_SOR_Family_Setup_Qty.Remove(checkData);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        message = "Delete success!",
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    return new Result
                    {
                        message = ex.ToString(),
                        obj = ex,
                        success = false
                    };
                }
            }
        }
        #endregion

        #region Analyst Review

        public List<AnalystReviewmodel> GetListAnalyst()
        {
            var picData = (from tbl in _db.tbl_SOR_Review_Analyst
                           join user in _db.AspNetUsers on tbl.PIC equals user.Id
                           select (new AnalystReviewmodel
                           {

                               ID = tbl.Ana_Inx,
                               Analyst = tbl.ANALYST,
                               Pic = user.FullName,
                               PicID = tbl.PIC
                           })).ToList();
            return picData;
        }
        public Result SaveDataAnalystReview(AnalystReviewmodel picData)
        {
            var _log = new LogWriter("AddData");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var dataInsert = new tbl_SOR_Review_Analyst();
                    dataInsert.ANALYST = picData.Analyst;
                    dataInsert.PIC = picData.Pic;
                    _db.tbl_SOR_Review_Analyst.Add(dataInsert);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception AddData!",
                        obj = -1
                    };
                }
            }
        }

        public Result DeleteDataAnalystReview(string id)
        {
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var checkData = _db.tbl_SOR_Review_Analyst.FirstOrDefault(x => x.Ana_Inx.ToString().Trim() == id.Trim());
                    _db.tbl_SOR_Review_Analyst.Remove(checkData);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        message = "Delete success!",
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    return new Result
                    {
                        message = ex.ToString(),
                        obj = ex,
                        success = false
                    };
                }
            }
        }
        public Result UpdateDataAnalystReview(AnalystReviewmodel picData, int picID)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var data = _db.tbl_SOR_Review_Analyst.Where(x => x.Ana_Inx == picID).FirstOrDefault();
                    data.ANALYST = picData.Analyst;
                    data.PIC = picData.Pic;
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }

        #endregion

        #region PIC Colunm

        public List<PICReviewmodel> GetListPICColumnHide()
        {
            var picData = (from tbl in _db.tbl_SOR_DeptHideColumn
                           select (new PICReviewmodel
                           {
                               ID = tbl.ID,
                               Dept = tbl.DEPT,
                               Pic = tbl.COLUMNSHOW,
                               ODERNUNMBER = tbl.ORDERNUMBER
                           })).ToList();
            return picData;
        }
        public Result SaveDataPICColunnHide(PICReviewmodel picData)
        {
            var _log = new LogWriter("AddData");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var dataInsert = new tbl_SOR_DeptHideColumn();
                    dataInsert.DEPT = picData.Dept;
                    dataInsert.COLUMNSHOW = picData.Pic;
                    dataInsert.ORDERNUMBER = picData.ODERNUNMBER;
                    _db.tbl_SOR_DeptHideColumn.Add(dataInsert);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception AddData!",
                        obj = -1
                    };
                }
            }
        }
        public Result UpdateDataPICHideColumn(PICReviewmodel picData, int picID)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var data = _db.tbl_SOR_DeptHideColumn.Where(x => x.ID == picID).FirstOrDefault();
                    data.DEPT = picData.Dept;
                    data.COLUMNSHOW = picData.Pic;
                    data.ORDERNUMBER = picData.ODERNUNMBER;
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }
        #endregion
        public Result DeleteDataPICColumnHide(string id)
        {
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var checkData = _db.tbl_SOR_DeptHideColumn.FirstOrDefault(x => x.ID.ToString().Trim() == id.Trim());
                    _db.tbl_SOR_DeptHideColumn.Remove(checkData);
                    _db.SaveChanges();
                    tranj.Commit();
                    return new Result
                    {
                        message = "Delete success!",
                        success = true,
                    };
                }
                catch (Exception ex)
                {
                    return new Result
                    {
                        message = ex.ToString(),
                        obj = ex,
                        success = false
                    };
                }
            }
        }
        #endregion

        #region Report
        public List<SelectListItem> GetdropdownPart()
        {
            List<SelectListItem> listpart = _db.tbl_SOR_Cur_Review_List.Select(x => new SelectListItem
            {
                Value = x.ANALYST,
                Text = x.ANALYST
            }).ToList();
            return listpart;
        }
        public List<SelectListItem> GetdropdownSoReview()
        {
            List<SelectListItem> lstSo = _db.tbl_SOR_Cur_Review_List.Select(x => new SelectListItem
            {
                Value = x.SO_NO,
                Text = x.SO_NO
            }).ToList();
            return lstSo;
        }

        //public List<sp_SOR_OTDFailByLine_Report_Result> SOR_OTDFailByLine_Report()
        //{
        //    List<sp_SOR_OTDFailByLine_Report_Result> data = _db.sp_SOR_OTDFailByLine_Report().ToList();
        //    return data;
        //}

        //public List<sp_SOR_RiskShip_Report_Result> SOR_RiskShip_Report1_Result()
        //{
        //    List<sp_SOR_RiskShip_Report_Result> data = _db.sp_SOR_RiskShip_Report().ToList();
        //    return data;
        //}

        #region Update Lst Data

        public List<ListSOItemReviewModel> GetListSOReviewByUserLogin(string depart,string isFilter,List<string> analyst)
        {
            bool? flag = null;
            var analystuser = "";
            if(analyst != null)
            {
                foreach (var item in analyst)
                {
                    analystuser += item;
                }
            }
            var result = _db.sp_SOR_GetListSoreviewbyUserLogin(depart, analystuser).Where(x => x.REVIEW_STATUS != "Reviewed").ToList();
            List<ListSOItemReviewModel> data = new List<ListSOItemReviewModel>();
            if (isFilter == "NotReview")
            {
                if(result != null)
                {
                    data =result.Where(x => x.ISSUBMIT == false || x.ISSUBMIT == null).Select(x => new ListSOItemReviewModel
                         {
                             SONO = x.SO_NO,
                             ItemReview = x.ITEM_REVIEW,
                             ReviewResult = x.RESULT == null ? flag : (x.RESULT == "Y" ? true : false),
                             ReviewResultText = x.RESULT,
                             Comment = x.COMMENT,
                             LastReview = x.LAST_RESULT == null ? null : (x.LAST_RESULT == "Y" ? "Y" : "N"),
                             LastComment = x.LAST_COMMENT,
                              ID = x.ITEM_REVIEW_ID,
                              Key = x.SO_NO + x.ITEM + x.LINE,
                             DateDownLoad = x.DOWNLOAD_DATE,
                             ReviewResult1 = x.RESULT == null ? flag : (x.RESULT == "N" ? true : false),
                             SOHold = x.SO_ON_HOLD,
                             DrawRevision = x.DR_REV,
                             LastBuild = x.LAST_BUILD_DR_REV,
                             LastWeeks = x.LAST_REVIEW_DR_REV,
                             BalanceQty = x.BLC_QTY,
                             BalanceValue = x.BLC_VALUE,
                             ShipToLocation = x.SHIP_TO,
                             NewSoReviewLW = x.NEW_REVIEW == true ? "Y" : "N",
                             FAI = x.FAI,
                             OrderQty = x.ORD_QTY,
                             RequiredDate = x.REQUIRED_DATE,
                             ITEM = x.ITEM,
                             Analyst = x.ANALYST,
                             Line = x.LINE,
                             PROMISE_DATE = x.PROMISE_DATE,
                             Status = x.REVIEW_STATUS,
                             SoDel = (x.LINE != null || x.LINE != "") ? x.LINE.Substring(4, 4) : null,
                             SOLine = (x.LINE != null || x.LINE != "") ? x.LINE.Substring(0, 4) : null,
                             IsSubmit = x.ISSUBMIT,
                             ResolutionOwner = x.ResolutionOwner
                         }).Distinct().OrderBy(x=>x.PROMISE_DATE).ToList();
                    return data;
                }
            }
            else if(isFilter == "All")
            {
                if(result != null)
                {
                     data = result.Select(x => new ListSOItemReviewModel
                    {
                        SONO = x.SO_NO,
                        ItemReview = x.ITEM_REVIEW,
                        ReviewResult = x.RESULT == null ? flag : (x.RESULT == "Y" ? true : false),
                        ReviewResultText = x.RESULT,
                        Comment = x.COMMENT,
                        LastReview = x.LAST_RESULT == null ? null: (x.LAST_RESULT == "Y" ? "Y" : "N"),
                        LastComment = x.LAST_COMMENT,
                         ID = x.ITEM_REVIEW_ID,
                         Key = x.SO_NO + x.ITEM + x.LINE,
                        DateDownLoad = x.DOWNLOAD_DATE,
                        ReviewResult1 = x.RESULT == null ? flag : (x.RESULT == "N" ? true : false),
                        SOHold = x.SO_ON_HOLD,
                        DrawRevision = x.DR_REV,
                        LastBuild = x.LAST_BUILD_DR_REV,
                        LastWeeks = x.LAST_REVIEW_DR_REV,
                        BalanceQty = x.BLC_QTY,
                        BalanceValue = x.BLC_VALUE,
                        ShipToLocation = x.SHIP_TO,
                         NewSoReviewLW = x.NEW_REVIEW == true ? "Y" : "N",
                         FAI = x.FAI,
                        OrderQty = x.ORD_QTY,
                        RequiredDate = x.REQUIRED_DATE,
                        ITEM = x.ITEM,
                        Analyst = x.ANALYST,
                         Line = x.LINE,
                         PROMISE_DATE = x.PROMISE_DATE,
                         Status = x.REVIEW_STATUS,
                         SoDel = (x.LINE != null || x.LINE != "") ? x.LINE.Substring(4, 4) : null,
                         SOLine = (x.LINE != null || x.LINE != "") ? x.LINE.Substring(0, 4) : null,
                         IsSubmit = x.ISSUBMIT
                     }).Distinct().OrderBy(x => x.PROMISE_DATE).ToList();
                    return data;
                }
            }

            else if( isFilter == "Reviewed")
            {
                data = result.Where(x => x.ISSUBMIT == true).Select(x => new ListSOItemReviewModel
                {
                    SONO = x.SO_NO,
                    ItemReview = x.ITEM_REVIEW,
                    ReviewResult = x.RESULT == null ? flag : (x.RESULT == "Y" ? true : false),
                    ReviewResultText = x.RESULT,
                    Comment = x.COMMENT,
                    LastReview = x.LAST_RESULT == null ? null : (x.LAST_RESULT == "Y" ? "Y" : "N"),
                    LastComment = x.LAST_COMMENT,
                    ID = x.ITEM_REVIEW_ID,
                    Key = x.SO_NO + x.ITEM + x.LINE,
                    DateDownLoad = x.DOWNLOAD_DATE,
                    ReviewResult1 = x.RESULT == null ? flag : (x.RESULT == "N" ? true : false),
                    SOHold = x.SO_ON_HOLD,
                    DrawRevision = x.DR_REV,
                    LastBuild = x.LAST_BUILD_DR_REV,
                    LastWeeks = x.LAST_REVIEW_DR_REV,
                    BalanceQty = x.BLC_QTY,
                    BalanceValue = x.BLC_VALUE,
                    ShipToLocation = x.SHIP_TO,
                    NewSoReviewLW = x.NEW_REVIEW == true ? "Y" : "N",
                    FAI = x.FAI,
                    OrderQty = x.ORD_QTY,
                    RequiredDate = x.REQUIRED_DATE,
                    ITEM = x.ITEM,
                    Analyst = x.ANALYST,
                    Line =  x.LINE,
                    PROMISE_DATE = x.PROMISE_DATE,
                    Status = x.REVIEW_STATUS,
                    SoDel = (x.LINE != null || x.LINE != "") ? x.LINE.Substring(4, 4) : null,
                    SOLine = (x.LINE != null || x.LINE != "") ? x.LINE.Substring(0, 4) : null,
                    IsSubmit = x.ISSUBMIT
                }).Distinct().OrderBy(x => x.PROMISE_DATE).ToList();
                return data;
            }
            return data;
        }
        #endregion

        public List<ListSOItemReviewModel> GetListSOReviewByPlanner(string depart, string isFilter, List<string> analyst)
        {
            if (isFilter == "NotReview")
            {
                var data = (from a in _db.tbl_SOR_Cur_Review_List
                            join b in _db.tbl_SOR_Cur_Review_Detail on a.SO_NO equals b.SO_NO
                            where (a.DOWNLOAD_DATE == b.DOWNLOAD_DATE && a.SO_NO == b.SO_NO 
                            && a.LINE == b.LINE && b.RESULT != "N/A"
                            && (a.PLAN_SHIP_DATE == null && a.TBD == null
                            && analyst.Contains(a.ANALYST) && a.REVIEW_STATUS != "Final Reviewed"
                            ))
                            select new ListSOItemReviewModel
                            {
                                SONO = a.SO_NO,
                                ItemReview = b.ITEM_REVIEW,
                                ReviewResultText = b.RESULT == null ? null : b.RESULT == "Y" ? "Y" : "N",
                                Comment = a.COMMENT,
                                Line = b.LINE,
                                DateDownLoad = a.DOWNLOAD_DATE,
                                PlanShipDate = a.PLAN_SHIP_DATE,
                                TBD = a.TBD == "TBD" ? true : false,
                                ID = a.REVIEW_ID,
                                Allcomment = b.COMMENT,
                                ResolutionOwner = a.ResolutionOwner,
                                SOHold = a.SO_ON_HOLD,
                                PROMISE_DATE = a.PROMISE_DATE,
                                DrawRevision = a.DR_REV,
                               LastBuild = a.LAST_BUILD_DR_REV,
                               LastWeeks = a.LAST_REVIEW_DR_REV,
                                BalanceQty = a.BLC_QTY,
                                BalanceValue = a.BLC_VALUE,
                                ShipToLocation = a.SHIP_TO,
                                NewSoReviewLW = a.NEW_REVIEW == true ? "Y" : "N",
                                FAI = a.FAI,
                                OrderQty = a.ORD_QTY,
                                RequiredDate = a.REQUIRED_DATE,
                                ITEM = a.ITEM,
                                Analyst = a.ANALYST,
                                Status = a.REVIEW_STATUS,
                                IsSubmit = b.ISSUBMIT
                                
                            }).ToList();
                var datasFinal = (from cc in data
                                  group cc by new
                                  {
                                      cc.SONO,
                                      cc.Line
                                  }
                             into myGroup
                                  select new ListSOItemReviewModel
                                  {
                                      ID = myGroup.Max(x => x.ID),
                                      SONO = myGroup.Key.SONO,
                                      Comment = myGroup.Max(x => x.Comment),
                                      SOHold = myGroup.Max(x => x.SOHold),
                                      DrawRevision = myGroup.Max(x => x.DrawRevision),
                                      Analyst = myGroup.Max(x => x.Analyst),
                                      LastBuild = myGroup.Max(x => x.LastBuild),
                                      LastWeeks = myGroup.Max(x => x.LastWeeks),
                                      Status = myGroup.Max(x => x.Status),
                                      BalanceQty = myGroup.Max(x => x.BalanceQty),
                                      BalanceValue = myGroup.Max(x => x.BalanceValue),
                                      ShipToLocation = myGroup.Max(x => x.ShipToLocation),
                                      NewSoReviewLW = myGroup.Max(x => x.NewSoReviewLW),
                                      PROMISE_DATE = myGroup.Max(x => x.PROMISE_DATE),
                                      FAI = myGroup.Max(x => x.FAI),
                                      OrderQty = myGroup.Max(x => x.OrderQty),
                                      RequiredDate = myGroup.Max(x => x.RequiredDate),
                                      ITEM = myGroup.Max(x => x.ITEM),
                                      Line = myGroup.Max(x => x.Line),
                                      DateDownLoad = myGroup.Max(x => x.DateDownLoad),
                                      PlanShipDate = myGroup.Max(x => x.PlanShipDate),
                                      TBD = myGroup.Max(x => x.TBD),
                                      ResolutionOwner = myGroup.Max(x => x.ResolutionOwner),
                                      #region list item Review
                                      CoCofRoHS = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CoCofRoHSComment = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Capacity = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CapacityComment = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      RawMaterial = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      RawMaterialComment = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Builtless = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      BuiltlessComment = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Carrier = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CarrierComment = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      ServiceTypeShipping = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)" && x.IsSubmit == true)
                                      .Max(x => x.ReviewResultText),
                                      ServiceTypeShippingComment = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)"
                                      && x.IsSubmit == true)
                                      .Max(x => x.Allcomment),
                                      Special = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      SpecialComment = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      AdditionalRequirementsReviewed = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      AdditionalRequirementsReviewedComment = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Drawing = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      DrawingComment = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Production = myGroup.Where(x => x.ItemReview.Trim() == "Production - Enough & usable Tool  Process capable /Enough machine / test equipment" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      ProductionComment = myGroup.Where(x => x.ItemReview.Trim() == "Production - Enough & usable Tool  Process capable /Enough machine / test equipment" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      #endregion
                                  }).Distinct().OrderBy(x => x.PROMISE_DATE).ToList();
                return datasFinal;
            }
            else if(isFilter == "All")
            {
                var data = (from a in _db.tbl_SOR_Cur_Review_List
                            join b in _db.tbl_SOR_Cur_Review_Detail on a.SO_NO equals b.SO_NO
                            where (a.DOWNLOAD_DATE == b.DOWNLOAD_DATE 
                            && a.SO_NO == b.SO_NO && a.LINE == b.LINE
                             && analyst.Contains(a.ANALYST) && a.REVIEW_STATUS != "Final Reviewed"
                            && b.RESULT != "N/A")
                            select new ListSOItemReviewModel
                            {
                                SONO = a.SO_NO,
                                ItemReview = b.ITEM_REVIEW,
                                ReviewResultText = b.RESULT == null ? null : b.RESULT == "Y" ? "Y" : "N",
                                Comment = a.COMMENT,
                                Line = b.LINE,
                                DateDownLoad = a.DOWNLOAD_DATE,
                                PlanShipDate = a.PLAN_SHIP_DATE,
                                TBD = a.TBD == "TBD" ? true : false,
                                ID = a.REVIEW_ID,
                                Allcomment = b.COMMENT,
                                ResolutionOwner = a.ResolutionOwner,
                                PROMISE_DATE = a.PROMISE_DATE,
                                SOHold = a.SO_ON_HOLD,
                                DrawRevision = a.DR_REV,
                                LastBuild = a.LAST_BUILD_DR_REV,
                                LastWeeks = a.LAST_REVIEW_DR_REV,
                                BalanceQty = a.BLC_QTY,
                                BalanceValue = a.BLC_VALUE,
                                ShipToLocation = a.SHIP_TO,
                                NewSoReviewLW = a.NEW_REVIEW == true ? "Y" : "N",
                                FAI = a.FAI,
                                OrderQty = a.ORD_QTY,
                                RequiredDate = a.REQUIRED_DATE,
                                ITEM = a.ITEM,
                                Analyst = a.ANALYST,
                                   Status = a.REVIEW_STATUS,
                                IsSubmit = b.ISSUBMIT
                            }).ToList();
                var datasFinal = (from cc in data
                                  group cc by new
                                  {
                                      cc.SONO,
                                      cc.Line
                                  }
                             into myGroup
                                  select new ListSOItemReviewModel
                                  {
                                      ID = myGroup.Max(x => x.ID),
                                      SONO = myGroup.Key.SONO,
                                      Comment = myGroup.Max(x => x.Comment),
                                      SOHold = myGroup.Max(x => x.SOHold),
                                      Analyst = myGroup.Max(x => x.Analyst),
                                      DrawRevision = myGroup.Max(x => x.DrawRevision),
                                      LastBuild = myGroup.Max(x => x.LastBuild),
                                      LastWeeks = myGroup.Max(x => x.LastWeeks),
                                      Status = myGroup.Max(x => x.Status),
                                      BalanceQty = myGroup.Max(x => x.BalanceQty),
                                      BalanceValue = myGroup.Max(x => x.BalanceValue),
                                      ShipToLocation = myGroup.Max(x => x.ShipToLocation),
                                      NewSoReviewLW = myGroup.Max(x => x.NewSoReviewLW),
                                      PROMISE_DATE = myGroup.Max(x => x.PROMISE_DATE),
                                      FAI = myGroup.Max(x => x.FAI),
                                      OrderQty = myGroup.Max(x => x.OrderQty),
                                      RequiredDate = myGroup.Max(x => x.RequiredDate),
                                      Line = myGroup.Max(x => x.Line),
                                      ITEM = myGroup.Max(x => x.ITEM),
                                      DateDownLoad = myGroup.Max(x => x.DateDownLoad),
                                      PlanShipDate = myGroup.Max(x => x.PlanShipDate),
                                      TBD = myGroup.Max(x => x.TBD),
                                      ResolutionOwner = myGroup.Max(x => x.ResolutionOwner),
                                      #region list item Review
                                      CoCofRoHS = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CoCofRoHSComment = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Capacity = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CapacityComment = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      RawMaterial = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      RawMaterialComment = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Builtless = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      BuiltlessComment = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Carrier = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CarrierComment = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      ServiceTypeShipping = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)" && x.IsSubmit == true)
                                      .Max(x => x.ReviewResultText),
                                      ServiceTypeShippingComment = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)"
                                      && x.IsSubmit == true)
                                      .Max(x => x.Allcomment),
                                      Special = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      SpecialComment = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      AdditionalRequirementsReviewed = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      AdditionalRequirementsReviewedComment = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Drawing = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      DrawingComment = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Production = myGroup.Where(x => x.ItemReview.Trim() == "Production - Enough & usable Tool  Process capable /Enough machine / test equipment" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      ProductionComment = myGroup.Where(x => x.ItemReview.Trim() == "Production - Enough & usable Tool  Process capable /Enough machine / test equipment" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      #endregion
                                  }).Distinct().OrderBy(x=>x.PROMISE_DATE).ToList();
                return datasFinal;
            }
            else 
            {
                var data = (from a in _db.tbl_SOR_Cur_Review_List
                            join b in _db.tbl_SOR_Cur_Review_Detail on a.SO_NO equals b.SO_NO
                            where (a.DOWNLOAD_DATE == b.DOWNLOAD_DATE && a.SO_NO == b.SO_NO 
                            && a.LINE == b.LINE && b.RESULT != "N/A" 
                            && analyst.Contains(a.ANALYST) && a.REVIEW_STATUS != "Final Reviewed"
                              && (a.PLAN_SHIP_DATE != null || a.TBD != null))
                          
                            select new ListSOItemReviewModel
                            {
                                SONO = a.SO_NO,
                                ItemReview = b.ITEM_REVIEW,
                                ReviewResultText = b.RESULT == null ? null : b.RESULT == "Y" ? "Y" : "N",
                                Comment = a.COMMENT,
                                Line = b.LINE,
                                DateDownLoad = a.DOWNLOAD_DATE,
                                PlanShipDate = a.PLAN_SHIP_DATE,
                                TBD = a.TBD == "TBD" ? true : false,
                                ID = a.REVIEW_ID,
                                Allcomment = b.COMMENT,
                                ResolutionOwner = a.ResolutionOwner,
                                SOHold = a.SO_ON_HOLD,
                                DrawRevision = a.DR_REV,
                                PROMISE_DATE = a.PROMISE_DATE,
                                LastBuild = a.LAST_BUILD_DR_REV,
                                LastWeeks = a.LAST_REVIEW_DR_REV,
                                BalanceQty = a.BLC_QTY,
                                BalanceValue = a.BLC_VALUE,
                                ShipToLocation = a.SHIP_TO,
                                NewSoReviewLW = a.NEW_REVIEW == true ? "Y" : "N",
                                FAI = a.FAI,
                                OrderQty = a.ORD_QTY,
                                RequiredDate = a.REQUIRED_DATE,
                                ITEM = a.ITEM,
                                Analyst = a.ANALYST,
                                Status = a.REVIEW_STATUS,
                                IsSubmit = b.ISSUBMIT
                            }).ToList();
                var datasFinal = (from cc in data
                                  group cc by new
                                  {
                                      cc.SONO,
                                      cc.Line
                                  }
                             into myGroup
                                  select new ListSOItemReviewModel
                                  {
                                      ID = myGroup.Max(x => x.ID),
                                      SONO = myGroup.Key.SONO,
                                      Comment = myGroup.Max(x => x.Comment),
                                      SOHold = myGroup.Max(x => x.SOHold),
                                      Analyst = myGroup.Max(x => x.Analyst),
                                      DrawRevision = myGroup.Max(x => x.DrawRevision),
                                      LastBuild = myGroup.Max(x => x.LastBuild),
                                      PROMISE_DATE = myGroup.Max(x => x.PROMISE_DATE),
                                      Status = myGroup.Max(x => x.Status),
                                      LastWeeks = myGroup.Max(x => x.LastWeeks),
                                      BalanceQty = myGroup.Max(x => x.BalanceQty),
                                      BalanceValue = myGroup.Max(x => x.BalanceValue),
                                      ShipToLocation = myGroup.Max(x => x.ShipToLocation),
                                      NewSoReviewLW = myGroup.Max(x => x.NewSoReviewLW),
                                      FAI = myGroup.Max(x => x.FAI),
                                      OrderQty = myGroup.Max(x => x.OrderQty),
                                      RequiredDate = myGroup.Max(x => x.RequiredDate),
                                      Line = myGroup.Max(x => x.Line),
                                      ITEM = myGroup.Max(x => x.ITEM),
                                      DateDownLoad = myGroup.Max(x => x.DateDownLoad),
                                      PlanShipDate = myGroup.Max(x => x.PlanShipDate),
                                      TBD = myGroup.Max(x => x.TBD),
                                      ResolutionOwner = myGroup.Max(x => x.ResolutionOwner),
                                      #region list item Review
                                      CoCofRoHS = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CoCofRoHSComment = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Capacity = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CapacityComment = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      RawMaterial = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      RawMaterialComment = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Builtless = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      BuiltlessComment = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Carrier = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CarrierComment = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      ServiceTypeShipping = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)" && x.IsSubmit == true)
                                      .Max(x => x.ReviewResultText),
                                      ServiceTypeShippingComment = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)"
                                      && x.IsSubmit == true)
                                      .Max(x => x.Allcomment),
                                      Special = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      SpecialComment = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      AdditionalRequirementsReviewed = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      AdditionalRequirementsReviewedComment = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Drawing = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      DrawingComment = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Production = myGroup.Where(x => x.ItemReview.Trim() == "Production - Enough & usable Tool  Process capable /Enough machine / test equipment" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      ProductionComment = myGroup.Where(x => x.ItemReview.Trim() == "Production - Enough & usable Tool  Process capable /Enough machine / test equipment" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      #endregion
                                  }).Distinct().OrderBy(x => x.PROMISE_DATE).ToList();
                return datasFinal;
            }
        }

        public List<ListSOItemReviewModel> GetListApproveSOReviewByPlanner(string depart, string isFilter, List<string> analyst)
        {
                var data = (from a in _db.tbl_SOR_Cur_Review_List
                            join b in _db.tbl_SOR_Cur_Review_Detail on a.SO_NO equals b.SO_NO
                            where (a.DOWNLOAD_DATE == b.DOWNLOAD_DATE && a.SO_NO == b.SO_NO
                            && a.LINE == b.LINE  )
                            select new ListSOItemReviewModel
                            {
                                SONO = a.SO_NO,
                                ItemReview = b.ITEM_REVIEW,
                                ReviewResultText = b.RESULT == null ? null : b.RESULT == "Y" ? "Y" : "N",
                                Comment = a.COMMENT,
                                Line = b.LINE,
                                DateDownLoad = a.DOWNLOAD_DATE,
                                PlanShipDate = a.PLAN_SHIP_DATE,
                                TBD = a.TBD == "TBD" ? true : false,
                                ID = a.REVIEW_ID,
                                Allcomment = b.COMMENT,
                                ResolutionOwner = a.ResolutionOwner,
                                SOHold = a.SO_ON_HOLD,
                                DrawRevision = a.DR_REV,
                                PROMISE_DATE = a.PROMISE_DATE,
                                LastBuild = a.LAST_BUILD_DR_REV,
                                LastWeeks = a.LAST_REVIEW_DR_REV,
                                BalanceQty = a.BLC_QTY,
                                BalanceValue =a.BLC_VALUE,
                                ShipToLocation = a.SHIP_TO,
                                NewSoReviewLW = a.NEW_REVIEW == true ? "Y" : "N",
                                FAI = a.FAI,
                                OrderQty = a.ORD_QTY,
                                RequiredDate = a.REQUIRED_DATE,
                                ITEM = a.ITEM,
                                Analyst = a.ANALYST,
                                Status = a.REVIEW_STATUS
                            }).ToList();
                var datasFinal = (from cc in data
                                  group cc by new
                                  {
                                      cc.SONO,
                                      cc.Line
                                  }
                             into myGroup
                                  select new ListSOItemReviewModel
                                  {
                                      ID = myGroup.Max(x => x.ID),
                                      SONO = myGroup.Key.SONO,
                                      Comment = myGroup.Max(x => x.Comment),
                                      SOHold = myGroup.Max(x => x.SOHold),
                                      Analyst = myGroup.Max(x => x.Analyst),
                                      DrawRevision = myGroup.Max(x => x.DrawRevision),
                                      LastBuild = myGroup.Max(x => x.LastBuild),
                                      PROMISE_DATE = myGroup.Max(x => x.PROMISE_DATE),
                                      Status = myGroup.Max(x => x.Status),
                                      LastWeeks = myGroup.Max(x => x.LastWeeks),
                                      BalanceQty = myGroup.Max(x => x.BalanceQty),
                                      BalanceValue = myGroup.Max(x => x.BalanceValue),
                                      ShipToLocation = myGroup.Max(x => x.ShipToLocation),
                                      NewSoReviewLW = myGroup.Max(x => x.NewSoReviewLW),
                                      FAI = myGroup.Max(x => x.FAI),
                                      OrderQty = myGroup.Max(x => x.OrderQty),
                                      RequiredDate = myGroup.Max(x => x.RequiredDate),
                                      Line = myGroup.Max(x => x.Line),
                                      ITEM = myGroup.Max(x => x.ITEM),
                                      DateDownLoad = myGroup.Max(x => x.DateDownLoad),
                                      PlanShipDate = myGroup.Max(x => x.PlanShipDate),
                                      TBD = myGroup.Max(x => x.TBD),
                                      ResolutionOwner = myGroup.Max(x => x.ResolutionOwner),
                                      #region list item Review
                                      CoCofRoHS = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CoCofRoHSComment = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true ).Max(x => x.Allcomment),
                                      Capacity = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CapacityComment = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      RawMaterial = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      RawMaterialComment = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Builtless = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      BuiltlessComment = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Carrier = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      CarrierComment = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      ServiceTypeShipping = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)" && x.IsSubmit == true)
                                      .Max(x => x.ReviewResultText),
                                      ServiceTypeShippingComment = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)"
                                      && x.IsSubmit == true)
                                      .Max(x => x.Allcomment),
                                      Special = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      SpecialComment = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      AdditionalRequirementsReviewed = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      AdditionalRequirementsReviewedComment = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      Drawing = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                      DrawingComment = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                      #endregion
                                  }).Distinct().OrderBy(x=>x.PROMISE_DATE).ToList();
            foreach (var item in datasFinal)
            {
                item.BalanceValue = Math.Round(item.BalanceValue.GetValueOrDefault(), 0, MidpointRounding.AwayFromZero);
            }
            return datasFinal;
        }
        public List<ListSOItemReviewModel> GetListApproveSOReviewByPlannerExport(string depart, string isFilter, List<string> analyst)
        {
            var data = (from a in _db.tbl_SOR_Cur_Review_List
                        join b in _db.tbl_SOR_Cur_Review_Detail on a.SO_NO equals b.SO_NO
                        where (a.DOWNLOAD_DATE == b.DOWNLOAD_DATE && a.SO_NO == b.SO_NO
                        && a.LINE == b.LINE)
                        select new ListSOItemReviewModel
                        {
                            SONO = a.SO_NO,
                            ItemReview = b.ITEM_REVIEW,
                            ReviewResultText = b.RESULT == null ? null : b.RESULT == "Y" ? "Y" : "N",
                            Comment = a.COMMENT,
                            Line = b.LINE,
                            DateDownLoad = a.DOWNLOAD_DATE,
                            PlanShipDate = a.PLAN_SHIP_DATE,
                            TBD = a.TBD == "TBD" ? true : false,
                            ID = a.REVIEW_ID,
                            Allcomment = b.COMMENT,
                            ResolutionOwner = a.ResolutionOwner,
                            SOHold = a.SO_ON_HOLD,
                            DrawRevision = a.DR_REV,
                            LastBuild = a.LAST_BUILD_DR_REV,
                            LastWeeks = a.LAST_REVIEW_DR_REV,
                            BalanceQty =a.BLC_QTY,
                            BalanceValue =a.BLC_VALUE,
                            ShipToLocation = a.SHIP_TO,
                            NewSoReviewLW = a.NEW_REVIEW == true ? "Y" : "N",
                            FAI = a.FAI,
                            OrderQty = a.ORD_QTY,
                            RequiredDate = a.REQUIRED_DATE,
                            ITEM = a.ITEM,
                            Analyst = a.ANALYST,
                            Status = a.REVIEW_STATUS,
                            PROMISE_DATE = a.PROMISE_DATE
                        }).ToList();
            var datasFinal = (from cc in data
                              group cc by new
                              {
                                  cc.SONO,
                                  cc.Line
                              }
                         into myGroup
                              select new ListSOItemReviewModel
                              {
                                  ID = myGroup.Max(x => x.ID),
                                  SONO = myGroup.Key.SONO,
                                  Comment = myGroup.Max(x => x.Comment),
                                  SOHold = myGroup.Max(x => x.SOHold),
                                  Analyst = myGroup.Max(x => x.Analyst),
                                  DrawRevision = myGroup.Max(x => x.DrawRevision),
                                  LastBuild = myGroup.Max(x => x.LastBuild),
                                  PROMISE_DATE = myGroup.Max(x => x.PROMISE_DATE),
                                  Status = myGroup.Max(x => x.Status),
                                  LastWeeks = myGroup.Max(x => x.LastWeeks),
                                  BalanceQty = myGroup.Max(x => x.BalanceQty),
                                  BalanceValue = myGroup.Max(x => x.BalanceValue),
                                  ShipToLocation = myGroup.Max(x => x.ShipToLocation),
                                  NewSoReviewLW = myGroup.Max(x => x.NewSoReviewLW),
                                  FAI = myGroup.Max(x => x.FAI),
                                  OrderQty = myGroup.Max(x => x.OrderQty),
                                  RequiredDate = myGroup.Max(x => x.RequiredDate),
                                  Line = myGroup.Max(x => x.Line),
                                  ITEM = myGroup.Max(x => x.ITEM),
                                  DateDownLoad = myGroup.Max(x => x.DateDownLoad),
                                  PlanShipDate = myGroup.Max(x => x.PlanShipDate),
                                  TBD = myGroup.Max(x => x.TBD),
                                  ResolutionOwner = myGroup.Max(x => x.ResolutionOwner),
                                  ResultExport = myGroup.Max(x => x.PlanShipDate) != null && myGroup.Max(x=>x.TBD ==false) ? myGroup.Max(x => x.PlanShipDate.GetValueOrDefault().ToString("dd-MMM-yy")) : "TBD",
                                  #region list item Review
                                  CoCofRoHS = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                  CoCofRoHSComment = myGroup.Where(x => x.ItemReview.Trim() == "CoC of RoHS, Reach" && x.IsSubmit == true).Max(x => x.Allcomment),
                                  Capacity = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                  CapacityComment = myGroup.Where(x => x.ItemReview.Trim() == "Capacity" && x.IsSubmit == true).Max(x => x.Allcomment),
                                  RawMaterial = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                  RawMaterialComment = myGroup.Where(x => x.ItemReview.Trim() == "Raw Material & consumable" && x.IsSubmit == true).Max(x => x.Allcomment),
                                  Builtless = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                  BuiltlessComment = myGroup.Where(x => x.ItemReview.Trim() == "Built less than 6 months" && x.IsSubmit == true).Max(x => x.Allcomment),
                                  Carrier = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                  CarrierComment = myGroup.Where(x => x.ItemReview.Trim() == "Carrier (Fedex, DHL, Schenker,…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                  ServiceTypeShipping = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)" && x.IsSubmit == true)
                                  .Max(x => x.ReviewResultText),
                                  ServiceTypeShippingComment = myGroup.Where(x => x.ItemReview.Trim() == "Service Type/Shipping method (IP, IE, Saver,.. Air/Sea,…)"
                                  && x.IsSubmit == true)
                                  .Max(x => x.Allcomment),
                                  Special = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                  SpecialComment = myGroup.Where(x => x.ItemReview.Trim() == "No Special request (BSO, IOR, COO…)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                  AdditionalRequirementsReviewed = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                  AdditionalRequirementsReviewedComment = myGroup.Where(x => x.ItemReview.Trim() == "Additional Requirements reviewed" && x.IsSubmit == true).Max(x => x.Allcomment),
                                  Drawing = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.ReviewResultText),
                                  DrawingComment = myGroup.Where(x => x.ItemReview.Trim() == "Drawing/ICD/BOM macthing, procedure available (FA&Rev changed)" && x.IsSubmit == true).Max(x => x.Allcomment),
                                  #endregion
                              }).Distinct().OrderBy(x => x.PROMISE_DATE).ToList();
            foreach (var item in datasFinal)
            {
                item.BalanceValueFormat = "$"+ Math.Round(item.BalanceValue.GetValueOrDefault(), 0, MidpointRounding.AwayFromZero).ToString();

            }
            return datasFinal;
        }
        #endregion
        #region Update Result Review

        public Result UpdateDataSoReviewResult(ListSOItemReviewModel picData, string idUser)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    if (picData.IsLock == true)
                    {
                        return new Result
                        {
                            success = false,
                            message = "Item review locked by Planner!",
                            obj = -1
                        };
                    }
                     //udpate result detail 
                    var data = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.ITEM_REVIEW_ID == picData.ID && ( x.ISSUBMIT == false || x.ISSUBMIT == null)).FirstOrDefault();
                    if (data != null)
                    {   if(picData.ReviewResult == true)
                        {
                            data.RESULT = "Y";
                        }
                        else
                        {
                            data.RESULT = "N";
                        }
                        data.COMMENT = picData.Comment;
                        data.REVIEW_BY = idUser;
                        data.REVIEW_AT = DateTime.Now;

                        //update status So reviewing
                        var soReview = _db.tbl_SOR_Cur_Review_List.Where(x => x.SO_NO.Trim() == picData.SONO.Trim() && x.DOWNLOAD_DATE == picData.DateDownLoad && x.LINE == picData.Line).FirstOrDefault();
                        if(soReview != null)
                        {
                            soReview.REVIEW_STATUS = "Reviewing";
                        }
                        _db.SaveChanges();

                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                        };
                    }
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }


        public Result SubmitDataSoReviewResult(ListSOItemReviewModel picData, string idUser)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    //udpate result detail 
                    var data = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.ITEM_REVIEW_ID == picData.ID && (x.ISSUBMIT == false || x.ISSUBMIT == null)).FirstOrDefault();
                    if (data != null)
                    {
                        if (picData.ReviewResult == true)
                        {
                            data.RESULT = "Y";
                        }
                        else
                        {
                            data.RESULT = "N";
                        }
                        data.COMMENT = picData.Comment;
                        data.REVIEW_BY = idUser;
                        data.REVIEW_AT = DateTime.Now;
                        data.ISSUBMIT = true;
                        _db.SaveChanges();
                        //update status So Review
                        bool isSubmitted = true;
                        bool isChangeStatus = true;
                        var commentAll = "";
                        var soReviewDetailList = _db.tbl_SOR_Cur_Review_Detail.Where(x => x.SO_NO.Trim() == picData.SONO.Trim() && x.DOWNLOAD_DATE == picData.DateDownLoad && x.LINE == picData.Line).ToList();
                        if (soReviewDetailList != null)
                        {
                            foreach (var item in soReviewDetailList)
                            {
                                if (item.RESULT == "N")
                                {
                                    isSubmitted = false;
                                }
                                else if(item.RESULT == null)
                                {
                                    isChangeStatus = false;
                                }
                                commentAll += item.COMMENT;
                            }
                        }
                        var soReview = _db.tbl_SOR_Cur_Review_List.Where(x => x.SO_NO.Trim() == picData.SONO.Trim() && x.DOWNLOAD_DATE == picData.DateDownLoad && x.LINE == picData.Line).FirstOrDefault();
                        if (isChangeStatus)
                        {
                            if (soReview != null)
                            {
                                if (isSubmitted)
                                {
                                    soReview.REVIEW_STATUS = "Reviewed";
                                    soReview.PLAN_SHIP_DATE = soReview.PROMISE_DATE;
                                    soReview.COMMENT = commentAll;
                                }
                                else
                                {
                                    soReview.REVIEW_STATUS = "Reviewed";
                                    soReview.COMMENT = commentAll;
                                }
                            }
                        }
                        else
                        {
                            soReview.REVIEW_STATUS = "Reviewing";
                        }
                        _db.SaveChanges();

                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                        };
                    }
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }

        public List<SelectListItem> GetDropdownlistSOreview()
        {
            List<SelectListItem> listuser = _db.tbl_SOR_Cur_Review_List.Where(x => x.REVIEW_STATUS != "Final Reviewed").Select(x => new SelectListItem
            {
                Value = x.SO_NO,
                Text = x.SO_NO.Trim(),
            }).Distinct().ToList();
            return listuser;
        }
        public List<SelectListItem> GetDropdownItembySOreview(string soNo, string line)
        {
            List<SelectListItem> listuser = (from a in _db.tbl_SOR_Cur_Review_List

                                             join v in _db.tbl_SOR_Cur_Review_Detail on a.SO_NO equals v.SO_NO 

                                             where(a.REVIEW_STATUS != "Approved" && a.SO_NO.Trim() == soNo.Trim() && a.LINE.Trim() == line && a.LINE == v.LINE)

                                             select(new SelectListItem

                                             {

                                                 Value = a.REVIEW_ID.ToString(),

                                                 Text = v.ITEM_REVIEW.Trim(),

                                             })).Distinct().ToList();

            return listuser;
        }
        public List<SelectListItem> GetDropdownLinebySOreview(string soNo)
        {
            List<SelectListItem> listuser = _db.tbl_SOR_Cur_Review_List.Where(x => x.REVIEW_STATUS != "Approved" && x.SO_NO.Trim() == soNo.Trim()).Select(x => new SelectListItem
            {
                Value = x.LINE.ToString(),
                Text = x.LINE.Trim(),
            }).Distinct().ToList();
            return listuser;
        }
        public string SORReviewPlanner()
        {
            var data = _db.SOR_Review_Planner();
            return "";
        }


        public List<TaskmanagementViewmodel> GetListTaskSoreview(DateTime date)
        {
            var result = from task in _db.TASKLISTs
                         join taskdetail in _db.TASKDETAILs on task.TopicID equals taskdetail.TopicID
                         join asp in _db.AspNetUsers on taskdetail.OWNER equals asp.Id
                         into joined
                         from j in joined.DefaultIfEmpty()
                         join asp1 in _db.AspNetUsers on taskdetail.ASSIGNEE equals asp1.Id
                          into joined1
                         from j1 in joined1.DefaultIfEmpty()
                         join asp2 in _db.AspNetUsers on taskdetail.APPROVE equals asp2.Id
                        into joined2
                         from j2 in joined2.DefaultIfEmpty()
                         where (task.TYPE == "SoReview")
                         select (new TaskmanagementViewmodel
                         {
                             RefNUMBER = task.Topic,
                             Taskname = taskdetail.TASKNAME,
                             TaskDescription = taskdetail.DESCRIPTION,
                             Owner = j.FullName,
                             Assignee = j1.FullName,
                             Approve = j2.FullName,
                             StartDay = taskdetail.EstimateStartDate,
                             DueDate = taskdetail.EstimateEndDate,
                             Status = taskdetail.STATUS,
                             ActualStarDay = taskdetail.ActualStartDate,
                             ActualEndDay = taskdetail.ActualEndDate,
                             Priority = taskdetail.PRIORITY,
                             Taskno = task.TopicID,
                             TaskDetailID = taskdetail.IDTask,
                             INSDATEs = taskdetail.CreatedDate
                         });

            return result.ToList();
        }

        public Result AddTaskForSoReview(string SoNo, string itemreview, string userID, string Assignee, string item, string taskname)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var currentTaskListID = 0;
                    var taskNO = SoNo + item;
                    var ckTaskList = _db.TASKLISTs.FirstOrDefault(x => x.Reference.Trim().Equals(taskNO.Trim()) && x.TYPE == "SoReview");
                    if (ckTaskList == null)
                    {
                        var TaskList = new TASKLIST
                        {
                            Topic = taskNO,
                            TYPE = "Soreview",
                            WRITEDATE = DateTime.Now,
                            WRITTENBY = userID,
                            Reference = taskNO,
                            Level = 1
                        };

                        _db.TASKLISTs.Add(TaskList);
                        _db.SaveChanges();

                        TASKDETAIL taskDetail = new TASKDETAIL
                        {
                            TopicID = TaskList.TopicID,
                            TASKNAME = itemreview,
                            DESCRIPTION = taskname,
                            OWNER = userID,
                            ASSIGNEE = Assignee,
                            APPROVE = userID,
                            EstimateStartDate = DateTime.Now,
                            EstimateEndDate = DateTime.Now.AddDays(7),
                            ActualStartDate = DateTime.Now,
                            ActualEndDate = DateTime.Now.AddDays(7),
                            CreatedDate = DateTime.Now,
                            STATUS = "Create",
                            Level = 1
                        };
                        _db.TASKDETAILs.Add(taskDetail);
                    }
                    else
                    {
                        TASKDETAIL taskDetail = new TASKDETAIL
                        {
                            TopicID = ckTaskList.TopicID,
                            TASKNAME = itemreview,
                            DESCRIPTION = taskname,
                            OWNER = userID,
                            ASSIGNEE = Assignee,
                            APPROVE = userID,
                            EstimateStartDate = DateTime.Now,
                            EstimateEndDate = DateTime.Now.AddDays(7),
                            ActualStartDate = DateTime.Now,
                            ActualEndDate = DateTime.Now.AddDays(7),
                            CreatedDate = DateTime.Now,
                            STATUS = "Create",
                            Level = 1
                        };
                        _db.TASKDETAILs.Add(taskDetail);

                        _db.SaveChanges();
                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                            message = "Create task success!",
                            obj = -1
                        };
                    }
                    return new Result
                    {
                        success = false,
                        message = "Task has created!",
                        obj = -1
                    };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }


        public Result UpdateDataPlannerSoReviewResult(ListSOItemReviewModel picData, string idUser)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var dataSoreview = _db.tbl_SOR_Cur_Review_List.Where(x => x.SO_NO.Trim() == picData.SONO.Trim() && x.DOWNLOAD_DATE == picData.DateDownLoad && x.LINE.Trim() == picData.Line.Trim()).FirstOrDefault();
                    if (dataSoreview != null && dataSoreview.REVIEW_STATUS != "Final Reviewed")
                    {
                        dataSoreview.ResolutionOwner = picData.ResolutionOwner;
                        if(picData.PlanShipDate != null && picData.TBD == false)
                        {
                            dataSoreview.PLAN_SHIP_DATE = picData.PlanShipDate;
                        }
                        else if(picData.TBD == true && picData.PlanShipDate == null)
                        {
                            dataSoreview.TBD = picData.TBD == true ? "TBD" : null;
                        }
                        dataSoreview.COMMENT = picData.Comment;
                        _db.SaveChanges();
                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                        };
                    }
                    else
                        return new Result
                        {
                            success = false,
                        };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }
        public Result SubmitDataPlannerSoReviewResult(ListSOItemReviewModel picData, string idUser)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var dataSoreview = _db.tbl_SOR_Cur_Review_List.Where(x => x.SO_NO.Trim() == picData.SONO.Trim() 
                    && x.DOWNLOAD_DATE == picData.DateDownLoad && x.LINE.Trim() == picData.Line.Trim() && x.REVIEW_STATUS == "Reviewed").FirstOrDefault();
                    if (dataSoreview != null)
                    {
                        dataSoreview.ResolutionOwner = picData.ResolutionOwner;
                        if (picData.PlanShipDate != null && picData.TBD == false)
                        {
                            dataSoreview.PLAN_SHIP_DATE = picData.PlanShipDate;
                        }
                        else if (picData.TBD == true && picData.PlanShipDate == null)
                        {
                            dataSoreview.TBD = picData.TBD == true ? "TBD" : null;
                        }
                        dataSoreview.REVIEW_STATUS = "Final Reviewed";
                        dataSoreview.COMMENT = picData.Comment;
                        _db.SaveChanges();
                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                        };
                    }
                    else
                        return new Result
                        {
                            success = false,
                        };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }
      public Result  ApproveDataPlannerSoReviewResult(ListSOItemReviewModel picData, string idUser)
        {
            var _log = new LogWriter("Updatedata");
            using (var tranj = _db.Database.BeginTransaction())
            {
                try
                {
                    var dataSoreview = _db.tbl_SOR_Cur_Review_List.Where(x => x.SO_NO.Trim() == picData.SONO.Trim()
                   && x.LINE.Trim() == picData.Line.Trim() && x.REVIEW_STATUS == "Final Reviewed").FirstOrDefault();
                    if (dataSoreview != null)
                    {
                        dataSoreview.REVIEW_STATUS = "Approved";
                        dataSoreview.APPROVEBY = idUser;
                        dataSoreview.APPROVEAT = DateTime.Now;
                        _db.SaveChanges();
                        tranj.Commit();
                        return new Result
                        {
                            success = true,
                        };
                    }
                    else
                        return new Result
                        {
                            success = false,
                        };
                }
                catch (Exception ex)
                {
                    tranj.Rollback();
                    _log.LogWrite(ex.ToString());
                    return new Result
                    {
                        success = false,
                        message = "Exception Updatedata!",
                        obj = -1
                    };
                }
            }
        }
        #endregion

        #region Task and File Management
        public List<TaskmanagementViewmodel> GetAllListTaskSoreview()
        {
            var result = from task in _db.TASKLISTs
                         join taskdetail in _db.TASKDETAILs on task.TopicID equals taskdetail.TopicID
                         join asp in _db.AspNetUsers on taskdetail.OWNER equals asp.Id
                         into joined
                         from j in joined.DefaultIfEmpty()
                         join asp1 in _db.AspNetUsers on taskdetail.ASSIGNEE equals asp1.Id
                          into joined1
                         from j1 in joined1.DefaultIfEmpty()
                         join asp2 in _db.AspNetUsers on taskdetail.APPROVE equals asp2.Id
                        into joined2
                         from j2 in joined2.DefaultIfEmpty()
                         where (task.TYPE == "SoReview")
                         select (new TaskmanagementViewmodel
                         {
                             RefNUMBER = task.Topic,
                             Taskname = taskdetail.TASKNAME,
                             TaskDescription = taskdetail.DESCRIPTION,
                             Owner = j.FullName,
                             Assignee = j1.FullName,
                             Approve = j2.FullName,
                             StartDay = taskdetail.EstimateStartDate,
                             DueDate = taskdetail.EstimateEndDate,
                             Status = taskdetail.STATUS,
                             ActualStarDay = taskdetail.ActualStartDate,
                             ActualEndDay = taskdetail.ActualEndDate,
                             Priority = taskdetail.PRIORITY,
                             Taskno = task.TopicID,
                             TaskDetailID = taskdetail.IDTask,
                             INSDATEs = taskdetail.CreatedDate
                         });

            return result.ToList();
        }
        #endregion
    }
}