using Dou.Misc.Attr;
using OilGas._applyClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    [Table("z_CarVehicleGas_LicenseNo_Logs")]
    public class CarVehicleGas_LicenseNo_Log
    {
        
        [Display(Name = "序")]
        [ColumnDef(VisibleEdit = false)]
        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string City { get; set; }

        [StringLength(3)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string CityCode { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "年度")]
        [ColumnDef(EditType = EditType.Select, SelectItemsClassNamespace = LienceNoYearSelectItem.AssemblyQualifiedName)]
        public string Year { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string LicenseNo { get; set; }

        [StringLength(50)]
        [Display(Name = "發文字號")]
        public string DispatchNo { get; set; }

        [StringLength(3)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Act { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? CreateTime { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Creator { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? ModifyTime { get; set; }

        [StringLength(5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Modifier { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? DeleteTime { get; set; }

        [ColumnDef(Visible = false)]
        public string Deletor { get; set; }
    }
}