using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    [Table("gas_total_tempV")]
    public class gas_total_tempV
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(17)]
        public string CaseType { get; set; }

        [StringLength(20)]
        public string Name { get; set; }
        
        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string value { get; set; }

        [StringLength(4)]
        public string ShortName { get; set; }
        
        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string area { get; set; }

        [StringLength(5)]
        public string CityName { get; set; }

        [StringLength(10)]
        public string GSLCode { get; set; }

        public int Total { get; set; }
    }
}