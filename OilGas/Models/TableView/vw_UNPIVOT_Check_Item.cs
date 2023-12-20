using DouHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas
{
    //資料量大
    [Table("vw_UNPIVOT_Check_Item")]
    public partial class vw_UNPIVOT_Check_Item
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CheckNo { get; set; }
        [StringLength(20)]

        [Key]
        [Column(Order = 2)]
        public string name { get; set; }
        [StringLength(5)]
        public string value { get; set; }
    }

    //資料量小(效能考量，拆出Check_Item)
    [Table("vw_UNPIVOT_Check_Item_Other")]
    public partial class vw_UNPIVOT_Check_Item_Other
    {
        public string workTable { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CheckNo { get; set; }
        [StringLength(20)]

        [Key]
        [Column(Order = 2)]
        public string name { get; set; }
        [StringLength(5)]
        public string value { get; set; }
    }
}