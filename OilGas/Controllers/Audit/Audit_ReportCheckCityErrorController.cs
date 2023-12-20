using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportCheckCityError", Name = "歷年各縣市石油設施檢查缺失統計", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 8, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportCheckCityErrorController : AGenericModelController<vw_Audit_ReportCheckCityError>
    {
        // GET: Audit_ReportCheckCityError
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportCheckCityError> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportCheckCityError>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ReportCheckCityError> GetDataDBObject(IModelEntity<vw_Audit_ReportCheckCityError> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportCheckCityError>().AsQueryable();
            }

            return base.GetDataDBObject(dbEntity, paras);
        }

        public ActionResult ExportAudit_ReportCheckCityError(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_歷年各縣市石油設施檢查缺失統計);
            string fileTitle = "查核輔導專區_歷年各縣市石油設施檢查缺失統計";

            List<string> titles = new List<string>() { "查核輔導專區_歷年各縣市石油設施檢查缺失統計，查詢條件:" };
            IEnumerable<dynamic> result = GetOutputData(ref titles, paras);

            //產出Dynamic資料 (給Excel)
            var list = result.ToList();

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            foreach (var row in list)
            {
                var f = row;
                f.SheetName = fileTitle;//sheep.名稱;
            }

            //產出excel            
            string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF1(fileTitle, titles, list, folder, "Y");
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

        private IEnumerable<dynamic> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
            Dou.Models.DB.IModelEntity<Check_Basic_View> check_Basic_View = new Dou.Models.DB.ModelEntity<Check_Basic_View>(dbContext);

            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var SYear = KeyValue.GetFilterParaValue(paras, "SYear");
            var EYear = KeyValue.GetFilterParaValue(paras, "EYear");

            titles.Add("石油設施類型:" + Code.GetCaseType().Where(a => a.Key == CaseType).FirstOrDefault().Value);
            titles.Add("查核年度:" + SYear + "~" + EYear);

            //統計            
            var iquery = check_Basic_View.GetAll().Select(a => new {
                a.CaseType, a.AreaCode, a.CaseNo, a.AllDoesmeet,
                CheckYear = a.CheckDate == null ? 0 : ((DateTime)a.CheckDate).Year - 1911
            });

            //條件
            iquery = iquery.Where(a => a.CaseType == CaseType);

            int intSYear = int.Parse(SYear);
            int intEYear = int.Parse(EYear);
            iquery = iquery.Where(a => a.CheckYear >= intSYear);
            iquery = iquery.Where(a => a.CheckYear <= intEYear);

            //datas group
            var datas = iquery.GroupBy(a => new { a.CheckYear, a.AreaCode })
                            .Select(a => new
                            {
                                a.Key.CheckYear,
                                a.Key.AreaCode,
                                CheckCount = a.Count(),
                                CheckAllDoesmeet = a.Sum(p => p.AllDoesmeet == null ? 0 : p.AllDoesmeet),
                                CheckNoHiatusCount = a.Sum(p => p.AllDoesmeet == null || p.AllDoesmeet == 0 ? 1 : 0)
                            }).ToList();

            //結果
            List<dynamic> result = new List<dynamic>();
            //各年度(Cross Join)
            var citys = cityCode.GetAll().OrderBy(a => a.Rank).ToList();
            List<int> years = new List<int>();
            for (int i = intSYear; i <= intEYear; i++)
                years.Add(i);            

            foreach (var city in citys)
            {
                dynamic f = new ExpandoObject();
                f.縣市別 = city.CityName;

                foreach(int year in years)
                {
                    var data = datas.Where(a => a.AreaCode == city.CityCode1 && a.CheckYear == year);

                    if (data.Count() > 0)
                    {
                        foreach (var row in data)
                        {
                            ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年查核家數", row.CheckCount));
                            ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年查核缺失數", row.CheckAllDoesmeet));
                            ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年零缺失家數", row.CheckNoHiatusCount));

                            double rate = Math.Round((double)row.CheckNoHiatusCount / (int)row.CheckCount * 100, 2);
                            ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年零缺失比例", rate.ToString() + "%"));
                        }
                    }
                    else
                    {
                        ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年查核家數", 0));
                        ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年查核缺失數", 0));
                        ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年零缺失家數", 0));
                        ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年零缺失比例", "0%"));
                    }
                }

                result.Add(f);
            }

            return result;
        }
    }

    public class vw_Audit_ReportCheckCityError
    {
        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
             SelectItemsClassNamespace = ReportCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        public string CaseType { get; }

        [Display(Name = "查詢年(起)", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.CheckYear2SelectItems.AssemblyQualifiedName)]
        public int SYear { get; }

        [Display(Name = "查詢年(迄)", Order = 3)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
        Filter = true, SelectItemsClassNamespace = OilGas.CheckYear2SelectItems.AssemblyQualifiedName)]
        public int EYear { get; }
    }
}