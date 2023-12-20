namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarFuel_OilData
    {
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public long ID { get; set; }

        [StringLength(10)]
        [Display(Name = "案件編號", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }

        [StringLength(10)]
        [Display(Name = "販售油品種類", Order = 2)]
        [ColumnDef(EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_SaleSoilClass, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        public string SaleSoilClass { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Tank_no { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Tank_place_type { get; set; }

        [Display(Name = "儲槽容量", Order = 3)]
        public int? Tank_type_tank { get; set; }

        [StringLength(52)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string MemberID { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }


        [Display(Name = "儲槽數量", Order = 4)]
        public int? Tank_type_tank_seat { get; set; }
    }
}
