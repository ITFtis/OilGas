namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using OilGas.Models.BASE;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Check_Item : Check_Item_BASE
    {


        [StringLength(1)]
        [Display(Name = "兩棚高度標誌完好", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G04 { get; set; }

        [StringLength(1)]
        [Display(Name = "燈柱(框架)及基座完好、無銹損", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G05 { get; set; }

        [StringLength(1)]
        [Display(Name = "接線蓋板完好", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G06 { get; set; }


        [StringLength(1)]
        [Display(Name = "「加油站設置管理規則」第29條:「經營加油站業務者，應依加油站營運設備目行安全檢查表自行實施加油站設施每日、每月及每半年檢查紀錄，應與實際相符，請且應保存一年以上。」", Order = 4)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string M01 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 4)]
        public string M_Notes { get; set; }













        [Display(Name = "查核編號", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Count { get; set; }

        [Display(Name = "查核編號", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Conform { get; set; }

        [Display(Name = "查核編號", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Doesmeet { get; set; }
        
        
    }
}
