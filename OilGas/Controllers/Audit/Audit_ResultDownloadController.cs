using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using DouHelper;
using Microsoft.Ajax.Utilities;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ResultDownload", Name = "石油設施查核結果彙整", MenuPath = "查核輔導專區/G統計報表專區", Action = "Index", Index = 8, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ResultDownloadController : AGenericModelController<vw_Audit_ResultDownloadController>
    {
        // GET: Audit_ResultDownload
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ResultDownloadController> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ResultDownloadController>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ResultDownloadController> GetDataDBObject(IModelEntity<vw_Audit_ResultDownloadController> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ResultDownloadController>().AsQueryable();
            }

            return null;
        }

        public ActionResult ExportAudit_ResultDownload(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G統計報表專區_石油設施查核結果彙整);
            string fileTitle = "查核輔導專區_石油設施查核結果彙整";

            //解決資料查詢錯誤，但查詢數量為全部(非分頁數量)
            //不使用dou filter過濾資料(iquery)            
            //var result = iquery.AsEnumerable();
            List<string> titles = new List<string>() { "查核輔導專區_石油設施查核結果彙整，查詢條件:" };
            string workTable = "";
            var result = GetOutputData(ref workTable, ref titles, paras);

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.OrderByDescending(a => a.CheckDate).ThenBy(a => a.CheckNo).ToList();
            
            //結果
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<CheckItemList> checkItemList = new Dou.Models.DB.ModelEntity<CheckItemList>(dbContext);
            var chkItems = checkItemList.GetAll().Where(a => a.CheckItemTable == workTable).ToList();

            //標頭合併儲存格設計
            Dictionary<string, int> dicsHeaderMerge = new Dictionary<string, int>();
            var tt = chkItems.GroupBy(a => new { a.CheckItemTitelNo , a.CheckItemTitel }).Select(a => new {
                no = a.Key.CheckItemTitelNo, name = a.Key.CheckItemTitel, amount = a.Count()
            }).OrderBy(a => a.no);
            foreach (var t in tt)
            {
                dicsHeaderMerge.Add(t.name, t.amount + 1);  //+1 多小計數據
            }

            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
            var cars = carVehicleGas_BusinessOrganization.GetAll().ToList();            

            foreach (var v in output)
            {                
                dynamic f = new ExpandoObject();

                f.查核編號 = v.CheckNo;
                var cc = cars.Where(a => a.Value == v.Business_theme).FirstOrDefault();
                f.營業主體 = cc == null || cc.Name == "其他" ? v.Business_theme_Name : cc.Name;
                f.加油站站名 = v.Gas_Name;
                f.查核日期 = DateFormat.ToDate4((DateTime)v.CheckDate);

                if (workTable== "Check_Item")
                {
                    f.A01 = GetChkStr(v.A01); f.A02 = GetChkStr(v.A02); f.A03 = GetChkStr(v.A03); f.A04 = GetChkStr(v.A04); f.A05 = GetChkStr(v.A05); f.A06 = GetChkStr(v.A06); f.小計_A_Doesmeet = v.A_Doesmeet; f.B01 = GetChkStr(v.B01); f.B02 = GetChkStr(v.B02); f.B03 = GetChkStr(v.B03); f.B04 = GetChkStr(v.B04); f.B05 = GetChkStr(v.B05); f.B06 = GetChkStr(v.B06); f.B07 = GetChkStr(v.B07); f.B08 = GetChkStr(v.B08); f.B09 = GetChkStr(v.B09); f.B10 = GetChkStr(v.B10); f.小計_B_Doesmeet = v.B_Doesmeet; f.C01 = GetChkStr(v.C01); f.C02 = GetChkStr(v.C02); f.C03 = GetChkStr(v.C03); f.C04 = GetChkStr(v.C04); f.C05 = GetChkStr(v.C05); f.C06 = GetChkStr(v.C06); f.C07 = GetChkStr(v.C07); f.C08 = GetChkStr(v.C08); f.C09 = GetChkStr(v.C09); f.C10 = GetChkStr(v.C10); f.C11 = GetChkStr(v.C11); f.C12 = GetChkStr(v.C12); f.C13 = GetChkStr(v.C13); f.C14 = GetChkStr(v.C14); f.小計_C_Doesmeet = v.C_Doesmeet; f.D01 = GetChkStr(v.D01); f.D02 = GetChkStr(v.D02); f.D03 = GetChkStr(v.D03); f.D04 = GetChkStr(v.D04); f.D05 = GetChkStr(v.D05); f.D06 = GetChkStr(v.D06); f.D07 = GetChkStr(v.D07); f.D08 = GetChkStr(v.D08); f.D09 = GetChkStr(v.D09); f.D10 = GetChkStr(v.D10); f.D11 = GetChkStr(v.D11); f.小計_D_Doesmeet = v.D_Doesmeet; f.E01 = GetChkStr(v.E01); f.E02 = GetChkStr(v.E02); f.E03 = GetChkStr(v.E03); f.小計_E_Doesmeet = v.E_Doesmeet; f.F01 = GetChkStr(v.F01); f.F02 = GetChkStr(v.F02); f.F03 = GetChkStr(v.F03); f.F04 = GetChkStr(v.F04); f.F05 = GetChkStr(v.F05); f.F06 = GetChkStr(v.F06); f.F07 = GetChkStr(v.F07); f.F08 = GetChkStr(v.F08); f.F09 = GetChkStr(v.F09); f.小計_F_Doesmeet = v.F_Doesmeet; f.G01 = GetChkStr(v.G01); f.G02 = GetChkStr(v.G02); f.G03 = GetChkStr(v.G03); f.G04 = GetChkStr(v.G04); f.G05 = GetChkStr(v.G05); f.G06 = GetChkStr(v.G06); f.小計_G_Doesmeet = v.G_Doesmeet; f.H01 = GetChkStr(v.H01); f.H02 = GetChkStr(v.H02); f.H03 = GetChkStr(v.H03); f.H04 = GetChkStr(v.H04); f.H05 = GetChkStr(v.H05); f.小計_H_Doesmeet = v.H_Doesmeet; f.I01 = GetChkStr(v.I01); f.I02 = GetChkStr(v.I02); f.I03 = GetChkStr(v.I03); f.I04 = GetChkStr(v.I04); f.I05 = GetChkStr(v.I05); f.I06 = GetChkStr(v.I06); f.I07 = GetChkStr(v.I07); f.I08 = GetChkStr(v.I08); f.I09 = GetChkStr(v.I09); f.I10 = GetChkStr(v.I10); f.小計_I_Doesmeet = v.I_Doesmeet; f.J01 = GetChkStr(v.J01); f.J02 = GetChkStr(v.J02); f.J03 = GetChkStr(v.J03); f.小計_J_Doesmeet = v.J_Doesmeet; f.K01 = GetChkStr(v.K01); f.K02 = GetChkStr(v.K02); f.小計_K_Doesmeet = v.K_Doesmeet; f.L01 = GetChkStr(v.L01); f.L02 = GetChkStr(v.L02); f.L03 = GetChkStr(v.L03); f.小計_L_Doesmeet = v.L_Doesmeet;
                }
                else if(workTable== "Check_Item_97")
                {
                    f.A01 = GetChkStr(v.A01); f.A02 = GetChkStr(v.A02); f.A03 = GetChkStr(v.A03); f.A04 = GetChkStr(v.A04); f.A05 = GetChkStr(v.A05); f.A06 = GetChkStr(v.A06); f.A07 = GetChkStr(v.A07); f.A08 = GetChkStr(v.A08); f.A09 = GetChkStr(v.A09); f.A10 = GetChkStr(v.A10); f.A11 = GetChkStr(v.A11); f.A12 = GetChkStr(v.A12); f.小計_A_Doesmeet = v.A_Doesmeet; f.B01 = GetChkStr(v.B01); f.B02 = GetChkStr(v.B02); f.B03 = GetChkStr(v.B03); f.B04 = GetChkStr(v.B04); f.B05 = GetChkStr(v.B05); f.B06 = GetChkStr(v.B06); f.B07 = GetChkStr(v.B07); f.B08 = GetChkStr(v.B08); f.B09 = GetChkStr(v.B09); f.B10 = GetChkStr(v.B10); f.小計_B_Doesmeet = v.B_Doesmeet; f.C01 = GetChkStr(v.C01); f.C02 = GetChkStr(v.C02); f.C03 = GetChkStr(v.C03); f.C04 = GetChkStr(v.C04); f.C05 = GetChkStr(v.C05); f.C06 = GetChkStr(v.C06); f.C07 = GetChkStr(v.C07); f.C08 = GetChkStr(v.C08); f.C09 = GetChkStr(v.C09); f.C10 = GetChkStr(v.C10); f.C11 = GetChkStr(v.C11); f.C12 = GetChkStr(v.C12); f.C13 = GetChkStr(v.C13); f.C14 = GetChkStr(v.C14); f.小計_C_Doesmeet = v.C_Doesmeet; f.D01 = GetChkStr(v.D01); f.D02 = GetChkStr(v.D02); f.D03 = GetChkStr(v.D03); f.D04 = GetChkStr(v.D04); f.D05 = GetChkStr(v.D05); f.D06 = GetChkStr(v.D06); f.D07 = GetChkStr(v.D07); f.D08 = GetChkStr(v.D08); f.D09 = GetChkStr(v.D09); f.D10 = GetChkStr(v.D10); f.D11 = GetChkStr(v.D11); f.小計_D_Doesmeet = v.D_Doesmeet; f.E01 = GetChkStr(v.E01); f.E02 = GetChkStr(v.E02); f.E03 = GetChkStr(v.E03); f.小計_E_Doesmeet = v.E_Doesmeet; f.F01 = GetChkStr(v.F01); f.F02 = GetChkStr(v.F02); f.F03 = GetChkStr(v.F03); f.F04 = GetChkStr(v.F04); f.F05 = GetChkStr(v.F05); f.F06 = GetChkStr(v.F06); f.F07 = GetChkStr(v.F07); f.F08 = GetChkStr(v.F08); f.F09 = GetChkStr(v.F09); f.F10 = GetChkStr(v.F10); f.小計_F_Doesmeet = v.F_Doesmeet; f.G01 = GetChkStr(v.G01); f.G02 = GetChkStr(v.G02); f.G03 = GetChkStr(v.G03); f.G04 = GetChkStr(v.G04); f.G05 = GetChkStr(v.G05); f.G06 = GetChkStr(v.G06); f.小計_G_Doesmeet = v.G_Doesmeet; f.H01 = GetChkStr(v.H01); f.H02 = GetChkStr(v.H02); f.H03 = GetChkStr(v.H03); f.H04 = GetChkStr(v.H04); f.小計_H_Doesmeet = v.H_Doesmeet; f.I01 = GetChkStr(v.I01); f.小計_I_Doesmeet = v.I_Doesmeet; f.J01 = GetChkStr(v.J01); f.J02 = GetChkStr(v.J02); f.J03 = GetChkStr(v.J03); f.J04 = GetChkStr(v.J04); f.J05 = GetChkStr(v.J05); f.J06 = GetChkStr(v.J06); f.J07 = GetChkStr(v.J07); f.J08 = GetChkStr(v.J08); f.J09 = GetChkStr(v.J09); f.J10 = GetChkStr(v.J10); f.J11 = GetChkStr(v.J11); f.小計_J_Doesmeet = v.J_Doesmeet; f.K01 = GetChkStr(v.K01); f.K02 = GetChkStr(v.K02); f.K03 = GetChkStr(v.K03); f.K04 = GetChkStr(v.K04); f.小計_K_Doesmeet = v.K_Doesmeet; f.L01 = GetChkStr(v.L01); f.L02 = GetChkStr(v.L02); f.小計_L_Doesmeet = v.L_Doesmeet; f.M01 = GetChkStr(v.M01); f.M02 = GetChkStr(v.M02); f.小計_M_Doesmeet = v.M_Doesmeet;
                }
                else if (workTable == "Check_Item_Fish")
                {
                    f.A01 = GetChkStr(v.A01); f.A02 = GetChkStr(v.A02); f.小計_A_Doesmeet = v.A_Doesmeet; f.B01 = GetChkStr(v.B01); f.B02 = GetChkStr(v.B02); f.小計_B_Doesmeet = v.B_Doesmeet; f.C01 = GetChkStr(v.C01); f.C02 = GetChkStr(v.C02); f.C03 = GetChkStr(v.C03); f.小計_C_Doesmeet = v.C_Doesmeet; f.D01 = GetChkStr(v.D01); f.D02 = GetChkStr(v.D02); f.D03 = GetChkStr(v.D03); f.小計_D_Doesmeet = v.D_Doesmeet; f.E01 = GetChkStr(v.E01); f.小計_E_Doesmeet = v.E_Doesmeet; f.F01 = GetChkStr(v.F01); f.F02 = GetChkStr(v.F02); f.F03 = GetChkStr(v.F03); f.F04 = GetChkStr(v.F04); f.F05 = GetChkStr(v.F05); f.小計_F_Doesmeet = v.F_Doesmeet; f.G01 = GetChkStr(v.G01); f.G02 = GetChkStr(v.G02); f.G03 = GetChkStr(v.G03); f.G04 = GetChkStr(v.G04); f.小計_G_Doesmeet = v.G_Doesmeet; f.H01 = GetChkStr(v.H01); f.H02 = GetChkStr(v.H02); f.H03 = GetChkStr(v.H03); f.小計_H_Doesmeet = v.H_Doesmeet;
                }
                else if (workTable == "Check_Item_Fish103")
                {
                    f.A01 = GetChkStr(v.A01); f.A02 = GetChkStr(v.A02); f.小計_A_Doesmeet = v.A_Doesmeet; f.B01 = GetChkStr(v.B01); f.B02 = GetChkStr(v.B02); f.小計_B_Doesmeet = v.B_Doesmeet; f.C01 = GetChkStr(v.C01); f.C02 = GetChkStr(v.C02); f.C03 = GetChkStr(v.C03); f.小計_C_Doesmeet = v.C_Doesmeet; f.D01 = GetChkStr(v.D01); f.D02 = GetChkStr(v.D02); f.D03 = GetChkStr(v.D03); f.小計_D_Doesmeet = v.D_Doesmeet; f.F01 = GetChkStr(v.F01); f.F02 = GetChkStr(v.F02); f.F03 = GetChkStr(v.F03); f.F04 = GetChkStr(v.F04); f.F05 = GetChkStr(v.F05); f.小計_F_Doesmeet = v.F_Doesmeet; f.G01 = GetChkStr(v.G01); f.G02 = GetChkStr(v.G02); f.G03 = GetChkStr(v.G03); f.G04 = GetChkStr(v.G04); f.小計_G_Doesmeet = v.G_Doesmeet; f.H01 = GetChkStr(v.H01); f.H02 = GetChkStr(v.H02); f.H03 = GetChkStr(v.H03); f.小計_H_Doesmeet = v.H_Doesmeet;
                }
                else if (workTable == "Check_Item_SelfUP")
                {
                    ////特殊(先H後G)
                    f.A01 = GetChkStr(v.A01); f.A02 = GetChkStr(v.A02); f.A03 = GetChkStr(v.A03); f.A04 = GetChkStr(v.A04); f.A05 = GetChkStr(v.A05); f.A06 = GetChkStr(v.A06); f.小計_A_Doesmeet = v.A_Doesmeet; f.B01 = GetChkStr(v.B01); f.B02 = GetChkStr(v.B02); f.B03 = GetChkStr(v.B03); f.小計_B_Doesmeet = v.B_Doesmeet; f.C01 = GetChkStr(v.C01); f.C02 = GetChkStr(v.C02); f.C03 = GetChkStr(v.C03); f.C04 = GetChkStr(v.C04); f.C05 = GetChkStr(v.C05); f.C06 = GetChkStr(v.C06); f.C07 = GetChkStr(v.C07); f.C08 = GetChkStr(v.C08); f.小計_C_Doesmeet = v.C_Doesmeet; f.D01 = GetChkStr(v.D01); f.D02 = GetChkStr(v.D02); f.小計_D_Doesmeet = v.D_Doesmeet; 
                    f.H01 = GetChkStr(v.H01); f.H02 = GetChkStr(v.H02); f.H03 = GetChkStr(v.H03); f.H04 = GetChkStr(v.H04); f.H05 = GetChkStr(v.H05); f.小計_H_Doesmeet = v.H_Doesmeet; f.G01 = GetChkStr(v.G01); f.小計_G_Doesmeet = v.G_Doesmeet;
                }
                else if (workTable == "Check_Item_SelfDown")
                {
                    ////特殊(先H後G)
                    f.A01 = GetChkStr(v.A01); f.A02 = GetChkStr(v.A02); f.A03 = GetChkStr(v.A03); f.A04 = GetChkStr(v.A04); f.小計_A_Doesmeet = v.A_Doesmeet; f.B01 = GetChkStr(v.B01); f.B02 = GetChkStr(v.B02); f.B03 = GetChkStr(v.B03); f.小計_B_Doesmeet = v.B_Doesmeet; f.C01 = GetChkStr(v.C01); f.C02 = GetChkStr(v.C02); f.C03 = GetChkStr(v.C03); f.C04 = GetChkStr(v.C04); f.C05 = GetChkStr(v.C05); f.C06 = GetChkStr(v.C06); f.小計_C_Doesmeet = v.C_Doesmeet; f.D01 = GetChkStr(v.D01); f.D02 = GetChkStr(v.D02); f.小計_D_Doesmeet = v.D_Doesmeet; 
                    f.H01 = GetChkStr(v.H01); f.H02 = GetChkStr(v.H02); f.H03 = GetChkStr(v.H03); f.H04 = GetChkStr(v.H04); f.H05 = GetChkStr(v.H05); f.小計_H_Doesmeet = v.H_Doesmeet; f.G01 = GetChkStr(v.G01); f.小計_G_Doesmeet = v.G_Doesmeet;
                }


                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //查無符合資料表數
            if (output.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            //產出excel
            int mergeFirstCol = 4;
            string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF2_1_spec1(chkItems, fileTitle, titles, dicsHeaderMerge, mergeFirstCol, list, folder, "N");
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

        private IQueryable<vw_Audit_ResultDownloadController> GetOutputData(ref string workTable, ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            
            Dou.Models.DB.IModelEntity<Check_Basic> check_Basic = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);            
            Dou.Models.DB.IModelEntity<vw_UNPIVOT_Check_Item> vw_Checks = new Dou.Models.DB.ModelEntity<vw_UNPIVOT_Check_Item>(dbContext);
            Dou.Models.DB.IModelEntity<vw_UNPIVOT_Check_Item_Other> vw_Checks_Others = new Dou.Models.DB.ModelEntity<vw_UNPIVOT_Check_Item_Other>(dbContext);                        

            //條件
            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");
            var CheckDate_Start_Between_ = KeyValue.GetFilterParaValue(paras, "CheckDate-Start-Between_");
            var CheckDate_End_Between_ = KeyValue.GetFilterParaValue(paras, "CheckDate-End-Between_");
            var CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");

            if (CaseType == "Check_Item")
            {
                titles.Add("營業主體:" + "汽/機車加油站");
            }
            else if (CaseType == "Check_Item_Fish")
            {
                titles.Add("營業主體:" + "漁船加油站");
            }
            else if (CaseType == "Check_Item_SelfUP")
            {
                titles.Add("營業主體:" + "自用加儲油設施(地上)");
            }
            else if (CaseType == "Check_Item_SelfDown")
            {
                titles.Add("營業主體:" + "自用加儲油設施(地下)");
            }                        

            //(特別處理)workTable設定
            //int workYear = DateTime.Now.Year;
            int workYear = 95; /////////////////////////////////xxxxxxxxxxxxxx
            if (!string.IsNullOrEmpty(CheckDate_Start_Between_))
            {
                DateTime date = DateTime.Parse(CheckDate_Start_Between_);
                workYear = date.Year - 1911;
            }
            workTable = Code.GetWorkTable(CaseType, workYear);

            //統計
            IQueryable<vw_Audit_ResultDownloadController> iquery = null;
            if (workTable == "Check_Item")
            {
                Dou.Models.DB.IModelEntity<Check_Item> check_Item = new Dou.Models.DB.ModelEntity<Check_Item>(dbContext);

                iquery = check_Item.GetAll().Where(a => a.CheckNo != null)
                        .Join(check_Basic.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ResultDownloadController {
                            CheckNo = o.CheckNo,
                            CheckDate = c.CheckDate,
                            Business_theme = c.Business_theme,
                            Business_theme_Name = c.Business_theme_Name,
                            Gas_Name = c.Gas_Name,
                            A_Doesmeet = o.A_Doesmeet, A01 = o.A01, A02 = o.A02, A03 = o.A03, A04 = o.A04, A05 = o.A05, A06 = o.A06,
                            B_Doesmeet = o.B_Doesmeet, B01 = o.B01, B02 = o.B02, B03 = o.B03, B04 = o.B04, B05 = o.B05, B06 = o.B06, B07 = o.B07, B08 = o.B08, B09 = o.B09, B10 = o.B10, 
                            C_Doesmeet = o.C_Doesmeet, C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = o.C04, C05 = o.C05, C06 = o.C06, C07 = o.C07, C08 = o.C08, C09 = o.C09, C10 = o.C10, C11 = o.C11, C12 = o.C12, C13 = o.C13, C14 = o.C14, 
                            D_Doesmeet = o.D_Doesmeet, D01 = o.D01, D02 = o.D02, D03 = o.D03, D04 = o.D04, D05 = o.D05, D06 = o.D06, D07 = o.D07, D08 = o.D08, D09 = o.D09, D10 = o.D10, D11 = o.D11, 
                            E_Doesmeet = o.E_Doesmeet, E01 = o.E01, E02 = o.E02, E03 = o.E03, 
                            F_Doesmeet = o.F_Doesmeet, F01 = o.F01, F02 = o.F02, F03 = o.F03, F04 = o.F04, F05 = o.F05, F06 = o.F06, F07 = o.F07, F08 = o.F08, F09 = o.F09, 
                            G_Doesmeet = o.G_Doesmeet, G01 = o.G01, G02 = o.G02, G03 = o.G03, G04 = o.G04, G05 = o.G05, G06 = o.G06, 
                            H_Doesmeet = o.H_Doesmeet, H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = o.H04, H05 = o.H05, 
                            I_Doesmeet = o.I_Doesmeet, I01 = o.I01, I02 = o.I02, I03 = o.I03, I04 = o.I04, I05 = o.I05, I06 = o.I06, I07 = o.I07, I08 = o.I08, I09 = o.I09, I10 = o.I10, 
                            J_Doesmeet = o.J_Doesmeet, J01 = o.J01, J02 = o.J02, J03 = o.J03, 
                            K_Doesmeet = o.K_Doesmeet, K01 = o.K01, K02 = o.K02, 
                            L_Doesmeet = o.L_Doesmeet, L01 = o.L01, L02 = o.L02, L03 = o.L03,
                        });
            }
            else if(workTable == "Check_Item_97")
            {
                Dou.Models.DB.IModelEntity<Check_Item_97> check_Item_97 = new Dou.Models.DB.ModelEntity<Check_Item_97>(dbContext);

                iquery = check_Item_97.GetAll().Where(a => a.CheckNo != null)
                        .Join(check_Basic.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ResultDownloadController {
                            CheckNo = o.CheckNo,
                            CheckDate = c.CheckDate,
                            Business_theme = c.Business_theme,
                            Business_theme_Name = c.Business_theme_Name,
                            Gas_Name = c.Gas_Name,
                            A_Doesmeet = o.A_Doesmeet, A01 = o.A01, A02 = o.A02, A03 = o.A03, A04 = o.A04, A05 = o.A05, A06 = o.A06, A07 = o.A07, A08 = o.A08, A09 = o.A09, A10 = o.A10, 
                            B_Doesmeet = o.B_Doesmeet, B01 = o.B01, B02 = o.B02, B03 = o.B03, B04 = o.B04, B05 = o.B05, B06 = o.B06, B07 = o.B07, B08 = o.B08, B09 = o.B09, B10 = o.B10, 
                            C_Doesmeet = o.C_Doesmeet, C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = o.C04, C05 = o.C05, C06 = o.C06, C07 = o.C07, C08 = o.C08, C09 = o.C09, C10 = o.C10, C11 = o.C11, C12 = o.C12, C13 = o.C13, C14 = o.C14, 
                            D_Doesmeet = o.D_Doesmeet, D01 = o.D01, D02 = o.D02, D03 = o.D03, D04 = o.D04, D05 = o.D05, D06 = o.D06, D07 = o.D07, D08 = o.D08, D09 = o.D09, D10 = o.D10, D11 = o.D11, 
                            E_Doesmeet = o.E_Doesmeet, E01 = o.E01, E02 = o.E02, E03 = o.E03, 
                            F_Doesmeet = o.F_Doesmeet, F01 = o.F01, F02 = o.F02, F03 = o.F03, F04 = o.F04, F05 = o.F05, F06 = o.F06, F07 = o.F07, F08 = o.F08, F09 = o.F09, F10 = o.F10, 
                            G_Doesmeet = o.G_Doesmeet, G01 = o.G01, G02 = o.G02, G03 = o.G03, G04 = o.G04.ToString(), G05 = o.G05.ToString(), G06 = o.G06.ToString(), 
                            H_Doesmeet = o.H_Doesmeet, H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = o.H04, 
                            I_Doesmeet = o.I_Doesmeet, I01 = o.I01, 
                            J_Doesmeet = o.J_Doesmeet, J01 = o.J01, J02 = o.J02, J03 = o.J03, J04 = o.J04, J05 = o.J05, J06 = o.J06, J07 = o.J07, J08 = o.J08, J09 = o.J09, J10 = o.J10, J11 = o.J11, 
                            K_Doesmeet = o.K_Doesmeet, K01 = o.K01, K02 = o.K02, K03 = o.K03, K04 = o.K04, 
                            L_Doesmeet = o.L_Doesmeet, L01 = o.L01, L02 = o.L02, 
                            M_Doesmeet = o.M_Doesmeet, M01 = o.M01, M02 = o.M02,                                            
                        });
            }
            else if(workTable == "Check_Item_Fish")
            {
                Dou.Models.DB.IModelEntity<Check_Item_Fish> check_Item_Fish = new Dou.Models.DB.ModelEntity<Check_Item_Fish>(dbContext);

                iquery = check_Item_Fish.GetAll().Where(a => a.CheckNo != null)
                        .Join(check_Basic.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ResultDownloadController {
                            CheckNo = o.CheckNo,
                            CheckDate = c.CheckDate,
                            Business_theme = c.Business_theme,
                            Business_theme_Name = c.Business_theme_Name,
                            Gas_Name = c.Gas_Name,
                            A_Doesmeet = o.A_Doesmeet, A01 = o.A01, A02 = o.A02, 
                            B_Doesmeet = o.B_Doesmeet, B01 = o.B01, B02 = o.B02, 
                            C_Doesmeet = o.C_Doesmeet, C01 = o.C01, C02 = o.C02, C03 = o.C03, 
                            D_Doesmeet = o.D_Doesmeet, D01 = o.D01, D02 = o.D02, D03 = o.D03, 
                            E_Doesmeet = o.E_Doesmeet, E01 = o.E01, 
                            F_Doesmeet = o.F_Doesmeet, F01 = o.F01, F02 = o.F02, F03 = o.F03, F04 = o.F04, F05 = o.F05, 
                            G_Doesmeet = o.G_Doesmeet, G01 = o.G01, G02 = o.G02, G03 = o.G03, G04 = o.G04, 
                            H_Doesmeet = o.H_Doesmeet, H01 = o.H01, H02 = o.H02, H03 = o.H03,                                         
                        });
            }
            else if(workTable == "Check_Item_Fish103")
            {
                Dou.Models.DB.IModelEntity<Check_Item_Fish103> check_Item_Fish103 = new Dou.Models.DB.ModelEntity<Check_Item_Fish103>(dbContext);

                iquery = check_Item_Fish103.GetAll().Where(a => a.CheckNo != null)
                        .Join(check_Basic.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ResultDownloadController {
                            CheckNo = o.CheckNo,
                            CheckDate = c.CheckDate,
                            Business_theme = c.Business_theme,
                            Business_theme_Name = c.Business_theme_Name,
                            Gas_Name = c.Gas_Name,
                            A_Doesmeet = o.A_Doesmeet, A01 = o.A01, A02 = o.A02, 
                            B_Doesmeet = o.B_Doesmeet, B01 = o.B01, B02 = o.B02, 
                            C_Doesmeet = o.C_Doesmeet, C01 = o.C01, C02 = o.C02, C03 = o.C03, 
                            D_Doesmeet = o.D_Doesmeet, D01 = o.D01, D02 = o.D02, D03 = o.D03, 
                            F_Doesmeet = o.F_Doesmeet, F01 = o.F01, F02 = o.F02, F03 = o.F03, F04 = o.F04, F05 = o.F05, 
                            G_Doesmeet = o.G_Doesmeet, G01 = o.G01, G02 = o.G02, G03 = o.G03, G04 = o.G04, 
                            H_Doesmeet = o.H_Doesmeet, H01 = o.H01, H02 = o.H02, H03 = o.H03,                                      
                        });
            }
            else if(workTable == "Check_Item_SelfUP")
            {
                Dou.Models.DB.IModelEntity<Check_Item_SelfUP> check_Item_SelfUP = new Dou.Models.DB.ModelEntity<Check_Item_SelfUP>(dbContext);

                iquery = check_Item_SelfUP.GetAll().Where(a => a.CheckNo != null)
                        .Join(check_Basic.GetAll().Where(a => a.Tank_Well == "0"), a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ResultDownloadController {
                            CheckNo = o.CheckNo,
                            CheckDate = c.CheckDate,
                            Business_theme = c.Business_theme,
                            Business_theme_Name = c.Business_theme_Name,
                            Gas_Name = c.Gas_Name,
                            A_Doesmeet = o.A_Doesmeet, A01 = o.A01, A02 = o.A02, A03 = o.A03, A04 = o.A04, A05 = o.A05, A06 = o.A06, 
                            B_Doesmeet = o.B_Doesmeet, B01 = o.B01, B02 = o.B02, B03 = o.B03, 
                            C_Doesmeet = o.C_Doesmeet, C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = o.C04, C05 = o.C05, C06 = o.C06, C07 = o.C07, C08 = o.C08, 
                            D_Doesmeet = o.D_Doesmeet, D01 = o.D01, D02 = o.D02, 
                            H_Doesmeet = o.H_Doesmeet, H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = o.H04, H05 = o.H05, 
                            G_Doesmeet = o.G_Doesmeet, G01 = o.G01,                          
                        });
            }
            else if(workTable == "Check_Item_SelfDown")
            {
                Dou.Models.DB.IModelEntity<Check_Item_SelfDown> check_Item_SelfDown = new Dou.Models.DB.ModelEntity<Check_Item_SelfDown>(dbContext);

                iquery = check_Item_SelfDown.GetAll().Where(a => a.CheckNo != null)
                        .Join(check_Basic.GetAll().Where(a => a.Tank_Well == "1"), a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ResultDownloadController {
                            CheckNo = o.CheckNo,
                            CheckDate = c.CheckDate,
                            Business_theme = c.Business_theme,
                            Business_theme_Name = c.Business_theme_Name,
                            Gas_Name = c.Gas_Name,
                            A_Doesmeet = o.A_Doesmeet, A01 = o.A01, A02 = o.A02, A03 = o.A03, A04 = o.A04, 
                            B_Doesmeet = o.B_Doesmeet, B01 = o.B01, B02 = o.B02, B03 = o.B03, 
                            C_Doesmeet = o.C_Doesmeet, C01 = o.C01, C02 = o.C02, C03 = o.C03, C04 = o.C04, C05 = o.C05, C06 = o.C06, 
                            D_Doesmeet = o.D_Doesmeet, D01 = o.D01, D02 = o.D02, 
                            H_Doesmeet = o.H_Doesmeet, H01 = o.H01, H02 = o.H02, H03 = o.H03, H04 = o.H04, H05 = o.H05,
                            G_Doesmeet = o.G_Doesmeet, G01 = o.G01,                         
                        });
            }
            
            if (!string.IsNullOrEmpty(CheckDate_Start_Between_))
            {
                DateTime date = DateTime.Parse(CheckDate_Start_Between_);
                iquery = iquery.Where(a => a.CheckDate >= date);
                titles.Add("查核日期(起):" + CheckDate_Start_Between_);
            }

            if (!string.IsNullOrEmpty(CheckDate_End_Between_))
            {
                DateTime date = DateTime.Parse(CheckDate_End_Between_);
                iquery = iquery.Where(a => a.CheckDate <= date);
                titles.Add("查核日期(迄):" + CheckDate_End_Between_);
            }

            //代碼-營業主體
            if (!string.IsNullOrEmpty(Business_theme))
            {
                List<string> sels = Business_theme.Split(',').ToList();
                iquery = iquery.Where(a => sels.Any(b => a.Business_theme == b));

                //titles add
                //代碼-營業主體
                Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> dal = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                var text = dal.GetAll().Where(a => sels.Any(b => b == a.Value)).Select(a => a.Name).ToList();
                titles.Add("營業主體:" + string.Join(",", text));
            }

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();
            iquery = iquery.Where(a => a.CheckNo != ""
                    && pCitys.Any(b => a.CheckNo.Substring(0, 1) == b));

            //條件
            //and REPLACE(REPLACE(REPLACE(LEFT(i.CheckNo,1),'L','B'),'S','E'),'R','D') IN ('A','F','U')
            if (!string.IsNullOrEmpty(CityCode1))
            {
                var codes = CityCode1.Split(',').ToList();
                codes = Code.ConvertTwCity(codes);

                iquery = iquery.Where(a => a.CheckNo != ""
                    && codes.Any(b => a.CheckNo.Substring(0, 1) == b));

                //代碼-縣市別                
                Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                var citys = cityCode.GetAll().Where(a => codes.Any(b => b == a.CityCode1)).OrderBy(a => a.Rank);
                titles.Add("縣市:" + string.Join(",", citys.Select(a => a.CityName).ToList()));
            }

            return iquery;
        }

        private static string GetChkStr(int? num)
        {
            if (num == null)
                return "";

            return GetChkStr(num.ToString());
        }

        private static string GetChkStr(string str)
        {
            string result = "";

            if (str == null)
                return "";

            switch (str.Trim())
            {
                case "2":
                    result = "1";
                    break;
                case "0":
                    result = "/";
                    break;
                case "":
                    result = "/";
                    break;
            }

            return result;
        }
    }

    public class vw_Audit_ResultDownloadController : OilGas.Check_Item_Report
    {
        


        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectGearingWith = "Business_theme,CaseType,true",
            SelectItemsClassNamespace = OrganizationCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        public string CaseType { get; }

        [Display(Name = "查核區間", Order = 2)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Date, FilterAssign = FilterAssignType.Between)]
        public DateTime? CheckDate { get; set; }

        [Display(Name = "縣市別", Order = 3)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        public string CityCode1 { get; set; }

        [Display(Name = "營業主體", Order = 3)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = CarVehicleGas_BusinessOrganizationVSelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(70)]
        public string Business_theme { get; set; }
    }
}