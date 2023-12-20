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

    public partial class SelfFuel_WorkBusinessDesc
    {   
        [Key]
        [ColumnDef(Display = "自用加儲油站編號", Sortable = true)]
        [NotMapped]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "自用加儲油站名稱", Sortable = true)]
        [NotMapped]
        public string Fuel_Name { get; set; }

        [ColumnDef(Display = "營業主體", Visible = false, Sortable = true)]
        [NotMapped]
        public string Business_theme { get; set; }

        [ColumnDef(Display = "發文日期", Visible = false, Sortable = true)]
        [NotMapped]
        public string Dispatch_date { get; set; }

        [ColumnDef(Display = "發文文號", Visible = false, Sortable = true)]
        [NotMapped]
        public string Dispatch_No { get; set; }

        [ColumnDef(Display = "發文類別", Sortable = true)]
        [NotMapped]
        public string DispatchName { get; set; }

        [ColumnDef(Display = "縣市別", Sortable = true)]
        [NotMapped]
        public string CityName { get; set; }

    }
}
