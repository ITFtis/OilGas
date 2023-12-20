namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using OilGas.Models.BASE;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Item_Action: Check_Item_BASE
    {


        [Display(Name = "兩棚高度標誌完好", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{0:\"未設置\",1:\"良好\",2:\"不良好\",3:\"無法檢查\"}")]
        public int? G04 { get; set; }


        [Display(Name = "燈柱(框架)及基座完好、無銹損", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{0:\"未設置\",1:\"良好\",2:\"不良好\",3:\"無法檢查\"}")]
        public int? G05 { get; set; }


        [Display(Name = "接線蓋板完好", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{0:\"未設置\",1:\"良好\",2:\"不良好\",3:\"無法檢查\"}")]
        public int? G06 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Frequency { get; set; }

      
    }
}
