namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using Newtonsoft.Json;
    using NPOI.SS.Formula.Functions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Util;
    using System.Xml.Linq;

    public partial class Audit_CounselingReportMissing1
    {

        [ColumnDef(Display = "講習會年度", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = CounselingYearSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string Counseling_Year { get; set; }

        [ColumnDef(Display = "查核輔導年度", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = CheckYearSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string CheckYear { get; set; }

        [Key]
        [ColumnDef(Display = "縣市別", VisibleEdit = false, Sortable = false)]
        public string workItem { get; set; }

        [ColumnDef(Display = "縣市代碼", Visible = false, VisibleEdit = false, Sortable = false)]
        public string CityCode { get; set; }

        [ColumnDef(Display = "排序", Visible = false, VisibleEdit = false, Sortable = false)]
        public int? Rank { get; set; }

        [ColumnDef(Display = "GSL代碼", Visible = false, VisibleEdit = false, Sortable = false)]
        public string GSLCode { get; set; }

        [ColumnDef(Display = "出席站", VisibleEdit = false, Sortable = false)]
        public int? AttendSCount1 { get; set; }

        [ColumnDef(Display = "轄內加油站", VisibleEdit = false, Sortable = false)]
        public int? DenominatorCount1 { get; set; }

        [ColumnDef(Display = "出席率(站)", VisibleEdit = false, Sortable = false)]
        public decimal? CounselingRate1 { get; set; }

        [ColumnDef(Display = "出席站", VisibleEdit = false, Sortable = false)]
        public int? AttendSCount2 { get; set; }

        [ColumnDef(Display = "轄內加油站", VisibleEdit = false, Sortable = false)]
        public int? DenominatorCount2 { get; set; }

        [ColumnDef(Display = "出席率(站)", VisibleEdit = false, Sortable = false)]
        public decimal? CounselingRate2 { get; set; }

        [ColumnDef(Display = "總缺失數", VisibleEdit = false, Sortable = false)]
        public int? SumCheckCount1 { get; set; }

        [ColumnDef(Display = "查核加油站", VisibleEdit = false, Sortable = false)]
        public int? SumCheckError1 { get; set; }

        [ColumnDef(Display = "平均缺失數", VisibleEdit = false, Sortable = false)]
        public decimal? Average1 { get; set; }

        [ColumnDef(Display = "總缺失數", VisibleEdit = false, Sortable = false)]
        public int? SumCheckCount2 { get; set; }

        [ColumnDef(Display = "查核加油站", VisibleEdit = false, Sortable = false)]
        public int? SumCheckError2 { get; set; }

        [ColumnDef(Display = "平均缺失數", VisibleEdit = false, Sortable = false)]
        public decimal? Average2 { get; set; }
    }

    public class CheckYearSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.CheckYearSelectItemsClassImp, OilGas";

        protected static IEnumerable<lsYear> _years;
        protected static IEnumerable<lsYear> YEARS
        {
            get
            {
                _years = DouHelper.Misc.GetCache<IEnumerable<lsYear>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_years == null)
                {
                    var tmpyear = Rpt_CarFuel_Land.GetAllCheck_Basic().Select(x => GetYear(x.CheckDate)).Distinct();
                    int nowYear = DateTime.Now.Year;
                    List<lsYear> lsYear = new List<lsYear>();

                    foreach (var year in tmpyear)
                    {
                        lsYear.Add(new lsYear { Text = year.ToString(), Value = year });
                    };

                    _years = lsYear.OrderBy(x=>x.Text);
                    DouHelper.Misc.AddCache(_years, AssemblyQualifiedName);
                }
                return _years;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return YEARS.Select(s => new KeyValuePair<string, object>(s.Text, s.Value));
        }

        private static int GetYear(DateTime? d1)
        {
            if (d1 != null)
                return d1.Value.Year;
            return 0;
        }
    }
}
