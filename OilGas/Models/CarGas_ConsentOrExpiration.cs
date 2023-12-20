namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarGas_ConsentOrExpiration
    {
        [ColumnDef(Display = "������", EditType = EditType.Date, Filter = true,
            FilterAssign = FilterAssignType.Between, Visible = false)]
        [NotMapped]
        public DateTime? Mod_date { get; set; }

        [Display(Name = "�����O", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY { get; set; }

        [ColumnDef(Display = "��B�p�A", EditType = EditType.Select, SelectItems = "{'1':'�P�N�{�w','2':'�P�N�w��'}",
            DefaultValue = "1", Required = true, Filter = true, Visible = false)]
        [NotMapped]
        public string CaseType { get; set; }

        [Display(Name = "�ץ�s��", Order = 1)]
        [NotMapped]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "�����O", Sortable = true)]
        [NotMapped]
        public string CityName { get; set; }

        [ColumnDef(Display = "������", Sortable = true)]
        [NotMapped]
        public string Recipient_date { get; set; }

        [ColumnDef(Display = "�W��", Sortable = true)]
        [NotMapped]
        public string Gas_Name { get; set; }

        [ColumnDef(Display = "��B���p", Sortable = true)]
        [NotMapped]
        public string Name_Operations { get; set; }

        [ColumnDef(Display = "�o����", Sortable = true)]
        [NotMapped]
        public string Dispatch_date { get; set; }

        [ColumnDef(Display = "������", Sortable = true)]
        [NotMapped]
        public string Build_Deadline { get; set; }

        [ColumnDef(Display = "�g�a�ϥΤ���", Sortable = true)]
        [NotMapped]
        public string Name_LandUse { get; set; }

        [ColumnDef(Display = "�g�a���O", Sortable = true)]
        [NotMapped]
        public string Name_LandType { get; set; }
    }
}
