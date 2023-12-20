namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class SelfFuel_Land
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        [Display(Name = "案件編號", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }



        [Display(Name = "土地權屬", Order = 2)]
        [ColumnDef(EditType = EditType.Select,
 SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
  SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_LandPriority, OilGas",
  SelectSourceModelValueField = "Value",
  SelectSourceModelDisplayField = "Name")]
        [StringLength(2)]
        public string LandPriority { get; set; }

        [Display(Name = "用地總面積", Order = 3)]
        public double? LandTotalSquare { get; set; }

        [StringLength(2)]
        [Display(Name = "土地使用分區", Order = 4)]
        [ColumnDef(EditType = EditType.Select,
 SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
  SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_LandUsageZone, OilGas",
  SelectSourceModelValueField = "Value",
  SelectSourceModelDisplayField = "Name")]
        public string LandUsageZone { get; set; }

        [StringLength(10)]
        [Display(Name = "其他土地使用分區", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string OtherLandUsageZone { get; set; }

        [StringLength(2)]
        [Display(Name = "土地使用分區", Order = 6)]
        [ColumnDef(EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_LandClass, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        public string LandClass { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(10)]
        public string CreateUserTemp { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? IsConfirm { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
