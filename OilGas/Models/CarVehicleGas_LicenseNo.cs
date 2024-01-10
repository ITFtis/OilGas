namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using OilGas._applyClass;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_LicenseNo
    {
        [DisplayName("序")]
        [ColumnDef(VisibleEdit = false)]
        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        [ColumnDef(Visible = false,VisibleEdit = false)]
        public string City { get; set; }

        [StringLength(3)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string CityCode { get; set; }

        [Required]
        [StringLength(3)]
        [DisplayName("年度")]
        [ColumnDef(EditType = EditType.Select, SelectItemsClassNamespace = LienceNoYearSelectItem.AssemblyQualifiedName)]
        public string Year { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string LicenseNo { get; set; }

        [StringLength(50)]
        [DisplayName("發文字號")]
        public string DispatchNo { get; set; }

        [StringLength(3)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Act { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? CreateTime { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible =false, VisibleEdit = false)]
        public string Creator { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? ModifyTime { get; set; }

        [StringLength(5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Modifier { get; set; }
    }
}
