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
    /// 加油站營運設備自行安全檢查表(無時間限制)
    /// </summary>
    [Table("Check_Basic_NoTime")]
    public class Check_Basic_NoTime
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

        [Display(Name = "檢查人員")]
        [StringLength(50)]
        public string CheckMan { get; set; }


    }
}