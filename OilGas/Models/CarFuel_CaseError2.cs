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

    public partial class CarFuel_CaseError2
    {   
        [Key]
        [ColumnDef(Display = "項次", Sortable = true)]
        public string CaseIndex { get; set; }

        [ColumnDef(Display = "案件編號", Sortable = true)]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "石油設施", Sortable = true)]
        public string Gas_Name { get; set; }

        [ColumnDef(Display = "營業主體集團", Sortable = true)]
        public string Business_theme { get; set; }

        [ColumnDef(Display = "營業主體名稱", Sortable = true)]
        public string Other_Business_theme { get; set; }

        [ColumnDef(Display = "石油設施地址", Sortable = true)]
        public string Address { get; set; }

        [ColumnDef(Display = "異動日期", Sortable = true)]
        public DateTime? Mod_date { get; set; }

        [ColumnDef(Display = "最後營運狀態", Visible = false, Sortable = true)]
        public string UsageState { get; set; }

        [ColumnDef(Display = "最後營運狀態", Sortable = true)]
        public string UsageStateName { get; set; }
    }
}
