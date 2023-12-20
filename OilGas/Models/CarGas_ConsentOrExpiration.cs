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
        [ColumnDef(Display = "到期日期", EditType = EditType.Date, Filter = true,
            FilterAssign = FilterAssignType.Between, Visible = false)]
        [NotMapped]
        public DateTime? Mod_date { get; set; }

        [Display(Name = "縣市別", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY { get; set; }

        [ColumnDef(Display = "營運況態", EditType = EditType.Select, SelectItems = "{'1':'同意認定','2':'同意籌建'}",
            DefaultValue = "1", Required = true, Filter = true, Visible = false)]
        [NotMapped]
        public string CaseType { get; set; }

        [Display(Name = "案件編號", Order = 1)]
        [NotMapped]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "縣市別", Sortable = true)]
        [NotMapped]
        public string CityName { get; set; }

        [ColumnDef(Display = "收件日期", Sortable = true)]
        [NotMapped]
        public string Recipient_date { get; set; }

        [ColumnDef(Display = "名稱", Sortable = true)]
        [NotMapped]
        public string Gas_Name { get; set; }

        [ColumnDef(Display = "營運狀況", Sortable = true)]
        [NotMapped]
        public string Name_Operations { get; set; }

        [ColumnDef(Display = "發文日期", Sortable = true)]
        [NotMapped]
        public string Dispatch_date { get; set; }

        [ColumnDef(Display = "到期日期", Sortable = true)]
        [NotMapped]
        public string Build_Deadline { get; set; }

        [ColumnDef(Display = "土地使用分區", Sortable = true)]
        [NotMapped]
        public string Name_LandUse { get; set; }

        [ColumnDef(Display = "土地類別", Sortable = true)]
        [NotMapped]
        public string Name_LandType { get; set; }
    }
}
