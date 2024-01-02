using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace OilGas.Models
{
    /// <summary>
    /// 加油站營運設備每日自行安全檢查表
    /// </summary>
    [Table("Check_Basic_AuditDay")]
    public class Check_Basic_AuditDay
    {
        [Key]
        [StringLength(10)]
        [ColumnDef(EditType = EditType.TextList, SelectItemsClassNamespace = OilGas.Models.CarFuel_BasicDataCaseNoSelectItems.AssemblyQualifiedName)]
        [Display(Name = "加油站編號")]
        public string CaseNo { get; set; }

        [Display(Name = "加油站名稱")]
        [StringLength(70)]
        [ColumnDef(EditType = EditType.TextList, SelectItemsClassNamespace = OilGas.Models.CarFuel_BasicDataGas_NameSelectItems.AssemblyQualifiedName)]
        public string Gas_Name { get; set; }

        [Display(Name = "檢查日期")]
        public DateTime? CheckDate { get; set; }

        [Display(Name = "天氣")]
        public int? Weather { get; set; }

        [Display(Name = "檢查人員")]
        [StringLength(50)]
        public string CheckMan { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]        
        public string A01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false)]
        public int? A01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string A01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A02 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false)]
        public int A02_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string A02_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A03 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false)]
        public int A03_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string A03_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A04 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false)]
        public int A04_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string A04_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A05 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false)]
        public int A05_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string A05_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A05_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A06 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false)]
        public int A06_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string A06_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A06_Note { get; set; }

    }
}