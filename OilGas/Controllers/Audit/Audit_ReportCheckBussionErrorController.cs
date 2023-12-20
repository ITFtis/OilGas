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

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportCheckBussionError", Name = "歷年各集團加油站檢查缺失統計", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 9, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportCheckBussionErrorController : AGenericModelController<vw_Audit_ReportCheckBussionError>
    {
        // GET: Audit_ReportCheckBussionError
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportCheckBussionError> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportCheckBussionError>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ReportCheckBussionError> GetDataDBObject(IModelEntity<vw_Audit_ReportCheckBussionError> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportCheckBussionError>().AsQueryable();
            }

            return base.GetDataDBObject(dbEntity, paras);
        }

        public ActionResult ExportAudit_ReportCheckBussionError(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_歷年各集團加油站檢查缺失統計);
            string fileTitle = "查核輔導專區_歷年各集團加油站檢查缺失統計";

            List<string> titles = new List<string>() { "查核輔導專區_歷年各集團加油站檢查缺失統計，查詢條件:" };
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

            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganizationV> carVehicleGas_BusinessOrganizationV = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganizationV>(dbContext);                        
            Dou.Models.DB.IModelEntity<Check_Basic> check_Basic = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);

            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var SYear = KeyValue.GetFilterParaValue(paras, "SYear");
            var EYear = KeyValue.GetFilterParaValue(paras, "EYear");

            titles.Add("石油設施類型:" + Code.GetCaseType().Where(a => a.Key == CaseType).FirstOrDefault().Value);
            titles.Add("查核年度:" + SYear + "~" + EYear);

            //統計            
            var iquery = check_Basic.GetAll().Select(a => new {
                a.CaseType, a.Business_theme, a.CaseNo, a.CheckNo,
                CheckYear = a.CheckDate == null ? 0 : ((DateTime)a.CheckDate).Year - 1911
            });

            //條件
            iquery = iquery.Where(a => a.CaseType == CaseType);

            int intSYear = int.Parse(SYear);
            int intEYear = int.Parse(EYear);
            iquery = iquery.Where(a => a.CheckYear >= intSYear);
            iquery = iquery.Where(a => a.CheckYear <= intEYear);
            
            List<int> years = new List<int>();
            for (int i = intSYear; i <= intEYear; i++)
                years.Add(i);

            //該項目有缺失家數 母體各年度(source)	動態資料表(check_Item...)
            List<Check5Class> datas = new List<Check5Class>();            
            foreach (var WorkYear in years)
            {
                var it = Code.GetCaseType3().Where(a => a.Key == CaseType);
                string strCase = CaseType;
                if(it.Count() > 0)
                {
                    strCase = it.FirstOrDefault().Value.ToString();
                }

                string workTable = Code.GetWorkTable(strCase, WorkYear);
                
                IQueryable<OilGas.Check_Item_Report> tmp = null;
                if (workTable == "Check_Item")
                {
                    Dou.Models.DB.IModelEntity<Check_Item> check_Item = new Dou.Models.DB.ModelEntity<Check_Item>(dbContext);

                    tmp = check_Item.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {
                                CheckYear = c.CheckYear,
                                CaseNo = c.CaseNo,
                                Business_theme = c.Business_theme,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet
                            }).Where(a => a.CheckYear == WorkYear);
                }
                else if (workTable == "Check_Item_97")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_97> check_Item_97 = new Dou.Models.DB.ModelEntity<Check_Item_97>(dbContext);

                    tmp = check_Item_97.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {
                                CheckYear = c.CheckYear,
                                CaseNo = c.CaseNo,
                                Business_theme = c.Business_theme,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet
                            }).Where(a => a.CheckYear == WorkYear);
                }
                else if (workTable == "Check_Item_Fish")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_Fish> check_Item_Fish = new Dou.Models.DB.ModelEntity<Check_Item_Fish>(dbContext);

                    tmp = check_Item_Fish.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {
                                CheckYear = c.CheckYear,
                                CaseNo = c.CaseNo,
                                Business_theme = c.Business_theme,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet
                            }).Where(a => a.CheckYear == WorkYear);
                }
                else if (workTable == "Check_Item_Fish103")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_Fish103> check_Item_Fish103 = new Dou.Models.DB.ModelEntity<Check_Item_Fish103>(dbContext);

                    tmp = check_Item_Fish103.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {
                                CheckYear = c.CheckYear,
                                CaseNo = c.CaseNo,
                                Business_theme = c.Business_theme,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet
                            }).Where(a => a.CheckYear == WorkYear);
                }
                else if (workTable == "Check_Item_SelfUP")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_SelfUP> check_Item_SelfUP = new Dou.Models.DB.ModelEntity<Check_Item_SelfUP>(dbContext);

                    tmp = check_Item_SelfUP.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {
                                CheckYear = c.CheckYear,
                                CaseNo = c.CaseNo,
                                Business_theme = c.Business_theme,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet
                            }).Where(a => a.CheckYear == WorkYear);
                }
                else if (workTable == "Check_Item_SelfDown")
                {
                    Dou.Models.DB.IModelEntity<Check_Item_SelfDown> check_Item_SelfDown = new Dou.Models.DB.ModelEntity<Check_Item_SelfDown>(dbContext);

                    tmp = check_Item_SelfDown.GetAll().Where(a => a.CheckNo != null)
                            .Join(iquery, a => a.CheckNo, b => b.CheckNo, (o, c) => new OilGas.Check_Item_Report
                            {
                                CheckYear = c.CheckYear,
                                CaseNo = c.CaseNo,
                                Business_theme = c.Business_theme,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : (int)o.AllDoesmeet
                            }).Where(a => a.CheckYear == WorkYear);
                }                

                var info = tmp.GroupBy(a => new { a.CheckYear, a.Business_theme })
                                .Select(a => new Check5Class
                                {
                                    CheckYear = (int)a.Key.CheckYear,
                                    Business_theme = a.Key.Business_theme,
                                    CheckCount = a.Count(),
                                    CheckAllDoesmeet = a.Sum(p => p.AllDoesmeet),
                                    CheckNoHiatusCount = a.Sum(p => p.AllDoesmeet == 0 ? 1 : 0)
                                }).ToList();

                datas.AddRange(info);
            }

            //結果
            List<dynamic> result = new List<dynamic>();
            var cars = carVehicleGas_BusinessOrganizationV.GetAll().Where(a => a.CaseType == CaseType)
                                        .OrderBy(a => a.Rank);
            foreach (var car in cars)
            {
                dynamic f = new ExpandoObject();
                f.集團名稱 = car.Name;

                foreach (var year in years)
                {
                    var data = datas.Where(a => a.Business_theme == car.Value && a.CheckYear == year);
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

    public class vw_Audit_ReportCheckBussionError
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