using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    [Table("WS_GSM")]
    public class WS_GSM
    {
        
        [Column(Order = 1)]
        [StringLength(10)]
        [ColumnDef(Visible = false)]
        public string CaseNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(12)]
        [Display(Name = "管制編號")]
        public string gsm_id { get; set; }

        [StringLength(100)]
        [Display(Name = "加油站名稱")]
        public string gsm_name { get; set; }

        [StringLength(100)]
        [Display(Name = "加油站地址")]
        public string gsm_field03 { get; set; }

        [StringLength(40)]
        [ColumnDef(Visible = false)]
        public string gsm_field07 { get; set; }

        [StringLength(40)]
        [ColumnDef(Visible = false)]
        public string gsm_register { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        public string gsm_field31 { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        public string gsm_field30 { get; set; }

        [StringLength(25)]
        [Display(Name = "公告污染場址類型")]
        public string Situation { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Situation_Date { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Limit_Date { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? take_Date { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? GW_Date { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Control_Date { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Rem_Date { get; set; }
    }
}