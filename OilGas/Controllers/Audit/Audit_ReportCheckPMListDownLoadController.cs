using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportCheckPMListDownLoad", Name = "石油設施查核名單篩選", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportCheckPMListDownLoadController : AGenericModelController<vw_Audit_ReportCheckPMListDownLoad>
    {
        // GET: Audit_ReportCheckPMListDownLoad
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportCheckPMListDownLoad> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportCheckPMListDownLoad>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ReportCheckPMListDownLoad> GetDataDBObject(IModelEntity<vw_Audit_ReportCheckPMListDownLoad> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportCheckPMListDownLoad>().AsQueryable();
            }

            return base.GetDataDBObject(dbEntity, paras);
        }

        public ActionResult ExportAudit_ReportCheckPMListDownLoad(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_石油設施查核名單篩選);
            string fileTitle = "查核輔導專區_石油設施查核名單篩選";

            //解決資料查詢錯誤，但查詢數量為全部(非分頁數量)
            //不使用dou filter過濾資料(iquery)            
            //var result = iquery.AsEnumerable();
            List<string> titles = new List<string>() { "查核輔導專區_石油設施查核名單篩選，查詢條件:" };
            var result = GetOutputData(ref titles, paras);

            //預設排序            
            result = result.OrderBy(a => a.CaseNo);

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();
                        
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.加油站編號 = data.CaseNo;
                f.加油站站名 = data.Gas_Name;
                f.營業主體 = data.Business_theme;
                f.縣市別 = data.ZipCode;
                f.加油站地址 = data.Address;
                f.上次查核編號 = data.LastCheckNo;
                f.上次查核年度 = data.LastCheckYaer;
                f.上次查核等級 = data.Case_Lev;
                f.預計查核年度 = data.End_CheckYaer;
                f.上次查核缺失數 = data.Case_CheckErrCount;
                f.無非防爆性電氣設備或器具置於第一種場所或第二種場所範圍內使用 = data.CheckType1;                
                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>("陰井之油氣濃度未逾25%LEL", data.CheckType2));
                f.陰井之油氣濃度有測值 = data.CheckType3;
                f.加油機內無積水 = data.CheckType4;                
                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>("無積油及無油氣，設備接頭無滲漏、加油機及電氣接線孔配電管接頭、防爆軟管接頭無鬆脫，預留管口密封", data.CheckType5));
                f.電氣設備漏電斷路器試鈕作用正常 = data.CheckType6;
                f.洗車機漏電器測試正常 = data.CheckType7;
                f.洗車機緊急停止裝置測試正常 = data.CheckType8;
                f.上一年度新設之加油站 = data.CheckTypeN1;
                f.上一年度變更營業主體之加油站 = data.CheckTypeN2;
                f.上一年度復業之加油站 = data.CheckTypeN3;
                f.陰井之油氣濃度 = data.Well_LEL;
                f.加油站目前狀態 = data.uType;
                f.加油站目前狀態明細 = data.UsageState_Name;

                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF1(fileTitle, titles, list, folder, "N");
            string path = folder + fileName;
            url = OilGas.Cm.PhysicalToUrl(path);

            if (url == "")
            {
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);
            }
        }

        //重新計算查核等級(btn_send0)
        public ActionResult ResetSend0()
        {
            try
            {
                string sql = GetSqlResetSend0();
                using (var db = new OilGasModelContextExt())
                {
                    SqlParameter[] parameters = new SqlParameter[] { };
                    db.Database.ExecuteSqlCommand(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message + ex.StackTrace;
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        //重新計算高風險因子(btn_send1)
        public ActionResult ResetSend1()
        {
            try
            {
                string sql = GetSqlResetSend1();
                using (var db = new OilGasModelContextExt())
                {
                    SqlParameter[] parameters = new SqlParameter[] { };
                    db.Database.ExecuteSqlCommand(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message + ex.StackTrace;
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        //重新計算新設、變更及復業(btn_send2)
        public ActionResult ResetSend2()
        {
            try
            {
                string sql = GetSqlResetSend2();
                using (var db = new OilGasModelContextExt())
                {
                    SqlParameter[] parameters = new SqlParameter[] { };
                    db.Database.CommandTimeout = 2 * 60;//秒單位
                    db.Database.ExecuteSqlCommand(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message + ex.StackTrace;
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<CheckCaseList> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            Dou.Models.DB.IModelEntity<CheckCaseList> checkCaseList = new Dou.Models.DB.ModelEntity<CheckCaseList>(dbContext);

            //條件
            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var EndCheckYaer = KeyValue.GetFilterParaValue(paras, "EndCheckYaer");
            var CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            var CheckTypeT = KeyValue.GetFilterParaValue(paras, "CheckTypeT");
            var CheckTypeN = KeyValue.GetFilterParaValue(paras, "CheckTypeN");

            var iquery = checkCaseList.GetAll();

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysNames();
            iquery = iquery.Where(a => a.ZipCode != ""
                    && pCitys.Any(b => a.ZipCode == b));

            if (!string.IsNullOrEmpty(CaseType))
            {
                string str = CaseType;
                iquery = iquery.Where(a => a.CaseType == CaseType);

                titles.Add("石油設施類型:" + Code.GetCaseType().Where(a => a.Key == CaseType).FirstOrDefault().Value);
            }

            if (!string.IsNullOrEmpty(EndCheckYaer))
            {
                int num = int.Parse(EndCheckYaer);

                //DB:End_CheckYaer為string，無法用iquery轉int做 <= 年度
                //distinct()取年度，ToList()實體化，最後iquery用Contains取資料
                var strYears = checkCaseList.GetAll().Select(a => a.End_CheckYaer)
                                .Distinct().ToList()
                                .Where(a => int.Parse(a) <= num)
                                .OrderBy(a => a);

                iquery = iquery.Where(a => strYears.Contains(a.End_CheckYaer));

                titles.Add("年度查詢:" + num.ToString());
            }

            //條件            
            if (!string.IsNullOrEmpty(CityCode1))
            {
                var codes = CityCode1.Split(',').ToList();
                codes = Code.ConvertTwCity(codes);

                Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                var citys = cityCode.GetAll().Where(a => codes.Any(b => b == a.CityCode1));

                iquery = iquery.Where(a => a.ZipCode != ""
                    && citys.Any(b => a.ZipCode == b.CityName));

                //代碼-縣市別                                
                citys = citys.OrderBy(a => a.Rank);
                titles.Add("縣市:" + string.Join(",", citys.Select(a => a.CityName).ToList()));
            }

            if (!string.IsNullOrEmpty(CheckTypeT))
            {
                var sels = CheckTypeT.Split(',').ToList();                
                var codes = Code.GetPMListCheckTypeT();
                titles.Add("高風險因子篩選項目:" + string.Join(", ", codes.Where(a => sels.Any(b => b == a.Key.ToString())).Select(a => a.Value)));
            }

            if (!string.IsNullOrEmpty(CheckTypeN))
            {
                var sels = CheckTypeN.Split(',').ToList();
                var codes = Code.GetPMListCheckTypeN();
                titles.Add("新設、變更營業主體及復業:" + string.Join(",", codes.Where(a => sels.Any(b => b == a.Key.ToString())).Select(a => a.Value)));
            }

            return iquery;
        }

        /// <summary>
        /// 重新計算查核等級
        /// </summary>
        /// <returns></returns>
        private static string GetSqlResetSend0()
        {
            string sqlstr = "";

            //重新計算CheckCaseList
            sqlstr += @"delete CheckCaseList where 1=1 ";
            sqlstr += @"
                        ";

            //初次建立加油站清單
            //        sqlstr = @"insert into CheckCaseList (CaseNo,CaseType,Gas_Name,Business_theme,ZipCode,Address,LastCheckNo,LastCheckYaer,LastCheckNo2,LastCheckYaer2,uType,UsageState_Name)
            //                        Select f.CaseNo as CaseNo,
            //                            'CarFuel_BasicData' as CaseType,
            //                            f.Gas_Name as Gas_Name,
            //	                        Case when b.Name <> '其他' then b.Name else f.otherBusiness_theme End as Business_theme,
            //	                        f.ZipCode as ZipCode,
            //	                        case when f.Address is null then f.AddressNo else f.Address end as Address,
            //	                        (select top 1 CheckNo from Check_Basic where CaseNo=f.CaseNo order by CheckDate desc ) as LastCheckNo,
            //	                        (select top 1 year(CheckDate) from Check_Basic where CaseNo=f.CaseNo order by CheckDate desc) as LastCheckYaer,
            //	                        (select top 1 CheckNo from Check_Basic where CaseNo=f.CaseNo and CheckNo not in (select top 1 CheckNo from Check_Basic where CaseNo=f.CaseNo order by CheckDate desc) order by CheckDate desc ) as LastCheckNo2,
            //	                        (select top 1 year(CheckDate) from Check_Basic where CaseNo=f.CaseNo and CheckNo not in (select top 1 CheckNo from Check_Basic where CaseNo=f.CaseNo order by CheckDate desc) order by CheckDate desc) as LastCheckYaer2,
            //		                    u.Type as uType,u.ShortName [UsageState_Name]
            //                        From CarFuel_BasicData f with(nolock)
            //                        Left Join UsageStateCode u with(nolock) On f.UsageState = u.Value
            //                        Left Join CarVehicleGas_BusinessOrganization b with(nolock) on b.Value = f.Business_theme
            //		                where u.Type='已開業' and EXISTS (select * from Check_Basic where isnull(CaseNo,'')=isnull(f.CaseNo,'') )
            //                        and not EXISTS (select * from CheckCaseList where isnull(CaseNo,'')=isnull(f.CaseNo,'') ) ";
            sqlstr += @"insert into CheckCaseList (CaseNo,CaseType,Gas_Name,Business_theme,ZipCode,Address,LastCheckNo,LastCheckYaer,LastCheckNo2,LastCheckYaer2,uType,UsageState_Name)
                        Select f.CaseNo as CaseNo,
                            'CarFuel_BasicData' as CaseType,
                            f.Gas_Name as Gas_Name,
	                        Case when b.Name <> '其他' then b.Name else f.otherBusiness_theme End as Business_theme,
	                        f.ZipCode as ZipCode,
	                        case when f.Address is null then f.AddressNo else f.Address end as Address,
	                        (select top 1 CheckNo from Check_Basic where CaseNo=f.CaseNo order by CheckDate desc ) as LastCheckNo,
	                        (select top 1 year(CheckDate) from Check_Basic where CaseNo=f.CaseNo order by CheckDate desc) as LastCheckYaer,
	                        (select top 1 CheckNo from Check_Basic where CaseNo=f.CaseNo and CheckNo not in (select top 1 CheckNo from Check_Basic where CaseNo=f.CaseNo order by CheckDate desc) order by CheckDate desc ) as LastCheckNo2,
	                        (select top 1 year(CheckDate) from Check_Basic where CaseNo=f.CaseNo and CheckNo not in (select top 1 CheckNo from Check_Basic where CaseNo=f.CaseNo order by CheckDate desc) order by CheckDate desc) as LastCheckYaer2,
		                    u.Type as uType,u.ShortName [UsageState_Name]
                        From CarFuel_BasicData f with(nolock)
                        Left Join UsageStateCode u with(nolock) On f.UsageState = u.Value
                        Left Join CarVehicleGas_BusinessOrganization b with(nolock) on b.Value = f.Business_theme
		                where u.Type='已開業' and not EXISTS (select * from CheckCaseList where isnull(CaseNo,'')=isnull(f.CaseNo,'') ) ";
            sqlstr += @"
                        ";

            //更新地址與縣市資料
            sqlstr += @"update CheckCaseList set
	                    Address = (select case when Address is null then AddressNo else Address end as Address from CarFuel_BasicData where CaseNo=CheckCaseList.CaseNo) 
                   where Address is null ";
            sqlstr += @"
                        ";

            sqlstr += @"update CheckCaseList set
                        ZipCode = (select CityName from CityCode c where GSLCode LIKE '%' + SUBSTRING(CheckCaseList.CaseNo, 5, 2) + '%') 
                   where not EXISTS (select * from CityCode where CityName=CheckCaseList.ZipCode) ";
            sqlstr += @"
                        ";

            //找出上一次的查核紀錄
            //LastCheckNo2=(select top 1 CheckNo from Check_Basic where CaseNo=CheckCaseList.CaseNo and CheckNo not in (select top 1 CheckNo from Check_Basic where CaseNo=CheckCaseList.CaseNo order by CheckDate desc) order by CheckDate desc ) ,
            //LastCheckYaer2=(select top 1 year(CheckDate) from Check_Basic where CaseNo=CheckCaseList.CaseNo and CheckNo not in (select top 1 CheckNo from Check_Basic where CaseNo=CheckCaseList.CaseNo order by CheckDate desc) order by CheckDate desc),
            sqlstr += @"update CheckCaseList set
	                    LastCheckNo=(select top 1 CheckNo from Check_Basic where CaseNo=CheckCaseList.CaseNo order by CheckDate desc ) ,
	                    LastCheckYaer=(select top 1 year(CheckDate) from Check_Basic where CaseNo=CheckCaseList.CaseNo order by CheckDate desc)
                        where CaseNo is not null and EXISTS (select * from Check_Basic where CaseNo=CheckCaseList.CaseNo)";
            sqlstr += @"
                        ";

            //更新CheckCaseList 計算Check_Item_97中查核缺失數與危險因子
            sqlstr += @"update CheckCaseList set
	                    Case_CheckErrCount=(select top 1 AllDoesmeet from Check_Item_97 where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)
                        where LastCheckNo is not null and EXISTS (select * from Check_Item_97 where CheckNo=CheckCaseList.LastCheckNo)";
            sqlstr += @"
                        ";

            //更新CheckCaseList 計算Check_Item中查核缺失數與危險因子
            sqlstr += @"update CheckCaseList set
	                    Case_CheckErrCount=(select top 1 AllDoesmeet from Check_Item where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)
                        where LastCheckNo is not null and EXISTS (select * from Check_Item where CheckNo=CheckCaseList.LastCheckNo)";
            sqlstr += @"
                        ";

            //更新CheckCaseList 區分上一次查核後的查核等級與下一次查核年限
            sqlstr += string.Format(@"update CheckCaseList set
	                    Case_Lev=case when Case_CheckErrCount = 0 then 'A' 
                            when Case_CheckErrCount >= 1 and Case_CheckErrCount <= 2 then 'B'
			                when Case_CheckErrCount >= 3 and Case_CheckErrCount <= 5 then 'C'
			                when Case_CheckErrCount >= 6 and Case_CheckErrCount <= 9 then 'D'
			                when Case_CheckErrCount >= 10 then 'E'
			                when Case_CheckErrCount is null then 'N'
                        end, 
	                    End_CheckYaer=case when Case_CheckErrCount = 0 then CONVERT(varchar,isnull(CONVERT(int,LastCheckYaer),{0}) + 5)
                            when Case_CheckErrCount >= 1 and Case_CheckErrCount <= 2 then CONVERT(varchar,isnull(CONVERT(int,LastCheckYaer),{0}) + 4)
			                when Case_CheckErrCount >= 3 and Case_CheckErrCount <= 5 then CONVERT(varchar,isnull(CONVERT(int,LastCheckYaer),{0}) + 3)
			                when Case_CheckErrCount >= 6 and Case_CheckErrCount <= 9 then CONVERT(varchar,isnull(CONVERT(int,LastCheckYaer),{0}) + 2)
			                when Case_CheckErrCount >= 10 or Case_CheckErrCount is null then CONVERT(varchar,isnull(CONVERT(int,LastCheckYaer),{0}) + 1)
                        end
                        ", DateTime.Now.AddYears(-1).Year.ToString());
            sqlstr += @"
                        ";

            return sqlstr;
        }

        /// <summary>
        /// 重新計算高風險因子
        /// </summary>
        /// <returns></returns>
        private static string GetSqlResetSend1()
        {
            string sqlstr = "";

            //第一次更新CheckCaseList   更新加油站基本資料
            //LastCheckNo2=(select top 1 CheckNo from Check_Basic where CaseNo=CheckCaseList.CaseNo and CheckNo not in (select top 1 CheckNo from Check_Basic where CaseNo=CheckCaseList.CaseNo order by CheckDate desc) order by CheckDate desc ) ,
            //LastCheckYaer2=(select top 1 year(CheckDate) from Check_Basic where CaseNo=CheckCaseList.CaseNo and CheckNo not in (select top 1 CheckNo from Check_Basic where CaseNo=CheckCaseList.CaseNo order by CheckDate desc) order by CheckDate desc),
            sqlstr += @"update CheckCaseList set
	                    Business_theme = (select Case when b.Name <> '其他' then b.Name else f.otherBusiness_theme End as Business_theme from CarFuel_BasicData f Left Join CarVehicleGas_BusinessOrganization b with(nolock) on b.Value = f.Business_theme where CaseNo=CheckCaseList.CaseNo),
	                    uType = (select (select top 1 Type from UsageStateCode where Value =f.UsageState) from CarFuel_BasicData f where CaseNo=CheckCaseList.CaseNo),
	                    UsageState_Name = (select (select top 1 ShortName from UsageStateCode where Value =f.UsageState) from CarFuel_BasicData f where CaseNo=CheckCaseList.CaseNo)
                        where CaseNo is not null and EXISTS (select * from CarFuel_BasicData where CaseNo=CheckCaseList.CaseNo)";
            sqlstr += @"
                        ";

            //第二次更新CheckCaseList 計算Check_Item_97中查核缺失數與危險因子
            sqlstr += @"update CheckCaseList set
                        CheckType1=case when (select top 1 isnull(B04,0) from Check_Item_97 where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
	                    CheckType4=case when (select top 1 isnull(D07,0) from Check_Item_97 where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
                        CheckType5=case when (select top 1 isnull(D10,0) from Check_Item_97 where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
	                    CheckType6=case when (select top 1 isnull(B06,0) from Check_Item_97 where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
	                    CheckType7=case when (select top 1 isnull(K03,0) from Check_Item_97 where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
	                    CheckType8=case when (select top 1 isnull(K04,0) from Check_Item_97 where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end
                        where LastCheckNo is not null and EXISTS (select * from Check_Item_97 where CheckNo=CheckCaseList.LastCheckNo)";
            sqlstr += @"
                        ";

            //第三次更新CheckCaseList 計算Check_Item中查核缺失數與危險因子
            sqlstr += @"update CheckCaseList set
                        CheckType1=case when (select top 1 isnull(B04,0) from Check_Item where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
	                    CheckType4=case when (select top 1 isnull(D07,0) from Check_Item where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
                        CheckType5=case when (select top 1 isnull(D10,0) from Check_Item where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
	                    CheckType6=case when (select top 1 isnull(B06,0) from Check_Item where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
	                    CheckType7=case when (select top 1 isnull(J02,0) from Check_Item where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end,
	                    CheckType8=case when (select top 1 isnull(J03,0) from Check_Item where CheckNo=CheckCaseList.LastCheckNo order by CheckDate desc)=2 then 1 else 0 end
                        where LastCheckNo is not null and EXISTS (select * from Check_Item where CheckNo=CheckCaseList.LastCheckNo)";
            sqlstr += @"
                        ";

            //第四次更新CheckCaseList 取回Check_Tank_well中陰井相關資料
            sqlstr += string.Format(@"update CheckCaseList set
	                    CheckType2=case when (select max(Detection) from Check_Tank_well where CheckNo=CheckCaseList.LastCheckNo) >= 25 then 1 else 0 end,
                        CheckType3=case when (select max(Detection) from Check_Tank_well where CheckNo=CheckCaseList.LastCheckNo) > 0 then 1 else 0 end,
                        Well_LEL=(select max(Detection) from Check_Tank_well where CheckNo=CheckCaseList.LastCheckNo) 
                        where LastCheckNo is not null and EXISTS (select * from Check_Basic where CheckNo=CheckCaseList.LastCheckNo) ");
            sqlstr += @"
                        ";

            return sqlstr;
        }

        /// <summary>
        /// 重新計算新設、變更及復業
        /// </summary>
        /// <returns></returns>
        private static string GetSqlResetSend2()
        {
            string sqlstr = "";

            //更新是否前一年為前一年度新設之加油站
            sqlstr += string.Format(@"update CheckCaseList set
                CheckTypeN1 = (select count(*) from CarFuel_Dispatch where CaseNo=CheckCaseList.CaseNo 
                                  and DispatchClass in('17') and Year(Dispatch_date)='{0}' )", DateTime.Now.AddYears(-1).Year.ToString());
            sqlstr += @"
                        ";

            //變更營業主體
            sqlstr += string.Format(@"update CheckCaseList set
                CheckTypeN2 = (select count(*) from RecordLog where CaseNo=CheckCaseList.CaseNo 
                                  and recordData like '%營業主體%' and recordData not like '%新增%' and recordData like '%從%' and year(Mod_date) = '{0}' )", DateTime.Now.AddYears(-1).Year.ToString());
            sqlstr += @"
                        ";

            //復業
            sqlstr += string.Format(@"update CheckCaseList set
                CheckTypeN3 = (select count(*) from CarFuel_Dispatch where CaseNo=CheckCaseList.CaseNo 
                                  and DispatchClass in('54') and Year(Dispatch_date)='{0}' )", DateTime.Now.AddYears(-1).Year.ToString());
            sqlstr += @"
                        ";

            return sqlstr;
        }
    }

    public class vw_Audit_ReportCheckPMListDownLoad
    {       
        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectItems = "{\"CarFuel_BasicData\":\"汽/機車加油站\"}")]
        public string CaseType { get; }

        [Display(Name = "年度查詢", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.EndCheckYaerSelectItems.AssemblyQualifiedName)]
        public string EndCheckYaer { get; }

        [Display(Name = "縣市別", Order = 3)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        public string CityCode1 { get; set; }

        [Display(Name = "高風險因子篩選項目", Order = 4)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = GetPMListCheckTypeTSelectItems.AssemblyQualifiedName)]
        public string CheckTypeT { get; set; }

        [Display(Name = "新設、變更營業主體及復業", Order = 5)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = GetPMListCheckTypeNSelectItems.AssemblyQualifiedName)]
        public string CheckTypeN { get; set; }
    }
}