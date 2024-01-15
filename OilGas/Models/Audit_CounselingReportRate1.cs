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
    using System.Web.Mvc;
    using System.Xml.Linq;

    public partial class Audit_CounselingReportRate1 
    {       
        [ColumnDef(Display = "講習會年度", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = CounselingYearSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string Counseling_Year { get; set; }

        [ColumnDef(Display = "查核年度", Visible = false, VisibleEdit = false, Sortable = true)]
        public string workYear { get; set; }

        [ColumnDef(Display = "縣市別", VisibleEdit = false, Sortable = true)]
        public string workItem { get; set; }

        [ColumnDef(Display = "縣市代碼", Visible = false, VisibleEdit = false, Sortable = true)]
        public string CityCode { get; set; }

        [ColumnDef(Display = "排序", Visible = false, VisibleEdit = false, Sortable = true)]
        public int? Rank { get; set; }

        [ColumnDef(Display = "GSL代碼", Visible = false, VisibleEdit = false, Sortable = true)]
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
}
