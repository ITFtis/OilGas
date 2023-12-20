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

    public partial class vw_CarFuel_CarVehicleGas_CheckError1
    {
        [Key]
        [ColumnDef(Display = "項次", Sortable = true)]
        public Int64 ItemIndex { get; set; }

        [ColumnDef(Display = "查核編號", Sortable = true)]
        public string CheckNo { get; set; }

        [ColumnDef(Display = "查核日期", Sortable = true)]
        public string CheckDate { get; set; }

        [ColumnDef(Display = "設施案號", Sortable = true)]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "名稱", Sortable = true)]
        public string Check_Gas_Name { get; set; }

        [ColumnDef(Display = "營業主體", Sortable = true)]
        public string Check_Business { get; set; }

        [ColumnDef(Display = "地址", Sortable = true)]
        public string Check_Addr { get; set; }

        [ColumnDef(Display = "名稱", Sortable = true)]
        public string Case_Gas_Name { get; set; }

        [ColumnDef(Display = "營業主體", Sortable = true)]
        public string Case_Business { get; set; }

        [ColumnDef(Display = "地址", Sortable = true)]
        public string Case_Addr { get; set; }

        [ColumnDef(Display = "", Sortable = true)]
        public string Case_UsageState { get; set; }

    }
}
