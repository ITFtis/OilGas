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

    public partial class Audit_CounselingReportMissing2
    {

        [ColumnDef(Display = "���߷|�~��", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = CounselingYearSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string Counseling_Year { get; set; }

        [ColumnDef(Display = "�d�ֻ��ɦ~��", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = CheckYearSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string CheckYear { get; set; }

        [Key]
        [ColumnDef(Display = "���ΧO", VisibleEdit = false, Sortable = false)]
        public string workItem { get; set; }

        [ColumnDef(Display = "�����N�X", Visible = false, VisibleEdit = false, Sortable = false)]
        public string CityCode { get; set; }

        [ColumnDef(Display = "�Ƨ�", Visible = false, VisibleEdit = false, Sortable = false)]
        public int? Rank { get; set; }

        [ColumnDef(Display = "GSL�N�X", Visible = false, VisibleEdit = false, Sortable = false)]
        public string GSLCode { get; set; }

        [ColumnDef(Display = "�X�u��", VisibleEdit = false, Sortable = false)]
        public int? AttendSCount1 { get; set; }

        [ColumnDef(Display = "�Ҥ��[�o��", VisibleEdit = false, Sortable = false)]
        public int? DenominatorCount1 { get; set; }

        [ColumnDef(Display = "�X�u�v(��)", VisibleEdit = false, Sortable = false)]
        public decimal? CounselingRate1 { get; set; }

        [ColumnDef(Display = "�X�u��", VisibleEdit = false, Sortable = false)]
        public int? AttendSCount2 { get; set; }

        [ColumnDef(Display = "�Ҥ��[�o��", VisibleEdit = false, Sortable = false)]
        public int? DenominatorCount2 { get; set; }

        [ColumnDef(Display = "�X�u�v(��)", VisibleEdit = false, Sortable = false)]
        public decimal? CounselingRate2 { get; set; }

        [ColumnDef(Display = "�`�ʥ���", VisibleEdit = false, Sortable = false)]
        public int? SumCheckCount1 { get; set; }

        [ColumnDef(Display = "�d�֥[�o��", VisibleEdit = false, Sortable = false)]
        public int? SumCheckError1 { get; set; }

        [ColumnDef(Display = "�����ʥ���", VisibleEdit = false, Sortable = false)]
        public decimal? Average1 { get; set; }

        [ColumnDef(Display = "�`�ʥ���", VisibleEdit = false, Sortable = false)]
        public int? SumCheckCount2 { get; set; }

        [ColumnDef(Display = "�d�֥[�o��", VisibleEdit = false, Sortable = false)]
        public int? SumCheckError2 { get; set; }

        [ColumnDef(Display = "�����ʥ���", VisibleEdit = false, Sortable = false)]
        public decimal? Average2 { get; set; }
    }
}
