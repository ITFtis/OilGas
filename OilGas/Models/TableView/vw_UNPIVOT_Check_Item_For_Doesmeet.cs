using DouHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace OilGas
{
    [Table("vw_UNPIVOT_Check_Item_For_Doesmeet")]
    public class vw_UNPIVOT_Check_Item_For_Doesmeet
    {
        public string workTable { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CheckNo { get; set; }

        public DateTime? CheckDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public string name { get; set; }
        
        public int value { get; set; }
    }
}