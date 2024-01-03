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

        [StringLength(1)]
        [Display(Name = "檢查結果")]        
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A02 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? A02_Way { get; set; }

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
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A03 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? A03_Way { get; set; }

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
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A04 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? A04_Way { get; set; }

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
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A05 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? A05_Way { get; set; }

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
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string A06 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? A06_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string A06_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A06_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string B01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? B01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string B01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string B02 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? B02_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string B02_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string C01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? C01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string C01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string C02 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? C02_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string C02_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string C03 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? C03_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string C03_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string D01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? D01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string D01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string D02 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? D02_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string D02_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string D03 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? D03_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string D03_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string D04 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? D04_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string D04_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string E01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? E01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string E01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string E01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string E02 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? E02_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string E02_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string E02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string E03 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? E03_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string E03_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string E03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string F01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? F01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string F01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string F02 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? F02_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string F02_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string F03 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? F03_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string F03_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string G01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? G01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string G01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string G01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string H01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? H01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string H01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string H01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string H02 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? H02_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string H02_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string H02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string H03 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? H03_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string H03_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string H03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string H04 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? H04_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string H04_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string H04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string I01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? I01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string I01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string I02 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? I02_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string I02_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"N\":\"不符合\",\"Y\":\"符合\"}")]
        public string j01 { get; set; }

        [Display(Name = "檢查方法")]
        [ColumnDef(Visible = false,
            EditType = EditType.Select, SelectItemsClassNamespace = CheckWaySelectItems.AssemblyQualifiedName)]
        public int? j01_Way { get; set; }

        [StringLength(300)]
        [Display(Name = "改善情形")]
        [ColumnDef(Visible = false)]
        public string j01_Improve { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string j01_Note { get; set; }
    }
}