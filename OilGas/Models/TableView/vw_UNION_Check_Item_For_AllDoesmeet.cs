using DouHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models.TableView
{
    [Table("vw_UNION_Check_Item_For_AllDoesmeet")]
    public class vw_UNION_Check_Item_For_AllDoesmeet
    {
        public string workTable { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CheckNo { get; set; }
                
        public int? AllDoesmeet { get; set; }        
    }
}