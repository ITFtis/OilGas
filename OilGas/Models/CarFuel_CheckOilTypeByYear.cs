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

    public partial class vw_CarFuel_CheckOilTypeByYear
    {   
        [Key]
        [ColumnDef(Display = "縣市別", Sortable = true)]
        public string CityName { get; set; }

        [ColumnDef(Display = "縣市排序", Visible = false, Sortable = true)]
        public int CityRank { get; set; }

        [ColumnDef(Display = "中油自營", Sortable = true)]
        public int CPC { get; set; }

        [ColumnDef(Display = "中油加盟", Sortable = true)]
        public int CPC_Join { get; set; }

        [ColumnDef(Display = "台塑加盟", Sortable = true)]
        public int FPCC_Join { get; set; }

    }
}
