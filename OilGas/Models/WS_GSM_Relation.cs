using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    [Table("WS_GSM_Relation")]
    public class WS_GSM_Relation
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }
        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string FacNo { get; set; }
    }
}