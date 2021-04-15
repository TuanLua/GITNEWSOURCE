
use Assembly

DECLARE @Line NVARCHAR(50) ='High Mix Volume',
@sogiolamviec float = 7

SELECT a.StepID,a.Rev,Number,StepName,RSq,SubGroup INTO #InOutstepID FROM Step a --WHERE InOut = 1;  --INNER JOIN @tbStep b ON a.StepID = b.StepID --where [Group] LIKE @line --AND Inout = 1 --AND StepID <> '35'

DECLARE @tbWO TABLE (WO NVARCHAR(50),Product nvarchar(50),Revision nvarchar(50),PCP nvarchar(50),Quantity DECIMAL(13,0),IssueDate Datetime,DueDate Datetime,PromisedDate Datetime,OffDate INT,ID INT)
INSERT INTO @tbWO
SELECT WOCode,ProductID,a.Revision,a.PCP,CASE WHEN Unists = 'PCS' THEN Quantity ELSE Quantity*NOE END,a.IssueDate,a.DueDate,a.PromisedDate,(SELECT COUNT(*) FROM tbOffDate WHERE OffDate BETWEEN IssueDate AND PromisedDate),a.IDBarcode FROM dbo.tbWO a INNER JOIN dbo.tbProduct b
ON a.PCP = b.PCP AND a.Revision = b.Revision AND a.ProductID = b.ID
WHERE Status = 'Open' --AND b.[Group] = @Line

-----Danh Sach WO đang Open
SELECT * FROM @tbWO


-----Routing Traveler gốc của các WO----------------------------------------------------------------------------------
SELECT b.WO,b.Product,b.Quantity,ROW_NUMBER() OVER(PARTITION BY b.WO ORDER BY a.ID) Sq,a.StepID,c.Number,c.StepName,c.SubGroup INTO #routingOrg FROM tbStep_Product a INNER JOIN @tbWO b ON a.ProductID = b.Product AND a.Revision = b.Revision AND a.PCP = b.PCP
INNER JOIN #InOutstepID c ON a.StepID = c.StepID AND a.Rev = c.Rev
ORDER by b.WO,a.ID

SELECT a.Date,GroupID ID,GroupName SubGroup,Target STD,a.Part INTO #std  FROM dbo.tb_Setting_RealTime_ProcessGroup a INNER JOIN 
                                    (SELECT MAX(Date) Date,Part,Line FROM dbo.tb_Setting_RealTime_ProcessGroup GROUP BY Part,Line)b ON a.Part = b.Part AND a.Date = b.Date AND a.Line=b.Line
INNER JOIN dbo.tbGroup ON a.GroupID = dbo.tbGroup.ID --WHERE a.Line LIKE @line

SELECT a.*,ISNULL(b.STD,0) STD,a.Quantity*ISNULL(b.STD,0)*60 TotalMinute INTO #raw FROM #routingOrg a LEFT JOIN #std b ON a.SubGroup = b.SubGroup AND a.Product = b.Part
ORDER BY WO,Sq

SELECT WO,SUM(TotalMinute) TotalTime INTO #temp1 FROM #raw GROUP BY WO

-----Số operator theo từng group step
SELECT WO,SubGroup,Quantity,STD,@sogiolamviec,SUM(TotalMinute)/(@sogiolamviec*60) OPNeed FROM #raw GROUP BY WO,SubGroup,Quantity,STD ORDER BY WO

-----Số operator theo từng ca ( có 1 WO làm ngay nghỉ tết 7016080_4)
SELECT a.WO,a.Quantity,a.STD,b.IssueDate,b.PromisedDate,b.OffDate,a.TotalHours/(b.TotalDays*@sogiolamviec) OPNeed FROM (SELECT WO,Quantity,STD,SUM(TotalMinute)/60 TotalHours FROM #raw GROUP BY WO,Quantity,STD) a
INNER JOIN (SELECT WO,IssueDate,PromisedDate,OffDate,CASE WHEN SUM(DATEDIFF(DAY,IssueDate,PromisedDate)-OffDate)+1 = 0 THEN 1 ELSE SUM(DATEDIFF(DAY,IssueDate,PromisedDate)-OffDate)+1 END TotalDays FROM @tbWO GROUP BY WO,IssueDate,PromisedDate,OffDate) b
ON a.WO  = b.WO
ORDER BY WO

-------Đến trước làm trước
SELECT ROW_NUMBER() OVER(ORDER BY IssueDate,ID) RowNumber,* FROM @tbWO

-------Thời gian gia công ngắn nhất
SELECT ROW_NUMBER() OVER(ORDER BY TotalTime,ID) RowNumber,a.*,TotalTime FROM @tbWO a INNER JOIN #temp1 b ON a.WO = b.WO

-------Thời gian giao hàng sớm nhất
SELECT ROW_NUMBER() OVER(ORDER BY PromisedDate,ID) RowNumber,* FROM @tbWO


DROP TABLE #InOutstepID
DROP TABLE #routingOrg
DROP TABLE #std
DROP TABLE #raw
DROP TABLE #temp1

