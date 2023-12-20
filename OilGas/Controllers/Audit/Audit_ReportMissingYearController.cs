using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportMissingYear", Name = "年度及查核缺失項目查詢符合該條件之油氣設施名單", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 11, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportMissingYearController : AGenericModelController<vw_Audit_ReportMissingYear>
    {
        // GET: Audit_ReportMissingYear
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportMissingYear> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportMissingYear>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ReportMissingYear> GetDataDBObject(IModelEntity<vw_Audit_ReportMissingYear> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportMissingYear>().AsQueryable();
            }

            List<string> titles = new List<string>();
            var result = GetOutputData(ref titles, paras);

            KeyValueParams ksort = paras.FirstOrDefault((KeyValueParams s) => s.key == "sort");
            KeyValueParams korder = paras.FirstOrDefault((KeyValueParams s) => s.key == "order");
            //分頁排序
            if (ksort.value != null && korder.value != null)
            {
                string sort = ksort.value.ToString();
                string order = korder.value.ToString();

                if (ksort.value.ToString() == "CheckYear")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckYear);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckYear);
                    }
                }
                else if (ksort.value.ToString() == "CheckAllCount")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckAllCount);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckAllCount);
                    }
                }
                else if (ksort.value.ToString() == "CheckHiatusCount")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckHiatusCount);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckHiatusCount);
                    }
                }
                else if (ksort.value.ToString() == "CheckHiatusRate")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckHiatusRate);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckHiatusRate);
                    }
                }
                else if (ksort.value.ToString() == "ItemHiatusAllRate")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.ItemHiatusAllRate);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.ItemHiatusAllRate);
                    }
                }
            }
            else
            {
                //預設排序                
                result = result.OrderBy(a => a.CheckYear);
            }

            return result;
        }

        public ActionResult ExportAudit_ReportMissingYear(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_年度及查核缺失項目查詢符合該條件之油氣設施名單);
            string fileTitle = "查核輔導專區_年度及查核缺失項目查詢符合該條件之油氣設施名單";

            List<string> titles = new List<string>() { "年度及查核缺失項目查詢符合該條件之油氣設施名單，查詢條件:" }; ;
            var result = GetOutputData(ref titles, paras);

            //預設排序                
            result = result.OrderBy(a => a.CheckYear);

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();

            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.年度 = data.CheckYear;
                f.查核家數 = data.CheckAllCount;
                f.該項目有缺失家數 = data.CheckHiatusCount;
                f.缺失發生比例 = data.CheckHiatusRate + "%";
                f.佔總缺失比例 = data.ItemHiatusAllRate + "%";

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

        //查核項目
        public ActionResult GetCheckItemList(string caseType)
        {
            var f = Code.GetCaseType2().Where(a => a.Key == caseType);

            List<CheckItemList> itemList = null;
            if (f.Count() == 0)
            {
                return Json(new { result = false, errorMessage = caseType + "查無對應CheckItemTable" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string text = f.FirstOrDefault().Value.ToString();
                JObject json = JObject.Parse(text);

                var otable = json["CheckItemTable"];
                if (otable == null)
                {
                    return Json(new { result = false, errorMessage = "json無欄位：" + "CheckItemTable" }, JsonRequestBehavior.AllowGet);
                }

                string table = otable.ToString();                
                itemList = CheckItemList.GetAllDatas().Where(a => a.CheckItemTable == table)
                                        .Select(a => new 
                                        {
                                            CheckItemTitelNo = a.CheckItemTitelNo,
                                            CheckItemTitel = a.CheckItemTitel,
                                            CheckItemTitelSum = a.CheckItemTitelSum
                                        }).Distinct()
                                        .OrderBy(a => a.CheckItemTitelNo).ThenBy(a => a.CheckItemTitelSum).ThenBy(a => a.CheckItemTitel)
                                        .ToList()
                                        .Select(a => new CheckItemList
                                        {
                                            CheckItemTitel = a.CheckItemTitel,
                                            CheckItemTitelSum = a.CheckItemTitelSum
                                        }).ToList();
            }

            if (itemList == null)
            {
                return Json(new { result = false, errorMessage = caseType + "對應錯誤" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true, itemList = itemList }, JsonRequestBehavior.AllowGet);
        }

        //查核細項
        public ActionResult GetCheckItemDetail(string caseType, string checkItemTitelSum)
        {
            var f = Code.GetCaseType2().Where(a => a.Key == caseType);

            List<CheckItemList> itemDetail = null;
            if (f.Count() == 0)
            {
                return Json(new { result = false, errorMessage = caseType + "查無對應CheckItemTable" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string text = f.FirstOrDefault().Value.ToString();
                JObject json = JObject.Parse(text);

                var otable = json["CheckItemTable"];
                if (otable == null)
                {
                    return Json(new { result = false, errorMessage = "json無欄位：" + "CheckItemTable" }, JsonRequestBehavior.AllowGet);
                }

                string table = otable.ToString();              
                itemDetail = CheckItemList.GetAllDatas().Where(a => a.CheckItemTable == table && a.CheckItemTitelSum == checkItemTitelSum)                                        
                                        .Select(a => new
                                        {                                            
                                            CheckItemDescNo = a.CheckItemDescNo,
                                            CheckItemDesc = a.CheckItemDesc,                                            
                                        }).Distinct()
                                        .OrderBy(a => a.CheckItemDescNo).ThenBy(a => a.CheckItemDesc)
                                        .ToList()
                                        .Select(a => new CheckItemList
                                        {
                                            CheckItemDescNo = a.CheckItemDescNo,
                                            CheckItemDesc = a.CheckItemDesc
                                        }).ToList();
            }

            if (itemDetail == null)
            {
                return Json(new { result = false, errorMessage = caseType + "對應錯誤" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true, itemDetail = itemDetail }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<vw_Audit_ReportMissingYear> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            Dou.Models.DB.IModelEntity<Check_Basic_View> check_Basic_View = new Dou.Models.DB.ModelEntity<Check_Basic_View>(dbContext);

            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var CheckItemTitel = KeyValue.GetFilterParaValue(paras, "CheckItemTitel");
            var CheckItemDescNo = KeyValue.GetFilterParaValue(paras, "CheckItemDescNo");
            var PWorkYear = KeyValue.GetFilterParaValue(paras, "WorkYear");

            var iquery = check_Basic_View.GetAll();

            string oriCaseType = "";
            if (CaseType == "Check_Item")
            {
                titles.Add("石油設施類型:" + "汽/機車加油站");
                oriCaseType = "CarFuel_BasicData";
            }
            else if (CaseType == "Check_Item_Fish")
            {
                titles.Add("石油設施類型:" + "漁船加油站");
                oriCaseType = "FishGas_BasicData";
            }
            else if (CaseType == "Check_Item_SelfUP")
            {
                titles.Add("石油設施類型:" + "自用加儲油設施(地上)");
                oriCaseType = "SelfFuel_Basic";
                //and (Tank_Well !='1' and Tank_Well !='是')
                iquery = iquery.Where(a => a.Tank_Well != "1" && a.Tank_Well != "是");
            }
            else if (CaseType == "Check_Item_SelfDown")
            {
                titles.Add("石油設施類型:" + "自用加儲油設施(地下)");
                oriCaseType = "SelfFuel_Basic";
                //and (Tank_Well='1' or Tank_Well='是')
                iquery = iquery.Where(a => a.Tank_Well == "1" || a.Tank_Well == "是");
            }

            iquery = iquery.Where(a => a.CaseType == oriCaseType);

            if (!string.IsNullOrEmpty(PWorkYear))
            {
                int num = int.Parse(PWorkYear);
                iquery = iquery.Where(a => a.CheckDate.Year - 1911 == num);

                titles.Add("查核年度:" + num.ToString());
            }

            //母體 source
            var source = iquery.Select(a => new
            {
                CheckYear = a.CheckDate.Year - 1911,
                a.AllDoesmeet,
            })
            .GroupBy(a => a.CheckYear)
            .Select(a => new
            {
                CheckYear = a.Key,
                CheckAllCount = a.Count(),
                TotalHiatusCount = a.Sum(p => p.AllDoesmeet == null ? 0 : (int)p.AllDoesmeet),
            });

            //該項目有缺失家數 母體各年度(source)	動態資料表(check_Item...)
            List<Check2Class> tmp_CheckHiatusCount = new List<Check2Class>();
            var workYears = source.Select(a => a.CheckYear).OrderBy(a => a).ToList();
            CheckItemList t1 = null;    //條件文字
            CheckItemList t2 = null;    //條件文字
            foreach (var WorkYear in workYears)
            {
                string workTable = Code.GetWorkTable(CaseType, WorkYear);

                IQueryable<OilGas.Check_Item_Report> tmp = null;
                if (workTable == "Check_Item")
                {
                    Dou.Models.DB.IModelEntity<Check_Item> check_Item = new Dou.Models.DB.ModelEntity<Check_Item>(dbContext);

                    tmp = check_Item.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {                                
                                CheckYear = c.CheckDate == null ? 0 : ((DateTime)c.CheckDate).Year - 1911,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet,
                                A_Doesmeet = o.A_Doesmeet, B_Doesmeet = o.B_Doesmeet, C_Doesmeet = o.C_Doesmeet, D_Doesmeet = o.D_Doesmeet, E_Doesmeet = o.E_Doesmeet, F_Doesmeet = o.F_Doesmeet, G_Doesmeet = o.G_Doesmeet, H_Doesmeet = o.H_Doesmeet, I_Doesmeet = o.I_Doesmeet, J_Doesmeet = o.J_Doesmeet, K_Doesmeet = o.K_Doesmeet, L_Doesmeet = o.L_Doesmeet, M_Doesmeet = o.M_Doesmeet,
                                A01 = o.A01, A02 = o.A02, A03 = o.A03, A04 = o.A04, A05 = o.A05, A06 = o.A06, B01 = o.B01, B02 = o.B02, B03 = o.B03, B04 = o.B04, B05 = o.B05, B06 = o.B06, B07 = o.B07, B08 = o.B08, B09 = o.B09, B10 = o.B10, C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = o.C04, C05 = o.C05, C06 = o.C06, C07 = o.C07, C08 = o.C08, C09 = o.C09, C10 = o.C10, C11 = o.C11, C12 = o.C12, C13 = o.C13, C14 = o.C14, D01 = o.D01, D02 = o.D02, D03 = o.D03, D04 = o.D04, D05 = o.D05, D06 = o.D06, D07 = o.D07, D08 = o.D08, D09 = o.D09, D10 = o.D10, D11 = o.D11, E01 = o.E01, E02 = o.E02, E03 = o.E03, F01 = o.F01, F02 = o.F02, F03 = o.F03, F04 = o.F04, F05 = o.F05, F06 = o.F06, F07 = o.F07, F08 = o.F08, F09 = o.F09, G01 = o.G01, G02 = o.G02, G03 = o.G03, G04 = o.G04, G05 = o.G05, G06 = o.G06, H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = o.H04, H05 = o.H05, I01 = o.I01, I02 = o.I02, I03 = o.I03, I04 = o.I04, I05 = o.I05, I06 = o.I06, I07 = o.I07, I08 = o.I08, I09 = o.I09, I10 = o.I10, J01 = o.J01, J02 = o.J02, J03 = o.J03, K01 = o.K01, K02 = o.K02, L01 = o.L01, L02 = o.L02, L03 = o.L03
                            });   
                }
                else if (workTable == "Check_Item_97")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_97> check_Item_97 = new Dou.Models.DB.ModelEntity<Check_Item_97>(dbContext);

                    tmp = check_Item_97.GetAll().Where(a => a.CheckNo != null).Where(a => a.CheckDate != null)  //96年，有重複1筆CheckDate=null(H-96006)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {                                
                                CheckYear = c.CheckDate == null ? 0 : ((DateTime)c.CheckDate).Year - 1911,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet,
                                A_Doesmeet = o.A_Doesmeet, B_Doesmeet = o.B_Doesmeet, C_Doesmeet = o.C_Doesmeet, D_Doesmeet = o.D_Doesmeet, E_Doesmeet = o.E_Doesmeet, F_Doesmeet = o.F_Doesmeet, G_Doesmeet = o.G_Doesmeet, H_Doesmeet = o.H_Doesmeet, I_Doesmeet = o.I_Doesmeet, J_Doesmeet = o.J_Doesmeet, K_Doesmeet = o.K_Doesmeet, L_Doesmeet = o.L_Doesmeet, M_Doesmeet = o.M_Doesmeet,
                                A01 = o.A01, A02 = o.A02, A03 = o.A03, A04 = o.A04, A05 = o.A05, A06 = o.A06, B01 = o.B01, B02 = o.B02, B03 = o.B03, B04 = o.B04, B05 = o.B05, B06 = o.B06, B07 = o.B07, B08 = o.B08, B09 = o.B09, B10 = o.B10, C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = o.C04, C05 = o.C05, C06 = o.C06, C07 = o.C07, C08 = o.C08, C09 = o.C09, C10 = o.C10, C11 = o.C11, C12 = o.C12, C13 = o.C13, C14 = o.C14, D01 = o.D01, D02 = o.D02, D03 = o.D03, D04 = o.D04, D05 = o.D05, D06 = o.D06, D07 = o.D07, D08 = o.D08, D09 = o.D09, D10 = o.D10, D11 = o.D11, E01 = o.E01, E02 = o.E02, E03 = o.E03, F01 = o.F01, F02 = o.F02, F03 = o.F03, F04 = o.F04, F05 = o.F05, F06 = o.F06, F07 = o.F07, F08 = o.F08, F09 = o.F09, G01 = o.G01, G02 = o.G02, G03 = o.G03, G04 = o.G04.ToString(), G05 = o.G05.ToString(), G06 = o.G06.ToString(), H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = o.H04, H05 = "", I01 = o.I01, I02 = "", I03 = "", I04 = "", I05 = "", I06 = "", I07 = "", I08 = "", I09 = "", I10 = "", J01 = o.J01, J02 = o.J02, J03 = o.J03, K01 = o.K01, K02 = o.K02, L01 = o.L01, L02 = o.L02, L03 = ""
                            });                    
                }
                else if (workTable == "Check_Item_Fish")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_Fish> check_Item_Fish = new Dou.Models.DB.ModelEntity<Check_Item_Fish>(dbContext);

                    tmp = check_Item_Fish.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {                                
                                CheckYear = c.CheckDate == null ? 0 : ((DateTime)c.CheckDate).Year - 1911,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet,
                                A_Doesmeet = o.A_Doesmeet, B_Doesmeet = o.B_Doesmeet, C_Doesmeet = o.C_Doesmeet, D_Doesmeet = o.D_Doesmeet, E_Doesmeet = o.E_Doesmeet, F_Doesmeet = o.F_Doesmeet, G_Doesmeet = o.G_Doesmeet, H_Doesmeet = o.H_Doesmeet, I_Doesmeet = 0, J_Doesmeet = 0, K_Doesmeet = 0, L_Doesmeet = 0, M_Doesmeet = 0,
                                A01 = o.A01, A02 = o.A02, A03 = "", A04 = "", A05 = "", A06 = "", B01 = o.B01, B02 = o.B02, B03 = "", B04 = "", B05 = "", B06 = "", B07 = "", B08 = "", B09 = "", B10 = "", C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = "", C05 = "", C06 = "", C07 = "", C08 = "", C09 = "", C10 = "", C11 = "", C12 = "", C13 = "", C14 = "", D01 = o.D01, D02 = o.D02, D03 = o.D03, D04 = "", D05 = "", D06 = "", D07 = "", D08 = "", D09 = "", D10 = "", D11 = "", E01 = o.E01, E02 = "", E03 = "", F01 = o.F01, F02 = o.F02, F03 = o.F03, F04 = o.F04, F05 = o.F05, F06 = "", F07 = "", F08 = "", F09 = "", G01 = o.G01, G02 = o.G02, G03 = o.G03, G04 = o.G04, G05 = "", G06 = "", H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = "", H05 = "", I01 = "", I02 = "", I03 = "", I04 = "", I05 = "", I06 = "", I07 = "", I08 = "", I09 = "", I10 = "", J01 = "", J02 = "", J03 = "", K01 = "", K02 = "", L01 = "", L02 = "", L03 = ""
                            });  
                }
                else if (workTable == "Check_Item_Fish103")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_Fish103> check_Item_Fish103 = new Dou.Models.DB.ModelEntity<Check_Item_Fish103>(dbContext);

                    tmp = check_Item_Fish103.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {                                
                                CheckYear = c.CheckDate == null ? 0 : ((DateTime)c.CheckDate).Year - 1911,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet,
                                A_Doesmeet = o.A_Doesmeet, B_Doesmeet = o.B_Doesmeet, C_Doesmeet = o.C_Doesmeet, D_Doesmeet = o.D_Doesmeet, E_Doesmeet = 0, F_Doesmeet = o.F_Doesmeet, G_Doesmeet = o.G_Doesmeet, H_Doesmeet = o.H_Doesmeet, I_Doesmeet = 0, J_Doesmeet = 0, K_Doesmeet = 0, L_Doesmeet = 0, M_Doesmeet = 0,
                                A01 = o.A01, A02 = o.A02, A03 = "", A04 = "", A05 = "", A06 = "", B01 = o.B01, B02 = o.B02, B03 = "", B04 = "", B05 = "", B06 = "", B07 = "", B08 = "", B09 = "", B10 = "", C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = "", C05 = "", C06 = "", C07 = "", C08 = "", C09 = "", C10 = "", C11 = "", C12 = "", C13 = "", C14 = "", D01 = o.D01, D02 = o.D02, D03 = o.D03, D04 = "", D05 = "", D06 = "", D07 = "", D08 = "", D09 = "", D10 = "", D11 = "", E01 = "", E02 = "", E03 = "", F01 = o.F01, F02 = o.F02, F03 = o.F03, F04 = o.F04, F05 = o.F05, F06 = "", F07 = "", F08 = "", F09 = "", G01 = o.G01, G02 = o.G02, G03 = o.G03, G04 = o.G04, G05 = "", G06 = "", H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = "", H05 = "", I01 = "", I02 = "", I03 = "", I04 = "", I05 = "", I06 = "", I07 = "", I08 = "", I09 = "", I10 = "", J01 = "", J02 = "", J03 = "", K01 = "", K02 = "", L01 = "", L02 = "", L03 = ""
                            }); 
                }
                else if (workTable == "Check_Item_SelfUP")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_SelfUP> check_Item_SelfUP = new Dou.Models.DB.ModelEntity<Check_Item_SelfUP>(dbContext);

                    tmp = check_Item_SelfUP.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery.Where(a => a.Tank_Well == "0"), a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {                                
                                CheckYear = c.CheckDate == null ? 0 : ((DateTime)c.CheckDate).Year - 1911,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet,
                                A_Doesmeet = o.A_Doesmeet, B_Doesmeet = o.B_Doesmeet, C_Doesmeet = o.C_Doesmeet, D_Doesmeet = o.D_Doesmeet, E_Doesmeet = 0, F_Doesmeet = 0, G_Doesmeet = o.G_Doesmeet, H_Doesmeet = o.H_Doesmeet, I_Doesmeet = 0, J_Doesmeet = 0, K_Doesmeet = 0, L_Doesmeet = 0, M_Doesmeet = 0,
                                A01 = o.A01, A02 = o.A02, A03 = o.A03, A04 = o.A04, A05 = o.A05, A06 = o.A06, B01 = o.B01, B02 = o.B02, B03 = o.B03, B04 = "", B05 = "", B06 = "", B07 = "", B08 = "", B09 = "", B10 = "", C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = o.C04, C05 = o.C05, C06 = o.C06, C07 = o.C07, C08 = o.C08, C09 = "", C10 = "", C11 = "", C12 = "", C13 = "", C14 = "", D01 = o.D01, D02 = o.D02, D03 = "", D04 = "", D05 = "", D06 = "", D07 = "", D08 = "", D09 = "", D10 = "", D11 = "", E01 = "", E02 = "", E03 = "", F01 = "", F02 = "", F03 = "", F04 = "", F05 = "", F06 = "", F07 = "", F08 = "", F09 = "", G01 = o.G01, G02 = "", G03 = "", G04 = "", G05 = "", G06 = "", H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = o.H04, H05 = o.H05, I01 = "", I02 = "", I03 = "", I04 = "", I05 = "", I06 = "", I07 = "", I08 = "", I09 = "", I10 = "", J01 = "", J02 = "", J03 = "", K01 = "", K02 = "", L01 = "", L02 = "", L03 = ""
                            });                    
                }
                else if (workTable == "Check_Item_SelfDown")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_SelfDown> check_Item_SelfDown = new Dou.Models.DB.ModelEntity<Check_Item_SelfDown>(dbContext);

                    tmp = check_Item_SelfDown.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery.Where(a => a.Tank_Well == "1"), a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {                                
                                CheckYear = c.CheckDate == null ? 0 : ((DateTime)c.CheckDate).Year - 1911,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet,
                                A_Doesmeet = o.A_Doesmeet, B_Doesmeet = o.B_Doesmeet, C_Doesmeet = o.C_Doesmeet, D_Doesmeet = o.D_Doesmeet, E_Doesmeet = 0, F_Doesmeet = 0, G_Doesmeet = o.G_Doesmeet, H_Doesmeet = o.H_Doesmeet, I_Doesmeet = 0, J_Doesmeet = 0, K_Doesmeet = 0, L_Doesmeet = 0, M_Doesmeet = 0,
                                A01 = o.A01, A02 = o.A02, A03 = o.A03, A04 = o.A04, A05 = "", A06 = "", B01 = o.B01, B02 = o.B02, B03 = o.B03, B04 = "", B05 = "", B06 = "", B07 = "", B08 = "", B09 = "", B10 = "", C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = o.C04, C05 = o.C05, C06 = o.C06, C07 = "", C08 = "", C09 = "", C10 = "", C11 = "", C12 = "", C13 = "", C14 = "", D01 = o.D01, D02 = o.D02, D03 = "", D04 = "", D05 = "", D06 = "", D07 = "", D08 = "", D09 = "", D10 = "", D11 = "", E01 = "", E02 = "", E03 = "", F01 = "", F02 = "", F03 = "", F04 = "", F05 = "", F06 = "", F07 = "", F08 = "", F09 = "", G01 = o.G01, G02 = "", G03 = "", G04 = "", G05 = "", G06 = "", H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = o.H04, H05 = o.H05, I01 = "", I02 = "", I03 = "", I04 = "", I05 = "", I06 = "", I07 = "", I08 = "", I09 = "", I10 = "", J01 = "", J02 = "", J03 = "", K01 = "", K02 = "", L01 = "", L02 = "", L03 = ""
                            }); 
                }

                if (!string.IsNullOrEmpty(CheckItemTitel))
                {
                    string str = CheckItemTitel;

                    //文字
                    if (t1 == null)
                    {
                        t1 = CheckItemListSelectItemsClassImp.Checks.Where(a => a.CheckItemTitelSum == str).FirstOrDefault();
                        titles.Add("查核項目:" + t1 == null ? "" : t1.CheckItemTitel);
                    }

                    #region  條件:CheckItemTitel

                    tmp = tmp.Where(a => (str == "A_Doesmeet" && a.A_Doesmeet > 0)
                                    || (str == "B_Doesmeet" && a.B_Doesmeet > 0)
                                    || (str == "C_Doesmeet" && a.C_Doesmeet > 0)
                                    || (str == "D_Doesmeet" && a.D_Doesmeet > 0)
                                    || (str == "E_Doesmeet" && a.E_Doesmeet > 0)
                                    || (str == "F_Doesmeet" && a.F_Doesmeet > 0)
                                    || (str == "G_Doesmeet" && a.G_Doesmeet > 0)
                                    || (str == "H_Doesmeet" && a.H_Doesmeet > 0)
                                    || (str == "I_Doesmeet" && a.I_Doesmeet > 0)
                                    || (str == "J_Doesmeet" && a.J_Doesmeet > 0)
                                    || (str == "K_Doesmeet" && a.K_Doesmeet > 0)
                                    || (str == "L_Doesmeet" && a.L_Doesmeet > 0)
                                );

                    #endregion
                }

                if (!string.IsNullOrEmpty(CheckItemDescNo))
                {
                    string str = CheckItemDescNo;

                    //文字
                    if (t2 == null)
                    {
                        t2 = CheckItemListSelectItemsClassImp.Checks.Where(a => a.CheckItemDescNo == str).FirstOrDefault();
                        titles.Add("查核細項:" + t2 == null ? "" : t2.CheckItemDesc);
                    }

                    #region  條件:CheckItemDescNo

                    tmp = tmp.Where(a => (str == "A01" && a.A01 == "2")
                                    || (str == "A02" && a.A02 == "2")
                                    || (str == "A03" && a.A03 == "2")
                                    || (str == "A04" && a.A04 == "2")
                                    || (str == "A05" && a.A05 == "2")
                                    || (str == "A06" && a.A06 == "2")
                                    || (str == "B01" && a.B01 == "2")
                                    || (str == "B02" && a.B02 == "2")
                                    || (str == "B03" && a.B03 == "2")
                                    || (str == "B04" && a.B04 == "2")
                                    || (str == "B05" && a.B05 == "2")
                                    || (str == "B06" && a.B06 == "2")
                                    || (str == "B07" && a.B07 == "2")
                                    || (str == "B08" && a.B08 == "2")
                                    || (str == "B09" && a.B09 == "2")
                                    || (str == "B10" && a.B10 == "2")
                                    || (str == "C01" && a.C01 == "2")
                                    || (str == "C02" && a.C02 == "2")
                                    || (str == "C03" && a.C03 == "2")
                                    || (str == "C04" && a.C04 == "2")
                                    || (str == "C05" && a.C05 == "2")
                                    || (str == "C06" && a.C06 == "2")
                                    || (str == "C07" && a.C07 == "2")
                                    || (str == "C08" && a.C08 == "2")
                                    || (str == "C09" && a.C09 == "2")
                                    || (str == "C10" && a.C10 == "2")
                                    || (str == "C11" && a.C11 == "2")
                                    || (str == "C12" && a.C12 == "2")
                                    || (str == "C13" && a.C13 == "2")
                                    || (str == "C14" && a.C14 == "2")
                                    || (str == "D01" && a.D01 == "2")
                                    || (str == "D02" && a.D02 == "2")
                                    || (str == "D03" && a.D03 == "2")
                                    || (str == "D04" && a.D04 == "2")
                                    || (str == "D05" && a.D05 == "2")
                                    || (str == "D06" && a.D06 == "2")
                                    || (str == "D07" && a.D07 == "2")
                                    || (str == "D08" && a.D08 == "2")
                                    || (str == "D09" && a.D09 == "2")
                                    || (str == "D10" && a.D10 == "2")
                                    || (str == "D11" && a.D11 == "2")
                                    || (str == "E01" && a.E01 == "2")
                                    || (str == "E02" && a.E02 == "2")
                                    || (str == "E03" && a.E03 == "2")
                                    || (str == "F01" && a.F01 == "2")
                                    || (str == "F02" && a.F02 == "2")
                                    || (str == "F03" && a.F03 == "2")
                                    || (str == "F04" && a.F04 == "2")
                                    || (str == "F05" && a.F05 == "2")
                                    || (str == "F06" && a.F06 == "2")
                                    || (str == "F07" && a.F07 == "2")
                                    || (str == "F08" && a.F08 == "2")
                                    || (str == "F09" && a.F09 == "2")
                                    || (str == "G01" && a.G01 == "2")
                                    || (str == "G02" && a.G02 == "2")
                                    || (str == "G03" && a.G03 == "2")
                                    || (str == "G04" && a.G04 == "2")
                                    || (str == "G05" && a.G05 == "2")
                                    || (str == "G06" && a.G06 == "2")
                                    || (str == "H01" && a.H01 == "2")
                                    || (str == "H02" && a.H02 == "2")
                                    || (str == "H03" && a.H03 == "2")
                                    || (str == "H04" && a.H04 == "2")
                                    || (str == "H05" && a.H05 == "2")
                                    || (str == "I01" && a.I01 == "2")
                                    || (str == "I02" && a.I02 == "2")
                                    || (str == "I03" && a.I03 == "2")
                                    || (str == "I04" && a.I04 == "2")
                                    || (str == "I05" && a.I05 == "2")
                                    || (str == "I06" && a.I06 == "2")
                                    || (str == "I07" && a.I07 == "2")
                                    || (str == "I08" && a.I08 == "2")
                                    || (str == "I09" && a.I09 == "2")
                                    || (str == "I10" && a.I10 == "2")
                                    || (str == "J01" && a.J01 == "2")
                                    || (str == "J02" && a.J02 == "2")
                                    || (str == "J03" && a.J03 == "2")
                                    || (str == "K01" && a.K01 == "2")
                                    || (str == "K02" && a.K02 == "2")
                                    || (str == "L01" && a.L01 == "2")
                                    || (str == "L02" && a.L02 == "2")
                                    || (str == "L03" && a.L03 == "2")
                                );

                    #endregion
                }

                //總計:該項目有缺失家數	tmp_CheckHiatusCount
                if (tmp != null)
                {
                    var info = tmp.Where(a => a.CheckYear == WorkYear)
                                .GroupBy(a => a.CheckYear).Select(a => new Check2Class
                                {
                                    CheckYear = (int)a.Key,
                                    CheckHiatusCount = a.Sum(p => p.AllDoesmeet > 0 ? 1 : 0)
                                }).ToList();

                    tmp_CheckHiatusCount.AddRange(info);
                }
            }

            //結果 母體:source
            var result = source.ToList()
                        .GroupJoin(tmp_CheckHiatusCount, a => a.CheckYear, b => b.CheckYear, (o, c) => new
                        {
                            o.CheckYear,
                            o.CheckAllCount,
                            o.TotalHiatusCount,
                            CheckHiatusCount = c.FirstOrDefault() == null ? 0 : c.FirstOrDefault().CheckHiatusCount
                        })
                        ////.AsEnumerable()
                        .Select(a => new vw_Audit_ReportMissingYear
                        {
                            CheckYear = a.CheckYear,
                            CheckAllCount = a.CheckAllCount,
                            CheckHiatusCount = a.CheckHiatusCount,                            
                            CheckHiatusRate = a.CheckAllCount == 0 ? 0 : Math.Round((double)a.CheckHiatusCount * 100 / a.CheckAllCount, 2),
                            ItemHiatusAllRate = a.TotalHiatusCount == 0 ? 0 : Math.Round((double)a.CheckHiatusCount * 100 / a.TotalHiatusCount, 2),
                        });

            //var zzzz = result.OrderBy(a => a.CheckYear).ToList();

            return result;
        }
    }

    public class vw_Audit_ReportMissingYear
    {
        [Display(Name = "年度")]
        [ColumnDef(Sortable = true)]
        public int CheckYear { get; set; }

        [Display(Name = "查核家數")]
        [ColumnDef(Sortable = true)]
        public int CheckAllCount { get; set; }

        [Display(Name = "該項目有缺失家數")]
        [ColumnDef(Sortable = true)]
        public int CheckHiatusCount { get; set; }

        //[Display(Name = "總缺失數")]
        //[ColumnDef(Visible = false)]
        //public int TotalHiatusCount { get; set; }

        [Display(Name = "缺失發生比例")]
        [ColumnDef(Sortable = true)]
        public double CheckHiatusRate { get; set; }

        [Display(Name = "佔總缺失比例")]
        [ColumnDef(Sortable = true)]
        public double ItemHiatusAllRate { get; set; }

        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectGearingWith = "Business_theme,CaseType,true",
            SelectItemsClassNamespace = OrganizationCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        public string CaseType { get; }

        [Display(Name = "查核項目", Order = 2)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
             //SelectGearingWith = "CheckItemDescNo,CheckItemTitel,true", 第1層和第2層下拉都會影響(查核項目)
             SelectItemsClassNamespace = CheckItemListSelectItemsClassImp.AssemblyQualifiedName)]
        public string CheckItemTitel { get; }

        [Display(Name = "查核細項", Order = 3)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
             SelectItems = "{}")]
        public string CheckItemDescNo { get; }

        [Display(Name = "查核年度", Order = 4)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.Controllers.Audit.Audit_StatisticReportEquipViewYearSelectItems.AssemblyQualifiedName)]
        public DateTime WorkYear { get; }


    }
}