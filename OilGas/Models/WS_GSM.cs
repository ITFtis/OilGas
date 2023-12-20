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
        [Key]
        [Column(Order = 1)]
        [StringLength(10)] 
        public string CaseNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(12)] 
        public string gsm_id { get; set; }

        [StringLength(100)] 
        public string gsm_name { get; set; }

        [StringLength(100)] 
        public string gsm_field03 { get; set; }

        [StringLength(40)] 
        public string gsm_field07 { get; set; }

        [StringLength(40)] 
        public string gsm_register { get; set; }

        [StringLength(30)] 
        public string gsm_field31 { get; set; }

        [StringLength(30)] 
        public string gsm_field30 { get; set; }

        [StringLength(25)] 
        public string Situation { get; set; }

        public DateTime? Situation_Date { get; set; }

        public DateTime? Limit_Date { get; set; }

        public DateTime? take_Date { get; set; }

        public DateTime? GW_Date { get; set; }

        public DateTime? Control_Date { get; set; }

        public DateTime? Rem_Date { get; set; }
    }
}