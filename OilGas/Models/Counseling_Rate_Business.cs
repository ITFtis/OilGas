namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Counseling_Rate_Business
    {
        [ColumnDef(Display = "講習會年度", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = CounselingYearSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string Counseling_Year { get; set; }

        [Key]
        [Column(Order = 0)]
        [ColumnDef(Display = "查核年度", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(4)]
        public string workYear { get; set; }

        [Key]
        [Column(Order = 1)]
        [ColumnDef(Display = "集團名稱", VisibleEdit = false, Sortable = true)]
        [StringLength(20)]
        public string workItem { get; set; }

        [ColumnDef(Display = "集團代碼", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(20)]
        public string workCode { get; set; }

        [ColumnDef(Display = "排序", Visible = false, VisibleEdit = false, Sortable = true)]
        public int? Rank { get; set; }

        [ColumnDef(Display = "簡稱", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(4)]
        public string ShortName { get; set; }

        [ColumnDef(Display = "GSL代碼", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(2)]
        public string OldCode { get; set; }

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
