namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;


    [Table("CounselingData")]
    public partial class CounselingData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int s_index { get; set; }

        [StringLength(10)]
        [Display(Name = "講習年度", Order = 1)]
        public string s_year { get; set; }

        [StringLength(255)]
        [Display(Name = "講習場次", Order = 1)]
        public string s_CounselingWork { get; set; }


        [StringLength(255)]
        [Display(Name = "職稱", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string s_PType { get; set; }

        [StringLength(255)]
        [Display(Name = "姓名", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string s_Person { get; set; }


        [StringLength(255)]
        [Display(Name = "加油站編號", Order = 1)]
        public string s_CaseNo { get; set; }

        [StringLength(255)]
        [Display(Name = "加油站名稱", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string s_GasName { get; set; }

        [StringLength(255)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string s_CityName { get; set; }

        [StringLength(255)]
        [Display(Name = "所屬縣市", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false, EditType = EditType.Select,
            SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
             SelectSourceModelNamespace = "OilGas.Models.CityCode, OilGas",
             SelectSourceModelValueField = "CityCode",
             SelectSourceModelDisplayField = "CityName")]
        public string s_CityCode { get; set; }

        [StringLength(10)]
        [Display(Name = "營業主體", Order = 1)]
        [ColumnDef( Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_BusinessOrganization, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        public string s_BusinessNo { get; set; }

        [StringLength(255)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string s_BusinessName { get; set; }





        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string s_num { get; set; }


    }
}
