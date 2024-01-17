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
        [ColumnDef(Display = "���߷|�~��", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = CounselingYearSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string Counseling_Year { get; set; }

        [ColumnDef(Display = "�d�֦~��", Visible = false, VisibleEdit = false, Sortable = true)]
        public string workYear { get; set; }

        [ColumnDef(Display = "�����O", VisibleEdit = false, Sortable = true)]
        public string workItem { get; set; }

        [ColumnDef(Display = "�����N�X", Visible = false, VisibleEdit = false, Sortable = true)]
        public string CityCode { get; set; }

        [ColumnDef(Display = "�Ƨ�", Visible = false, VisibleEdit = false, Sortable = true)]
        public int? Rank { get; set; }

        [ColumnDef(Display = "GSL�N�X", Visible = false, VisibleEdit = false, Sortable = true)]
        public string GSLCode { get; set; }

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
