using Dou.Misc.Attr;
using Dou.Models.DB;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static OilGas.Models.GasFuleStationNameSelectItemsClassImp;

namespace OilGas.Models
{
    /// <summary>
    /// 加油站營運設備自行安全檢查表(無時間限制)
    /// </summary>
    [Table("Check_Basic_NoTime")]
    public class Check_Basic_NoTime
    {
        [Key]
        [StringLength(7)]
        [Display(Name ="查核編號")]
        
        public string Check_Number { get; set; }

        [StringLength(20)]
        [Required]
        [ColumnDef(EditType = EditType.TextList, SelectGearingWith = "Gas_Name,bt",
            SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
         SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_BusinessOrganization, OilGas"
            ,SelectSourceModelValueField ="Value"
            ,SelectSourceModelDisplayField ="Name")]
        [Display(Name = "營業主體")]
        public string Business_theme { get; set; }

        
        [StringLength(30)]
        [ColumnDef(Visible = false,EditType = EditType.TextList, SelectGearingWith = "Gas_Name,ob", SelectItemsClassNamespace = OtherBThemeSelectItemsClassImp.AssemblyQualifiedName)]
        [Display(Name = "其他營業主體")]
        public string otherBusiness_theme { get; set; }


        [Display(Name = "加油(氣)站名稱")]
        [Required]
        [StringLength(70)]
        [ColumnDef(EditType = EditType.TextList,SelectItemsClassNamespace = GasFuleStationNameSelectItemsClassImp.AssemblyQualifiedName)]
        public string Gas_Name { get; set; }


        [Required]
        [Display(Name = "檢查日期")]
        [Column(Order = 2)]
        public DateTime? CheckDate { get; set; }

        [StringLength(50)]
        [Display(Name = "地址")]
        [ColumnDef(EditType = EditType.Text)]
        public string Address { get; set; }

        [StringLength(10)]
        [Display(Name = "電話")]
        [ColumnDef(EditType = EditType.Text)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "檢查人員")]
        [StringLength(50)]
        public string CheckMan { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string A01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string A02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string A03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string A04 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string A05 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A05_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string A06 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string A06_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B04 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B05 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B05_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B06 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B06_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B07 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B07_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B08 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B08_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B09 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B09_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string B10 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string B10_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C04 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C05 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C05_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C06 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C06_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C07 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C07_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C08 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C08_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C09 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C09_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C10 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C10_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C11 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C11_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C12 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C12_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C13 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C13_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string C14 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string C14_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D04 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D05 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D05_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D06 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D06_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D07 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D07_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D08 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D08_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D09 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D09_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D10 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D10_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string D11 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string D11_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string E01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string E01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string E02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string E02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string E03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string E03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string F01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string F02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string F03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string F04 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string F05 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F05_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string F06 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F06_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string F07 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F07_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string F08 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F08_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string F09 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string F09_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string G01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string G01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string G02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string G02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string G03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string G03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string G04 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string G04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string G05 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string G05_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string G06 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string G06_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string H01 { get; set; }

        [Display(Name = "實際數值")]
        [ColumnDef(Visible = false)]
        public int H01_Value { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string H01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string H02 { get; set; }

        [Display(Name = "實際數值")]
        [ColumnDef(Visible = false)]
        public int H02_Value { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string H02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string H03 { get; set; }

        [Display(Name = "實際數值")]
        [ColumnDef(Visible = false)]
        public int H03_Value { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string H03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string H04 { get; set; }

        [Display(Name = "實際數值")]
        [ColumnDef(Visible = false)]
        public int H04_Value { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string H04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string H05 { get; set; }

        [Display(Name = "實際數值")]
        [ColumnDef(Visible = false)]
        public int H05_Value { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string H05_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I04 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I04_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I05 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I05_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I06 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I06_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I07 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I07_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I08 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I08_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I09 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I09_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string I10 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string I10_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string J01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string J01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string J02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string J02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string J03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string J03_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string K01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string K01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string K02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string k02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string L01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string L01_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string L02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string L02_Note { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果")]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItemsClassNamespace = CheckResultSelectItems.AssemblyQualifiedName)]
        public string L03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註")]
        [ColumnDef(Visible = false)]
        public string L03_Note { get; set; }


    }

    public class GasFuleStationNameSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.GasFuleStationNameSelectItemsClassImp, OilGas";

        //取得加油加氣站名稱

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            OilGasModelContextExt _db = new OilGasModelContextExt();
            IModelEntity<CarFuel_BasicData> _CarFuel_BasicData = new ModelEntity<CarFuel_BasicData>(_db);
            IModelEntity<CarGas_BasicData> _CarGas_BasicData = new ModelEntity<CarGas_BasicData>(_db);
            IModelEntity<CarVehicleGas_BusinessOrganization> _businessT = new ModelEntity<CarVehicleGas_BusinessOrganization>(_db);
            var buss = _businessT.GetAll().Where(x => x.IsEnable == true);

            var combine = _CarFuel_BasicData.GetAll().Select(c => new GasFuleStationName
            {
                StationName = c.Gas_Name,
                BusinessTheme = c.Business_theme,
                otherBusinessTheme = c.otherBusiness_theme,
                Address = c.Address,
            }).Concat(_CarGas_BasicData.GetAll().Select(x => new GasFuleStationName
            {
                StationName = x.Gas_Name,
                BusinessTheme = x.Business_theme,
                otherBusinessTheme = x.otherBusiness_theme,
                Address = x.Address,
            }));
            

            var combineJoin = combine.Join(buss, x => x.BusinessTheme, y => y.Value, (cb, bu) => new GasFuleStationName
            {
                StationName = cb.StationName.Trim(),
                BusinessTheme = bu.Name.Trim(),
                otherBusinessTheme = cb.otherBusinessTheme.Trim(),
                Address = cb.Address.Trim(),
            }).ToArray();


            var t = combineJoin.Select(z => new KeyValuePair<string, object>(z.StationName, JsonConvert.SerializeObject(new { v = z.StationName, ob = z.otherBusinessTheme, bt = z.BusinessTheme, add = z.Address })));

            return t;
        }

        public class GasFuleStationName
        {
            public string StationName { get; set; }
            public string BusinessTheme { get; set; }
            public string otherBusinessTheme { get; set; }
            public string Address { get; set; }
        }
    }

    //取得其他營業主體的清單
    public class OtherBThemeSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.OtherBThemeSelectItemsClassImp, OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            OilGasModelContextExt _db = new OilGasModelContextExt();
            IModelEntity<CarFuel_BasicData> _CarFuel_BasicData = new ModelEntity<CarFuel_BasicData>(_db);
            IModelEntity<CarGas_BasicData> _CarGas_BasicData = new ModelEntity<CarGas_BasicData>(_db);


            var combine = _CarFuel_BasicData.GetAll()
                .Where(x => !string.IsNullOrEmpty(x.otherBusiness_theme))
                .Select(x => new OtherTheme
                {
                    otherBusiness_theme = x.otherBusiness_theme.Trim(),
                })
                .ToArray()
                .Concat(_CarGas_BasicData.GetAll()
                .Where(y => !string.IsNullOrEmpty(y.otherBusiness_theme))
                .Select(y => new OtherTheme
                {
                    otherBusiness_theme = y.otherBusiness_theme.Trim(),
                })
                .DistinctBy(a => a.otherBusiness_theme)).ToArray();

            return combine.Select(z => new KeyValuePair<string, object>(z.otherBusiness_theme, JsonConvert.SerializeObject(new { v = z.otherBusiness_theme })));
        }

        public class OtherTheme
        {
            public string otherBusiness_theme { get; set; }
        }
    }



}