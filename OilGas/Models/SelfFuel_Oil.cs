namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static OilGas.Controllers.basicController;

    public partial class SelfFuel_Oil
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        [ColumnDef(VisibleEdit = false)]
        [Display(Name = "案件編號", Order = 1)]
        public string CaseNo { get; set; }

        [StringLength(2)]
        [Display(Name = "販售油品種類", Order = 2)]
        [ColumnDef(EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_SaleSoilClass, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        public string SoilClass { get; set; }

        [Display(Name = "儲槽容量", Order = 3)]
        public double? TroughCapacity { get; set; }

        [Display(Name = "地上儲槽數量", Order = 4)]
        public int? Ground { get; set; }

        [Display(Name = "地下儲槽數量", Order = 5)]
        public int? UnderGround { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string CreateUserTemp { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? IsConfirm { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
