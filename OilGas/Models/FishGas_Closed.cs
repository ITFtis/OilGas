namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_FishGas_Closed
    {
        [ColumnDef(Display = "查詢期間-類別", EditType = EditType.Select, SelectItems = "{'1':'現況資料修改時間','2':'變更歷程資料時間'}",
            DefaultValue = "1", Required = true, Filter = true, Visible = false)]
        public string CaseType { get; set; }

        [ColumnDef(Display = "縣市別", Sortable = true)]
        public string CityName { get; set; }

        [ColumnDef(Display = "縣市排序", Sortable = true)]
        public int CityRank { get; set; }

        [ColumnDef(Display = "查詢時間", EditType = EditType.Date, Filter = true, 
            FilterAssign = FilterAssignType.Between, Visible = false)]
        public DateTime? Mod_date { get; set; }

        [Key]
        [ColumnDef(Display = "案件編號", Sortable = true)]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "中油", Sortable = true)]
        public int cpc { get; set; }

        [ColumnDef(Display = "中油-暫停營業", Sortable = true)]
        public int cpc_closed { get; set; }

        [ColumnDef(Display = "非中油", Sortable = true)]
        public int notcpc { get; set; }

        [ColumnDef(Display = "非中油-暫停營業", Sortable = true)]
        public int notcpc_closed { get; set; }
    }

    public partial class vw_FishGas_Closed_show
    {
        [ColumnDef(Display = "查詢期間-類別", EditType = EditType.Select, SelectItems = "{'1':'現況資料修改時間','2':'變更歷程資料時間'}",
            DefaultValue = "1", Required = true, Filter = true, Visible = false)]
        public string CaseType { get; set; }

        [Display(Name = "縣市", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY { get; set; }
        [ColumnDef(Display = "查詢時間", EditType = EditType.Date, Filter = true,
            FilterAssign = FilterAssignType.Between, Visible = false)]
        [NotMapped]
        public DateTime? Mod_date { get; set; }

        [ColumnDef(Display = "縣市別", Sortable = true)]
        public string CityName { get; set; }

        [ColumnDef(Display = "中油(己開業扣除暫停營業)", Sortable = true)]
        public int cpc { get; set; }

        [ColumnDef(Display = "中油暫停營業", Sortable = true)]
        public int cpc_closed { get; set; }

        [ColumnDef(Display = "非中油(己開業扣除暫停營業)", Sortable = true)]
        public int notcpc { get; set; }

        [ColumnDef(Display = "非中油暫停營業", Sortable = true)]
        public int notcpc_closed { get; set; }

        [ColumnDef(Display = "總計", Sortable = true)]
        public int tv { get; set; }

    }
}
