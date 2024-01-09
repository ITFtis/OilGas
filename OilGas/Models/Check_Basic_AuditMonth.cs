using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    /// <summary>
    /// 加油站營運設備每月自行安全檢查表
    /// </summary>
    [Table("Check_Basic_AuditMonth")]
    public class Check_Basic_AuditMonth
    {
        [Key]
        [StringLength(10)]
        [ColumnDef(EditType = EditType.TextList, SelectItemsClassNamespace = OilGas.Models.CarFuel_BasicDataCaseNoSelectItems.AssemblyQualifiedName)]
        [Display(Name = "加油站編號")]
        [Column(Order = 1)]
        public string CaseNo { get; set; }

        [Display(Name = "加油站名稱")]
        [StringLength(70)]
        [ColumnDef(EditType = EditType.TextList, SelectItemsClassNamespace = OilGas.Models.CarFuel_BasicDataGas_NameSelectItems.AssemblyQualifiedName)]
        public string Gas_Name { get; set; }

        [Key]
        [Display(Name = "檢查日期")]
        [Column(Order = 2)]
        public DateTime? CheckDate { get; set; }

        [Display(Name = "天氣")]
        [ColumnDef(EditType = EditType.Select, SelectItemsClassNamespace = WeatherSelectItems.AssemblyQualifiedName)]
        public int? Weather { get; set; }

        [Display(Name = "檢查人員")]
        [StringLength(50)]
        public string CheckMan { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? A01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string A01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A01_Note { get; set; }
    }
}