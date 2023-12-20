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

    public partial class vw_CarFuel_CaseError1
    {
        [Key]
        [ColumnDef(Display = "項次", Sortable = true)]
        public Int64 CaseIndex { get; set; }

        [ColumnDef(Display = "案件編號", Sortable = true)]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "石油設施", Sortable = true)]
        public string Gas_Name { get; set; }

        [ColumnDef(Display = "營業主體", Sortable = true)]
        public string Business_theme { get; set; }

        [ColumnDef(Display = "石油設施地址", Sortable = true)]
        public string Address { get; set; }

        [ColumnDef(Display = "異動日期", Sortable = true)]
        public string Mod_date { get; set; }

        [ColumnDef(Display = "最後營運狀態", Sortable = true)]
        public string UsageStateName { get; set; }

        [ColumnDef(Display = "最後發文日期", Sortable = true)]
        public string Last_Dispatch_date { get; set; }

        [ColumnDef(Display = "最後發文狀況", Sortable = true)]
        public string Last_DispatchName { get; set; }

    }
}
