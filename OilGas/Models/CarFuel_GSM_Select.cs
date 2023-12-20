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

    public partial class vw_CarFuel_GSM_Select
    {
        [ColumnDef(Display = "列管項目", EditType = EditType.Select,
            SelectItems = "{'0':'公告細八','1':'公告七條五','2':'公告地下水','3':'公告控制場址','4':'公告整治場址'}",
            DefaultValue = "0", Filter = true, Visible = false)]
        [NotMapped]
        public string CaseType { get; set; }

        [Display(Name = "縣市", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY { get; set; }

        [ColumnDef(Display = "案件編號")]
        public string CaseNo { get; set; }

        [Key]
        [ColumnDef(Display = "地下儲油槽管制編號")]
        public string gsm_id { get; set; }

        [ColumnDef(Display = "加油站名稱")]
        public string gsm_name { get; set; }

        [ColumnDef(Display = "gsm_field03", Visible = false)]
        public string gsm_field03 { get; set; }

        [ColumnDef(Display = "gsm_field07", Visible = false)]
        public string gsm_field07 { get; set; }

        [ColumnDef(Display = "gsm_register", Visible = false)]
        public string gsm_register { get; set; }

        [ColumnDef(Display = "gsm_field31", Visible = false)]
        public string gsm_field31 { get; set; }

        [ColumnDef(Display = "gsm_field30", Visible = false)]
        public string gsm_field30 { get; set; }

        [ColumnDef(Display = "公告污染場址類型")]
        public string Situation { get; set; }

        [ColumnDef(Display = "Limit_Date", Visible = false)]
        public DateTime? Limit_Date { get; set; }

        [ColumnDef(Display = "take_Date", Visible = false)]
        public DateTime? take_Date { get; set; }

        [ColumnDef(Display = "GW_Date", Visible = false)]
        public DateTime? GW_Date { get; set; }

        [ColumnDef(Display = "Control_Date", Visible = false)]
        public DateTime? Control_Date { get; set; }

        [ColumnDef(Display = "Rem_Date", Visible = false)]
        public DateTime? Rem_Date { get; set; }

        [ColumnDef(Display = "Post_Date", Visible = false)]
        public DateTime? Post_Date { get; set; }

        [ColumnDef(Display = "公告列管日期")]
        [NotMapped]
        public string LTGCR_Date { get; set; }

        [ColumnDef(Display = "解除列管日期", EditType = EditType.Date)]
        public DateTime? Situation_Date { get; set; }
    }
}

