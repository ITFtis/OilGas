namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Item_Fish103_Action
    {
        public int id { get; set; }

        [StringLength(10)]
        public string CheckNo { get; set; }

        public DateTime? CheckDate { get; set; }

        [StringLength(1)]
        public string A01 { get; set; }

        [StringLength(1)]
        public string A02 { get; set; }

        [Column(TypeName = "ntext")]
        public string A_Notes { get; set; }

        [StringLength(1)]
        public string B01 { get; set; }

        [StringLength(1)]
        public string B02 { get; set; }

        [Column(TypeName = "ntext")]
        public string B_Notes { get; set; }

        [StringLength(1)]
        public string C01 { get; set; }

        [StringLength(1)]
        public string C02 { get; set; }

        [StringLength(1)]
        public string C03 { get; set; }

        [Column(TypeName = "ntext")]
        public string C_Notes { get; set; }

        [StringLength(1)]
        public string D01 { get; set; }

        [StringLength(1)]
        public string D02 { get; set; }

        [StringLength(1)]
        public string D03 { get; set; }

        [Column(TypeName = "ntext")]
        public string D_Notes { get; set; }

        [StringLength(1)]
        public string F01 { get; set; }

        [StringLength(1)]
        public string F02 { get; set; }

        [StringLength(1)]
        public string F03 { get; set; }

        [StringLength(1)]
        public string F04 { get; set; }

        [StringLength(1)]
        public string F05 { get; set; }

        [Column(TypeName = "ntext")]
        public string F_Notes { get; set; }

        [StringLength(1)]
        public string G01 { get; set; }

        [StringLength(1)]
        public string G02 { get; set; }

        [StringLength(1)]
        public string G03 { get; set; }

        [StringLength(1)]
        public string G04 { get; set; }

        [Column(TypeName = "ntext")]
        public string G_Notes { get; set; }

        [StringLength(1)]
        public string H01 { get; set; }

        [StringLength(1)]
        public string H02 { get; set; }

        [StringLength(1)]
        public string H03 { get; set; }

        [Column(TypeName = "ntext")]
        public string H_Notes { get; set; }

        [StringLength(10)]
        public string H01_Value { get; set; }

        [StringLength(10)]
        public string H02_Value { get; set; }

        [StringLength(10)]
        public string H03_Value { get; set; }

        public int? A_Count { get; set; }

        public int? A_Conform { get; set; }

        public int? A_Doesmeet { get; set; }

        public int? A_Unable { get; set; }

        public int? B_Count { get; set; }

        public int? B_Conform { get; set; }

        public int? B_Doesmeet { get; set; }

        public int? B_Unable { get; set; }

        public int? C_Count { get; set; }

        public int? C_Conform { get; set; }

        public int? C_Doesmeet { get; set; }

        public int? C_Unable { get; set; }

        public int? D_Count { get; set; }

        public int? D_Conform { get; set; }

        public int? D_Doesmeet { get; set; }

        public int? D_Unable { get; set; }

        public int? F_Count { get; set; }

        public int? F_Conform { get; set; }

        public int? F_Doesmeet { get; set; }

        public int? F_Unable { get; set; }

        public int? G_Count { get; set; }

        public int? G_Conform { get; set; }

        public int? G_Doesmeet { get; set; }

        public int? G_Unable { get; set; }

        public int? H_Count { get; set; }

        public int? H_Conform { get; set; }

        public int? H_Doesmeet { get; set; }

        public int? H_Unable { get; set; }

        public int? AllCount { get; set; }

        public int? AllConform { get; set; }

        public int? AllDoesmeet { get; set; }

        public int? AllUnable { get; set; }

        [StringLength(50)]
        public string CaseNo { get; set; }

        public int? Change { get; set; }

        public int? Frequency { get; set; }
    }
}
