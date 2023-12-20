using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static OilGas.Controllers.Audit.Audit_ReportCheck_counts_CrossAnalysisController;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportCheck_counts_ImportantFactor", Name = "分級管理及重要因子交叉分析清單報表", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 4, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportCheck_counts_ImportantFactorController : AGenericModelController<vw_Audit_ReportCheck_counts_ImportantFactor>
    {
        // GET: Audit_ReportCheck_counts_ImportantFactor
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportCheck_counts_ImportantFactor> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportCheck_counts_ImportantFactor>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ReportCheck_counts_ImportantFactor> GetDataDBObject(IModelEntity<vw_Audit_ReportCheck_counts_ImportantFactor> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportCheck_counts_ImportantFactor>().AsQueryable();
            }

            return base.GetDataDBObject(dbEntity, paras);
        }

        public ActionResult ExportAudit_ReportCheck_counts_ImportantFactor(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_分級管理及重要因子交叉分析清單報表);
            string fileTitle = "查核輔導專區_分級管理及重要因子交叉分析清單報表";

            List<string> titles = new List<string>() { "查核輔導專區_分級管理及重要因子交叉分析清單報表，查詢條件:" };
            IQueryable<dynamic> result = GetOutputData(ref titles, paras);            

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();

            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.案件編號 = data.CaseNo;
                f.加油站名稱 = data.Gas_Name;
                f.營業主體 = data.Business_themeName;
                f.電話 = data.TelNo;
                f.加油站地址 = data.Address;
                f.負責人 = data.Boss;
                f.分級管理 = data.Grade;
                f.前一年是否查核 = data.LastYear;
                f.a2_4 = data.B02;
                f.a3_12 = data.C12;
                f.a4_7 = data.D07;
                f.a4_10 = data.D10;
                f.陰井油氣檢測有數值 = data.Detection;

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

        private IQueryable<object> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            Dou.Models.DB.IModelEntity<Check_Item> check_Item = new Dou.Models.DB.ModelEntity<Check_Item>(dbContext);
            Dou.Models.DB.IModelEntity<CarFuel_BasicData> carFuel_BasicData = new Dou.Models.DB.ModelEntity<CarFuel_BasicData>(dbContext);
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
            Dou.Models.DB.IModelEntity<Check_Basic> check_Basic = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);
            Dou.Models.DB.IModelEntity<Check_Tank_well> check_Tank_well = new Dou.Models.DB.ModelEntity<Check_Tank_well>(dbContext);

            //條件
            var CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            var ImportantCheckYaer = KeyValue.GetFilterParaValue(paras, "ImportantCheckYaer");
            var ImportantFactor = KeyValue.GetFilterParaValue(paras, "ImportantFactor");

            //必要條件
            titles.Add("預計查核名單年度:" + ImportantCheckYaer);

            //母表            
            DateTime EYear = DateTime.Parse((int.Parse(ImportantCheckYaer) + 1911).ToString() + "/01/01");
            DateTime SYearA = EYear.AddYears(-5);
            DateTime SYearB = EYear.AddYears(-4);
            DateTime SYearC = EYear.AddYears(-3);
            DateTime SYearD = EYear.AddYears(-2);
            DateTime SYearE = EYear.AddYears(-1);

            //Select CheckNo,B02,C12,D07,D10,'A' [Grade] From Check_Item with(nolock) where Checkdate >= '2010/1/1' and Checkdate < '2015/1/1' and AllDoesmeet = 0
            var main = check_Item.GetAll().Where(p => p.CheckDate >= SYearA && p.CheckDate < EYear).Where(p => p.AllDoesmeet == 0)
                            .Select(o => new {
                                Grade = "A", o.CheckNo, o.B02, o.C12, o.D07, o.D10
                            })
                            .Union(
                                check_Item.GetAll().Where(p => p.CheckDate >= SYearB && p.CheckDate < EYear).Where(p => p.AllDoesmeet >= 1 && p.AllDoesmeet <= 2)
                                .Select(o => new {
                                    Grade = "B", o.CheckNo, o.B02, o.C12, o.D07, o.D10
                                })
                            )
                            .Union(
                                check_Item.GetAll().Where(p => p.CheckDate >= SYearC && p.CheckDate < EYear).Where(p => p.AllDoesmeet >= 3 && p.AllDoesmeet <= 5)
                                .Select(o => new {
                                    Grade = "C", o.CheckNo, o.B02, o.C12, o.D07, o.D10
                                })
                            )
                            .Union(
                                check_Item.GetAll().Where(p => p.CheckDate >= SYearD && p.CheckDate < EYear).Where(p => p.AllDoesmeet >= 6 && p.AllDoesmeet <= 9)
                                .Select(o => new {
                                    Grade = "D", o.CheckNo, o.B02, o.C12, o.D07, o.D10
                                })
                            )
                            .Union(
                                check_Item.GetAll().Where(p => p.CheckDate >= SYearE && p.CheckDate < EYear).Where(p => p.AllDoesmeet > 10 )
                                .Select(o => new {
                                    Grade = "E", o.CheckNo, o.B02, o.C12, o.D07, o.D10
                                })
                            );
            
           var iquery = main.Join(check_Basic.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new
                                    {
                                        o.CheckNo, o.Grade, 
                                        B02 = o.B02 == "2" ? "是" : "否",
                                        C12 = o.C12 == "2" ? "是" : "否",
                                        D07 = o.D07 == "2" ? "是" : "否",
                                        D10 = o.D10 == "2" ? "是" : "否",
                                        c.CaseNo, c.CheckDate,
                                    });

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            iquery = iquery.Where(a => pCitys.Any(b => a.CaseNo.Substring(4, 2) == b));

            //條件
            if (!string.IsNullOrEmpty(CityCode1))
            {
                List<string> sels = CityCode1.Split(',').ToList();
                iquery = iquery.Where(a => sels.Any(b => a.CaseNo.Substring(4, 2) == b));

                //代碼-縣市別
                Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                titles.Add("縣市:" + string.Join(",", cityCode.GetAll().Where(a => sels.Any(b => a.GSLCode.IndexOf(b) > -1)).OrderBy(a => a.Rank).Select(a => a.CityName)));
            }

            var c2 = check_Basic.GetAll().Where(p => p.CheckDate >= SYearE && p.CheckDate < EYear);
            DateTime minDate = DateTime.Parse("1900/1/1");            

            var result = iquery.Join(carFuel_BasicData.GetAll(), a=>a.CaseNo, b=>b.CaseNo, (o, c)=> new
                                    {
                                        o.CheckNo, o.Grade, o.B02, o.C12, o.D07, o.D10, o.CaseNo, o.CheckDate,
                                        c.Gas_Name, c.Business_theme, c.otherBusiness_theme, c.TelNo, c.Address, c.Boss
                                    })
                                .GroupJoin(carVehicleGas_BusinessOrganization.GetAll().Where(p => p.IsEnable==true),
                                            a=>a.Business_theme, b=>b.Value, (o,c) => new { 
                                        o.CheckNo, o.Grade, o.B02, o.C12, o.D07, o.D10, o.CaseNo, o.CheckDate, o.Gas_Name, o.Business_theme, o.TelNo, o.Address, o.Boss,
                                        Business_themeName = c.FirstOrDefault() == null || o.Business_theme == "16" ? o.otherBusiness_theme : c.FirstOrDefault().Name
                                    })
                                .GroupJoin(check_Tank_well.GetAll(), a => a.CheckNo, b => b.CheckNo, (o , c) => new
                                {
                                    o.CheckNo, o.Grade, o.B02, o.C12, o.D07, o.D10, o.CaseNo, o.CheckDate, o.Gas_Name, o.Business_theme, o.TelNo, o.Address, o.Boss, o.Business_themeName,
                                    c
                                })
                                .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                                {
                                    o.CheckNo, o.Grade, o.B02, o.C12, o.D07, o.D10, o.CaseNo, o.CheckDate, o.Gas_Name, o.Business_theme, o.TelNo, o.Address, o.Boss, o.Business_themeName,                                    
                                    Detection = c == null || c.Detection <= 0 ? "否" : "是"
                                })
                                .GroupJoin(c2, a => a.CaseNo, b => b.CaseNo, (o, c) => new 
                                { 
                                    o.CheckNo, o.Grade, o.B02, o.C12, o.D07, o.D10, o.CaseNo, o.CheckDate, o.Gas_Name, o.Business_theme, o.TelNo, o.Address, o.Boss, o.Business_themeName, o.Detection,                                    
                                    LastYear = c.FirstOrDefault() == null || c.FirstOrDefault().CheckDate <= minDate ? "否" : "是"
                                })                                
                                .Where(a => !string.IsNullOrEmpty(a.CaseNo))
                                .Distinct();
            
            if (!string.IsNullOrEmpty(ImportantFactor))
            {
                List<string> sels = ImportantFactor.Split(',').ToList();

                result = result.Where(a => (sels.Contains("1") && a.B02 == "是")
                                        || (sels.Contains("2") && a.C12 == "是")
                                        || (sels.Contains("3") && a.D07 == "是")
                                        || (sels.Contains("4") && a.D10 == "是")
                                        || (sels.Contains("5") && a.Detection == "是"));

                //代碼-重要因子
                var codes = Code.GetImportantFactor();
                titles.Add("重要因子:" + string.Join(",", codes.Where(a => sels.Any(b => b == a.Key)).Select(a => a.Value)));
            }

            //預設排序 (就系統 -> 無)
            result = result.OrderBy(a => a.CheckDate).ThenBy(a => a.CaseNo);

            ////var zr = result.ToList();

            return result;
        }
    }

    public class vw_Audit_ReportCheck_counts_ImportantFactor
    {
        [Display(Name = "縣市別", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
               Filter = true, SelectItemsClassNamespace = UsercitySelectItemsClassImp.AssemblyQualifiedName)]
        public string CityCode1 { get; }

        [Display(Name = "預計查核年", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.ImportantCheckYaerSelectItems.AssemblyQualifiedName)]
        public string ImportantCheckYaer { get; }

        [Display(Name = "重要因子", Order = 3)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = GetImportantFactorSelectItems.AssemblyQualifiedName)]
        public string ImportantFactor { get; set; }
    }
}