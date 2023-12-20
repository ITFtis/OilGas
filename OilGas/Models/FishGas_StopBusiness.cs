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

    public partial class FishGas_StopBusiness
    {
        [Key]
        [ColumnDef(Display = "�d�ߦ~��", EditType = EditType.Select,
            SelectItemsClassNamespace = YearSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false)]
        [NotMapped]
        public string Year_Start { get; set; }

        [ColumnDef(Display = "�����W��", Sortable = true)]
        [NotMapped]
        public string workCity { get; set; }

        [ColumnDef(Display = "�����N�X", Visible = false, Sortable = true)]
        [NotMapped]
        public string CityCode { get; set; }

        [ColumnDef(Display = "�����Ƨ�", Visible = false, Sortable = true)]
        [NotMapped]
        public int Rank { get; set; }

        [ColumnDef(Display = "GSL�N�X", Visible = false, Sortable = true)]
        [NotMapped]
        public string GSLCode { get; set; }

        [ColumnDef(Display = "�s�]", Sortable = true)]
        [NotMapped]
        public int AddBusiness { get; set; }

        [ColumnDef(Display = "�Ȱ���~", Sortable = true)]
        [NotMapped]
        public int StopBusiness { get; set; }

        [ColumnDef(Display = "��_��~", Sortable = true)]
        [NotMapped]
        public int ReBusiness { get; set; }

        [ColumnDef(Display = "���~", Sortable = true)]
        [NotMapped]
        public int EndBusiness { get; set; }

        [ColumnDef(Display = "��~", Visible = false, Sortable = true)]
        [NotMapped]
        public int Business { get; set; }

    }
}
