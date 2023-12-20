namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Web.Mvc;

    public partial class Audit_ReportMissing_statistics_County
    {
        [Key]
        [ColumnDef(Display = "石油設施類型", Visible = false, Filter = true, EditType = EditType.Select,
            SelectGearingWith = "workYear,CaseType,true",
            SelectItemsClassNamespace = OrganizationCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string CaseType { get; set; }

        [ColumnDef(Display = "查詢年度", Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = AuditReportMissingStatisticsCountyYearsSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string workYear { get; set; }

        [ColumnDef(Display = "縣市", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        [NotMapped]
        public string CityCode1 { get; set; }

        [ColumnDef(Display = "檢查設備名稱", Sortable = true)]
        public string CheckItemTitel { get; set; }

        [ColumnDef(Display = "檢查項目", Sortable = true)]
        public int CheckItemCount { get; set; }

        [ColumnDef(Display = "缺失數(項次)", Sortable = true)]
        public int CheckItemErrCount { get; set; }

    }

    //view(客製化)
    public class AuditReportMissingStatisticsCountyYearsSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.AuditReportMissingStatisticsCountyYearsSelectItemsClassImp, OilGas";

        static IEnumerable<CaseTypeToWorkYear> _CW;
        public static IEnumerable<CaseTypeToWorkYear> CW
        {
            get
            {
                if (_CW == null || _CW.Count() == 0)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<Check_Basic> model = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);

                    var datas = model.GetAll().Where(a => a.CaseType == "CarFuel_BasicData").Select(a => new {
                        CaseType = a.CaseType,
                        CheckDate =  a.CheckDate,
                    }).Concat(model.GetAll().Where(a => a.CaseType == "FishGas_BasicData").Select(a => new {
                        CaseType = a.CaseType,
                        CheckDate = a.CheckDate,
                    })).Concat(model.GetAll().Where(a => a.CaseType == "SelfFuel_Basic").Select(a => new {
                        CaseType = a.CaseType + "_Up",
                        CheckDate = a.CheckDate,
                    })).Concat(model.GetAll().Where(a => a.CaseType == "SelfFuel_Basic").Select(a => new {
                        CaseType = a.CaseType + "_Down",
                        CheckDate = a.CheckDate,
                    })).ToList();

                    var datas2 = datas.Select(a => new
                    {
                        CaseType = a.CaseType,
                        workYear = a.CheckDate != null ? (int.Parse(a.CheckDate.ToString().Substring(0, 4)) - 1911).ToString() : "",
                    }).GroupBy(a => new { CaseType = a.CaseType, workYear = a.workYear }).ToList();

                    _CW = datas2.Select(a => new CaseTypeToWorkYear
                    {
                        CaseType = a.Key.CaseType,
                        workYear = a.Key.workYear
                    }).OrderBy(a => Convert.ToInt16(a.workYear));
                }
                return _CW;
            }
        }

        public static void Reset()
        {
            _CW = null;
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var result = CW.Select(s => new KeyValuePair<string, object>(string.Format("{0}_{1}", s.workYear, s.CaseType),
                "{\"v\":\"" + s.workYear + "\",\"CaseType\":\"" + s.CaseType + "\"}"));
            return result;
        }
    }

    public class CaseTypeToWorkYear
    {
        public string CaseType { get; set;}
        public string workYear { get; set;}
    }
}
