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
        [ColumnDef(Display = "���߷|�~��", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = CounselingYearSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string Counseling_Year { get; set; }

        [Key]
        [Column(Order = 0)]
        [ColumnDef(Display = "�d�֦~��", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(4)]
        public string workYear { get; set; }

        [Key]
        [Column(Order = 1)]
        [ColumnDef(Display = "���ΦW��", VisibleEdit = false, Sortable = true)]
        [StringLength(20)]
        public string workItem { get; set; }

        [ColumnDef(Display = "���ΥN�X", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(20)]
        public string workCode { get; set; }

        [ColumnDef(Display = "�Ƨ�", Visible = false, VisibleEdit = false, Sortable = true)]
        public int? Rank { get; set; }

        [ColumnDef(Display = "²��", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(4)]
        public string ShortName { get; set; }

        [ColumnDef(Display = "GSL�N�X", Visible = false, VisibleEdit = false, Sortable = true)]
        [StringLength(2)]
        public string OldCode { get; set; }

        [ColumnDef(Display = "�X�u�H��", VisibleEdit = false, Sortable = true)]
        public int? AttendPCount { get; set; }

        [ColumnDef(Display = "�X�u��", VisibleEdit = false, Sortable = true)]
        public int? AttendSCount { get; set; }

        [ColumnDef(Display = "�Ҥ��[�o��", Sortable = true)]
        public int? DenominatorCount { get; set; }

        [ColumnDef(Display = "�X�u�v(�H)", VisibleEdit = false, Sortable = true)]
        public decimal? Average1 { get; set; }

        [ColumnDef(Display = "�X�u�v(��)", VisibleEdit = false, Sortable = true)]
        public decimal? Average2 { get; set; }
    }
}
