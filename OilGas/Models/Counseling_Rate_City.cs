namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Counseling_Rate_City
    {
        [ColumnDef(Display = "講習會年度", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = CounselingYearSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string Counseling_Year { get; set; }

        [Key]
        [ColumnDef(Display = "查核年度", Visible = false, VisibleEdit = false, Sortable = true)]
        [Column(Order = 0)]
        [StringLength(4)]
        public string workYear { get; set; }

        [Key]
        [ColumnDef(Display = "縣市別", VisibleEdit = false, Sortable = true)]
        [Column(Order = 1)]
        [StringLength(5)]
        public string workItem { get; set; }

        [ColumnDef(Display = "縣市代碼", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(50)]
        public string CityCode { get; set; }

        [ColumnDef(Display = "排序", Visible = false, VisibleEdit = false, Sortable = true)]
        public int? Rank { get; set; }

        [ColumnDef(Display = "GSL代碼", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(10)]
        public string GSLCode { get; set; }

        [ColumnDef(Display = "出席人數", VisibleEdit = false, Sortable = true)]
        public int? AttendPCount { get; set; }

        [ColumnDef(Display = "出席站", VisibleEdit = false, Sortable = true)]
        public int? AttendSCount { get; set; }

        [ColumnDef(Display = "轄內加油站", Sortable = true)]
        public int? DenominatorCount { get; set; }

        [ColumnDef(Display = "出席率(人)", VisibleEdit = false, Sortable = true)]
        public decimal? Average1 { get; set; }

        [ColumnDef(Display = "出席率(站)", VisibleEdit = false, Sortable = true)]
        public decimal? Average2 { get; set; }
    }

    public class CounselingYearSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.CounselingYearSelectItemsClassImp, OilGas";

        protected static IEnumerable<lsYear> _years;
        protected static IEnumerable<lsYear> YEARS
        {
            get
            {
                _years = DouHelper.Misc.GetCache<IEnumerable<lsYear>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_years == null)
                {
                    var tmpyear = Rpt_CarFuel_Land.GetAllCounselingData().Select(x => x.s_year).Distinct();
                    int nowYear = DateTime.Now.Year;
                    List<lsYear> lsYear = new List<lsYear>();

                    foreach (var year in tmpyear)
                    {
                        lsYear.Add(new lsYear { Text = year.ToString(), Value = int.Parse(year) });
                    };

                    _years = lsYear;
                    DouHelper.Misc.AddCache(_years, AssemblyQualifiedName);
                }
                return _years;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return YEARS.Select(s => new KeyValuePair<string, object>(s.Text, s.Value));
        }
    }
}
